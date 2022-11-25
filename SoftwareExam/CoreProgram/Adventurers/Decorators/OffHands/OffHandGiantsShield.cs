using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandGiantsShield : BasicOffHand {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior" };
        public static new readonly Currency Cost = new(0, 0, 7);

        public OffHandGiantsShield(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 305;
        }

        public override void EditStats() {
            Health += 3;
        }

        public override string GetEquipmentDescription() {
            return "Very heavy and big off hand shield. Grants +3 health";
        }

        public static string GetItemDescription() {
            return new OffHandGiantsShield(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Giant's Shield";
        }

        public static string GetItemName() {
            return new OffHandGiantsShield(new Warrior()).GetEquipmentName();
        }
    }
}
