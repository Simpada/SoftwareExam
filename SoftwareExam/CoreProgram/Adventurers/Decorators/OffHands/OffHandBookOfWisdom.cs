using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands
{
    internal class OffHandBookOfWisdom : BasicOffHand
    {

        public static new readonly string[] AllowedClasses = new string[] { "Mage" };
        public static new readonly Currency Cost = new(0,5,3);

        public OffHandBookOfWisdom(Adventurer Adventurer) : base(Adventurer)
        {
            ItemId = 303;
        }

        public override void EditStats()
        {
            Damage += 3;
        }

        public override string GetEquipmentDescription()
        {
            return "This book contains ancient knowledge. Grants +3 damage";
        }

        public static string GetItemDescription()
        {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandBookOfWisdom(new Mage()).GetEquipmentDescription();
        }

        public override string GetEquipmentName() {
            return "Book of Wisdom";
        }

        public static string GetItemName() {
            // This is kinda dumb, but it works without need for repeating code
            return new OffHandBookOfWisdom(new Warrior()).GetEquipmentName();
        }
    }
}
