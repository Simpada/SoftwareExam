using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats {
    internal class HatPlateHelmet : BasicHat {
        public HatPlateHelmet(Adventurer adventurer) : base(adventurer) {
            AllowedClasses = new string[] { "Warrior" };
            ItemId = 201;
        }

        public override void EditStats() {
            Health += 4;
        }

        public override string GetEquipmentDescription() {
            return "A durable helmet that increase your health by 2";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorPlateArmor(new Warrior()).GetEquipmentDescription();
        }
    }
}
