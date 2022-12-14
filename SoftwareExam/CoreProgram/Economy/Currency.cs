namespace SoftwareExam.CoreProgram.Economy {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// A class that defines a special type of money system, with comparable, converters, and operator overloaders
    /// </summary>
    public class Currency : IComparable {

        private int _gold;
        private int _silver;
        private int _copper;

        public Currency(int copper, int silver, int gold) {
            _copper = copper;
            _silver = silver;
            _gold = gold;
        }

        public Currency() {
            _gold = 0;
            _silver = 0;
            _copper = 0;
        }

        #region Currency conversion

        public Currency Add(Currency currency) {
            int tempCopper = _copper + currency._copper;
            int tempSilver = _silver + currency._silver;
            int tempGold = _gold + currency._gold;

            return Convert(new Currency(tempCopper, tempSilver, tempGold));
        }

        public Currency Subtract(Currency currency) {
            int tempCopper = _copper - currency._copper;
            int tempSilver = _silver - currency._silver;
            int tempGold = _gold - currency._gold;

            return Convert(new Currency(tempCopper, tempSilver, tempGold));
        }

        public Currency Multiply(double multiplier) {

            int tempGold = (int)(_gold * multiplier);
            int tempSilver = (int)(_silver * multiplier);
            int tempCopper = (int)(_copper * multiplier);

            return Convert(new Currency(tempCopper, tempSilver, tempGold));
        }

        public static Currency Convert(Currency currency) {
            while (currency._copper >= 10) {
                currency._silver++;
                currency._copper -= 10;
            }
            while (currency._silver >= 10) {
                currency._gold++;
                currency._silver -= 10;
            }

            while (currency._copper < 0) {
                currency._silver--;
                currency._copper += 10;
            }

            while (currency._silver < 0) {
                currency._gold--;
                currency._silver += 10;
            }

            //Should not be possible to have negative money
            if (currency._gold < 0) {
                currency._gold = 0;
            }
            return currency;
        }
        #endregion

        #region Convertions and Comparisons
        //Converts all to copper to check price.
        public static int CheckPriceInCopper(Currency currency) {
            return currency._copper + currency._silver * 10 + currency._gold * 100;
        }

        //Currency checks
        public int CompareTo(Currency currency) {
            int price = CheckPriceInCopper(currency);
            int balance = CheckPriceInCopper(this);

            if (balance > price) {
                return 1;
            } else if (balance < price) {
                return -1;
            }
            return 0;
        }

        public int CompareTo(object? obj) {
            if (obj is Currency currency) {
                return CompareTo(currency);
            }
            throw new InvalidCastException("Not a currency object");
        }

        public override bool Equals(object? obj) {
            if (obj is Currency currency) {
                return CompareTo(currency) == 0;
            }
            throw new InvalidCastException("Not a currency object");
        }

        public override string ToString() {
            return $"GP: {_gold}, SP: {_silver}, CP: {_copper}";
        }
        #endregion

        #region Operator overloading

        public static Currency operator +(Currency currency1, Currency currency2) {
            return currency1.Add(currency2);
        }

        public static Currency operator -(Currency currency1, Currency currency2) {
            return currency1.Subtract(currency2);
        }

        public static Currency operator *(Currency currency, double multiplier) {
            return currency.Multiply(multiplier);
        }

        public static bool operator >(Currency currency1, Currency currency2) {
            return currency1.CompareTo(currency2) == 1;
        }

        public static bool operator <(Currency currency1, Currency currency2) {
            return currency1.CompareTo(currency2) == -1;
        }

        public static bool operator >=(Currency currency1, Currency currency2) {
            return currency1.CompareTo(currency2) >= 0;
        }

        public static bool operator <=(Currency currency1, Currency currency2) {
            return currency1.CompareTo(currency2) <= 0;
        }

        public static bool operator ==(Currency currency1, Currency currency2) {
            return currency1.Equals(currency2);
        }

        public static bool operator !=(Currency currency1, Currency currency2) {
            return !currency1.Equals(currency2);
        }
        #endregion

        #region Properties

        public int Gold {
            get {
                return _gold;
            }
            set {
                if (value < 0) {
                    throw new Exception("Invalid, gold must be larger than 0");
                }
            }
        }
        public int Silver {
            get {
                return _silver;
            }
            set {
                if (value < 0) {
                    throw new Exception("Invalid, silver must be larger than 0");
                }
            }
        }
        public int Copper {
            get {
                return _copper;
            }
            set {
                if (value < 0) {
                    throw new Exception("Invalid, bronze must be larger than 0");
                }
            }
        }
        #endregion
    }
}
