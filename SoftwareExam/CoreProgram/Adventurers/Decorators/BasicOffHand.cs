using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    internal class BasicOffHand : BaseDecoratedAdventurer {
        public BasicOffHand(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 300;
        }

        public override void EditStats() {
            
        }

        public override string GetEquipmentDescription() {
            return "There's nothing special about this off hand.";
        }
    }
}
