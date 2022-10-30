using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    internal class PlayMenu {


        
        public string GetVillage() {


            return @" 
    The Adventurer's League    

    [1] Enter Guild House (Plan expeditions)
    [2] Go to The Tavern (Hire Adventurers)
    [3] Shop at The Armory (Upgrade Adventurers)
    [4] Save
    [0] Exit Game                                                     .
                                            ^                        /_\
      ))                 ))                /|\                       |0|
    .-#----------.    .--#----------.     //|\\                .-----' '-----.
   /___[ARMORY]___\  /_[GUILD HOUSE]_\    //|\\      _____    /__[OL TAVERN]__\
    | .----. .-. |    | +-----+ .-. |    ///|\\\    /_____\    | [] .-.-. [] |
....| '----' |*| |....| +-----+ |*| |....///|\\\....|''#''|....|    | | |    |....



";
        }


        public string GetGuildHouse() {

            return "";
        }
        public string GetTavern() {

            return "";
        }

        public string GetArmory() {

            return "";
        }


        public string GetExitMenu() {

            return @" 
    [1] Go To Main Menu
    [2] Save and Go To Main Menu
    [3] Quit Game
    [4] Save and Quit Game
";

        }

    }

}
