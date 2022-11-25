namespace SoftwareExam.CoreProgram.Expedition {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// A class that manages the player lock and makes sure it is threadsafe
    /// </summary>
    public class LogWriter {

        private readonly object _lock = new();
        private readonly ManualResetEvent _taskPauseEvent = new(true);

        public void Pause() {
            _taskPauseEvent.Reset();
        }
        public void Resume() {
            _taskPauseEvent.Set();
        }

        /// <summary>
        /// Clears and updates the mission log, is locked to be threadsafe
        /// </summary>
        /// <param name="player">Required to access the player's log and function</param>
        /// <param name="logMessage">The message to write in the log</param>
        public void UpdateLog(Player player, string logMessage) {

            lock (_lock) {

                _taskPauseEvent.WaitOne();

                if (player.Log.Count >= 5) {
                    player.AddLogMessage(logMessage);

                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - player.Log.Count);
                    for (int i = 0; i < player.Log.Count; i++) {
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                    }

                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - player.Log.Count);
                    Console.WriteLine(player.GetLogMessages());

                } else {
                    Console.WriteLine(logMessage);
                    player.AddLogMessage(logMessage);
                }
            }
        }
    }
}
