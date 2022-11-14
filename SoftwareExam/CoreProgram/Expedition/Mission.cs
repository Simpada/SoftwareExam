using SoftwareExam.CoreProgram.Adventurers;

namespace SoftwareExam.CoreProgram.Expedition
{
    public class Mission
    {
        public Adventurer Adventurer { get; set; }
        public Map Map { get; set; } = new();
        public List<Encounter> Encounters { get; set; } = new();
        public Currency Reward;
        public string LogMessage = "";
        public float TimeLeft = 0;

        public Mission (Map map, Adventurer adventurer) {
            Adventurer = adventurer;
            Map = map;
            for (int i = 0; i < Map.Encounters; i++) {
                Encounters.Add(new Encounter());
            }
            Reward = map.Reward;

            StartMission();
        }

        private async void StartMission() {

            Task Encounter = RunEncounter();

            while (Encounters.Count > 0) {

                await Task.WhenAny(Encounter);

                Console.WriteLine(LogMessage);
                // Update Player log array

                Encounters.Remove(Encounters[^1]);
                Encounter = RunEncounter();
            }

            // Update Player Currency
        }

        private async Task RunEncounter() {
            await Task.Delay(15000);
            LogMessage = "nothing happened";
        }

    }
}
