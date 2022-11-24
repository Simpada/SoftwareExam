namespace SoftwareExam.CoreProgram.Expedition.Encounters
{
    public abstract class EncounterFactory
    {
        public abstract Encounter CreateEncounter(string adventurerName, int adventureLuck, int adventurerDamage);
    }
}
