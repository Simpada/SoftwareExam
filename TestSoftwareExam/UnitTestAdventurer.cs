using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TestSoftwareExam {
    internal class UnitTestAdventurer {

        Adventurer _adventurer;


        [SetUp]
        public void SetUp() {
            _adventurer = new Warrior();
        }

        [Test]
        public void TestAddNewItem() {

            int initialHealth = _adventurer.Health;
            int initialDamage = _adventurer.Damage;
            int initialLuck = _adventurer.Luck;

            _adventurer = Adventurer.AddNewItem(new ArmorElvenRobe(_adventurer));
            

            Assert.Multiple(() => {
                Assert.That(_adventurer.Health, Is.EqualTo(initialHealth + 2));
                Assert.That(_adventurer.Damage, Is.EqualTo(initialDamage));
                Assert.That(_adventurer.Luck, Is.EqualTo(initialLuck + 1));
            });
        }
    }
}
