using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    public class Rogue : Adventurer {

        public Rogue() {
            Class = "Rogue";
            Health = 5;
            Damage = 5;
            Luck = 10;

            SymbolArray = new string[] {
                "  |\\   ",
                "  | \\      ",
                "  | |      ",
                " [===]     ",
                "  | |      ",
                "  |_|      "
            };
        }

        public override string GetEquipmentDescription() {
            throw new NotImplementedException();
        }

        public override string GetEquipmentName() {
            throw new NotImplementedException();
        }

        //    public override string ToString() {
        //        return @$"    |  |\      
        //|  | \      Name:   {Name}
        //|  | |      Class:  {Class}
        //| [===]     Health: {Health}
        //|  | |      Damage: {Damage}
        //|  |_|      Luck:   {Luck}";
        //    }

    }
}
