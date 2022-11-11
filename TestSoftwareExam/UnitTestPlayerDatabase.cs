using SoftwareExam.CoreProgram;
using SoftwareExam.DataBase;

/*
 * DB gets locked between tests, so could not use the same database for testing.
 * Could not use [TestCase()] because of GitHub Actions.
 * 
 */

namespace TestSoftwareExam
{
    public class UnitTestPlayerDatabase
    {
        // Not true
        private DataBaseAccess DatabaseAccess;


        private void Prepare(string db) {
            string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, db);

            try {
                if (File.Exists(databasePath)) {
                    File.Delete(databasePath);
                }
            } catch (IOException) { }

            DatabaseAccess = new("Data Source = " + db);
        }

        [Test]
        public void TestRetrievePlayer()
        {
            Prepare("testDatabase1.db");

            //Expected
            Player tempPlayer = new Player(1, "Den Sinna krigaren", new Currency(5, 5, 500));
            DatabaseAccess.Save(tempPlayer);

            //Actual
            DatabaseAccess.GetPlayerById(1, out int playerId, out string playerName, out int copper, out int silver, out int gold);
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
            DatabaseAccess.Save(player1);
            DatabaseAccess.Save(player2);
            DatabaseAccess.Save(player3);

            string[] playerNames = DatabaseAccess.RetrieveAllPlayerNames();

            foreach (string playerName in playerNames) {
                Console.WriteLine("Player name: " + playerName);
            }

            Assert.That(playerNames, Does.Contain("one"));
            Assert.That(playerNames, Does.Contain("two"));
            Assert.That(playerNames, Does.Contain("three"));
        }

        [Test]
        public void TestOverwriteSave()
        {
            Prepare("testDatabase3.db");

            Player originalPlayer = new(1, "Original", new Currency(1, 1, 1));
            Player newPlayer = new(1, "New player", new Currency(1, 1, 1));

            DatabaseAccess.Save(originalPlayer);
            DatabaseAccess.Save(newPlayer);

            Player playerResult = new();
            playerResult.PlayerName = DatabaseAccess.GetPlayernameById(1);

            Assert.That(newPlayer.PlayerName, Is.EqualTo(playerResult.PlayerName));
        }
    }
}
