using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    internal class GameManager {

        private readonly Recruitment Recruitment;
        private readonly DataBaseAccess DataBaseAccess;
        private Player Player;

        public GameManager() {
            Player = new Player();
            DataBaseAccess = new DataBaseAccess("");
            Recruitment = new Recruitment();
        }

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

        public void SetPlayer(Player player) {
            Player = player;
        }

        public List<Adventurer> GetAllAdventurers() {
            return Player.Adventurers;
        }
        public Adventurer GetAdventurer(int who) {
            return Player.Adventurers[who];
        }

        public string SaveGame() {
            throw new NotImplementedException();
        }

        internal string GetBalanceString() {
            return Player.Balance.ToString();
        }
        
        
        internal Currency GetBalanceValue() {
            return Player.Balance;
        }

        internal int GetAdventurerCount() {
            return Player.Adventurers.Count();
        }

        internal int GetAvailableAdventurers() {
            return Player.AvailableAdventurers;
        }
    }
}
