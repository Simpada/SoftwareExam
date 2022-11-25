using Microsoft.Data.Sqlite;

namespace SoftwareExam.DataBase {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class InitializeDatabase {
        private readonly string _dataSource;

        public InitializeDatabase(string dataSource) {
            _dataSource = dataSource;
            Init();
        }

        public void Init() {
            if (!CheckIfDatabaseExists()) {
                CreateTablePlayer();
                CreateTableAdventurers();
                CreateTableDecorators();
                CreateTableMission();
                CreateTableLogs();
            }
        }

        public bool CheckIfDatabaseExists() {
            if (File.Exists(_dataSource)) {
                return true;
            }
            return false;
        }

        public void CreateTablePlayer() {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS players
                (
                    player_id INT NOT NULL PRIMARY KEY,
                    player_name TEXT NOT NULL,
                    copper INT NOT NULL,
                    silver INT NOT NULL,
                    gold INT NOT NULL
                )
            ";
            command.ExecuteNonQuery();
        }

        public void CreateTableAdventurers() {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS adventurers
                (
                    adventurer_id INTEGER NOT NULL PRIMARY KEY,
                    adventurer_name TEXT NOT NULL,
                    class TEXT NOT NULL,
                    health INT NOT NULL,
                    damage INT NOT NULL,
                    luck INT NOT NULL,
                    player_id INT NOT NULL,
                    CONSTRAINT fk_player
                        FOREIGN KEY(player_id) REFERENCES players(player_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }

        private void CreateTableDecorators() {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS decorators
                (
                    decorator_id INT NOT NULL,
                    adventurer_id INT NOT NULL,
                    PRIMARY KEY (decorator_id, adventurer_id),
                    CONSTRAINT fk_adventurers
                        FOREIGN KEY(adventurer_id) REFERENCES adventurers(adventurer_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }

        private void CreateTableMission() {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS missions
                (
                    adventurer_id INTEGER NOT NULL PRIMARY KEY,
                    time_left INT NOT NULL,
                    destination varchar(50) NOT NULL,
                    encounters INT NOT NULL,
                    current_adventurer_health INT NOT NULL,
                    current_copper_reward INT NOT NULL,
                    current_silver_reward INT NOT NULL,
                    current_gold_reward INT NOT NULL,
                    completion_copper_reward INT NOT NULL,
                    completion_silver_reward INT NOT NULL,
                    completion_gold_reward INT NOT NULL,
                    CONSTRAINT fk_adventureres_ms
                        FOREIGN KEY(adventurer_id) REFERENCES adventurers(adventurer_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }
        private void CreateTableLogs() {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS logs
                (
                    log_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    log_entry varchar(1000),
                    player_id INT NOT NULL,
                    CONSTRAINT fk_players_log
                        FOREIGN KEY(player_id) REFERENCES players(player_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }
    }
}
