using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponEldersStaff : BasicWeapon {
        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 0, 6);

        public WeaponEldersStaff(Adventurer adventurer) : base(adventurer) {
            Value = BaseAdventurer.Value + Cost;
            ItemId = 503;
        }

        public override void EditStats() {
            Damage += +2;
            Luck += 3;
        }

        public override string GetEquipmentDescription() {
            return "Wisest staff of all. Grants +2 damage and +3 luck";
        }

        public static string GetItemDescription() {
            return new WeaponEldersStaff(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Elders Staff";
        }

        public static string GetItemName() {
            return new WeaponEldersStaff(new Warrior()).GetEquipmentName();
        }
    }
}
