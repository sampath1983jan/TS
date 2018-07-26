using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data
{
  public abstract  class DataConnection
    {
        public string HostName;
        public string UserName;
        public string Password;
        public string DataSourceName;
        public string Port;
        public abstract string getConnectionstring();
        public abstract bool TestConnection();
    }
}
