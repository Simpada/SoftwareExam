using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ArmorElvenRobe : BasicArmor {

        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 0, 8);

        public ArmorElvenRobe(Adventurer adventurer) : base(adventurer) {

            Value = BaseAdventurer.Value + Cost;
            ItemId = 102;
        }

        public override void EditStats() {
            Health += 2;
            Luck += 1;
        }

        public override string GetEquipmentDescription() {
            return "This elven robe grants +2 boost to health and +1 to luck";
        }

        public static string GetItemDescription() {
            return new ArmorElvenRobe(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Elven Robe";
        }

        public static string GetItemName() {
            return new ArmorElvenRobe(new Warrior()).GetEquipmentName();
        }
    }
}
