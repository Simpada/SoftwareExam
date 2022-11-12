using Microsoft.Data.Sqlite;
using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Adventurers.Decorators;
using SoftwareExam.CoreProgram.Adventurers.Factory;
using System.Collections;
using System.Numerics;
using System.Reflection.PortableExecutable;

/**
 * SQLite AdoNet
 * Should take id, playerName, balance for now.
 *
 */

namespace SoftwareExam.DataBase {

    public class DataBaseAccess {

        //private InitializeDatabase InitDb;
        private readonly string DataSource = "";

        public DataBaseAccess(string dataSource)
        {
            using (SqliteConnection connection = new(dataSource)) {
                connection.Open();
                _ = new InitializeDatabase(dataSource);
                DataSource = dataSource;
            }
        }

        public void Save(Player player)
        {
            if (CheckIfPlayerExists(player.Id)) {
                Delete(player.Id);
            }
            Add(player);
        }

        //Used to check if there is a player for overwriting a saved game.
        public bool CheckIfPlayerExists(int id)
        {
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
            }
            else {
                return false;
            }
        }

        //Should actually not access player - best way to return info?
        public void Add(Player player)
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

            for (int i = 0; i < player.Adventurers.Count; i++) {
                using SqliteCommand adventurerCommand = connection.CreateCommand();

                // This is often risky, but we know that all adventurers in the list are always BaseDecoratedAdventurers
                Adventurer adventurer = Adventurer.FindBase( (BaseDecoratedAdventurer) player.Adventurers[i]);

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
        }


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

                AdventurerFactory factory = new RogueFactory();
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

        //Only for testing
        public string GetPlayernameById(int id)
        {
            using SqliteConnection connection = new(DataSource);
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT player_name
                FROM players
                WHERE player_id = @id;
            ";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                return reader.GetString(0);
                //should also retrieve adventurer list later.
            }
            else {
                return "";
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
