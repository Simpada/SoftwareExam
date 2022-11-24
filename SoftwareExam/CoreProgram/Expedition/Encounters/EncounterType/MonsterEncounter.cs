namespace SoftwareExam.CoreProgram.Expedition.Encounters.Encounter
{
    internal class MonsterEncounter : Encounter
    {
        public override void GenerateEncounter()
        {
            throw new NotImplementedException();
        }

        public bool RunEncounter(out Currency reward, out string description)
        {
            int monsterRoll = Random.Next(20) + EncounterDamage;
            int adventurerRoll = Random.Next(20) + AdventurerDamage;

            if (adventurerRoll >= monsterRoll)
            {
                reward = Reward;
                description = Description;
                return true;
            }
            else
            {
                reward = new();
                description = Description;
                return false;
            }
        }

        public override bool RunEncounter(out Currency reward, out string description)
        {
            throw new NotImplementedException();
        }
    }
}
