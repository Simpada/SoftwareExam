using SoftwareExam.CoreProgram.Adventurers.Decorator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers {
    internal abstract class Adventurer {

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Luck { get; set; }

        public string Name { get; set; } = "";
        public string Class { get; set; } = "";


        private List<BaseDecoratedAdventurer> Equipment = new();


        public Adventurer() {
            Name = RandomName();
        }

        private string RandomName() {

            return "Ken The Mass Murderer";
        }

        public override string ToString() {
            return $"|--------------------------------------\n" +
                   $"|  Name:   {Name}\n" +
                   $"|  Class:  {Class}\n" +
                   $"|  Health: {Health}\n" +
                   $"|  Damage: {Damage}\n" +
                   $"|  Luck:   {Luck}\n" +
                   $"|--------------------------------------";
        }

        public abstract string GetEquipmentDescription();

        public void ReEquip() {

            // This function will apply all equipment anew to a DecoratedAdventurer

        }

    }
}
