﻿using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats
{
    internal class HatElvenTopHat : BasicHat
    {
        public HatElvenTopHat(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Mage" };
            ItemId = 202;
        }

        public override void EditStats()
        {
            Health += 1;
            Luck += 1;
        }

        public override string GetEquipmentDescription()
        {
            return "A regular elven top hat that increases health and luck by +1";
        }

        public static string GetItemDescription()
        {
            return new HatElvenTopHat(new Mage()).GetEquipmentDescription();
        }
    }
}