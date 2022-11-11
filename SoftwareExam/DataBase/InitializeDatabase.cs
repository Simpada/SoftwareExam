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
                    adventurer_id INTEGER NOT NULL AUTOINCREMENT PRIMARY KEY,
                    adventurer_name TEXT NOT NULL,
                    class TEXT NOT NULL,
                    health INTEGER NOT NULL,
                    damaage INTEGER NOT NULL,
                    luck INTEGER NOT NULL,
                    player_id INTEGER NOT NULL,
                    CONSTRAINT fk_player
                        FOREIGN KEY(player_id) REFERENCES player_id(players)
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
                        FOREIGN KEY(adventurer_id) REFERENCES adventurer_id(adventurers)
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
