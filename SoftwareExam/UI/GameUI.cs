using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    public class GameUI {

        private readonly GameManager Manager;
        private readonly StartMenu StartMenu;
        private readonly PlayMenu PlayMenu;

        private char input;
        private string Ui = "";

        public GameUI() {
            Manager = new GameManager();
            StartMenu = new StartMenu();
            PlayMenu = new PlayMenu(); 
        }

        public void Run() {
            while(true) {
                MainMenu();
                PlayGame();
            }
        }

        // Interractions in the main menu
        #region
        private void MainMenu() {
            Console.Clear();
            Console.WriteLine(StartMenu.GetStartingMenu());

            bool StartGame = false;

            while (!StartGame) {
                
                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    StartGame = LoadSave();
                    if (!StartGame) {
                        Console.WriteLine(StartMenu.GetStartingMenu());
                    }
                } else if (input == '2') {
                    HowToPlay();
                    Console.WriteLine(StartMenu.GetStartingMenu());
                } else if (input == '0') {
                    Environment.Exit(0);
                } else {
                    InvalidInput(StartMenu.GetStartingMenu());
                }
            }
        }

        private void HowToPlay() {
            Console.Clear();
            Console.WriteLine(StartMenu.GetAboutMenu());

            while (true) {
                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    Console.Clear();
                    break;
                }
                InvalidInput(StartMenu.GetAboutMenu());
            }
        }

        private bool LoadSave() {

            string[] savedNames = Manager.GetPlayers();
            Console.Clear();
            Console.WriteLine(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));

            while (true) {

                int SaveState;
                int SaveSlot;
                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    SaveState = Manager.LoadGame(1);
                    SaveSlot = 1;
                } else if (input == '2') {
                    SaveState = Manager.LoadGame(2);
                    SaveSlot = 2;
                } else if (input == '3') {
                    SaveState = Manager.LoadGame(3);
                    SaveSlot = 3;
                } else if (input == '4') {
                    SaveState = Manager.LoadGame(4);
                    SaveSlot = 4;
                } else if (input == '0') {
                    Console.Clear();
                    return false;
                } else {
                    InvalidInput(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));
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
                savedNames = Manager.GetPlayers();
                Console.Clear();
                Console.WriteLine(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));
            }

            return true;
        }

        private bool Continue(int SaveFile) {


            Console.Clear();


            Console.WriteLine(StartMenu.GetContinue());

            while (true) {

                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    return true;
                } else if (input == '2') {
                    Manager.DeleteSave(SaveFile);
                    return NewGame(SaveFile);
                } else if (input == '3') {
                    Manager.DeleteSave(SaveFile);
                    return false;
                } else if (input == '0') {
                    Console.Clear();
                    return false;
                } else {
                    InvalidInput(StartMenu.GetContinue());
                }
            }
        }

        private bool NewGame(int SaveFile) {

            Console.Clear();

            Console.Write(StartMenu.GetNewGame());

            while (true) {
                string? Name;

                Name = Console.ReadLine();


                if (Name != null && Regex.IsMatch(Name, @"^[a-zA-Z]+[a-zA-Z ]$")) {
                    Manager.NewGame(SaveFile, Name);
                    Manager.SaveGame();
                    break;
                } else if (Name == "") {
                    return false;
                } else {
                    Console.WriteLine(StartMenu.GetNewGame());
                    Console.WriteLine("\n    Invalid name!");
                }
            }
            return true;
        }

        #endregion


        // Interraction in the game menu
        
        public void UpdateUi() {

            Console.Clear();
            Console.WriteLine(Ui);
            Console.WriteLine(PlayMenu.GetLog(Manager.GetLogMessage()));

        }

        #region
        private void PlayGame() {
            ResetPlayMenu();


            while (true) {
                UpdateUi();

                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    // Enter Guild House
                    GuildMenuSelectMap();
                    ResetPlayMenu();
                } else if (input == '2') {
                    // Enter Tavern
                    TavernMenu();
                    ResetPlayMenu();
                } else if (input == '3') {
                    // Enter Armory
                    ArmoryMenu();
                    ResetPlayMenu();
                } else if (input == '4') {
                    // Access DB and save
                    Manager.SaveGame();
                    Ui = PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetBalanceString()) +
                        "\n" +
                        PlayMenu.GetVillage();
                } else if (input == '0') {
                    ExitMenu();
                    break;
                } else {
                    InvalidInput(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetBalanceString()));
                    Ui += "\n" + PlayMenu.GetVillage();
                }
            }
        }

        private void GuildMenuSelectMap() {
            Console.Clear();

            Ui = PlayMenu.GetGuildHouseExpeditions(Manager.GetExpeditionMaps(), Manager.GetBalanceString());

            int mapNr;
            int adventurerNr;

            while (true) {
                UpdateUi();

                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    mapNr = 0;
                } else if (input == '2') {
                    mapNr = 1;
                } else if (input == '3') {
                    mapNr = 2;
                } else if (input == '4') {
                    mapNr = 3;
                } else if (input == '0') {
                    return;
                } else {
                    InvalidInput(PlayMenu.GetGuildHouseExpeditions(Manager.GetExpeditionMaps(), Manager.GetBalanceString()));
                    continue;
                }

                adventurerNr = GuildMenuSelectAdventurer();
                if (adventurerNr >= 0) {
                    break;
                }
                Ui = PlayMenu.GetGuildHouseExpeditions(Manager.GetExpeditionMaps(), Manager.GetBalanceString());
            }

            Manager.PrepareExpedition(mapNr,adventurerNr);

            // Must print the available maps, then if clicked, continue to adventurer selection
        }

        private int GuildMenuSelectAdventurer() {

            Ui = PlayMenu.GetGuildHouseAdventurers(Manager.GetAvailableAdventurerCards());

            int adventurerNr;

            while (true) {
                UpdateUi();

                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    adventurerNr = 0;
                } else if (input == '2') {
                    adventurerNr = 1;
                } else if (input == '3') {
                    adventurerNr = 2;
                } else if (input == '4') {
                    adventurerNr = 3;
                } else if (input == '5') {
                    adventurerNr = 4;
                } else if (input == '0') {
                    return -1;
                } else {
                    InvalidInput(PlayMenu.GetGuildHouseAdventurers(Manager.GetAvailableAdventurerCards()));
                    continue;
                }

                if (Manager.GetAvilability(adventurerNr)) {
                    break;
                }
                Ui = PlayMenu.GetGuildHouseAdventurers(Manager.GetAvailableAdventurerCards()) +
                    "\nThis Adventurer is unavailable";
            }
            return adventurerNr;
        }

        private void TavernMenu() {

            Ui = PlayMenu.GetTavern(Manager.GetAllAdventurerCards(), Manager.GetBalanceString());

            

            while (true) {
                UpdateUi();

                int AdventurerCount = Manager.GetAdventurerCount();
                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    if (AdventurerCount >= 1) {
                        DismissAdventurer(0);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '2') {
                    if (AdventurerCount >= 2) {
                        DismissAdventurer(1);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '3') {
                    if (AdventurerCount >= 3) {
                        DismissAdventurer(2);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '4') {
                    if (AdventurerCount >= 4) {
                        DismissAdventurer(3);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '5') {
                    if (AdventurerCount >= 5) {
                        DismissAdventurer(4);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '0') {
                    break;
                } else {
                    InvalidInput(PlayMenu.GetTavern(Manager.GetAllAdventurerCards(), Manager.GetBalanceString()));
                    continue;
                }

                Ui = PlayMenu.GetTavern(Manager.GetAllAdventurerCards(), Manager.GetBalanceString());
            }

        }

        private void DismissAdventurer(int who) {

            Manager.GetAdventurerSellValue(who, out string name, out string value);

            Ui = PlayMenu.GetTavernDismissing(name, value);

            while (true) {
                UpdateUi();

                input = Console.ReadKey().KeyChar;

                if (input == 'y') {
                    Manager.DismissAdventurer(who);
                    return;
                } else if (input == 'n') {
                    return;
                } else {
                    InvalidInput(PlayMenu.GetTavernDismissing(name, value));
                }
            }
        }

        private void RecruitAdventurer() {

            Manager.CheckBalance(out bool canAfford, out string newBalance, out string cost);
            
            Ui = PlayMenu.GetTavernRecruiting(canAfford, newBalance, cost);

            while (true) {
                UpdateUi();
                
                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    Manager.RecruitAdventurer(1);
                } else if (input == '2') {
                    Manager.RecruitAdventurer(2);
                } else if (input == '3') {
                    Manager.RecruitAdventurer(3);
                } else if (input != '0') {
                    Manager.CheckBalance(out canAfford, out newBalance, out cost);
                    InvalidInput(PlayMenu.GetTavernRecruiting(canAfford, newBalance, cost));
                    continue;
                }
                return;
            }
}

        private void ArmoryMenu() {
            Ui = PlayMenu.GetArmory(Manager.GetAllItemCards());

            while (true) {

                UpdateUi();

                int AdventurerCount = Manager.GetAdventurerCount();
                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    if (AdventurerCount >= 1) {
                        
                    } else {

                    }
                } else if (input == '2') {
                    if (AdventurerCount >= 2) {
                    
                    } else {
                    
                    }
                } else if (input == '3') {
                    if (AdventurerCount >= 3) {
                    
                    } else {
                    
                    }
                } else if (input == '4') {
                    if (AdventurerCount >= 4) {
                    
                    } else {
                    
                    }
                } else if (input == '5') {
                    if (AdventurerCount >= 5) {
                    
                    } else {
                    
                    }
                } else if (input == '0') {
                    break;
                } else {
                    InvalidInput(PlayMenu.GetArmory(Manager.GetAllItemCards()));
                    continue;
                }

                Ui = PlayMenu.GetArmory(Manager.GetAllItemCards());
            }
        }

        private void ExitMenu() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetExitMenu());

            while (true) {

                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    // Return directly to main menu
                    return;
                } else if (input == '2') {
                    // Save first, then return to main menu
                    Manager.SaveGame();
                    return;
                } else if (input == '3') {
                    // Exit without saving
                    Environment.Exit(0);
                } else if (input == '4') {
                    // Save first, then exit
                    Manager.SaveGame();
                    Environment.Exit(0);
                } else {
                    InvalidInput(PlayMenu.GetExitMenu());
                    Console.Clear();
                    Console.WriteLine(Ui);
                }

            }
        }
        private void ResetPlayMenu() {
            Ui = PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetBalanceString())+ 
                "\n\n" +
                PlayMenu.GetVillage();
        }
        #endregion

        private void InvalidInput(string _display) {
            Ui = _display +
                "\n" +
                "Invalid input";
        }
    }
}
