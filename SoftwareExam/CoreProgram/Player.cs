using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.CoreProgram.Economy;
using SoftwareExam.CoreProgram.Expedition;
using System.Text.RegularExpressions;

namespace SoftwareExam.CoreProgram {
    /// <summary>
    /// Contains all information about the player, its balance, adventurers, which missions are active,
    /// its log, etc. This class is what is saved to and loaded from the database
    /// </summary>
    public class Player {
        private int _id = -1;
        private string _playerName = "";
        private Currency _balance = new(0, 0, 2);

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public List<Adventurer> Adventurers = new();
        public List<Mission> Missions = new();
        public List<string> Log { get; } = new();

        private readonly object Lock = new();

        #region Setting and checking values

        /// <summary>
        /// Makes sure the name fits our naming convention
        /// </summary>
        /// <param name="userName">the name to check</param>
        /// <returns>Bool saying if the name is accepted or not</returns>
        public static bool ValidateUserName(string userName) {
            if (userName.Length <= 0 || userName.Length > 20 || userName == null) {
                return false;
            }
            if (!Regex.IsMatch(userName, @"^[a-zA-Z ]+$")) {
                return false;
            }
            return true;
        }

        public string PlayerName {
            get {
                return _playerName;
            }
            set {
                if (ValidateUserName(value)) {
                    _playerName = value;
                }
            }
        }
        public int Id {
            get {
                return _id;
            }
            set {
                if (value < 1 || value > 4) {
                    throw new Exception("save between 1-4");
                } else {
                    _id = value;
                }
            }
        }

        public Currency Balance {
            get {
                return _balance;
            }
        }



        public void SetCurrency(int copper, int silver, int gold) {
            _balance = new Currency(copper, silver, gold);
        }

        public void AlterCurrency(Currency currency, bool add) {
            lock (Lock) {
                if (add) {
                    _balance += currency;
                } else {
                    _balance -= currency;

                }
            }
        }

        public void AddLogMessage(string logMessage) {
            lock (Lock) {
                if (Log.Count >= 5) {
                    Log.RemoveAt(0);
                }
                Log.Add(logMessage);
            }
        }


        public string GetLogMessages() {
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion

        /// <summary>
        /// Completes a mission that the player has, removing it form the array, and granting the player its reward
        /// </summary>
        public void CompleteMission() {
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

        /// <summary>
        /// Pauses or resumes all missions
        /// </summary>
        /// <param name="pause">Bool determining if its supposed to pause or resume</param>
        public void Pause(bool pause) {
            lock (Lock) {
                foreach (Mission mission in Missions) {
                    if (pause) {
                        mission.Pause();
                    } else {
                        mission.Resume();
                    }
                }
            }
        }

        /// <summary>
        /// Clears all missions, used when returning to the main menu
        /// </summary>
        public void TerminateMissions() {
            Missions.Clear();
        }

        /// <summary>
        /// Sells an adventurer the player has, granting parts of its value
        /// </summary>
        /// <param name="who">The index of the adventurer to sell</param>
        public void SellAdventurer(int who) {
            AlterCurrency(Adventurers[who].Value * 0.7, true);
            Adventurers.RemoveAt(who);
        }
    }
}
