namespace SoftwareExam.CoreProgram.Expedition.Encounters
{
    public abstract class Encounter
    {
        protected readonly Random Random = new();
        protected readonly string AdventurerName;
        private string Description = "";

        public Encounter(string adventurerName)
        {
            AdventurerName = adventurerName;
        }

        public abstract bool RunEncounter(out Currency reward, out string description);

        protected string PickOne(string[] alternatives)
        {
            return alternatives[Random.Next(alternatives.Length)];
        }
    }
}