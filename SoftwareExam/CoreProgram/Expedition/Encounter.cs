namespace SoftwareExam.CoreProgram.Expedition {

    public class Encounter {

        private readonly Random Random = new();
        private string Description = "";
        private int EncounterDamage = 0;
        private int AdventurerDamage = 0;
        private int AdvendurerLuck = 0;
        private Currency Reward = new();
        private readonly string AdventurerName;

        public Encounter(int adventurerDamage, int adventurerLuck, string adventurerName, string description) {
            AdventurerDamage = adventurerDamage;
            AdvendurerLuck = adventurerLuck;
            AdventurerName = adventurerName;
            Description = description;
        }

        public bool RunEncounter(out Currency reward, out string description)
        {
            int monsterRoll = Random.Next(20) + EncounterDamage;
            int adventurerRoll = Random.Next(20) + AdventurerDamage;

            if (adventurerRoll >= monsterRoll) {
                reward = Reward;
                description = Description;
                return true;
            } else {
                reward = new();
                description = Description;
                return false;
            }
        }

        private void CreateMonsterEncounter(int adventurerLuck)
        {
            List<List<Object>> list = new List<List<Object>>()
            {
                new List<Object>()
                {
                    $"{AdventurerName} encountered a dragon.",
                    Random.Next(6) + 5,
                    new Currency(Random.Next(5), Random.Next(5), Random.Next(5))
                },
                new List<Object>()
                {
                    $"{AdventurerName} encountered a goblin.",
                    Random.Next(3),
                    new Currency(Random.Next(5), Random.Next(3), 0)
                },
                new List<Object>()
                {
                    $"{AdventurerName} encountered a slime.",
                    Random.Next(1),
                    new Currency(Random.Next(1), Random.Next(1), 0)
                },
                 new List<Object>()
                {
                    $"{AdventurerName} encountered an ogre.",
                    Random.Next(3) + 3,
                    new Currency(Random.Next(5), Random.Next(3), Random.Next(3))
                },
                 new List<Object>()
                {
                    $"{AdventurerName} encountered a lion.",
                    Random.Next(3) ,
                    new Currency(Random.Next(2), Random.Next(3), Random.Next(1))
                },
            };
        }

    }
}