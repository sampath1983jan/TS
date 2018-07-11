using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security
{
    public abstract class Privilege
    {
        public int UserKey;
        public string Datasourcekey;
      //  public int Privilegekey;
        public bool Add;
        public bool View;
        public bool Remove;
        public bool Change;
        public abstract bool Save();
        public abstract bool Delete();
        public Data.Privilege privilege;
        public Privilege(string datasourcekey,int userkey, bool add, bool view, bool remove, bool change) {
            Datasourcekey = datasourcekey;
          //  Privilegekey = privilegekey;
            Add = add;
            View = view;
            Remove = remove;
            Change = change;
            UserKey = userkey;
            privilege = new Data.Privilege();
        }

     
    }
}
