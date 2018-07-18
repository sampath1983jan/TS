using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Component.Attributes
{
    public enum AttributeType{
        //_Number = 1,
        //_Float = 2,
        //_Text = 3,
        //_LongText = 4,
        //_Date = 5,
        //_DateTime = 6,
        //_Time = 7,
        //_Bool = 8,
        //_Entity = 9,
        //_Lookup = 10,
        //_MultiLookup = 11,
        //_Picture = 12,
        //_File = 13,
        _None=-1,
        _Auto = 14,
        _Year=15,
        _Month=16,
        _Quarter=17, 
        _Calculator=18
    }
public class ComponentAttribute:TechSharpy.Entitifier.Core.EntityField 
    {                       
        public int AttributeID;
        public AttributeType Type;         
        public string ComponentKey;      
        public int Cryptography;
        public string RegExpression;
        public int ParentAttribute;
        public string ParentComponentKey;
        private Data.ComponentAttribute datacomponentAttribute;

        public override bool Save()
        {             
            if (base.Save())
            {
                return datacomponentAttribute.SaveComponentAttribute(base.InstanceID, this.Type, this.ComponentKey, this.Cryptography, this.RegExpression, this.ParentComponentKey,
                this.ParentAttribute);
            }
            else {
                return false;
            }            
        }
        public override bool Remove()
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
        public override bool Hide()
        {
            return base.Hide();
        }
        public override void Load()
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
                ParentAttribute = g.IsNull("parentAttribute") ? 0 : g.Field<int>("parentAttribute"),
            }).FirstOrDefault();
            this.InstanceID = cat.InstanceID;
            this.Type = cat.Type;
            this.ComponentKey = cat.ComponentKey;
            this.Cryptography = cat.Cryptography;
            this.RegExpression = cat.RegExpression;
            this.ParentAttribute = cat.ParentAttribute;
            this.ParentComponentKey = cat.ParentComponentKey;
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
            datacomponentAttribute = new Data.ComponentAttribute();
        }
    }

    
}
