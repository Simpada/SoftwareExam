using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    public abstract class BaseDecoratedAdventurer : Adventurer {

        public Adventurer BaseAdventurer { get; set; }

        public BaseDecoratedAdventurer(Adventurer Adventurer) {
            BaseAdventurer = Adventurer;
            Health = BaseAdventurer.Health;
            Damage = BaseAdventurer.Damage;
            Luck = BaseAdventurer.Luck;
            Name = BaseAdventurer.Name;
            Class = BaseAdventurer.Class;
            Value = BaseAdventurer.Value;
            Equipment = BaseAdventurer.Equipment;
        }

        public abstract void EditStats();

        public override string ToString() {
            return BaseAdventurer.ToString();
        }

    }
}
