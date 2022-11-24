using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
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
        public override string GetEquipmentName() {
            return "JuggernautsHelmet";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new HatJuggernautsHelmet(new Warrior()).GetEquipmentName();
        }
    }
}
