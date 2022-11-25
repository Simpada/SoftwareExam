namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    internal class TreasureFactory : IEncounterFactory
    {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage)
        {
            return new TreasureEncounter(adventurerName, adventurerLuck, adventurerDamage);
        }
    }
}
