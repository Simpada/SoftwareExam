using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets
{
    internal class TrinketRingOfPower : BasicTrinket
    {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
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
            return "Magical ring. Grants +10 damage";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketRingOfPower(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Ring of Power";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketRingOfPower(new Warrior()).GetEquipmentName();
        }
    }
}
