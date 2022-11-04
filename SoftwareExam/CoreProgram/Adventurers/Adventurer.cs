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

        public List<BaseDecoratedAdventurer> Equipment { get; set; } = new();
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

        public void GetStartingGear() {

            BaseDecoratedAdventurer Hat = new BasicHat(this);
            BaseDecoratedAdventurer Armor = new BasicArmor(Hat);
            BaseDecoratedAdventurer Weapon = new BasicWeapon(Armor);
            BaseDecoratedAdventurer OffHand = new BasicOffHand(Weapon);
            BaseDecoratedAdventurer Trinket = new BasicTrinket(OffHand);

            Equipment.Add(Hat);
            Equipment.Add(Armor);
            Equipment.Add(Weapon);
            Equipment.Add(OffHand);
            Equipment.Add(Trinket);
        }

        private string PickOne(string[] alternatives) {
            return alternatives[Random.Next(alternatives.Length)];
        }

        public void AddEquipment(BaseDecoratedAdventurer Item) {
            Equipment.Add(Item);
        }

        public static Adventurer EquipGear(Adventurer Adventurer) {

            Adventurer CoreAdventurer = FindBase(Adventurer.Equipment[^1]);

            List<BaseDecoratedAdventurer> Gear = CoreAdventurer.Equipment;


            // This doesn't work because am stupid
            foreach (var Item in Adventurer.Equipment) {
                Item.BaseAdventurer = CoreAdventurer;
                CoreAdventurer = Item;
            }

            CoreAdventurer.Equipment = Gear;

            return CoreAdventurer;
        }
        
        private static Adventurer FindBase(BaseDecoratedAdventurer DecoratedAdventurer) {

            if (DecoratedAdventurer.BaseAdventurer is BaseDecoratedAdventurer adventurer) {
                return FindBase(adventurer);
            } else {
                return DecoratedAdventurer.BaseAdventurer;
            }
        }
    }
}
