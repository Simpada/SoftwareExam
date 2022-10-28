using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurer {
    internal class Rogue : Adventurer {

        public Rogue() {
            Health = 5;
            Damage = 5;
            Luck = 10;
        }

        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return $"Name: {Name}\n" +
                   "Class: Rogue\n" +
                   $"Health: {Health}\n" +
                   $"Damage: {Damage}\n" +
                   $"Luck: {Luck}";
        }
    }
}
