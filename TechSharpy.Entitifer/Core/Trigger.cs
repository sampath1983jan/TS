using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifer.Core
{
    public enum EventType {
        _insert=1,
        _update=2,
        _delete=3
    }

    public enum ActionType {
        _before=0,
        _after=1
    }
    
    class Trigger
    {
        public int TriggerID;
        public int Entitykey;
        public string TriggerName;
        public EventType Type;
        public ActionType Action;
        public List<string> Steps;
        public Trigger(string triggerName, EventType type, ActionType action, List<string> steps)
        {
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
            Type = type;
            Action = action;
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }

        public Trigger(int entitykey,int triggerID)
        {
            TriggerID = triggerID;
            Entitykey = entitykey;
            Init();
        }

        public void AddSteps(string step) {
            this.Steps.Add(step);
        }

        public void Init() {

        }


    }
}
