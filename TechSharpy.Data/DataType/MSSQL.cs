using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TechSharpy.Data.ABS;

namespace TechSharpy.Data.DataType
{
    public class MSSQL :DataAccess
    {
        public override int ExecuteQuery(Query DataQuery)
        {
            throw new NotImplementedException();
        }

        public override bool ExecuteTQuery(MYSQLTQueryBuilder tQuery)
        {
            throw new NotImplementedException();
        }

        public override DataTable GetData(Query DataQuery)
        {
            throw new NotImplementedException();
        }
    }
}
