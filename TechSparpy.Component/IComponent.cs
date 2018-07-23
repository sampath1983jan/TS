using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;

namespace TechSharpy.Component
{
    public interface IComponent {
          bool ComponentSave();
          bool ComponentRemove();
          bool ComponentHide();
          void ComponentInit();
    }
    public class Component:Entitifier.Core.EntitySchema, IComponent

    {
        public string ComponentName;
        public int ID;
        public string Category;
        public string ComponentDescription;
        public ComponentType Type;
        public List<ComponentAttribute> ComponentAttributes;
        public Data.Component dataComponent;
        public Component(int iD)
        {
            DataTable dt;
            dataComponent = new Data.Component();
            ID = iD;
            ComponentAttributes = new List<ComponentAttribute>();            
            dt =dataComponent.GetComponentByID(this.ID);
            foreach (DataRow dr in dt.Rows) {
                this.ID = dr.IsNull("ComponentID") ? -1 : dr.Field<int>("ComponentID");
                this.EntityKey = dr.IsNull("entityKey") ? -1 : dr.Field<int>("entityKey");
                this.Type = dr.IsNull("componentType") ? ComponentType._CoreComponent : dr.Field<ComponentType>("componentType");
            }
            base.Init();
        }
      

        public Component() {
            ComponentAttributes = new List<ComponentAttribute>();
            EntityInstances = new List<Entitifier.Core.EntityField>();
        }

        public void AddAttribute(ComponentAttribute componentAttribute) {
            componentAttribute.setEntityFieldType();
            EntityInstances.Add(componentAttribute);
            ComponentAttributes.Add(componentAttribute);
        }
         
        protected void setEntityType() {
            if (this.Type == ComponentType._CoreComponent)
            {
                base.EntityType = Entitifier.Core.EntityType._Master;
            }
            else if (this.Type == ComponentType._ComponentTransaction)
            {
                base.EntityType = Entitifier.Core.EntityType._Transaction;
            }
            else if (this.Type == ComponentType._ComponentAttribute)
            {
                base.EntityType = Entitifier.Core.EntityType._MasterAttribute;
            }
            else if (this.Type == ComponentType._GlobalComponent)
            {
                base.EntityType = Entitifier.Core.EntityType._Sudo;
            }
            else if (this.Type == ComponentType._SecurityComponent)
            {
                base.EntityType = Entitifier.Core.EntityType._System;
            }
            else if (this.Type == ComponentType._SubComponent) {
                base.EntityType = Entitifier.Core.EntityType._RelatedMaster;
            }
        }

        public bool ComponentSave()
        {
            throw new NotImplementedException();
        }

        public bool ComponentRemove()
        {
            throw new NotImplementedException();
        }

        public bool ComponentHide()
        {
            throw new NotImplementedException();
        }

        public void ComponentInit()
        {
            throw new NotImplementedException();
        }
    }
}
