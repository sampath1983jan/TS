using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Data;
namespace TechSharpy.Entitifier.Core
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
    
    public class Trigger
    {
        public int TriggerID;
        public int Entitykey;
        public string TriggerName;
        public EventType Type;
        public ActionType Action;
        public List<string> Steps;
        public TechSharpy.Entitifier.Data.Trigger dataTrigger;
        public Trigger() {
            dataTrigger = new Data.Trigger();
        }
        public Trigger(string triggerName, EventType type, ActionType action, List<string> steps)
        {
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
            Type = type;
            Action = action;
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
            dataTrigger = new Data.Trigger();
        }

        public Trigger(int entitykey)
        {
            TriggerID = -1;
            Entitykey = entitykey;
            dataTrigger = new Data.Trigger();
            Init();
        }

        public bool Save() {
            if (TriggerID > 0)
            {

                if (dataTrigger.Save(TriggerID, TriggerName, Entitykey, Action, Type, string.Join(",", Steps.ToArray())))
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                int nextid = 0;
                nextid = dataTrigger.Save(TriggerName, Entitykey, Action, Type, Steps.ToString());
                return true;
            }                      
        }

        public void AddSteps(string step) {
            this.Steps.Add(step);
        }

        public void Init() {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt= dataTrigger.getTriggers(Entitykey);

            var e = dt.AsEnumerable().Select(g => new Trigger
            {
                TriggerID = g.IsNull("EntityID") ? 0 : g.Field<int>("EntityID"),
                TriggerName = g.IsNull("Name") ? "" : g.Field<string>("Name"),
                Entitykey = g.IsNull("Entitykey") ? 0 : g.Field<int>("Entitykey"),
                Steps = g.IsNull("Steps") ? new List<string>() : g.Field<string>("Steps").Split(',').ToList(),
                Type = g.IsNull("EventType") ? EventType._insert : g.Field<EventType>("EventType"),
                Action = g.IsNull("ActionType") ? ActionType._before : g.Field<ActionType>("ActionType")
            }).First();
            this.TriggerID = e.TriggerID;
            this.TriggerName = e.TriggerName;
            this.Entitykey = e.Entitykey;
            this.Steps = e.Steps;
            this.Type = e.Type;
            this.Action = e.Action;
        }


    }
}
