using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands {
    internal class OffHandWoodenShield : BasicOffHand {

        public OffHandWoodenShield(Adventurer Adventurer) : base(Adventurer) {
            AllowedClasses = new string[] { "Warrior" };
        }

        public override void EditStats() {
            Health += 2;
        }
    }
}
