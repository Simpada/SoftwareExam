using SoftwareExam.CoreProgram.Adventurers.Decorator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    internal abstract class Adventurer {

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Luck { get; set; }

        public string Name { get; set; } = "";
        public string Class { get; set; } = "";


        private List<BaseDecoratedAdventurer> Equipment = new();
        private readonly Random Random = new();

        public Adventurer() {
            Name = RandomName();
        }

        private string RandomName() {

            string name = PickOne(
                new string[] {
                    "Bernie",
                    "Charles",
                    "Clara",
                    "Frida",
                    "Ken",
                    "Percy",
                    "William"
                });

            string title = PickOne(
                new string[] {
                    "Dumb",
                    "Hairy",
                    "Icomprehensible",
                    "Mass Murderer",
                    "Obnoxious",
                    "Rude",
                    "Wanderer"
                });


            return $"{name} the {title}";
        }

        public abstract override string ToString();

        public abstract string GetEquipmentDescription();

        public void ReEquip() {

            // This function will apply all equipment anew to a DecoratedAdventurer

        }

        private string PickOne(string[] alternatives) {
            return alternatives[Random.Next(alternatives.Length)];
        }


    }
}
