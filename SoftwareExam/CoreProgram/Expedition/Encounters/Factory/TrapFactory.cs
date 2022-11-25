namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrapFactory : IEncounterFactory {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage) {
            return new TrapEncounter(adventurerName, adventurerLuck, adventurerDamage);
        }
    }
}
