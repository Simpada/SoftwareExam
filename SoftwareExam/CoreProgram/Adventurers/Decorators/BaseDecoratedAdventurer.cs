using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public abstract class BaseDecoratedAdventurer : Adventurer {

        public int ItemId { get; set; } = 0;
        public static readonly string[] AllowedClasses = new string[] { "Warrior", "Mage", "Rogue" };
        public Adventurer BaseAdventurer { get; set; }
        public static readonly Currency Cost = new();

        /// <summary>
        /// Makes certain the decorator, inherits its base stats from its baseAdventurer
        /// </summary>
        /// <param name="adventurer"></param>
        public BaseDecoratedAdventurer(Adventurer adventurer) {
            Id = adventurer.Id;
            BaseAdventurer = adventurer;
            Health = BaseAdventurer.Health;
            Damage = BaseAdventurer.Damage;
            Luck = BaseAdventurer.Luck;
            Name = BaseAdventurer.Name;
            Class = BaseAdventurer.Class;
            Value = BaseAdventurer.Value;
            Equipment = BaseAdventurer.Equipment;
            SymbolArray = BaseAdventurer.SymbolArray;
        }

        public abstract void EditStats();
    }
}
