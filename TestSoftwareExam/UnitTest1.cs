using SoftwareExam.CoreProgram;

namespace TestSoftwareExam
{

    public class Tests {

        [SetUp]
        public void Setup() {
        }

        [TestCase(1, 1, 1, 1, 1, 1, 2, 2, 2)]    // Addition
        [TestCase(3, 3, 3, 3, 3, 3, 6, 6, 6)]    // Addition
        [TestCase(0, 5, 0, 5, 0, 5, 5, 5, 5)]    // Addition
        public void TestAddCurrency(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectSilver, int expectedGold)
        {
            Currency c1 = new Currency(copper1, silver1, gold1);
            Currency c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectSilver, expectedGold);

            //Using methods
            Assert.That(expectedCurrency.Copper, Is.EqualTo( (c1.Add(c2)).Copper) );
            Assert.That(expectedCurrency.Silver, Is.EqualTo( (c1.Add(c2)).Silver) );
            Assert.That(expectedCurrency.Gold, Is.EqualTo( (c1.Add(c2)).Gold) );

            //Using operator overloading
            Assert.That(expectedCurrency.Copper, Is.EqualTo( c1.Copper + c2.Copper ));
            Assert.That(expectedCurrency.Silver, Is.EqualTo( c1.Silver + c2.Silver ));
            Assert.That(expectedCurrency.Gold, Is.EqualTo( c1.Silver + c2.Silver ));
        }


        [TestCase(1, 1, 1, 1, 1, 1, 0, 0, 0)]    // Subtraction
        [TestCase(9, 3, 9, 3, 9, 3, 6, 6, 6)]    // Subtraction
        [TestCase(5, 0, 5, 0, 5, 0, 5, 5, 5)]    // Subtraction
        public void TestSubtractCurrency(int copper1, int silver1, int gold1, int copper2, int silver2, int gold2, int expectedCopper, int expectedSilver, int expectedGold)
        {
            Currency c1 = new Currency(copper1, silver1, gold1);
            Currency c2 = new Currency(copper2, silver2, gold2);

            //Expected
            Currency expectedCurrency = new Currency(expectedCopper, expectedSilver, expectedGold);

            //Using methods
            Assert.That(expectedCurrency.Copper, Is.EqualTo( (c1.Subtract(c2)).Copper) );
            Assert.That(expectedCurrency.Silver, Is.EqualTo( (c1.Subtract(c2)).Silver) );
            Assert.That(expectedCurrency.Gold, Is.EqualTo( (c1.Subtract(c2)).Gold) );

            //Using operator overloading
            Assert.That(expectedCurrency.Copper, Is.EqualTo( c1.Copper + c2.Copper ));
            Assert.That(expectedCurrency.Silver, Is.EqualTo( c1.Silver + c2.Silver ));
            Assert.That(expectedCurrency.Gold, Is.EqualTo( c1.Silver + c2.Silver ));
        }







    }
}