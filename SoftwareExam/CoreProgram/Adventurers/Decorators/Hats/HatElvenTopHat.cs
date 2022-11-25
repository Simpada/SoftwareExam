using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HatElvenTopHat : BasicHat {

        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 5, 1);

        public HatElvenTopHat(Adventurer adventurer) : base(adventurer) {
            Value = BaseAdventurer.Value + Cost;
            ItemId = 202;
        }

        public override void EditStats() {
            Health += 1;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "A regular elven top hat that increases health and luck by +1";
        }

        public static string GetItemDescription() {
            return new HatElvenTopHat(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Elven Top Hat";
        }

        public static string GetItemName() {
            return new HatElvenTopHat(new Warrior()).GetEquipmentName();
        }
    }
}
