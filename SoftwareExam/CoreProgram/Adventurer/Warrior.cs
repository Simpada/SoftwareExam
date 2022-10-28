using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurer {
    internal class Warrior : Adventurer {

        public Warrior() {
            Health = 10;
            Damage = 5;
            Luck = 5;
        }

        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            throw new NotImplementedException();
        }
    }
}
