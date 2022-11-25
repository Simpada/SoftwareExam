namespace SoftwareExam.CoreProgram.Adventurers.Factory {
    internal class MageFactory : IAdventurerFactory {
        public Adventurer CreateAdventurer() {
            return new Mage();
        }
    }
}
