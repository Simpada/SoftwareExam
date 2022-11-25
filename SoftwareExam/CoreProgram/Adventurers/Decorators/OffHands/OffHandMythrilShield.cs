using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandMythrilShield : BasicOffHand {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 0, 12);

        public OffHandMythrilShield(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 304;
        }

        public override void EditStats() {
            Health += 10;
        }

        public override string GetEquipmentDescription() {
            return "Strongest shield. Grants +10 health";
        }

        public static string GetItemDescription() {
            return new OffHandMythrilShield(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Mythril Shield";
        }

        public static string GetItemName() {
            return new OffHandMythrilShield(new Warrior()).GetEquipmentName();
        }
    }
}
