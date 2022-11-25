using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorFourCloverSuit : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(3, 3, 3);

        public ArmorFourCloverSuit(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 104;
        }

        public override void EditStats() {
            Luck += 5;
        }

        public override string GetEquipmentDescription() {
            return "This ultra lucky suit grants no health but +5 luck!";
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
