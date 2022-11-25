using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponIronSword : BasicWeapon {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue" };
        public static new readonly Currency Cost = new(0, 0, 1);

        public WeaponIronSword(Adventurer adventurer) : base(adventurer) {
            Value = BaseAdventurer.Value + Cost;
            ItemId = 501;
        }

        public override void EditStats() {
            Damage += 2;
        }

        public override string GetEquipmentDescription() {
            return "Regular iron sword. Grants +2 damage";
        }

        public static string GetItemDescription() {
            return new WeaponIronSword(new Warrior()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Iron Sword";
        }

        public static string GetItemName() {
            return new WeaponIronSword(new Warrior()).GetEquipmentName();
        }
    }
}
