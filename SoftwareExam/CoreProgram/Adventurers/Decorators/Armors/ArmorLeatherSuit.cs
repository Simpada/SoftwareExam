using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Armors
{
    internal class ArmorLeatherSuit : BasicArmor
    {
        public static new readonly string[] AllowedClasses = new string[] { "Warrior", "Rogue"};
        public static new readonly Currency Cost = new(0,0,1);


        public ArmorLeatherSuit(Adventurer adventurer) : base(adventurer)
        {
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
        public override string GetEquipmentName() {
            return "Leather Suit";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new ArmorLeatherSuit(new Warrior()).GetEquipmentName();
        }
    }
}
