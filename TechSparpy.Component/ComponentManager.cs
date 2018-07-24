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
        private Component _iComponent;
        private ComponentType _Type;

        public Component Component { get { return _iComponent; } }
        public ComponentType Type { get { return _Type; } }
        private int ComponentID;

        public int GetComponentID() {
            return ComponentID;
        }

        public ComponentManager(ComponentType componentType,Component icc) {
            _Type = componentType;
            _iComponent = icc;
        }

        public ComponentManager(int componentID) {
            this.ComponentID = componentID;
            Init();
        }

        public void AddAttribute(ComponentAttribute componentAttribute) {
            Component.AddAttribute(componentAttribute);
        }

        private void Init() {            
            _iComponent = new Component(this.ComponentID);
            _Type = _iComponent.Type;
            if (Type == ComponentType._CoreComponent || Type == ComponentType._ComponentAttribute || Type == ComponentType._ComponentTransaction)
            {
                BusinessComponent bs = new BusinessComponent(this.ComponentID);
            }
            else if (Type == ComponentType._SecurityComponent)
            {
            }
            else if (Type == ComponentType._GlobalComponent)
            {
                
            }
        }

        public void Save() {
            if (Type == ComponentType._CoreComponent || Type == ComponentType._ComponentAttribute || Type == ComponentType._ComponentTransaction)
            {                
                BusinessComponent bs = (BusinessComponent)Component;                
                bs.ComponentSave();
            }
            else if (Type == ComponentType._SecurityComponent)
            {
                SecurityComponent sc = (SecurityComponent)Component;          
                sc.ComponentSave();
            }
            else if (Type == ComponentType._GlobalComponent)
            {
                GlobalComponent gc= (GlobalComponent)Component;
                gc.ComponentSave();
            }                      
        }

        public void Remove() {
            if (Type == ComponentType._CoreComponent || Type== ComponentType._ComponentAttribute || Type== ComponentType._ComponentTransaction)
            {
                BusinessComponent bs = (BusinessComponent)Component;
                bs.ComponentRemove();
            }
            else if (Type == ComponentType._SecurityComponent)
            {
                SecurityComponent sc = (SecurityComponent)Component;
                sc.ComponentRemove();
            }
            else if (Type == ComponentType._GlobalComponent)
            {
                GlobalComponent gc = (GlobalComponent)Component;
                gc.ComponentRemove();
            }
        }
    }
}
