﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Expedition {
    public class Expeditions {

        private List<Map> Maps = new();

        public Expeditions() {
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
                MapDescriptions += "    |-----------------------------------------";
            }


            return MapDescriptions;
        }

        public void SelectMap(int mapNr) {

            ReplaceMap(mapNr);
        }

        private void ReplaceMap(int mapNr) {

            foreach (Map map in Maps) {
                if ( (int)map.Difficulty == mapNr) {

                }
            }

        }

    }
}
