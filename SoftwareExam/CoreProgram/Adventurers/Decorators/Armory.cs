using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    public class Armory {

        private List<BasicArmor> Armors = new();
        private List<BasicHat> Hats = new();
        private List<BasicOffHand> OffHands = new();
        private List<BasicTrinket> Trinkets = new();
        private List<BasicWeapon> Weapons = new();

        private List<BaseDecoratedAdventurer> Inventory;
        private readonly int InventorySize = 8;

        private Random Random = new Random();

        private readonly int InventoryRefreshRate = 60000;

        public Armory() {

            // Starts Refresh Inventory on its own thread, that will always run, resetting inventory ever x seconds

        }

        private void InitializeItems() {
            Armors.Add(new ArmorPlateArmor(null));

        }

        private void RefreshInventory() {

            while (true) {

                Inventory = new();
                Inventory.Add(Armors[Random.Next(Armors.Count)]);
                Inventory.Add(Hats[Random.Next(Armors.Count)]);
                Inventory.Add(OffHands[Random.Next(Armors.Count)]);
                Inventory.Add(Trinkets[Random.Next(Armors.Count)]);
                Inventory.Add(Weapons[Random.Next(Armors.Count)]);

                List<BaseDecoratedAdventurer> Selection = new();
                



                Thread.Sleep(InventoryRefreshRate);
            }
        }

        public void BuyItem(Adventurer adventurer) {

        }


    }
}
