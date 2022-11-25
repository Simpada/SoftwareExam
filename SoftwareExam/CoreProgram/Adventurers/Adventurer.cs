using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers {
    /// <summary>
    /// This class sets how an adventurer functions
    /// </summary>
    public abstract class Adventurer {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Class { get; set; } = "";

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Luck { get; set; }

        public List<BaseDecoratedAdventurer> Equipment { get; set; } = new();
        public Currency Value { get; set; } = new Currency(0, 0, 5);
        public bool OnMission { get; set; } = false;
        public string[] SymbolArray = new string[6];

        private readonly Random _random = new();

        public Adventurer() {
            Name = RandomName();
        }

        #region Setting up a new Adventurer

        private string RandomName() {

            string name = PickOne(
                new string[] {
                    "Bernie",
                    "Charles",
                    "Clara",
                    "Frida",
                    "Ken",
                    "Percy",
                    "William",
                    "Peter",
                    "Argdraxadil",
                    "Bert",
                    "Tim",
                    "Samuel",
                    "Lyla",
                    "Frey",
                    "Samantha",
                    "Svetlana",
                    "Sergei",
                    "Karl",
                    "Pell",
                    "Gardax",
                    "Goth'Mog",
                    "Sauron",
                    "Geralt",
                    "Mr. Samson",
                    "Kargath"
                });

            string title = PickOne(
                new string[] {
                    "Dumb",
                    "Hairy",
                    "Incomprehensible",
                    "Mass Murderer",
                    "Obnoxious",
                    "Rude",
                    "Wanderer",
                    "Enchanter",
                    "Beutiful",
                    "Warrior",
                    "Rogue",
                    "Mage",
                    "Unsuccessful",
                    "Drunk",
                    "Tall",
                    "Short",
                    "Sophisticated"
                });

            return $"{name} the {title}";
        }

        private string PickOne(string[] alternatives) {
            return alternatives[_random.Next(alternatives.Length)];
        }

        /// <summary>
        /// Gets starting gear, then equips them to the new adventurer
        /// </summary>
        /// <returns>An adventurer with starting gear</returns>
        public Adventurer GetStartingGear() {

            BaseDecoratedAdventurer Hat = new BasicHat(this);
            BaseDecoratedAdventurer Armor = new BasicArmor(Hat);
            BaseDecoratedAdventurer Weapon = new BasicWeapon(Armor);
            BaseDecoratedAdventurer OffHand = new BasicOffHand(Weapon);
            BaseDecoratedAdventurer Trinket = new BasicTrinket(OffHand);

            Equipment.Add(Hat);
            Equipment.Add(Armor);
            Equipment.Add(Weapon);
            Equipment.Add(OffHand);
            Equipment.Add(Trinket);

            return Adventurer.EquipGear(this);
        }
        #endregion

        #region To String Methods for Different states

        /// <summary>
        /// Creates an adventurer card, detailing its stats
        /// </summary>
        /// <returns>A string containing the adventurer card</returns>
        public override string ToString() {
            return @$"    |{SymbolArray[0]} 
    |{SymbolArray[1]}Name:   {Name}
    |{SymbolArray[2]}Class:  {Class}
    |{SymbolArray[3]}Health: {Health}
    |{SymbolArray[4]}Damage: {Damage}
    |{SymbolArray[5]}Luck:   {Luck}";
        }

        /// <summary>
        /// Gets a card, but with item stats
        /// </summary>
        /// <returns>A string with the adventurer card plus item descriptions</returns>
        public string GetItemCard() {

            string HatName = "";
            string Hat = "";
            string ArmorName = "";
            string Armor = "";
            string WeaponName = "";
            string Weapon = "";
            string OffHandName = "";
            string OffHand = "";
            string TrinketName = "";
            string Trinket = "";

            foreach (var item in Equipment) {

                int itemId = GetItemType(item.ItemId);

                switch (itemId) {
                    case 1:
                    ArmorName = item.GetEquipmentName();
                    Armor = item.GetEquipmentDescription();
                    break;
                    case 2:
                    HatName = item.GetEquipmentName();
                    Hat = item.GetEquipmentDescription();
                    break;
                    case 3:
                    OffHandName = item.GetEquipmentName();
                    OffHand = item.GetEquipmentDescription();
                    break;
                    case 4:
                    TrinketName = item.GetEquipmentName();
                    Trinket = item.GetEquipmentDescription();
                    break;
                    case 5:
                    WeaponName = item.GetEquipmentName();
                    Weapon = item.GetEquipmentDescription();
                    break;
                }
            }

            return @$"{ToString()}
    |
    |___EQUIPMENT_____________________________
    |   Hat:      {HatName}
    |             {Hat}
    |-----------------------------------------
    |   Armor:    {ArmorName}
    |             {Armor}
    |-----------------------------------------
    |   Weapon:   {WeaponName}
    |             {Weapon}
    |-----------------------------------------
    |   Off-Hand: {OffHandName}
    |             {OffHand}
    |-----------------------------------------
    |   Trinket:  {TrinketName}
    |             {Trinket}
    |";
        }

        public string GetAvailability(int index) {

            string availability = "";

            if (OnMission) {
                availability += "    |           ON A MISSION\n";
            } else {
                availability += $"    |       [{index}] CHOOSE ADVENTURER\n";
            }
            availability += ToString();
            availability += "\n    |-----------------------------------------\n";

            return availability;
        }
        #endregion

        public abstract string GetEquipmentDescription();
        public abstract string GetEquipmentName();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        #region Equipping and Changing Gear

        /// <summary>
        /// Removes all gear from an adventurer, and equips its current list of items
        /// </summary>
        /// <param name="oldAdventurer">The outdated adventurer with incorrect gear</param>
        /// <returns>A new adventurer with correct gear</returns>
        public static Adventurer EquipGear(Adventurer oldAdventurer) {

            Adventurer newAdventurer = FindBase(oldAdventurer.Equipment[^1]);

            List<BaseDecoratedAdventurer> gear = newAdventurer.Equipment;

            foreach (var Item in oldAdventurer.Equipment) {
                Item.BaseAdventurer = newAdventurer;
                Item.InheritStats(newAdventurer);
                Item.EditStats();
                newAdventurer = Item;
            }

            newAdventurer.Equipment = gear;

            return newAdventurer;
        }

        /// <summary>
        /// Recursive method that finds the core adventurer, one without any items or decorators
        /// </summary>
        /// <param name="_parentAdventurer">The adventurer to check if it no longer has decorators</param>
        /// <returns>The adventurer without any decorators</returns>
        public static Adventurer FindBase(BaseDecoratedAdventurer _parentAdventurer) {

            if (_parentAdventurer.BaseAdventurer is BaseDecoratedAdventurer _decoratedAdventurer) {
                return FindBase(_decoratedAdventurer);
            } else {
                return _parentAdventurer.BaseAdventurer;
            }
        }

        /// <summary>
        /// Makes sure that stat increases are kept as items are equipped
        /// </summary>
        /// <param name="_baseAdventurer"></param>
        public void InheritStats(Adventurer _baseAdventurer) {

            Health = _baseAdventurer.Health;
            Damage = _baseAdventurer.Damage;
            Luck = _baseAdventurer.Luck;

        }

        /// <summary>
        /// Adds a new item to an adventurer, then re-equips them
        /// </summary>
        /// <param name="newItem">The new item to add</param>
        /// <returns>The adventurer with its new item</returns>
        public static Adventurer AddNewItem(BaseDecoratedAdventurer newItem) {

            Adventurer changingAdventurer = newItem.BaseAdventurer;

            int itemType = GetItemType(newItem.ItemId);

            foreach (BaseDecoratedAdventurer OldItem in changingAdventurer.Equipment) {

                int equippedItemType = GetItemType(OldItem.ItemId);

                if (itemType == equippedItemType) {
                    changingAdventurer.Equipment.Remove(OldItem);
                    break;
                }
            }
            changingAdventurer.Equipment.Add(newItem);

            return Adventurer.EquipGear(changingAdventurer);
        }

        // Parses the id of the item down to its type
        private static int GetItemType(int itemId) {
            if (itemId >= 1000) itemId /= 1000;
            if (itemId >= 100) itemId /= 100;
            if (itemId >= 10) itemId /= 10;
            return itemId;
        }
        #endregion
    }
}
