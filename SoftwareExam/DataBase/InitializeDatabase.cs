using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.DataBase
{
    public class InitializeDatabase
    {
        private readonly string _dataSource;

        public InitializeDatabase(string dataSource)
        {
            _dataSource = dataSource;
            Init();
        }

        public void Init()
        {
            if (!CheckIfDatabaseExists()) {
                CreateTablePlayer();
                CreateTableAdventurers();
                CreateTableDecorators();
                CreateTableMission();
                CreateTableLogs();
            }
        }


        public bool CheckIfDatabaseExists()
        {
            if (File.Exists(_dataSource)) {
                return true;
            }
            return false;
        }

        public void CreateTablePlayer()
        {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();
            
            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS players
                (
                    player_id INTEGER NOT NULL PRIMARY KEY,
                    player_name TEXT NOT NULL,
                    copper INTEGER NOT NULL,
                    silver INTEGER NOT NULL,
                    gold INTEGER NOT NULL
                )
            ";
            command.ExecuteNonQuery();
        }

        public void CreateTableAdventurers()
        {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS adventurers
                (
                    adventurer_id INTEGER NOT NULL PRIMARY KEY,
                    adventurer_name TEXT NOT NULL,
                    class TEXT NOT NULL,
                    health INTEGER NOT NULL,
                    damage INTEGER NOT NULL,
                    luck INTEGER NOT NULL,
                    player_id INTEGER NOT NULL,
                    CONSTRAINT fk_player
                        FOREIGN KEY(player_id) REFERENCES players(player_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }

        private void CreateTableDecorators()
        {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS decorators
                (
                    decorator_id INTEGER NOT NULL,
                    adventurer_id INTEGER NOT NULL,
                    PRIMARY KEY (decorator_id, adventurer_id),
                    CONSTRAINT fk_adventurers
                        FOREIGN KEY(adventurer_id) REFERENCES adventurers(adventurer_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }

        private void CreateTableMission()
        {
            using SqliteConnection connection = new(_dataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS missions
                (
                    adventurer_id INTEGER NOT NULL PRIMARY KEY,
                    time_left INTEGER NOT NULL,
                    destination varchar(50) NOT NULL,
                    encounters INTEGER NOT NULL,
                    copper INTEGER NOT NULL,
                    silver INTEGER NOT NULL,
                    gold INTEGER NOT NULL,
                    CONSTRAINT fk_adventureres_ms
                        FOREIGN KEY(adventurer_id) REFERENCES adventurers(adventurer_id)
                        ON DELETE CASCADE
                )
            ";
            command.ExecuteNonQuery();
        }

        private void CreateTableLogs()
        {
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


        public string DataSource
        {
            get
            {
                return _dataSource;
            }
        }
    }
}
