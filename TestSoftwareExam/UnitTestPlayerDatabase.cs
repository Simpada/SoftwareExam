using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
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
        // Not true, it is never null
        private DataBaseAccess _databaseAccess;


        private void Prepare(string db)
        {
            string databasePath = Path.GetRelativePath(Environment.CurrentDirectory, db);

            try {
                if (File.Exists(databasePath)) {
                    File.Delete(databasePath);
                }
            }
            catch (IOException) { }

            _databaseAccess = new("Data Source = " + db);
        }

        [Test]
        public void TestRetriveAllPlayerNames()
        {
            Prepare("testDatabase1.db");

            Player player1 = new()
            {
                Id = 1,
                PlayerName = "one"
            };
            Player player2 = new()
            {
                Id = 2,
                PlayerName = "two"
            };
            Player player3 = new()
            {
                Id = 3,
                PlayerName = "three"
            };
            _databaseAccess.SaveGame(player1);
            _databaseAccess.SaveGame(player2);
            _databaseAccess.SaveGame(player3);

            string[] playerNames = _databaseAccess.RetrieveAllPlayerNames();

            foreach (string playerName in playerNames) {
                Console.WriteLine("Player name: " + playerName);
            }

            Assert.That(playerNames, Does.Contain("one"));
            Assert.That(playerNames, Does.Contain("two"));
            Assert.That(playerNames, Does.Contain("three"));
        }

        [Test]
        public void TestRetrievePlayerCurrency()
        {
            Prepare("testDatabase2");

            //Create a player
            Player player1 = new Player()
            {
                Id = 1,
                PlayerName = "Test"
            };
            player1.SetCurrency(5, 5, 5);

            Assert.That(player1.Balance.Copper == 5, Is.True);
            Assert.That(player1.Balance.Silver == 5, Is.True);
            Assert.That(player1.Balance.Gold == 5, Is.True);


            _databaseAccess.SaveGame(player1);
            Player loadPlayer1 = _databaseAccess.Load(player1.Id);

            Assert.That(player1.PlayerName, Is.EqualTo(loadPlayer1.PlayerName));
            Assert.That(player1.Balance.Copper, Is.EqualTo(loadPlayer1.Balance.Copper));
            Assert.That(player1.Balance.Silver, Is.EqualTo(loadPlayer1.Balance.Silver));
            Assert.That(player1.Balance.Gold, Is.EqualTo(loadPlayer1.Balance.Gold));
        }

        [Test]
        public void TestRetrieveAdventurers()
        {
            Prepare("testDatabase3");

            //Add adventurers to player
            Adventurer warrior = new Warrior().GetStartingGear();
            Adventurer rogue = new Rogue().GetStartingGear();
            Adventurer mage = new Mage().GetStartingGear();

            Player player1 = new()
            {
                Id = 1,
                PlayerName = "TestAdventureres"
            };

            player1.Adventurers.Add(warrior);
            player1.Adventurers.Add(rogue);
            player1.Adventurers.Add(mage);


            _databaseAccess.SaveGame(player1);
            Player player2 = _databaseAccess.Load(player1.Id);
            player2.Adventurers = _databaseAccess.GetAdventurers(player2.Id);

            Assert.That(player2.Adventurers.Count, Is.EqualTo(player1.Adventurers.Count));
            Assert.That(player2.Adventurers[0].Name, Is.EqualTo(player1.Adventurers[0].Name));
            Assert.That(player2.Adventurers[1].Name, Is.EqualTo(player1.Adventurers[1].Name));
            Assert.That(player2.Adventurers[2].Name, Is.EqualTo(player1.Adventurers[2].Name));
        }

    }
}
