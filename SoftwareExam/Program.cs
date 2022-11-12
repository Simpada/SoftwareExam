using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.DataBase;
using SoftwareExam.UI;

namespace SoftwareExam {
    internal class Program {

        static void Main(string[] args) {

            GameUI Game = new ();
            Game.Run();

        }
    }
}
