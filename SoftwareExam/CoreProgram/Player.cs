﻿using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Expedition;
using System.Text.RegularExpressions;

namespace SoftwareExam.CoreProgram
{
    public class Player
    {
        private int _id = -1;
        private string _playerName = "";
        public Currency Balance { get; set; } = new(0, 0, 2);
        public List<Adventurer> Adventurers = new();
        public List<Adventure> Adventures = new();
        public int AvailableAdventurers { get; set; } = 0;
        public List<string> Log { get; set; }

        public Player()
        {
            //Empty
        }

        public Player(int id, string playerName, Currency balance)
        {
            _id = id;
            _playerName = playerName;
            Balance = balance;
        }

        public Player(int id, string playerName, Currency balance, List<Adventurer> adventurers)
        {
            _id = id;
            _playerName = playerName;
            Balance = balance;
            Adventurers = adventurers;
        }


        public static bool ValidateUserName(string userName)
        {
            if (userName.Length <= 0 || userName.Length > 20 || userName == null) {
                return false;
            }
            if (!Regex.IsMatch(userName, @"^[a-zA-Z ]+$")) {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"Playername: {_playerName}";
        }

        //Property
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                if (ValidateUserName(value)) {
                    _playerName = value;
                }
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value < 1 || value > 4) {
                    throw new Exception("save between 1-4");
                }
                else {
                    _id = value;
                }
            }
        } 

        public void SetCurrency(int copper, int silver, int gold) {
            Balance = new Currency(copper, silver, gold);
        }
    }
}
