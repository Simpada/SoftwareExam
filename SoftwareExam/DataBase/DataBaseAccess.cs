using Microsoft.Data.Sqlite;
using SoftwareExam.CoreProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/**
 * SQLite AdoNet
 * Should take id, playerName, balance for now.
 *
 */

namespace SoftwareExam.DataBase {

    public class DataBaseAccess {

        private readonly string DataSource = "";

        public DataBaseAccess(string dataSource)
        {
            DataSource = dataSource;
        }

        public void CreateDb()
        {
            using SqliteConnection connection = new(DataSource);
            connection.Open();
            CreateTablePlayer(connection);
        }

        private void CreateTablePlayer(SqliteConnection connection)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE players
                (
                    id INTEGER NOT NULL PRIMARY KEY,
                    player_name TEXT NOT NULL,
                    copper INTEGER NOT NULL,
                    silver INTEGER NOT NULL,
                    gold INTEGER NOT NULL
                )
            ";
            command.ExecuteNonQuery();
        }

        public void Save(Player player)
        {
            using SqliteConnection connection = new SqliteConnection(DataSource);
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
        public Player RetrieveById(int id)
        {
            using SqliteConnection connection = new SqliteConnection(DataSource);
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
            using SqliteConnection connection = new SqliteConnection(DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id, player_names
                FROM players;
            ";
            command.ExecuteNonQuery();


            string[] playerNames = new string[4];

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                playerNames[reader.GetInt32(0)] = reader.GetString(1);
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
            using SqliteConnection connection = new SqliteConnection(DataSource);
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

        public Player GetPlayerFromEntry(SqliteCommand command)
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
            using SqliteConnection connection = new SqliteConnection(DataSource);
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                DROP TABLE IF EXISTS '$table';
            ";
            command.Parameters.AddWithValue("$table", table);
        }

        public void ResetTable()
        {
            using SqliteConnection connection = new SqliteConnection(DataSource);
            connection.Open();

            DropTable("players");

            CreateTablePlayer(connection);
        }

    }
}
