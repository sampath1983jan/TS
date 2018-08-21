using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TechSharpy.Data;

namespace TechSharpy.Entitifier.Core
{
    [Serializable]
    public abstract class FieldAttribute {
        public string Name;
        public Int32 InstanceID;
        public EntityFieldType FieldType;
        public bool IsKey;
        public bool IsRequired;
        public bool IsUnique;
        public Int32 LookUpID;
        public bool IsCore;
        public bool EnableEncription;
        public  Int32 EntityKey;
        public string Value;
        public bool IsReadOnly;
        public string DefaultValue;
        public int DisplayOrder;
        public List<string> LookUpArray;
        public bool enableContentLimit;
        public string Min;
        public string Max;
        public int MaxLength;
        public string DisplayName;
        public bool AutoIncrement;
        public int Incrementfrom;
        public int Incrementby;
        public bool IsShow;
        public string Description;

        protected  abstract bool Save();
        protected  abstract bool Remove();
        protected  abstract bool Hide();
        protected  abstract void Load();
    }
    [Serializable]
    public class EntityField: FieldAttribute
    {    
        
                
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
            enableContentLimit = false;
            EnableEncription = false;
            dataEntity = new Data.EntitySchema();
        }
        public EntityField(string name, int instanceID, EntityFieldType fieldType, bool isKey, bool isRequired, bool isUnique, 
            int lookUpID, bool isCore, int entityKey, string value, bool isReadOnly, string defaultValue, int displayOrder,
            List<string> lookUpArray, string min, string max, int maxLength, string displayName, bool autoIncrement, int incrementfrom,
            int incrementby,string description,bool enableencription,bool enablelimit)
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
            enableContentLimit = enablelimit;
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
            enableContentLimit = false;
            dataEntity = new Data.EntitySchema();
        }
        internal bool SaveField() {
            return Save();
        }
        protected  override bool Save()
        {
            if (this.InstanceID > 0)
            {
                if (dataEntity.SaveField(this.Name, this.InstanceID, this.FieldType, this.IsKey, this.IsRequired, this.IsUnique, this.LookUpID, this.IsCore, this.EntityKey, this.Value, this.IsReadOnly
                , this.DefaultValue, this.DisplayOrder, new List<string>(), this.Min, this.Max, this.MaxLength, this.DisplayName, this.AutoIncrement, this.Incrementfrom,
                this.Incrementby, this.Description, this.EnableEncription, this.enableContentLimit)) {
                    return true;

                     
                }
            }
            else
            {
                int next = 0;
                next = dataEntity.SaveField(this.Name, this.FieldType, this.IsKey, this.IsRequired, this.IsUnique, this.LookUpID, this.IsCore, this.EntityKey, this.Value, this.IsReadOnly
                , this.DefaultValue, this.DisplayOrder, new List<string>(), this.Min, this.Max, this.MaxLength, this.DisplayName, this.AutoIncrement, this.Incrementfrom,
                this.Incrementby, this.Description, this.EnableEncription, this.enableContentLimit);
                this.InstanceID = next;
                if (next > 0)
                {
                    return true;
                }
            }
            return false;
        }

        protected  override bool Remove()
        {
            if (dataEntity.DeleteEntityField(-1, this.EntityKey, this.InstanceID))
            {
                return true;
            }
            else return false;            
        }

        protected  override bool Hide()
        {
            return true;
        }

        protected  override void Load()
        {
            DataTable dt = new DataTable();
            dt= dataEntity.GetEntityField(-1, this.EntityKey, this.InstanceID);

            var cat = dt.AsEnumerable().Select(g => new EntityField
            {
                EntityKey = g.IsNull("EntityID") ? 0 : g.Field<int>("EntityID"),
                InstanceID = g.IsNull("FieldID") ? 0 : g.Field<int>("FieldID"),
                DisplayName = g.IsNull("DisplayName") ? "" : g.Field<string>("DisplayName"),
                Name = g.IsNull("FieldName") ? "" : g.Field<string>("FieldName"),
                Description = g.IsNull("FieldDescription") ?"" : g.Field<string>("FieldDescription"),
                FieldType = g.IsNull("FieldType") ? EntityFieldType._Text : g.Field<EntityFieldType>("FieldType"),
                LookUpID = g.IsNull("LookUpId") ? 0 : g.Field<int>("LookUpId"),
                IsRequired = g.IsNull("isRequired") ? false : g.Field<bool>("isRequired"),
                IsUnique = g.IsNull("isUnique") ? false : g.Field<bool>("isUnique"),
                IsKey = g.IsNull("isKeyField") ? false : g.Field<bool>("isKeyField"),
                AutoIncrement = g.IsNull("autoIncrement") ? false : g.Field<bool>("autoIncrement"),
                MaxLength = g.IsNull("Contentlimit") ? 0 : g.Field<int>("Contentlimit"),
                Incrementfrom = g.IsNull("incrementfrom") ? 0 : g.Field<int>("incrementfrom"),
                Incrementby = g.IsNull("incrementby") ? 0 : g.Field<int>("incrementby"),
                Value = g.IsNull("value") ? "" : g.Field<string>("value"),
                DefaultValue = g.IsNull("defaultValue") ? "" : g.Field<string>("defaultValue"),
                DisplayOrder = g.IsNull("displayOrder") ? 0 : g.Field<int>("displayOrder"),
                Min = g.IsNull("Minimum") ? "" : g.Field<string>("Minimum"),
                Max = g.IsNull("Maximum") ? "" : g.Field<string>("Maximum"),
                IsCore = g.IsNull("isCoreField") ? false : g.Field<bool>("isCoreField"),
                IsReadOnly = g.IsNull("isReadOnly") ? false : g.Field<bool>("isReadOnly"),
                EnableEncription = g.IsNull("EnableEncription") ? false : g.Field<bool>("EnableEncription"),
                enableContentLimit = g.IsNull("EnableContentlimit") ? false : g.Field<bool>("EnableContentlimit"),
            }).FirstOrDefault();

            this.EntityKey = cat.EntityKey;
            this.InstanceID = cat.InstanceID;
            this.DisplayOrder = cat.DisplayOrder;
            this.DisplayName = cat.DisplayName;
            this.Name = cat.Name;
            this.Description = cat.Description;
            this.FieldType = cat.FieldType;
            LookUpID = cat.LookUpID;            
            IsRequired = cat.IsRequired;
            IsUnique = cat.IsUnique;
            this.IsCore = cat.IsCore;
            this.IsReadOnly = cat.IsReadOnly;
            this.EnableEncription = cat.EnableEncription;
            this.enableContentLimit = cat.enableContentLimit;
            this.IsKey = cat.IsKey;
            this.AutoIncrement = cat.AutoIncrement;
            this.MaxLength = cat.MaxLength;
            this.Incrementfrom = cat.Incrementfrom;
            this.Incrementby = cat.Incrementby;
            this.Value = cat.Value;
            this.DefaultValue = cat.DefaultValue;
            this.Min = cat.Min;
            this.Max = cat.Max;
            


        }

         

        
    }


    


     


}


