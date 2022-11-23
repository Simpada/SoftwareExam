﻿using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Expedition;
using SoftwareExam.DataBase;

namespace SoftwareExam.CoreProgram
{
    public class GameManager {

        private readonly Recruitment Recruitment;
        private readonly DataBaseAccess DataBaseAccess;
        private readonly Armory Armory;
        private readonly Expeditions Expeditions;
        private Player Player;
        private readonly int MaxAdventurers = 5;

        public GameManager() {
            Player = new Player();
            DataBaseAccess = new DataBaseAccess("Data Source = AdventureLeague.db");
            Recruitment = new Recruitment();
            Armory = new Armory();
            Expeditions = new Expeditions(Player);
        }


        public void SetPlayer(Player player) {
            Player = player;
        }

        public string GetLogMessage() {
            return Player.GetLogMessages();
        }

        public string GetBalanceString() {
            return Player.Balance.ToString();
        }

        public Currency GetBalanceValue() {
            return Player._balance;
        }

        public void CheckBalance(out bool canAfford, out string newBalance, out string cost) {

            canAfford = Recruitment.CheckBalance(Player._balance);
            cost = Recruitment.Price.ToString();
            newBalance = (Player._balance - Recruitment.Price).ToString();

        }

        // Relates to adventurers
        #region
        public bool RecruitAdventurer(int type) {
            // Replace with push to Player object

            Adventurer? adventurer = Recruitment.RecruitAdventurer(type, Player._balance);

            if (adventurer == null) {
                return false;
            } else {
                Player._balance -= Recruitment.Price;
                Player.Adventurers.Add(adventurer);
                return true;
            }
        }

        public void DismissAdventurer(int who) {
            Player.Adventurers.RemoveAt(who);
        }

        public string[] GetAllAdventurerCards() {

            // This sets the maximum amount of adventurers you can display
            string[] AdventurerCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }

        public string GetAvailableAdventurerCards() {

            string AvailableAdventurers = "";

            for (int i = 0; i < Player.Adventurers.Count; i++) {
                if (Player.Adventurers[i].OnMission) {
                    AvailableAdventurers += $"    |           ON A MISSION\n";
                } else {
                    AvailableAdventurers += $"    |       [{i + 1}] CHOOSE ADVENTURER\n";
                }
                AvailableAdventurers += Player.Adventurers[i].ToString();
                AvailableAdventurers += "\n    |-----------------------------------------\n";
            }
            return AvailableAdventurers;
        }

        public bool GetAvilability(int index) {

            if (Player.Adventurers.Count >= index + 1) {
                return !Player.Adventurers[index].OnMission;
            }
            return false;
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

        public int GetAdventurerCount() {
            return Player.Adventurers.Count;
        }
        #endregion


        public string GetExpeditionMaps() {
            return Expeditions.GetMaps();
        }

        public void PrepareExpedition(int mapNr, int adventurerNr) {
            Expeditions.PrepareMission(mapNr, Player.Adventurers[adventurerNr]);
        }

        public void SaveGame() {

            DataBaseAccess.Save(Player);

        }

        public int LoadGame(int Id) {

            Player = DataBaseAccess.GetPlayerById(Id);
            Expeditions.Player = Player;

            List<Adventurer> Adventurers = DataBaseAccess.GetAdventurers(Id);

            for (int i = 0; i < Adventurers.Count; i++) {
                List<int> itemCodes = DataBaseAccess.GetDecorators(Adventurers[i].Id);

                // Parse items and give to adventurers here

                foreach (int itemCode in itemCodes) {

                    switch (itemCode) {
                        case 100:
                        Adventurer.AddNewItem(new BasicArmor(Adventurers[i]));
                        break;
                        case 200:
                        Adventurer.AddNewItem(new BasicHat(Adventurers[i]));
                        break;
                        case 300:
                        Adventurer.AddNewItem(new BasicOffHand(Adventurers[i]));
                        break;
                        case 400:
                        Adventurer.AddNewItem(new BasicTrinket(Adventurers[i]));
                        break;
                        case 500:
                        Adventurer.AddNewItem(new BasicWeapon(Adventurers[i]));
                        break;
                    }
                }
                Adventurers[i] = Adventurer.EquipGear(Adventurers[i]);
            }

            Player.Adventurers = Adventurers;

            return Player.Id;
        }

        public string[] GetPlayers()
        {
            return DataBaseAccess.RetrieveAllPlayerNames();
        }

        public void DeleteSave(int saveFile) {
            DataBaseAccess.Delete(saveFile);
        }

        public void NewGame(int saveFile, string name) {
            Player.Id = saveFile;
            Player.PlayerName = name;
            Player.SetCurrency(0,5,1);
            Player.Adventurers = new();
            Random random = new();
            _ = RecruitAdventurer(random.Next(3) + 1);
        }

        public void Pause() {
            Expeditions.Pause();
        }

        public void Resume() {
            Expeditions.Resume();
        }
    }
}
