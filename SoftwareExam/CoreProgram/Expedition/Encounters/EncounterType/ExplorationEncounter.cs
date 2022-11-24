namespace SoftwareExam.CoreProgram.Expedition.Encounters
{
    internal class ExplorationEncounter : Encounter
    {
        public ExplorationEncounter(string adventurerName, int adventureLuck, int adventurerDamage) : base(adventurerName, adventureLuck, adventurerDamage)
        {
            Description = AdventurerName + PickOne(
                new string[] {
                    " saw a rabbit. It ran away.",
                    $" found a crossroad. They went {Direction()}.",
                    " sat down for a fast picknick.",
                    " hides when 100 dragons flew past.",
                    " chased a boar, but tripped.",
                    " watched the sunrise.",
                    " stopped to flirt with a succubus.",
                    " found a stick. It was a cool stick, so they kept it."
                });
        }

        public override bool RunEncounter(out Currency reward, out string description)
        {
            reward = new();
            description = Description;            
            return true;
        }

        private string Direction()
        {
            if (Random.Next(2) >= 1)
            {
                return "right";
            }
            else
            {
                return "left";
            }
        }
    }
}
