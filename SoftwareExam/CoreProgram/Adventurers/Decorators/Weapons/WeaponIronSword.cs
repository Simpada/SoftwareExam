using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons {
    internal class WeaponIronSword : BasicWeapon {
        public WeaponIronSword(Adventurer Adventurer) : base(Adventurer) {
            AllowedClasses = new string[] { "Warrior", "Rogue" };
            Id = 501;
        }

        public override void EditStats() {
            Damage += 2;
        }
    }
}
