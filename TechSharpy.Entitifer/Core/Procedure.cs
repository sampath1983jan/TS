using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;
using System.Data;
namespace TechSharpy.Entitifier.Core
{
   public class Procedure
    {
        public string Name ="";
        public int ProcedureID;
        public List<Step> Steps;
        public string DatasourceKey;
        public Data.Procedure dataProcedure;
        public Procedure(int procedureID,string datasourcekey, string name, List<Step> steps)
        {
            ProcedureID = procedureID;
            Name = name;
            Steps = steps;
            DatasourceKey = datasourcekey;
        }
        public Procedure() { }
        public Procedure(int procedureID,string datasourcekey)
        { 
            ProcedureID = procedureID;
            DatasourceKey = datasourcekey;
            Init();
        }

        private void Init() {
            DataTable dt = new DataTable();
            dt= dataProcedure.GetProcedure(this.ProcedureID);
          var e=  dt.AsEnumerable().Select(g => new Procedure
            {
                ProcedureID = g.IsNull("ProcedureID") ? 0 : g.Field<int>("ProcedureID"),
                Name = g.IsNull("Name") ? "" : g.Field<string>("Name"),
                DatasourceKey = g.IsNull("DatasourceKey") ? "" : g.Field<string>("DatasourceKey"),
                Steps = g.IsNull("Steps") ? new List<Step>() : Newtonsoft.Json.JsonConvert.DeserializeObject<List<Step>>(g.Field<string>("Steps"))                             
            }).FirstOrDefault();

            ProcedureID = e.ProcedureID;
            Name = e.Name;
            DatasourceKey = e.DatasourceKey;
            Steps = e.Steps;
        }

        public bool Save() {
            if (this.ProcedureID > 0)
            {
                if (dataProcedure.Save(this.ProcedureID, this.Name, Newtonsoft.Json.JsonConvert.SerializeObject(this.Steps), this.DatasourceKey))
                {
                    return true;
                }
                else return false;
            }
            else {
                if (dataProcedure.Save(this.Name, Newtonsoft.Json.JsonConvert.SerializeObject(this.Steps), this.DatasourceKey) > 0)
                {
                    return true;
                }
                else return false;
            }            
        }
        public bool Delete() {
            if (dataProcedure.Delete(this.ProcedureID, this.DatasourceKey))
            {
                return true;
            }
            else return false;
        }

    }
}
