using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using SoftwareExam.CoreProgram.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets
{
    internal class TrinketMimir : BasicTrinket
    {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0,0,5);

        public TrinketMimir(Adventurer adventurer) : base(adventurer)
        {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 403;
        }

        public override void EditStats()
        {
            Health += 3;
            Luck += 5;
        }

        public override string GetEquipmentDescription()
        {
            return "Wise talking severed head. Grants +3 health and +5 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketMimir(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Mimir";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketMimir(new Warrior()).GetEquipmentName();
        }
    }
}
