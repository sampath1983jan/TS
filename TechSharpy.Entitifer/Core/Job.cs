using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace TechSharpy.Entitifier.Core
{
   public  class Job
    {
        public enum JobType {            
           _daily=1,
           _monthly=2,
           _weekly=3,
           _yearly=4,
           _hourly=5,
           _min=6
        }
        public enum Status {
           _active=1,
           _inactive=2
        }
        public string Datasourcekey;
        public int JobID;
        public string Name;
        public string Description;
        public List<Step> steps;
        public DateTime jobstartDate;
        public DateTime jobTime;
        public JobType Type;
        public Status status;
        private DateTime CurrentSchedule;
        private TechSharpy.Entitifier.Data.Job dataJob;

        public Job() {

        }
        public Job(string Datasourcekey,string name, string description, List<Step> steps, DateTime jobstartDate,
            DateTime jobTime, JobType type)
        {
            Name = name;
            Description = description;
            this.steps = steps;
            this.jobstartDate = jobstartDate;
            this.jobTime = jobTime;
            Type = type;
            dataJob = new Data.Job();
        }
        public Job(int jobID, string Datasourcekey)
        {         
            JobID = jobID;
            Init();
        }
        private void Init() {
            DataTable dt = new DataTable();
           dt= dataJob.GetJob(this.JobID);
            var jb = dt.AsEnumerable().Select(g => new Job
            {
                JobID = g.IsNull("FunctionID") ? 0 : g.Field<int>("FunctionID"),
                Name = g.IsNull("Name") ? "" : g.Field<string>("Name"),
                Description = g.IsNull("Description") ? "" : g.Field<string>("Description"),
                jobstartDate = g.IsNull("jobstartDate") ? DateTime.MinValue : g.Field<DateTime>("jobstartDate"),
                jobTime = g.IsNull("jobTime") ? DateTime.MinValue : g.Field<DateTime>("jobTime"),
                Type = g.IsNull("Type") ? JobType._daily : g.Field<JobType>("Type"),
                status = g.IsNull("status") ? Status._active : (g.Field<Status>("status")),
                CurrentSchedule = g.IsNull("CurrentSchedule") ? DateTime.MinValue : g.Field<DateTime>("CurrentSchedule"),
                Datasourcekey = g.IsNull("Datasourcekey") ? "" : g.Field<string>("Datasourcekey")
            }).FirstOrDefault();
        }
        public bool Save() {
            if (this.JobID <= 0)
            {
                this.JobID = dataJob.Save(this.Datasourcekey, this.Name, this.Description,
                Newtonsoft.Json.JsonConvert.SerializeObject(this.steps).ToString(), this.jobstartDate,
                this.jobTime, this.Type, this.CurrentSchedule, Status._active);
                if (this.JobID > 0)
                {
                    return true;
                }
                else return false;
            }
            else {
                if (dataJob.Save(this.JobID, this.Datasourcekey, this.Name, this.Description,
                    Newtonsoft.Json.JsonConvert.SerializeObject(this.steps).ToString(),
                    jobstartDate, jobTime, this.Type))
                {
                    return true;
                }
                else return false;
            }           
        }        
        private void StartNow() {

        }
        public bool ChangeStatus() {
            if (dataJob.UpdateStatus(this.JobID, this.Datasourcekey, this.status)){
                return true;
            } else return false;            
        }
        private bool JobUpdate() {
            if (dataJob.UpdateCurrentSchedule(this.JobID, this.Datasourcekey)) {
                return true;
            }else return false;
        }        
    }
}
