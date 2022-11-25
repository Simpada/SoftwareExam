using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponOrbOfDarkness : BasicWeapon {
        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 0, 6);

        public WeaponOrbOfDarkness(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 504;
        }

        public override void EditStats() {
            Damage += +10;
            Luck -= 3;
        }

        public override string GetEquipmentDescription() {
            return "Darkness makes you powerful but blind. Grants +10 damage but -3 luck";
        }

        public static string GetItemDescription() {
            return new WeaponOrbOfDarkness(new Mage()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Orb of Darkness";
        }

        public static string GetItemName() {
            return new WeaponOrbOfDarkness(new Warrior()).GetEquipmentName();
        }
    }
}
