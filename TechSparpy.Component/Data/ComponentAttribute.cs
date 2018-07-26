using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Component.Data
{
    public class ComponentAttribute
    {
         private DataTable Result;
        DataBase rd;
        Query iQuery;
        public ComponentAttribute()
        {
            Result = new DataTable();
            try
            {               
                rd = new DataBase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveComponentAttribute(int FieldInstanceID, Attributes.AttributeType attributeType,
            string componentKey,
            int cryptography,string regExpression,string parentComponent,string parentAttribute,UsageType usageFieldType) {
             int inext = rd.getNextID("ComponentAttribute");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("ComponentAttributeID", "C_ComponentAttribute", FieldType._Number, "", inext.ToString())
                .AddField("FieldInstanceID", "C_ComponentAttribute", FieldType._Number,"",FieldInstanceID.ToString())
                .AddField("Attributetype", "C_ComponentAttribute", FieldType._Number, "", ((int)attributeType).ToString())
                .AddField("componentKey", "C_ComponentAttribute", FieldType._String, "", (componentKey).ToString())
                .AddField("cryptography", "C_ComponentAttribute", FieldType._Number, "", (cryptography).ToString())
                .AddField("regExpression", "C_ComponentAttribute", FieldType._String, "", (regExpression).ToString())
                .AddField("parentComponent", "C_ComponentAttribute", FieldType._String, "", (parentComponent).ToString())
                .AddField("parentAttribute", "C_ComponentAttribute", FieldType._String, "", (parentAttribute).ToString())
                .AddField("UsageFieldType", "C_ComponentAttribute", FieldType._Number, "", ((int)usageFieldType).ToString())
                .AddField("LastUPD", "C_ComponentAttribute", FieldType._DateTime, "", (DateTime.Now.ToString()));
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;            
        }

        public bool SaveComponentAttribute(int componentAttribute,int FieldInstanceID, 
            Attributes.AttributeType attributeType,
            string componentKey,
            int cryptography, string regExpression, string parentComponent, int parentAttributes,UsageType usageFieldType) {
            iQuery = new QueryBuilder(QueryType._Update)
               .AddField("Attributetype", "C_ComponentAttribute", FieldType._Number, "", ((int)attributeType).ToString())
               // .AddField("componentKey", "C_ComponentAttribute", FieldType._String, "", (componentKey).ToString())
                .AddField("cryptography", "C_ComponentAttribute", FieldType._Number, "", (cryptography).ToString())
                .AddField("regExpression", "C_ComponentAttribute", FieldType._String, "", (regExpression).ToString())
                .AddField("parentComponent", "C_ComponentAttribute", FieldType._String, "", (parentComponent).ToString())
                .AddField("parentAttribute", "C_ComponentAttribute", FieldType._Number, "", (parentAttributes).ToString())
                .AddField("UsageFieldType", "C_ComponentAttribute", FieldType._Number, "", ((int)usageFieldType).ToString())
                 .AddField("LastUPD", "C_ComponentAttribute", FieldType._DateTime, "", (DateTime.Now.ToString()))
                 .AddWhere(0, "C_ComponentAttribute", "ComponentAttributeID", FieldType._Number,Operator._Equal,componentAttribute.ToString(),
                 Condition._None);
            return true;
        }

        public bool Delete(string componentKey, int attributeID) {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "C_ComponentAttribute")
                .AddWhere(0, "C_ComponentAttribute", "componentKey", FieldType._String, Operator._Equal, componentKey.ToString(),
                 Condition._And)
                 .AddWhere(0, "C_ComponentAttribute", "ComponentAttributeID", FieldType._Number, Operator._Equal, attributeID.ToString(),
                 Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;            
        }

        public bool Delete(string componentKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "C_ComponentAttribute")                 
                 .AddWhere(0, "C_ComponentAttribute", "ComponentAttributeID", FieldType._String, Operator._Equal, componentKey.ToString(),
                 Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        
        public DataTable GetComponentAttribute(string componentKey,int attributeID) {
            iQuery = new QueryBuilder(QueryType._Select)
            .AddField("ComponentAttributeID", "C_ComponentAttribute", FieldType._Number, "")
            .AddField("FieldInstanceID", "C_ComponentAttribute", FieldType._Number)
            .AddField("Attributetype", "C_ComponentAttribute", FieldType._Number)
            .AddField("componentKey", "C_ComponentAttribute", FieldType._String)
            .AddField("cryptography", "C_ComponentAttribute", FieldType._Number)
            .AddField("regExpression", "C_ComponentAttribute", FieldType._String, "")
            .AddField("parentComponent", "C_ComponentAttribute", FieldType._String)
            .AddField("parentAttribute", "C_ComponentAttribute", FieldType._Number)
            .AddField("UsageFieldType", "C_ComponentAttribute", FieldType._Number)
            .AddField("LastUPD", "C_ComponentAttribute", FieldType._DateTime)
            .AddWhere(0, "C_ComponentAttribute", "ComponentAttributeID", FieldType._Number, Operator._Equal, attributeID.ToString(), Condition._And )
            .AddWhere(0, "C_ComponentAttribute", "componentKey", FieldType._String, Operator._Equal, componentKey, Condition._None);
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }

        public DataTable GetComponentAttributes(string componentKey) {
            iQuery = new QueryBuilder(QueryType._Select)
             .AddField("ComponentAttributeID", "C_ComponentAttribute", FieldType._Number, "")
             .AddField("FieldInstanceID", "C_ComponentAttribute", FieldType._Number)
             .AddField("Attributetype", "C_ComponentAttribute", FieldType._Number)
             .AddField("componentKey", "C_ComponentAttribute", FieldType._String)
             .AddField("cryptography", "C_ComponentAttribute", FieldType._Number)
             .AddField("regExpression", "C_ComponentAttribute", FieldType._String, "")
             .AddField("parentComponent", "C_ComponentAttribute", FieldType._String)
             .AddField("parentAttribute", "C_ComponentAttribute", FieldType._Number)
              .AddField("UsageFieldType", "C_ComponentAttribute", FieldType._Number)
             .AddField("LastUPD", "C_ComponentAttribute", FieldType._DateTime)
             .AddWhere(0, "C_ComponentAttribute", "componentKey", FieldType._String, Operator._Equal, componentKey, Condition._None);
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;            
        }
    }


}
