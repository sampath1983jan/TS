using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data
{
    public enum QueryType
    {
        _Select,
        _Insert,
        _Update,
        _Delete,
        _BulkInsert,
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
    public enum JoinType
    {
        _InnerJoin,
        _LeftJoin,
        _RightJoin,

    }
    public enum Condition
    {
        _None,
        _And,
        _Or
    }
    public enum SortType
    {
        _Asc,
        _Desc
    }
    public enum Aggregate
    {
        _None,
        _Sum,
        _Average,
        _Min,
        _Max,
        _Count

    }

    public static class QueryResource
    {
        public static string _delete_table_more_exist = "Delete function cannot perform due to more then one table exist in the query.";
    }


    public   class QueryBuilder : ABS.Query
    {
        public QueryBuilder(QueryType type) : base(type)
        {

        }
        internal override string getOperator(Operator opt, string fval)
        {
            throw new NotImplementedException();
        }

        internal override string toString()
        {
            throw new NotImplementedException();
        }

        internal override bool ValidateSchema()
        {
            throw new NotImplementedException();
        }
    }

}
