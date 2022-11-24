﻿namespace SoftwareExam.CoreProgram.Expedition.Encounters
{
    internal class TreasureEncounter : Encounter
    {
        public TreasureEncounter(string adventurerName, int adventureLuck, int adventurerDamage) : base(adventurerName, adventureLuck, adventurerDamage)
        {
        }

        public override bool RunEncounter(out Currency reward, out string description)
        {
            reward = RollReward();

            description = AdventurerName + PickOne(new string[] {
                    " came across a dead adventurer.",
                    " found a hollow tree trunk.",
                    " broke into a farmer's house and broke a pot.",
                    " stumbled upon a treasure chest.",
                    " picked a noblemans pocket.",
                    " found coins on the ground.",
                    " discovered an abandoned dragon hoard."
            }) + $" They found {reward}.";

            return true;
        }

        private Currency RollReward()
        {
            int rollCopper = Random.Next(12) * AdventurerLuck;
            return Currency.Convert(new Currency(rollCopper, 0, 0));
        }
    }
}