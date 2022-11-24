using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Expedition;
using SoftwareExam.DataBase;

namespace SoftwareExam.CoreProgram
{
    public class GameManager {

        private readonly Recruitment Recruitment;
        private readonly DataBaseAccess DataBaseAccess;
        private readonly Armory Armory;
        private readonly Expeditions Expeditions;
        private Player Player;
        private readonly int MaxAdventurers = 5;

        public GameManager() {
            Player = new Player();
            DataBaseAccess = new DataBaseAccess("Data Source = AdventureLeague.db");
            Recruitment = new Recruitment();
            Armory = new Armory();
            Expeditions = new Expeditions(Player);
        }


        public void SetPlayer(Player player) {
            Player = player;
        }

        public string GetLogMessage() {
            return Player.GetLogMessages();
        }

        public string GetBalanceString() {
            return Player.Balance.ToString();
        }

        public Currency GetBalanceValue() {
            return Player.Balance;
        }

        public void CheckBalance(out bool canAfford, out string newBalance, out string cost) {

            canAfford = Recruitment.CheckBalance(Player.Balance);
            cost = Recruitment.Price.ToString();
            newBalance = (Player.Balance - Recruitment.Price).ToString();

        }

        // Relates to adventurers
        #region
        public bool RecruitAdventurer(int type) {
            // Replace with push to Player object

            Adventurer? adventurer = Recruitment.RecruitAdventurer(type, Player.Balance);

            if (adventurer == null) {
                return false;
            } else {
                Player.AlterCurrency(Recruitment.Price, false);
                Player.Adventurers.Add(adventurer);
                return true;
            }
        }

        public void DismissAdventurer(int who) {
            Player.Adventurers.RemoveAt(who);
        }

        public string[] GetAllAdventurerCards() {

            // This sets the maximum amount of adventurers you can display
            string[] AdventurerCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                AdventurerCards[i] = Adventurers[i].ToString();
            }

            return AdventurerCards;
        }

        public string GetAvailableAdventurerCards() {

            string AvailableAdventurers = "";

            for (int i = 0; i < Player.Adventurers.Count; i++) {
                if (Player.Adventurers[i].OnMission) {
                    AvailableAdventurers += $"    |           ON A MISSION\n";
                } else {
                    AvailableAdventurers += $"    |       [{i + 1}] CHOOSE ADVENTURER\n";
                }
                AvailableAdventurers += Player.Adventurers[i].ToString();
                AvailableAdventurers += "\n    |-----------------------------------------\n";
            }
            return AvailableAdventurers;
        }

        public bool GetAvilability(int index) {

            if (Player.Adventurers.Count >= index + 1) {
                return !Player.Adventurers[index].OnMission;
            }
            return false;
        }


        public string[] GetAllItemCards() {

            string[] ItemCards = new string[MaxAdventurers];

            List<Adventurer> Adventurers = Player.Adventurers;

            for (int i = 0; i < Adventurers.Count; i++) {
                ItemCards[i] = Adventurers[i].GetItemCard();
            }

            return ItemCards;
        }

        public void GetAdventurerSellValue(int who, out string name, out string value) {

            double sellMultiplier = 0.7;

            Adventurer adventurer = Player.Adventurers[who];

            name = adventurer.Name;
            value = (adventurer.Value * sellMultiplier).ToString();
        }

        public Adventurer GetAdventurer(int who) {
            return Player.Adventurers[who];
        }

        public int GetAdventurerCount() {
            return Player.Adventurers.Count;
        }
        #endregion


        public string GetExpeditionMaps() {
            return Expeditions.GetMaps();
        }

        public void PrepareExpedition(int mapNr, int adventurerNr) {
            Expeditions.PrepareMission(mapNr, Player.Adventurers[adventurerNr]);
        }

        public void SaveGame() {

            DataBaseAccess.Save(Player);

        }

        public int LoadGame(int Id) {

            Player = DataBaseAccess.GetPlayerById(Id);
            Expeditions.Player = Player;

            List<Adventurer> Adventurers = DataBaseAccess.GetAdventurers(Id);

            for (int i = 0; i < Adventurers.Count; i++) {
                List<int> itemCodes = DataBaseAccess.GetDecorators(Adventurers[i].Id);

                // Parse items and give to adventurers here

                foreach (int itemCode in itemCodes) {
                    Adventurer.AddNewItem(ItemParser.GetItem(itemCode, Adventurers[i]));
                }
                Adventurers[i] = Adventurer.EquipGear(Adventurers[i]);
            }

            Player.Adventurers = Adventurers;

            GetMissions(Id);

            return Player.Id;
        }

        public void GetMissions(int id)
        {
            List <Mission> missions = DataBaseAccess.GetMissionsForAdventurers(id);

            foreach (var Mission in missions) {
                Mission.Player = Player;

                foreach (var Adventurer in Player.Adventurers) {
                    if (Adventurer.Id == Mission.AdventurerId) {
                        Mission.Adventurer = Adventurer;
                        break;
                    }
                }
                if (Mission.Adventurer == null) {
                    throw new Exception("Adventurer cannot be found. Saving/loading process error");
                }

                Mission.LogWriter = Expeditions.Log;

                Task.Run(() => Mission.Start());
            }
        }



        public string[] GetPlayers()
        {
            return DataBaseAccess.RetrieveAllPlayerNames();
        }

        public void DeleteSave(int saveFile) {
            DataBaseAccess.Delete(saveFile);
        }

        public void NewGame(int saveFile, string name) {
            Player.Id = saveFile;
            Player.PlayerName = name;
            Player.SetCurrency(0,5,1);
            Player.Adventurers = new();
            Random random = new();
            _ = RecruitAdventurer(random.Next(3) + 1);
        }

        public void Pause() {
            Expeditions.Pause();
        }

        public void Resume() {
            Expeditions.Resume();
        }

        public void Terminate()
        {
            foreach (var mission in Player.Missions) {
                mission.Terminate();
            }

            Player.TerminateMissions();
        }

        public List<string> GetInventoryNames() {
            return Armory.GetItemNames();
        }
        
        public List<string> GetInventoryDescriptions() {
            return Armory.GetItemDescriptions();
        }

        public List<string> GetInventoryPrices() {
            return Armory.GetItemPrices();
        }

        public string GetItemCards(int id) {
            return Player.Adventurers[id].GetItemCard();
        }

        public void EnterArmory(int adventurerId) {
            Armory.EnterArmory(Player.Adventurers[adventurerId].Class);
        }
        public void ExitArmory() {
            Armory.Resume();
        }
    }
}
