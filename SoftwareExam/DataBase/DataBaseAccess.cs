using Microsoft.Data.Sqlite;
using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers.Factory;
using SoftwareExam.CoreProgram.Expedition;

/**
 * SQLite AdoNet
 * Should take id, playerName, balance for now.
 *
 */

namespace SoftwareExam.DataBase {

    public class DataBaseAccess {

        //private InitializeDatabase InitDb;
        private readonly string DataSource = "";

        public DataBaseAccess(string dataSource) {
            using (SqliteConnection connection = new(dataSource)) {
                connection.Open();
                _ = new InitializeDatabase(dataSource);
                DataSource = dataSource;
            }
        }

        public void Save(Player player) {
            if (CheckIfPlayerExists(player.Id)) {
                Delete(player.Id);
            }
            Add(player);
        }

        //Used to check if there is a player for overwriting a saved game.
        public bool CheckIfPlayerExists(int id) {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT player_name
                FROM players
                WHERE player_id = @id
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                return true;
            } else {
                return false;
            }
        }

        //Should actually not access player - best way to return info?
        public void Add(Player player) {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO players (player_id, player_name, copper, silver, gold)
                VALUES (@playerId, @playerName, @copper, @silver, @gold)
            ";
            command.Parameters.AddWithValue("@playerId", player.Id);
            command.Parameters.AddWithValue("@playerName", player.PlayerName);
            command.Parameters.AddWithValue("@copper", player.Balance.Copper);
            command.Parameters.AddWithValue("@silver", player.Balance.Silver);
            command.Parameters.AddWithValue("@gold", player.Balance.Gold);
            command.ExecuteNonQuery();


            //Save player logs
            for (int i = 0; i < player.Log.Count; i++) {
                using SqliteCommand logsCommand = connection.CreateCommand();
                logsCommand.CommandText = @"
                    INSERT INTO logs (log_entry, player_id)
                    VALUES (@logEntry, @playerId)
                    ";
                logsCommand.Parameters.AddWithValue("@logEntry", player.Log[i]);
                logsCommand.Parameters.AddWithValue("@playerId", player.Id);
                logsCommand.ExecuteNonQuery();
            }


            for (int i = 0; i < player.Adventurers.Count; i++) {
                using SqliteCommand adventurerCommand = connection.CreateCommand();

                // This is often risky, but we know that all adventurers in the list are always BaseDecoratedAdventurers
                Adventurer adventurer = Adventurer.FindBase((BaseDecoratedAdventurer)player.Adventurers[i]);

                adventurerCommand.CommandText = @"
                        INSERT INTO adventurers (adventurer_name, class, health, damage, luck, player_id)
                        VALUES (@adventurerName, @class, @health, @damage, @luck, @playerId)
                    ";
                adventurerCommand.Parameters.AddWithValue("@adventurerName", adventurer.Name);
                adventurerCommand.Parameters.AddWithValue("@class", adventurer.Class);
                adventurerCommand.Parameters.AddWithValue("@health", adventurer.Health);
                adventurerCommand.Parameters.AddWithValue("@damage", adventurer.Damage);
                adventurerCommand.Parameters.AddWithValue("@luck", adventurer.Luck);
                adventurerCommand.Parameters.AddWithValue("@playerId", player.Id);
                adventurerCommand.ExecuteNonQuery();


                using SqliteCommand getIdCommand = connection.CreateCommand();
                getIdCommand.CommandText = "SELECT MAX(adventurer_id) FROM adventurers";
                getIdCommand.ExecuteNonQuery();
                using SqliteDataReader reader = getIdCommand.ExecuteReader();
                int id = 0;
                if (reader.Read()) {
                    id = reader.GetInt32(0);
                }
                player.Adventurers[i].Id = id;


                for (var j = 0; j < 5; j++) {
                    using SqliteCommand decoratorCommand = connection.CreateCommand();
                    decoratorCommand.CommandText = @"
                            INSERT INTO decorators (decorator_id, adventurer_id)
                            VALUES (@decoratorId, @adventurerId)
                        ";
                    decoratorCommand.Parameters.AddWithValue("@decoratorId", player.Adventurers[i].Equipment[j].ItemId);
                    decoratorCommand.Parameters.AddWithValue("@adventurerId", id);
                    decoratorCommand.ExecuteNonQuery();
                }
            }

            //Check if adv out on mission. Have to check which adventure is out on an adventure
            for (int i = 0; i < player.Missions.Count; i++) {
                using SqliteCommand missionCommand = connection.CreateCommand();

                missionCommand.CommandText = @"
                    INSERT INTO missions (
                        adventurer_id, time_left, destination, encounters, current_adventurer_health,
                        current_copper_reward, current_silver_reward, current_gold_reward,
                        completion_copper_reward, completion_silver_reward, completion_gold_reward
                        )
                    VALUES (
                        @adventurerId, @timeLeft, @destination, @encounters, @adventurerHealth,
                        @currentCopperReward, @currentSilverReward, @currentGoldReward,
                        @completionCopperReward, @completionSilverReward, @completionGoldReward
                        )
                ";

                missionCommand.Parameters.AddWithValue("@adventurerId", player.Missions[i].Adventurer.Id);
                missionCommand.Parameters.AddWithValue("@timeLeft", player.Missions[i].TimeLeft);
                missionCommand.Parameters.AddWithValue("@destination", player.Missions[i].Destination);
                missionCommand.Parameters.AddWithValue("@encounters", player.Missions[i].EncounterNumber);
                missionCommand.Parameters.AddWithValue("@adventurerHealth", player.Missions[i].AdventurerHealth);

                missionCommand.Parameters.AddWithValue("@currentCopperReward", player.Missions[i].Reward.Copper);
                missionCommand.Parameters.AddWithValue("@currentSilverReward", player.Missions[i].Reward.Silver);
                missionCommand.Parameters.AddWithValue("@currentGoldReward", player.Missions[i].Reward.Gold);

                missionCommand.Parameters.AddWithValue("@completionCopperReward", player.Missions[i].CompletionReward.Copper);
                missionCommand.Parameters.AddWithValue("@completionSilverReward", player.Missions[i].CompletionReward.Silver);
                missionCommand.Parameters.AddWithValue("@completionGoldReward", player.Missions[i].CompletionReward.Gold);
                missionCommand.ExecuteNonQuery();
            }
        }


        //LOAD GAME

        public Player GetPlayerById(int id) {

            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT *
                FROM players
                WHERE player_id = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            Player player = new();

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                player.Id = reader.GetInt32(0);
                player.PlayerName = reader.GetString(1);
                int copper = reader.GetInt32(2);
                int silver = reader.GetInt32(3);
                int gold = reader.GetInt32(4);
                player.SetCurrency(copper, silver, gold);

            }

            //Get all logs to load
            using SqliteCommand logCommand = connection.CreateCommand();
            logCommand.CommandText = @"
                    SELECT log_entry
                    FROM logs
                    JOIN players
                        ON Logs.player_id = Players.player_id
                    WHERE Players.player_id = @id
            ";
            logCommand.Parameters.AddWithValue("@id", id);
            logCommand.ExecuteNonQuery();

            using SqliteDataReader logReader = logCommand.ExecuteReader();
            while (logReader.Read()) {
                player.AddLogMessage(logReader.GetString(0));
            };
            return player;
        }

        public List<Adventurer> GetAdventurers(int id) {

            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT *
                FROM adventurers
                WHERE player_id = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            List<Adventurer> adventurers = new();

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                int adventurerId = reader.GetInt32(0);
                string name = reader.GetString(1);
                string adventurerClass = reader.GetString(2);
                int health = reader.GetInt32(3);
                int damage = reader.GetInt32(4);
                int luck = reader.GetInt32(5);

                IAdventurerFactory factory = new RogueFactory();
                switch (adventurerClass) {
                    case "Warrior":
                    factory = new WarriorFactory();
                    break;
                    case "Mage":
                    factory = new MageFactory();
                    break;
                }
                Adventurer adventurer = factory.CreateAdventurer();

                adventurer.Id = adventurerId;
                adventurer.Name = name;
                adventurer.Health = health;
                adventurer.Damage = damage;
                adventurer.Luck = luck;

                adventurers.Add(adventurer);
            }
            return adventurers;


        }

        public List<int> GetDecorators(int id) {

            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT decorator_id
                FROM decorators
                WHERE adventurer_id = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            List<int> itemCodes = new();

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                itemCodes.Add(reader.GetInt32(0));
            }

            return itemCodes;
        }

        //Triple join. But could use list from GetAdventuerers
        public List<Mission> GetMissionsForAdventurers(int id) {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT 
                    Adventurers.adventurer_id, time_left, destination, encounters, current_adventurer_health,
                    Missions.current_copper_reward, Missions.current_silver_reward, Missions.current_gold_reward,
                    Missions.completion_copper_reward, Missions.completion_silver_reward, Missions.completion_gold_reward
                FROM Missions
                JOIN Adventurers
                    ON Missions.adventurer_id = Adventurers.adventurer_id
                JOIN Players
                    ON Adventurers.player_id = Players.player_id
                WHERE Players.player_id = @id                
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            List<Mission> missions = new();

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                Mission mission = new();

                mission.AdventurerId = reader.GetInt32(0);
                mission.TimeLeft = reader.GetInt32(1);
                mission.Destination = reader.GetString(2);
                mission.EncounterNumber = reader.GetInt32(3);
                mission.AdventurerHealth = reader.GetInt32(4);
                mission.Reward = new(reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7));
                mission.CompletionReward = new(reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10));
                missions.Add(mission);
            }
            return missions;
        }

        ////Only for testing
        //public string GetPlayernameById(int id)
        //{
        //    using SqliteConnection connection = new(DataSource);
        //    connection.Open();

        //    using SqliteCommand command = connection.CreateCommand();
        //    command.CommandText = @"
        //        SELECT player_name
        //        FROM players
        //        WHERE player_id = @id;
        //    ";
        //    command.Parameters.AddWithValue("@id", id);
        //    command.ExecuteNonQuery();

        //    using SqliteDataReader reader = command.ExecuteReader();
        //    if (reader.Read()) {
        //        return reader.GetString(0);
        //        //should also retrieve adventurer list later.
        //    }
        //    else {
        //        return "";
        //    }
        //}

        public string[] RetrieveAllPlayerNames() {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT player_id, player_name
                FROM players;
            ";
            command.ExecuteNonQuery();


            string[] playerNames = new string[4];


            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                playerNames[reader.GetInt32(0) - 1] = reader.GetString(1);
            }

            for (int i = 0; i < playerNames.Length; i++) {
                if (String.IsNullOrEmpty(playerNames[i])) {
                    playerNames[i] = "Empty";
                }
            }
            return playerNames;
        }

        public void Delete(int id) {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                DELETE
                FROM players
                WHERE player_id = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public void DropTable(string table) {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @$"
                DROP TABLE
                IF EXISTS
                {table};
            ";
            //No user interaction Does actually not need preparedstatement.
            //command.Parameters.AddWithValue("@table", table);
            command.ExecuteNonQuery();
        }


    }
}
