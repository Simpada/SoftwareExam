using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorFourCloverSuit : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(9, 9, 9);

        public ArmorFourCloverSuit(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 104;
        }

        public override void EditStats() {
            Luck += 4;
        }

        public override string GetEquipmentDescription() {
            return "This ultra lucky armor grants no health but +4 luck!";
        }

        public static string GetItemDescription() {
            return new ArmorFourCloverSuit(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Four Clover Suit";
        }

        public static string GetItemName() {
            return new ArmorFourCloverSuit(new Warrior()).GetEquipmentName();
        }
    }
}
