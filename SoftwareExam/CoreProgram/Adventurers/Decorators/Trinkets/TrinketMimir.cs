using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketMimir : BasicTrinket {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 0, 9);

        public TrinketMimir(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 403;
        }

        public override void EditStats() {
            Health += 2;
            Luck += 3;
        }

        public override string GetEquipmentDescription() {
            return "Wise talking severed head. Grants +2 health and +3 luck";
        }

        public static string GetItemDescription() {
            return new TrinketMimir(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Mimir";
        }

        public static string GetItemName() {
            return new TrinketMimir(new Warrior()).GetEquipmentName();
        }
    }
}
