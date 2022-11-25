namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BasicArmor : BaseDecoratedAdventurer {
        public BasicArmor(Adventurer adventurer) : base(adventurer) {
            ItemId = 100;
        }
        public override void EditStats() { }
        public override string GetEquipmentDescription() {
            return "There's nothing special about this armor.";
        }
        public override string GetEquipmentName() {
            return "Basic Armor";
        }
    }
}
