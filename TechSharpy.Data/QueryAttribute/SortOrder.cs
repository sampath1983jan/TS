using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data.QueryAttribute
{

    public class SortOrder
    {

        string TableName;
        string FieldName;
        SortType Type;
        public SortOrder(string pTableName, string pFieldName, SortType pType)
        {
            this.TableName = pTableName;
            this.FieldName = pFieldName;
            this.Type = pType;

        }
    }

}
