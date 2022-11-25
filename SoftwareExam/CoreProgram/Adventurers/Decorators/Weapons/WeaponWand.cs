using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponWand : BasicWeapon {
        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 0, 2);

        public WeaponWand(Adventurer adventurer) : base(adventurer) {
            Value = BaseAdventurer.Value + Cost;
            ItemId = 507;
        }

        public override void EditStats() {
            Damage += 2;
        }

        public override string GetEquipmentDescription() {
            return "This want is a good first weapon for any mage. Grants +2 damage";
        }

        public static string GetItemDescription() {
            return new WeaponWand(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Wand";
        }

        public static string GetItemName() {
            return new WeaponWand(new Warrior()).GetEquipmentName();
        }
    }
}
