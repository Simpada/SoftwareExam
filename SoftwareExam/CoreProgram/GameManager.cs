using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    public class GameManager {

        private readonly Recruitment Recruitment;
        private readonly DataBaseAccess DataBaseAccess;
        private Player Player;

        public GameManager() {
            Player = new Player();
            DataBaseAccess = new DataBaseAccess("Data Source = tempDatabase.db");
            Recruitment = new Recruitment();
        }


        public void SetPlayer(Player player) {
            Player = player;
        }

        internal string GetBalanceString() {
            return Player.Balance.ToString();
        }


        internal Currency GetBalanceValue() {
            return Player.Balance;
        }

        // Relates to adventurers
        #region
        public bool RecruitAdventurer(int type) {
            // Replace with push to Player object

            Adventurer? adventurer = Recruitment.RecruitAdventurer(type, Player.Balance);

            if (adventurer == null) {
                return false;
            } else {
                Player.Balance -= Recruitment.Price;
                Player.Adventurers.Add(adventurer);
                return true;
            }
        }

        public void DismissAdventurer(int who) {
            Player.Adventurers.RemoveAt(who);
        }

        public List<Adventurer> GetAllAdventurers() {
            return Player.Adventurers;
        }
        public Adventurer GetAdventurer(int who) {
            return Player.Adventurers[who];
        }
        internal int GetAdventurerCount() {
            return Player.Adventurers.Count();
        }

        internal int GetAvailableAdventurers() {
            return Player.AvailableAdventurers;
        }
        #endregion

        public void SaveGame() {
            
            DataBaseAccess.Save(Player);

        }
        
        public void LoadGame(int id) {

            //DataBaseAccess.GetPlayerById(id ,out int playerId, out string playerName, out int copper, out int silver, out int gold);
            //Player = new(playerId, playerName, new Currency(copper, silver, gold));

        }



        public string[] GetPlayers()
        {
            return DataBaseAccess.RetrieveAllPlayerNames();

        }
    }
}
