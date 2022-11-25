namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    internal class TrapFactory : IEncounterFactory
    {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage)
        {
            return new TrapEncounter(adventurerName, adventurerLuck, adventurerDamage);
        }
    }
}
