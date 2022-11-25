namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class BasicWeapon : BaseDecoratedAdventurer {

        public BasicWeapon(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 500;
        }
        public override void EditStats() { }
        public override string GetEquipmentDescription() {
            return "There's nothing special about this weapon.";
        }
        public override string GetEquipmentName() {
            return "Basic Weapon";
        }
    }
}
