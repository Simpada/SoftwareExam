using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram {

    internal class Currency {

        private int _gold;
        private int _silver;
        private int _copper;


        public void Add(Currency currency)
        {
            _gold += currency._gold;
            _silver += currency._silver;
            _copper += currency._copper;

            Convert();
        }


        public void Substract(Currency currency)
        {
            _gold -= currency._gold;
            _silver -= currency._silver;
            _copper -= currency._copper;

            Convert();
        }

        public void Convert()
        {
            while (_copper >= 10) {
                _silver++;
                _copper -= 10;
            }
            while (_silver >= 10) {
                _gold++;
                _silver -= 10;
            }

            while (_copper < 0) {
                _silver--;
                _copper += 10;
            }

            while (_silver < 0) {
                _gold--;
                _silver += 10;
            }

        }

        //Converts all to copper to check price.
        private int CheckPriceInCopper(Currency currency)
        {
            return currency._copper + (currency._silver * 10) + (currency._gold * 100);
        }


        public bool GreaterThan(Currency currency)
        {
            int price = CheckPriceInCopper(currency);
            int balance = CheckPriceInCopper(this);

            if (price > balance) {
                return false;
            }
            return true;
        }

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
                    throw new Exception("");
                }
            }
        }
    }
}
