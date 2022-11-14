using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorOfGod : BasicArmor
    {
        public ArmorOfGod(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
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
    }
}
