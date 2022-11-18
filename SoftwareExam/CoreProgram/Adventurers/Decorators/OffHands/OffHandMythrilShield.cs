﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands
{
    internal class OffHandMythrilShield : BasicOffHand
    {
        public OffHandMythrilShield(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
            ItemId = 304;
        }

        public override void EditStats()
        {
            Health += 10;
        }

        public override string GetEquipmentDescription()
        {
            return "Strongest shield. Grants +10 health";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandMythrilShield(new Warrior()).GetEquipmentDescription();
        }
    }
}