namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BasicTrinket : BaseDecoratedAdventurer {
        public BasicTrinket(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 400;
        }
        public override void EditStats() { }
        public override string GetEquipmentDescription() {
            return "There's nothing special about this trinket.";
        }
        public override string GetEquipmentName() {
            return "Basic Trinket";
        }
    }
}
