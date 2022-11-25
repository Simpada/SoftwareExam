using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketRingOfPower : BasicTrinket {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 0, 4);

        public TrinketRingOfPower(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 404;
        }

        public override void EditStats() {
            Damage += 10;
        }

        public override string GetEquipmentDescription() {
            return "Magical ring. Grants +10 damage";
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
