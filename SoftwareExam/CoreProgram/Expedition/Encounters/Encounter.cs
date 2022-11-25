using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Expedition.Encounters
{
    public abstract class Encounter
    {
        protected readonly Random Random = new();
        protected readonly string AdventurerName;
        protected readonly int AdventurerLuck;
        protected readonly int AdventurerDamage;
        protected string Description;
        protected Currency Reward;

        public Encounter(string adventurerName, int adventurerLuck, int adventurerDamage)
        {
            AdventurerName = adventurerName;
            AdventurerLuck = adventurerLuck;
            AdventurerDamage = adventurerDamage;
        }

        public abstract bool RunEncounter(out Currency reward, out string description);

        protected string PickOne(string[] alternatives)
        {
            return alternatives[Random.Next(alternatives.Length)];
        }
    }
}