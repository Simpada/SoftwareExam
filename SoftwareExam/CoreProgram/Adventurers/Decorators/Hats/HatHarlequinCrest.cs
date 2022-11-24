﻿using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats
{
    internal class HatHarlequinCrest : BasicHat
    {
        public HatHarlequinCrest(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Rogue", "Mage" };
            ItemId = 203;
        }

        public override void EditStats()
        {
            Health += 1;
            Luck += 10;
        }

        public override string GetEquipmentDescription()
        {
            return "This hat was found on Hell difficulty in Diablo 2. Gives a lot of luck";
        }

        public static string GetItemDescription()
        {
            return new HatHarlequinCrest(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Harlequin Crest";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new HatHarlequinCrest(new Warrior()).GetEquipmentName();
        }
    }
}
