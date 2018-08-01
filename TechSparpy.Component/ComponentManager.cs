using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;

namespace TechSharpy.Component
{
   public class ComponentManager
    {         
                 
        public static IComponent Create(ICompnentFactory factory, ComponentType ct)
        {
            return factory.Create(ct);
        }

        public static IComponent Create(ICompnentFactory factory, int ComponentID)
        {
            return factory.Create(ComponentID);
        }

        public static IComponent Create(ICompnentFactory factory,string ComponentName, string ComponentDescription, ComponentType componentType,
         string primarykeys)
        {
            return factory.Create(ComponentName, ComponentDescription, componentType, primarykeys);
        }
       
              
    }
}
