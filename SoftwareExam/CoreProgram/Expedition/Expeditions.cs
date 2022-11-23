using SoftwareExam.CoreProgram.Adventurers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Expedition {
    public class Expeditions {

        private List<Map> Maps = new();
        public Player Player { set; get; }
        public LogWriter Log { get; } = new();

        public Expeditions(Player player) {
            Player = player;
            SetUpMaps();
        }

        private void SetUpMaps () {
            Maps.Clear();
            for (int i = 0; i < 4; i++) {
                Maps.Add(Map.GetMap(i));
            }
        }

        public string GetMaps() {

            Maps.Sort();
            
            string MapDescriptions = "    |-----------------------------------------";

            foreach (Map map in Maps) {
                MapDescriptions += map.ToString();
                MapDescriptions += "\n    |-----------------------------------------";
            }


            return MapDescriptions;
        }

        public void PrepareMission(int mapNr, Adventurer adventurer) {

            Map Destination = new();

            foreach (Map map in Maps) {
                if ((int)map.Difficulty == mapNr) {
                    Destination = map;
                    break;
                }
            }

            _ = new Mission(Player, Destination, adventurer, Log);

            Maps.Remove(Destination);
            Maps.Add(Map.GetMap(mapNr));
        }

        public void Resume() {
            Log.Resume();
        }

        public void Pause() {
            Log.Pause();
        }
    }
}
