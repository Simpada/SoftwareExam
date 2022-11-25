using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandBookOfWisdom : BasicOffHand {

        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 0, 5);

        public OffHandBookOfWisdom(Adventurer Adventurer) : base(Adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 303;
        }

        public override void EditStats() {
            Damage += 3;
        }

        public override string GetEquipmentDescription() {
            return "This off hand book contains ancient knowledge. Grants +3 damage";
        }

        public static string GetItemDescription() {
            return new OffHandBookOfWisdom(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Book of Wisdom";
        }

        public static string GetItemName() {
            return new OffHandBookOfWisdom(new Warrior()).GetEquipmentName();
        }
    }
}
