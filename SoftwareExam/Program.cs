using SoftwareExam.CoreProgram.Adventurer;
using SoftwareExam.UI;

namespace SoftwareExam {
    internal class Program {

        static void Main(string[] args) {

            GameUI Game = new GameUI();
            Game.Run();

            Adventurer test = new Rogue();
            Console.WriteLine(test.ToString());

        }
    }
}