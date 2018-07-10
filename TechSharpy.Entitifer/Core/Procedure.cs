using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;
namespace TechSharpy.Entitifier.Core
{
    class Procedure
    {
        public string Name ="";
        public int ProcedureID;
        public List<Step> Steps;

        public Procedure(int procedureID, string name, List<Step> steps)
        {
            ProcedureID = procedureID;
            Name = name;
            Steps = steps;
        }

        public Procedure(int procedureID)
        { 
            ProcedureID = procedureID;
        }
    }
}
