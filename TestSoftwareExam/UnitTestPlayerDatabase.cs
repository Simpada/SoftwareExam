using SoftwareExam.CoreProgram;
using SoftwareExam.DataBase;

namespace TestSoftwareExam
{
    public class UnitTestPlayerDatabase
    {
        public DataBaseAccess databaseAccess;

        [SetUp]
        public void Setup()
        {
            //Ask about SetUp. Does not run on 2nd test????????? Lock?
            string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, "testDatabase.db");

            try {
                if (File.Exists(databasePath)) {
                    File.Delete(databasePath);
                }
            }
            catch (IOException e) {}

            databaseAccess = new("Data Source = testDatabase.db");
        }

        [Test]
        public void TestRetrievePlayer()
        {

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
            string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, "testDatabase2.db");

            try {
                if (File.Exists(databasePath)) {
                    File.Delete(databasePath);
                }
            } catch (IOException e) { }

            DataBaseAccess databaseAccess2 = new("Data Source = testDatabase2.db");


            Player player1 = new(1, "one", new Currency(5, 5, 100));
            Player player2 = new(2, "two", new Currency(5, 5, 100));
            Player player3 = new(3, "three", new Currency(5, 5, 100));
            databaseAccess2.Save(player1);
            databaseAccess2.Save(player2);
            databaseAccess2.Save(player3);

            string[] playerNames = databaseAccess2.RetrieveAllPlayerNames();

            foreach (string playerName in playerNames) {
                Console.WriteLine("Player name: " + playerName);
            }

            Assert.That(playerNames, Does.Contain("one"));
            Assert.That(playerNames, Does.Contain("two"));
            Assert.That(playerNames, Does.Contain("three"));
        }

    }
}
