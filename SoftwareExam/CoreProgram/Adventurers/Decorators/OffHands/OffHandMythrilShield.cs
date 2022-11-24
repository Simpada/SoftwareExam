using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands
{
    internal class OffHandMythrilShield : BasicOffHand
    {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0,0,12);

        public OffHandMythrilShield(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 304;
        }

        public override void EditStats()
        {
            Health += 10;
        }

        public override string GetEquipmentDescription()
        {
            return "Strongest shield. Grants +10 health";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandMythrilShield(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Mythril Shield";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandMythrilShield(new Warrior()).GetEquipmentName();
        }
    }
}
