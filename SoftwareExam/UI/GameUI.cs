using SoftwareExam.CoreProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    internal class GameUI {

        private GameManager Manager;
        private StartMenu StartMenu;
        private PlayMenu PlayMenu;

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
                    Console.WriteLine(PlayMenu.GetPlayMenu());
                    Console.WriteLine(Message);
                    Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
                } else if (input == '0') {
                    ExitMenu();
                    break;
                } else {
                    InvalidInput(PlayMenu.GetPlayMenu());
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
            Console.WriteLine(PlayMenu.GetTavern());

            /*Needs to get the character cards from adventurers to print in the tavernm
            
            YE OL' TAVERN
            [0] Return to town
                        
            |-----------------------------------------
            |   _       [1] DISMISS ADVENTURER
            |  \+/      Name:   Charles The Hairy
            |   |       Class:  Mage
            |   |       Health: 7
            |   |       Damage: 15
            |   V       Luck:   1
            |-----------------------------------------
            | ______    [2] DISMISS ADVENTURER
            | | __ |    Name:   Frida The Incomprehensible
            | | || |    Class:  Warrior
            | | || |    Health: 20
            | \ '' /    Damage: 11
            |  \__/     Luck:   15
            |-----------------------------------------
            |   |\      [3] DISMISS ADVENTURER
            |   | |     Name:   Ken The Mass Murderer
            |   | |     Class:  Rogue
            |  [===]    Health: 7
            |   | |     Damage: 10
            |   |_|     Luck:   20
            |-----------------------------------------
            |  
            |  
            |   [4] RECRUIT NEW ADVENTURER
            |  
            |
            |  
            |-----------------------------------------
            |  
            |  
            |   [5] RECRUIT NEW ADVENTURER
            |  
            |
            |  
            |-----------------------------------------
            */

            while (true) {

                input = Console.ReadKey().KeyChar;

                if (input == '1') {

                } else if (input == '2') {

                } else if (input == '3') {

                } else if (input == '4') {

                } else if (input == '5') {

                } else if (input == '0') {
                    break;
                } else {
                    InvalidInput(PlayMenu.GetTavern());
                }
            }

        }



        private void DismissAdventurer() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetTavernDismissing("Hank", "GP: 2, SP: 5, CP: 7"));
        }

        private void RecruitAdventurer() {
            Console.Clear();
            Console.WriteLine(PlayMenu.GetTavernRecruiting());
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
            Console.WriteLine(PlayMenu.GetPlayMenu() + "\n");
            Console.WriteLine(PlayMenu.GetVillage(ExpeditionLog));
        }

        private static void InvalidInput(string _display) {
            Console.Clear();
            Console.WriteLine(_display);
            Console.WriteLine("Invalid input");
        }
    }
}
