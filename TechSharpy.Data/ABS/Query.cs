using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechSharpy.Data.QueryAttribute;

namespace TechSharpy.Data.ABS
{
    [Serializable]
    public abstract class Query
    {

        public List<Field> QueryFields;
        public List<Table> Tables;
        public List<Join> Joins;
        public List<WhereGroup> WhereGroups;
        public List<SortOrder> Ordersby;
        public QueryType Type;
        internal abstract bool ValidateSchema();
        internal abstract string toString();
        internal abstract string getOperator(Operator opt, string fval);

        public Query AddTable(string pTableName, string pAliasName = "")
        {
            if (!isTableExist(pTableName) && !isTableExist(pAliasName))
                this.Tables.Add(new Table(pTableName, pAliasName));
            return this;
        }

        public Query AddField(string pFieldName, string TableName, FieldType pType, string AliasName = "", string Value = "", Operator pOperator = Operator._Equal, Aggregate Agg = Aggregate._None)
        {
            QueryFields.Add(new Field(pFieldName, TableName, AliasName, Value, pOperator, pType, Agg));
            if (!isTableExist(TableName))
                this.Tables.Add(new Table(TableName));
            return this;
        }
        public Query AddField(string pFieldNames, string TableName = "", string AliasName = "")
        {

            string[] fields;
            string[] separators = { "," };
            fields = pFieldNames.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < fields.Length; i++)
            {
                QueryFields.Add(new Field(fields[i].ToString(), TableName, AliasName, "", Operator._Equal, FieldType._String));
            }
            if (TableName != "")
            {
                if (!isTableExist(TableName))
                    this.Tables.Add(new Table(TableName));
            }

            return this;
        }

        public Query AddJoin(string pTableName, string pFieldName, JoinType pjoin, string pJoinTable, string pJoinField, Condition pCondition = Condition._None)
        {
            Joins.Add(new Join(pTableName, pFieldName, pjoin, pJoinTable, pJoinField, pCondition));
            return this;
        }


        public Query AddWhere(int pGroupIndex, string pTableName, string pFieldName, FieldType pType, string value = "", Condition condition=Condition._None)
        {
            WhereGroup wg;

            if (this.WhereGroups.Count > pGroupIndex)
            {
                wg = this.WhereGroups[pGroupIndex];
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pType, Operator._Equal, value, condition));
            }
            else
            {
                wg = new WhereGroup();
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pType, Operator._Equal, value, condition));
                this.WhereGroups.Add(wg);
            }
            return this;
        }

        public Query AddWhere(int pGroupIndex, string pTableName, string pFieldName, FieldType pType,  string value = "")
        {
            WhereGroup wg;

            if (this.WhereGroups.Count > pGroupIndex)
            {
                wg = this.WhereGroups[pGroupIndex];
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pType, Operator._Equal, value, Condition._None));
            }
            else
            {
                wg = new WhereGroup();
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pType, Operator._Equal, value, Condition._None));
                this.WhereGroups.Add(wg);
            }
            return this;
        }

        public Query AddWhere(int pGroupIndex, string pTableName, string pFieldName, FieldType pType, Operator pOperator = Operator._Equal, string value = "", Condition pCondition = Condition._And)
        {
            WhereGroup wg;

            if (this.WhereGroups.Count > pGroupIndex)
            {
                wg = this.WhereGroups[pGroupIndex];
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pType, pOperator, value, pCondition));
            }
            else
            {
                wg = new WhereGroup();
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pType, pOperator, value, pCondition));
                this.WhereGroups.Add(wg);
            }
            return this;
        }

        public Query AddWhere(int pGroupIndex, string pTableName, string pFieldName, string pJoinTable, string pJoinField, JoinType pJoinType = JoinType._InnerJoin, Condition pCondition = Condition._And)
        {
            WhereGroup wg;
            if (this.WhereGroups.Count > pGroupIndex)
            {
                wg = this.WhereGroups[pGroupIndex];
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pJoinTable, pJoinField,
                pJoinType, pCondition));
            }
            else
            {
                wg = new WhereGroup();
                wg.whereCases.Add(new WhereCase(pGroupIndex, pTableName, pFieldName, pJoinTable, pJoinField,
                pJoinType, pCondition));
                this.WhereGroups.Add(wg);
            }
            return this;
        }

        public Query AddSortOrder(string pTableName, string pFieldName, SortType pOrder)
        {
            this.Ordersby.Add(new SortOrder(pTableName, pFieldName, pOrder));
            return this;
        }


        public Query(QueryType type)
        {
            Type = type;
            QueryFields = new List<Field>();
            Tables = new List<Table>();
            WhereGroups = new List<WhereGroup>();
            Ordersby = new List<SortOrder>();
            Joins = new List<Join>();

        }

        public string getTableAlias(string tbName)
        {
            for (int itbl = 0; itbl < this.Tables.Count; itbl++)
            {
                if (this.Tables[itbl].TableName == tbName)
                {
                    //if (this.Tables[itbl].AliasName != "")
                    //{
                    //    return this.Tables[itbl].AliasName;
                    //}
                    //else {
                    //    return this.Tables[itbl].TableName;
                    //}
                    return this.Tables[itbl].AliasName != "" ? this.Tables[itbl].AliasName : this.Tables[itbl].TableName;
                }
            }
            return tbName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetConnectioin()
        {
            string Connection = ConfigurationManager.AppSettings["ConnectionString"];
            SMRHRT.Services.Security.CryptoProvider cryp = new SMRHRT.Services.Security.CryptoProvider("check your connection");
            Connection = cryp.DecryptString(Connection);
            return Connection;
        }

        public string MakeValidateData(FieldType fldType, string Value, string FieldName)
        {
            if (fldType == FieldType._Number || fldType == FieldType._Double || fldType == FieldType._Decimal
                || fldType == FieldType._Currency || fldType == FieldType._Lookup)
            {
                if (!Value.IsNumeric())
                {
                    //throw new Exception(string.Format("Invalid Data in {0}", FieldName));
                    return Value;
                }
                else
                {
                    return Value;
                }
            }
            else if (fldType == FieldType._Date || fldType == FieldType._DateTime || fldType == FieldType._Time)
            {
                if (!Value.IsDate())
                {
                    throw new Exception(string.Format("Invalid Data in {0}", FieldName));
                }
                else
                {
                    return "'" + Convert.ToDateTime(Value).ToString("yyyy/MM/dd HH:mm:ss") + "'";
                }
            }
            else if (fldType == FieldType._String || fldType == FieldType._Text)
            {
                return "'" + Value.ToString() + "'";
            }
            else if (fldType == FieldType._Question)
            {
                if (!Value.IsBool())
                {
                    throw new Exception(string.Format("Invalid Data in {0}", FieldName));
                }
                else
                {
                    return Boolean.Parse(Value).ToString();
                }
            }
            else if (fldType == FieldType._File || fldType == FieldType._Image)
            {
                return Value;
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public bool isTableExist(string tbName)
        {

            for (int iTblCount = 0; iTblCount < Tables.Count; iTblCount++)
            {
                if (this.Tables[iTblCount].TableName == tbName || this.Tables[iTblCount].AliasName == tbName)
                {
                    return true;

                }
            }
            return false;
        }


    }
}
