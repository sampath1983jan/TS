using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechSharpy.Data.QueryAttribute;

namespace TechSharpy.Data
{
    public enum TQueryType { 
    _Create,
        _AlterTable,
        _AlterTableColumnDataType,
        _RemoveTableColumn,
        _AlterColumnName,
    }
  
   public class MYSQLTQueryBuilder:ABS.TQuery
    {
       private string Grouper = "`";
        public MYSQLTQueryBuilder(TQueryType pType) : base(pType) {

        }                 
      public override string toString()
        {
            string createtempate = "Create Table {0} ({1})";
            string addcolumntemplate = "ALTER TABLE {0} {1}";
            string altercolumntemplate = "ALTER TABLE {0} {1}";

            StringBuilder sb = new StringBuilder();
            StringBuilder sbField = new StringBuilder();
            if (this.Type == TQueryType._Create)
            {

                string keyfield = "";
                foreach (Field f in Fields)
                {
                    sbField.Append("," + Grouper + f.Name + Grouper + " " + f.GetBaseDataType() + " " + f.GetNullType());
                    if (f.IsKeyField == true)
                    {
                        keyfield = keyfield + "," + Grouper + f.Name + Grouper;
                    }
                }
                if (keyfield != "")
                {
                    if (keyfield.StartsWith(","))
                    {
                        keyfield = keyfield.Substring(1);
                    }
                    keyfield = ",PRIMARY KEY(" + keyfield + ")";
                }
                sb.AppendFormat(createtempate, this.Table.TableName.Replace(" ", "_"), sbField.ToString().Substring(1) + keyfield);

            }
            else if (this.Type == TQueryType._AlterTableColumnDataType)
            {
                foreach (Field f in Fields)
                {
                    sbField.Append("," + " MODIFY COLUMN " + Grouper + f.Name + Grouper + " " + f.GetBaseDataType() + " " + f.GetNullType());
                }
                sb.AppendFormat(altercolumntemplate, this.Table.TableName.Replace(" ", "_"), sbField.ToString().Substring(1));

            }
            else if (this.Type == TQueryType._AlterTable)
            {

                foreach (Field f in Fields)
                {
                    sbField.Append("," + " ADD COLUMN " + Grouper + f.Name + Grouper + " " + f.GetBaseDataType() + " " + f.GetNullType());
                }
                sb.AppendFormat(addcolumntemplate, this.Table.TableName.Replace(" ", "_"), sbField.ToString().Substring(1));

            }
            else if (this.Type == TQueryType._RemoveTableColumn)
            {
                foreach (Field f in Fields)
                {
                    sbField.Append("," + " drop " + Grouper + f.Name + Grouper);
                }
                sb.AppendFormat(altercolumntemplate, this.Table.TableName.Replace(" ", "_"), sbField.ToString().Substring(1));

            }
            else if (this.Type == TQueryType._AlterColumnName)
            {
                foreach (Field f in Fields)
                {
                    sbField.Append("," + " CHANGE  " + Grouper + f.Name + Grouper + " " + Grouper + f.ReName + Grouper + " " + f.GetBaseDataType());
                }
                sb.AppendFormat(altercolumntemplate, this.Table.TableName.Replace(" ", "_"), sbField.ToString().Substring(1));
            }
            return sb.ToString();

            //  throw new NotImplementedException();
        }

        public override bool ValidateSchema()
        {
            return true;
          //  throw new NotImplementedException();
        }
    }
}