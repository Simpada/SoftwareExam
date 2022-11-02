using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    internal class GameUI {

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
            List<string> dbNames = new();
            dbNames.Add("Frank");
            dbNames.Add("");
            dbNames.Add("Henriette");
            dbNames.Add("");


            List<string> saveNames = new();

            // This will later get its input from DB
            foreach(string name in dbNames) {
                if (String.IsNullOrEmpty(name)) {
                    saveNames.Add("Empty");
                } else {
                    saveNames.Add(name);
                }
            }


            Console.WriteLine(StartMenu.GetSaveMenu(saveNames[0], saveNames[1], saveNames[2], saveNames[3]));

            while (true) {

                input = Console.ReadKey().KeyChar;
                if (input == '1') {
                    // Will set the save state to 1 and start game
                } else if (input == '2') {
                    // Will set the save state to 2 and start game
                } else if (input == '3') {
                    // Will set the save state to 3 and start game
                } else if (input == '4') {
                    // Will set the save state to 4 and start game
                } else if (input == '0') {
                    Console.Clear();
                    return false;
                } else {
                    InvalidInput(StartMenu.GetSaveMenu(saveNames[0], saveNames[1], saveNames[2], saveNames[3]));
                    continue;
                }
                break;
            }

            return true;
        }
        #endregion



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
                    string Message = Manager.SaveGame();
                    Console.Clear();
                    Console.WriteLine(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetAvailableAdventurers(), Manager.GetBalance()));
                    Console.WriteLine(Message);
                    Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
                } else if (input == '0') {
                    ExitMenu();
                    break;
                } else {
                    InvalidInput(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetAvailableAdventurers(), Manager.GetBalance()));
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
            GetTavernMenu(out string[] AdventurerCards, out List<Adventurer> Adventurers);

            while (true) {

                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    if (Adventurers.Count >= 1) {
                        DismissAdventurer(0);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '2') {
                    if (Adventurers.Count >= 2) {
                        DismissAdventurer(1);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '3') {
                    if (Adventurers.Count >= 3) {
                        DismissAdventurer(2);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '4') {
                    if (Adventurers.Count >= 4) {
                        DismissAdventurer(3);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '5') {
                    if (Adventurers.Count >= 5) {
                        DismissAdventurer(4);
                    } else {
                        RecruitAdventurer();
                    }
                } else if (input == '0') {
                    break;
                } else {
                    InvalidInput(PlayMenu.GetTavern(AdventurerCards));
                    continue;
                }

                Console.Clear();
                GetTavernMenu(out AdventurerCards, out Adventurers);
            }

        }

        private void GetTavernMenu(out string[] AdventurerCards, out List<Adventurer> Adventurers) {
            AdventurerCards = new string[5];
            Adventurers = Manager.GetAllAdventurers();
            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
                if (i >= AdventurerCards.Length) { break; }
            }
            Console.WriteLine(PlayMenu.GetTavern(AdventurerCards));
        }

        private void DismissAdventurer(int who) {
            Console.Clear();
            Adventurer Adventurer = Manager.GetAdventurer(who);
            Currency Value = Adventurer.Value * 0.7;

            Console.WriteLine(PlayMenu.GetTavernDismissing(Adventurer.Name, Value.ToString()));

            while (true) {

                input = Console.ReadKey().KeyChar;

                if (input == 'y') {
                    Manager.DismissAdventurer(who);
                    return;
                } else if (input == 'n') {
                    return;
                } else {
                    InvalidInput(PlayMenu.GetTavernDismissing(Adventurer.Name, Value.ToString()));
                }
            }
        }

        private void RecruitAdventurer() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetTavernRecruiting());

            while (true) {

                input = Console.ReadKey().KeyChar;

                if (input == '1') {
                    Manager.RecruitAdventurer(1);
                } else if (input == '2') {
                    Manager.RecruitAdventurer(2);
                } else if (input == '3') {
                    Manager.RecruitAdventurer(3);
                } else if (input != '0') {
                    InvalidInput(PlayMenu.GetTavernRecruiting());
                    continue;
                }
                return;
            }

        }

        private void ArmoryMenu() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetArmory());
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
            Console.WriteLine(PlayMenu.GetPlayMenu(Manager.GetAdventurerCount(), Manager.GetAvailableAdventurers(), Manager.GetBalance()) + "\n");
            Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
        }

        private static void InvalidInput(string _display) {
            Console.Clear();
            Console.WriteLine(_display);
            Console.WriteLine("Invalid input");
        }
    }
}
