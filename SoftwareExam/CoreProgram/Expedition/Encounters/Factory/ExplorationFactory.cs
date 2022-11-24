namespace SoftwareExam.CoreProgram.Expedition.Encounters.Factory
{
    internal class ExplorationFactory : EncounterFactory
    {

        public override Encounter CreateEncounter(string adventurerName, int adventureLuck, int adventurerDamage)
        {
            return new ExplorationEncounter(adventurerName,adventureLuck,adventurerDamage);
        }
    }
}
