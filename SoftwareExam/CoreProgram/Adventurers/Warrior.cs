using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    internal class Warrior : Adventurer {

        public Warrior() {
            Class = "Warrior";
            Health = 10;
            Damage = 5;
            Luck = 5;
        }

        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }



        public override string ToString() {

            return @$"    | ______    
    | | __ |    Name:   {Name}
    | | || |    Class:  {Class}
    | | || |    Health: {Health}
    | \ '' /    Damage: {Damage}
    |  \__/     Luck:   {Luck}";
        }

    }
}
