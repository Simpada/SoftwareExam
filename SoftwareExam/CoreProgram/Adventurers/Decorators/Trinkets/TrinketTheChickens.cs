using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets
{
    internal class TrinketTheChickens : BasicTrinket
    {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0,0,8);

        public TrinketTheChickens(Adventurer adventurer) : base(adventurer)
        {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 402;
        }

        public override void EditStats()
        {
            Damage += 7;
        }

        public override string GetEquipmentDescription()
        {
            return "Bullying chickens will bring its army of doom. Grants +7 damage";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketTheChickens(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "The Chickens";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketTheChickens(new Warrior()).GetEquipmentName();
        }
    }
}
