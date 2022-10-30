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

        char input;


        public GameUI() {
            Manager = new GameManager();
            StartMenu = new StartMenu();
            PlayMenu = new PlayMenu(); 
        }



        public void Run() {

            MainMenu();


            PlayGame();

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

            Console.Clear();
            Console.WriteLine(PlayMenu.GetVillage());

        }


        private static void InvalidInput(string _display) {
            Console.Clear();
            Console.WriteLine(_display);
            Console.WriteLine("Invalid input");
        }
    }
}
