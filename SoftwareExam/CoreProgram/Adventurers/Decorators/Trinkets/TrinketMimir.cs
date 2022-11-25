using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketMimir : BasicTrinket {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 0, 5);

        public TrinketMimir(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 403;
        }

        public override void EditStats() {
            Health += 3;
            Luck += 5;
        }

        public override string GetEquipmentDescription() {
            return "Wise talking severed head. Grants +3 health and +5 luck";
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
