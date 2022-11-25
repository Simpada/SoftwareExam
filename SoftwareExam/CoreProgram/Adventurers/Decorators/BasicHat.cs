namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BasicHat : BaseDecoratedAdventurer {

        public BasicHat(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 200;
        }
        public override void EditStats() { }
        public override string GetEquipmentDescription() {
            return "There's nothing special about this hat.";
        }
        public override string GetEquipmentName() {
            return "Basic Hat";
        }
    }
}
