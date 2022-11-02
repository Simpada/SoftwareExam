using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    internal class Recruitment {

        private AdventurerFactory? Factory;

        public Adventurer RecruitAdventurer(int type) {

            if (type == 1) {
                Factory = new WarriorFactory();
            } else if (type == 2) {
                Factory = new MageFactory();
            } else if (type == 3) {
                Factory = new RogueFactory();
            } else {
                throw new Exception("Invalid recruitment class");
            }

            return Factory.CreateAdventurer();
        }
    }
}
