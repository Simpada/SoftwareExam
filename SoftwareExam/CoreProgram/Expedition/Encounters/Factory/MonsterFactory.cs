namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    public class MonsterFactory : IEncounterFactory
    {
        public Encounter CreateEncounter(string adventurerName, int adventurerLuck, int adventurerDamage)
        {
            return new MonsterEncounter(adventurerName, adventurerLuck, adventurerDamage);
        }
    }
}
