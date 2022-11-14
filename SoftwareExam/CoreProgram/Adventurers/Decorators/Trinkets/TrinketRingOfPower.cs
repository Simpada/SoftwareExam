using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets
{
    internal class TrinketRingOfPower : BasicTrinket
    {
        public TrinketRingOfPower(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 404;
        }

        public override void EditStats()
        {
            Damage += 10;
        }

        public override string GetEquipmentDescription()
        {
            return "Magical ring. Grants +10 damge";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketRingOfPower(new Mage()).GetEquipmentDescription();
        }
    }
}
