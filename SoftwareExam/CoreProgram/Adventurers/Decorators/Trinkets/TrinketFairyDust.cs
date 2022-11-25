using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketFairyDust : BasicTrinket {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(3, 3, 3);

        public TrinketFairyDust(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 405;
        }

        public override void EditStats() {
            Health += 1;
            Damage += 1;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "Carrying fairy dust is thought to bring good fortune. It gives +1 damage, health, and luck";
        }

        public static string GetItemDescription() {
            return new TrinketFairyDust(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Fairy Dust";
        }

        public static string GetItemName() {
            return new TrinketFairyDust(new Warrior()).GetEquipmentName();
        }
    }
}
