namespace SoftwareExam.CoreProgram.Expedition.Encounters {
    public interface IEncounterFactory {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage);
    }
}