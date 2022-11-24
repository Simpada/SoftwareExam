namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    public class MonsterFactory : EncounterFactory
    {
        public override Encounter CreateEncounter()
        {

            return null;
        }



        public static List<object> CreateMonsterEncounter(int adventurerLuck, string adventurerName)
        {
            Random random = new();
            List<object> list = new List<object>();
            int RollEncounter = random.Next(20) + 1;
            int rewardMultiplier = adventurerLuck / 100 + 1;


            /**
             * Index 0 - string description
             * Index 1 - int damage
             * Index 2 - Reward (Currency)
             */
            switch (RollEncounter) {
                case <= 5:
                    list.Add($"{adventurerName} encountered a slime");
                    list.Add(random.Next(3));
                    list.Add(new Currency(random.Next(4) * rewardMultiplier, random.Next(2) * rewardMultiplier, 0));
                    break;
                case <= 8:
                    list.Add($"{adventurerName} encountered a goblin");
                    list.Add(random.Next(5));
                    list.Add(new Currency(random.Next(4) * rewardMultiplier, random.Next(4) * rewardMultiplier, 0));
                    break;
                case <= 12:
                    list.Add($"{adventurerName} encountered a lion");
                    list.Add(random.Next(7));
                    list.Add(new Currency(random.Next(3) * rewardMultiplier, random.Next(7) * rewardMultiplier, 0));
                    break;
                case <= 15:
                    list.Add($"{adventurerName} encountered an ogre");
                    list.Add(random.Next(10));
                    list.Add(new Currency(random.Next(4) * rewardMultiplier, random.Next(4) * rewardMultiplier, random.Next(2) * rewardMultiplier));
                    break;
                case < 20:
                    list.Add($"{adventurerName} encountered a dragon");
                    list.Add(random.Next(15));
                    list.Add(new Currency(random.Next(8) * rewardMultiplier, random.Next(7) * rewardMultiplier, random.Next(4) * rewardMultiplier));
                    break;
                case >= 20:
                    list.Add($"{adventurerName} encountered the demon lord!");
                    list.Add(random.Next(20));
                    list.Add(new Currency(random.Next(9) * rewardMultiplier, random.Next(9) * rewardMultiplier, random.Next(10) * rewardMultiplier));
                    break;
            }
            return list;
        }
    }
}
