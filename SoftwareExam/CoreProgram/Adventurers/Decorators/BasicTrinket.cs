using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    internal class BasicTrinket : BaseDecoratedAdventurer{
        public BasicTrinket(Adventurer Adventurer) : base(Adventurer) {
        }

        public override void EditStats() {
            
        }

        public override string GetEquipmentDescription() {
            return "There's nothing special about this trinket.";
        }

        public override string ToString() {
            return base.ToString();
        }

    }
}
