using SoftwareExam.CoreProgram;
using SoftwareExam.DataBase;

namespace TestSoftwareExam
{
    public class UnitTestPlayerDatabase
    {
        // Not true
        private DataBaseAccess databaseAccess;

        [SetUp]
        public void Setup()
        {
            //Ask about SetUp. Does not run on 2nd test????????? Lock?

            //string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, "testDatabase.db");

            //try {
            //    if (File.Exists(databasePath)) {
            //        File.Delete(databasePath);
            //    }
            //}
            //catch (IOException e) {}

            //databaseAccess = new("Data Source = testDatabase.db");
        }

        private void Prepare(string db) {

            string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, db);

            try {
                if (File.Exists(databasePath)) {
                    File.Delete(databasePath);
                }
            } catch (IOException) { }

            databaseAccess = new("Data Source = " + db);

        }

        [Test]
        public void TestRetrievePlayer()
        {
            Prepare("testDatabase1.db");

            //Expected
            Player tempPlayer = new Player(1, "Den Sinna krigaren", new Currency(5, 5, 500));
            databaseAccess.Save(tempPlayer);

            //Actual
            databaseAccess.GetPlayerById(1, out int playerId, out string playerName, out int copper, out int silver, out int gold);
            Player playerFromDatabase = new(playerId, playerName, new Currency(copper, silver, gold));


            Console.WriteLine("You have retrieved from database: " + playerFromDatabase.PlayerName);
            Assert.That(playerFromDatabase.PlayerName, Is.EqualTo(tempPlayer.PlayerName));
        }

        [Test]
        public void TestRetriveAllPlayerNames()
        {
            Prepare("testDatabase2.db");


            Player player1 = new(1, "one", new Currency(5, 5, 100));
            Player player2 = new(2, "two", new Currency(5, 5, 100));
            Player player3 = new(3, "three", new Currency(5, 5, 100));
            databaseAccess.Save(player1);
            databaseAccess.Save(player2);
            databaseAccess.Save(player3);

            string[] playerNames = databaseAccess.RetrieveAllPlayerNames();

            foreach (string playerName in playerNames) {
                Console.WriteLine("Player name: " + playerName);
            }

            Assert.That(playerNames, Does.Contain("one"));
            Assert.That(playerNames, Does.Contain("two"));
            Assert.That(playerNames, Does.Contain("three"));
        }

    }
}
