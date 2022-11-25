namespace SoftwareExam.CoreProgram.Adventurers.Factory {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MageFactory : IAdventurerFactory {
        public Adventurer CreateAdventurer() {
            return new Mage();
        }
    }
}
