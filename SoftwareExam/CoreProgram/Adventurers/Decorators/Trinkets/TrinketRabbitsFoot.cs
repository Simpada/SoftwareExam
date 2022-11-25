using SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets {
    internal class TrinketRabbitsFoot : BasicTrinket {

        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue", "Mage" };
        public static new readonly Currency Cost = new(0,5,2);

        public TrinketRabbitsFoot(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 401;

            Value = BaseAdventurer.Value + Cost;
        }

        public override void EditStats() {
            Luck += 2;
        }

        public override string GetEquipmentDescription()
        {
            return "This old smelly rabbit foot grants you +2 luck somehow";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketRabbitsFoot(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Rabbits Foot";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new TrinketRabbitsFoot(new Warrior()).GetEquipmentName();
        }
    }
}
