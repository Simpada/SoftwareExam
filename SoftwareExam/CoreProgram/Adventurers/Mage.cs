using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    public class Mage : Adventurer {

        public Mage() {
            Class = "Mage";
            Health = 5;
            Damage = 10;
            Luck = 5;
        }

        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return @$"    |   _       
    |  \*/      Name:   {Name}
    |   |       Class:  {Class}
    |   |       Health: {Health}
    |   |       Damage: {Damage}
    |   V       Luck:   {Luck}";


        }

    }
}
