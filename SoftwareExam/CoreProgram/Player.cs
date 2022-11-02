using SoftwareExam.CoreProgram.Adventurers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram
{
    public class Player
    {
        public int id;
        private string _playerName;
        public Currency Balance { get; set; }
        public List<Adventurer> Adventurers = new();


        public bool ValidateUserName(string userName)
        {
            if (userName.Length <= 0 || userName.Length > 20 || userName == null) {
                return false;
            }
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$")) {
                return false;
            }
            return true;
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
    }
}
