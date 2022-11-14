using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets
{
    internal class TrinketMimir : BasicTrinket
    {
        public TrinketMimir(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 403;
        }

        public override void EditStats()
        {
            Health += 3;
            Luck += 5;
        }

        public override string GetEquipmentDescription()
        {
            return "Wise talking sevrered head. Grants +3 health and +5 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketMimir(new Mage()).GetEquipmentDescription();
        }
    }
}
