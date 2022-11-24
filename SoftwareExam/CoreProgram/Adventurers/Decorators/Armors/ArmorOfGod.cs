using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorOfGod : BasicArmor
    {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };

        public ArmorOfGod(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 105;
        }

        public override void EditStats()
        {
            Health += 777;
            Luck += 777;
        }

        public override string GetEquipmentDescription()
        {
            return "You are basically immortal now";
        }

        public static string GetItemDescription()
        {
            return new ArmorOfGod(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Armor of God";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorOfGod(new Warrior()).GetEquipmentName();
        }
    }
}
