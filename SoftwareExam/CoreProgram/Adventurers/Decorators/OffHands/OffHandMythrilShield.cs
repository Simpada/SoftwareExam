using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandMythrilShield : BasicOffHand {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 2, 8);

        public OffHandMythrilShield(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 304;
        }

        public override void EditStats() {
            Health += 2;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "A strong and light off hand shield. Grants +2 health, and +1 luck";
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
