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
            AddEquipment(this);
        }

        public BaseDecoratedAdventurer AddItem (Adventurer Adventurer) {
            BaseAdventurer = Adventurer;
            Adventurer.AddEquipment(this);
            return this;
        }

        public abstract void EditStats();

        public override string ToString() {
            return BaseAdventurer.ToString();
        }

    }
}
