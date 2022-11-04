using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    internal class BasicArmor : BaseDecoratedAdventurer{
        public BasicArmor(Adventurer Adventurer) : base(Adventurer) {
        }
        public override void EditStats() {
            
        }

        public override string GetEquipmentDescription() {
            return "There's nothing special about this armor.";
        }

        public override string ToString() {
            return base.ToString();
        }
    }
}
