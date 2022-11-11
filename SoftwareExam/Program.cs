using SoftwareExam.CoreProgram;
using SoftwareExam.CoreProgram.Adventurers;
using SoftwareExam.DataBase;
using SoftwareExam.UI;

namespace SoftwareExam {
    internal class Program {

        static void Main(string[] args) {

            Player p1 = new(1, "Aasdasd", new Currency(2, 6, 12));
            Player p2 = new(2, "ogkso", new Currency(1, 3, 6));
            Player p3 = new(4, "fo j29rj22", new Currency(6, 7, 120));

            DataBaseAccess db = new("Data Source = tempDatabase.db");

            db.Save(p1);
            db.Save(p2);
            db.Save(p3);

            GameUI Game = new ();
            Game.Run();

        }
    }
}