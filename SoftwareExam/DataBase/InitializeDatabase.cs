﻿using Microsoft.Data.Sqlite;
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
            init();
        }

        public void init()
        {
            if (CheckIfDatabaseExists()) {
                CreateDb();
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

        public void CreateDb()
        {
            using SqliteConnection connection = new(DataSource);
            connection.Open();
        }

        private void CreateTablePlayer()
        {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
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
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS adventurers
                (
                    adventurer_id INTEGER NOT NULL PRIMARY KEY,
                    adventurer_name TEXT NOT NULL,
                    class TEXT NOT NULL,
                    health INTEGER NOT NULL,
                    damaage INTEGER NOT NULL,
                    luck INTEGER NOT NULL
                    player_id INTEGER NOT NULL,
                    FOREIGN KEY(player_id) REFERENCES player_id(players)
                )
            ";
            command.ExecuteNonQuery();
        }

        private void CreateTableDecorators()
        {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS decorators
                (
                    decorator_id INTEGER NOT NULL,
                    adventurer_id INTEGER NOT NULL,
                    PRIMARY KEY (decoratir_id, adventurer_id),
                    FOREIGN KEY(adventurer_id) REFERENCES adventurer_id(adventurers)
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
