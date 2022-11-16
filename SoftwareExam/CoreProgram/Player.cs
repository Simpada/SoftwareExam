using SoftwareExam.CoreProgram.Adventurers;
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
        public List<Mission> Missions = new();
        public int AvailableAdventurers { get; set; } = 0;
        private int AdventurersOnMission = 0;
        public List<string> Log { get; set; } = new();

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

        public Player(int id, string playerName, Currency balance, List<Adventurer> adventurers, List<string> log)
        {
            _id = id;
            _playerName = playerName;
            Balance = balance;
            Adventurers = adventurers;
            Log = log;
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

        public void AddLogMessage(string logMessage) {
            if (Log.Count > 5) {
                Log.RemoveAt(0);
            }
            Log.Add(logMessage);
        }

        internal string GetLogMessages() {
            string LogMessage = "";
            for(int i = 0; i < Log.Count; i++) {
                LogMessage += Log[i];
                if (i+1 < Log.Count) {
                    LogMessage += "\n";
                }
            }
            return LogMessage;
        }
    }
}
