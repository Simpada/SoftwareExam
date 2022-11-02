using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    internal class GameManager {

        private Recruitment Recruitment;
        private DataBaseAccess DataBaseAccess;

        private List<Adventurer> Adventurers = new();

        public GameManager() {
            DataBaseAccess = new DataBaseAccess();
            Recruitment = new Recruitment();
        }

        public void RecruitAdventurer(int type) {
            // Replace with push to Player object
            Adventurers.Add(Recruitment.RecruitAdventurer(type));
        }

        public void DismissAdventurer(int who) {
            Adventurers.RemoveAt(who);
        }

        public List<Adventurer> GetAllAdventurers() {
            return Adventurers;
        }
        public Adventurer GetAdventurer(int who) {
            return Adventurers[who];
        }

        public string SaveGame() {
            throw new NotImplementedException();
        }
    }
}
