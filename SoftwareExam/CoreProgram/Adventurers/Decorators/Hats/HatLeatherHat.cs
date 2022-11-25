using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HatLeatherHat : BasicHat {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior","Rogue" };
        public static new readonly Currency Cost = new(0, 0, 1);

        public HatLeatherHat(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 205;
        }

        public override void EditStats() {
            Health += 1;
        }

        public override string GetEquipmentDescription() {
            return "Leather hat grants +1 health";
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
