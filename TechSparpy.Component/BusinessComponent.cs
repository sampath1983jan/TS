using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component
{
    public enum ComponentType {
         _CoreComponent=1,
         _SubComponent=2,
         _ComponentAttribute=3,
         _ComponentTransaction=4,
         _SecurityComponent=5,
         _GlobalComponent=6,
    }
  internal class BusinessComponent:Component,IComponent
    {
        #region Member variables and properties
        private string _category = "";
        private string _componentDescription = "";
        private List<ComponentAttribute> _componentAttributes;
        private string _componentName = "";
        public string Category { get => _category; set => _category = value; }
        public string ComponentDescription { get => _componentDescription; set => _componentDescription = value; }
        public List<ComponentAttribute> ComponentAttributes { get => _componentAttributes; set => _componentAttributes = value; }
        public string ComponentName { get => _componentName; set => _componentName = value; }
        public int ComponentID => this.ID;
        private Data.BusinessComponent databusinessComponent;
        #endregion

        #region Constructors
        public BusinessComponent(ComponentType type) : base()
        {
            ComponentAttributes = new List<ComponentAttribute>();
            this.Type = type;
            databusinessComponent = new Data.BusinessComponent();
        }              
        public BusinessComponent()
        {
            ComponentAttributes = new List<ComponentAttribute>();
        }
        public BusinessComponent(int Id):base(Id)
        {
            this.ID = Id;
            ComponentAttributes = new List<ComponentAttribute>();
            DataTable dt;
            databusinessComponent = new Data.BusinessComponent();
            dt = databusinessComponent.GetComponentByID(this.ID);
            var bc = dt.AsEnumerable().Select(g => new BusinessComponent
            {
             //   EntityKey = g.IsNull("entityKey") ? -1 : g.Field<int>("entityKey"),
                ID = g.IsNull("ComponentID") ? -1 : g.Field<int>("ComponentID"),
            //    Type = g.IsNull("ComponentType") ? ComponentType._ComponentAttribute : g.Field<ComponentType>("componentType"),
                ComponentName = g.IsNull("componentName") ? "" : g.Field<string>("componentName"),
                Description = g.IsNull("componentDescription") ? "" : g.Field<string>("componentDescription"),

            }).FirstOrDefault();
            this.ComponentName = bc.ComponentName;
            this.ComponentDescription = bc.ComponentDescription;
        //    this.Type = bc.Type;
          //  this.EntityKey = bc.EntityKey;
            this.ID = bc.ID;
            InitComponentAttribute();
        }
        #endregion 
        #region Internal methods
        private void InitComponentAttribute()
        {
            DataTable dt = new DataTable();
            Data.ComponentAttribute datacomponentAttribute = new Data.ComponentAttribute();
            dt = datacomponentAttribute.GetComponentAttributes(ID.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                int InstanceID = dr.IsNull("FieldInstanceID") ? 0 : dr.Field<int>("FieldInstanceID");
                var ca = new ComponentAttribute();
                ca = (ComponentAttribute)GetFieldInstanceByID(InstanceID).CopyTo<ComponentAttribute>();
                ca.Type = dr.IsNull("Attributetype") ? AttributeType._None : dr.Field<AttributeType>("Attributetype");
                ca.ComponentKey = dr.IsNull("componentKey") ? "" : dr.Field<string>("componentKey");
                ca.Cryptography = dr.IsNull("cryptography") ? 0 : dr.Field<int>("cryptography");
                ca.RegExpression = dr.IsNull("regExpression") ? "" : dr.Field<string>("regExpression");
                ca.ParentComponentKey = dr.IsNull("parentComponent") ? "" : dr.Field<string>("parentComponent");
                ca.ParentAttribute = dr.IsNull("parentAttribute") ? "" : dr.Field<string>("parentAttribute");
                this.ComponentAttributes.Add(ca);
            }
        }
        private void AddAttribute(ComponentAttribute componentAttribute)
        {
            componentAttribute.setEntityFieldType();
            EntityInstances.Add(componentAttribute);
            ComponentAttributes.Add(componentAttribute);
        }
        private FieldAttribute GetFieldInstanceByID(int instanceID)
        {
            return (FieldAttribute)this.EntityInstances.Where(a => a.InstanceID == instanceID).FirstOrDefault();
        }
        private bool BaseRemove()
        {
            bool isBaseRemoved;
            isBaseRemoved = base.Remove();
            if (isBaseRemoved)
                isBaseRemoved = base.Delete();
            return isBaseRemoved;
        }
        private bool CreateBase()
        {
            base.TableName = this.ComponentName.Replace(" ", "_");
            setEntityType();
            if (!base.Save().HasCriticalError())
            {
                if (base.Create())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


        internal bool ComponentHide()
        {
            throw new NotImplementedException();
        }
        
        internal bool ComponentRemove()
        {
            if (BaseRemove())
            {                
                if (databusinessComponent.Delete(this.ID, this.EntityKey))
                {
                    return true;
                }
                else {
                    return false;
                }               
            }
            else {
                return true;
            }                      
        }
        
        internal bool ComponentSave()
        {
            if (base.EntityKey > 0  && base.ID >0)
            {
              
                if (!base.Save().HasCriticalError())
                {
                    if (databusinessComponent.Update(this.ID, this.EntityKey, this.Category, this.Type, this.ComponentName, this.ComponentDescription))
                    {
                        foreach (ComponentAttribute ca in this.ComponentAttributes) {
                            ca.SaveAttribute();
                        }
                        return true;
                    }
                    else return false;
                }
                else {
                    return false;
                }
            }
            else {              
                if (CreateBase())
                {                                        
                    if (databusinessComponent.Save(this.ID, this.Category, this.Type, this.ComponentName, this.ComponentDescription))
                    {
                        foreach (ComponentAttribute ca in this.ComponentAttributes)
                        {
                            ca.ComponentKey = ID.ToString();
                            ca.EntityKey = base.EntityKey;
                            ca.SaveAttribute();
                        }   
                        return true;
                    }
                    else return false;
                }
                else {
                    return false;
                }
            }            
        }
                
        internal void ComponentInit()
        {
           // return ComponentHide();
            
        }



        #region Implimentation methods
        void IComponent.AddComponentAttribute(ComponentAttribute componentAttribute)
        {
            AddAttribute(componentAttribute);
        }

        bool IComponent.ComponentSave()
        {
            return ComponentSave();
        }

        bool IComponent.ComponentRemove()
        {
            return ComponentRemove();
        }

        bool IComponent.ComponentHide()
        {
            return ComponentHide();
        }

        void IComponent.ComponentInit()
        {
            ComponentInit();
        }
        #endregion



    }
}
