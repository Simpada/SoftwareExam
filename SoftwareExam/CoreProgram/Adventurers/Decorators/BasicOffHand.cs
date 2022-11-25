namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BasicOffHand : BaseDecoratedAdventurer {
        public BasicOffHand(Adventurer adventurer) : base(adventurer) {
            ItemId = 300;
        }
        public override void EditStats() { }
        public override string GetEquipmentDescription() {
            return "There's nothing special about this off-hand.";
        }
        public override string GetEquipmentName() {
            return "Basic Off Hand";
        }
    }
}
