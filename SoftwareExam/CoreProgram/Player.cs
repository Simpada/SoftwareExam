using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Expedition;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SoftwareExam.CoreProgram
{
    public class Player
    {
        private int _id = -1;
        private string _playerName = "";

        // Needs Lock
        private Currency _balance = new(0, 0, 2);
        public List<Adventurer> Adventurers = new();
        public List<Mission> Missions = new();
        public List<string> Log { get; } = new();

        private readonly object Lock = new();

        public Player()
        {
            //Empty
        }

        public Player(int id, string playerName, Currency balance)
        {
            _id = id;
            _playerName = playerName;
            _balance = balance;
        }

        public Player(int id, string playerName, Currency balance, List<Adventurer> adventurers, List<string> log)
        {
            _id = id;
            _playerName = playerName;
            _balance = balance;
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

        public Currency Balance
        {
            get
            {
                return _balance;
            }
        }

        public void SetCurrency(int copper, int silver, int gold)
        {
            lock (Lock) {
                _balance = new Currency(copper, silver, gold);
            }
        }

        public void AlterCurrency(Currency currency, bool add)
        {
            lock (Lock) {
                if (add) {
                    _balance += currency;
                }
                else {
                    _balance -= currency;

                }
            }
        }

        public void AddLogMessage(string logMessage)
        {

            lock (Lock) {
                if (Log.Count >= 5) {
                    Log.RemoveAt(0);
                }
                Log.Add(logMessage);
            }
        }

        public string GetLogMessages()
        {

            lock (Lock) {
                string LogMessage = "";
                for (int i = 0; i < Log.Count; i++) {
                    LogMessage += Log[i];
                    if (i + 1 < Log.Count) {
                        LogMessage += "\n";
                    }
                }
                return LogMessage;
            }
        }

        public void CompleteMission()
        {

            lock (Lock) {
                Mission? CompletedMission = null;
                foreach (Mission mission in Missions) {
                    if (mission.Completed) {
                        CompletedMission = mission;
                        break;
                    }
                }
                if (CompletedMission != null) {
                    AlterCurrency(CompletedMission.Reward, true);
                    Missions.Remove(CompletedMission);
                }
                
            }
        }

        public void ResumePause(bool pause)
        {
            lock (Lock) {
                foreach (Mission mission in Missions) {
                    if (pause) {
                        mission.Pause();
                    }
                    else {
                        mission.Resume();
                    }
                }
            }
        }

        public void TerminateMissions()
        {
            Missions.Clear();
        }
    }
}
