namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    public class MonsterFactory : EncounterFactory
    {
        public override Encounter CreateEncounter(string adventurerName, int adventureLuck, int adventurerDamage)
        {
            return new MonsterEncounter(adventurerName, adventureLuck, adventurerDamage);
        }

    }
}
