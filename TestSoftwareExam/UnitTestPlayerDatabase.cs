using SoftwareExam.CoreProgram;
using SoftwareExam.DataBase;

namespace TestSoftwareExam
{
    public class UnitTestPlayerDatabase
    {

        [SetUp]
        public void Setup()
        {
            DataBaseAccess db = new();
        }

        [Test]
        public void TestRetrievePlayer()
        {
            Player player = new();
            
        }





    }
}
