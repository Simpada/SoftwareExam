using SoftwareExam.CoreProgram;

namespace TestSoftwareExam
{
    public class Tests {

        private Currency c1;
        private Currency c2;

        [SetUp]
        public void Setup() {
            c1 = new Currency();
            c2 = new Currency();
        }

        //Addition and Subtraction
        #region
        // Currency1 | Currency2 | Expected
        [TestCase(1, 1, 1,   1, 1, 1,   2, 2, 2)]
        [TestCase(3, 3, 3,   3, 3, 3,   6, 6, 6)]
        [TestCase(0, 5, 0,   5, 0, 5,   5, 5, 5)]
        public void TestAddCurrency(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectSilver, int expectedGold)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectSilver, expectedGold);

            //Using methods
            Assert.That((c1.Add(c2)).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1.Add(c2)).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1.Add(c2)).Gold, Is.EqualTo( expectedCurrency.Gold ));

            //Using operator overloading
            Assert.That((c1 + c2).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1 + c2).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1 + c2).Gold, Is.EqualTo( expectedCurrency.Gold ));
        }


        [TestCase(1, 1, 1,   1, 1, 1,   0, 0, 0)]
        [TestCase(9, 6, 3,   3, 3, 3,   6, 3, 0)]
        [TestCase(5, 0, 5,   0, 0, 0,   5, 0, 5)]
        [TestCase(0, 3, 1,   0, 9, 0,   0, 4, 0)]
        public void TestSubtractCurrency(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectedSilver, int expectedGold)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectedSilver, expectedGold);

            //Using methods
            Assert.That((c1.Subtract(c2)).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1.Subtract(c2)).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1.Subtract(c2)).Gold, Is.EqualTo( expectedCurrency.Gold ));

            ////Using operator overloading
            Assert.That((c1 - c2).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1 - c2).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1 - c2).Gold, Is.EqualTo( expectedCurrency.Gold ));
        }
        #endregion


        //Currency Conversion
        #region
        [TestCase(5, 0, 0,   5, 0, 0,   0, 1, 0)]    // should convert 10 bronze to 1 silver
        [TestCase(0, 5, 0,   0, 5, 0,   0, 0, 1)]    // should convert 10 silver to 1 gold
        public void TestConversionOnAdd(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectedSilver, int expectedGold)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectedSilver, expectedGold);

            Assert.That((c1 + c2).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1 + c2).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1 + c2).Gold, Is.EqualTo(  expectedCurrency.Gold ));
        }


        [TestCase(0, 0, 1,   0, 1, 0,   0, 9, 0)]    // should reduce 1 gold and convert to 9 silver
        [TestCase(0, 1, 0,   1, 0, 0,   9, 0, 0)]    // should reduce 1 silver and convert to 9 bronze
        [TestCase(0, 0, 1,   5, 0, 0,   5, 9, 0)]    // should expect conversion from 1 gold -> 10 silver then 1 silver -> 10 copper
        public void TestConversionOnSubtract(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectedSilver, int expectedGold)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectedSilver, expectedGold);

            Assert.That((c1 - c2).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1 - c2).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1 - c2).Gold, Is.EqualTo( expectedCurrency.Gold ));
        }


        [Test]
        public void TestCopperConversionMethod()
        {
            c1 = new Currency(0, 1, 50);

            int totalInCopper = Currency.CheckPriceInCopper(c1);
            int expectedInCopper = 5010;

            Assert.That(totalInCopper, Is.EqualTo(expectedInCopper));
        }
        #endregion


        //Currency comparison
        #region
        // Balance | Price
        [TestCase(9, 9, 9,    5, 5, 5)]
        public void TestGreaterAndLesserThan(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            Assert.That(c1 > c2, Is.EqualTo(true));
            Assert.That(c1 < c2, Is.EqualTo(false));
        }

        [TestCase(5, 5, 5,   5, 5, 5)]
        public void TestEquals(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            Assert.That(c1 == c2, Is.EqualTo(true));
            Assert.That(c1 != c2, Is.EqualTo(false));
        }

        [TestCase(5, 5, 5,   5, 5, 5)]
        [TestCase(6, 6, 6,   5, 5, 5)]
        public void TestGreaterOrEqual(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            Assert.That(c1 >= c2, Is.EqualTo(true));
        }

        [TestCase(5, 5, 5,   5, 5, 5)]
        [TestCase(5, 5, 5,   7, 7, 7)]
        public void TestLesserOrEqual(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2)
        {
            c1 = new Currency(copper1, silver1, gold1);
            c2 = new Currency(copper2, silver2, gold2);

            Assert.That(c1 <= c2, Is.EqualTo(true));
        }
        #endregion

    }
}