namespace SoftwareExam.CoreProgram.Expedition {

    public class Map {

        public enum Difficulties{
            Easy = 1,
            Medium = 2,
            Hard = 3,
            Legendary = 4
        }

        private readonly Random Random = new();

        public int Encounters { get; set; }
        public Currency ExpeditionCost { get; set; } = new Currency();
        public Currency Reward { get; set; } = new Currency();
        public string Location { get; set; } = "";
        public Difficulties Difficulty { get; set; }

        public static Map GetMap(int difficulty) {
            throw new NotImplementedException();
        }
    }
}
