using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    internal class BasicWeapon : BaseDecoratedAdventurer{
        public BasicWeapon(Adventurer Adventurer) : base(Adventurer) {
            ItemId = 500;
        }
        public override void EditStats() {
            
        }

        public override string GetEquipmentDescription() {
            return "There's nothing special about this weapon.";
        }
        public override string GetEquipmentName() {
            return "Basic Weapon";
        }

    }
}
