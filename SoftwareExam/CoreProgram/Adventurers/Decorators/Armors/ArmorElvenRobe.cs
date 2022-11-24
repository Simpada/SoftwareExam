using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorElvenRobe : BasicArmor
    {

        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0, 5, 2);

        public ArmorElvenRobe(Adventurer adventurer) : base(adventurer)
        {
            ItemId = 102;
        }

        public override void EditStats()
        {
            Health += 2;
            Luck += 1;
        }

        public override string GetEquipmentDescription()
        {
            return "This elven robe grants +2 boost to health and +1 to luck";
        }

        public static string GetItemDescription()
        {
            return new ArmorElvenRobe(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Elven Robe";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorElvenRobe(new Warrior()).GetEquipmentName();
        }
    }
}
