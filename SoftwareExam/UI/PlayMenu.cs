using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    internal class PlayMenu {


        
        public string GetPlayMenu(int totalAdventurers, int availableAdventurers, string balance) {
            return $@" 
    THE ADVENTURER'S LEAGUE   

    Your balance is : {balance}
    You have {totalAdventurers} adventurers
    {availableAdventurers} are in town
    {totalAdventurers-availableAdventurers} are on a quest

    [1] Enter Guild House (Plan expeditions)                
    [2] Go to The Tavern (Hire Adventurers)                 
    [3] Shop at The Armory (Upgrade Adventurers)            
    [4] Save
    [0] Exit Game
";
        }

        public string GetVillage(string log) {

            return $@"
                                                                      .
                                            ^                        /_\
      ))                 ))                /|\                       |0|
    .-#----------.    .--#----------.     //|\\                .-----' '-----.
   /___[ARMORY]___\  /_[GUILD HOUSE]_\    //|\\      _____    /__[OL TAVERN]__\
    | .----. .-. |    | +-----+ .-. |    ///|\\\    /_____\    | [] .-.-. [] |
....| '----' |*| |....| +-----+ |*| |....///|\\\....|''#''|....|    | | |    |....


    EXPEDITION LOG: 
{log}

";
        }


        public string GetGuildHouseExpeditions() {

            return $@"

";
        }


        public string GetTavern(String[] AdventurerCards) {

            string[] cards = new string[5];

            for (int i = 0; i < AdventurerCards.Length; i++) {

                if (String.IsNullOrEmpty(AdventurerCards[i])) {
                    cards[i] = $"    |\n    |\n    |\n    |       [{i+1}] RECRUIT NEW ADVENTURER \n    |\n    |\n    |";
                } else {
                    string TavernCard = $"    |       [{i + 1}] DISMISS ADVENTURER\n" +
                        $"{AdventurerCards[i]}";
                    cards[i] = TavernCard;
                }
            }

            int _index = 0;

            return $@"
    YE OL' TAVERN
    [0] Return to town
                        
    |-----------------------------------------
{cards[_index++]}
    |-----------------------------------------
{cards[_index++]}
    |-----------------------------------------
{cards[_index++]}
    |-----------------------------------------
{cards[_index++]} 
    |-----------------------------------------
{cards[_index++]}
    |-----------------------------------------

";
        }

        public string GetTavernRecruiting() {

            return $@"
    YE OL' TAVERN
    [0] Cancel Recruitment

    |-----------------------------------------
    |       [1] RECRUIT NEW WARRIOR
    | ______    
    | | __ |    
    | | || |    Health: 10
    | | || |    Damage: 5
    | \ '' /    Luck:   5
    |  \__/     
    |-----------------------------------------
    |       [2] RECRUIT NEW MAGE
    |   _       
    |  \*/      
    |   |       Health: 5
    |   |       Damage: 10
    |   |       Luck:   5
    |   V       
    |-----------------------------------------
    |       [3] RECRUIT NEW ROGUE
    |   |\      
    |   | \     
    |   | |     Health: 5
    |  [===]    Damage: 5
    |   | |     Luck:   10
    |   |_|     
    |-----------------------------------------

";
        }

        public string GetTavernDismissing(string name, string profit) {

            return $@"

    Are you sure you want to dismiss {name}?
    This action cannot be undone!
    You will earn {profit}.

    [Y] Yes
    [N] No

";
        }

        public string GetArmory() {

            return $@"

";
        }

        public string GetArmoryBrowsing() {

            return $@"

";
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
