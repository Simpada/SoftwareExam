﻿using SoftwareExam.CoreProgram.Adventurers;
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

        public string GetBalanceString() {
            return Player.Balance.ToString();
        }

        public Currency GetBalanceValue() {
            return Player.Balance;
        }

        public void CheckBalance(out bool canAfford, out string newBalance, out string cost) {

            canAfford = Recruitment.CheckBalance(Player.Balance);
            cost = Recruitment.Price.ToString();
            newBalance = (Player.Balance - Recruitment.Price).ToString();

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

        public string[] GetAllAdventurerCards() {

            // This sets the maximum amount of adventurers you can display
            string[]AdventurerCards = new string[5];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }
        public void GetAdventurerSellValue(int who, out string name, out string value) {

            double sellMultiplier = 0.7;

            Adventurer adventurer = Player.Adventurers[who];

            name = adventurer.Name;
            value = (adventurer.Value * sellMultiplier).ToString();
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
            
            //DataBaseAccess.Save(Player);

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
