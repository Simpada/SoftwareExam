namespace SoftwareExam.CoreProgram.Adventurers {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Rogue : Adventurer {

        public Rogue() {
            Class = "Rogue";
            Health = 2;
            Damage = 3;
            Luck = 3;

            SymbolArray = new string[] {
                "  |\\   ",
                "  | \\      ",
                "  | |      ",
                " [===]     ",
                "  | |      ",
                "  |_|      "
            };
        }

        #region Necessary functions in the abstract class, but does nothing here
        public override string GetEquipmentDescription() {
            return "";
        }

        public override string GetEquipmentName() {
            return "";
        }
        #endregion
    }
}
