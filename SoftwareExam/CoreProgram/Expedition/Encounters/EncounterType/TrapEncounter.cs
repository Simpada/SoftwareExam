using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Expedition.Encounters {
    internal class TrapEncounter : Encounter {
        public TrapEncounter(string adventurerName, int adventurerLuck, int adventurerDamage) : base(adventurerName, adventurerLuck, adventurerDamage) {
        }

        public override bool RunEncounter(out Currency reward, out string description) {
            int trapDifficulty = Random.Next(20);
            int adventurerRoll = Random.Next(20) + AdventurerLuck;
            reward = new();

            description = PickOne(new string[] {
                    $"{AdventurerName} stepped on a mouse trap.",
                    $"{AdventurerName} triggered a tripwire, sending poison darts towards them.",
                    $"{AdventurerName} stepped on a banana peel and lost balance.",
                    $"A tree suddenly fell towards {AdventurerName}.",
                    $"{AdventurerName} chased a boar, but tripped over a rock.",
            });

            if (trapDifficulty >= adventurerRoll) {
                description += $" {AdventurerName} could not avoid it and got hurt.";
                return false;
            } else {
                description += $" {AdventurerName} nimbly avoided it.";

                return true;
            }
        }
    }
}
