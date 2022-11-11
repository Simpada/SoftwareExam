using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Hats;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets;
using SoftwareExam.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {
    public class GameManager {

        private readonly Recruitment Recruitment;
        private readonly DataBaseAccess DataBaseAccess;
        private readonly Armory Armory;
        private Player Player;
        private readonly int MaxAdventurers = 5;

        public GameManager() {
            Player = new Player();
            DataBaseAccess = new DataBaseAccess("Data Source = tempDatabase.db");
            Recruitment = new Recruitment();
            Armory = new Armory();
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
            string[]AdventurerCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }


        public string[] GetAllItemCards() {

            string[] ItemCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                ItemCards[i] = Adventurers[i].GetItemCard();
            }

            return ItemCards;
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

            DataBaseAccess.Save(Player);

        }

        public void LoadGame(int Id) {

            Player = DataBaseAccess.GetPlayerById(Id);

            List<Adventurer> Adventurers = DataBaseAccess.GetAdventurers(Id);

            foreach (Adventurer adventurer in Adventurers) {
                List<int> itemCodes = DataBaseAccess.GetDecorators(adventurer.Id);

                // Parse items and give to adventurers here

                foreach (int itemCode in itemCodes) {

                    switch (itemCode) {
                        case 100:
                        Adventurer.AddNewItem(new BasicArmor(adventurer));
                        break;
                        case 200:
                        Adventurer.AddNewItem(new BasicHat(adventurer));
                        break;
                        case 300:
                        Adventurer.AddNewItem(new BasicOffHand(adventurer));
                        break;
                        case 400:
                        Adventurer.AddNewItem(new BasicTrinket(adventurer));
                        break;
                        case 500:
                        Adventurer.AddNewItem(new BasicWeapon(adventurer));
                        break;
                    }

                }


            }


            Player.Adventurers = Adventurers;

        }

        public string[] GetPlayers()
        {
            return DataBaseAccess.RetrieveAllPlayerNames();

        }
    }
}
