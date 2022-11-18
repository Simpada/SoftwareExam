using SoftwareExam.CoreProgram.Adventurers;

namespace SoftwareExam.CoreProgram.Expedition
{
    public class Mission
    {
        private readonly Player Player;
        public Adventurer Adventurer { get; set; }
        public Map? Map { get; set; }
        public List<Encounter> Encounters { get; set; } = new();
        public int EncounterNumber { get; set; }
        public string Destination { get; set; } = "";
        public Currency Reward = new();
        private string LogMessage = "";
        public int TimeLeft = 0;
        private readonly int[] WaitTimes;
        private readonly Random Random = new();

        public Mission(Player player, Adventurer adventurer) {

            Player = player;
            Adventurer = adventurer;
            WaitTimes = new int[EncounterNumber];

            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            StartMission();
        }

        public Mission (Player player, Map map, Adventurer adventurer) {
            Player = player;
            Adventurer = adventurer;
            Map = map;
            for (int i = 0; i < Map.Encounters; i++) {
                Encounters.Add(new Encounter());
            }
            EncounterNumber = Encounters.Count;
            WaitTimes = new int[EncounterNumber];
            Reward = map.Reward;
            Destination = map.Location;

            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            TimeLeft = Random.Next(Encounters.Count * 10) + Encounters.Count * 10;
            StartMission();
        }
        
        private void UpdateLog() {

            /*
            - This has a huge issue
            It is very much not thread save, and of triggered just as a window shifts, it might get printed very wrong,
            Must find a way to block this action unless a condition is met
            Maybe use a ManualResetEvent? giving UI priority, so it always get to finish running
             */

            if (Player.Log.Count >= 5) {
                Player.AddLogMessage(LogMessage);

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - Player.Log.Count);
                for (int i = 0; i < Player.Log.Count; i++) {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - Player.Log.Count);
                //foreach (string message in Player.Log) {
                //    Console.WriteLine(message);
                //}
                Console.WriteLine(Player.GetLogMessages());
                
            } else {
                Console.WriteLine(LogMessage);
                Player.AddLogMessage(LogMessage);
            }
        }

        private async void StartMission() {

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


            LogMessage = $"    - {Adventurer.Name} has headed towards {Destination}";
            UpdateLog();

            for (int i = 0; i < Encounters.Count; i++) {
                Task Encounter = RunEncounter(Encounters[i], WaitTimes[i]);

                await Task.WhenAny(Encounter);
                UpdateLog();
            }

            await Task.Delay(5000);
            LogMessage = $"    - {Adventurer.Name} has returned!";
            UpdateLog();
            Adventurer.OnMission = false;
            // Update Player Currency
        }

        private async Task RunEncounter(Encounter encounter, int encounterTime) {
            await Task.Delay(encounterTime * 1000);
            TimeLeft -= encounterTime;
            LogMessage = $"    - {Adventurer.Name} wandered in circles for {encounterTime} hours";

            EncounterNumber--;
        }
    }
}
