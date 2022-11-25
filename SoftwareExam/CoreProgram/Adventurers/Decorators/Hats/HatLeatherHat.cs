using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HatLeatherHat : BasicHat {
        public static new readonly string[] AllowedClasses = new string[] { "Rogue" };
        public static new readonly Currency Cost = new(0, 0, 5);

        public HatLeatherHat(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 205;
        }

        public override void EditStats() {
            Health += 2;
            Luck += 2;
        }

        public override string GetEquipmentDescription() {
            return "Leather hat grants +2 health and +2 luck";
        }

        public static string GetItemDescription() {
            return new HatLeatherHat(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Leather Hat";
        }

        public static string GetItemName() {
            return new HatLeatherHat(new Warrior()).GetEquipmentName();
        }
    }
}
