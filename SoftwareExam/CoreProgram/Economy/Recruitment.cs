using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Factory;

namespace SoftwareExam.CoreProgram.Economy {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public class Recruitment {

        private IAdventurerFactory? _factory;

        public Currency Price { get; set; } = new(0, 0, 5);

        /// <summary>
        /// Checks the type of adventurer wanted, gets the appropriate factory, and makes a new adventurer with starting gear, if the player can afford it
        /// </summary>
        /// <param name="type">The type of adventurer</param>
        /// <param name="balance">How much money the player has</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Adventurer? RecruitAdventurer(int type, Currency balance) {

            if (type == 1) {
                _factory = new WarriorFactory();
            } else if (type == 2) {
                _factory = new MageFactory();
            } else if (type == 3) {
                _factory = new RogueFactory();
            } else {
                throw new Exception("Invalid recruitment class");
            }

            if (CheckBalance(balance)) {
                Adventurer NewAdventurer = _factory.CreateAdventurer();
                return NewAdventurer.GetStartingGear();
            } else {
                return null;
            }
        }

        public bool CheckBalance(Currency balance) {

            if (balance >= Price) {
                return true;
            } else {
                return false;
            }
        }
    }
}
