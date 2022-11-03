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

        private void Prepare(string connection) {

            string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, connection);

            try {
                if (File.Exists(databasePath)) {
                    File.Delete(databasePath);
                }
            } catch (IOException e) { }

            databaseAccess = new("Data Source = " + connection);
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

        [TestCase(1, "one", 5, 5, 100)]
        [TestCase(2, "two", 5, 5, 100)]
        [TestCase(3, "three", 5, 5, 100)]
        public void TestRetriveAllPlayerNames(int id, string name, int copper, int silver, int gold)
        {
            Prepare("Data Source = testDatabase2.db");

            Player player = new(id, name, new Currency(copper, silver, gold));
            databaseAccess.Save(player);

            string[] playerNames = databaseAccess.RetrieveAllPlayerNames();

            foreach (string playerName in playerNames) {
                Console.WriteLine("Player name: " + playerName);
            }

            Assert.That(playerNames, Does.Contain(name));
        }

    }
}
