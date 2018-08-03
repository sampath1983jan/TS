using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;
using TechSharpy.Data;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component
{
    public class BusinessAttribute : Component, IComponent
    {
        private readonly int _parentComponentID;
        private int _parentAttributeID;
        private string _componentName;
        private string _category;
        private string _componentDescription;
        private List<ComponentAttribute> _componentAttributes;
        
        private Data.BusinessAttributeComponent databusinessAttribute;
        public string ComponentName { get => _componentName; set => _componentName=value; }
        public string Category { get => _category; set => _category=value; }
        public string ComponentDescription { get => _componentDescription; set => _componentDescription=value; }
        public List<ComponentAttribute> ComponentAttributes { get => _componentAttributes; set => _componentAttributes=value; }
        public string TitlePattern;        
        public int ComponentID => this.ID;
        public int ParentComponentID { get => _parentComponentID; }
        public int RelatedAttributeID { get => _parentAttributeID; }
        public Services.ErrorHandling.ErrorInfoCollection Errors;

        #region Constructors
        public BusinessAttribute(string componentName, string componentDescription,
            ComponentType componentType, string primarykeys, string titlePattern,
            int parentComponentID,int AttributeID) : base()
            {
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
            ComponentName = componentName;
                _parentComponentID = parentComponentID;
                _parentAttributeID = AttributeID;
                ComponentDescription = componentDescription;
                PrimaryKeys = primarykeys.Split(',').ToList();
                TitlePattern = titlePattern;
                ComponentAttributes = new List<ComponentAttribute>();
                this.Type = ComponentType._ComponentAttribute;
                databusinessAttribute = new Data.BusinessAttributeComponent();
            }
        public BusinessAttribute(int Id) : base(Id)
            {
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
            DataTable dt = new DataTable();
                this.ID = Id;
                dt= databusinessAttribute.GetComponentByID(this.ID);
                foreach(DataRow g in dt.Rows)
                {                
                    ID = g.IsNull("ComponentID") ? -1 : g.Field<int>("ComponentID");
                    TitlePattern = g.IsNull("TitlePattern") ? "" : g.Field<string>("TitlePattern");
                    ComponentName = g.IsNull("componentName") ? "" : g.Field<string>("componentName");
                    Description = g.IsNull("componentDescription") ? "" : g.Field<string>("componentDescription");
                    _parentComponentID = g.IsNull("ParentComponentID") ? -1 : g.Field<int>("ParentComponentID");
                    _parentAttributeID = g.IsNull("RelatedAttributeID") ? -1 : g.Field<int>("RelatedAttributeID");
                };             
                InitComponentAttribute();

            }
        #endregion

        private FieldAttribute GetFieldInstanceByID(int instanceID)
        {
            return (FieldAttribute)this.EntityInstances.Where(a => a.InstanceID == instanceID).FirstOrDefault();
        }
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
        internal bool ComponentSave()
        {
            if (base.EntityKey > 0 && base.ID > 0)
            {

                if (!base.Save().HasCriticalError())
                {
                    if (databusinessAttribute.Update(this.ID, this.EntityKey, this.Category, this.Type, this.ComponentName, 
                        this.ComponentDescription, this.TitlePattern, this.ParentComponentID, this.RelatedAttributeID))
                    {
                        foreach (ComponentAttribute ca in this.ComponentAttributes)
                        {
                            ca.SaveAttribute();
                        }
                        return true;
                    }
                    else return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (CreateBase())
                {
                    if (databusinessAttribute.Save(this.ID, this.Category, this.Type, this.ComponentName,
                        this.ComponentDescription, TitlePattern,this.ParentComponentID,this.RelatedAttributeID))
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
                else
                {
                    return false;
                }
            }
        }
        internal bool ComponentRemove()
        {
            if (BaseRemove())
            {
                if (databusinessAttribute.Delete(this.ID, this.EntityKey))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void AddAttribute(ComponentAttribute componentAttribute)
        {
            componentAttribute.setEntityFieldType();
            EntityInstances.Add(componentAttribute);
            ComponentAttributes.Add(componentAttribute);
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
            throw new NotImplementedException();
        }

        void IComponent.ComponentInit()
        {
            throw new NotImplementedException();
        }

        void IComponent.AddComponentAttribute(ComponentAttribute componentAttribute)
        {            
            AddAttribute(componentAttribute);
            AddEntityField(componentAttribute);
        }

        public bool RemoveComponentAttribute(int AttributeID)
        {
            var attr = this.ComponentAttributes.Where(a => a.AttributeID == AttributeID).FirstOrDefault();
            if (base.RemoveField(attr.InstanceID))
            {
               
                if (attr.RemoveAttribute())
                {
                    RemoveEntityField(attr);
                    return true;
                }
                else {
                    return false;
                }
            }
            else return false;
        }
    }
}
