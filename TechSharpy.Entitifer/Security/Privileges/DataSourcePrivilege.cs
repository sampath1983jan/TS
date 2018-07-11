using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security.Privileges
{
    public class DataSourcePrivilege
    {       
        public string DataSourceKey;
        public int DataSourceID;
        public string Name;        
        public bool View;
        public bool Change;
        public bool Remove;
        public User user;
        public List<EntitySchemaPrivilege> Entityprivileges;
        public List<FunctionPrivilege> FunctionPrivileges;
        public List<ProcedurePrivilege> ProcedurePrivileges;
        public List<JobPrivilege> JobPrivileges;
        public List<ModelPrivilege> ModelPrivileges;
        private Data.Privilege dataPrivilege;
        public DataSourcePrivilege(string dataSourceKey,int userKey)
        {
            user = new User(userKey);
          DataSourceID = 0;
            DataSourceKey = dataSourceKey;
            Entityprivileges = new List<EntitySchemaPrivilege>();
            FunctionPrivileges = new List<FunctionPrivilege>();
            ProcedurePrivileges = new List<ProcedurePrivilege>();
            JobPrivileges = new List<JobPrivilege>();
            ModelPrivileges = new List<ModelPrivilege>();
            Remove = false;
            View = false;
            Change = false;
            dataPrivilege = new Data.Privilege();
            if (user == null)
            {
                throw new Exception("Security failure");
            }
        }

        public DataSourcePrivilege(string name, bool view, bool change,bool remove,int Userkey, string dataSourceKey)             
        {
            user = new User(Userkey);
            Remove = remove;
          DataSourceID = 0;
            Name = name;
            DataSourceKey = dataSourceKey;
            View = view;
            Change = change;
            Entityprivileges = new List<EntitySchemaPrivilege>();
            FunctionPrivileges = new List<FunctionPrivilege>();
            ProcedurePrivileges = new List<ProcedurePrivilege>();
            JobPrivileges = new List<JobPrivilege>();
            ModelPrivileges = new List<ModelPrivilege>();
            dataPrivilege = new Data.Privilege();
            if (user == null) {
                throw new Exception("Security failure");
            }
        }

        public bool Save() {
            if (dataPrivilege.SaveDataSourcePrivilege (this.user.UserID, this.DataSourceKey, 
                this.View, false, this.Remove,this.Change))
            {
                return true;
            }
            else return false;
            
        }

        public bool Delete() {
            return true;
        }

        public void SaveEntityPrivilege() {
            foreach (EntitySchemaPrivilege ep in Entityprivileges) {
                ep.Save();
            }
        }
        public void SaveFunctionPrivilege()
        {
            foreach (FunctionPrivilege ep in FunctionPrivileges)
            {
                ep.Save();
            }
        }

        public void SaveProcedurePrivilege()
        {
            foreach (ProcedurePrivilege ep in ProcedurePrivileges)
            {
                ep.Save();
            }
        }

        public void SaveJobPrivilege()
        {
            foreach (JobPrivilege ep in JobPrivileges)
            {
                ep.Save();
            }
        }

        public void SaveModelPrivilege()
        {
            foreach (ModelPrivilege ep in ModelPrivileges)
            {
                ep.Save();
            }
        }
        
        public bool AddEntityPrivilege(int Userkey, bool add,bool view,bool remove,bool edit) {
           EntitySchemaPrivilege ep= new EntitySchemaPrivilege(DataSourceKey, Userkey,  add, view, remove, edit);
            if (ep.Save())
            {
                Entityprivileges.Add(ep);
                return true;
            }
            else return false;        
        }

        public bool AddFunctionPrivilege(int Userkey,  bool add, bool view, bool remove, bool edit)
        {
            FunctionPrivilege ep = new FunctionPrivilege(DataSourceKey, Userkey,  add, view, remove, edit);
            if (ep.Save())
            {
                FunctionPrivileges.Add(ep);
                return true;
            }
            else return false;
        }
        public bool AddJobPrivilege(int Userkey, bool add, bool view, bool remove, bool edit)
        {
            JobPrivilege ep = new JobPrivilege(DataSourceKey, Userkey,  add, view, remove, edit);
            if (ep.Save())
            {
                JobPrivileges.Add(ep);
                return true;
            }
            else return false;
        }

        public bool AddProcedurePrivilege(int Userkey, bool add, bool view, bool remove, bool edit)
        {
            ProcedurePrivilege ep = new ProcedurePrivilege(DataSourceKey, Userkey,  add, view, remove, edit);
            if (ep.Save())
            {
                ProcedurePrivileges.Add(ep);
                return true;
            }
            else return false;
        }

        public bool AddModelPrivilege(int Userkey,  bool add, bool view, bool remove, bool edit)
        {
            ModelPrivilege ep = new ModelPrivilege(DataSourceKey, Userkey,  add, view, remove, edit);
            if (ep.Save())
            {
                ModelPrivileges.Add(ep);
                return true;
            }
            else return false;
        }
    }

}
