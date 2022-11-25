using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponWindripperBow : BasicWeapon {
        public static new readonly string[] AllowedClasses = new string[] { "Rogue" };
        public static new readonly Currency Cost = new(0, 0, 7);

        public WeaponWindripperBow(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 505;
        }

        public override void EditStats() {
            Damage += +5;
        }

        public override string GetEquipmentDescription() {
            return "Swift and precise bow. Grants +5 damage";
        }

        public static string GetItemDescription() {
            return new WeaponWindripperBow(new Mage()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Windripper Bow";
        }

        public static string GetItemName() {
            return new WeaponWindripperBow(new Warrior()).GetEquipmentName();
        }
    }
}
