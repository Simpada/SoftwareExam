using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    internal class PlayMenu {


        
        public string GetPlayMenu() {
            return $@" 
    THE ADVENTURER'S LEAGUE   

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
        public string GetTavern() {

            return $@"
    YE OL' TAVERN
    [0] Return to town
                        
    |-----------------------------------------
    |       [1] DISMISS ADVENTURER
    |   _       
    |  \*/      Name:   Charles The Hairy
    |   |       Class:  Mage
    |   |       Health: 7
    |   |       Damage: 15
    |   V       Luck:   1
    |-----------------------------------------
    |       [2] DISMISS ADVENTURER
    | ______    
    | | __ |    Name:   Frida The Incomprehensible
    | | || |    Class:  Warrior
    | | || |    Health: 20
    | \ '' /    Damage: 11
    |  \__/     Luck:   15
    |-----------------------------------------
    |       [3] DISMISS ADVENTURER
    |   |\      
    |   | |     Name:   Ken The Mass Murderer
    |   | |     Class:  Rogue
    |  [===]    Health: 7
    |   | |     Damage: 10
    |   |_|     Luck:   20
    |-----------------------------------------
    |  
    |  
    |
    |       [4] RECRUIT NEW ADVENTURER
    |  
    |
    |  
    |-----------------------------------------
    |  
    |
    |  
    |       [5] RECRUIT NEW ADVENTURER
    |  
    |
    |  
    |-----------------------------------------

";
        }

        public string GetTavernRecruiting() {

            return $@"

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
