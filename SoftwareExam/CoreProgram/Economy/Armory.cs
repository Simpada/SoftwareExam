using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;

namespace SoftwareExam.CoreProgram.Economy {

    public class Armory {

        private readonly List<int> Armors = new();
        private readonly List<int> Hats = new();
        private readonly List<int> OffHands = new();
        private readonly List<int> Trinkets = new();
        private readonly List<int> Weapons = new();

        private readonly List<int> FullInventory = new();
        private readonly List<int> DisplayInventory = new();
        private readonly int InventorySize = 8;

        private readonly Random Random = new();

        private readonly int InventoryRefreshRate = 60000;

        private readonly ManualResetEvent TaskPauseEvent = new(true);

        public Armory() {

            InitializeItems();
            // Starts Refresh Inventory on its own thread, that will always run, resetting inventory every x seconds

            Task.Run(() => { RefreshInventory(); });

        }

        public void Pause() {
            TaskPauseEvent.Reset();
        }
        public void Resume() {
            TaskPauseEvent.Set();
        }

        #region Pretend this part doesn't exist
        private void InitializeItems() {
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
            Weapons.Add(506);
        }
        #endregion

        private void RefreshInventory() {
            while (true) {
                TaskPauseEvent.WaitOne();
                FullInventory.Clear();
                for (int i = 0; i < InventorySize; i++) {
                    int randomNumber = Random.Next(5);
                    switch (randomNumber) {
                        case 0:
                        FullInventory.Add(Armors[Random.Next(Armors.Count)]);
                        break;
                        case 1:
                        FullInventory.Add(Hats[Random.Next(Hats.Count)]);
                        break;
                        case 2:
                        FullInventory.Add(OffHands[Random.Next(OffHands.Count)]);
                        break;
                        case 3:
                        FullInventory.Add(Trinkets[Random.Next(Trinkets.Count)]);
                        break;
                        case 4:
                        FullInventory.Add(Weapons[Random.Next(Weapons.Count)]);
                        break;
                    }
                }
                Thread.Sleep(InventoryRefreshRate);
            }
        }

        public void EnterArmory(string adventurerClass) {

            Pause();

            DisplayInventory.Clear();

            foreach (int itemId in FullInventory) {

                foreach (string allowedClass in ItemParser.GetAllowedClasses(itemId)) {

                    if (allowedClass == adventurerClass) {
                        DisplayInventory.Add(itemId);
                    }
                }
            }
        }

        public List<string> GetItemNames() {

            List<string> Names = new();

            foreach (var item in DisplayInventory) {
                Names.Add(ItemParser.GetItemName(item));
            }
            return Names;
        }

        public List<string> GetItemDescriptions() {

            List<string> Descriptions = new();

            foreach (var item in DisplayInventory) {
                Descriptions.Add(ItemParser.GetItemDescription(item));
            }
            return Descriptions;
        }

        public List<string> GetItemPrices() {

            List<string> Prices = new();

            foreach (var item in DisplayInventory) {
                Prices.Add(ItemParser.GetItemCost(item).ToString());
            }
            return Prices;
        }

        public bool CanAffordItem(int itemIndex, Currency currency, out bool noItem, out Currency price) {

            if (itemIndex >= DisplayInventory.Count) {
                price = new();
                noItem = true;
                return false;
            }

            noItem = false;
            price = ItemParser.GetItemCost(DisplayInventory[itemIndex]);

            if (currency >= price) {
                return true;
            }
            return false;
        }

        public BaseDecoratedAdventurer BuyItem(int itemIndex, Adventurer adventurer) {
            int itemId = DisplayInventory[itemIndex];

            FullInventory.Remove(itemId);

            return ItemParser.GetItem(itemId, adventurer);
        }


    }
}
