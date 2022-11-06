using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
    internal class HatPlateHelmet : BasicHat {
        public HatPlateHelmet(Adventurer Adventurer) : base(Adventurer) {
            AllowedClasses = new string[] { "Warrior" };
            Id = 201;
        }

        public override void EditStats() {
            Health += 2;
        }

        public override string GetEquipmentDescription() {
            return "A durable helmet that increase your health";
        }
    }
}
