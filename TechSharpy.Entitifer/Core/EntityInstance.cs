using System;
using System.Collections.Generic;
using System.Text;

namespace TechSharpy.Entitifier.Core
{
    public class EntityField
    {
        public string Name;
        public Int32 InstanceID;
        public EntityFieldType FieldType;
        public bool IsKey;
        public bool IsRequired;
        public bool IsUnique;
        public Int32 LookUpID;
        public bool IsCore;
        public bool EnableEncription;
        public Int32 EntityKey;
        public string Value;
        public bool IsReadOnly;
        public string DefaultValue;
        public int DisplayOrder;
        public List<string> LookUpArray;
        public bool enableLimit;
        public string Min;
        public string Max;
        public int MaxLength;
        public string DisplayName; 
        public bool AutoIncrement;
        public Int64 Incrementfrom;
        public Int64 Incrementby;
        public bool IsShow;
        public string Description;
        private Data.EntitySchema dataEntity;
        public EntityField() {
            InstanceID = -1;
            Name = "";
            InstanceID = -1;
            FieldType = EntityFieldType._Text;
            IsKey = false;
            IsRequired = false;
            IsUnique = false;
            LookUpID = -1;
            IsCore = false;
            EntityKey = -1;
            Value = "";
            IsReadOnly = false;
            DefaultValue = "";
            DisplayOrder = -1;
            LookUpArray = new List<string>();
            Min = "0";
            Max = "0";
            MaxLength = 0;
            DisplayName = "";
            AutoIncrement = false;
            Incrementfrom = 0;
            Incrementby = 0;
            Description = "";
            enableLimit = false;
            EnableEncription = false;
            dataEntity = new Data.EntitySchema();
        }
        public EntityField(string name, int instanceID, EntityFieldType fieldType, bool isKey, bool isRequired, bool isUnique, 
            int lookUpID, bool isCore, int entityKey, string value, bool isReadOnly, string defaultValue, int displayOrder,
            List<string> lookUpArray, string min, string max, int maxLength, string displayName, bool autoIncrement, long incrementfrom,
            long incrementby,string description,bool enableencription,bool enablelimit)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            InstanceID = instanceID;
            FieldType = fieldType;
            IsKey = isKey;
            IsRequired = isRequired;
            IsUnique = isUnique;
            LookUpID = lookUpID;
            IsCore = isCore;
            EntityKey = entityKey;
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsReadOnly = isReadOnly;
            DefaultValue = defaultValue ?? throw new ArgumentNullException(nameof(defaultValue));
            DisplayOrder = displayOrder;
            LookUpArray = lookUpArray ?? throw new ArgumentNullException(nameof(lookUpArray));
            Min = min ?? throw new ArgumentNullException(nameof(min));
            Max = max ?? throw new ArgumentNullException(nameof(max));
            MaxLength = maxLength;
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            AutoIncrement = autoIncrement;
            Incrementfrom = incrementfrom;
            Incrementby = incrementby;
            IsShow = true;
            Description = description;
            enableLimit = enablelimit;
            EnableEncription = enableencription;
            dataEntity = new Data.EntitySchema();
        }

        public EntityField(int pinstanceID)
        {
            InstanceID = pinstanceID;
            Name = "";
            InstanceID = -1;
            FieldType = EntityFieldType._Text;
            IsKey = false;
            IsRequired = false;
            IsUnique = false;
            LookUpID = -1;
            IsCore = false;
            EntityKey = -1;
            Value = "";
            IsReadOnly = false;
            DefaultValue = "";
            DisplayOrder = -1;
            LookUpArray =  new List<string>();
            Min = "0";
            Max = "0";
            MaxLength = 0;
            DisplayName = "";
            AutoIncrement = false ;
            Incrementfrom = 0;
            Incrementby = 0;
            EnableEncription = false;
            Description = "";
            enableLimit = false;
            dataEntity = new Data.EntitySchema();
        }

        public bool Save() {
             
            if (this.InstanceID > 0)
            {
                return (dataEntity.SaveField(this.Name, this.InstanceID, this.FieldType, this.IsKey, this.IsRequired, this.IsUnique, this.LookUpID, this.IsCore, this.EntityKey, this.Value, this.IsReadOnly
                , this.DefaultValue, this.DisplayOrder, new List<string>(), this.Min, this.Max, this.MaxLength, this.DisplayName, this.AutoIncrement, this.Incrementfrom,
                this.Incrementby, this.Description, this.EnableEncription, this.enableLimit));                                        

            }
            else {
                int next = 0;
                next =  dataEntity.SaveField(this.Name, this.FieldType, this.IsKey, this.IsRequired, this.IsUnique, this.LookUpID, this.IsCore, this.EntityKey, this.Value, this.IsReadOnly
                , this.DefaultValue, this.DisplayOrder, new List<string>(), this.Min, this.Max, this.MaxLength, this.DisplayName, this.AutoIncrement, this.Incrementfrom,
                this.Incrementby, this.Description, this.EnableEncription, this.enableLimit);
                this.InstanceID = next;
                if (next > 0) {
                    return true;
                }               
            }            
            return false;
        }

        public bool Remove() {
            return true;
        }

        public bool Hide() {
            return true;
        }

    }
}
