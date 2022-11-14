using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorLeatherSuit : BasicArmor
    {
        public ArmorLeatherSuit(Adventurer adventurer) : base(adventurer)
        {
            AllowedClasses = new String[] { "Warrior", "Rogue" };
            ItemId = 103;
        }

        public override void EditStats()
        {
            Health += 3;
            Luck += 1;
        }

        public override string GetEquipmentDescription()
        {
            return "This leather suit grants +3 boost to health and +1 to luck";
        }

        public static string GetItemDescription()
        {
            return new ArmorLeatherSuit(new Rogue()).GetEquipmentDescription();
        }
    }
}
