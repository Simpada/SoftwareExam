using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Expedition.Encounters;
using SoftwareExam.CoreProgram.Expedition.Encounters.Factory;
using System.Diagnostics;

namespace SoftwareExam.CoreProgram.Expedition
{
    public class Mission
    {
        public Player Player { set; private get; }
        public LogWriter LogWriter { set; private get; }
        private readonly ManualResetEvent TaskPauseEvent = new(true);
        public Adventurer Adventurer { get; set; }
        public int AdventurerId { get; set; } = -1;
        public Map? Map { get; set; }
        public List<Encounter> Encounters { get; set; } = new();
        public int EncounterNumber { get; set; }
        public string Destination { get; set; } = "";
        public Currency Reward { get; set; } = new();
        private string LogMessage = "";
        public int TimeLeft { get; set; } = 0;
        public bool Completed { get; set; } = false;
        private bool Terminated = false;

        public CancellationTokenSource TokenSource = new();
        private readonly CancellationToken Token;

        private int[] WaitTimes;
        private readonly Random Random = new();

        public Mission()
        {
            Token = TokenSource.Token;
        }

        public Mission (Player player, Map map, Adventurer adventurer, LogWriter logWriter) {
            Token = TokenSource.Token;

            Player = player;
            LogWriter = logWriter;
            Adventurer = adventurer;
            Map = map;
            for (int i = 0; i < Map.Encounters; i++) {

                int encounterType = Random.Next(20) + Adventurer.Luck;
                EncounterFactory encounterFactory;

                switch (encounterType) {
                    case >= 18:
                        encounterFactory = new TreasureFactory();
                        break;
                    case >= 12:
                        encounterFactory = new MonsterFactory();
                        break;
                    case >= 6:
                        encounterFactory = new ExplorationFactory();
                        break;
                    default:
                        encounterFactory = new TrapFactory();
                        break;
                }
                Encounters.Add(encounterFactory.CreateEncounter());
            }

            EncounterNumber = Encounters.Count;
            WaitTimes = new int[EncounterNumber];
            Reward = map.Reward;
            Destination = map.Location;

            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            TimeLeft = Random.Next(Encounters.Count * 10) + Encounters.Count * 10;
            PrepareMission(false);
        }

        public void Start()
        {
            for (int i = 0; i < EncounterNumber; i++) {
                Encounters.Add(new Encounter());

            }
            WaitTimes = new int[EncounterNumber];
            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            PrepareMission(true);
        }

        private void PrepareMission(bool resume)
        {

            int[] EncounterTimes = new int[Encounters.Count];

            for (int i = 0; i < Encounters.Count; i++) {
                int time = Random.Next(TimeLeft - 10) + 10;
                EncounterTimes[i] = time;
            }
            Array.Sort(EncounterTimes);


            int lastTime = 0;
            for (int i = 0; i < Encounters.Count; i++) {
                int time = EncounterTimes[i] - lastTime;
                if (time <= 0) {
                    time = 1;
                }
                WaitTimes[i] = time;
                lastTime = EncounterTimes[i];
            }

            StartMission(resume);
            //MissionTask.Start();
        }

        private async void StartMission(bool resume)
        {
            if (!resume) {
                LogMessage = $"    - {Adventurer.Name} has headed towards {Destination}";
                LogWriter.UpdateLog(Player, LogMessage);
            }

            for (int i = 0; i < Encounters.Count; i++) {
                Task Encounter = RunEncounter(Encounters[i], WaitTimes[i]);

                await Task.WhenAny(Encounter);

                if (Terminated) {
                    break;
                } else {
                    LogWriter.UpdateLog(Player, LogMessage);
                }

            }

            if (!Terminated) {

                try {
                    await Task.Delay(5000, Token);
                }
                catch (Exception e){
                }
                
                LogMessage = $"    - {Adventurer.Name} has returned!";
                LogWriter.UpdateLog(Player, LogMessage);
                Adventurer.OnMission = false;

                Completed = true;
                Player.CompleteMission();
            }


        }

        private async Task RunEncounter(Encounter encounter, int encounterTime) {
            try {
                await Task.Delay(encounterTime * 1000, Token);
            }
            catch (Exception e) {
            }
            TimeLeft -= encounterTime;
            LogMessage = $"    - {Adventurer.Name} wandered in circles for {encounterTime} hours";
            EncounterNumber--;
        }

        public void Pause() {
            TaskPauseEvent.Reset();
        }

        public void Resume() {
            TaskPauseEvent.Set();
        }

        public void Terminate()
        {
            Terminated = true;
            TokenSource.Cancel();
        }
    }
}
