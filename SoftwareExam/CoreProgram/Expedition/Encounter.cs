namespace SoftwareExam.CoreProgram.Expedition {

    public class Encounter {

        private readonly Random Random = new();
        private readonly string AdventurerName;
        private readonly int AdventurerDamage = 0;
        private readonly int AdvendurerLuck = 0;
        private readonly int EncounterDamage = 0;
        private string Description = "";
        private Currency Reward = new();
        private readonly List<List<CreateEncounter>> MonsterEncounters = new List<List<CreateEncounter>>();

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

        //Get encounter, put in right list depending on difficulty
        public void GetEncounter()
        {
            


            //CreateEncounter.CreateMonsterEncounter(AdvendurerLuck, AdventurerName);
        }

    }
}