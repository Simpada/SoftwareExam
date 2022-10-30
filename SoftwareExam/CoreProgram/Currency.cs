using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {

    internal class Currency : IComparable{

        private int _gold;
        private int _silver;
        private int _copper;

        public Currency(int gold, int silver, int copper)
        {
            _gold = gold;
            _silver = silver;
            _copper = copper;
        }

        public Currency()
        {
            _gold = 0;
            _silver = 0;
            _copper = 0;
        }

        //Currency conversion
        #region
        public Currency Add(Currency currency)
        {
            int tempGold = _gold + currency._gold;
            int tempSilver = _silver + currency._silver;
            int tempCopper = _copper + currency._copper;

            return Convert(new Currency(tempGold, tempSilver, tempCopper));
        }

        public Currency Subtract(Currency currency)
        {
            int tempGold = _gold - currency._gold;
            int tempSilver = _silver - currency._silver;
            int tempCopper = _copper - currency._copper;

            return Convert(new Currency(tempGold, tempSilver, tempCopper));
        }

        public Currency Convert(Currency currency)
        {
            while (currency._copper >= 10) {
                currency._silver++;
                currency._copper -= 10;
            }
            while (_silver >= 10) {
                currency._gold++;
                currency._silver -= 10;
            }

            while (_copper < 0) {
                currency._silver--;
                currency._copper += 10;
            }

            while (_silver < 0) {
                currency._gold--;
                currency._silver += 10;
            }
            return currency;
        }
        #endregion

        //Converts all to copper to check price.
        private int CheckPriceInCopper(Currency currency)
        {
            return currency._copper + (currency._silver * 10) + (currency._gold * 100);
        }

        //Currency checks
        public int CompareTo(Currency currency)
        {
            int price = CheckPriceInCopper(currency);
            int balance = CheckPriceInCopper(this);

            if (balance > price) {
                return 1;
            } else if (balance < price) {
                return -1;
            }
            return 0;
        }

        public int CompareTo(object? obj)
        {
            if (obj is Currency currency) {
                return CompareTo(currency);
            }
            throw new InvalidCastException("Not a currency object");
        }


        //Operator overloading
        #region
        public static Currency operator +(Currency currency1, Currency currency2)
        {
            return currency1.Add(currency2);
        }

        public static Currency operator -(Currency currency1, Currency currency2)
        {
            return currency1.Subtract(currency2);
        }

        public static bool operator >(Currency currency1, Currency currency2)
        {
            return currency1.CompareTo(currency2) == 1;
        }
        
        public static bool operator <(Currency currency1, Currency currency2)
        {
            return currency1.CompareTo(currency2) == -1;
        }

        public static bool operator >=(Currency currency1, Currency currency2)
        {
            return currency1.CompareTo(currency2) >= 0;
        }

        public static bool operator <=(Currency currency1, Currency currency2)
        {
            return currency1.CompareTo(currency2) <= 0;
        }
        #endregion

        //Properties
        #region
        public int Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                if (value < 0) {
                    throw new Exception("Invalid, gold must be larger than 0");
                }
            }
        }
        public int Silver
        {
            get
            {
                return _silver;
            }
            set
            {
                if (value < 0) {
                    throw new Exception("Invalid, silver must be larger than 0");
                }
            }
        }
        public int Copper
        {
            get
            {
                return _copper;
            }
            set
            {
                if (value < 0) {
                    throw new Exception("Invalid, bronze must be larger than 0");
                }
            }
        }
        #endregion
    }
}
