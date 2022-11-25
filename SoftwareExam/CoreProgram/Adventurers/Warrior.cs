namespace SoftwareExam.CoreProgram.Adventurers {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Warrior : Adventurer {

        public Warrior() {
            Class = "Warrior";
            Health = 5;
            Damage = 3;
            Luck = 0;

            SymbolArray = new string[] {
                " ______  ",
                " | __ |    ",
                " | || |    ",
                " | || |    ",
                " \\ '' /    ",
                "  \\__/     "
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
