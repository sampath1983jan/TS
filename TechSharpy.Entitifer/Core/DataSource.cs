using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Core
{
    class DataSource
    {
        public int DataSourceID;
        public string SourceKey;
        public string Name;
        public List<EntitySchema> Entitys;
        public List<Function> Functions;
        public List<Procedure> EntityProcedures;
        public List<Job> Jobs;
        public List<EntityModel> Model;

        public DataSource(int dataSourceID, string sourceKey, string name, List<EntitySchema> entitys, 
            List<Function> functions, List<Procedure> entityProcedures, List<Job> jobs, List<EntityModel> model)
        {
            DataSourceID = dataSourceID;
            SourceKey = sourceKey;
            Name = name;
            Entitys = entitys;
            Functions = functions;
            EntityProcedures = entityProcedures;
            Jobs = jobs;
            Model = model;
        }

        public DataSource(int dataSourceID)
        {
            DataSourceID = dataSourceID;
            SourceKey = "";
            Name = "";
            Entitys = new List<EntitySchema>();
            Functions = new List<Function>();
            EntityProcedures = new List<Procedure>();
            Jobs = new List<Job>() ;
            Model = new List<EntityModel>();
        }

        public DataSource()
        {
            DataSourceID = -1;
            SourceKey = "";
            Name = "";
            Entitys = new List<EntitySchema>();
            Functions = new List<Function>();
            EntityProcedures = new List<Procedure>();
            Jobs = new List<Job>();
            Model = new List<EntityModel>();
        }

        public bool SaveEntity(EntitySchema entity) {
            return true;
        }

        public bool SaveFunction(Function function) {
            return true;
        }

        public bool SaveProcedure(Procedure procedure) {
            return true;
        }
        public bool SaveJob(Job job) {
            return true;
        }


    }
}
