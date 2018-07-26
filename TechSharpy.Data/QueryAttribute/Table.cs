using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data.QueryAttribute
{
   public class Table
    {
        public string TableName;
        public string AliasName;
        public Table(string pTableName, string pAliasName = "")
        {
            this.TableName = pTableName;
            this.AliasName = pAliasName;
        }
    }
}
