using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Expedition.Encounters {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MonsterEncounter : Encounter {
        private readonly int _encounterDamage;

        public MonsterEncounter(string adventurerName, int adventurerLuck, int adventurerDamage) : base(adventurerName, adventurerLuck, adventurerDamage) {
            int rollEncounter = Random.Next(20) + 1;
            int rewardMultiplier = AdventurerLuck / 100 + 1;

            switch (rollEncounter) {
                case <= 5:
                Description = $"{adventurerName} encountered a slime.";
                _encounterDamage = Random.Next(3);
                Reward = new Currency(Random.Next(4) * rewardMultiplier, Random.Next(2) * rewardMultiplier, 0);
                break;
                case <= 8:
                Description = $"{adventurerName} encountered a goblin.";
                _encounterDamage = Random.Next(5);
                Reward = new Currency(Random.Next(4) * rewardMultiplier, Random.Next(4) * rewardMultiplier, 0);
                break;
                case <= 12:
                Description = $"{adventurerName} encountered a lion.";
                _encounterDamage = Random.Next(7);
                Reward = new Currency(Random.Next(3) * rewardMultiplier, Random.Next(7) * rewardMultiplier, 0);
                break;
                case <= 15:
                Description = $"{adventurerName} encountered an ogre.";
                _encounterDamage = Random.Next(10);
                Reward = new Currency(Random.Next(4) * rewardMultiplier, Random.Next(4) * rewardMultiplier, Random.Next(2) * rewardMultiplier);
                break;
                case < 20:
                Description = $"{adventurerName} encountered a dragon.";
                _encounterDamage = Random.Next(15);
                Reward = new Currency(Random.Next(8) * rewardMultiplier, Random.Next(7) * rewardMultiplier, Random.Next(4) * rewardMultiplier);
                break;
                default:
                Description = $"{adventurerName} encountered the demon lord!";
                _encounterDamage = Random.Next(20);
                Reward = new Currency(Random.Next(9) * rewardMultiplier, Random.Next(9) * rewardMultiplier, Random.Next(10) * rewardMultiplier);
                break;
            }
        }
        public override bool RunEncounter(out Currency reward, out string description) {
            int monsterRoll = Random.Next(20) + _encounterDamage;
            int adventurerRoll = Random.Next(20) + AdventurerDamage;

            if (adventurerRoll >= monsterRoll) {
                reward = Reward;
                description = Description + $" {AdventurerName} slew the monster and earned {Reward}.";
                return true;
            } else {
                reward = new();
                description = Description + $" {AdventurerName} was defeated and suffered a wound.";
                return false;
            }
        }
    }
}
