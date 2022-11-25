using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;
using SoftwareExam.CoreProgram.Expedition;

namespace SoftwareExam.CoreProgram {

    /// <summary>
    /// A facade that works as the middlepoint between all core game mechanics and classes
    /// </summary>
    public class GameManager {

        private readonly Recruitment _recruitment;
        private readonly Armory _armory;
        private readonly Expeditions _expeditions;
        private Player _player;
        private readonly int _maxAdventurers = 5;

        /// <summary>
        /// Constructor for GameManager that sets up necessary classes
        /// </summary>
        public GameManager() {
            _player = new ();
            _recruitment = new ();
            _armory = new ();
            _expeditions = new (_player);
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
        /// <summary>
        /// Tells the save manager to save the game, using the player object
        /// </summary>
        public void SaveGame() {
            SaveManager.SaveGame(_player);
        }
        /// <summary>
        /// Tells the save manager to delete the save with a specific ID
        /// </summary>
        /// <param name="saveFile">What id to delete a save at</param>
        public static void DeleteSave(int saveFile) {
            SaveManager.DeleteSave(saveFile);
        }
        /// <summary>
        /// Tells the save manager to load a save at an id
        /// </summary>
        /// <param name="Id">the id of the save game to load</param>
        /// <returns>The id of the player, if negative, no player was found</returns>
        public int LoadGame(int Id) {

            _player = SaveManager.LoadGame(_expeditions.Log, Id);
            _expeditions.Player = _player;

            return _player.Id;
        }

        /// <summary>
        /// Gets a list of the names of all players
        /// </summary>
        /// <returns>A list of player names</returns>
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

        public string GetLogMessage() {
            return _player.GetLogMessages();
        }

        public string GetBalanceString() {
            return _player.Balance.ToString();
        }

        // Calls recruitment to check balance
        public void CheckBalance(out bool canAfford, out string newBalance, out string cost) {

            canAfford = _recruitment.CheckBalance(_player.Balance);
            cost = _recruitment.Price.ToString();
            newBalance = (_player.Balance - _recruitment.Price).ToString();
        }
        #endregion

        #region Adventurers

        // Gets the amount of adventurer's the player has
        public int GetAdventurerCount() {
            return _player.Adventurers.Count;
        }

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

        public void DismissAdventurer(int who) {
            _player.SellAdventurer(who);

        }

        // Checks if the adventurer exists, and if it does, if it is on a mission
        public bool GetAvailability(int index) {

            if (_player.Adventurers.Count >= index + 1) {
                return !_player.Adventurers[index].OnMission;
            }
            return false;
        }

        #region Adventurer Strings for UI

        // Get's the adventurer card of all adventurers, this should probably be done in the player or elsewhere
        public string[] GetAllAdventurerCards() {

            // This sets the maximum amount of adventurers you can display
            string[] AdventurerCards = new string[_maxAdventurers];

            List<Adventurer> Adventurers = _player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }

        // Get's the adventurer cards plus their item descriptions, might also be done elsewhere
        public string[] GetAllItemCards() {

            string[] ItemCards = new string[_maxAdventurers];

            List<Adventurer> Adventurers = _player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                ItemCards[i] = Adventurers[i].GetItemCard();
            }

            return ItemCards;
        }

        // Creates a string of available adventurers, and their status for UI, not the best to have it in GameManager
        public string GetAvailableAdventurerCards() {

            string AvailableAdventurers = "";

            for (int i = 0; i < _player.Adventurers.Count; i++) {
                AvailableAdventurers += _player.Adventurers[i].GetAvailability(i + 1);
            }
            return AvailableAdventurers;
        }

        // Checks an adventurer's worth, currently doesn't work!!!
        public void GetAdventurerSellValue(int who, out string name, out string value) {

            double sellMultiplier = 0.7;

            Adventurer adventurer = _player.Adventurers[who];

            name = adventurer.Name;
            value = (adventurer.Value * sellMultiplier).ToString();
        }

        #endregion

        #endregion

        #region Expeditions

        // Gets all maps from expeditions
        public string GetExpeditionMaps() {
            return _expeditions.GetMaps();
        }

        // Checks if the player can afford to go on a specific expedition
        public bool CanAffordExpedition(int mapNr) {

            return _expeditions.PurchaseMap(mapNr, _player.Balance);
        }

        // Gives expeditions a map and an adventurer, so it can run the mission
        public void PrepareExpedition(int mapNr, int adventurerNr) {

            _expeditions.PrepareMission(mapNr, _player.Adventurers[adventurerNr], out Currency cost);
            _player.AlterCurrency(cost, false);
        }
        #endregion

        #region Armory
        public List<string> GetInventoryNames() {
            return _armory.GetItemNames();
        }

        public List<string> GetInventoryDescriptions() {
            return _armory.GetItemDescriptions();
        }

        public List<string> GetInventoryPrices() {
            return _armory.GetItemPrices();
        }

        public string GetItemCards(int id) {
            return _player.Adventurers[id].GetItemCard();
        }

        // Attempts to buy an item, checking prices, and buying it if it can be afforded and exists
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

        // These two functions pauses the Armory refresh function while browsing
        public void EnterArmory(int adventurerId) {
            _armory.EnterArmory(_player.Adventurers[adventurerId].Class);
        }
        public void ExitArmory() {
            _armory.Resume();
        }

        #endregion

    }
}
