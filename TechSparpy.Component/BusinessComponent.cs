using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Component
{
    public enum ComponentType {
         _CoreComponent=1,
         _SubComponent=2,
         _ComponentAttribute=3,
         _ComponentTransaction=4
    }

    public class BusinessComponent:IComponent
    {
        public ComponentType Type;
        public BusinessComponent(int Id):base(Id)
        {
            
        }        

        public override bool ComponentHide()
        {
            throw new NotImplementedException();
        }

        public override bool ComponentRemove()
        {
            if (base.Remove())
            {
                return true;
            }
            else {
                return true;
            }                      
        }

        public override bool ComponentSave()
        {
            if (! base.Save().HasCriticalError() ) {

            }
            throw new NotImplementedException();
        }
    }
}
