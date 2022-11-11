using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    public class BasicHat : BaseDecoratedAdventurer {

        public BasicHat(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 200;
        }

        public override void EditStats() {
            
        }

        public override string GetEquipmentDescription() {
            return "There's nothing special about this hat.";
        }

    }
}
