using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Component.Data
{
    public class BusinessComponent
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public BusinessComponent()
        {
            try
            {
                rd = new DataBase();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        
    }
}
