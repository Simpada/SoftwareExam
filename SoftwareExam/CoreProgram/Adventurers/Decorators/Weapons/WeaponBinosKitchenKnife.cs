﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons
{
    internal class WeaponBinosKitchenKnife : BasicWeapon
    {
        public WeaponBinosKitchenKnife(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Warrior", "Rogue" };
            ItemId = 502;
        }

        public override void EditStats()
        {
            Damage += +5;
            Luck += 1;
        }

        public override string GetEquipmentDescription()
        {
            return "Bino does not like his kitchen dagger stolen. Grants +5 damage and +1 luck";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new WeaponBinosKitchenKnife(new Warrior()).GetEquipmentDescription();
        }

    }
}