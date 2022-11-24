using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons
{
    internal class WeaponWindripperBow : BasicWeapon
    {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue"};
        public WeaponWindripperBow(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 505;
        }

        public override void EditStats()
        {
            Damage += +5;
            Luck = 1;
        }

        public override string GetEquipmentDescription()
        {
            return "Swift and precise bow. Grants +5 damage and +1 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponWindripperBow(new Mage()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Windripper Bow";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponWindripperBow(new Warrior()).GetEquipmentName();
        }
    }
}
