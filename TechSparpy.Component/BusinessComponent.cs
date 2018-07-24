using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;

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

    public class BusinessComponent:Component
    {
             
        public Data.BusinessComponent databusinessComponent;
        public BusinessComponent(int Id):base(Id)
        {
            DataTable dt;
            databusinessComponent = new Data.BusinessComponent();
            dt = databusinessComponent.GetComponentByID(this.ID);
            var bc = dt.AsEnumerable().Select(g => new BusinessComponent
            {
                EntityKey = g.IsNull("entityKey") ? -1 : g.Field<int>("entityKey"),
                ID = g.IsNull("ComponentID") ? -1 : g.Field<int>("ComponentID"),
                Type = g.IsNull("ComponentType") ? ComponentType._ComponentAttribute : g.Field<ComponentType>("componentType"),
                ComponentName = g.IsNull("componentName") ? "" : g.Field<string>("componentName"),
                Description = g.IsNull("componentDescription") ? "" : g.Field<string>("componentDescription"),

            }).FirstOrDefault();
            this.ComponentName = bc.ComponentName;
            this.ComponentDescription = bc.ComponentDescription;
            this.Type = bc.Type;
            this.EntityKey = bc.EntityKey;
            this.ID = bc.ID;
            InitComponentAttribute();
        }
        private void InitComponentAttribute()
        {
            DataTable dt = new DataTable();
            Data.ComponentAttribute datacomponentAttribute = new Data.ComponentAttribute();
            dt = datacomponentAttribute.GetComponentAttributes(ID.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                ComponentAttribute ca = new ComponentAttribute();
                int InstanceID = dr.IsNull("FieldInstanceID") ? 0 : dr.Field<int>("FieldInstanceID");
                ca = new ComponentAttribute()
                {
                    Type = dr.IsNull("Attributetype") ? AttributeType._None : dr.Field<AttributeType>("FieldInstanceID"),
                    ComponentKey = dr.IsNull("componentKey") ? "" : dr.Field<string>("componentKey"),
                    Cryptography = dr.IsNull("cryptography") ? 0 : dr.Field<int>("cryptography"),
                    RegExpression = dr.IsNull("regExpression") ? "" : dr.Field<string>("regExpression"),
                    ParentComponentKey = dr.IsNull("parentComponent") ? "" : dr.Field<string>("parentComponent"),
                    ParentAttribute = dr.IsNull("parentAttribute") ? 0 : dr.Field<int>("parentAttribute")
                };
                ca = (ComponentAttribute)GetFieldInstanceByID(InstanceID);
                this.ComponentAttributes.Add(ca);
            }
        }
        private Entitifier.Core.EntityField GetFieldInstanceByID(int instanceID)
        {
            return this.EntityInstances.Where(a => a.InstanceID == instanceID).FirstOrDefault();
        }

        public BusinessComponent(ComponentType type):base() {
            this.Type = type;
            databusinessComponent = new Data.BusinessComponent();
        }
        public BusinessComponent() {

        }
        public new bool ComponentHide()
        {
            throw new NotImplementedException();
        }

        public new bool ComponentRemove()
        {
            if (base.Remove())
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

        public new bool ComponentSave()
        {
            if (base.EntityKey > 0)
            {
              
                if (!base.Save().HasCriticalError())
                {
                    if (databusinessComponent.Save(this.ID, this.EntityKey, this.Category, this.Type, this.ComponentName, this.ComponentDescription))
                    {
                        foreach (ComponentAttribute ca in base.ComponentAttributes) {
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
                base.TableName = this.ComponentName.Replace(" ", "_");
                setEntityType();
                if (!base.Save().HasCriticalError())
                {
                   
                    this.ID = databusinessComponent.Save( this.Category, this.Type, this.ComponentName, this.ComponentDescription);
                    if (this.ID > 0)
                    {
                        foreach (ComponentAttribute ca in base.ComponentAttributes)
                        {
                            ca.ComponentKey = ID.ToString();
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

        public new void ComponentInit()
        {
            //Data.BusinessComponent databusinessComponent = new Data.BusinessComponent();
            //base.Init();
            //DataTable dt = new DataTable();
        }

        
    }
}
