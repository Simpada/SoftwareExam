using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;
using SoftwareExam.CoreProgram.Expedition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoftwareExam {
    internal class UnitTestMaps {

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestRecruitmentNotEnoughMoney(int difficulty) {

            Map map = Map.GetMap(difficulty);
            
            Assert.Multiple(() => {
                Assert.That(map.ExpeditionCost.Gold, Is.EqualTo(3 * difficulty));
                Assert.That(map.Difficulty, Is.EqualTo((Map.Difficulties)difficulty));
                Assert.That(map.Reward, Is.GreaterThan( new Currency (0,0,difficulty * 3)));
            });
        }

    }
}
