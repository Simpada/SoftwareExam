using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.UI {
    public static class StartMenu {

        public static string GetStartingMenu() {
            return @"
     _____________________________________
    /                                     \
    | Welcome to The Adventurer's League! |
    |                                     |
    |       [                             |
    |  @xxxx[{::::::::::::::::::::::>     |
    |       [                             |
    |                                     |
    \_____________________________________/

    [1] : Play Game
    [2] : How To Play
    [0] : Exit

";
        }

        public static string GetAboutMenu() {
            return @"
    [1] : Return

    The Adventurer's League is a simple fantasy management game.
    The goal is to become a wealthy Guild Leader.
    
    Ol Tavern:
    In the tavern, you can recruit and sell adventurers.

    Armory: 
    In the armory, you can replace or upgrade the gear of your adventurers.

    Guild House:
    At the guild house, you can select missions and send your
    adventurer's out to earn you money. 

    Health:
    Health determined the durability of an adventurer, high
    health makes an adventurer stronger against certain enemies

    Damage: 
    Damage determines the combat strength of an adventurer.
    High damage helps deal with certain enemies.

    Luck:
    Luck determines an adventurer's general luck and good fortune
    High luck results in better encounters, and a chance of finding
    more treasure.

    [1] : Return
";
        }

        public static string GetSaveMenu(string name1, string name2, string name3, string name4) {
            return $@"
     ___________________
    /                   \
        [1] Save 1      
        {name1}     
    \___________________/
     ___________________
    /                   \
        [2] Save 2      
        {name2}     
    \___________________/
     ___________________
    /                   \
        [3] Save 3      
        {name3}    
    \___________________/
     ___________________
    /                   \
        [4] Save 4      
        {name4}     
    \___________________/

    
    [0] Return
";
        }

        public static string GetContinue() {
            return @"
    [1] Continue
    [2] New Game (Will delete the old save)
    [3] Delete Save
    [0] Cancel
";
        }

        public static string GetNewGame() {
            return @"
    Enter blank to Cancel

    Enter a name that does not contain numbers or symbols
    -: ";
        }

    }
}
