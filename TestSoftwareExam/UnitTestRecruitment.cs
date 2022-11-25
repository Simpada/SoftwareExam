using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;

namespace TestSoftwareExam {
    internal class UnitTestRecruitment {

        private Recruitment _recruitment;

        [OneTimeSetUp]
        public void InitialSetUp() {
            _recruitment = new Recruitment();
        }

        [TestCase(1, "Warrior", 10, 5, 5)]
        [TestCase(2, "Mage", 5, 10, 5)]
        [TestCase(3, "Rogue", 5, 5, 10)]
        public void TestRecruitment(int type, string expectedClass, int health, int damage, int luck) {
            Adventurer? adventurer = _recruitment.RecruitAdventurer(type, new(10, 10, 10));

            Assert.That(adventurer, Is.Not.Null);

            Assert.Multiple(() => {
                Assert.That(adventurer.Class, Is.EqualTo(expectedClass));
                Assert.That(adventurer.Health, Is.EqualTo(health));
                Assert.That(adventurer.Damage, Is.EqualTo(damage));
                Assert.That(adventurer.Luck, Is.EqualTo(luck));
            });
        }

        [Test]
        public void TestRecruitmentNotEnoughMoney() {
            Adventurer? adventurer = _recruitment.RecruitAdventurer(1, new(0, 5, 0));

            Assert.That(adventurer, Is.Null);
        }
    }
}
