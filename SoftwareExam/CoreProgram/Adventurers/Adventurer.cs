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

        public string[] SymbolArray = new string[6];

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
                    "Incomprehensible",
                    "Mass Murderer",
                    "Obnoxious",
                    "Rude",
                    "Wanderer"
                });


            return $"{name} the {title}";
        }

        public override string ToString() {
            return @$"    |{SymbolArray[0]} 
    |{SymbolArray[1]}Name:   {Name}
    |{SymbolArray[2]}Class:  {Class}
    |{SymbolArray[3]}Health: {Health}
    |{SymbolArray[4]}Damage: {Damage}
    |{SymbolArray[5]}Luck:   {Luck}";
        }

        public abstract string GetEquipmentDescription();

        public Adventurer GetStartingGear() {

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

            return Adventurer.EquipGear(this);
        }
        
        private string PickOne(string[] alternatives) {
            return alternatives[Random.Next(alternatives.Length)];
        }

        public void InheritStats(Adventurer BaseAdventurer) {

            Health = BaseAdventurer.Health;
            Damage = BaseAdventurer.Damage; 
            Luck = BaseAdventurer.Luck;

        }

        public static Adventurer EquipGear(Adventurer OldAdventurer) {

            Adventurer NewAdventurer = FindBase(OldAdventurer.Equipment[^1]);

            List<BaseDecoratedAdventurer> Gear = NewAdventurer.Equipment;

            foreach (var Item in OldAdventurer.Equipment) {
                Item.BaseAdventurer = NewAdventurer;
                Item.InheritStats(NewAdventurer);
                Item.EditStats();
                NewAdventurer = Item;
            }

            NewAdventurer.Equipment = Gear;

            return NewAdventurer;
        }
        
        private static Adventurer FindBase(BaseDecoratedAdventurer ParentAdventurer) {

            if (ParentAdventurer.BaseAdventurer is BaseDecoratedAdventurer DecoratedAdventurer) {
                return FindBase(DecoratedAdventurer);
            } else {
                return ParentAdventurer.BaseAdventurer;
            }
        }

        public static Adventurer AddNewItem(BaseDecoratedAdventurer NewItem) {

            Adventurer ChangingAdventurer = NewItem.BaseAdventurer;

            int ItemType = NewItem.Id;
            if (ItemType >= 100) ItemType /= 100;
            if (ItemType >= 10) ItemType /= 10;

            foreach (BaseDecoratedAdventurer OldItem in ChangingAdventurer.Equipment) {

                int EquippedItemType = OldItem.Id;
                if (EquippedItemType >= 100) EquippedItemType /= 100;
                if (EquippedItemType >= 10) EquippedItemType /= 10;

                if (ItemType == EquippedItemType) {
                    ChangingAdventurer.Equipment.Remove(OldItem);
                    break;
                }
            }
            ChangingAdventurer.Equipment.Add(NewItem);

            return Adventurer.EquipGear(ChangingAdventurer);
        }
    }
}
