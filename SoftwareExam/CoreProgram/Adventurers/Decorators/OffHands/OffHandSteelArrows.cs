using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandSteelArrows : BasicOffHand {

        public static new readonly string[] AllowedClasses = new string[] { "Rogue" };
        public static new readonly Currency Cost = new(0, 0, 3);

        public OffHandSteelArrows(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 302;
        }

        public override void EditStats() {
            Damage += 3;
        }

        public override string GetEquipmentDescription() {
            return "Stronger than wooden arrows. Grants +3 damage";
        }

        public static string GetItemDescription() {
            return new OffHandSteelArrows(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Steel Arrows";
        }

        public static string GetItemName() {
            return new OffHandSteelArrows(new Warrior()).GetEquipmentName();
        }
    }
}
