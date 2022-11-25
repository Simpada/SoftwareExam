using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorOfGod : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(2, 5, 25);

        public ArmorOfGod(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 105;
        }

        public override void EditStats() {
            Health += 7;
            Luck += 7;
        }

        public override string GetEquipmentDescription() {
            return "This divine armor shines with brilliant light and grants +7 health and +7 luck";
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
