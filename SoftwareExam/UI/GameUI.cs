using SoftwareExam.CoreProgram;
using System.Text.RegularExpressions;

namespace SoftwareExam.UI {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Controls all interractions between the person playing the game and the programme, checking button inputs, and calling appropriate methods
    /// </summary>
    public class GameUI {

        private readonly GameManager _manager;
        private char _input;
        private string _ui = "";

        public GameUI() {
            _manager = new GameManager();
        }

        public void Run() {
            while (true) {
                MainMenu();
                PlayGame();
            }
        }

        #region Interractions in the main menu
        private void MainMenu() {
            _manager.Terminate();

            Console.Clear();
            Console.WriteLine(StartMenu.GetStartingMenu());

            bool StartGame = false;

            while (!StartGame) {
                _input = Console.ReadKey().KeyChar;
                if (_input == '1') {
                    StartGame = SelectSave();
                    if (!StartGame) {
                        Console.WriteLine(StartMenu.GetStartingMenu());
                    }
                } else if (_input == '2') {
                    HowToPlay();
                    Console.WriteLine(StartMenu.GetStartingMenu());
                } else if (_input == '0') {
                    Environment.Exit(0);
                } else {
                    InvalidInput(StartMenu.GetStartingMenu());
                    Console.Clear();
                    Console.WriteLine(_ui);
                }
            }
            _manager.Resume();
        }

        private void HowToPlay() {
            Console.Clear();
            Console.WriteLine(StartMenu.GetAboutMenu());

            while (true) {
                _input = Console.ReadKey().KeyChar;
                if (_input == '1') {
                    Console.Clear();
                    break;
                }
                InvalidInput(StartMenu.GetAboutMenu());
                Console.Clear();
                Console.WriteLine(_ui);
            }
        }

        private bool SelectSave() {

            string[] savedNames = GameManager.GetPlayers();
            Console.Clear();
            Console.WriteLine(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));

            while (true) {
                int SaveState;
                int SaveSlot;
                _input = Console.ReadKey().KeyChar;
                if (_input == '1') {
                    SaveState = _manager.LoadGame(1);
                    SaveSlot = 1;
                } else if (_input == '2') {
                    SaveState = _manager.LoadGame(2);
                    SaveSlot = 2;
                } else if (_input == '3') {
                    SaveState = _manager.LoadGame(3);
                    SaveSlot = 3;
                } else if (_input == '4') {
                    SaveState = _manager.LoadGame(4);
                    SaveSlot = 4;
                } else if (_input == '0') {
                    Console.Clear();
                    return false;
                } else {
                    InvalidInput(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));
                    Console.Clear();
                    Console.WriteLine(_ui);
                    continue;
                }

                if (SaveState >= 0) {
                    if (Continue(SaveSlot)) {
                        break;
                    }
                } else {
                    if (NewGame(SaveSlot)) {
                        break;
                    }
                }
                savedNames = GameManager.GetPlayers();
                Console.Clear();
                Console.WriteLine(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));
            }
            return true;
        }

        private bool Continue(int saveFile) {

            Console.Clear();
            Console.WriteLine(StartMenu.GetContinue());

            while (true) {

                _input = Console.ReadKey().KeyChar;
                if (_input == '1') {
                    return true;
                } else if (_input == '2') {
                    GameManager.DeleteSave(saveFile);
                    return NewGame(saveFile);
                } else if (_input == '3') {
                    GameManager.DeleteSave(saveFile);
                    return false;
                } else if (_input == '0') {
                    Console.Clear();
                    return false;
                } else {
                    InvalidInput(StartMenu.GetContinue());
                    Console.Clear();
                    Console.WriteLine(_ui);
                }
            }
        }

        private bool NewGame(int saveFile) {

            Console.Clear();

            Console.Write(StartMenu.GetNewGame());

            while (true) {
                string? Name;

                Name = Console.ReadLine();

                if (Name != null && Regex.IsMatch(Name, @"^[a-zA-Z]+[a-zA-Z ]$")) {
                    _manager.NewGame(saveFile, Name);
                    break;
                } else if (Name == "") {
                    return false;
                } else {
                    Console.Clear();
                    Console.WriteLine("\n    Invalid name!");
                    Console.Write(StartMenu.GetNewGame());
                }
            }
            return true;
        }

        #endregion


        #region Interraction in the game menu

        private void PlayGame() {
            _manager.Resume();
            ResetPlayMenu();

            while (true) {
                UpdateUi();

                _input = Console.ReadKey().KeyChar;
                if (_input == '1') {
                    // Enter Guild House
                    GuildMenuSelectMap();
                    ResetPlayMenu();
                } else if (_input == '2') {
                    // Enter Tavern
                    TavernMenu();
                    ResetPlayMenu();
                } else if (_input == '3') {
                    // Enter Armory
                    ArmoryMenu();
                    ResetPlayMenu();
                } else if (_input == '4') {
                    // Access DB and save
                    _manager.SaveGame();
                    _ui = PlayMenu.GetPlayMenu(_manager.GetAdventurerCount(), _manager.GetBalanceString()) +
                        "\n" +
                        PlayMenu.GetVillage();
                } else if (_input == '0') {
                    if (ExitMenu()) {
                        break;
                    }
                    ResetPlayMenu();
                } else {
                    InvalidInput(PlayMenu.GetPlayMenu(_manager.GetAdventurerCount(), _manager.GetBalanceString()));
                    _ui += "\n" + PlayMenu.GetVillage();
                }
            }
        }
        private void ResetPlayMenu() {
            _ui = PlayMenu.GetPlayMenu(_manager.GetAdventurerCount(), _manager.GetBalanceString()) +
                "\n\n" +
                PlayMenu.GetVillage();
        }
        private bool ExitMenu() {

            _manager.Pause();
            Console.Clear();
            Console.WriteLine(PlayMenu.GetExitMenu());

            while (true) {

                _input = Console.ReadKey().KeyChar;
                if (_input == '1') {
                    // Return directly to main menu
                    return true;
                } else if (_input == '2') {
                    // Save first, then return to main menu
                    _manager.SaveGame();
                    return true;
                } else if (_input == '3') {
                    // Exit without saving
                    Environment.Exit(0);
                } else if (_input == '4') {
                    // Save first, then exit
                    _manager.SaveGame();
                    Environment.Exit(0);
                } else if (_input == '0') {
                    // Back to Game
                    return false;
                } else {
                    InvalidInput(PlayMenu.GetExitMenu());
                    Console.Clear();
                    Console.WriteLine(_ui);
                }

            }
        }


        #region Guild Menu / Expedition UI
        private void GuildMenuSelectMap() {

            _ui = PlayMenu.GetGuildHouseExpeditions(_manager.GetExpeditionMaps(), _manager.GetBalanceString());

            int mapNr;
            int adventurerNr;

            while (true) {
                UpdateUi();

                _input = Console.ReadKey().KeyChar;

                if (_input == '1') {
                    mapNr = 0;
                } else if (_input == '2') {
                    mapNr = 1;
                } else if (_input == '3') {
                    mapNr = 2;
                } else if (_input == '4') {
                    mapNr = 3;
                } else if (_input == '0') {
                    return;
                } else {
                    InvalidInput(PlayMenu.GetGuildHouseExpeditions(_manager.GetExpeditionMaps(), _manager.GetBalanceString()));
                    continue;
                }


                if (_manager.CanAffordExpedition(mapNr)) {

                    adventurerNr = GuildMenuSelectAdventurer();
                    if (adventurerNr >= 0) {

                        _manager.PrepareExpedition(mapNr, adventurerNr);

                        break;
                    }
                } else {

                    _ui = PlayMenu.GetGuildHouseExpeditions(_manager.GetExpeditionMaps(), _manager.GetBalanceString()) +
                            "\n    You Cannot Afford This";
                    continue;

                }

                _ui = PlayMenu.GetGuildHouseExpeditions(_manager.GetExpeditionMaps(), _manager.GetBalanceString());
            }

            // Must print the available maps, then if clicked, continue to adventurer selection
        }

        private int GuildMenuSelectAdventurer() {

            _ui = PlayMenu.GetGuildHouseAdventurers(_manager.GetAvailableAdventurerCards());

            int adventurerNr;

            while (true) {
                UpdateUi();

                _input = Console.ReadKey().KeyChar;

                if (_input == '1') {
                    adventurerNr = 0;
                } else if (_input == '2') {
                    adventurerNr = 1;
                } else if (_input == '3') {
                    adventurerNr = 2;
                } else if (_input == '4') {
                    adventurerNr = 3;
                } else if (_input == '5') {
                    adventurerNr = 4;
                } else if (_input == '0') {
                    return -1;
                } else {
                    InvalidInput(PlayMenu.GetGuildHouseAdventurers(_manager.GetAvailableAdventurerCards()));
                    continue;
                }

                if (_manager.GetAvailability(adventurerNr)) {
                    break;
                }
                _ui = PlayMenu.GetGuildHouseAdventurers(_manager.GetAvailableAdventurerCards()) +
                    "\n    This Adventurer is unavailable";
            }
            return adventurerNr;
        }

        #endregion

        #region Tavern / Recruitment UI
        private void TavernMenu() {

            _ui = PlayMenu.GetTavern(_manager.GetAllAdventurerCards(), _manager.GetBalanceString());

            while (true) {
                UpdateUi();

                int AdventurerCount = _manager.GetAdventurerCount();
                _input = Console.ReadKey().KeyChar;

                if (_input == '1') {
                    if (AdventurerCount >= 1) {
                        DismissAdventurer(0);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (_input == '2') {
                    if (AdventurerCount >= 2) {
                        DismissAdventurer(1);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (_input == '3') {
                    if (AdventurerCount >= 3) {
                        DismissAdventurer(2);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (_input == '4') {
                    if (AdventurerCount >= 4) {
                        DismissAdventurer(3);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (_input == '5') {
                    if (AdventurerCount >= 5) {
                        DismissAdventurer(4);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (_input == '0') {
                    break;
                } else {
                    InvalidInput(PlayMenu.GetTavern(_manager.GetAllAdventurerCards(), _manager.GetBalanceString()));
                    continue;
                }

                _ui = PlayMenu.GetTavern(_manager.GetAllAdventurerCards(), _manager.GetBalanceString());
            }

        }

        private void DismissAdventurer(int who) {

            _manager.GetAdventurerSellValue(who, out string name, out string value);

            _ui = PlayMenu.GetTavernDismissing(name, value);

            while (true) {
                UpdateUi();

                _input = Console.ReadKey().KeyChar;

                if (_input == 'y') {
                    _manager.DismissAdventurer(who);
                    return;
                } else if (_input == 'n') {
                    return;
                } else {
                    InvalidInput(PlayMenu.GetTavernDismissing(name, value));
                }
            }
        }

        private void RecruitAdventurer() {

            _manager.CheckBalance(out bool canAfford, out string newBalance, out string cost);

            _ui = PlayMenu.GetTavernRecruiting(canAfford, newBalance, cost);

            while (true) {
                UpdateUi();

                _input = Console.ReadKey().KeyChar;

                if (_input == '1') {
                    _manager.RecruitAdventurer(1);
                } else if (_input == '2') {
                    _manager.RecruitAdventurer(2);
                } else if (_input == '3') {
                    _manager.RecruitAdventurer(3);
                } else if (_input != '0') {
                    _manager.CheckBalance(out canAfford, out newBalance, out cost);
                    InvalidInput(PlayMenu.GetTavernRecruiting(canAfford, newBalance, cost));
                    continue;
                }
                return;
            }
        }
        #endregion

        #region Armory / Purchase Items UI
        private void ArmoryMenu() {
            _ui = PlayMenu.GetArmory(_manager.GetAllItemCards(), _manager.GetBalanceString());
            bool invalidInput = false;

            while (true) {
                UpdateUi();

                int AdventurerCount = _manager.GetAdventurerCount();
                _input = Console.ReadKey().KeyChar;

                if (_input == '1') {
                    if (AdventurerCount >= 1 && _manager.GetAvailability(0)) {
                        ArmoryInventory(0);
                        invalidInput = false;
                    } 
                } else if (_input == '2') {
                    if (AdventurerCount >= 2 && _manager.GetAvailability(1)) {
                        ArmoryInventory(1);
                        invalidInput = false;
                    }
                } else if (_input == '3') {
                    if (AdventurerCount >= 3 && _manager.GetAvailability(2)) {
                        ArmoryInventory(2);
                        invalidInput = false;
                    }
                } else if (_input == '4') {
                    if (AdventurerCount >= 4 && _manager.GetAvailability(3)) {
                        ArmoryInventory(3);
                        invalidInput = false;
                    }
                } else if (_input == '5') {
                    if (AdventurerCount >= 5 && _manager.GetAvailability(4)) {
                        ArmoryInventory(4);
                        invalidInput = false;
                    }
                } else if (_input == '0') {
                    break;
                } else {
                    invalidInput = true;
                }
                if (invalidInput) {
                    InvalidInput(PlayMenu.GetArmory(_manager.GetAllItemCards(), _manager.GetBalanceString()));
                    continue;
                }
                
                _manager.ExitArmory();
                _ui = PlayMenu.GetArmory(_manager.GetAllItemCards(), _manager.GetBalanceString());
            }
        }

        private void ArmoryInventory(int id) {

            _manager.EnterArmory(id);
            _ui = PlayMenu.GetArmoryBrowsing(_manager.GetItemCards(id), _manager.GetBalanceString(), _manager.GetInventoryNames(), _manager.GetInventoryDescriptions(), _manager.GetInventoryPrices());


            while (true) {

                UpdateUi();

                _input = Console.ReadKey().KeyChar;

                string PurchaseResult;
                if (_input == '1') {
                    PurchaseResult = _manager.BuyItem(0, id);
                } else if (_input == '2') {
                    PurchaseResult = _manager.BuyItem(1, id);
                } else if (_input == '3') {
                    PurchaseResult = _manager.BuyItem(2, id);
                } else if (_input == '4') {
                    PurchaseResult = _manager.BuyItem(3, id);
                } else if (_input == '5') {
                    PurchaseResult = _manager.BuyItem(4, id);
                } else if (_input == '6') {
                    PurchaseResult = _manager.BuyItem(5, id);
                } else if (_input == '7') {
                    PurchaseResult = _manager.BuyItem(6, id);
                } else if (_input == '8') {
                    PurchaseResult = _manager.BuyItem(7, id);
                } else if (_input == '0') {
                    break;
                } else {
                    _manager.EnterArmory(id);
                    InvalidInput(PlayMenu.GetArmoryBrowsing(_manager.GetItemCards(id), _manager.GetBalanceString(), _manager.GetInventoryNames(), _manager.GetInventoryDescriptions(), _manager.GetInventoryPrices()));
                    continue;
                }
                if (PurchaseResult == "") {
                    PurchaseResult = "Invalid Input";
                }
                _manager.EnterArmory(id);
                _ui = PlayMenu.GetArmoryBrowsing(_manager.GetItemCards(id), _manager.GetBalanceString(), _manager.GetInventoryNames(), _manager.GetInventoryDescriptions(), _manager.GetInventoryPrices()) +
                    $"\n    {PurchaseResult}";
            }

        }
        #endregion


        #endregion

        /// <summary>
        /// A function that can easily be called by interractions in the game menu, to refresh the window.
        /// This helps udate everything inside, without filling the console with repeated information
        /// </summary>
        private void UpdateUi() {
            _manager.Pause();
            Console.Clear();
            Console.WriteLine(_ui);
            Console.WriteLine(PlayMenu.GetLog(_manager.GetLogMessage()));
            _manager.Resume();
        }

        private void InvalidInput(string _display) {
            _ui = _display +
                "\nInvalid input";
        }
    }
}
