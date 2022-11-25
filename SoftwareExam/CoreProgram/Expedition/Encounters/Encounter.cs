using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Expedition.Encounters {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public abstract class Encounter {
        protected readonly Random Random = new();
        protected readonly string AdventurerName;
        protected readonly int AdventurerLuck;
        protected readonly int AdventurerDamage;
        protected string Description;
        protected Currency Reward;

        public Encounter(string adventurerName, int adventurerLuck, int adventurerDamage) {
            AdventurerName = adventurerName;
            AdventurerLuck = adventurerLuck;
            AdventurerDamage = adventurerDamage;
        }

        /// <summary>
        /// Runs an encounter to determine its outcome and potential reward
        /// </summary>
        /// <param name="reward">The reward the player gets, might not be anything</param>
        /// <param name="description">The description of the event that occured</param>
        /// <returns></returns>
        public abstract bool RunEncounter(out Currency reward, out string description);

        protected string PickOne(string[] alternatives) {
            return alternatives[Random.Next(alternatives.Length)];
        }
    }
}
