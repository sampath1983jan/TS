﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using TechSharpy.Data.QueryAttribute;

namespace TechSharpy.Data
{
    public enum QueryType
    {
        _Select,
        _Insert,
        _Update,
        _Delete
    }
    public enum FieldType
    {
        _Number,
        _Decimal,
        _Currency,
        _Double,
        _String,
        _Lookup,
        _Date,
        _DateTime,
        _Question,
        _Text,
        _Time,
        _File,
        _Image,
        _Entity
    }
    public enum Operator
    {
        _Equal,
        _NotEqual,
        _In,
        _NotIn,
        _Between,
        _Greater,
        _Greaterthan,
        _Less,
        _Lessthan,
        _Contain,
        _StartWidth,
        _EndWidth,

    }
    public enum JoinType{
        _InnerJoin,
        _LeftJoin,
        _RightJoin,
        
    }
    public enum Condition { 
        _None,
        _And,
        _Or
    }
    public enum SortType{
        _Asc,
        _Desc
    }
    public enum Aggregate { 
        _None,
        _Sum,
        _Average,
        _Min,
        _Max,
        _Count

    }

     public static class QueryResource{
         public static string _delete_table_more_exist = "Delete function cannot perform due to more then one table exist in the query.";
      }

     
   public class MYSQLQueryBuilder :ABS.Query
    {     
       
        //public List<Field> QueryFields;
        //public List<Table> Tables;
        //public List<Join> Joins;
        //public List<WhereGroup> WhereGroups;
        //public List<SortOrder> Ordersby;
        private const string Select = "Select {0} from {1} {2}";
        private const string Delete = "Delete from {0}";
        private const string Update = "update {0} set {1} {2}";
        private const string Insert = "Insert Into {0} ({1}) values({2}) ";
        public char FieldSelector = '`';
       /// <summary>
       /// 
       /// </summary>
       /// <param name="queryType"></param>
        public MYSQLQueryBuilder(QueryType queryType):base(queryType) {            
        }                                
        internal override string toString()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (this.Type == QueryType._Select)
            {
                System.Text.StringBuilder sbField = new System.Text.StringBuilder();
                System.Text.StringBuilder sbJoin = new System.Text.StringBuilder();
                System.Text.StringBuilder sbWhereGroup = new System.Text.StringBuilder();
                for (int ifld = 0; ifld < this.QueryFields.Count; ifld++)
                {
                    Field fld = this.QueryFields[ifld];
                    sbField.Append("," + FieldSelector + fld.TableName + FieldSelector + "." + fld.Name);
                }
                string fd = "";
                fd = sbField.ToString();
                if (fd.StartsWith(","))
                {
                    fd = fd.Substring(1);
                }
                for (int ijoin = 0; ijoin < this.Joins.Count; ijoin++)
                {
                    Join jn = this.Joins[ijoin];
                    string joinType = "";
                    if (jn.Type == JoinType._InnerJoin)
                    {
                        joinType = " Inner Join ";

                    }
                    else if (jn.Type == JoinType._LeftJoin)
                    {
                        joinType = " Left Join ";
                    }
                    else if (jn.Type == JoinType._RightJoin)
                    {
                        joinType = " Right Join ";
                    }

                    if (ijoin == 0)
                    {
                        sbJoin.Append(FieldSelector + jn.TableName + FieldSelector + " " + getTableAlias(jn.TableName) + joinType
                            + FieldSelector + getTableAlias(jn.JoinTable) + FieldSelector + " on " + getTableAlias(jn.TableName) + "." + jn.FieldName + " = "
                            + FieldSelector + getTableAlias(jn.JoinTable) + FieldSelector + "." + jn.JoinField);
                    }
                    else
                    {
                        sbJoin.Append(joinType
                               + FieldSelector + getTableAlias(jn.JoinTable) + FieldSelector + " on " + getTableAlias(jn.TableName) + "." + jn.FieldName + " = "
                               + FieldSelector + getTableAlias(jn.JoinTable) + FieldSelector + "." + jn.JoinField);
                    }

                }
                if (this.Joins.Count == 0)
                {
                    if (this.Tables.Count > 1)
                    {
                        throw new Exception("More than one table to select");
                    }
                    else
                    {
                        sbJoin.Append(" " + this.Tables[0].TableName + " ");
                    }
                }

                for (int iwhereg = 0; iwhereg < this.WhereGroups.Count; iwhereg++)
                {
                    if (iwhereg == 0)
                    {
                        sbWhereGroup.Append(" Where (");
                    }
                    else
                    {
                        if (this.WhereGroups[iwhereg].condition == Condition._And)
                        {
                            sbWhereGroup.Append(" AND (");
                        }
                        else if (this.WhereGroups[iwhereg].condition == Condition._Or)
                        {
                            sbWhereGroup.Append(" OR (");
                        }
                        else
                        {
                            sbWhereGroup.Append(" ");
                        }
                    }
                    System.Text.StringBuilder sbWherecase = new System.Text.StringBuilder();
                    for (int iwhere = 0; iwhere < this.WhereGroups[iwhereg].whereCases.Count; iwhere++)
                    {
                        WhereCase ws = this.WhereGroups[iwhereg].whereCases[iwhere];

                        if (iwhere != 0)
                        {
                            if (ws.condition == Condition._And)
                            {
                                sbWherecase.Append(" AND ");
                            }
                            else if (ws.condition == Condition._Or)
                            {
                                sbWherecase.Append(" OR ");
                            }
                            else
                            {
                                sbWherecase.Append(" ");
                            }
                        }
                        sbWherecase.Append(FieldSelector + getTableAlias(ws.TableName) + FieldSelector + "." + FieldSelector + ws.FieldName + FieldSelector + getOperator(ws.Operation,
                        MakeValidateData(ws.Type, ws.ConditionValue, ws.FieldName)));

                    }

                    sbWhereGroup.Append(sbWherecase + " ) ");

                }
                sb.AppendFormat(Select, fd, sbJoin, sbWhereGroup);
            }
            else if (this.Type == QueryType._Insert)
            {
                System.Text.StringBuilder sbField = new System.Text.StringBuilder();
                System.Text.StringBuilder sbFieldvalue = new System.Text.StringBuilder();
                for (int ifld = 0; ifld < this.QueryFields.Count; ifld++)
                {
                    Field fld = this.QueryFields[ifld];
                    sbField.Append("," + FieldSelector + fld.TableName + FieldSelector + "." + fld.Name);
                    sbFieldvalue.Append("," + MakeValidateData(fld.Type, fld.Value, fld.Name));
                }
                sb.AppendFormat(Insert, FieldSelector + this.Tables[0].TableName + FieldSelector, sbField.ToString().Substring(1), sbFieldvalue.ToString().Substring(1));
            }

            else if (this.Type == QueryType._Delete)
            {
                if (this.Tables.Count > 1)
                {
                    throw new Exception(QueryResource._delete_table_more_exist);
                }
                sb.AppendFormat(Delete, this.Tables[0].TableName);
            }

            else if (this.Type == QueryType._Update)
            {
                System.Text.StringBuilder sbField = new System.Text.StringBuilder();
                System.Text.StringBuilder sbWhereGroup = new System.Text.StringBuilder();

                for (int ifld = 0; ifld < this.QueryFields.Count; ifld++)
                {
                    if (ifld != 0)
                    {
                        sbField.Append(",");
                    }
                    Field fd;
                    fd = this.QueryFields[ifld];
                    sbField.Append(FieldSelector + getTableAlias(fd.TableName) + FieldSelector + "." + FieldSelector + fd.Name + FieldSelector + " = ");
                    sbField.Append(MakeValidateData(fd.Type, fd.Value, fd.Name));
                }

                for (int iwhereg = 0; iwhereg < this.WhereGroups.Count; iwhereg++)
                {
                    if (iwhereg == 0)
                    {
                        sbWhereGroup.Append(" Where (");
                    }
                    else
                    {
                        if (this.WhereGroups[iwhereg].condition == Condition._And)
                        {
                            sbWhereGroup.Append(" AND (");
                        }
                        else if (this.WhereGroups[iwhereg].condition == Condition._Or)
                        {
                            sbWhereGroup.Append(" OR (");
                        }
                        else
                        {
                            sbWhereGroup.Append(" ");
                        }
                    }
                    System.Text.StringBuilder sbWherecase = new System.Text.StringBuilder();
                    for (int iwhere = 0; iwhere < this.WhereGroups[iwhereg].whereCases.Count; iwhere++)
                    {
                        WhereCase ws = this.WhereGroups[iwhereg].whereCases[iwhere];

                        if (iwhere != 0)
                        {
                            if (ws.condition == Condition._And)
                            {
                                sbWherecase.Append(" AND ");
                            }
                            else if (ws.condition == Condition._Or)
                            {
                                sbWherecase.Append(" OR ");
                            }
                            else
                            {
                                sbWherecase.Append(" ");
                            }
                        }
                        sbWherecase.Append(FieldSelector + getTableAlias(ws.TableName) + FieldSelector + "." + FieldSelector + ws.FieldName + FieldSelector + getOperator(ws.Operation,
                        MakeValidateData(ws.Type, ws.ConditionValue, ws.FieldName)));

                    }
                    sbWhereGroup.Append(sbWherecase + " ) ");
                    //  sbWhereGroup.Append(")");
                }
                sb.AppendFormat(Update, this.Tables[0].TableName + " as " + getTableAlias(this.Tables[0].TableName), sbField.ToString(), sbWhereGroup.ToString());
            }
            return sb.ToString();
        }

        internal override string getOperator(Operator opt, string fval)
        {
            if (opt == Operator._Equal)
            {
                return " = " + fval;
            }
            else if (opt == Operator._Contain)
            {
                return string.Format("like '%{0}%' ", fval);
            }
            else if (opt == Operator._EndWidth)
            {
                return string.Format("like '%{0}' ", fval);
            }
            else if (opt == Operator._StartWidth)
            {
                return string.Format("like '{0}%' ", fval);
            }
            else if (opt == Operator._Greater)
            {
                return " > " + fval;
            }
            else if (opt == Operator._Less)
            {
                return " < " + fval;
            }
            else if (opt == Operator._Greaterthan)
            {
                return " >= " + fval;
            }
            else if (opt == Operator._Lessthan)
            {
                return "<=" + fval;
            }
            else if (opt == Operator._In)
            {
                return " in (" + fval + ")";
            }
            else if (opt == Operator._NotEqual)
            {
                return " != " + fval;
            }
            else if (opt == Operator._NotIn)
            {
                return " not in (" + fval + ")";
            }
            return " = " + fval;
        }

        internal override bool ValidateSchema()
        {
            var BoolResult = true;            
            var Schema = toString();            
            string qry = Schema.ToLower();
            try
            {
                if (qry.StartsWith("select") == false)
                {
                    BoolResult = false;
                }
                else if (qry.IndexOf("delete ") > 0 | qry.IndexOf("insert ") > 0 | qry.IndexOf("update ") > 0 | qry.IndexOf("drop ") > 0)
                {
                    BoolResult = false;
                }
                else if (qry.IndexOf("from") < 1)
                {
                    BoolResult = false;
                }
                else if (qry.IndexOf("#") > 0 | qry.IndexOf("--") > 0 | qry.IndexOf("*/") > 0 | qry.IndexOf("/*") > 0)
                {
                    BoolResult = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BoolResult;
        }              
    }

   
 
     


   
}
