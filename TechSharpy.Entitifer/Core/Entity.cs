using System;
using System.Collections.Generic;
using System.Data;
using TechSharpy.Data;
using System.Linq;
using TechSharpy.Services;
using TechSharpy.Entitifier.Data;
namespace TechSharpy.Entitifier.Core
{
    public enum EntityType
    {
        _None=0,
        _Master=1,
        _MasterAttribute=2,
        _RelatedMaster=3,
        _Transaction=4,        
        _Sudo=5,
        _System=6
    }
    public enum EntityFieldType {
        _Number=1,
        _Float=2,
        _Text=3,
        _LongText=4,
        _Date=5,
        _DateTime=6,
        _Time=7,
        _Bool=8,
        _Entity=9,
        _Lookup=10,
        _MultiLookup=11,
        _Picture=12,
        _File=13,
        _Auto=14,
    }

    public class EntitySchema
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private const string ModuleName = "Data Schema";
        /// <summary>
        /// 
        /// </summary>
        protected string Name;
        /// <summary>
        /// 
        /// </summary>
        protected TechSharpy.Entitifier.Core.EntityType EntityType;
        /// <summary>
        /// 
        /// </summary>
        protected Int32 EntityKey;
        /// <summary>
        /// 
        /// </summary>
        protected string TableName;
        /// <summary>
        /// 
        /// </summary>
        public List<string> PrimaryKeys;
        /// <summary>
        /// 
        /// </summary>
        protected string Description;
        /// <summary>
        /// 
        /// </summary>
        public bool IsShow;
        /// <summary>
        /// 
        /// </summary>
        protected List<Trigger> Triggers;
        /// <summary>
        /// 
        /// </summary>
        protected List<EntityField> EntityInstances;        
        private Data.EntitySchema dataEntity;
        private Services.ErrorHandling.ErrorInfoCollection Errors;
        #endregion
        
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public EntitySchema()
        {
            dataEntity = new Data.EntitySchema();
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="entityType"></param>
        /// <param name="entityKey"></param>
        /// <param name="tableName"></param>
        /// <param name="primaryKeys"></param>
        public EntitySchema(string name, string description, EntityType entityType, int entityKey, string tableName, List<string> primaryKeys)
        {
            Description = description;
            Name = name;
            EntityType = entityType;
            EntityKey = entityKey;
            TableName = tableName;
            PrimaryKeys = primaryKeys;
            IsShow = true;
            dataEntity = new Data.EntitySchema();
            EntityInstances = new List<EntityField>();
            Triggers = new List<Trigger>();
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityKey"></param>
        public EntitySchema(int entityKey)
        {
            this.Name = "";
            this.Description = "";
            EntityKey = entityKey;
            PrimaryKeys = new List<string>();
            dataEntity = new Data.EntitySchema();
            EntityInstances = new List<EntityField>();
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
            Triggers = new List<Trigger>();
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
            Init();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        public EntitySchema(EntityType entityType)
        {
            this.Name = "";
            this.Description = "";
            EntityType = entityType;
            EntityKey = -1;
            dataEntity = new Data.EntitySchema();
            EntityInstances = new List<EntityField>();
            PrimaryKeys = new List<string>();
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
    
            Triggers = new List<Trigger>();
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        private void SetDefault() {
            this.Name =  Name == null ?  this.TableName: this.Name;
            this.Description = Description == null ? "" : this.Description;
            PrimaryKeys = PrimaryKeys == null ? new List<string>() : PrimaryKeys;
            this.TableName = TableName == null ? "" : this.TableName;
            this.EntityInstances = EntityInstances == null ? new List<EntityField>() : this.EntityInstances;
        }



        #region Methods or procedures
        /// <summary>
        /// Save Entity 
        /// </summary>
        /// <returns></returns>
        internal protected Services.ErrorHandling.ErrorInfoCollection Save()
        {
            SetDefault();
            if (this.TableName == "")
            {
                Errors.Add("Table Name not generated.Contact system admin", Services.ErrorHandling.ErrorInfo.ErrorType._critical);
                return Errors;
            }
            else if (this.EntityInstances.Count ==0) {
                Errors.Add("Unable to generate table because field count is zero contact system admin", Services.ErrorHandling.ErrorInfo.ErrorType._critical);
                return Errors;
            }
            TQueryBuilder tq;
            if (this.EntityKey > 0)
            {
                if (!dataEntity.CheckEntityExist(this.TableName, this.EntityKey))
                {
                    EntitySchema en = new EntitySchema(this.EntityKey);
                    if (dataEntity.Update(-1, this.EntityKey, this.TableName, this.Name, this.Description, string.Join(",", this.PrimaryKeys.ToArray()), this.EntityType))
                    {
                        Errors.Add(ModuleName + "updated successfully", Services.ErrorHandling.ErrorInfo.ErrorType._noerror);                        
                        tq = new TQueryBuilder(TQueryType._AddPrimarykey);
                        if (this.EntityKey > 0)
                        {
                            tq.TableName(this.TableName.Replace(" ", ""));
                            foreach (EntityField fd in this.EntityInstances)
                            {
                                if (fd.IsKey == true) {
                                    tq.AddField(fd.Name, fd.IsKey, fd.IsUnique, getDataType(fd.FieldType), true, "");
                                }                                
                            }
                            dataEntity.ExecuteNonQuery(tq);
                        }
                    }
                    else
                    {
                        Errors.Add("Unable to update " + ModuleName, Services.ErrorHandling.ErrorInfo.ErrorType._critical);
                    }
                }
                else {
                    Errors.Add("table already exist", Services.ErrorHandling.ErrorInfo.ErrorType._critical);
                }
                //Write code here to update dataschema when updating tabl name,primarykeysss
            }
            else
            {
                if (!dataEntity.CheckEntityExist(this.TableName,-1))
                {
                    this.EntityKey = dataEntity.Save(-1, this.TableName, this.Name, this.Description, string.Join(",", this.PrimaryKeys.ToArray()), this.EntityType);
                    tq = new TQueryBuilder(TQueryType._Create);
                    if (this.EntityKey > 0)
                    {
                        tq.TableName(this.TableName.Replace(" ", ""));
                        foreach (EntityField fd in this.EntityInstances)
                        {                            
                                tq.AddField(fd.Name, fd.IsKey, fd.IsUnique, getDataType(fd.FieldType), true, "");                                                        
                        }
                        dataEntity.ExecuteNonQuery(tq);
                    }
                }
                else
                {
                    Errors.Add("table already exist", Services.ErrorHandling.ErrorInfo.ErrorType._critical);
                }
            }
            return Errors;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pClientID"></param>
        /// <param name="pEntityFieldID"></param>
        /// <param name="pEntityID"></param>
        /// <param name="pFieldName"></param>
        /// <param name="pFieldDescription"></param>
        /// <param name="pFieldType"></param>
        /// <param name="LookUpID"></param>
        /// <param name="pIsRequired"></param>
        /// <param name="pIsUnique"></param>
        /// <param name="pIsKeyField"></param>
        /// <param name="pEnableContentLimit"></param>
        /// <param name="pContentLimit"></param>
        /// <param name="pMin"></param>
        /// <param name="pMax"></param>
        /// <param name="pFileExtension"></param>
        /// <param name="pIsCore"></param>
        /// <param name="pIsEditable"></param>
        /// <param name="pEnableEncription"></param>
        /// <param name="pAcceptNull"></param>
        /// <param name="pDisplayName"></param>
        /// <param name="value"></param>
        /// <param name="isReadonly"></param>
        /// <param name="defaultValue"></param>
        /// <param name="displayorder"></param>
        /// <param name="pmaxLength"></param>
        /// <param name="displayName"></param>
        /// <param name="autoIncrement"></param>
        /// <param name="incrementfrom"></param>
        /// <param name="incrementby"></param>
        /// <returns></returns>
        internal protected bool AddField(int pClientID, int pEntityFieldID, Int32 pEntityID, string pFieldName, string pFieldDescription, EntityFieldType pFieldType,
           int LookUpID, bool pIsRequired, bool pIsUnique, bool pIsKeyField,
          bool pEnableContentLimit, string pMin, string pMax, string pFileExtension,
           bool pIsCore, bool pIsEditable, bool pEnableEncription, bool pAcceptNull, string pDisplayName, string value, bool isReadonly,
           string defaultValue, int displayorder, int pmaxLength, string displayName, bool autoIncrement, 
           int incrementfrom, int incrementby)
        {
            TQueryBuilder tq;
            EntityField fd = new EntityField(pFieldName, pEntityFieldID, pFieldType, pIsKeyField, pIsRequired, pIsUnique, LookUpID, pIsCore, pEntityID, value, isReadonly,
                defaultValue, displayorder, new List<string>(), pMin, pMax, pmaxLength, displayName, autoIncrement, incrementfrom, incrementby,
                Description, pEnableEncription, pEnableContentLimit);

            fd.InstanceID = pEntityFieldID;
            if (pEntityFieldID <= 0)
            {
                tq = new TQueryBuilder(TQueryType._AlterTable);
                tq.TableName(this.TableName.Replace(" ", ""));
                if (fd.SaveField())
                {
                    if (fd.InstanceID > 0)
                    {
                        tq.AddField(fd.Name, fd.IsKey, fd.IsUnique, getDataType(fd.FieldType), true, "");
                        dataEntity.ExecuteNonQuery(tq);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
               
                return true;
            }
          
        }
        /// <summary>
        /// remove Entity 
        /// </summary>
        /// <returns></returns>
        internal protected bool Remove()
        {
            if (dataEntity.Delete(-1, this.EntityKey))
            {
                return dataEntity.DeleteEntityFields(-1, this.EntityKey);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntityFieldID"></param>
        /// <returns></returns>
        internal protected bool RemoveField(int EntityFieldID)
        {
            return dataEntity.DeleteEntityField(-1, this.EntityKey, EntityFieldID);
        }
        /// <summary>
        /// hide entityschema
        /// </summary>
        /// <returns></returns>
        internal protected bool Hide()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        internal protected bool addTrigger(Trigger trigger) {
            if (trigger.Save())
            {
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// Load EntitySchema
        /// </summary>
        internal protected void Init()
        {
            DataTable dt = new DataTable();
            dt = dataEntity.GetEntity(-1, EntityKey);
            var e = dt.AsEnumerable().Select(g => new EntitySchema
            {
                EntityKey = g.IsNull("EntityID") ? 0 : g.Field<int>("EntityID"),
                Name = g.IsNull("Name") ? "" : g.Field<string>("Name"),
                TableName = g.IsNull("TableName") ? "" : g.Field<string>("TableName"),
                PrimaryKeys = g.IsNull("Keys") ? new List<string>() : g.Field<string>("Keys").Split(',').ToList(),
                Description = g.IsNull("Description") ? "" : g.Field<string>("Description"),
                EntityType = g.IsNull("Type") ? EntityType._Master : g.Field<EntityType>("Type")
            }).First();
            this.EntityKey = e.EntityKey;
            this.Name = e.Name;
            this.TableName = e.TableName;
            this.EntityType = e.EntityType;
            this.Description = e.Description;
            this.PrimaryKeys = e.PrimaryKeys;
            InitField();
          //  InitTrigger();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitTrigger() {
            DataTable dt = new DataTable();           
            Data.Trigger tr = new Data.Trigger();
           dt= tr.getTriggers(this.EntityKey);
        this.Triggers  =    dt.AsEnumerable().Select(g => new Trigger
            {
            TriggerID = g.IsNull("TriggerID") ? -1 : g.Field<int>("TriggerID"),            
            Entitykey = g.IsNull("Entitykey") ? -1 : g.Field<int>("Entitykey"),
            TriggerName = g.IsNull("TriggerName") ? "" : g.Field<string>("TriggerName"),
            Type = g.IsNull("eventType") ? EventType._insert : g.Field<EventType>("eventType"),
            Action = g.IsNull("Actiontype") ? ActionType._after: g.Field<ActionType>("Actiontype"),
            Steps = g.IsNull("steps") ?  new List<string>() : g.Field<string>("steps").Split(',').ToList(),                 
            }).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitField()
        {
            DataTable dt = new DataTable();
            dt = dataEntity.GetEntityFields(-1, this.EntityKey);
            EntityInstances = dt.AsEnumerable().Select(g => new EntityField
            {
                EntityKey = this.EntityKey,
                //ClientI = -1,
                InstanceID = g.IsNull("FieldID") ? -1 : g.Field<int>("FieldID"),
                Name = g.IsNull("FieldName") ? "" : g.Field<string>("FieldName"),
                Description = g.IsNull("FieldDescription") ? "" : g.Field<string>("FieldDescription"),
                FieldType = g.IsNull("FieldType") ? EntityFieldType._Text : g.Field<EntityFieldType>("FieldType"),
                LookUpID = g.IsNull("LookUpId") ? -1 : g.Field<int>("LookUpId"),
                IsRequired = g.IsNull("isRequired") ? false : g.Field<object>("isRequired").ToString() =="1" ? true:false,
                IsUnique = g.IsNull("isUnique") ? false : g.Field<object>("isUnique").ToString() == "1" ? true : false,
                IsKey = g.IsNull("isKeyField") ? false : g.Field<object>("isKeyField").ToString() == "1" ? true : false,
                IsCore = g.IsNull("isCoreField") ? false : g.Field<object>("isCoreField").ToString() == "1" ? true : false,
                IsReadOnly = g.IsNull("IsReadOnly") ? false : g.Field<object>("IsReadOnly").ToString() == "1" ? true : false,
                EnableEncription = g.IsNull("EnableEncription") ? false : g.Field<object>("EnableEncription").ToString() == "1" ? true : false,
                enableContentLimit = g.IsNull("EnableContentlimit") ? false : g.Field<object>("EnableContentlimit").ToString() == "1" ? true : false,
                //  FileExtension = g.IsNull("FileExtension") ? "" : g.Field<string>("FileExtension"),
                //  LookUpArray = g.IsNull("LookUpArray") ? new List<string>() : g.Field<string>("LookUpArray").Split(',').ToList(),
                DisplayOrder = g.IsNull("DisplayOrder") ? 0 : g.Field<int>("DisplayOrder"),
                MaxLength = g.IsNull("Contentlimit") ? 0 : g.Field<int>("Contentlimit"),
                Min = g.IsNull("Minimum") ? "-1" : g.Field<string>("Minimum"),
                Max = g.IsNull("Maximum") ? "-1" : g.Field<string>("Maximum"),
                DisplayName = g.IsNull("DisplayName") ? "" : g.Field<string>("DisplayName"),
                DefaultValue = g.IsNull("DefaultValue") ? "" : g.Field<string>("DefaultValue"),
                Value = g.IsNull("Value") ? "" : g.Field<string>("Value"),
                  AutoIncrement = g.IsNull("AutoIncrement") ? false : g.Field<object>("AutoIncrement").ToString() == "1" ? true : false,
                Incrementby = g.IsNull("Incrementby") ? 0 : g.Field<int>("Incrementby"),
                Incrementfrom = g.IsNull("Incrementfrom") ? 0 : g.Field<int>("Incrementfrom"),
               // IsShow = g.IsNull("IsShow") ? false : g.Field<bool>("IsShow"),
            }).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eft"></param>
        /// <returns></returns>
        internal protected TechSharpy.Data.FieldType getDataType(EntityFieldType eft)
        {
            if (eft == EntityFieldType._Number)
            {
                return FieldType._Number;
            }
            else if (eft == EntityFieldType._Text)
            {
                return FieldType._String;
            }
            else if (eft == EntityFieldType._LongText) {
                return FieldType._Text;
            }
            else if (eft == EntityFieldType._Auto)
            {
                return FieldType._String;
            }
            else if (eft == EntityFieldType._Bool)
            {
                return FieldType._Question;
            }
            else if (eft == EntityFieldType._Date)
            {
                return FieldType._Date;
            }
            else if (eft == EntityFieldType._DateTime)
            {
                return FieldType._DateTime;
            }
            else if (eft == EntityFieldType._File)
            {
                return FieldType._Text;
            }
            else if (eft == EntityFieldType._Picture)
            {
                return FieldType._Text;
            }
            else if (eft == EntityFieldType._Time)
            {
                return FieldType._DateTime;
            }
            else if (eft == EntityFieldType._Float)
            {
                return FieldType._Decimal;
            }
            else if (eft == EntityFieldType._LongText)
            {
                return FieldType._Text;
            }
            else if (eft == EntityFieldType._Lookup)
            {
                return FieldType._Number;
            }
            else if (eft == EntityFieldType._MultiLookup)
            {
                return FieldType._Text;
            }
            else if (eft == EntityFieldType._Entity)
            {
                return FieldType._Number;
            }
            else
            {
                return FieldType._String;
            }

        }

        #endregion

    }
}
