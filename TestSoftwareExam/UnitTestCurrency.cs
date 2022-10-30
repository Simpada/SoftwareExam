using SoftwareExam.CoreProgram;

namespace TestSoftwareExam
{
    public class Tests {

        [SetUp]
        public void Setup() {
        }

        // Currency1 | Currency2 | Expected
        [TestCase(1, 1, 1,   1, 1, 1,   2, 2, 2)]
        [TestCase(3, 3, 3,   3, 3, 3,   6, 6, 6)]
        [TestCase(0, 5, 0,   5, 0, 5,   5, 5, 5)]
        public void TestAddCurrency(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectSilver, int expectedGold)
        {
            Currency c1 = new Currency(copper1, silver1, gold1);
            Currency c2 = new Currency(copper2, silver2, gold2);

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
            Currency c1 = new Currency(copper1, silver1, gold1);
            Currency c2 = new Currency(copper2, silver2, gold2);

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


        [TestCase(5, 0, 0,   5, 0, 0,   0, 1, 0)]    // should convert 10 bronze to 1 silver
        [TestCase(0, 5, 0,   0, 5, 0,   0, 0, 1)]    // should convert 10 silver to 1 gold
        public void TestConversionOnAdd(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectedSilver, int expectedGold)
        {
            Currency c1 = new Currency(copper1, silver1, gold1);
            Currency c2 = new Currency(copper2, silver2, gold2);

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
            Currency c1 = new Currency(copper1, silver1, gold1);
            Currency c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectedSilver, expectedGold);

            Assert.That((c1 - c2).Copper, Is.EqualTo( expectedCurrency.Copper ));
            Assert.That((c1 - c2).Silver, Is.EqualTo( expectedCurrency.Silver ));
            Assert.That((c1 - c2).Gold, Is.EqualTo( expectedCurrency.Gold ));
        }




    }
}