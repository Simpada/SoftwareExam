namespace SoftwareExam.CoreProgram.Adventurers.Factory {
    internal class RogueFactory : IAdventurerFactory {
        public Adventurer CreateAdventurer() {
            return new Rogue();
        }
    }
}
