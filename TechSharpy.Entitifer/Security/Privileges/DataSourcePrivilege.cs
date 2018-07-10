using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security.Privileges
{
    class DataSourcePrivilege
    {
        public int DataSourceID;
        public string DataSourceKey;
        public string Name;        
        public bool View;
        public bool Change;
        public List<EntitySchemaPrivilege> Entityprivileges;
        public List<FunctionPrivilege> FunctionPrivileges;
        public List<ProcedurePrivilege> ProcedurePrivileges;
        public List<JobPrivilege> JobPrivileges;
        public List<ModelPrivilege> ModelPrivileges;

        public DataSourcePrivilege(string dataSourceKey)
        {
            DataSourceKey = dataSourceKey;
            Entityprivileges = new List<EntitySchemaPrivilege>();
            FunctionPrivileges = new List<FunctionPrivilege>();
            ProcedurePrivileges = new List<ProcedurePrivilege>();
            JobPrivileges = new List<JobPrivilege>();
            ModelPrivileges = new List<ModelPrivilege>();
        }

        public DataSourcePrivilege(string name, bool view, bool change)             
        {
            Name = name;
            DataSourceKey = "-1";
            View = view;
            Change = change;
            Entityprivileges = new List<EntitySchemaPrivilege>();
            FunctionPrivileges = new List<FunctionPrivilege>();
            ProcedurePrivileges = new List<ProcedurePrivilege>();
            JobPrivileges = new List<JobPrivilege>();
            ModelPrivileges = new List<ModelPrivilege>();
        }

        public bool Save() {
            return true;
        }

        public bool Delete() {
            return true;
        }

        public bool AddEntityPrivilege(int Userkey,int entityKey, bool add,bool view,bool remove,bool edit) {
           EntitySchemaPrivilege ep= new EntitySchemaPrivilege(DataSourceID, Userkey, entityKey, add, view, remove, edit);
            if (ep.Save())
            {
                Entityprivileges.Add(ep);
                return true;
            }
            else return false;        
        }

        public bool AddFunctionPrivilege(int Userkey, int entityKey, bool add, bool view, bool remove, bool edit)
        {
            FunctionPrivilege ep = new FunctionPrivilege(DataSourceID, Userkey, entityKey, add, view, remove, edit);
            if (ep.Save())
            {
                FunctionPrivileges.Add(ep);
                return true;
            }
            else return false;
        }
        public bool AddJobPrivilege(int Userkey, int entityKey, bool add, bool view, bool remove, bool edit)
        {
            JobPrivilege ep = new JobPrivilege(DataSourceID, Userkey, entityKey, add, view, remove, edit);
            if (ep.Save())
            {
                JobPrivileges.Add(ep);
                return true;
            }
            else return false;
        }

        public bool AddProcedurePrivilege(int Userkey, int entityKey, bool add, bool view, bool remove, bool edit)
        {
            ProcedurePrivilege ep = new ProcedurePrivilege(DataSourceID, Userkey, entityKey, add, view, remove, edit);
            if (ep.Save())
            {
                ProcedurePrivileges.Add(ep);
                return true;
            }
            else return false;
        }

        public bool AddModelPrivilege(int Userkey, int entityKey, bool add, bool view, bool remove, bool edit)
        {
            ModelPrivilege ep = new ModelPrivilege(DataSourceID, Userkey, entityKey, add, view, remove, edit);
            if (ep.Save())
            {
                ModelPrivileges.Add(ep);
                return true;
            }
            else return false;
        }
    }

}
