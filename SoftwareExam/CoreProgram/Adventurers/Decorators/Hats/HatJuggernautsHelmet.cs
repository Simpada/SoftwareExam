using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats
{
    internal class HatJuggernautsHelmet : BasicHat
    {
        public HatJuggernautsHelmet(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Warrior" };
            ItemId = 204;
        }

        public override void EditStats()
        {
            Health += 5;
        }

        public override string GetEquipmentDescription()
        {
            return "Orange flat helmet but grants +5 health";
        }

        public static string GetItemDescription()
        {
            return new HatJuggernautsHelmet(new Mage()).GetEquipmentDescription();
        }
    }
}
