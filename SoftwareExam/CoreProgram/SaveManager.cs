using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Expedition;
using SoftwareExam.DataBase;

namespace SoftwareExam.CoreProgram {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// A class that handles saving and loading information to and from the database
    /// </summary>
    public static class SaveManager {

        private static readonly DataBaseAccess _dataBaseAccess = new("Data Source = AdventureLeague.db");

        /// <summary>
        /// Asks the database for a list of saved names
        /// </summary>
        /// <returns>An array of strings containing names</returns>
        public static string[] RetrieveAllPlayerNames() {
            return _dataBaseAccess.RetrieveAllPlayerNames();
        }

        public static bool CheckPlayer(int id) {
            return _dataBaseAccess.CheckIfPlayerExists(id);
        }

        /// <summary>
        /// Saves the player and its content to the database
        /// </summary>
        /// <param name="player">The player object to save</param>
        public static void SaveGame(Player player) {
            _dataBaseAccess.SaveGame(player);
        }
        /// <summary>
        /// Deletes a save from the database
        /// </summary>
        /// <param name="saveFile">The id of the save to delete</param>
        public static void DeleteSave(int saveFile) {
            _dataBaseAccess.Delete(saveFile);
        }
        /// <summary>
        /// Loads a save from the database
        /// </summary>
        /// <param name="logWriter">A logwriter to pass to potential missions</param>
        /// <param name="id">The id to find the player in the database</param>
        /// <returns>The player object for the gamemanager to use</returns>
        public static Player LoadGame(LogWriter logWriter, int id) {

            Player player = _dataBaseAccess.Load(id);

            player.Adventurers = GetAdventurers(id);
            GetMissions(player, logWriter, id);

            return player;
        }

        // Goes through the adventurers in the database and gives them to the player
        private static List<Adventurer> GetAdventurers(int id) {

            List<Adventurer> adventurers = _dataBaseAccess.GetAdventurers(id);

            for (int i = 0; i < adventurers.Count; i++) {
                List<int> itemCodes = _dataBaseAccess.GetDecorators(adventurers[i].Id);

                foreach (int itemCode in itemCodes) {
                    Adventurer.AddNewItem(ItemParser.GetItem(itemCode, adventurers[i]));
                }
                adventurers[i] = Adventurer.EquipGear(adventurers[i]);
            }
            return adventurers;
        }
        // Goes through missions, assigns adventurers and gives them to the player 
        private static void GetMissions(Player player, LogWriter logWriter, int id) {
            List<Mission> missions = _dataBaseAccess.GetMissionsForAdventurers(id);

            foreach (var Mission in missions) {
                Mission.Player = player;

                foreach (var Adventurer in player.Adventurers) {
                    if (Adventurer.Id == Mission.AdventurerId) {
                        Mission.Adventurer = Adventurer;
                        break;
                    }
                }
                if (Mission.Adventurer == null) {
                    throw new Exception("Adventurer cannot be found. Saving/loading process error");
                }

                Mission.LogWriter = logWriter;

                Task.Run(() => Mission.Start());
            }
        }
    }
}
