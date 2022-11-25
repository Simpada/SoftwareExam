using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoftwareExam
{
    internal class UnitTestRecruitment {

        private Recruitment Recruitment;

        [OneTimeSetUp]
        public void InitialSetUp() {
            Recruitment = new Recruitment();
        }

        [TestCase(1, "Warrior", 10, 5, 5)]
        [TestCase(2, "Mage", 5, 10, 5)]
        [TestCase(3, "Rogue", 5, 5, 10)]
        public void TestFactory(int type, string expectedClass, int health, int damage, int luck) {
            Adventurer? adventurer = Recruitment.RecruitAdventurer(type, new(10, 10, 10));

            Assert.That(adventurer, Is.Not.Null);

            Assert.Multiple(() => {
                Assert.That(adventurer.Class, Is.EqualTo(expectedClass));
                Assert.That(adventurer.Health, Is.EqualTo(health));
                Assert.That(adventurer.Damage, Is.EqualTo(damage));
                Assert.That(adventurer.Luck, Is.EqualTo(luck));
            });
        }
    }
}
