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
        public static new readonly string[] AllowedClasses = new string[] { "Warrior"};
        public static new readonly Currency Cost = new(0,0,7);

        public HatJuggernautsHelmet(Adventurer adventurer) : base(adventurer)
        {

            Value = BaseAdventurer.Value + Cost;
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
            return "Juggernauts Helmet";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new HatJuggernautsHelmet(new Warrior()).GetEquipmentName();
        }
    }
}
