using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;
using SoftwareExam.CoreProgram.Expedition;

namespace SoftwareExam.CoreProgram {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// A facade that works as the middlepoint between all core game mechanics and classes
    /// </summary>
    public class GameManager {

        private readonly Recruitment _recruitment;
        private readonly Armory _armory;
        private readonly Expeditions _expeditions;
        private Player _player;
        private readonly int _maxAdventurers = 5;

        public GameManager() {
            _player = new();
            _recruitment = new();
            _armory = new();
            _expeditions = new(_player);
        }

        #region Core Functions

        /// <summary>
        /// Sets fields in the player to a standard for a new game
        /// </summary>
        /// <param name="saveFile">which </param>
        /// <param name="name"></param>
        public void NewGame(int saveFile, string name) {
            _player.Id = saveFile;
            _player.PlayerName = name;
            _player.SetCurrency(0, 0, 7);
            _player.Adventurers = new();
            Random random = new();
            _ = RecruitAdventurer(random.Next(3) + 1);
            SaveGame();
        }

        public void SaveGame() {
            SaveManager.SaveGame(_player);
        }

        public static void DeleteSave(int saveFile) {
            SaveManager.DeleteSave(saveFile);
        }

        public int LoadGame(int Id) {

            _player = SaveManager.LoadGame(_expeditions.Log, Id);
            _expeditions.Player = _player;

            return _player.Id;
        }

        public static string[] GetPlayers() {
            return SaveManager.RetrieveAllPlayerNames();
        }

        /// <summary>
        /// Pauses the logwriter and misisons
        /// </summary>
        public void Pause() {
            _expeditions.Pause();
            _player.Pause(true);
        }
        /// <summary>
        /// Resumes operation of the logwriter and missions
        /// </summary>
        public void Resume() {
            _expeditions.Resume();
            _player.Pause(false);
        }
        /// <summary>
        /// Terminates all missions, only used when returning to main menu
        /// </summary>
        public void Terminate() {
            foreach (var mission in _player.Missions) {
                mission.Terminate();
            }
            _player.TerminateMissions();
        }

        #endregion

        #region Player Information

        /// <summary>
        /// To get the log from the player
        /// </summary>
        /// <returns>A string containing the log of the player</returns>
        public string GetLogMessage() {
            return _player.GetLogMessages();
        }

        /// <summary>
        /// Gets how much money they player has for the UI
        /// </summary>
        /// <returns>A string displaying the balance of the player</returns>
        public string GetBalanceString() {
            return _player.Balance.ToString();
        }
        #endregion

        #region Adventurers

        // Gets the amount of adventurer's the player has
        public int GetAdventurerCount() {
            return _player.Adventurers.Count;
        }

        /// <summary>
        /// Checks the balance of the player, to chek if they can recruit new adventurers
        /// </summary>
        /// <param name="canAfford">A bool that tells if the player has enough money</param>
        /// <param name="newBalance">A string that informs how much money the player would have after the transaction</param>
        /// <param name="cost">The price of the purchase</param>
        public void CheckBalance(out bool canAfford, out string newBalance, out string cost) {

            canAfford = _recruitment.CheckBalance(_player.Balance);
            cost = _recruitment.Price.ToString();
            newBalance = (_player.Balance - _recruitment.Price).ToString();
        }

        /// <summary>
        /// Recruits a new adventurer of the player can afford it
        /// </summary>
        /// <param name="type">What type of adventurer to recruit</param>
        /// <returns>A bool telling if the purchase was successful</returns>
        public bool RecruitAdventurer(int type) {

            Adventurer? adventurer = _recruitment.RecruitAdventurer(type, _player.Balance);

            if (adventurer == null) {
                return false;
            } else {
                _player.AlterCurrency(_recruitment.Price, false);
                _player.Adventurers.Add(adventurer);
                return true;
            }
        }

        /// <summary>
        /// Sells an adventurer
        /// </summary>
        /// <param name="who">The index of the adventurer to sell</param>
        public void DismissAdventurer(int who) {
            _player.SellAdventurer(who);

        }

        /// <summary>
        /// Checks if the adventurer is on a misson or not
        /// </summary>
        /// <param name="index">The index of the adventurer to check</param>
        /// <returns>A bool saying if the adventurer is on a mission</returns>
        public bool GetAvailability(int index) {

            if (_player.Adventurers.Count >= index + 1) {
                return !_player.Adventurers[index].OnMission;
            }
            return false;
        }

        #region Adventurer Strings for UI

        /// <summary>
        /// Gets the "card" for all adventurers for the UI
        /// </summary>
        /// <returns>An array of card for each adventurer</returns>
        public string[] GetAllAdventurerCards() {

            // This sets the maximum amount of adventurers you can display
            string[] AdventurerCards = new string[_maxAdventurers];

            List<Adventurer> Adventurers = _player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }

        /// <summary>
        /// Gets the "card" for adventurers, including their items
        /// </summary>
        /// <returns>An array of item cards for each adventurer</returns>
        public string[] GetAllItemCards() {

            string[] ItemCards = new string[_maxAdventurers];

            List<Adventurer> Adventurers = _player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                ItemCards[i] = Adventurers[i].GetItemCard();
            }

            return ItemCards;
        }

        /// <summary>
        /// Gives the item card of a specific adventurer
        /// </summary>
        /// <param name="id">The index of the adventurer to get the card from</param>
        /// <returns>A string containing the adventurer's card</returns>
        public string GetItemCards(int id) {
            return _player.Adventurers[id].GetItemCard();
        }

        /// <summary>
        /// Gets the status of each adventurer, saying if they are on missions or not
        /// </summary>
        /// <returns>A string detailing the status of all the adventurers</returns>
        public string GetAvailableAdventurerCards() {

            string AvailableAdventurers = "";

            for (int i = 0; i < _player.Adventurers.Count; i++) {
                AvailableAdventurers += _player.Adventurers[i].GetAvailability(i + 1);
            }
            return AvailableAdventurers;
        }

        /// <summary>
        /// Gets the sell value of an adventurer
        /// </summary>
        /// <param name="who">The index of the adventurer to check</param>
        /// <param name="name">The name of adventurer</param>
        /// <param name="value">The sell value of the adventuer</param>
        public void GetAdventurerSellValue(int who, out string name, out string value) {

            double sellMultiplier = 0.7;

            Adventurer adventurer = _player.Adventurers[who];

            name = adventurer.Name;
            value = (adventurer.Value * sellMultiplier).ToString();
        }

        #endregion

        #endregion

        #region Expeditions

        /// <summary>
        /// Gets all the maps in the expedition class
        /// </summary>
        /// <returns>A string containing the description of all the maps</returns>
        public string GetExpeditionMaps() {
            return _expeditions.GetMaps();
        }

        /// <summary>
        /// Checks if a player can afford to go to a specific map
        /// </summary>
        /// <param name="mapNr">The index of the map to check</param>
        /// <returns>A bool saying if the player can afford the map</returns>
        public bool CanAffordExpedition(int mapNr) {

            return _expeditions.PurchaseMap(mapNr, _player.Balance);
        }

        /// <summary>
        /// Send an adventurer on a mission to a map
        /// </summary>
        /// <param name="mapNr">The index if the map to check</param>
        /// <param name="adventurerNr">Which adventurer to send on the mission</param>
        public void PrepareExpedition(int mapNr, int adventurerNr) {

            _expeditions.PrepareMission(mapNr, _player.Adventurers[adventurerNr], out Currency cost);
            _player.AlterCurrency(cost, false);
        }
        #endregion

        #region Armory

        /// <summary>
        /// Gets all the names of items available for sale in the armory
        /// </summary>
        /// <returns>A list of strings, giving the names of the items</returns>
        public List<string> GetInventoryNames() {
            return _armory.GetItemNames();
        }

        /// <summary>
        /// Gets all the descriptions of items available for sale in the armory
        /// </summary>
        /// <returns>A list of strings, giving the description of the items</returns>
        public List<string> GetInventoryDescriptions() {
            return _armory.GetItemDescriptions();
        }

        /// <summary>
        /// Gets all the prices of items available for sale in the armory
        /// </summary>
        /// <returns>A list of strings, giving the prices of the items</returns>
        public List<string> GetInventoryPrices() {
            return _armory.GetItemPrices();
        }

        /// <summary>
        /// Attempt to buy an item, checking prices, and buying it if the player can afford
        /// </summary>
        /// <param name="itemId">The index of the item to try and buy</param>
        /// <param name="adventurerId">The adventurer to buy the item for</param>
        /// <returns>A string informing of the level of success</returns>
        public string BuyItem(int itemId, int adventurerId) {
            bool CanAfford = _armory.CanAffordItem(itemId, _player.Balance, out bool noItem, out Currency price);

            if (noItem) {
                return "";
            }

            if (CanAfford) {

                Adventurer adventurer = _player.Adventurers[adventurerId];

                _player.AlterCurrency(price, false);
                _player.Adventurers[adventurerId] = Adventurer.AddNewItem(_armory.BuyItem(itemId, adventurer));
                return "Purchase Successful";

            } else {
                return "You cannot afford this!";
            }
        }

        /// <summary>
        /// Starts browsing the inventory of the armory
        /// </summary>
        /// <param name="adventurerId">Which adventurer is browsing the inventory</param>
        public void EnterArmory(int adventurerId) {
            _armory.EnterArmory(_player.Adventurers[adventurerId].Class);
        }
        /// <summary>
        /// Leaves the armory, restarting its normal refresh cycle
        /// </summary>
        public void ExitArmory() {
            _armory.Resume();
        }

        #endregion

    }
}
