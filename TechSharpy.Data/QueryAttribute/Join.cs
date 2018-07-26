using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data.QueryAttribute
{
    public class Join
    {
        public string TableName;
        public string FieldName;
        public JoinType Type;
        public Condition condition;
        public string JoinTable;
        public string JoinField;
        //public Operator Operation;
        public Join(string pTableName, string pFieldName, JoinType pjoin, string pJoinTable,
            string pJoinField, Condition pCondition = Condition._None)
        {
            this.TableName = pTableName;
            this.FieldName = pFieldName;
            this.Type = pjoin;
            this.JoinTable = pJoinTable;
            this.JoinField = pJoinField;
            this.condition = pCondition;
        }
    }
}
