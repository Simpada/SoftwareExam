using SoftwareExam.CoreProgram.Adventurers.Decorators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    public abstract class Adventurer {

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Luck { get; set; }

        public string Name { get; set; } = "";
        public string Class { get; set; } = "";

        public Currency Value { get; set; } = new Currency();

        public List<BaseDecoratedAdventurer> Equipment = new();
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

        public Adventurer ReEquip() {

            Adventurer CoreAdventurer = FindBase(Equipment[^1]);

            // This doesn't work because am stupid
            foreach (var Item in Equipment) {
                CoreAdventurer = Item.AddItem(CoreAdventurer);
            }


            return CoreAdventurer;
        }

        private Adventurer FindBase(BaseDecoratedAdventurer DecoratedAdventurer) {

            if (DecoratedAdventurer.BaseAdventurer is BaseDecoratedAdventurer) {
                return FindBase(DecoratedAdventurer);
            } else {
                return DecoratedAdventurer.BaseAdventurer;
            }

        } 

        private string PickOne(string[] alternatives) {
            return alternatives[Random.Next(alternatives.Length)];
        }

        public void AddEquipment(BaseDecoratedAdventurer Item) {
            Equipment.Add(Item);
        }

    }
}
