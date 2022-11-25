using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorLeatherSuit : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue" };
        public static new readonly Currency Cost = new(0, 0, 2);

        public ArmorLeatherSuit(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 103;
        }

        public override void EditStats() {
            Health += 1;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "This leather armor grants +1 boost to health and +1 to luck";
        }

        public static string GetItemDescription() {
            return new ArmorLeatherSuit(new Rogue()).GetEquipmentDescription();
        }
        public override string GetEquipmentName() {
            return "Leather Suit";
        }

        public static string GetItemName() {
            return new ArmorLeatherSuit(new Warrior()).GetEquipmentName();
        }
    }
}
