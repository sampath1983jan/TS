using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Component
{
 public abstract class IComponent:Entitifier.Core.EntitySchema
    {
        public string ComponentName;
        public int ID;
        public string Category;
        public abstract  bool  ComponentSave();
        public abstract  bool ComponentRemove();
        public abstract  bool ComponentHide();

        protected   IComponent(int iD)
        {
            ID = iD;
        }
    }
}
