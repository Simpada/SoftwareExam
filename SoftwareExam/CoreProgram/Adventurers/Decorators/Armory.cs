using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {

    public class Armory {

        private readonly List<int> Armors = new();
        private readonly List<int> Hats = new();
        private readonly List<int> OffHands = new();
        private readonly List<int> Trinkets = new();
        private readonly List<int> Weapons = new();

        private List<int> Inventory = new();
        private readonly int InventorySize = 8;

        private readonly Random Random = new Random();

        private readonly int InventoryRefreshRate = 60000;

        private readonly ManualResetEvent TaskPauseEvent = new (true);

        public Armory() {

            InitializeItems();
            // Starts Refresh Inventory on its own thread, that will always run, resetting inventory every x seconds

            Task.Run(() => {RefreshInventory();});

        }

        public void Pause() {
            TaskPauseEvent.Reset();
            
        }
        public void Resume() {
            TaskPauseEvent.Set();
        }

        #region Pretend this part doesn't exist
        private void InitializeItems() {
            // huge switch to parse item id's into items with initialiser
            Armors.Add(101);
            Armors.Add(102);
            Armors.Add(103);
            Armors.Add(104);
            Armors.Add(105);

            Hats.Add(201);
            Hats.Add(202);
            Hats.Add(203);
            Hats.Add(204);
            Hats.Add(205);

            OffHands.Add(301);
            OffHands.Add(302);
            OffHands.Add(303);
            OffHands.Add(304);
            OffHands.Add(305);
            
            Trinkets.Add(401);
            Trinkets.Add(402);
            Trinkets.Add(403);
            Trinkets.Add(404);
            Trinkets.Add(405);
            
            Weapons.Add(501);
            Weapons.Add(502);
            Weapons.Add(503);
            Weapons.Add(504);
            Weapons.Add(505);

        }
        #endregion

        private void RefreshInventory() {

            while (true) {

                TaskPauseEvent.WaitOne();

                for (int i = InventorySize; i >= 0; i--) {
                    int randomNumber = Random.Next(5);
                    switch (randomNumber) { 
                        case 0:
                        Inventory.Add(Armors[Random.Next(Armors.Count)]);
                        break;
                        case 1:
                        Inventory.Add(Hats[Random.Next(Hats.Count)]);
                        break;
                        case 2:
                        Inventory.Add(OffHands[Random.Next(OffHands.Count)]);
                        break;
                        case 3:
                        Inventory.Add(Trinkets[Random.Next(Trinkets.Count)]);
                        break;
                        case 4:
                        Inventory.Add(Weapons[Random.Next(Weapons.Count)]);
                        break;
                    }
                }

                Thread.Sleep(InventoryRefreshRate);
            }
        }

        public void BuyItem(Adventurer adventurer) {

        }


    }
}
