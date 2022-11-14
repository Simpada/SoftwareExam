﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorFourCloverSuit : BasicArmor
    {
        public ArmorFourCloverSuit(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
            ItemId = 104;
        }

        public override void EditStats()
        {
            Luck += 5;
        }

        public override string GetEquipmentDescription()
        {
            return "This ultra lucky suit grants no health but +5 luck!";
        }

        public static string GetItemDescription()
        {
            return new ArmorFourCloverSuit(new Warrior()).GetEquipmentDescription();
        }
    }
}
