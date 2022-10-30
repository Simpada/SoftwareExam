using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorator {
    internal class BaseDecoratedAdventurer : Adventurer {
        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            throw new NotImplementedException();
        }
    }
}
