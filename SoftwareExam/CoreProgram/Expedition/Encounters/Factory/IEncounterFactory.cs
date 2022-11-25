namespace SoftwareExam.CoreProgram.Expedition.Encounters {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IEncounterFactory {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage);
    }
}