using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class OffHandDagger : BasicOffHand {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior","Rogue" };
        public static new readonly Currency Cost = new(6, 3, 6);

        public OffHandDagger(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 306;
        }

        public override void EditStats() {
            Damage += 3;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "This off Hand dagger is held in the other hand, granting +3 damage, and +1 luck.";
        }

        public static string GetItemDescription() {
            return new OffHandDagger(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Off Hand Dagger";
        }

        public static string GetItemName() {
            return new OffHandDagger(new Warrior()).GetEquipmentName();
        }
    }
}
