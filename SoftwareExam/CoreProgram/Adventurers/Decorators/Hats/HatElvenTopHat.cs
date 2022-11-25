using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using SoftwareExam.CoreProgram.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats
{
    internal class HatElvenTopHat : BasicHat
    {

        public static new readonly string[] AllowedClasses = new string[] { "Mage"};
        public static new readonly Currency Cost = new(0,5,1);

        public HatElvenTopHat(Adventurer adventurer) : base(adventurer)
        {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 202;
        }

        public override void EditStats()
        {
            Health += 1;
            Luck += 1;
        }

        public override string GetEquipmentDescription()
        {
            return "A regular elven top hat that increases health and luck by +1";
        }

        public static string GetItemDescription()
        {
            return new HatElvenTopHat(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Elven Top Hat";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new HatElvenTopHat(new Warrior()).GetEquipmentName();
        }
    }
}
