using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
    internal class ArmorPlateArmor : BasicArmor {


        public ArmorPlateArmor(Adventurer Adventurer) : base(Adventurer) {
            AllowedClasses = new string[] {"Warrior"};
            Id = 101;
        }

        public override void EditStats() {
            Health += 5;
        }

    }
}
