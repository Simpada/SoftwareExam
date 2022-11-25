namespace SoftwareExam.CoreProgram.Adventurers {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Mage : Adventurer {

        public Mage() {
            Class = "Mage";
            Health = 1;
            Damage = 6;
            Luck = 1;

            SymbolArray = new string[] {
                "   _",
                "  \\*/      ",
                "   |       ",
                "   |       ",
                "   |       ",
                "   V       "
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
