using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HatWizardHat : BasicHat {
        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(5, 5, 5);

        public HatWizardHat(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 204;
        }

        public override void EditStats() {
            Health += 1;
            Damage += 1;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "This pointy hat grants +1 health, damage, and luck!";
        }

        public static string GetItemDescription() {
            return new HatWizardHat(new Mage()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Wizard Hat";
        }

        public static string GetItemName() {
            return new HatWizardHat(new Warrior()).GetEquipmentName();
        }
    }
}
