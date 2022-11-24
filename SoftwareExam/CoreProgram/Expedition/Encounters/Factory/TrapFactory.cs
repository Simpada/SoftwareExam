namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    internal class TrapFactory : EncounterFactory
    {
        public override Encounter CreateEncounter(string adventurerName, int adventureLuck, int adventurerDamage)
        {
            return new TrapEncounter(adventurerName, adventureLuck, adventurerDamage);
        }
    }
}
