using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Factory {
    internal interface IAdventurerFactory {

        public abstract Adventurer CreateAdventurer();

    }
}
