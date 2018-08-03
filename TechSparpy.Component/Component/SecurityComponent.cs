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
    public class Security : Component, IComponent
    {
        private int _componentID;
        private string _componentName;
        private string _category;
        private string _componentDescription;
        private string _titlePattern;
        private List<ComponentAttribute> _componentAttributes;
        private readonly int _parentComponentID;
        private int _parentAttributeID;
        private int _userComponentID;
        private int _userKey;
        private Data.SecurityComponent datasecurityComponent;
        /// <summary>
        /// 
        /// </summary>
        public string ComponentName { get => _componentName; set => _componentName=value; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get => _category; set => _category=value; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentDescription { get => _componentDescription; set => _componentDescription=value; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentAttribute> ComponentAttributes { get => _componentAttributes; set => _componentAttributes=value; }
        /// <summary>
        /// 
        /// </summary>
        public int ComponentID => _componentID;
        /// <summary>
        /// 
        /// </summary>
        public string TitlePattern { get => _titlePattern; set => _titlePattern = value; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentComponentID { get => _parentComponentID; }
        /// <summary>
        /// 
        /// </summary>
        public int RelatedAttributeID { get => _parentAttributeID; }
        /// <summary>
        /// 
        /// </summary>
        public int UserComponentID { get => _userComponentID; }
        /// <summary>
        /// 
        /// </summary>
        public int UserKey { get => _userKey; }
             

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="componentDescription"></param>
        /// <param name="componentType"></param>
        /// <param name="primarykeys"></param>
        /// <param name="titlePattern"></param>
        /// <param name="parentComponentID"></param>
        /// <param name="AttributeID"></param>
        public Security(string componentName, string componentDescription,
        ComponentType componentType, string primarykeys, string titlePattern,
        int parentComponentID, int AttributeID,int UserComponentID,int UserKey) : base()
        {
            ComponentName = componentName;
            _parentComponentID = parentComponentID;
            _parentAttributeID = AttributeID;
            ComponentDescription = componentDescription;
            PrimaryKeys = primarykeys.Split(',').ToList();
            TitlePattern = titlePattern;
            ComponentAttributes = new List<ComponentAttribute>();
            this.Type = ComponentType._ComponentAttribute;
            datasecurityComponent = new Data.SecurityComponent();
            addSecurityAttributes();
        }
        public Security(int Id) : base(Id)
        {
            DataTable dt = new DataTable();
            this.ID = Id;
            datasecurityComponent = new Data.SecurityComponent();
            dt = datasecurityComponent.GetComponentByID(this.ID);
            foreach (DataRow g in dt.Rows)
            {
                ID = g.IsNull("ComponentID") ? -1 : g.Field<int>("ComponentID");
                TitlePattern = g.IsNull("TitlePattern") ? "" : g.Field<string>("TitlePattern");
                ComponentName = g.IsNull("componentName") ? "" : g.Field<string>("componentName");
                Description = g.IsNull("componentDescription") ? "" : g.Field<string>("componentDescription");
                _parentComponentID = g.IsNull("ParentComponentID") ? -1 : g.Field<int>("ParentComponentID");
                _parentAttributeID = g.IsNull("RelatedAttributeID") ? -1 : g.Field<int>("RelatedAttributeID");
                _userComponentID = g.IsNull("UserComponentID") ? 0 : g.Field<int>("UserComponentID");
                _userKey = g.IsNull("UserKey") ? 0 : g.Field<int>("UserKey");
            };
            
          InitComponentAttribute();

        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        private FieldAttribute GetFieldInstanceByID(int instanceID)
        {
            return (FieldAttribute)this.EntityInstances.Where(a => a.InstanceID == instanceID).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool BaseRemove()
        {
            bool isBaseRemoved;
            isBaseRemoved = base.Remove();
            if (isBaseRemoved)
                isBaseRemoved = base.Delete();
            return isBaseRemoved;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ComponentSave()
        {
            if (base.EntityKey > 0 && base.ID > 0)
            {

                if (!base.Save().HasCriticalError())
                {
                    if (datasecurityComponent.Update(this.ID, this.EntityKey, this.Category, this.Type, this.ComponentName,
                        this.ComponentDescription, this.TitlePattern, this.ParentComponentID, this.RelatedAttributeID, this.UserComponentID, this.UserKey))
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
                    if (datasecurityComponent.Save(this.ID, this.Category, this.Type, this.ComponentName,
                        this.ComponentDescription, TitlePattern, this.ParentComponentID, this.RelatedAttributeID,this.UserComponentID,this.UserKey))
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ComponentRemove()
        {
            if (BaseRemove())
            {
                if (datasecurityComponent.Delete(this.ID, this.EntityKey))
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentAttribute"></param>
        private void AddAttribute(ComponentAttribute componentAttribute)
        {
            componentAttribute.setEntityFieldType();
            EntityInstances.Add(componentAttribute);
            ComponentAttributes.Add(componentAttribute);
        }
        /// <summary>
        /// 
        /// </summary>
        private void addSecurityAttributes()
        {
            var h = ComponentManager.Create(new ComponentHandlerFactory(),_parentComponentID);
            ComponentAttribute parentAttribute  =h.ComponentAttributes.Where(a => a.AttributeID == _parentAttributeID).FirstOrDefault();

            var s = ComponentManager.Create(new ComponentHandlerFactory(), _userComponentID);
            ComponentAttribute userComponent = s.ComponentAttributes.Where(a => a.AttributeID == _userKey).FirstOrDefault();

            ComponentAttribute ca = new ComponentAttribute();
            ca.Type = parentAttribute.Type;
            ca.Name = parentAttribute.Name;
            ca.DisplayName = parentAttribute.DisplayName ;
            ca.DisplayOrder =0;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = true;
            ca.IsUnique = true;
            ca.IsShow = false;
            ca.UsageFieldType = UsageType._NONE;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.FieldType = parentAttribute.FieldType;
            ca.DefaultValue = "";
            ca.Description = "Component which is link with this privileges";
            ca.enableContentLimit = false;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);

            ca = new ComponentAttribute();
            ca.Type = userComponent.Type;
            ca.Name = userComponent.Name;
            ca.DisplayName = userComponent.DisplayName;
            ca.DisplayOrder = 0;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = true;
            ca.IsUnique = true;
            ca.IsShow = false;
            ca.UsageFieldType = UsageType._NONE;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.FieldType = userComponent.FieldType;
            ca.DefaultValue = "";
            ca.Description = "User component link with this privileges";
            ca.enableContentLimit = false;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);

            ca = new ComponentAttribute();
            ca.Type = AttributeType._Bool;
            ca.Name = "View";
            ca.DisplayName = "View";
            ca.DisplayOrder = 1;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = true;
            ca.IsUnique = true;
            ca.IsShow = false;
            ca.UsageFieldType = UsageType._InputField;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.FieldType = Entitifier.Core.EntityFieldType._Bool;
            ca.DefaultValue = "";
            ca.Description = "Privileges for view";
            ca.enableContentLimit = false;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);

            ca = new ComponentAttribute();
            ca.Type = AttributeType._Bool;
            ca.Name = "Add";
            ca.DisplayName = "Add";
            ca.DisplayOrder = 2;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = false;
            ca.IsUnique = false;
            ca.IsShow = true;
            ca.FieldType = Entitifier.Core.EntityFieldType._Bool;
            ca.UsageFieldType = UsageType._InputField;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.MaxLength = 1000;
            ca.DefaultValue = "";
            ca.Description = "Add New Record";
            ca.enableContentLimit = true;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);

            ca = new ComponentAttribute();
            ca.Type = AttributeType._Bool;
            ca.Name = "Edit";
            ca.DisplayName = "Modify";
            ca.DisplayOrder = 3;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = false;
            ca.IsUnique = false;
            ca.IsShow = true;
            ca.UsageFieldType = UsageType._InputField;
            ca.FieldType = Entitifier.Core.EntityFieldType._Bool;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.MaxLength = 1000;
            ca.DefaultValue = "";
            ca.Description = "Modify Records";
            ca.enableContentLimit = true;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);
            ca = new ComponentAttribute();
            ca.Type = AttributeType._Bool;
            ca.Name = "Delete";
            ca.DisplayName = "Remove";
            ca.DisplayOrder = 4;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = false;
            ca.IsUnique = false;
            ca.IsShow = true;
            ca.UsageFieldType = UsageType._InputField;
            ca.AutoIncrement = false;
            ca.FieldType = Entitifier.Core.EntityFieldType._Bool;
            ca.Cryptography = 0;
            ca.MaxLength = 1000;
            ca.DefaultValue = "";
            ca.Description = "Privilege for remove record(s)";
            ca.enableContentLimit = false;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);
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
            throw new NotImplementedException();
        }

        public bool RemoveComponentAttribute(int AttributeID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateComponentAttribute(ComponentAttribute componentAttribute)
        {
            throw new NotImplementedException();
        }
    }
}
