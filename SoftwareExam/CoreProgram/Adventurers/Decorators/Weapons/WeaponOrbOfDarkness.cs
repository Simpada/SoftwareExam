using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponOrbOfDarkness : BasicWeapon {
        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 0, 10);

        public WeaponOrbOfDarkness(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 504;
        }

        public override void EditStats() {
            Damage += 7;
        }

        public override string GetEquipmentDescription() {
            return "Darkness makes you powerful but blind. Grants +7 damage";
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
