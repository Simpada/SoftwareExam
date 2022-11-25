using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;

namespace SoftwareExam.CoreProgram.Economy {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// A class that stores an automatically refreshing inventory of items that can be purchases by the player for its adventurers
    /// </summary>
    public class Armory {

        private readonly List<int> _armors = new();
        private readonly List<int> _hats = new();
        private readonly List<int> _offHands = new();
        private readonly List<int> _trinkets = new();
        private readonly List<int> _weapons = new();
        private readonly List<int> _fullInventory = new();
        private readonly List<int> _displayInventory = new();
        private readonly int _inventorySize = 8;
        private readonly int _inventoryRefreshRate = 60000;
        private readonly Random _random = new();

        private readonly ManualResetEvent _taskPauseEvent = new(true);

        public Armory() {
            InitializeItems();
            // Starts Refresh Inventory on its own thread, that will always run, resetting inventory every x seconds

            Task.Run(() => { RefreshInventory(); });
        }
        #region Pretend this part doesn't exist
        // This is purely to initialize our store with item codes, this is kinda ugly and manual, but its difficult to automate
        private void InitializeItems() {
            _armors.Add(101);
            _armors.Add(102);
            _armors.Add(103);
            _armors.Add(104);
            _armors.Add(105);

            _hats.Add(201);
            _hats.Add(202);
            _hats.Add(203);
            _hats.Add(204);
            _hats.Add(205);

            _offHands.Add(301);
            _offHands.Add(302);
            _offHands.Add(303);
            _offHands.Add(304);
            _offHands.Add(305);
            _offHands.Add(306);

            _trinkets.Add(401);
            _trinkets.Add(402);
            _trinkets.Add(403);
            _trinkets.Add(404);
            _trinkets.Add(405);

            _weapons.Add(501);
            _weapons.Add(502);
            _weapons.Add(503);
            _weapons.Add(504);
            _weapons.Add(505);
            _weapons.Add(506);
            _weapons.Add(507);
        }
        #endregion

        #region Base Functions

        // Pauses and resumes the RefresInventory function
        public void Pause() {
            _taskPauseEvent.Reset();
        }
        public void Resume() {
            _taskPauseEvent.Set();
        }
 
        private void RefreshInventory() {
            while (true) {
                _taskPauseEvent.WaitOne();
                _fullInventory.Clear();
                for (int i = 0; i < _inventorySize; i++) {
                    int randomNumber = _random.Next(5);
                    switch (randomNumber) {
                        case 0:
                        _fullInventory.Add(_armors[_random.Next(_armors.Count)]);
                        break;
                        case 1:
                        _fullInventory.Add(_hats[_random.Next(_hats.Count)]);
                        break;
                        case 2:
                        _fullInventory.Add(_offHands[_random.Next(_offHands.Count)]);
                        break;
                        case 3:
                        _fullInventory.Add(_trinkets[_random.Next(_trinkets.Count)]);
                        break;
                        case 4:
                        _fullInventory.Add(_weapons[_random.Next(_weapons.Count)]);
                        break;
                    }
                }
                Thread.Sleep(_inventoryRefreshRate);
            }
        }

        /// <summary>
        /// Shows appropriate inventory for the class of the adventure that entered the store
        /// </summary>
        /// <param name="adventurerClass">The class of the adventurer in the store</param>
        public void EnterArmory(string adventurerClass) {

            Pause();
            _displayInventory.Clear();

            foreach (int itemId in _fullInventory) {

                foreach (string allowedClass in ItemParser.GetAllowedClasses(itemId)) {

                    if (allowedClass == adventurerClass) {
                        _displayInventory.Add(itemId);
                    }
                }
            }
        }
        #endregion

        #region Information about items

        // Gets names, descriptions, and prices of items in the store respectively. 
        public List<string> GetItemNames() {

            List<string> names = new();

            foreach (var item in _displayInventory) {
                names.Add(ItemParser.GetItemName(item));
            }
            return names;
        }

        public List<string> GetItemDescriptions() {

            List<string> descriptions = new();

            foreach (var item in _displayInventory) {
                descriptions.Add(ItemParser.GetItemDescription(item));
            }
            return descriptions;
        }

        public List<string> GetItemPrices() {

            List<string> prices = new();

            foreach (var item in _displayInventory) {
                prices.Add(ItemParser.GetItemCost(item).ToString());
            }
            return prices;
        }
        #endregion

        #region Transactions

        /// <summary>
        /// Checks if the Player can afford a given item
        /// </summary>
        /// <param name="itemIndex">Index of the item</param>
        /// <param name="balance">Balance of the player</param>
        /// <param name="noItem">If no item was bought</param>
        /// <param name="price">The price of the item</param>
        /// <returns></returns>
        public bool CanAffordItem(int itemIndex, Currency balance, out bool noItem, out Currency price) {

            if (itemIndex >= _displayInventory.Count) {
                price = new();
                noItem = true;
                return false;
            }

            noItem = false;
            price = ItemParser.GetItemCost(_displayInventory[itemIndex]);

            if (balance >= price) {
                return true;
            }
            return false;
        }

        public BaseDecoratedAdventurer BuyItem(int itemIndex, Adventurer adventurer) {
            int itemId = _displayInventory[itemIndex];

            _fullInventory.Remove(itemId);

            return ItemParser.GetItem(itemId, adventurer);
        }
        #endregion
    }
}
