﻿using Microsoft.Data.Sqlite;
using SoftwareExam.CoreProgram;
using System.Collections;

/**
 * SQLite AdoNet
 * Should take id, playerName, balance for now.
 *
 */

namespace SoftwareExam.DataBase {

    public class DataBaseAccess {

        private InitializeDatabase InitDb;
        private readonly string DataSource = "";

        public DataBaseAccess(string dataSource)
        {
            using (SqliteConnection connection = new(dataSource)) {
                connection.Open();
                InitDb = new(dataSource);
                DataSource = dataSource;
            }
        }


        //Should actually not access player - best way to return info?
        public void Save(Player player)
        {
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
        }
        
        // This shouldn't break layering, but needs way to parse adventurers later
        public void Save(ArrayList SaveArray)
        {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO players (player_id, player_name, copper, silver, gold)
                VALUES (@playerId, @playerName, @copper, @silver, @gold)
            ";
            command.Parameters.AddWithValue("@playerId", SaveArray[0]);
            command.Parameters.AddWithValue("@playerName", SaveArray[1]);
            command.Parameters.AddWithValue("@copper", SaveArray[2]);
            command.Parameters.AddWithValue("@silver", SaveArray[3]);
            command.Parameters.AddWithValue("@gold", SaveArray[4]);
            command.ExecuteNonQuery();
        }

        public void GetPlayerById(int id, out int playerId, out string playerName, out int copper, out int silver, out int gold)
        {
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

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                playerId = reader.GetInt32(0);
                playerName = reader.GetString(1);
                copper = reader.GetInt32(2);
                silver = reader.GetInt32(3);
                gold = reader.GetInt32(4);
                //should also retrieve adventurer list later.
            }
            else {
                playerId = -1;
                playerName = "";
                copper = 0;
                silver = 0;
                gold = 0;
            }
        }

        public string[] RetrieveAllPlayerNames()
        {
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

        public void Delete(int id)
        {
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

        public void DropTable(string table)
        {
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
