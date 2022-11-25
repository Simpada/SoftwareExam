using SoftwareExam.CoreProgram.Adventurers;
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



        // Core game functionalities

        // These we might want to move to a separate class
        public void NewGame(int saveFile, string name) {
            Player.Id = saveFile;
            Player.PlayerName = name;
            Player.SetCurrency(0, 0, 7);
            Player.Adventurers = new();
            Random random = new();
            _ = RecruitAdventurer(random.Next(3) + 1);
        }
        public void SaveGame() {
            DataBaseAccess.Save(Player);
        }
        public void DeleteSave(int saveFile) {
            DataBaseAccess.Delete(saveFile);
        }

        public int LoadGame(int Id) {

            //Maybe split the content here into a class called get Adventurers, and this calls that, then GetMissions
            Player = DataBaseAccess.GetPlayerById(Id);
            Expeditions.Player = Player;

            List<Adventurer> Adventurers = DataBaseAccess.GetAdventurers(Id);

            for (int i = 0; i < Adventurers.Count; i++) {
                List<int> itemCodes = DataBaseAccess.GetDecorators(Adventurers[i].Id);

                foreach (int itemCode in itemCodes) {
                    Adventurer.AddNewItem(ItemParser.GetItem(itemCode, Adventurers[i]));
                }
                Adventurers[i] = Adventurer.EquipGear(Adventurers[i]);
            }

            Player.Adventurers = Adventurers;

            GetMissions(Id);

            return Player.Id;
        }
        // This one is a part of LoadGame above
        public void GetMissions(int id) {
            List<Mission> missions = DataBaseAccess.GetMissionsForAdventurers(id);

            foreach (var Mission in missions) {
                Mission.Player = Player;

                foreach (var Adventurer in Player.Adventurers) {
                    if (Adventurer.Id == Mission.AdventurerId) {
                        Mission.Adventurer = Adventurer;
                        break;
                    }
                }
                if (Mission.Adventurer == null) {
                    throw new Exception("Adventurer cannot be found. Saving/loading process error");
                }

                Mission.LogWriter = Expeditions.Log;

                Task.Run(() => Mission.Start());
            }
        }



        // Only for getting names
        public string[] GetPlayers() {
            return DataBaseAccess.RetrieveAllPlayerNames();
        }



        // Core functions, but not related to save/load/new game or DB stuff
        public void Pause() {
            Expeditions.Pause();
            Player.Pause(true);
        }

        public void Resume() {
            Expeditions.Resume();
            Player.Pause(false);
        }

        public void Terminate() {
            foreach (var mission in Player.Missions) {
                mission.Terminate();
            }

            Player.TerminateMissions();
        }




        // Calls to The player object for information

        public string GetLogMessage() {
            return Player.GetLogMessages();
        }

        public string GetBalanceString() {
            return Player.Balance.ToString();
        }



        // Calls recruitment to check balance
        public void CheckBalance(out bool canAfford, out string newBalance, out string cost) {

            canAfford = Recruitment.CheckBalance(Player.Balance);
            cost = Recruitment.Price.ToString();
            newBalance = (Player.Balance - Recruitment.Price).ToString();

        }

        // Relates to adventurers
        #region
        public bool RecruitAdventurer(int type) {

            Adventurer? adventurer = Recruitment.RecruitAdventurer(type, Player.Balance);

            if (adventurer == null) {
                return false;
            } else {
                Player.AlterCurrency(Recruitment.Price, false);
                Player.Adventurers.Add(adventurer);
                return true;
            }
        }

        public void DismissAdventurer(int who) {
            Player.Adventurers.RemoveAt(who);
        }







        // Get's the adventurer card of all adventurers, this should probably be done in the player or elsewhere
        public string[] GetAllAdventurerCards() {

            // This sets the maximum amount of adventurers you can display
            string[] AdventurerCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }

        // Get's the adventurer cards plus their item descriptions, might also be done elsewhere
        public string[] GetAllItemCards() {

            string[] ItemCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                ItemCards[i] = Adventurers[i].GetItemCard();
            }

            return ItemCards;
        }

        // Creates a string of available adventurers, and their status for UI, not the best to have it in GameManager
        public string GetAvailableAdventurerCards() {

            string AvailableAdventurers = "";

            for (int i = 0; i < Player.Adventurers.Count; i++) {
                AvailableAdventurers += Player.Adventurers[i].GetAvailability(i+1);
            }
            return AvailableAdventurers;
        }



        // Checks if the adventurer exists, and if it does, if it is on a mission
        public bool GetAvailability(int index) {

            if (Player.Adventurers.Count >= index + 1) {
                return !Player.Adventurers[index].OnMission;
            }
            return false;
        }


        // Checks an adventurer's worth, currently doesn't work!!!
        public void GetAdventurerSellValue(int who, out string name, out string value) {

            double sellMultiplier = 0.7;

            Adventurer adventurer = Player.Adventurers[who];

            name = adventurer.Name;
            value = (adventurer.Value * sellMultiplier).ToString();
        }


        // Gets the amount of adventurer's the player has
        public int GetAdventurerCount() {
            return Player.Adventurers.Count;
        }
        #endregion


        #region Expeditions

        // Gets all maps from expeditions
        public string GetExpeditionMaps() {
            return Expeditions.GetMaps();
        }

        // Checks if the player can afford to go on a specific expedition
        public bool CanAffordExpedition(int mapNr) {

            return Expeditions.PurchaseMap(mapNr, Player.Balance);
        }

        // Gives expeditions a map and an adventurer, so it can run the mission
        public void PrepareExpedition(int mapNr, int adventurerNr) {

            Expeditions.PrepareMission(mapNr, Player.Adventurers[adventurerNr], out Currency cost);
            Player.AlterCurrency(cost, false);
        }
        #endregion


        #region Armory
        public List<string> GetInventoryNames() {
            return Armory.GetItemNames();
        }
        
        public List<string> GetInventoryDescriptions() {
            return Armory.GetItemDescriptions();
        }

        public List<string> GetInventoryPrices() {
            return Armory.GetItemPrices();
        }

        public string GetItemCards(int id) {
            return Player.Adventurers[id].GetItemCard();
        }

        // Attempts to buy an item, checking prices, and buying it if it can be afforded and exists
        public string BuyItem(int itemId, int adventurerId) {
            bool CanAfford = Armory.CanAffordItem(itemId, Player.Balance, out bool noItem, out Currency price);

            if (noItem) {
                return "";
            }

            if (CanAfford) {

                Adventurer adventurer = Player.Adventurers[adventurerId];

                Player.AlterCurrency(price, false);
                Player.Adventurers[adventurerId] = Adventurer.AddNewItem( Armory.BuyItem(itemId, adventurer) );
                return "Purchase Successful";

            } else {
                return "You cannot afford this!";
            }
        }

        // These two functions pauses the Armory refresh function while browsing
        public void EnterArmory(int adventurerId) {
            Armory.EnterArmory(Player.Adventurers[adventurerId].Class);
        }
        public void ExitArmory() {
            Armory.Resume();
        }

        #endregion

    }
}
