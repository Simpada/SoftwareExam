using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketRingOfPower : BasicTrinket {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(9, 7, 13);

        public TrinketRingOfPower(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 404;
        }

        public override void EditStats() {
            Damage += 5;
            Luck += 2;
        }

        public override string GetEquipmentDescription() {
            return "This magical ring has some weird text that appears when its near fire. Grants +5 damage and +2 luck";
        }

        public static string GetItemDescription() {
            return new TrinketRingOfPower(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Ring of Power";
        }

        public static string GetItemName() {
            return new TrinketRingOfPower(new Warrior()).GetEquipmentName();
        }
    }
}
