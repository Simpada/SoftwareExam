﻿using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
    internal class ArmorPlateArmor : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior"};
        public ArmorPlateArmor(Adventurer adventurer) : base(adventurer) {
            ItemId = 101;
        }

        public override void EditStats() {
            Health += 5;
        }

        public override string GetEquipmentDescription() {
            return "This heavy armor grants a +5 boost to health.";
        }
        
        public static string GetItemDescription() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorPlateArmor(new Warrior()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Plate Armor";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorPlateArmor(new Warrior()).GetEquipmentName();
        }
    }
}
