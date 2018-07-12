﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TechSharpy.Data;
using TechSharpy.Entitifier.Core;
using static TechSharpy.Entitifier.Core.Job;

namespace TechSharpy.Entitifier.Data
{
    class Job:DataAccess
    {
        DataTable dtResult;
        Query iQuery;
        public Job()
        {
            try
            {
                this.Init();
                dtResult = new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetJob(int JobID)
        {
            iQuery = new Query(QueryType._Select)
               .AddField("jobid", "s_entityjob", FieldType._Number, "")
                .AddField("datasourcekey", "s_entityjob", FieldType._String, "")
                .AddField("name", "s_entityjob", FieldType._String, "")
                .AddField("description", "s_entityjob", FieldType._String, "")
                .AddField("steps", "s_entityjob", FieldType._String, "")
                .AddField("jobstartdate", "s_entityjob", FieldType._DateTime, "")
                .AddField("jobtime", "s_entityjob", FieldType._DateTime, "")
                .AddField("currentschedule", "s_entityjob", FieldType._DateTime, "")
                .AddField("type", "s_entityjob", FieldType._Number, "")
                .AddField("status", "s_entityjob", FieldType._Number, "");
            dtResult = this.GetData(iQuery);
            return dtResult;
        }


        public int Save(string Datasourcekey, string name, string description, string steps,
            DateTime jobstartDate,DateTime jobTime, JobType type,DateTime currentSchedule, Status status)
        {
            int inext = this.getNextID("Job");
            iQuery = new Query(QueryType._Insert)
                .AddField("jobid", "s_entityjob", FieldType._Number, "", inext.ToString())
                .AddField("datasourcekey", "s_entityjob", FieldType._String, "", Datasourcekey.ToString())
                .AddField("name", "s_entityjob", FieldType._String, "", name.ToString())
                .AddField("description", "s_entityjob", FieldType._String, "", description.ToString())
                .AddField("steps", "s_entityjob", FieldType._String, "", steps.ToString())
                .AddField("jobstartdate", "s_entityjob", FieldType._DateTime, "", jobstartDate.ToString())
                .AddField("jobtime", "s_entityjob", FieldType._DateTime, "", jobTime.ToString())
                .AddField("currentschedule", "s_entityjob", FieldType._DateTime, "", currentSchedule.ToString())
                .AddField("type", "s_entityjob", FieldType._Number, "", type.ToString())
                .AddField("status", "s_entityjob", FieldType._Number, "", status.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return inext ;
            }
            else {
                return -1;
            }
        }

        public bool UpdateCurrentSchedule(int jobid,string datasourcekey)
        {

            iQuery = new Query(QueryType._Update)              
                 .AddField("currentschedule", "s_entityjob", FieldType._DateTime, "", DateTime.Now.ToString())              
                .AddWhere(0, "s_entityjob", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._And)
                .AddWhere(0, "s_entityjob", "jobid", FieldType._Number, Operator._Equal, jobid.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateStatus(int jobid, string datasourcekey,Status status)
        {

            iQuery = new Query(QueryType._Update)
                 .AddField("status", "s_entityjob", FieldType._Number, "", status.ToString())
                .AddWhere(0, "s_entityjob", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._And)
                .AddWhere(0, "s_entityjob", "jobid", FieldType._Number, Operator._Equal, jobid.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Save(int jobid,string datasourcekey, string name, string description, string steps,
            DateTime jobstartDate, DateTime jobTime, JobType type)
        {
            
            iQuery = new Query(QueryType._Update)                
            //    .AddField("datasourcekey", "s_entityjob", FieldType._String, "", datasourcekey.ToString())
                .AddField("name", "s_entityjob", FieldType._String, "", name.ToString())
                .AddField("description", "s_entityjob", FieldType._String, "", description.ToString())
                .AddField("steps", "s_entityjob", FieldType._String, "", steps.ToString())
                .AddField("jobstartdate", "s_entityjob", FieldType._DateTime, "", jobstartDate.ToString())
                .AddField("jobtime", "s_entityjob", FieldType._DateTime, "", jobTime.ToString())
            //    .AddField("currentschedule", "s_entityjob", FieldType._DateTime, "", currentSchedule.ToString())
             //.AddField("status", "s_entityjob", FieldType._Number, "", status.ToString())
                .AddField("type", "s_entityjob", FieldType._Number, "", type.ToString())
                .AddWhere(0, "s_entityjob", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(),Condition._And)
                .AddWhere(0, "s_entityjob", "jobid", FieldType._Number, Operator._Equal, jobid.ToString());
                
            
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
