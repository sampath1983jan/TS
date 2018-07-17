using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql;
using System.Configuration;
using MySql.Data.MySqlClient;
using TechSharpy.Data.ABS;

namespace TechSharpy.Data
{
    public abstract class DataAccess
    {       
        public abstract System.Data.DataTable GetData(Query DataQuery);
        public abstract int ExecuteQuery(Query DataQuery);
        public abstract bool ExecuteTQuery(MYSQLTQueryBuilder tQuery);           
    }
}

