using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    public class Privilege:DataAccess
    {
        DataTable dtResult;
        Query iQuery;
        public Privilege()
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

        public bool SaveDataSourcePrivilege(int Userkey,string datasourcekey, bool View, bool Add, bool Delete,bool change) {
            iQuery = new Query(QueryType._Select)
                .AddField("Userkey", "s_DSPrivilege")
                .AddField("datasourcekey", "s_DSprivilege")
                .AddField("view", "s_DSprivilege")
                .AddField("add", "s_DSprivilege")
                .AddField("remove", "s_DSprivilege")
                .AddWhere(0, "s_DSprivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_DSprivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey, Condition._None);
            dtResult = this.GetData(iQuery);

            if (dtResult.Rows.Count > 0)
            {
                iQuery = new Query(QueryType._Update)
                .AddField("view", "s_DSprivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_DSprivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_DSprivilege", FieldType._Question, "", Delete.ToString())
                 .AddField("change", "s_DSprivilege", FieldType._Question, "", change.ToString())
                .AddWhere(0, "s_DSprivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_DSprivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey, Condition._None);
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }
            else {
                iQuery = new Query(QueryType._Insert)
               .AddField("Userkey", "s_DSPrivilege", FieldType._String, "", Userkey.ToString())
               .AddField("datasourcekey", "s_DSprivilege", FieldType._String, "", datasourcekey)
              .AddField("view", "s_DSprivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_DSprivilege", FieldType._Question, "", Add.ToString())
                 .AddField("change", "s_DSprivilege", FieldType._Question, "", change.ToString())
                .AddField("remove", "s_DSprivilege", FieldType._Question, "", Delete.ToString())
                .AddField("LastUPD", "s_DSprivilege", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0) {
                    return true;
                } else return false;
            }

        }

        public bool SaveProcedurePrivilege(int Userkey,string datasourcekey, 
             bool View, bool Add, bool Delete,bool change)
        {
            iQuery = new Query(QueryType._Select)
                .AddField("Userkey", "s_ProcedurePrivilege")
                .AddField("datasourcekey", "s_ProcedurePrivilege")
                .AddField("view", "s_ProcedurePrivilege")
                .AddField("add", "s_ProcedurePrivilege")
                .AddField("remove", "s_ProcedurePrivilege")
                .AddWhere(0, "s_ProcedurePrivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_ProcedurePrivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
              //  .AddWhere(0, "s_ProcedurePrivilege", "ProcedureID", FieldType._Number, Operator._Equal, procedureID.ToString(), Condition._None);
            dtResult = this.GetData(iQuery);

            if (dtResult.Rows.Count > 0)
            {
                iQuery = new Query(QueryType._Update)
                .AddField("view", "s_ProcedurePrivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_ProcedurePrivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_ProcedurePrivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_ProcedurePrivilege", FieldType._Question, "", change.ToString())
                .AddWhere(0, "s_ProcedurePrivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_ProcedurePrivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
              //  .AddWhere(0, "s_ProcedurePrivilege", "ProcedureID", FieldType._Number, Operator._Equal, procedureID.ToString(), Condition._None);
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                iQuery = new Query(QueryType._Insert)
               .AddField("Userkey", "s_ProcedurePrivilege", FieldType._String, "", Userkey.ToString())
              // .AddField("ProcedureID", "s_ProcedurePrivilege", FieldType._String, "", procedureID.ToString())
               .AddField("datasourcekey", "s_ProcedurePrivilege", FieldType._String, "", datasourcekey.ToString())
                .AddField("change", "s_ProcedurePrivilege", FieldType._Question, "", change.ToString())
              .AddField("view", "s_ProcedurePrivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_ProcedurePrivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_ProcedurePrivilege", FieldType._Question, "", Delete.ToString())
                .AddField("LastUPD", "s_ProcedurePrivilege", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }

        }

        public bool SaveFunctionPrivilege(int Userkey, string datasourcekey, 
            bool View, bool Add, bool Delete,bool change)
        {
            iQuery = new Query(QueryType._Select)
                .AddField("Userkey", "s_FunctionPrivilege")
                .AddField("datasourcekey", "s_FunctionPrivilege")
                .AddField("view", "s_FunctionPrivilege")
                .AddField("add", "s_FunctionPrivilege")
                .AddField("remove", "s_FunctionPrivilege")
                .AddWhere(0, "s_FunctionPrivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_FunctionPrivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
               // .AddWhere(0, "s_FunctionPrivilege", "FunctionID", FieldType._Number, Operator._Equal, FunctionID.ToString(), Condition._None);
            dtResult = this.GetData(iQuery);

            if (dtResult.Rows.Count > 0)
            {
                iQuery = new Query(QueryType._Update)
                .AddField("view", "s_FunctionPrivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_FunctionPrivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_FunctionPrivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_FunctionPrivilege", FieldType._Question, "", change.ToString())
                .AddWhere(0, "s_FunctionPrivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_FunctionPrivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
                //.AddWhere(0, "s_FunctionPrivilege", "FunctionID", FieldType._Number, Operator._Equal, FunctionID.ToString(), Condition._None);
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                iQuery = new Query(QueryType._Insert)
               .AddField("Userkey", "s_FunctionPrivilege", FieldType._String, "", Userkey.ToString())
            //   .AddField("FunctionID", "s_FunctionPrivilege", FieldType._String, "", FunctionID.ToString())
               .AddField("datasourcekey", "s_FunctionPrivilege", FieldType._String, "", datasourcekey.ToString())
              .AddField("view", "s_FunctionPrivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_FunctionPrivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_FunctionPrivilege", FieldType._Question, "", Delete.ToString())
                  .AddField("change", "s_FunctionPrivilege", FieldType._Question, "", change.ToString())
                .AddField("LastUPD", "s_FunctionPrivilege", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }

        }

        public bool SaveEntityPrivilege(int Userkey, string datasourcekey,  bool View, bool Add, bool Delete,bool change)
        {
            iQuery = new Query(QueryType._Select)
                .AddField("Userkey", "s_entityPrivilege")
                .AddField("datasourcekey", "s_entityPrivilege")
                .AddField("view", "s_entityPrivilege")
                .AddField("add", "s_entityPrivilege")
                .AddField("remove", "s_entityPrivilege")
                .AddWhere(0, "s_entityPrivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_entityPrivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
               // .AddWhere(0, "s_entityPrivilege", "EntityID", FieldType._Number, Operator._Equal, EntityID.ToString(), Condition._None);
            dtResult = this.GetData(iQuery);

            if (dtResult.Rows.Count > 0)
            {
                iQuery = new Query(QueryType._Update)
                .AddField("view", "s_entityPrivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_entityPrivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_entityPrivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_entityPrivilege", FieldType._Question, "", change.ToString())
                .AddWhere(0, "s_entityPrivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_entityPrivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
                //.AddWhere(0, "s_entityPrivilege", "EntityID", FieldType._Number, Operator._Equal, EntityID.ToString(), Condition._None);
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                iQuery = new Query(QueryType._Insert)
               .AddField("Userkey", "s_entityPrivilege", FieldType._String, "", Userkey.ToString())
              // .AddField("EntityID", "s_entityPrivilege", FieldType._String, "", EntityID.ToString())
               .AddField("datasourcekey", "s_entityPrivilege", FieldType._String, "", datasourcekey.ToString())
              .AddField("view", "s_entityPrivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_entityPrivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_entityPrivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_entityPrivilege", FieldType._Question, "", change.ToString())
                .AddField("LastUPD", "s_entityPrivilege", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }

        }

        public bool SaveJobPrivilege(int Userkey, string datasourcekey,
            bool View, bool Add, bool Delete,bool change)
        {
            iQuery = new Query(QueryType._Select)
                .AddField("Userkey", "s_jobprivilege")
                .AddField("datasourcekey", "s_jobprivilege")
                .AddField("view", "s_jobprivilege")
                .AddField("add", "s_jobprivilege")
                .AddField("remove", "s_jobprivilege")
                .AddWhere(0, "s_jobprivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_jobprivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
              //  .AddWhere(0, "s_jobprivilege", "JobID", FieldType._Number, Operator._Equal, JobID.ToString(), Condition._None);
            dtResult = this.GetData(iQuery);

            if (dtResult.Rows.Count > 0)
            {
                iQuery = new Query(QueryType._Update)
                .AddField("view", "s_jobprivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_jobprivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_jobprivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_jboprivilege", FieldType._Question, "", change.ToString())
                .AddWhere(0, "s_jobprivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_jobprivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
                //.AddWhere(0, "s_jobprivilege", "JobID", FieldType._Number, Operator._Equal, JobID.ToString(), Condition._None);
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                iQuery = new Query(QueryType._Insert)
               .AddField("Userkey", "s_jobprivilege", FieldType._String, "", Userkey.ToString())
            //   .AddField("JobID", "s_jobprivilege", FieldType._String, "", JobID.ToString())
               .AddField("datasourcekey", "s_jobprivilege", FieldType._String, "", datasourcekey.ToString())
              .AddField("view", "s_jobprivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_jobprivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_jobprivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_jobprivilege", FieldType._Question, "", change.ToString())
                .AddField("LastUPD", "s_jobprivilege", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }

        }
        
        public bool SaveModelPrivilege(int Userkey, string datasourcekey, 
         bool View, bool Add, bool Delete, bool change)
        {
            iQuery = new Query(QueryType._Select)
                .AddField("Userkey", "s_modelprivilege")
                .AddField("datasourcekey", "s_modelprivilege")
                .AddField("view", "s_modelprivilege")
                .AddField("add", "s_modelprivilege")
                .AddField("remove", "s_modelprivilege")
                .AddWhere(0, "s_modelprivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_modelprivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
             //   .AddWhere(0, "s_modelprivilege", "modelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);
            dtResult = this.GetData(iQuery);

            if (dtResult.Rows.Count > 0)
            {
                iQuery = new Query(QueryType._Update)
                .AddField("view", "s_modelprivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_modelprivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_modelprivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_jboprivilege", FieldType._Question, "", change.ToString())
                .AddWhere(0, "s_modelprivilege", "Userkey", FieldType._Number, Operator._Equal, Userkey.ToString(), Condition._And)
                .AddWhere(0, "s_modelprivilege", "datasourcekey", FieldType._Number, Operator._Equal, datasourcekey.ToString(), Condition._None);
               // .AddWhere(0, "s_modelprivilege", "modelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                iQuery = new Query(QueryType._Insert)
               .AddField("Userkey", "s_modelprivilege", FieldType._String, "", Userkey.ToString())
               //.AddField("modelID", "s_modelprivilege", FieldType._String, "", modelID.ToString())
               .AddField("datasourcekey", "s_modelprivilege", FieldType._String, "", datasourcekey.ToString())
              .AddField("view", "s_modelprivilege", FieldType._Question, "", View.ToString())
                .AddField("add", "s_modelprivilege", FieldType._Question, "", Add.ToString())
                .AddField("remove", "s_modelprivilege", FieldType._Question, "", Delete.ToString())
                .AddField("change", "s_modelprivilege", FieldType._Question, "", change.ToString())
                .AddField("LastUPD", "s_modelprivilege", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return true;
                }
                else return false;
            }

        }


          

    }
}
