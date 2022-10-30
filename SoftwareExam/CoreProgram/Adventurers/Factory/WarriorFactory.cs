using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Factory {
    internal class WarriorFactory : AdventurerFactory {
        public override Adventurer CreateAdventurer() {
            return new Warrior();
        }
    }
}
