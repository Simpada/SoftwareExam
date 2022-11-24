﻿using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets
{
    internal class TrinketFairyDust : BasicTrinket
    {
        public TrinketFairyDust(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 405;
        }

        public override void EditStats()
        {
            Luck += 1;
        }

        public override string GetEquipmentDescription()
        {
            return "Carrying fairy dust is thought to bring unbelievable luck.";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketFairyDust(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Fairy Dust";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketFairyDust(new Warrior()).GetEquipmentName();
        }
    }
}
