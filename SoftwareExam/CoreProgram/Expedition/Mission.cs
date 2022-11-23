using SoftwareExam.CoreProgram.Adventurers;

namespace SoftwareExam.CoreProgram.Expedition
{
    public class Mission
    {
        private readonly Player Player;
        private readonly LogWriter LogWriter;
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

        public Mission(Player player, Adventurer adventurer, LogWriter logWriter) {

            Player = player;
            Adventurer = adventurer;
            WaitTimes = new int[EncounterNumber];

            Player.Missions.Add(this);
            Adventurer.OnMission = true;

            StartMission();
            LogWriter = logWriter;
        }

        public Mission (Player player, Map map, Adventurer adventurer, LogWriter logWriter) {
            Player = player;
            LogWriter = logWriter;
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
        
        //private void UpdateLog(Player player, string logMessage) {

        //    /*
        //    - This has a huge issue
        //    It is very much not thread save, and of triggered just as a window shifts, it might get printed very wrong,
        //    Must find a way to block this action unless a condition is met
        //    Maybe use a ManualResetEvent? giving UI priority, so it always get to finish running
        //     */

        //    if (player.Log.Count >= 5) {
        //        player.AddLogMessage(logMessage);

        //        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - player.Log.Count);
        //        for (int i = 0; i < player.Log.Count; i++) {
        //            Console.WriteLine(new string(' ', Console.WindowWidth));
        //        }

        //        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - player.Log.Count);
        //        //foreach (string message in Player.Log) {
        //        //    Console.WriteLine(message);
        //        //}
        //        Console.WriteLine(player.GetLogMessages());
                
        //    } else {
        //        Console.WriteLine(logMessage);
        //        player.AddLogMessage(logMessage);
        //    }
        //}

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
            LogWriter.UpdateLog(Player, LogMessage);

            for (int i = 0; i < Encounters.Count; i++) {
                Task Encounter = RunEncounter(Encounters[i], WaitTimes[i]);

                await Task.WhenAny(Encounter);
                LogWriter.UpdateLog(Player, LogMessage);
            }

            await Task.Delay(5000);
            LogMessage = $"    - {Adventurer.Name} has returned!";
            LogWriter.UpdateLog(Player, LogMessage);
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
