using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Security;
 
namespace TechSharpy.Entitifier.Core
{
   public class DataSource
    {
        public int DataSourceID;
        public string SourceKey;
        public string Name;
        public List<EntitySchema> Entitys;
        public List<Function> Functions;
        public List<Procedure> EntityProcedures;
        public List<Job> Jobs;
        public List<EntityModel> Model;
        public List<User> users;
        public Security.Privileges.DataSourcePrivilege dataSourcePrivilege;
        private Data.DataSource dataDatasource;
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
            dataDatasource = new Data.DataSource();
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
            dataDatasource = new Data.DataSource();
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
            dataDatasource = new Data.DataSource();
        }

        public bool Save() {
            this.DataSourceID = dataDatasource.Save(this.SourceKey, this.Name);
            if (this.DataSourceID >0)
            {
                Security.User u = new User(1);
                Security.Privileges.DataSourcePrivilege dsp = new Security.Privileges.DataSourcePrivilege(this.Name,true,true,true, u.UserID, this.SourceKey);
                if (!u.CheckUser())
                {
                    u.UserName = "admin";
                    u.Password = "123";
                    u.Type = User.UserType._admin;
                    u.Save();
                }
                else {
                    dsp.Save();
                }
                return true;
            }
            else return false;
        }

        public bool Delete() {
            if (dataDatasource.Delete(DataSourceID))
            {
                return true;
            }
            else return false;
        }

        public bool SaveEntity(EntitySchema entity) {
            if (entity.Save().HasCriticalError() ==true) {

                return true;

            }return false ;
        }

        public bool SaveFunction(Function function) {
            if (function.Save()) {
                return true;
            }
            else return false;
        }

        public bool SaveProcedure(Procedure procedure) {
            return true;
        }

        public bool SaveJob(Job job) {
            return true;
        }


    }
}
