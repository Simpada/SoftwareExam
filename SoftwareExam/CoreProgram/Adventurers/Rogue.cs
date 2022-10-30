using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    internal class Rogue : Adventurer {

        public Rogue() {
            Class = "Rogue";
            Health = 5;
            Damage = 5;
            Luck = 10;
        }

        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }

    }
}
