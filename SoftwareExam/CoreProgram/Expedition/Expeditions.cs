using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Expedition {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Handles interractions with maps as well as starting missions on maps
    /// </summary>
    public class Expeditions {

        private readonly List<Map> _maps = new();
        public Player Player { set; get; }
        public LogWriter Log { get; } = new();

        public Expeditions(Player player) {
            Player = player;
            SetUpMaps();
        }

        private void SetUpMaps() {
            _maps.Clear();
            for (int i = 0; i < 4; i++) {
                _maps.Add(Map.GetMap(i));
            }
        }

        /// <summary>
        /// Gets all map descriptions
        /// </summary>
        /// <returns>A string with the descriptions</returns>
        public string GetMaps() {

            _maps.Sort();

            string mapDescriptions = "    |-----------------------------------------";

            foreach (Map map in _maps) {
                mapDescriptions += map.ToString();
                mapDescriptions += "\n    |-----------------------------------------";
            }
            return mapDescriptions;
        }

        /// <summary>
        /// Attempts the purchase of a map
        /// </summary>
        /// <param name="mapNr">Difficulty level of the map to buy</param>
        /// <param name="balance">Player Balance</param>
        /// <returns>Bool saying if the purchase was successfull</returns>
        public bool PurchaseMap(int mapNr, Currency balance) {

            Currency cost = new();

            foreach (Map map in _maps) {
                if ((int)map.Difficulty == mapNr) {
                    cost = map.ExpeditionCost;
                    break;
                }
            }

            if (balance >= cost) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Prepares a map and sends an adventurer on a mission there, then takes the money from the player
        /// </summary>
        /// <param name="mapNr">The difficulty of the map to run</param>
        /// <param name="adventurer">The adventurer going on a mission</param>
        /// <param name="cost">The cost of sending the adventurer</param>
        public void PrepareMission(int mapNr, Adventurer adventurer, out Currency cost) {

            Map destination = new();

            foreach (Map map in _maps) {
                if ((int)map.Difficulty == mapNr) {
                    destination = map;
                    break;
                }
            }

            cost = destination.ExpeditionCost;
            _ = new Mission(Player, destination, adventurer, Log);

            _maps.Remove(destination);
            _maps.Add(Map.GetMap(mapNr));
        }

        public void Resume() {
            if (Log != null) {
                Log.Resume();
            }
        }
        public void Pause() {
            if (Log != null) {
                Log.Pause();
            }
        }
    }
}
