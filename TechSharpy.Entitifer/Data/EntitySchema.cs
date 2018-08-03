using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Entitifier.Data
{
    public class EntitySchema

    {
        DataTable dtResult;
        TechSharpy.Data.DataBase rd;
        public EntitySchema()
        {
            try
            {
                rd = new DataBase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetEntityList(int ClientID)
        {
            dtResult = new DataTable();
            //dtResult.Pivot("","");
            Query selectQ = new QueryBuilder(QueryType._Select).AddTable("s_entity").AddField("*", "s_entity").
                AddWhere(0, "s_entity", "ClientID", FieldType._Number, Operator._Equal, ClientID.ToString());
            dtResult = rd.ExecuteQuery(selectQ).GetResult;
            return dtResult;
        }

        public DataTable GetEntity(int ClientID, Int64 EntityID)
        {
            dtResult = new DataTable();
            Query selectQ = new QueryBuilder(QueryType._Select).AddTable("s_entity").AddField("*", "s_entity").
                AddWhere(0, "s_entity", "ClientID", FieldType._Number, Operator._Equal, ClientID.ToString()).
                AddWhere(0, "s_entity", "EntityID", FieldType._Number, Operator._Equal, EntityID.ToString());
            dtResult = rd.ExecuteQuery(selectQ).GetResult;
            return dtResult;
        }

        public Boolean DeleteEntityFields(int pClientID, int pEntityID)
        {
            Query DeleteQ = new QueryBuilder(QueryType._Delete).AddTable("s_entityfields").
                AddWhere(0, "s_entity", "ClientID", FieldType._Number, Operator._Equal, pClientID.ToString(),Condition._And).
               AddWhere(0, "s_entityfields", "EntityID", FieldType._Number, Operator._Equal, pEntityID.ToString());
           // int iResult;
           // iResult = (rd.ExecuteQuery(DeleteQ).Result);
            if ((rd.ExecuteQuery(DeleteQ).Result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DeleteEntityField(int pClientID, int pEntityID,int pEntityFieldID)
        {
            Query DeleteQ = new QueryBuilder(QueryType._Delete).AddTable("s_entityfields").AddWhere(0, "s_entityfields", "ClientID", FieldType._Number, Operator._Equal, pClientID.ToString()).
               AddWhere(0, "s_entityfields", "FieldID", FieldType._Number, Operator._Equal, pEntityFieldID.ToString(),Condition._And)
               .AddWhere(0, "s_entityfields", "EntityID", FieldType._Number, Operator._Equal, pEntityID.ToString());
         //   int iResult;
          
            if ((rd.ExecuteQuery(DeleteQ).Result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Delete(int pClientID, int pEntityID)
        {
            Query DeleteQ = new QueryBuilder(QueryType._Delete).AddTable("s_entity").AddWhere(0, "s_entity", "ClientID", FieldType._Number, Operator._Equal, pClientID.ToString()).
               AddWhere(0, "s_entity", "EntityID", FieldType._Number, Operator._Equal, pEntityID.ToString());
         //   int iResult;
           // iResult = rd.ExecuteQuery(DeleteQ);
            if ((rd.ExecuteQuery(DeleteQ).Result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Save(int pClientID, string pTableName, string Name, string pDescription, string keys, TechSharpy.Entitifier.Core.EntityType pType)
        {
            int NextID = rd.getNextID("Entity");
            Query iQuery = new QueryBuilder(QueryType._Insert
                ).AddTable("s_entity")
                .AddField("EntityID", "s_entity", FieldType._Number, "", NextID.ToString())
                .AddField("Name", "s_entity", FieldType._String, "", Name.ToString())
                .AddField("Keys", "s_entity", FieldType._String, "", keys.ToString())
                .AddField("TableName", "s_entity", FieldType._String, "", pTableName.ToString())
                .AddField("Description", "s_entity", FieldType._String, "", pDescription.ToString())
                .AddField("Type", "s_entity", FieldType._Number, "", Convert.ToInt32(pType).ToString())
                .AddField("ClientID", "s_entity", FieldType._Number, "", pClientID.ToString())
          .AddField("LastUPD", "s_entity", FieldType._DateTime, "", DateTime.Now.ToString());
            if ((rd.ExecuteQuery(iQuery).Result))
            {
                return NextID;
            }
            else
            {
                return -1;
            }
        }

        public bool Update(int pClientID, Int64 pEntityID, string pTableName, string Name, string pDescription, string keys,
           TechSharpy.Entitifier.Core.EntityType pType)
        {
            Query iQuery = new QueryBuilder(QueryType._Update
                ).AddTable("s_entity")
                .AddField("Description", "s_entity", FieldType._String, "", pDescription.ToString())
                .AddField("Name", "s_entity", FieldType._String, "", Name.ToString())
                .AddField("Keys", "s_entity", FieldType._String, "", keys.ToString())
                .AddField("TableName", "s_entity", FieldType._String, "", pTableName.ToString())
                .AddField("Description", "s_entity", FieldType._String, "", pDescription.ToString())
                .AddField("Type", "s_entity", FieldType._Number, "", Convert.ToInt32(pType).ToString())
            // .AddField("ClientID", "s_entity", FieldType._Number, "", pClientID.ToString())
            .AddField("lastUpD", "s_entity", FieldType._DateTime, "", DateTime.Now.ToString())
           .AddWhere(0, "s_entity", "ClientID", FieldType._Number, Operator._Equal, pClientID.ToString()).
               AddWhere(0, "s_entity", "EntityID", FieldType._Number, Operator._Equal, pEntityID.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckEntityExist(string EntityName,int entitykey)
        {
            dtResult = new DataTable();
            Query selectQ = new QueryBuilder(QueryType._Select).AddTable("s_entity").AddField("*", "s_entity").
                AddWhere(0, "s_entity", "entityID", FieldType._Number, Operator._NotEqual, entitykey.ToString()).
               AddWhere(0, "s_entity", "TableName", FieldType._String, Operator._Equal, EntityName.ToString());
             
            dtResult = rd.ExecuteQuery(selectQ).GetResult;
            if (dtResult.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetEntityFields(int ClientID, Int64 EntityID)
        {
            dtResult = new DataTable();
            Query selectQ = new QueryBuilder(QueryType._Select).AddTable("s_entityfields").AddField("*", "s_entityfields").
              // AddWhere(0, "s_entityfields", "ClientID", FieldType._Number, Operator._Equal, ClientID.ToString()).
               AddWhere(0, "s_entityfields", "EntityID", FieldType._Number, Operator._Equal, EntityID.ToString());
            dtResult = rd.ExecuteQuery(selectQ).GetResult;
            return dtResult;
        }


        public DataTable GetEntityField(int ClientID, Int64 EntityID, int pFieldID)
        {
            dtResult = new DataTable();
            Query selectQ = new QueryBuilder(QueryType._Select).AddTable("s_entityfields").AddField("*", "s_entityfields").
          //     AddWhere(0, "s_entityfields", "ClientID", FieldType._Number, Operator._Equal, ClientID.ToString()).
               AddWhere(0, "s_entityfields", "EntityID", FieldType._Number, Operator._Equal, EntityID.ToString()).
               AddWhere(0, "s_entityfields", "FieldID", FieldType._Number, Operator._Equal, pFieldID.ToString());
            dtResult = rd.ExecuteQuery(selectQ).GetResult;
            return dtResult;
        }

        public int SaveField(string name, TechSharpy.Entitifier.Core. EntityFieldType fieldType, bool isKey, bool isRequired, bool isUnique,
            int lookUpID, bool isCore, int entityKey, string value, bool isReadOnly, string defaultValue, int displayOrder,
            List<string> lookUpArray, string min, string max, int maxLength, string displayName, bool autoIncrement, long incrementfrom,
            long incrementby, string description, bool enableencription, bool enablelimit)
        {

            int NextID = rd.getNextID("EntityField");
            Query iQuery = new QueryBuilder(QueryType._Insert
                ).AddTable("s_entityfields")
                .AddField("EntityID", "s_entityfields", FieldType._Number, "", entityKey.ToString())
                .AddField("FieldID", "s_entityfields", FieldType._Number, "", NextID.ToString())
               // .AddField("ClientID", "s_entityfields", FieldType._Number, "", pClientID.ToString())
               .AddField("DisplayName", "s_entityfields", FieldType._String, "", displayName.ToString())
                .AddField("FieldName", "s_entityfields", FieldType._String, "", name.ToString())
                .AddField("FieldDescription", "s_entityfields", FieldType._String, "", description.ToString())
                .AddField("FieldType", "s_entityfields", FieldType._Number, "", Convert.ToInt32(fieldType).ToString())
                .AddField("LookUpId", "s_entityfields", FieldType._Number, "", lookUpID.ToString())
                 .AddField("isRequired", "s_entityfields", FieldType._Question, "", isRequired.ToString())
                .AddField("isUnique", "s_entityfields", FieldType._Question, "", isUnique.ToString())
                .AddField("isKeyField", "s_entityfields", FieldType._Question, "", isKey.ToString())
                .AddField("autoIncrement", "s_entityfields", FieldType._Question, "", autoIncrement.ToString())
                .AddField("Contentlimit", "s_entityfields", FieldType._Number, "", maxLength.ToString())
                 .AddField("incrementfrom", "s_entityfields", FieldType._Number, "", incrementfrom.ToString())
                 .AddField("incrementby", "s_entityfields", FieldType._Number, "", incrementby.ToString())
                .AddField("value", "s_entityfields", FieldType._String, "", value.ToString())
                .AddField("defaultValue", "s_entityfields", FieldType._String, "", defaultValue.ToString())
                .AddField("displayOrder", "s_entityfields", FieldType._String, "", displayOrder.ToString())
                .AddField("Minimum", "s_entityfields", FieldType._String, "", min.ToString())
                .AddField("Maximum", "s_entityfields", FieldType._String, "", max.ToString())
                //.AddField("FileExtension", "s_entityfields", FieldType._String, "", pFileExtension.ToString())
                 .AddField("isCoreField", "s_entityfields", FieldType._Question, "", isCore.ToString())
                .AddField("isReadOnly", "s_entityfields", FieldType._Question, "", isReadOnly.ToString())
                .AddField("EnableEncription", "s_entityfields", FieldType._Question, "", enableencription.ToString())
                 .AddField("EnableContentlimit", "s_entityfields", FieldType._Question, "", enablelimit.ToString())
                 //.AddField("AcceptNull", "s_entityfields", FieldType._Question, "", pAcceptNull.ToString())
                  .AddField("LastUPD", "s_entityfields", FieldType._DateTime, "", DateTime.Now.ToString());

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return NextID;
            }
            else
            {
                return -1;
            }
        }

        public bool SaveField(string name,int FieldID, TechSharpy.Entitifier.Core.EntityFieldType fieldType, bool isKey, bool isRequired, bool isUnique,
            int lookUpID, bool isCore, int entityKey, string value, bool isReadOnly, string defaultValue, int displayOrder,
            List<string> lookUpArray, string min, string max, int maxLength, string displayName, bool autoIncrement, long incrementfrom,
            long incrementby, string description, bool enableencription, bool enablelimit)
        {

            Query iQuery = new QueryBuilder(QueryType._Update
                ).AddTable("s_entityfields")
                    .AddField("EntityID", "s_entityfields", FieldType._Number, "", entityKey.ToString())
            //    .AddField("FieldID", "s_entityfields", FieldType._Number, "", FieldID.ToString())
               // .AddField("ClientID", "s_entityfields", FieldType._Number, "", pClientID.ToString())
               .AddField("DisplayName", "s_entityfields", FieldType._String, "", displayName.ToString())
                .AddField("FieldName", "s_entityfields", FieldType._String, "", name.ToString())
                .AddField("FieldDescription", "s_entityfields", FieldType._String, "", description.ToString())
                .AddField("FieldType", "s_entityfields", FieldType._Number, "", Convert.ToInt32(fieldType).ToString())
                .AddField("LookUpId", "s_entityfields", FieldType._Number, "", lookUpID.ToString())
                 .AddField("isRequired", "s_entityfields", FieldType._Question, "", isRequired.ToString())
                .AddField("isUnique", "s_entityfields", FieldType._Question, "", isUnique.ToString())
                .AddField("isKeyField", "s_entityfields", FieldType._Question, "", isKey.ToString())
                .AddField("autoIncrement", "s_entityfields", FieldType._Question, "", autoIncrement.ToString())
                .AddField("Contentlimit", "s_entityfields", FieldType._Number, "", maxLength.ToString())
                 .AddField("incrementfrom", "s_entityfields", FieldType._Number, "", incrementfrom.ToString())
                 .AddField("incrementby", "s_entityfields", FieldType._Number, "", incrementby.ToString())
                .AddField("value", "s_entityfields", FieldType._String, "", value.ToString())
                .AddField("defaultValue", "s_entityfields", FieldType._String, "", defaultValue.ToString())
                .AddField("displayOrder", "s_entityfields", FieldType._String, "", displayOrder.ToString())
                .AddField("Minimum", "s_entityfields", FieldType._Number, "", min.ToString())
                .AddField("Maximum", "s_entityfields", FieldType._Number, "", max.ToString())
                 //.AddField("FileExtension", "s_entityfields", FieldType._String, "", pFileExtension.ToString())
                 .AddField("isCoreField", "s_entityfields", FieldType._Question, "", isCore.ToString())
                .AddField("isReadOnly", "s_entityfields", FieldType._Question, "", isReadOnly.ToString())
                .AddField("EnableEncription", "s_entityfields", FieldType._Question, "", enableencription.ToString())
                 .AddField("EnableContentlimit", "s_entityfields", FieldType._Question, "", enablelimit.ToString())
                  //.AddField("AcceptNull", "s_entityfields", FieldType._Question, "", pAcceptNull.ToString())
                  .AddField("LastUPD", "s_entityfields", FieldType._DateTime, "", DateTime.Now.ToString())
                  .AddWhere (0, "s_entityfields", "FieldID", FieldType._Number,Operator._Equal,FieldID.ToString(),Condition._And )
                  .AddWhere(0, "s_entityfields", "EntityID", FieldType._Number, Operator._Equal, entityKey.ToString(), Condition._None);

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExecuteNonQuery(TQueryBuilder tq)
        {
            try
            {
                if (rd.ExecuteTQuery(tq).Result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Create Entity", ex.InnerException);
            }

        }
    }
}
