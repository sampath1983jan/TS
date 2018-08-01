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
                .AddField("NodeID", "s_EntityModelNode")
                .AddField("depth", "s_EntityModelNode")
                .AddField("nodekey", "s_EntityModelNode")
                .AddField("entitykey", "s_EntityModelNode")
                .AddWhere(0, "s_EntityModelNode", "Entitykey", FieldType._Number, entitykey.ToString(), Condition._And)
                .AddWhere(0, "s_EntityModelNode", "modelID", FieldType._Number, modelID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }
        public DataTable GetNodeCountByEntityKey(int entitykey, int modelID,int exKey)
        {
            iQuery = new QueryBuilder(QueryType._Select)
              .AddField("left", "s_EntityModelNode")
                .AddField("right", "s_EntityModelNode")
                .AddField("NodeID", "s_EntityModelNode")
                .AddField("depth", "s_EntityModelNode")
                .AddField("nodekey", "s_EntityModelNode")
                .AddField("entitykey", "s_EntityModelNode")
                .AddWhere(0, "s_EntityModelNode", "Entitykey", FieldType._Number,Operator._NotEqual, exKey.ToString(), Condition._And)
                .AddWhere(0, "s_EntityModelNode", "nodekey", FieldType._Number, entitykey.ToString(), Condition._And)
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

     
        public int SaveNode(int modelID,int Entitykey, int Nodekey,string nodeJoints,int depth) {
      
            int NextID = 0;
           
            try
            {
                  NextID = rd.getNextID("EntityModelNode");
                iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("ModelID", "s_EntityModelNode", FieldType._Number, "", modelID.ToString())
                .AddField("NodeID", "s_EntityModelNode", FieldType._Number, "", NextID.ToString())
                .AddField("Entitykey", "s_EntityModelNode", FieldType._Number, "", Entitykey.ToString()) 
                .AddField("left", "s_EntityModelNode", FieldType._Number, "", 0.ToString())
                .AddField("right", "s_EntityModelNode", FieldType._Number, "", 1.ToString())
                .AddField("Nodekey", "s_EntityModelNode", FieldType._Number, "", Nodekey.ToString())  // parent key
                .AddField("NodeJoints", "s_EntityModelNode", FieldType._String, "", nodeJoints.ToString())
                .AddField("depth", "s_EntityModelNode", FieldType._Number, "", depth.ToString());
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
        public DataTable GetDepth(int entityKey) {
            iQuery = new QueryBuilder(QueryType._Select)              
              .AddField("depth", "s_EntityModelNode")
              .AddWhere(0, "s_EntityModelNode", "Entitykey", FieldType._Number, entityKey.ToString());             
            return rd.ExecuteQuery(iQuery).GetResult;
        }
        public bool SaveNode(int modelID, int nodeID, int Entitykey, int Nodekey, string nodeJoints,int depth)
        {
            try
            {             
                iQuery = new QueryBuilder(QueryType._Update)             
                .AddField("Entitykey", "s_EntityModelNode", FieldType._Number, "", Entitykey.ToString())                
                .AddField("Nodekey", "s_EntityModelNode", FieldType._Number, "", Nodekey.ToString())
                .AddField("NodeJoints", "s_EntityModelNode", FieldType._String, "", nodeJoints.ToString())
                .AddField("depth", "s_EntityModelNode", FieldType._Number, "", depth.ToString())
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
        public int UpdatePosition(int nodeID, int modelID, int Entitykey, int Nodekey, int depth)        {
            int _Count = GetNodeCountByEntityKey(Nodekey, modelID,Entitykey).Rows.Count;
            DataTable dtParent = new DataTable();
            dtParent = GetNodeByEntityKey(Nodekey, modelID);
            int _left;
            int _right;
            int _newleft;
            int _newright;

            if (dtParent.Rows.Count > 0)
            {
                _left = dtParent.Rows[0]["left"] == DBNull.Value ? 0 : (int)dtParent.Rows[0]["left"];
                _right = dtParent.Rows[0]["right"] == DBNull.Value ? 0 : (int)dtParent.Rows[0]["right"];
            }
            else return 0;

            _newleft = (_Count * 2) + (_left + 1);
            _newright = (_Count * 2) + (_left + 2);
            iQuery = new QueryBuilder(QueryType._Update)
                .AddField("left", "s_EntityModelNode", FieldType._Number, "", _newleft.ToString())
                .AddField("right", "s_EntityModelNode", FieldType._Number, "", _newright.ToString())
                .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, nodeID.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                iQuery = new QueryBuilder(QueryType._Update)
              .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left`+ 2)".ToString())
              //  .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` +2)".ToString())
              .AddWhere(0, "s_EntityModelNode", "left", FieldType._Number, Operator._Greater, _newleft.ToString(), Condition._And)
              //  .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greaterthan, _right.ToString(), Condition._And)
              .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, nodeID.ToString(), Condition._None);
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    // return true;
                }
                iQuery = new QueryBuilder(QueryType._Update)
             // .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left`+ 2)".ToString())
             .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` +2)".ToString())
             // .AddWhere(0, "s_EntityModelNode", "left", FieldType._Number, Operator._Greater, _left.ToString(), Condition._And)
             .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greaterthan, _right.ToString(), Condition._And)
             .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, nodeID.ToString(), Condition._None);
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    // return true;
                }
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void UpdateRemovedPosition(int nodeID, int entitykey, int nodekey, int ModelID) {
            int _Count = GetNodeCountByEntityKey(entitykey, ModelID,-1).Rows.Count;
            _Count = _Count + 1;
            DataTable dtChild, dtParent = new DataTable();
            dtChild = GetNodeByEntityKey(entitykey, ModelID);
            dtParent = GetNodeByEntityKey(nodekey, ModelID);
            int _left = 0;
            int _right = 0;
            int _newleft = 0;
            int _newright = 0;
            int parentNode = 0;
            if (dtChild.Rows.Count > 0){
                _left = dtChild.Rows[0]["left"] == DBNull.Value ? 0 : (int)dtChild.Rows[0]["left"];
                _right = dtChild.Rows[0]["right"] == DBNull.Value ? 0 : (int)dtChild.Rows[0]["right"];
            }
            if (dtParent.Rows.Count > 0){
                parentNode = (int)dtParent.Rows[0]["NodeID"];
                _newleft = dtParent.Rows[0]["left"] == DBNull.Value ? 0 : (int)dtParent.Rows[0]["left"];
            }
            _newright = _left;// (_right) - (_Count * 2); 
            if (parentNode > 0)
            {
                iQuery = new QueryBuilder(QueryType._Update)
               // .AddField("left", "s_EntityModelNode", FieldType._Number, "", _newleft.ToString())
               .AddField("right", "s_EntityModelNode", FieldType._Number, "", _newright.ToString())
               .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, parentNode.ToString());
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    iQuery = new QueryBuilder(QueryType._Update)
                //    .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left` - "+ (_Count *2) +")".ToString())
                .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` - " + (_Count * 2) + ")".ToString())
             .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greater, _newright.ToString(), Condition._And)
             .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, parentNode.ToString(), Condition._None);
                    if (rd.ExecuteQuery(iQuery).Result)
                    {
                        // return true;
                    }
                    //_newleft = (_right) - (_Count * 2);
                    iQuery = new QueryBuilder(QueryType._Update)
              //    .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left` - "+ (_Count *2) +")".ToString())
              .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left` - " + (_Count * 2) + ")".ToString())
           .AddWhere(0, "s_EntityModelNode", "left", FieldType._Number, Operator._Greater, _newleft.ToString(), Condition._And)
           .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, parentNode.ToString(), Condition._None);
                    if (rd.ExecuteQuery(iQuery).Result)
                    {
                        // return true;
                    }

                }
            }

        }

        public void UpdatePositionChangeNode(int nodeID, int entitykey, int nodekey, int ModelID) {
            int _Count = GetNodeCountByEntityKey(entitykey, ModelID,-1).Rows.Count;
            _Count = _Count + 1;
            DataTable dtChild, dtParent = new DataTable();
            dtChild = GetNodeByEntityKey(entitykey, ModelID);       
         //   int _NewParentChildCount = GetNodeCountByEntityKey(nodekey, ModelID).Rows.Count;
            int _left = 0;
            int _right = 0;
            //int _newleft = 0;
            //int _newright = 0;
            //int parentNode = 0;
            if (dtChild.Rows.Count > 0)
            {
                _left = dtChild.Rows[0]["left"] == DBNull.Value ? 0 : (int)dtChild.Rows[0]["left"];
                _right = dtChild.Rows[0]["right"] == DBNull.Value ? 0 : (int)dtChild.Rows[0]["right"];
            }                        
           //if(parentNode > 0){
                iQuery = new QueryBuilder(QueryType._Update)
             .AddField("left", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`left`- " + (_Count * 2) + ")".ToString())
             //  .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` +2)".ToString())
             .AddWhere(0, "s_EntityModelNode", "left", FieldType._Number, Operator._Greater, _right.ToString(), Condition._And)
           //  .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Less, _right.ToString(), Condition._And)
             //  .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greaterthan, _right.ToString(), Condition._And)
             .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, nodeID.ToString(), Condition._None);
                 rd.ExecuteQuery(iQuery);

                iQuery = new QueryBuilder(QueryType._Update)
           .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right`- " + (_Count * 2) + ")".ToString())
           //  .AddField("right", "s_EntityModelNode", FieldType._Number, "", "(`s_EntityModelNode`.`right` +2)".ToString())
           .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greater, _right.ToString(), Condition._And)
           //  .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Less, _right.ToString(), Condition._And)
           //  .AddWhere(0, "s_EntityModelNode", "right", FieldType._Number, Operator._Greaterthan, _right.ToString(), Condition._And)
           .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._NotEqual, nodeID.ToString(), Condition._None);
                rd.ExecuteQuery(iQuery);
            //}
            dtParent = GetNodeByEntityKey(nodekey, ModelID);
            int prentDept = 0;
            if (dtParent.Rows.Count > 0) {
                prentDept = dtParent.Rows[0]["depth"] == DBNull.Value ? 0 : (int)dtParent.Rows[0]["depth"];
            }
            prentDept = prentDept + 1;
            UpdatePosition(nodeID, ModelID, entitykey, nodekey, prentDept);
         //   dtChild = GetNodeByEntityKey(entitykey, ModelID);
            dtChild = GetNodeCountByEntityKey(entitykey, ModelID,-1);
            foreach (DataRow dr in dtChild.Rows) {
                prentDept = prentDept + 1;
                int _chldNodeID = dr["NodeID"] == DBNull.Value ? -1 : (int)dr["NodeID"];
                int _chldnodekey = dr["nodekey"] == DBNull.Value ? -1 : (int)dr["nodekey"];
                int _chldentitykey = dr["entitykey"] == DBNull.Value ? -1 : (int)dr["entitykey"];
                UpdatePosition(_chldNodeID, ModelID, _chldentitykey, _chldnodekey, prentDept);
            }
             
        }

        public bool RemoveNode(int nodeID,int entitykey,int nodekey,int ModelID) {
            
            iQuery = new QueryBuilder(QueryType._Delete).AddTable("s_EntityModelNode")
            .AddWhere(0, "s_EntityModelNode", "NodeID", FieldType._Number, Operator._Equal, nodeID.ToString());
            if ((rd.ExecuteQuery(iQuery).Result))
            {
                iQuery = new QueryBuilder(QueryType._Delete).AddTable("s_EntityModelNode")
               .AddWhere(0, "s_EntityModelNode", "nodekey", FieldType._Number, Operator._Equal, entitykey.ToString())
                 .AddWhere(0, "s_EntityModelNode", "ModelID", FieldType._Number, Operator._Equal, ModelID.ToString());
                if ((rd.ExecuteQuery(iQuery).Result))
                {
                    UpdateRemovedPosition(nodeID, entitykey, nodekey, ModelID);
                    return true;
                }
                else return false;
            }
            else
            {
                return false;
            }
        }        
        public bool RemoveModel(int modelID) {
            iQuery = new QueryBuilder(QueryType._Delete).AddTable("s_EntityModel")
                            .AddWhere(0, "s_EntityModel", "ModelID", FieldType._Number, Operator._Equal, modelID.ToString(), Condition._None);
            return (rd.ExecuteQuery(iQuery).Result);             
        }
    }
}

