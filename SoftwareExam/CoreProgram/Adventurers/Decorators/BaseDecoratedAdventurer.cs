﻿using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    public abstract class BaseDecoratedAdventurer : Adventurer {

        internal int Id = 0;

        public string[] AllowedClasses = new string[] {"Warrior", "Mage", "Rogue"};

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
            SymbolArray = BaseAdventurer.SymbolArray;
        }

        public abstract void EditStats();

    }
}