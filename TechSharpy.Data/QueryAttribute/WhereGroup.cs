using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data.QueryAttribute
{
    public class WhereGroup
    {
        public List<WhereCase> whereCases;
        public Int16 GroupIndex;
        public Condition condition;
        public WhereGroup()
        {
            this.whereCases = new List<WhereCase>();
            this.condition = new Condition();
        }
    }

    public class WhereCase
    {
        public int GroupIndex;
        public string TableName;
        public string FieldName;
        public FieldType Type;
        public JoinType Join;
        public Condition condition;
        public string JoinTable;
        public string JoinField;
        public string ConditionValue;
        public Operator Operation;


        public WhereCase(int pGroupIndex, string pTableName, string pFieldName, string pJoinTable, string pJoinField,
            JoinType pJoinType = JoinType._InnerJoin, Condition pCondition = Condition._And)
        {
            this.GroupIndex = pGroupIndex;
            this.TableName = pTableName;
            this.FieldName = pFieldName;
            this.Join = pJoinType;
            this.condition = pCondition;
            this.JoinField = pJoinField;
            this.JoinTable = pJoinTable;

        }
        public WhereCase(int pGroupIndex, string pTableName, string pFieldName, FieldType pType, Operator pOperator = Operator._Equal,
             string value = "", Condition pCondition = Condition._And)
        {
            this.GroupIndex = pGroupIndex;
            this.TableName = pTableName;
            this.FieldName = pFieldName;
            this.Join = JoinType._InnerJoin;
            this.condition = pCondition;
            this.Type = pType;
            this.JoinTable = "";
            this.JoinField = "";
            this.ConditionValue = value;
            this.Operation = pOperator;

        }

    }
}
