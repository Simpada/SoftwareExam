using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorOfGod : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 0, 1000);

        public ArmorOfGod(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 105;
        }

        public override void EditStats() {
            Health += 777;
            Luck += 777;
        }

        public override string GetEquipmentDescription() {
            return "You are basically immortal now";
        }

        public static string GetItemDescription() {
            return new ArmorOfGod(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Armor of God";
        }

        public static string GetItemName() {
            return new ArmorOfGod(new Warrior()).GetEquipmentName();
        }
    }
}
