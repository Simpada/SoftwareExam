using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons
{
    internal class WeaponSwordOfOmens : BasicWeapon
    {
        public WeaponSwordOfOmens(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Warrior" };
            ItemId = 506;
        }

        public override void EditStats()
        {
            Damage += +15;
            Luck += 10;
        }

        public override string GetEquipmentDescription()
        {
            return @"Only wielded by the Lord of the ThunderCats. Chant the magical words to gain its powers. Grants +15 damage and +10 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponSwordOfOmens(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Sword of Omens";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponSwordOfOmens(new Warrior()).GetEquipmentName();
        }
    }
}
