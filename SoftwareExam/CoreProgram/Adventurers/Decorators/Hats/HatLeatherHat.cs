using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats
{
    internal class HatLeatherHat : BasicHat
    {
        public HatLeatherHat(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Rogue" };
            ItemId = 205;
        }

        public override void EditStats()
        {
            Health += 2;
            Luck += 2;
        }

        public override string GetEquipmentDescription()
        {
            return "Leather hat grants +2 health and +2 luck";
        }

        public static string GetItemDescription()
        {
            return new HatLeatherHat(new Mage()).GetEquipmentDescription();
        }
    }
}
