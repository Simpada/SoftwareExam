﻿using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandWoodenShield : BasicOffHand {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior" };
        public static new readonly Currency Cost = new(0, 5, 1);

        public OffHandWoodenShield(Adventurer adventurer) : base(adventurer) {
            ItemId = 301;

            Value = BaseAdventurer.Value + Cost;
        }

        public override void EditStats() {
            Health += 2;
        }

        public override string GetEquipmentDescription() {
            return "Regular wooden shield. Grants +2 health";
        }

        public static string GetItemDescription() {
            return new OffHandWoodenShield(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Wooden Shield";
        }

        public static string GetItemName() {
            return new OffHandWoodenShield(new Warrior()).GetEquipmentName();
        }
    }
}
