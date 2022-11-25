using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Expedition.Encounters {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MonsterEncounter : Encounter {
        private readonly int _encounterDamage;

        public MonsterEncounter(string adventurerName, int adventurerLuck, int adventurerDamage) : base(adventurerName, adventurerLuck, adventurerDamage) {
            int rollEncounter = Random.Next(20) + 1;

            switch (rollEncounter) {
                case <= 5:
                Description = $"{adventurerName} encountered a slime.";
                _encounterDamage = Random.Next(3);
                Reward = Currency.Convert(new Currency(Random.Next(1, 6) * (AdventurerLuck + 1), 0, 0) );
                break;
                case <= 8:
                Description = $"{adventurerName} encountered a goblin.";
                _encounterDamage = Random.Next(3, 5);
                Reward = Currency.Convert(new Currency(Random.Next(4, 8) * (AdventurerLuck + 1), 0, 0));
                break;
                case <= 12:
                Description = $"{adventurerName} encountered a lion.";
                _encounterDamage = Random.Next(5, 7);
                Reward = Currency.Convert(new Currency(Random.Next(6, 12) * (AdventurerLuck + 1), 0, 0));
                break;
                case <= 15:
                Description = $"{adventurerName} encountered an ogre.";
                _encounterDamage = Random.Next(7, 10);
                Reward = Currency.Convert(new Currency(Random.Next(8, 20) * (AdventurerLuck + 1), 0, 0));
                break;
                case < 20:
                Description = $"{adventurerName} encountered a dragon.";
                _encounterDamage = Random.Next(10, 15);
                Reward = Currency.Convert(new Currency(Random.Next(12, 30) * (AdventurerLuck + 1), 0, 0));
                break;
                default:
                Description = $"{adventurerName} encountered the demon lord!";
                _encounterDamage = Random.Next(15, 20);
                Reward = Currency.Convert(new Currency(Random.Next(20, 50) * (AdventurerLuck + 1), 0, 0));
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
