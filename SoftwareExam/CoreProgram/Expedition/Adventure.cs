using SoftwareExam.CoreProgram.Adventurers;


namespace SoftwareExam.CoreProgram.Expedition
{
    public class Adventure
    {
        public Adventurer Adventurer { get; set; }

        //Change to Map later
        public int Map { get; set; } = new();
        //Change to Encounter later
        public List<int> Encounters { get; set; } = new();

    }
}
