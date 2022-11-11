using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
    internal class TrinketRabbitsFoot : BasicTrinket {

        public TrinketRabbitsFoot(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 401;
        }

        public override void EditStats() {
            Luck += 2;
        }

    }
}
