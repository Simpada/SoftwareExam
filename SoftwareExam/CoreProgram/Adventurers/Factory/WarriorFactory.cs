namespace SoftwareExam.CoreProgram.Adventurers.Factory {
    internal class WarriorFactory : IAdventurerFactory {
        public Adventurer CreateAdventurer() {
            return new Warrior();
        }
    }
}
