using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Expedition {
    public class LogWriter {

        private readonly object Lock = new();
        private readonly ManualResetEvent TaskPauseEvent = new(true);

        public void Pause() {
            TaskPauseEvent.Reset();

        }
        public void Resume() {
            TaskPauseEvent.Set();
        }

        public void UpdateLog(Player player, string logMessage) {

            TaskPauseEvent.WaitOne();

            lock (Lock) {
                if (player.Log.Count >= 5) {
                    player.AddLogMessage(logMessage);

                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - player.Log.Count);
                    for (int i = 0; i < player.Log.Count; i++) {
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                    }

                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - player.Log.Count);
                    //foreach (string message in Player.Log) {
                    //    Console.WriteLine(message);
                    //}
                    Console.WriteLine(player.GetLogMessages());

                } else {
                    Console.WriteLine(logMessage);
                    player.AddLogMessage(logMessage);
                }
            }
        }
    }
}
