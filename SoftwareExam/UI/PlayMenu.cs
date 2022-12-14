namespace SoftwareExam.UI {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static class PlayMenu {

        #region Main Display / Log

        public static string GetLog(string log) {
            return $@"
    EXPEDITION LOG: 
{log}";
        }

        public static string GetPlayMenu(int totalAdventurers, string balance) {
            return $@" 
    THE ADVENTURER'S LEAGUE   

    Your balance is : {balance}
    You have {totalAdventurers} adventurers

    [1] Enter Guild House (Plan expeditions)                
    [2] Go to The Tavern (Hire Adventurers)                 
    [3] Shop at The Armory (Upgrade Adventurers)            
    [4] Save
    [0] Exit Game
";
        }

        public static string GetVillage() {

            return $@"
                                                                      .
                                            ^                        /_\
      ))                 ))                /|\                       |0|
    .-#----------.    .--#----------.     //|\\                .-----' '-----.
   /___[ARMORY]___\  /_[GUILD HOUSE]_\    //|\\      _____    /__[OL TAVERN]__\
    | .----. .-. |    | +-----+ .-. |    ///|\\\    /_____\    | [] .-.-. [] |
....| '----' |*| |....| +-----+ |*| |....///|\\\....|''#''|....|    | | |    |....

";
        }

        #endregion

        #region Guild House / Expeditions Display
        public static string GetGuildHouseExpeditions(string maps, string balance) {
            return $@"
    THE GUILD OF ADVENTURERS
    [0] Return to town

    Your balance is : {balance}

    Available Maps:
{maps}
";
        }

        public static string GetGuildHouseAdventurers(string adventurerCards) {
            return $@"
    Available Adenturers:
    [0] Return to guild house

    |-----------------------------------------
{adventurerCards}
";
        }
        #endregion

        #region Tavern / Recruitment Display
        public static string GetTavern(string[] adventurerCards, string balance) {

            string[] cards = new string[adventurerCards.Length];

            for (int i = 0; i < adventurerCards.Length; i++) {

                if (string.IsNullOrEmpty(adventurerCards[i])) {
                    cards[i] = $"    |\n    |\n    |\n    |       [{i + 1}] RECRUIT NEW ADVENTURER \n    |\n    |\n    |";
                } else if (adventurerCards[i].Contains("MISSION")) {
                    string TavernCard = $"{adventurerCards[i]}";
                    cards[i] = TavernCard;
                } else {
                    string TavernCard = $"    |       [{i + 1}] DISMISS ADVENTURER\n" +
                        $"{adventurerCards[i]}";
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

        public static string GetTavernRecruiting(bool canAfford, string newBalance, string cost) {

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

        public static string GetTavernDismissing(string name, string profit) {
            return $@"

    Are you sure you want to dismiss {name}?
    This action cannot be undone!
    You will earn {profit}.

    [Y] Yes
    [N] No

";
        }
        #endregion

        #region Armory / Buy Gear Display
        public static string GetArmory(string[] itemCards, string balance) {

            string[] cards = new string[itemCards.Length];

            for (int i = 0; i < itemCards.Length; i++) {

                if (string.IsNullOrEmpty(itemCards[i])) {
                    cards[i] = $"    |\n    |\n    |\n    |          NO ADVENTURER \n    |\n    |\n    |";
                } else {

                    string tavernCard;
                    if (!itemCards[i].Contains("MISSION")) {
                        tavernCard = $"    |       [{i + 1}] BUY GEAR \n" +
                        $"{itemCards[i]}";
                    } else {
                        tavernCard = $"{itemCards[i]}";
                    }
                    
                    cards[i] = tavernCard;
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
                        
    Balance = {balance}
    |-----------------------------------------
{ArmoryDisplay}
";
        }

        public static string GetArmoryBrowsing(string itemCard, string balance, List<string> names, List<string> descriptions, List<string> prices) {

            string display = "";
            for (int i = 0; i < descriptions.Count; i++) {
                display += $"    |   [{i + 1}] {names[i]}"
                    + $"\n    |   {descriptions[i]}"
                    + $"\n    |   Price:  {prices[i]}";
                display += "\n    |-----------------------------------------\n";
            }

            return $@"
    CURRENT INVENTORY
    [0] Return to Armory

    |-----------------------------------------
{itemCard}
    |
    |   AVAILABLE ITEMS: 
    |   Balance: {balance}
    |_________________________________________
{display}
";
        }

        public static string GetExitMenu() {

            return @" 
    [1] Go To Main Menu
    [2] Save and Go To Main Menu
    [3] Quit Game
    [4] Save and Quit Game
    [0] Resume Game
";
        }
    }
    #endregion
}
