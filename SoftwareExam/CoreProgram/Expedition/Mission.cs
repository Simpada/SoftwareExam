using SoftwareExam.CoreProgram.Adventurers;

namespace SoftwareExam.CoreProgram.Expedition
{
    public class Mission
    {
        private Player Player;
        public Adventurer Adventurer { get; set; }
        public Map Map { get; set; } = new();
        public List<Encounter> Encounters { get; set; } = new();
        public Currency Reward;
        public string LogMessage = "";
        public float TimeLeft = 0;

        public Mission (Player player, Map map, Adventurer adventurer) {
            Player = player;
            Adventurer = adventurer;
            Map = map;
            for (int i = 0; i < Map.Encounters; i++) {
                Encounters.Add(new Encounter());
            }
            Reward = map.Reward;


            Adventurer.OnMission = true;
            StartMission();
        }

        
        private async void UpdateLog() {

            if (Player.Log.Count >= 5) {
                Player.AddLogMessage(LogMessage);

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 5);
                for (int i = 0; i < Player.Log.Count; i++) {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 5);
                foreach (string message in Player.Log) {
                    Console.WriteLine(message);
                }

            } else {
                Console.WriteLine(LogMessage);
                Player.AddLogMessage(LogMessage);
            }
        }

        private async void StartMission() {

            Task Encounter = RunEncounter();
            LogMessage = $"    - {Adventurer.Name} has headed towards {Map.Location}";
            UpdateLog();

            while (Encounters.Count > 0) {

                await Task.WhenAny(Encounter);

                // Update Player log array

                Encounters.Remove(Encounters[^1]);
                Encounter = RunEncounter();

                UpdateLog();
            }

            await Task.Delay(5000);
            LogMessage = $"    - {Adventurer.Name} has returned!";
            UpdateLog();
            Adventurer.OnMission = false;
            // Update Player Currency
        }

        private async Task RunEncounter() {
            await Task.Delay(5000);
            LogMessage = $"    - {Adventurer.Name} wandered in circles";
        }

    }
}
