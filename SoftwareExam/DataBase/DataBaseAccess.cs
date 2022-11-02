using Microsoft.Data.Sqlite;
using SoftwareExam.CoreProgram;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DataBaseAccess()
        {
            CreateDb();
        }

        public void CreateDb()
        {
            using SqliteConnection connection = new("Data Source = examplePlayerSqlite.db");
            CreateTablePlayer(connection);
            
            connection.Open();
        }

        public void CreateTablePlayer(SqliteConnection connection)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE players
                (
                    id INTEGER NOT NULL PRIMARY KEY,
                    player_name TEXT NOT NULL
                    copper INTEGER NOT NULL
                    silver INTEGER NOT NULL
                    gold INTEGER NOT NULL
                )
            ";
            command.ExecuteNonQuery();
        }

        public void Save()
        {
            Player tempPlayer = new Player(); //Temp player to test
            tempPlayer.PlayerName = "sinna krigare";
            tempPlayer.Balance = new Currency(5, 5, 500);

            using SqliteConnection connection = new SqliteConnection("Data Source = examplePlayerSqlite.db");
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO players (id, player_name, copper, silver, gold)
                VALUES ($id, $playerName, $copper, $silver, $gold)
            ";

            command.Parameters.AddWithValue("id", tempPlayer.Id);
            command.Parameters.AddWithValue("$playerName", tempPlayer.PlayerName);
            command.Parameters.AddWithValue("$copper", tempPlayer.Balance.Copper);
            command.Parameters.AddWithValue("$silver", tempPlayer.Balance.Silver);
            command.Parameters.AddWithValue("$gold", tempPlayer.Balance.Gold);
            command.ExecuteNonQuery();
        }

        //Temp. setting method to player for testing
        public void RetrieveById(int id)
        {
            int generatedId = -1;

            using SqliteConnection connection = new SqliteConnection("Data Source = examplePlayerSqlite.db");
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT *
                FROM players
                WHERE id = '{$id}';
            ";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();

            GetPlayerFromEntry(command);
        }

        public void RetrieveAll()
        {
            using SqliteConnection connection = new SqliteConnection("Data Source = examplePlayerSqlite.db");
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT *
                FROM players;
            ";
            command.ExecuteNonQuery();

            GetPlayerFromEntry(command);
        }

        public void Delete(int id)
        {
            using SqliteConnection connection = new SqliteConnection("Data Source = examplePlayerSqlite.db");
            connection.Open();

            SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                DELETE *
                FROM players
                WHERE id = '{$id}';
            ";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }

        public Player GetPlayerFromEntry(SqliteCommand command)
        {
            int generatedId = -1;

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                generatedId = reader.GetInt32(0);

                Player retrievedPlayer = new();
                retrievedPlayer.Id = generatedId;
                retrievedPlayer.PlayerName = reader.GetString(2);
                retrievedPlayer.Balance = new Currency(reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                //should also retrieve adventurer list later.

                return retrievedPlayer;
            }
            return null;
        }
    }
}
