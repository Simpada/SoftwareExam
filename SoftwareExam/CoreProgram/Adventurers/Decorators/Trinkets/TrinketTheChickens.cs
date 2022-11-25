using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketTheChickens : BasicTrinket {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 0, 8);

        public TrinketTheChickens(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 402;
        }

        public override void EditStats() {
            Damage += 7;
        }

        public override string GetEquipmentDescription() {
            return "Bullying chickens will bring its army of doom. Grants +7 damage";
        }

        public static string GetItemDescription() {
            return new TrinketTheChickens(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "The Chickens";
        }

        public static string GetItemName() {
            return new TrinketTheChickens(new Warrior()).GetEquipmentName();
        }
    }
}
