using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security.Privileges
{
    public class ProcedurePrivilege :Privilege
    {

        public ProcedurePrivilege(string dskey, int userkey, bool add, bool view,
            bool remove, bool change) : base(dskey, userkey,  add, view, remove, change)
        {

        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {

            if (privilege.SaveProcedurePrivilege(this.UserKey, this.Datasourcekey,
                 this.View, this.Add, this.Remove, this.Change))
            {
                return true;
            }
            else return false;
            //throw new NotImplementedException();
        }
    }
}
