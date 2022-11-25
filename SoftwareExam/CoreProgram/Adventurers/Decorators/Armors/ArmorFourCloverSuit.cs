using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorFourCloverSuit : BasicArmor
    {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(3, 3, 3);


        public ArmorFourCloverSuit(Adventurer adventurer) : base(adventurer)
        {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 104;
        }

        public override void EditStats()
        {
            Luck += 5;
        }

        public override string GetEquipmentDescription()
        {
            return "This ultra lucky suit grants no health but +5 luck!";
        }

        public static string GetItemDescription()
        {
            return new ArmorFourCloverSuit(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Four Clover Suit";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorFourCloverSuit(new Warrior()).GetEquipmentName();
        }
    }
}
