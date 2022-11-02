using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    internal class GameManager {

        private DataBaseAccess _dataBaseAccess;

        private List<Adventurer> Adventurers = new();

        public GameManager() {
            _dataBaseAccess = new DataBaseAccess("");

            
        }

        public string SaveGame() {
            throw new NotImplementedException();
        }
    }
}
