using System;
using TechSharpy.Data;
using TechSharpy.Data.ABS;
using System.Data;

namespace TechSharpy.FormBuilder.Data
{
    public class FormElement
    {
        DataTable dtResult;
        Query iQuery;
        TechSharpy.Data.DataBase rd;
        public FormElement()
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

        public int Save(int formID, int elementtype, string title, string code, int mode, string elementattribute
            ,int componentid) {
            int nextid = rd.getNextID("formelement");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("elementid", "s_formelement", FieldType._Number, "", nextid.ToString())
                .AddField("formID", "s_formelement", FieldType._Number, "", formID.ToString())
                .AddField("elementtype", "s_formelement", FieldType._Number, "", elementtype.ToString())
                .AddField("title", "s_formelement", FieldType._String, "", title.ToString())
                .AddField("code", "s_formelement", FieldType._String , "", code.ToString())
                .AddField("Mode", "s_formelement", FieldType._Number, "", mode.ToString())
                .AddField("componentid", "s_formelement", FieldType._Number, "", componentid.ToString())
                .AddField("elementattribute", "s_formelement",FieldType._Text,"",elementattribute)
                .AddField("LastUPD", "s_form", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return nextid;
            }
            else return -1;
        }
        public bool Update(int formID,int formelementid, int elementtype, string title, string code, int mode, string elementattribute) {
            iQuery = new QueryBuilder(QueryType._Update)             
               .AddField("elementtype", "s_formelement", FieldType._Number, "", elementtype.ToString())
               .AddField("title", "s_formelement", FieldType._String, "", title.ToString())
               .AddField("code", "s_formelement", FieldType._String, "", code.ToString())
               .AddField("Mode", "s_formelement", FieldType._Number, "", mode.ToString())
               .AddField("elementattribute", "s_formelement", FieldType._Text, "", elementattribute)
               .AddField("LastUPD", "s_form", FieldType._DateTime, "", DateTime.Now.ToString())
               .AddWhere(0, "s_formelement", "elementid", FieldType._Number, formelementid.ToString(),Condition._And)
               .AddWhere(0, "s_formelement", "FormID", FieldType._Number, formID.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool Delete(int formid, int formelementid)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "s_formelement")
                  .AddWhere(0, "s_formelement", "elementid", FieldType._Number, formelementid.ToString(), Condition._And)
           .AddWhere(0, "s_formelement", "FormID", FieldType._Number, formid.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

         

    }
}
