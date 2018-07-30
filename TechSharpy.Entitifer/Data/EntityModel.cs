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
        public DataTable GetNodeByEntityKey(int entitykey,int modelID) {
            iQuery = new QueryBuilder(QueryType._Select)
                .AddField("left", "s_EntityModelNode")
                .AddField("right", "s_EntityModelNode")
                .AddWhere(0, "s_EntityModelNode", "Nodekey", FieldType._Number, entitykey.ToString(), Condition._And)
                .AddWhere(0, "s_EntityModelNode", "modelID", FieldType._Number, modelID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }
        public int Save(string ModelName,string Sourcekey) {           
            try
            {
                int NextID = rd.getNextID("EntityModel");
                iQuery = new QueryBuilder(QueryType._Insert)
                    .AddField("ModelID", "s_EntityModel", FieldType._Number, "", NextID.ToString())
                    .AddField("ModelName", "s_EntityModel", FieldType._String, "", ModelName)
             //   .AddField("Sourcekey", "s_EntityModel", FieldType._String, "", Sourcekey)
                .AddField("LastUPD", "s_EntityModel", FieldType._DateTime, "", DateTime.Now.ToString());
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
            iQuery = new QueryBuilder(QueryType._Update).AddTable("s_EntityModel").
                AddField("modelName", "s_EntityModel", FieldType._String, "", modelName)
                            .AddWhere(0, "s_EntityModel", "ModelID", 
                            FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);


            return (rd.ExecuteQuery(iQuery).Result);
            
        }
        public int SaveNode(int modelID,int Entitykey, int left, int right, int Nodekey,string nodeJoints) {
            DataTable dtParent = new DataTable();
            int NextID = 0;
            try
            {
                  NextID = rd.getNextID("EntityModelNode");
                iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("ModelID", "s_EntityModelNode", FieldType._Number, "", modelID.ToString())
                .AddField("NodeID", "s_EntityModelNode", FieldType._Number, "", NextID.ToString())
                .AddField("Entitykey", "s_EntityModelNode", FieldType._Number, "", Entitykey.ToString())
                .AddField("left", "s_EntityModelNode", FieldType._Number, "", left.ToString())
                .AddField("right", "s_EntityModelNode", FieldType._Number, "", right.ToString())
                .AddField("Nodekey", "s_EntityModelNode", FieldType._Number, "", Nodekey.ToString())
                .AddField("NodeJoints", "s_EntityModelNode", FieldType._String, "", nodeJoints.ToString());
                if (rd.ExecuteQuery(iQuery).Result)
                {
                   // return NextID;
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
            dtParent = GetNodeByEntityKey(Entitykey, modelID);
            int _left;
            int _right;
            int _newleft;
            int _newright;
            if (dtParent.Rows.Count > 0)
            {
                _left = dtParent.Rows[0]["left"] == DBNull.Value ? 0 : (int)dtParent.Rows[0]["left"];
                _right = dtParent.Rows[0]["right"] == DBNull.Value ? 0 : (int)dtParent.Rows[0]["right"];
            }
            else return NextID;

            _newleft = (_left + 1);
            _newright = _left + 2;
            iQuery = new QueryBuilder(QueryType._Update)
                .AddField("left", "s_EntityModelNode", FieldType._Number, "", _newleft.ToString())
                .AddField("right", "s_EntityModelNode", FieldType._Number, "", _newright.ToString())
                .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, NextID.ToString());
            if (rd.ExecuteQuery(iQuery).Result) {   
                iQuery = new QueryBuilder(QueryType._Update)
              .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left`+ 2)".ToString())
            //  .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` +2)".ToString())
              .AddWhere(0, "s_EntityModelNode", "left", FieldType._Number, Operator._Greater, _left.ToString(), Condition._And)
            //  .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greaterthan, _right.ToString(), Condition._And)
              .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number,Operator._NotEqual, NextID.ToString(),Condition._None);
                if (rd.ExecuteQuery(iQuery).Result) {
                   // return true;
                }
                iQuery = new QueryBuilder(QueryType._Update)
            // .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left`+ 2)".ToString())
             .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` +2)".ToString())
            // .AddWhere(0, "s_EntityModelNode", "left", FieldType._Number, Operator._Greater, _left.ToString(), Condition._And)
             .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greaterthan, _right.ToString(), Condition._And)
             .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, NextID.ToString(), Condition._None);
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    // return true;
                }

            }

            return NextID;


        }
        public bool SaveNode(int modelID, int nodeID, int Entitykey, int left, int right, int Nodekey, string nodeJoints)
        {


            try
            {
                //   int NextID = rd.getNextID("s_EntityModel");
                iQuery = new QueryBuilder(QueryType._Update)
                //    .AddField("ModelID", "s_EntityModelNode", FieldType._Number, "", modelID.ToString())
                .AddField("Entitykey", "s_EntityModelNode", FieldType._Number, "", Entitykey.ToString())
                .AddField("left", "s_EntityModelNode", FieldType._Number, "", left.ToString())
                .AddField("right", "s_EntityModelNode", FieldType._Number, "", right.ToString())
                .AddField("Nodekey", "s_EntityModelNode", FieldType._Number, "", Nodekey.ToString())
                .AddField("NodeJoints", "s_EntityModelNode", FieldType._String, "", nodeJoints.ToString())
                .AddWhere(0, "s_EntityModelNode", "ModelID", FieldType._Number, modelID.ToString(), Condition._And)
                .AddWhere(0, "s_EntityModelNode", "nodeID", FieldType._Number, nodeID.ToString());
                return (rd.ExecuteQuery(iQuery).Result);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetModel(int ModelID) {
            iQuery = new QueryBuilder(QueryType._Select)
                .AddField("ModelID", "vw_entityModel")
                .AddField("ModelName", "vw_entityModel")
                .AddField("NodeID", "vw_entityModel")
                .AddField("EntityKey", "vw_entityModel")
                .AddField("Nodekey", "vw_entityModel")
                .AddField("left", "vw_entityModel")
                .AddField("right", "vw_entityModel")
                .AddField("NodeJoints", "vw_entityModel")
                .AddWhere(0, "vw_entityModel", "ModelID", FieldType._Number, ModelID.ToString());
             dtResult= rd.ExecuteQuery(iQuery).GetResult;
            return dtResult;
        }
        public bool RemoveNode(int nodeID) {
            iQuery = new QueryBuilder(QueryType._Delete).AddTable("s_EntityModelNode")
                .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._Equal, nodeID.ToString());
            return (rd.ExecuteQuery(iQuery).Result);            
        }        
        public bool RemoveModel(int modelID) {
            iQuery = new QueryBuilder(QueryType._Delete).AddTable("s_EntityModel")
                            .AddWhere(0, "s_EntityModel", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);
            return (rd.ExecuteQuery(iQuery).Result);             
        }
    }
}

