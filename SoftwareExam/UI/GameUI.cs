using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    public class GameUI {

        private readonly GameManager Manager;
        private readonly StartMenu StartMenu;
        private readonly PlayMenu PlayMenu;

        private char input;

        private string ExpeditionLog = "";

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
            Console.Clear();

            // This list will later be replaced with getting names from the DB
            string[] savedNames = Manager.GetPlayers();


            Console.WriteLine(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));

            while (true) {

                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    Manager.LoadGame(1);
                } else if (input == '2') {
                    Manager.LoadGame(2);
                } else if (input == '3') {
                    Manager.LoadGame(3);
                } else if (input == '4') {
                    Manager.LoadGame(4);
                } else if (input == '0') {
                    Console.Clear();
                    return false;
                } else {
                    InvalidInput(StartMenu.GetSaveMenu(savedNames[0], savedNames[1], savedNames[2], savedNames[3]));
                    continue;
                }
                break;
            }

            return true;
        }
        #endregion


        // Interraction in the game menu
        #region
        private void PlayGame() {
            ResetPlayMenu();

            while (true) {

                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    // Enter Guild House
                    GuildMenu();
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
                    Console.Clear();
                    Console.WriteLine(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetAvailableAdventurers(), Manager.GetBalanceString()));
                    //Console.WriteLine(Message);
                    Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
                } else if (input == '0') {
                    ExitMenu();
                    break;
                } else {
                    InvalidInput(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetAvailableAdventurers(), Manager.GetBalanceString()));
                    Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
                }
            }
        }


        private void GuildMenu() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetGuildHouseExpeditions());

            // Must print the available maps, then if clicked, continue to adventurer selection
        }

        private void TavernMenu() {
            Console.Clear();            

            Console.WriteLine(PlayMenu.GetTavern(Manager.GetAllAdventurerCards(), Manager.GetBalanceString()));

            while (true) {
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

                Console.Clear();
                Console.WriteLine(PlayMenu.GetTavern(Manager.GetAllAdventurerCards(), Manager.GetBalanceString()));
            }

        }

        private void DismissAdventurer(int who) {
            Console.Clear();

            Manager.GetAdventurerSellValue(who, out string name, out string value);

            Console.WriteLine(PlayMenu.GetTavernDismissing(name, value));

            while (true) {

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
            Console.Clear();

            Manager.CheckBalance(out bool canAfford, out string newBalance, out string cost);
            
            Console.WriteLine(PlayMenu.GetTavernRecruiting(canAfford, newBalance, cost));

            while (true) {
                
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
            Console.Clear();

            Console.WriteLine(PlayMenu.GetArmory(Manager.GetAllAdventurerCards(), Manager.GetBalanceString()));

            while (true) {
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
                    InvalidInput(PlayMenu.GetArmory(Manager.GetAllAdventurerCards(), Manager.GetBalanceString()));
                    continue;
                }

                Console.Clear();
                Console.WriteLine(PlayMenu.GetArmory(Manager.GetAllAdventurerCards(), Manager.GetBalanceString()));
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
                }

            }
        }
        private void ResetPlayMenu() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetAvailableAdventurers(), Manager.GetBalanceString()) + "\n");
            Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
        }
        #endregion

        private static void InvalidInput(string _display) {
            Console.Clear();
            Console.WriteLine(_display);
            Console.WriteLine("Invalid input");
        }
    }
}
