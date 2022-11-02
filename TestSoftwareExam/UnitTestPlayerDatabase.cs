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
            db.DropTable("players");
            db.CreateDb();
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

        [TestCase(1, "ASDasd", 5, 5, 100)]
        [TestCase(2, "sdfwe", 5, 5, 100)]
        [TestCase(3, "42g4g", 5, 5, 100)]
        public void TestRetriveAllPlayerNames(int id, string name, int copper, int silver, int gold)
        {
            Player player = new(id, name, new Currency(copper, silver, gold));
            db.Save(player);

            string[] playerNames = db.RetrieveAllPlayerNames();

            foreach (string playerName in playerNames) {
                Console.WriteLine("Player name: " + playerName);
            }

        }

    }
}
