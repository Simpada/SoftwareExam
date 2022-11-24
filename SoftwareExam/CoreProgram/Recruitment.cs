using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    public class Recruitment {

        private IAdventurerFactory? Factory;

        public Currency Price { get; set; } = new(0, 0, 5);

        public bool CheckBalance(Currency balance) {

            if (balance >= Price) {
                return true;
            } else {
                return false;
            }
        }

        public Adventurer? RecruitAdventurer(int type, Currency balance) {

            if (type == 1) {
                Factory = new WarriorFactory();
            } else if (type == 2) {
                Factory = new MageFactory();
            } else if (type == 3) {
                Factory = new RogueFactory();
            } else {
                throw new Exception("Invalid recruitment class");
            }

            if (CheckBalance(balance)) {
                Adventurer NewAdventurer = Factory.CreateAdventurer();
                return NewAdventurer.GetStartingGear(); 
            } else {
                return null;
            }            
        }
    }
}
