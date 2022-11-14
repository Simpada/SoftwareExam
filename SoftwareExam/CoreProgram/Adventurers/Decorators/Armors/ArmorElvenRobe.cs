using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorElvenRobe : BasicArmor
    {
        public ArmorElvenRobe(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new string[] {"Mage"};
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
    }
}
