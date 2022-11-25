using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Expedition;
using SoftwareExam.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {

    public static class SaveManager {

        private static readonly DataBaseAccess DataBaseAccess = new ("Data Source = AdventureLeague.db");

        public static string[] RetrieveAllPlayerNames() {
            return DataBaseAccess.RetrieveAllPlayerNames();
        }

        public static void NewGame(Player player, int saveFile, string name) {
            player.Id = saveFile;
            player.PlayerName = name;
            player.SetCurrency(0, 0, 7);
            player.Adventurers = new();
        }
        public static void SaveGame(Player player) {
            DataBaseAccess.Save(player);
        }
        public static void DeleteSave(int saveFile) {
            DataBaseAccess.Delete(saveFile);
        }
        public static Player LoadGame(LogWriter logWriter, int Id) {

            Player player = DataBaseAccess.GetPlayerById(Id);

            player.Adventurers = GetAdventurers(player, Id);
            GetMissions(player, logWriter, Id);

            return player;
        }

        private static List<Adventurer> GetAdventurers(Player player, int Id) {

            List<Adventurer> adventurers = DataBaseAccess.GetAdventurers(Id);

            for (int i = 0; i < adventurers.Count; i++) {
                List<int> itemCodes = DataBaseAccess.GetDecorators(adventurers[i].Id);

                foreach (int itemCode in itemCodes) {
                    Adventurer.AddNewItem(ItemParser.GetItem(itemCode, adventurers[i]));
                }
                adventurers[i] = Adventurer.EquipGear(adventurers[i]);
            }
            return adventurers;
        }

        private static void GetMissions(Player player, LogWriter logWriter, int id) {
            List<Mission> missions = DataBaseAccess.GetMissionsForAdventurers(id);

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
