using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Expedition
{
    internal class CreateEncounter
    {
        private static string EncounterDescription = "";

        public CreateEncounter()
        {
            //Empty
        }

        public static string CreateMonsterEncounter(int adventurerLuck, string adventurerName)
        {
            Random random = new();
            int RollEncounter = random.Next(20) + adventurerLuck;

            switch (RollEncounter) {
                case <= 5:
                    EncounterDescription = $"{adventurerName} encountered a slime";
                    break;
                case <= 8:
                    EncounterDescription = $"{adventurerName} encountered a goblin";
                    break;
                case <= 12:
                    EncounterDescription = $"{adventurerName} encountered a lion";
                    break;
                case <= 15:
                    EncounterDescription = $"{adventurerName} encountered an ogre";
                    break;
                case <= 20:
                    EncounterDescription = $"{adventurerName} encountered a dragon";
                    break;
            }
            return EncounterDescription;
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
