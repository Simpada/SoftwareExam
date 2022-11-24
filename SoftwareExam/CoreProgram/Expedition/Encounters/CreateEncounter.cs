namespace SoftwareExam.CoreProgram.Expedition.Encounters
{
    public static class CreateEncounter
    {
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
            switch (RollEncounter)
            {
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


    //    public void CreateMonsterEncounterrr(int adventurerLuck, string adventurerName)
    //    {
    //        List<List<Object>> list = new List<List<Object>>()
    //        {
    //            new List<Object>()
    //            {
    //                $"{adventurerName} encountered a dragon.",
    //                Random.Next(6) + 5,
    //                new Currency(Random.Next(5), Random.Next(5), Random.Next(5))
    //            },
    //            new List<Object>()
    //            {
    //                $"{adventurerName} encountered a goblin.",
    //                Random.Next(3),
    //                new Currency(Random.Next(5), Random.Next(3), 0)
    //            },
    //            new List<Object>()
    //            {
    //                $"{adventurerName} encountered a slime.",
    //                Random.Next(1),
    //                new Currency(Random.Next(1), Random.Next(1), 0)
    //            },
    //             new List<Object>()
    //            {
    //                $"{adventurerName} encountered an ogre.",
    //                Random.Next(3) + 3,
    //                new Currency(Random.Next(5), Random.Next(3), Random.Next(3))
    //            },
    //             new List<Object>()
    //            {
    //                $"{adventurerName} encountered a lion.",
    //                Random.Next(3) ,
    //                new Currency(Random.Next(2), Random.Next(3), Random.Next(1))
    //            },
    //        };
    //    //}


    //}
}
