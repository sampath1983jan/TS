using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Entitifier.Data
{
    class EntityModel

    {
        DataTable dtResult;
        TechSharpy.Data.DataBase rd;
        Query iQuery;
        public EntityModel()
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

        public int Save(string ModelName,string Sourcekey) {           
            try
            {
                int NextID = rd.getNextID("EntityModel");
                iQuery = new QueryBuilder(QueryType._Insert)
                    .AddField("ModelID", "EntityModel", FieldType._Number, "", NextID.ToString())
                    .AddField("ModelName", "EntityModel", FieldType._String, "", ModelName)
                .AddField("Sourcekey", "EntityModel", FieldType._String, "", Sourcekey)
                .AddField("LastUPD", "EntityModel", FieldType._DateTime, "", DateTime.Now.ToString());
                if (rd.ExecuteQuery(iQuery).Result)
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
            iQuery = new QueryBuilder(QueryType._Update).AddTable("EntityModelNode").
                AddField("modelName", "EntityModelNode", FieldType._String, "", modelName)
                            .AddWhere(0, "EntityModelNode", "ModelID", 
                            FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);

            
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public int SaveNode(int modelID,int Entitykey, int left, int right, int Nodekey,string nodeJoints) {
            try
            {
                int NextID = rd.getNextID("EntityModelNode");
                iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("ModelID", "EntityModelNode", FieldType._Number, "", modelID.ToString())
                .AddField("NodeID", "EntityModelNode", FieldType._Number, "", NextID.ToString())
                .AddField("Entitykey", "EntityModelNode", FieldType._Number, "", Entitykey.ToString())
                .AddField("left", "EntityModelNode", FieldType._Number, "", left.ToString())
                .AddField("right", "EntityModelNode", FieldType._Number, "", right.ToString())
                .AddField("Nodekey", "EntityModelNode", FieldType._Number, "", Nodekey.ToString())
                .AddField("NodeJoints", "EntityModelNode", FieldType._String, "", nodeJoints.ToString());
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    return NextID;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveNode(int modelID, int nodeID, int Entitykey, int left, int right, int Nodekey, string nodeJoints)
        {
            try
            {
                //   int NextID = rd.getNextID("EntityModel");
                iQuery = new QueryBuilder(QueryType._Insert)
                //   .AddField("ModelID", "EntityModelNode", FieldType._Number, "", modelID.ToString())
                .AddField("Entitykey", "EntityModelNode", FieldType._Number, "", Entitykey.ToString())
                .AddField("left", "EntityModelNode", FieldType._Number, "", left.ToString())
                .AddField("right", "EntityModelNode", FieldType._Number, "", right.ToString())
                .AddField("Nodekey", "EntityModelNode", FieldType._Number, "", Nodekey.ToString())
                .AddField("NodeJoints", "EntityModelNode", FieldType._String, "", nodeJoints.ToString())
                .AddWhere(0, "EntityModelNode", "ModelID", FieldType._Number, modelID.ToString(), Condition._And)
                .AddWhere(0, "EntityModelNode", "nodeID", FieldType._Number, nodeID.ToString());
                if (rd.ExecuteQuery(iQuery).Result)
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

            iQuery = new QueryBuilder(QueryType._Delete).AddTable("EntityModelNode")
                .AddWhere(0, "EntityModelNode", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(),Condition._And).
               AddWhere(0, "EntityModelNode", "Entitykey", FieldType._Number, Operator._Equal, Entitykey.ToString(), Condition._And)
               .AddWhere(0, "EntityModelNode", "Nodekey", FieldType._Number, Operator._Equal, Nodekey.ToString());
            
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool RemoveModel(int modelID) {
            iQuery = new QueryBuilder(QueryType._Delete).AddTable("EntityModelNode")
                            .AddWhere(0, "EntityModelNode", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);                                               
            if (rd.ExecuteQuery(iQuery).Result)
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
