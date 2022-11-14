using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands
{
    internal class OffHandSteelArrows : BasicOffHand
    {
        public OffHandSteelArrows(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Rogue" };
            ItemId = 302;
        }

        public override void EditStats()
        {
            Damage += 3;
        }

        public override string GetEquipmentDescription()
        {
            return "Stronger than wooden arrows. Grants +3 damage";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandSteelArrows(new Warrior()).GetEquipmentDescription();
        }
    }
}
