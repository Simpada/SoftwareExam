using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HatPlateHelmet : BasicHat {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior" };
        public static new readonly Currency Cost = new(0, 5, 3);

        public HatPlateHelmet(Adventurer adventurer) : base(adventurer) {
            ItemId = 201;

            Value = BaseAdventurer.Value + Cost;
        }

        public override void EditStats() {
            Health += 2;
        }

        public override string GetEquipmentDescription() {
            return "A durable helmet that increase your health by 2";
        }

        public static string GetItemDescription() {
            return new HatPlateHelmet(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Plate Helmet";
        }

        public static string GetItemName() {
            return new HatPlateHelmet(new Warrior()).GetEquipmentName();
        }
    }
}
