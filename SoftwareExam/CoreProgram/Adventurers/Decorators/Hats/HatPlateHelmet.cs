using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using SoftwareExam.CoreProgram.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Hats
{
    internal class HatPlateHelmet : BasicHat {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior"};
        public static new readonly Currency Cost = new(0,0,2);

        public HatPlateHelmet(Adventurer adventurer) : base(adventurer) {
            ItemId = 201;

            Value = BaseAdventurer.Value + Cost;
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
            return new HatPlateHelmet(new Warrior()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Plate Helmet";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new HatPlateHelmet(new Warrior()).GetEquipmentName();
        }
    }
}
