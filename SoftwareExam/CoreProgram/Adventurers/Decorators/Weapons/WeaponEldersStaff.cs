using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons
{
    internal class WeaponEldersStaff : BasicWeapon
    {
        public WeaponEldersStaff(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Mage" };
            ItemId = 503;
        }

        public override void EditStats()
        {
            Damage += +2;
            Luck += 3;
        }

        public override string GetEquipmentDescription()
        {
            return "Wisest staff of all. Grants +2 damage and +3 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponEldersStaff(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Elders Staff";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponEldersStaff(new Warrior()).GetEquipmentName();
        }
    }
}
