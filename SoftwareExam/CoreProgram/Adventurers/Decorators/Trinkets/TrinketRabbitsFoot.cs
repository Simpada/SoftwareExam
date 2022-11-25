using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrinketRabbitsFoot : BasicTrinket {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0, 5, 2);

        public TrinketRabbitsFoot(Adventurer adventurer) : base(adventurer) {
            ItemId = 401;

            Value = BaseAdventurer.Value + Cost;
        }

        public override void EditStats() {
            Luck += 2;
        }

        public override string GetEquipmentDescription() {
            return "This old smelly rabbit foot grants you +2 luck somehow";
        }

        public static string GetItemDescription() {
            return new TrinketRabbitsFoot(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Rabbits Foot";
        }

        public static string GetItemName() {
            return new TrinketRabbitsFoot(new Warrior()).GetEquipmentName();
        }
    }
}
