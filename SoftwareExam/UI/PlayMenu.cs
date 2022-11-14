using SoftwareExam.CoreProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    public class PlayMenu {


        
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


        public string GetTavern(string[] AdventurerCards, string balance) {

            string[] cards = new string[AdventurerCards.Length];

            for (int i = 0; i < AdventurerCards.Length; i++) {

                if (string.IsNullOrEmpty(AdventurerCards[i])) {
                    cards[i] = $"    |\n    |\n    |\n    |       [{i+1}] RECRUIT NEW ADVENTURER \n    |\n    |\n    |";
                } else {
                    string TavernCard = $"    |       [{i + 1}] DISMISS ADVENTURER\n" +
                        $"{AdventurerCards[i]}";
                    cards[i] = TavernCard;
                }
            }

            int _index = 0;

            string MerchantDisplay = "";
            foreach (string card in cards) {
                MerchantDisplay += cards[_index++];
                MerchantDisplay += "\n    |-----------------------------------------\n";
            }


            return $@"
    YE OL' TAVERN
    [0] Return to town

    Your balance is : {balance}
                        
    |-----------------------------------------
{MerchantDisplay}
";
        }

        public string GetTavernRecruiting(bool canAfford, string newBalance, string cost) {


            string balanceMessage;
            string[] buyMessage = new string[3];

            if (canAfford) {
                balanceMessage = $@"    Your new balance will be: {newBalance}
    All decisions to recruit are final
    NO REFUNDS!";
                buyMessage[0] = "[1] RECRUIT NEW WARRIOR";
                buyMessage[1] = "[2] RECRUIT NEW MAGE";
                buyMessage[2] = "[3] RECRUIT NEW ROGUE";
            } else {
                balanceMessage = "\n    You cannot afford to recruit a new adventurer";
                buyMessage[0] = "YOU CANNOT AFFORD THIS!";
                buyMessage[1] = "YOU CANNOT AFFORD THIS!";
                buyMessage[2] = "YOU CANNOT AFFORD THIS!";
            }

            return $@"
    YE OL' TAVERN

    [0] Cancel Recruitment

    Recruitment cost: {cost}
{balanceMessage}

    |-----------------------------------------
    |       {buyMessage[0]}
    | ______    
    | | __ |    
    | | || |    Health: 10
    | | || |    Damage: 5
    | \ '' /    Luck:   5
    |  \__/     
    |-----------------------------------------
    |       {buyMessage[1]}
    |   _       
    |  \*/      
    |   |       Health: 5
    |   |       Damage: 10
    |   |       Luck:   5
    |   V       
    |-----------------------------------------
    |       {buyMessage[2]}
    |  |\       
    |  | \      
    |  | |      Health: 5
    | [===]     Damage: 5
    |  | |      Luck:   10
    |  |_|     
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

        public string GetArmory(string[] ItemCards) {
             

            string[] cards = new string[ItemCards.Length];

            for (int i = 0; i < ItemCards.Length; i++) {

                if (string.IsNullOrEmpty(ItemCards[i])) {
                    cards[i] = $"    |\n    |\n    |\n    |          NO ADVENTURER \n    |\n    |\n    |";
                } else {
                    string TavernCard = $"    |       [{i + 1}] BUY GEAR \n" +
                        $"{ItemCards[i]}";
                    cards[i] = TavernCard;
                }
            }

            int _index = 0;

            string ArmoryDisplay = "";
            foreach (string card in cards) {
                ArmoryDisplay += cards[_index++];
                ArmoryDisplay += "\n    |-----------------------------------------\n";
            }

            return $@"
    THE GRAND ARMORY
    [0] Return to town
                        

    |-----------------------------------------
{ArmoryDisplay}
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
