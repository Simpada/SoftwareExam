using SoftwareExam.CoreProgram;
using SoftwareExam.DataBase;

namespace TestSoftwareExam
{
    public class UnitTestPlayerDatabase
    {
        public DataBaseAccess db;

        [SetUp]
        public void Setup()
        {
            db = new("Data Source = TestDb.db");
            db.ResetTable();
        }

        [Test]
        public void TestRetrievePlayer()
        {
            //Expected
            Currency balance = new Currency(5, 5, 500);
            Player tempPlayer = new Player(1, "sinna krigare", balance);
            db.Save(tempPlayer);

            Player playerFromDatabase = db.RetrieveById(1);
            Console.WriteLine("You have retrieved from database: " + playerFromDatabase.PlayerName);

            Assert.That(playerFromDatabase.PlayerName, Is.EqualTo(tempPlayer.PlayerName));
        }



    }
}
