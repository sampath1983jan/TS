﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;

namespace TechSharpy.Component.Attributes
{
    public enum AttributeType{
        _Number = 1,
        _Float = 2,
        _Text = 3,
        _LongText = 4,
        _Date = 5,
        _DateTime = 6,
        _Time = 7,
        _Bool = 8,
        _Entity = 9,
        _Lookup = 10,
        _MultiLookup = 11,
        _Picture = 12,
        _File = 13,
        _None =-1,
        _Auto = 14,
        _Year=15,
        _Month=16,
        _Quarter=17, 
        _Calculator=18,
        _PrimaryKeyField=19
    }
    public enum UsageType {
        _NONE=0,
        _InputField=1,
        _CalculationField=2,
        
    }
    [Serializable]
public class ComponentAttribute:TechSharpy.Entitifier.Core.EntityField 
    {                       
        public int AttributeID;
        public AttributeType Type;         
        public string ComponentKey;      
        public int Cryptography;
        public string RegExpression;
        public string ParentAttribute;
        public string ParentComponentKey;
        public UsageType UsageFieldType;
        private Data.ComponentAttribute datacomponentAttribute;

        internal bool SaveAttribute() {          
            return Save();
        }
        internal bool RemoveAttribute() {
            return Remove();
        }
        internal void setEntityFieldType() {            
            if (this.Type == AttributeType._Number || this.Type == AttributeType._Year
                || this.Type == AttributeType._Month || this.Type == AttributeType._PrimaryKeyField)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Number;
            }
            else if (this.Type == AttributeType._Float || this.Type == AttributeType._Calculator)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Float;
            }
            else if (this.Type == AttributeType._LongText) {
                base.FieldType = Entitifier.Core.EntityFieldType._LongText;
            }
            else if (this.Type == AttributeType._Text)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Text;
            }
            else if (this.Type == AttributeType._Date)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Date;
            }
            else if (this.Type == AttributeType._DateTime)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._DateTime;
            }
            else if (this.Type == AttributeType._Time)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Time;
            }
            else if (this.Type == AttributeType._Bool)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Bool;
            }
            else if (this.Type == AttributeType._Lookup)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Lookup;
            }
            else if (this.Type == AttributeType._MultiLookup)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._MultiLookup;
            }
            else if (this.Type == AttributeType._Picture)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Picture;
            }
            else if (this.Type == AttributeType._File)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._File;
            }
            else if (this.Type == AttributeType._Auto)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Auto;
            }
            else if (this.Type == AttributeType._None)
            {
                base.FieldType = Entitifier.Core.EntityFieldType._Text;
            }
        }
        private void SetDefault() {
            this.UsageFieldType = UsageType._InputField;
            this.ComponentKey = this.ComponentKey == null ? "" : this.ComponentKey;
            this.RegExpression = this.RegExpression == null ? "" : RegExpression;
            this.ParentComponentKey = this.ParentComponentKey == null ? "" : ParentComponentKey;
            this.ParentAttribute = this.ParentAttribute == null ? "" : ParentAttribute;

        }
        protected  override bool Save()
        {
            SetDefault();
            if (base.Save())
            {
                return datacomponentAttribute.SaveComponentAttribute(base.InstanceID, this.Type, this.ComponentKey, this.Cryptography, 
                    this.RegExpression, this.ParentComponentKey,
                this.ParentAttribute,this.UsageFieldType);
            }
            else {
                return false;
            }            
        }
        protected  override bool Remove()
        {
            if (base.Remove()) {
                if (datacomponentAttribute.Delete(this.ComponentKey, this.AttributeID))
                {
                    return true;
                }
                else return false;
            }            
            else return false;
        }
        protected  override bool Hide()
        {
            return base.Hide();
        }
        protected  override void Load()
        {
            DataTable dt = new DataTable();
            dt = datacomponentAttribute.GetComponentAttribute(this.ComponentKey, this.AttributeID);
            var cat = dt.AsEnumerable().Select(g => new ComponentAttribute
            {
                InstanceID = g.IsNull("FieldInstanceID") ? 0 : g.Field<int>("FieldInstanceID"),
                Type = g.IsNull("Attributetype") ? AttributeType._None : g.Field<AttributeType>("FieldInstanceID"),
                ComponentKey = g.IsNull("componentKey") ? "" : g.Field<string>("componentKey"),
                Cryptography = g.IsNull("cryptography") ? 0 : g.Field<int>("cryptography"),
                RegExpression = g.IsNull("regExpression") ? "" : g.Field<string>("regExpression"),
                ParentComponentKey = g.IsNull("parentComponent") ? "" : g.Field<string>("parentComponent"),
                ParentAttribute = g.IsNull("parentAttribute") ? "" : g.Field<string>("parentAttribute"),
                UsageFieldType = g.IsNull("UsageFieldType") ?  UsageType._InputField : g.Field<UsageType>("UsageFieldType"),
            }).FirstOrDefault();
            this.InstanceID = cat.InstanceID;
            this.Type = cat.Type;
            this.ComponentKey = cat.ComponentKey;
            this.Cryptography = cat.Cryptography;
            this.RegExpression = cat.RegExpression;
            this.ParentAttribute = cat.ParentAttribute;
            this.ParentComponentKey = cat.ParentComponentKey;
            this.UsageFieldType = cat.UsageFieldType;
          base.Load();
        }    
        public ComponentAttribute(int attributeID,string componentKey)
        {
            ComponentKey = componentKey;
            AttributeID = attributeID;
            datacomponentAttribute = new Data.ComponentAttribute();
            Load();
        }        
        public  ComponentAttribute() {
            UsageFieldType = UsageType._InputField;
            SetDefault();
                   datacomponentAttribute = new Data.ComponentAttribute();
        }
    }

    
}
