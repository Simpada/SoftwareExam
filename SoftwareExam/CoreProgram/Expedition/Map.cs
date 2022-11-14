namespace SoftwareExam.CoreProgram.Expedition {

    public class Map : IComparable<Map> {

        public enum Difficulties {
            EASY = 0,
            MEDIUM = 1,
            HARD = 2,
            LEGENDARY = 3
        }

        public int Encounters { get; set; }
        public Currency ExpeditionCost { get; set; } = new Currency();
        public Currency Reward { get; set; } = new Currency();
        public string Location { get; set; } = "";
        public Difficulties Difficulty { get; set; }

        public static Map GetMap(int difficulty) {

            Random Random = new();

            int gold = (difficulty + 1) * (Random.Next(2) + 1);
            int silver = (difficulty + 1) * (Random.Next(4) + 1);
            int copper = (difficulty + 1) * (Random.Next(6) + 1);

            Map Map = new() {
                Difficulty = (Difficulties)difficulty,
                Reward = Currency.Convert(new Currency(copper, silver, gold)),
                ExpeditionCost = new Currency(0, 0, 3) * (double)difficulty,
                Encounters = (difficulty + 1) * (Random.Next(3) + 1),
                Location = GetLocation(Random)
            };

            return Map;
        }
        public override string ToString() {

            return $@"
    |
    |   {Difficulty} MAP
    |
    |   Expedition cost: {ExpeditionCost}
    |   Expedition to {Location}
    |   
    |   Reward: {Reward}
    |";

        }
        
        public static Map GetMap(int difficulty, int copper, int silver, int gold, int encounters, string location) {

            Map Map = new() {
                Difficulty = (Difficulties)difficulty,
                Reward = new Currency(copper, silver, gold),
                Encounters = encounters,
                Location = location
            };

            return Map;
        }

        public static string GetLocation(Random random) {
            string[] Locations = {
                "The Peaks of Fire",
                "The Fields of Sorrow",
                "The Spooky Forest",
                "The Radiant Coast",
                "The Hills of Bad Stuff",
                "The Badlands",
                "The Mysterious Grove",
                "The Sinister Swamp",
                "The Cow Level",
                "The Forbidden Castle",
                "The Mountain of Doom",
                "The Sherwood Forest",
                "The Elephant Graveyard",
                "The City of The Dead",
                "The Caverns of Misery",
                "The Frigid Tundra",
                "The Scorching Dunes"
            };

            return Locations[random.Next(Locations.Length)];
        }

        public int CompareTo(Map? other) {

            if (other == null) {
                throw new NullReferenceException();
            }

            if (Difficulty > other.Difficulty) {
                return 1;
            } else if (Difficulty < other.Difficulty) {
                return -1;
            } else {
                return 0;
            }
        }
    }
}
