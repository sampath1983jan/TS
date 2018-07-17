using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechSharpy.Data.QueryAttribute;

namespace TechSharpy.Data.ABS
{
    public abstract class TQuery
    {
        public Table Table;
        public List<Field> Fields;
        public TQueryType Type;
        public abstract string toString();
        public abstract bool ValidateSchema();

        public TQuery(TQueryType pType)
        {
            this.Type = pType;
            Fields = new List<Field>();
        }
        public TQuery TableName(string tbName)
        {
            this.Table = new Table(tbName, "");
            return this;
        }
        public int GetFieldCount()
        {
            return this.Fields.Count;
        }

        public TQuery AddField(string FieldName, bool isKeyField, bool isUniqueField, FieldType pFieldType, bool pAcceptNull, string pReName = "")
        {
            Field fld = new Field(FieldName, this.Table.TableName, "", "", 0, pFieldType, 0);
            fld.IsKeyField = isKeyField;
            fld.IsUnique = isUniqueField;
            fld.IsRequired = pAcceptNull;
            fld.ReName = pReName;
            this.Fields.Add(fld);
            return this;
        }

    }


}
