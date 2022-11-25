using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeaponBinosKitchenKnife : BasicWeapon {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue" };
        public static new readonly Currency Cost = new(0, 0, 9);

        public WeaponBinosKitchenKnife(Adventurer adventurer) : base(adventurer) {
            Value = BaseAdventurer.Value + Cost;
            ItemId = 502;
        }

        public override void EditStats() {
            Damage += +5;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "Bino does not like his kitchen dagger stolen. Grants +5 damage and +1 luck";
        }

        public static string GetItemDescription() {
            return new WeaponBinosKitchenKnife(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Bino's Kitchen Knife";
        }

        public static string GetItemName() {
            return new WeaponBinosKitchenKnife(new Warrior()).GetEquipmentName();
        }
    }
}
