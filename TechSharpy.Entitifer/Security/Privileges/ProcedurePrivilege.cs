using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security.Privileges
{
    public class ProcedurePrivilege :Privilege
    {

        public ProcedurePrivilege(int dskey, int userkey, int privilegekey, bool add, bool view,
            bool remove, bool change) : base(dskey, userkey, privilegekey, add, view, remove, change)
        {

        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
