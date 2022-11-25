namespace SoftwareExam.CoreProgram.Adventurers {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Warrior : Adventurer {

        public Warrior() {
            Class = "Warrior";
            Health = 10;
            Damage = 5;
            Luck = 5;

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
