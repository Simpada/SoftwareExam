using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;
using SoftwareExam.CoreProgram.Expedition.Encounters;
using SoftwareExam.CoreProgram.Expedition.Encounters.Factory;

namespace SoftwareExam.CoreProgram.Expedition {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Represents a mission that an adventurer undertakes
    /// Handles generation of encounters, and the general running of the missions
    /// Uses threads to allow multiple missions to be active while the player can still do other things
    /// </summary>
    public class Mission {
        public Player Player { set; private get; }
        public LogWriter LogWriter { set; private get; }
        public Adventurer Adventurer { get; set; }
        public int AdventurerHealth { get; set; } = 0;
        public int AdventurerId { get; set; } = -1;
        public List<Encounter> Encounters { get; set; } = new();
        public int EncounterNumber { get; set; }
        public string Destination { get; set; } = "";
        public Currency Reward { get; set; } = new();
        public Currency CompletionReward { get; set; } = new();
        public int TimeLeft { get; set; } = 0;
        public bool Completed { get; set; } = false;

        private bool _terminated = false;
        private CancellationTokenSource _tokenSource = new();
        private readonly CancellationToken _token;
        private readonly ManualResetEvent _taskPauseEvent = new(true);

        private string LogMessage = "";
        private int[] WaitTimes;
        private readonly Random Random = new();

        public Mission() {
            _token = _tokenSource.Token;
        }

        public Mission(Player player, Map map, Adventurer adventurer, LogWriter logWriter) {
            _token = _tokenSource.Token;

            Player = player;
            LogWriter = logWriter;
            Adventurer = adventurer;
            AdventurerHealth = Adventurer.Health;
            EncounterNumber = map.Encounters;

            GenerateEncounters();

            WaitTimes = new int[EncounterNumber];
            CompletionReward = map.Reward;
            Destination = map.Location;

            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            TimeLeft = Random.Next(Encounters.Count * 10) + Encounters.Count * 10;
            PrepareMission(false);
        }
        public void Pause() {
            _taskPauseEvent.Reset();
        }
        public void Resume() {
            _taskPauseEvent.Set();
        }
        /// <summary>
        /// Terminates the running mission and its threads
        /// </summary>
        public void Terminate() {
            _terminated = true;
            _tokenSource.Cancel();
        }

        #region Setup

        /// <summary>
        /// Generates encounters for the missions based on encounter number
        /// </summary>
        private void GenerateEncounters() {
            for (int i = 0; i < EncounterNumber; i++) {

                int encounterType = Random.Next(20) + Adventurer.Luck;
                IEncounterFactory encounterFactory = encounterType switch {
                    >= 20 => new TreasureFactory(),
                    >= 14 => new MonsterFactory(),
                    >= 8 => new ExplorationFactory(),
                    _ => new TrapFactory(),
                };
                Encounters.Add(encounterFactory.CreateEncounter(Adventurer.Name, Adventurer.Luck, Adventurer.Damage));
            }
        }

        public void Start() {
            GenerateEncounters();
            WaitTimes = new int[EncounterNumber];
            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            PrepareMission(true);
        }

        /// <summary>
        /// Prepares a mission by setting up random values
        /// </summary>
        /// <param name="resume">To determine if its a new mission, or continuing one after a load</param>
        private void PrepareMission(bool resume) {

            int[] encounterTimes = new int[Encounters.Count];

            for (int i = 0; i < Encounters.Count; i++) {
                int time = Random.Next(TimeLeft - 10) + 10;
                encounterTimes[i] = time;
            }
            Array.Sort(encounterTimes);


            int lastTime = 0;
            for (int i = 0; i < Encounters.Count; i++) {
                int time = encounterTimes[i] - lastTime;
                if (time <= 0) {
                    time = 1;
                }
                WaitTimes[i] = time;
                lastTime = encounterTimes[i];
            }

            StartMission(resume);
        }
        #endregion

        #region Running The Mission
        /// <summary>
        /// Runs the mission, starting tasks that run each encounter. The function is async, allowing it to run on its own
        /// </summary>
        /// <param name="resume">To determine if its a new mission, or continuing one after a load</param>
        private async void StartMission(bool resume) {
            if (!resume) {
                LogMessage = $"    - {Adventurer.Name} has headed towards {Destination}";
                LogWriter.UpdateLog(Player, LogMessage);
            }

            for (int i = 0; i < Encounters.Count; i++) {
                Task encounter = RunEncounter(Encounters[i], WaitTimes[i]);

                await Task.WhenAny(encounter);
                _taskPauseEvent.WaitOne();

                if (_terminated) {
                    break;
                } else {
                    LogWriter.UpdateLog(Player, LogMessage);
                }
            }

            if (!_terminated) {
                try {
                    await Task.Delay(5000, _token);
                } catch (Exception) {
                }
                _taskPauseEvent.WaitOne();

                Reward += CompletionReward;

                LogMessage = $"    - {Adventurer.Name} has returned! You have earned {Reward}.";
                CompleteMission();
            }
        }

        private void CompleteMission() {
            LogWriter.UpdateLog(Player, LogMessage);
            Adventurer.OnMission = false;

            Completed = true;
            Player.CompleteMission();
        }

        private async Task RunEncounter(Encounter encounter, int encounterTime) {
            try {
                await Task.Delay(encounterTime * 1000, _token);
            } catch (Exception) {
            }
            TimeLeft -= encounterTime;
            bool success = Encounters[EncounterNumber - 1].RunEncounter(out Currency reward, out string description);
            LogMessage = "    - " + description;

            if (success) {
                Reward += reward;
            } else {
                AdventurerHealth--;
                if (AdventurerHealth <= 0) {
                    LogMessage = $"    - {Adventurer.Name} is wounded and has returned without the full reward! You have earned {Reward}.";
                    CompleteMission();
                    Terminate();
                }
            }
            EncounterNumber--;
        }
        #endregion
    }
}
