using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security.Privileges
{
   public class JobPrivilege : Privilege
    {

        public JobPrivilege(string dskey, int userkey, bool add, bool view,
            bool remove, bool change) : base(dskey, userkey, add, view, remove, change)
        {

        }
        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            // throw new NotImplementedException();
            if (privilege.SaveJobPrivilege(this.UserKey, this.Datasourcekey, this.View,
                this.Add, this.Remove,this.Change))
            {
                return true;
            }
            else return false;
        }
    }
}
