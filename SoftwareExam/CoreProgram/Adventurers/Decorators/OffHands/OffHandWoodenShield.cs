using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
    internal class OffHandWoodenShield : BasicOffHand {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior"};
        public OffHandWoodenShield(Adventurer adventurer) : base(adventurer) {
            ItemId = 301;
        }

        public override void EditStats() {
            Health += 2;
        }

        public override string GetEquipmentDescription()
        {
            return "Regular wooden shield. Grants +2 health";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandWoodenShield(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Wooden Shield";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandWoodenShield(new Warrior()).GetEquipmentName();
        }
    }
}
