using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Core
{
    public enum StepType {        
        _condition=1,
        _Step=2
    }
    public class Step
    {
        public string Name;
        public int StepID;
        public StepType Type;
        public string YesStatement;
        public string NoStatement;
        public Step(string name, StepType type, string statement)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StepID = -1;
            Type = type;
            YesStatement = statement ?? throw new ArgumentNullException(nameof(statement));
        }
        public Step(string name, StepType type, string statement,string noSatement)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StepID = -1;
            Type = type;
            YesStatement = statement ?? throw new ArgumentNullException(nameof(statement));
            NoStatement = noSatement ?? throw new ArgumentNullException(nameof(noSatement));
        }
        public Step(int stepID) {
            StepID = stepID;
        }
        
    }
}
