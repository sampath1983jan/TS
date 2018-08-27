using System;
using TechSharpy.Data;
using TechSharpy.Data.ABS;
using System.Data;
namespace TechSharpy.FormBuilder.Data
{
    public class Form
    {
        DataTable dtResult;
        Query iQuery;
        TechSharpy.Data.DataBase rd;
        public Form()
        {
            try
            {
                rd = new DataBase();
                dtResult = new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Save(int formComponentID,string name,string code,string description,string category) {
            var nextid = rd.getNextID("form");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("FormID", "s_form", FieldType._Number, "", nextid.ToString())
                .AddField("Name", "s_form", FieldType._String, "", name.ToString())
                .AddField("code", "s_form", FieldType._String, "", code.ToString())
                .AddField("Description", "s_form", FieldType._String, "", description.ToString())
                .AddField("Category", "s_form", FieldType._String , "", category.ToString())
                .AddField("FormComponentID", "s_form", FieldType._Number, "", formComponentID.ToString())
                .AddField("LastUPD", "s_form", FieldType._DateTime , "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return nextid ;
            }
            else return -1;            
        }
        public bool Update(int formid, int formcomponentid, string name, string code, string description, string category) {
            iQuery = new QueryBuilder(QueryType._Update)
                // .AddField("FormID", "s_form", FieldType._Number, "", nextid.ToString())
                .AddField("Name", "s_form", FieldType._String, "", name.ToString())
                .AddField("code", "s_form", FieldType._String, "", code.ToString())
                .AddField("Description", "s_form", FieldType._String, "", description.ToString())
                .AddField("Category", "s_form", FieldType._String, "", category.ToString())
                .AddField("FormComponentID", "s_form", FieldType._Number, "", formcomponentid.ToString())
                .AddField("LastUPD", "s_form", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "s_form", "FormID", FieldType._Number, formid.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Delete(int formid) {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*","s_form")
                   .AddWhere(0, "s_form", "FormID", FieldType._Number, formid.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public DataTable GetForm(int formID)
        {
            iQuery = new QueryBuilder(QueryType._Select )
           .AddField("Name", "s_form", FieldType._String, "")
                .AddField("code", "s_form", FieldType._String, "")
                .AddField("Description", "s_form", FieldType._String)
                .AddField("Category", "s_form", FieldType._String)
                .AddField("FormComponentID", "s_form", FieldType._Number)               
                .AddField("elementid", "s_fieldelement")
                .AddField("elementtype", "s_fieldelement")
                .AddField("title", "s_fieldelement")
                .AddField("code", "s_fieldelement")
                .AddField("Mode", "s_fieldelement")
                .AddField("elementattribute", "s_fieldelement")
                .AddJoin ("s_form","formid",JoinType._InnerJoin, "s_fieldelement","formid")
                .AddWhere(0, "s_form", "FormID", FieldType._Number, formID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }
    }
}
