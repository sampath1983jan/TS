using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    class EntityModel: DataAccess

    {
        DataTable dtResult;
        Query iQuery;
        public EntityModel()
        {
            try
            {
                this.Init();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(string ModelName) {           
            try
            {
                int NextID = this.getNextID("EntityModel");
                iQuery = new Query(QueryType._Insert).AddField("ModelID", "EntityModel", FieldType._Number, "", NextID.ToString())
                .AddField("ModelName", "EntityModel", FieldType._String, "", ModelName).AddField("LastUPD", "EntityModel", FieldType._DateTime, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
                {
                    return NextID;
                }
                else {
                    return -1;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            
        }
        public bool Save(int modelID,string modelName)
        {
            iQuery = new Query(QueryType._Delete).AddTable("EntityModelMode").AddField("modelName", "EntityModelMode", FieldType._String, "", modelName)
                            .AddWhere(0, "EntityModelMode", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);

            int iResult;
            iResult = this.ExecuteQuery(iQuery);
            if (iResult >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool SaveNode(int modelID,int Entitykey, int left, int right, int Nodekey) {
            try
            {                
                iQuery = new Query(QueryType._Insert).AddField("ModelID", "EntityModelMode", FieldType._Number, "", modelID.ToString())
                .AddField("Entitykey", "EntityModelMode", FieldType._Number, "", Entitykey.ToString())
                  .AddField("left", "EntityModelMode", FieldType._Number, "", left.ToString())
                .AddField("right", "EntityModelMode", FieldType._Number, "", right.ToString())
                .AddField("Nodekey", "EntityModelMode", FieldType._Number, "", Nodekey.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
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
                throw ex;
            }
        }

        public bool RemoveNode(int modelID, int Entitykey, int Nodekey) {

            iQuery = new Query(QueryType._Delete).AddTable("EntityModelMode")
                .AddWhere(0, "EntityModelMode", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(),Condition._And).
               AddWhere(0, "EntityModelMode", "Entitykey", FieldType._Number, Operator._Equal, Entitykey.ToString(), Condition._And)
               .AddWhere(0, "EntityModelMode", "Nodekey", FieldType._Number, Operator._Equal, Nodekey.ToString());
            int iResult;
            iResult = this.ExecuteQuery(iQuery);
            if (iResult >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool RemoveModel(int modelID) {
            iQuery = new Query(QueryType._Delete).AddTable("EntityModelMode")
                            .AddWhere(0, "EntityModelMode", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);                      
                           
            int iResult;
            iResult = this.ExecuteQuery(iQuery);
            if (iResult >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
