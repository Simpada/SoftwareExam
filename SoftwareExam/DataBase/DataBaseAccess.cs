using Microsoft.Data.Sqlite;
using SoftwareExam.CoreProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * SQLite AdoNet
 * Should take id, playerName, balance for now.
 *
 */

namespace SoftwareExam.DataBase {

    public class DataBaseAccess {

        public InitializeDatabase initDb = new("Data Source = gameDatabase.db");

        public void Save(Player player)
        {
            using SqliteConnection connection = new SqliteConnection(initDb.DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO players (id, player_name, copper, silver, gold)
                VALUES ($id, $playerName, $copper, $silver, $gold)
            ";
            command.Parameters.AddWithValue("$id", player.Id);
            command.Parameters.AddWithValue("$playerName", player.PlayerName);
            command.Parameters.AddWithValue("$copper", player.Balance.Copper);
            command.Parameters.AddWithValue("$silver", player.Balance.Silver);
            command.Parameters.AddWithValue("$gold", player.Balance.Gold);
            command.ExecuteNonQuery();
        }

        //Temp. setting method to player for testing
        public Player? RetrieveById(int id)
        {
            using SqliteConnection connection = new SqliteConnection(initDb.DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT *
                FROM players
                WHERE id = $id;
            ";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();

            return GetPlayerFromEntry(command);
        }

        public string[] RetrieveAllPlayerNames()
        {
            using SqliteConnection connection = new SqliteConnection(initDb.DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id, player_name
                FROM players;
            ";
            command.ExecuteNonQuery();


            string[] playerNames = new string[4];

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                playerNames[reader.GetInt32(0)-1] = reader.GetString(1);
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
            using SqliteConnection connection = new SqliteConnection(initDb.DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                DELETE
                FROM players
                WHERE id = $id;
            ";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }

        public Player? GetPlayerFromEntry(SqliteCommand command)
        {
            int generatedId = -1;

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                generatedId = reader.GetInt32(0);

                Player retrievedPlayer = new();
                retrievedPlayer.Id = generatedId;
                retrievedPlayer.PlayerName = reader.GetString(1);
                retrievedPlayer.Balance = new Currency(reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
                //should also retrieve adventurer list later.

                return retrievedPlayer;
            }
            return null;
        }


        public void DropTable(string table)
        {
            using SqliteConnection connection = new(initDb.DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @$"
                DROP TABLE
                
                '{table}';
            ";

            //Why not work with IF EXISTS on drop '$table';
            //command.Parameters.AddWithValue("$table", table);
            command.ExecuteNonQuery();
        }
    }
}
