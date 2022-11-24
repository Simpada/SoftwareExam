using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
    internal class WeaponIronSword : BasicWeapon {
        public WeaponIronSword(Adventurer adventurer) : base(adventurer) {
            AllowedClasses = new string[] { "Warrior", "Rogue" };
            ItemId = 501;
        }

        public override void EditStats() {
            Damage += 2;
        }

        public override string GetEquipmentDescription()
        {
            return "Regular iron sword. Grants +2 damage";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponIronSword(new Warrior()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Iron Sword";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponIronSword(new Warrior()).GetEquipmentName();
        }
    }
}
