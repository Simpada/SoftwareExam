using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors {
    internal class ArmorPlateArmor : BasicArmor {


        public ArmorPlateArmor(Adventurer Adventurer) : base(Adventurer) {
            AllowedClasses = new string[] {"Warrior"};
            ItemId = 101;
        }

        public override void EditStats() {
            Health += 5;
        }

        public override string GetEquipmentDescription() {
            return "This heavy armor grants a +5 boost to health.";
        }
        
        public static string GetItemDescription() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorPlateArmor(new Warrior()).GetEquipmentDescription();
        }
    }
}
