﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;
using System.Data;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component
{
    public class GlobalSetting : Component, IComponent
    {
        private int _componentID;
        private string _componentName;
        private string _category;
        private string _componentDescription;
        private string _titlePattern;
        private List<ComponentAttribute> _componentAttributes;
        private Data.GlobalComponent  dataglobalComponent;

        public string ComponentName { get => _componentName; set => _componentName=value; }
        public string Category { get => _category; set => _category = value; }
        public string ComponentDescription { get => _componentDescription; set => _componentDescription = value; }
        public List<ComponentAttribute> ComponentAttributes { get => _componentAttributes; set => _componentAttributes=value; }
        public int ComponentID { get => _componentID; }
        public string TitlePattern { get => _titlePattern; set => _titlePattern = value; }        
        public GlobalSetting(int componentID):base(componentID) {
            this.Type = ComponentType._GlobalComponent;
            dataglobalComponent = new Data.GlobalComponent();
            _componentAttributes = new List<ComponentAttribute>();
            DataTable dt = new DataTable();
            dt = dataglobalComponent.GetComponentByID(this.ID);
            var bc = dt.AsEnumerable().Select(g => new GlobalSetting
            {                
                ID = g.IsNull("ComponentID") ? -1 : g.Field<int>("ComponentID"),
                _titlePattern= g.IsNull("titlepattern") ? "" : g.Field<string>("titlepattern"),
                ComponentName = g.IsNull("componentName") ? "" : g.Field<string>("componentName"),
                Description = g.IsNull("componentDescription") ? "" : g.Field<string>("componentDescription"),

            }).FirstOrDefault();
            this.ComponentName = bc.ComponentName;
            this.ComponentDescription = bc.ComponentDescription;
            this._titlePattern = bc.TitlePattern;
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
        private FieldAttribute GetFieldInstanceByID(int instanceID)
        {
            return (FieldAttribute)this.EntityInstances.Where(a => a.InstanceID == instanceID).FirstOrDefault();
        }
        public GlobalSetting():base() {
            this.Type = ComponentType._GlobalComponent;
            dataglobalComponent = new Data.GlobalComponent();
            _componentAttributes = new List<ComponentAttribute>();
            addGlobalAttributes();
        }
        private void addGlobalAttributes() {
            ComponentAttribute ca = new ComponentAttribute();
            ca.Type = AttributeType._Number;
            ca.Name = "Key";
            ca.DisplayName = "Key";
            ca.DisplayOrder = 1;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = true;
            ca.IsUnique = true;
            ca.IsShow = false;
            ca.UsageFieldType = UsageType._NONE;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.FieldType = Entitifier.Core.EntityFieldType._Number;
            ca.DefaultValue = "";
            ca.Description = "Global setting key field";
            ca.enableContentLimit = false;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);

            ca = new ComponentAttribute();
            ca.Type = AttributeType._Text;
            ca.Name = "Label";
            ca.DisplayName = "Label";
            ca.DisplayOrder = 2;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = false;
            ca.IsUnique = false;
            ca.IsShow = true;
            ca.FieldType = Entitifier.Core.EntityFieldType._Text;
            ca.UsageFieldType = UsageType._InputField;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.MaxLength = 1000;
            ca.DefaultValue = "";
            ca.Description = "Title field of the global setting";
            ca.enableContentLimit = true;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);
            ca = new ComponentAttribute();
            ca.Type = AttributeType._Text;
            ca.Name = "Value";
            ca.DisplayName = "Value";
            ca.DisplayOrder = 3;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = false;
            ca.IsUnique = false;
            ca.IsShow = true;
            ca.UsageFieldType = UsageType._InputField;
            ca.FieldType = Entitifier.Core.EntityFieldType._Text;
            ca.AutoIncrement = false;
            ca.Cryptography = 0;
            ca.MaxLength = 1000;
            ca.DefaultValue = "";
            ca.Description = "Value field of the global setting";
            ca.enableContentLimit = true;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);
            ca = new ComponentAttribute();
            ca.Type = AttributeType._Number;
            ca.Name = "ValueType";
            ca.DisplayName = "Value Type";
            ca.DisplayOrder = 4;
            ca.IsRequired = true;
            ca.IsCore = true;
            ca.IsKey = false;
            ca.IsUnique = false;
            ca.IsShow = true;
            ca.UsageFieldType = UsageType._InputField;
            ca.AutoIncrement = false;
            ca.FieldType = Entitifier.Core.EntityFieldType._Number;
            ca.Cryptography = 0;
            ca.MaxLength = 1000;
            ca.DefaultValue = "";
            ca.Description = "Datatype of the individual global setting";
            ca.enableContentLimit = false;
            ca.EnableEncription = false;
            ComponentAttributes.Add(ca);
            EntityInstances.Add(ca);
        }
        private bool BaseRemove()
        {
            bool isBaseRemoved;
            isBaseRemoved = base.Remove();
            if (isBaseRemoved)
                isBaseRemoved = base.Delete();
            return isBaseRemoved;
        }
        public void AddComponentAttribute(ComponentAttribute componentAttribute)
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
        public bool ComponentRemove()
        {
            if (BaseRemove())
            {
                if (dataglobalComponent.Delete(this.ID, this.EntityKey))
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
        public bool ComponentSave()
        {

            if (base.EntityKey > 0 && base.ID > 0)
            {
                if (!base.Save().HasCriticalError())
                {
                    if (dataglobalComponent.Update(this.ID, this.EntityKey, this.Category, 
                        this.Type, this.ComponentName, this.ComponentDescription,this.TitlePattern))
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
                    if (dataglobalComponent.Save(this.ID, this.Category, this.Type, 
                        this.ComponentName, this.ComponentDescription,this.TitlePattern))
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
