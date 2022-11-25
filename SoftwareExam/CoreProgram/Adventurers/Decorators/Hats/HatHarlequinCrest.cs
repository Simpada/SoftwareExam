using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HatHarlequinCrest : BasicHat {

        public static new readonly string[] AllowedClasses = new string[] { "Mage", "Rogue"};
        public static new readonly Currency Cost = new(0, 0, 10);

        public HatHarlequinCrest(Adventurer adventurer) : base(adventurer) {
            Value = BaseAdventurer.Value + Cost;
            ItemId = 203;
        }

        public override void EditStats() {
            Health += 1;
            Damage += 1;
            Luck += 3;
        }

        public override string GetEquipmentDescription() {
            return "This hat was found on Hell difficulty in Diablo 2. Gives +1 health, +1 damage, and +3 luck!";
        }

        public static string GetItemDescription() {
            return new HatHarlequinCrest(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Harlequin Crest";
        }

        public static string GetItemName() {
            return new HatHarlequinCrest(new Warrior()).GetEquipmentName();
        }
    }
}
