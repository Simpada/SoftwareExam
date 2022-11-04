using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    public class Recruitment {

        private AdventurerFactory? Factory;


        public Currency Price { get; set; } = new(0, 5, 0);

        public bool CheckBalance(Currency balance) {

            if (balance >= Price) {
                return true;
            } else {
                return false;
            }

        }


        public Adventurer? RecruitAdventurer(int type, Currency balance) {

            if (type == 1) {
                Factory = new WarriorFactory();
            } else if (type == 2) {
                Factory = new MageFactory();
            } else if (type == 3) {
                Factory = new RogueFactory();
            } else {
                throw new Exception("Invalid recruitment class");
            }

            if (CheckBalance(balance)) {
                return DressAdventurer(Factory.CreateAdventurer());
            } else {
                return null;
            }            
        }

        private static Adventurer DressAdventurer(Adventurer Adventurer) {

            Adventurer.AddEquipment(new BasicHat(Adventurer));
            Adventurer.AddEquipment(new BasicArmor(Adventurer));
            Adventurer.AddEquipment(new BasicWeapon(Adventurer));
            Adventurer.AddEquipment(new BasicOffHand(Adventurer));
            Adventurer.AddEquipment(new BasicTrinket(Adventurer));

            //Adventurer = Adventurer.ReEquip();

            return Adventurer;
        }

    }
}
