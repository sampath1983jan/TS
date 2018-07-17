using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data.QueryAttribute
{
    public class Field
    {
        public string Name;
        public string TableName;
        public string Alias;
        public string Value;
        public Operator Operation;
        public FieldType Type;
        public Aggregate FieldAggregate;
        public int Size;
        public bool IsUnique;
        public bool IsKeyField;
        public bool IsRequired;
        public string ReName = "";
        public Field(string pfieldName, string pTableName, string pAliasName,
            string pValue, Operator pOperator, FieldType pType, Aggregate pAgg = Aggregate._None)
        {
            this.Name = pfieldName;
            this.TableName = pTableName;
            this.Alias = pAliasName;
            this.Value = pValue;
            this.Operation = pOperator;
            this.Type = pType;
            this.FieldAggregate = pAgg;
        }
        public string GetNullType()
        {
            if (this.IsKeyField)
            {
                return "NOT NULL";
            }
            else if (this.IsRequired)
            {
                return "NOT NULL";
            }
            else
            {
                return "NULL";
            }
        }
        public string GetBaseDataType()
        {
            if (this.Type == FieldType._Number || this.Type == FieldType._Lookup
                || this.Type == FieldType._Entity)
            {
                return "INT";
            }
            else if (this.Type == FieldType._String)
            {
                if (this.Size == 0)
                {
                    this.Size = 255;
                }
                return "VARCHAR(" + this.Size + ")";
            }
            else if (this.Type == FieldType._Text)
            {
                return "TEXT";
            }
            else if (this.Type == FieldType._Currency || this.Type == FieldType._Decimal || this.Type == FieldType._Double)
            {
                return "DOUBLE(18,3)";
            }
            else if (this.Type == FieldType._File || this.Type == FieldType._Image)
            {
                return "LONGTEXT";
            }
            else if (this.Type == FieldType._Date || this.Type == FieldType._DateTime || this.Type == FieldType._Time)
            {
                return "DATETIME";
            }
            else if (this.Type == FieldType._Question)
            {
                return "BIT";
            }
            else
            {
                return "TEXT";
            }
        }
    }
}
