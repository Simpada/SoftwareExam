namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory {
    internal class ExplorationFactory : IEncounterFactory {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage) {
            return new ExplorationEncounter(adventurerName, adventurerLuck, adventurerDamage);
        }
    }
}
