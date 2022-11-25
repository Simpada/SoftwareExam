using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using SoftwareExam.CoreProgram.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands
{
    internal class OffHandGiantsShield : BasicOffHand
    {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior"};
        public static new readonly Currency Cost = new(0,0,10);

        public OffHandGiantsShield(Adventurer adventurer) : base(adventurer)
        {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 305;
        }

        public override void EditStats()
        {
            Health += 15;
            Luck -= 3;
        }

        public override string GetEquipmentDescription()
        {
            return "Very heavy and big shield. Grants +15 health but -3 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandGiantsShield(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Giant's Shield";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandGiantsShield(new Warrior()).GetEquipmentName();
        }
    }
}
