using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorPlateArmor : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior" };
        public static new readonly Currency Cost = new(0, 0, 10);

        public ArmorPlateArmor(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 101;
        }

        public override void EditStats() {
            Health += 5;
        }

        public override string GetEquipmentDescription() {
            return "This heavy armor grants a +5 boost to health.";
        }

        public static string GetItemDescription() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorPlateArmor(new Warrior()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Plate Armor";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorPlateArmor(new Warrior()).GetEquipmentName();
        }
    }
}
