using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TechSharpy.Entitifier;
using System.Linq;
using System.Data;

namespace TechSharpy.Entitifier.Core
{
    public interface IEntityModel {
        int ModelID { set; get; }
        string Sourcekey { set; get; }
        string ModelName { set; get; }
        List<EntityNode> Entitynodes { set; get; }
    }
    public class EntityModel:IEntityModel
    {          
        public List<EntityField> EntityNodeFields;
        private Data.EntityModel dataEntityModel;
        private int _modelID;
        private string _modelName;
        private List<EntityNode> _entityNodes;
        public int ModelID { get => _modelID; set => _modelID=value; }
        public string Sourcekey { get => _modelName; set => _modelName=value; }
        public string ModelName { get => _modelName; set => _modelName=value; }
        public List<EntityNode> Entitynodes { get => _entityNodes; set => _entityNodes=value; }
        public EntityModel() {

        }
        public EntityModel(string modelName, List<EntityNode> entitynodes)
        {
            ModelID = -1;
            _modelName = modelName;            
            _entityNodes = entitynodes;
            dataEntityModel = new Data.EntityModel();            
        }
        public EntityModel(int modelID, string sourcekey,string modelName)
        {
            ModelID = modelID;
            ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
            _entityNodes = new List<EntityNode>();
            dataEntityModel = new Data.EntityModel();
        }
        public EntityModel(int modelID)
        {
            ModelID = modelID;
          //  _modelID = -1;
            dataEntityModel = new Data.EntityModel();
            _entityNodes = new List<EntityNode>();
            EntityNodeFields = new List<EntityField>();
          //  Sourcekey = "";
        }
        private bool IsNodeExist(int nodeid,int entitykey,int nodekey) {
            EntityNode en;
            if (nodeid <= 0) {
                en = Entitynodes.Where(a => a.Entitykey == entitykey && a.NodeKey == nodekey).FirstOrDefault();
                if (en == null)
                {
                    return false;
                }
                else {
                    return true;
                }
            }
            else return false;
        }
        public bool Validation() {
            // Existing Node validation
            // Parent Node validation
            foreach (EntityNode en in Entitynodes) {
                if (IsNodeExist(en.NodeID, en.Entitykey, en.NodeKey)) {
                    return false;
                }
            }            
            return true;
        }
        public bool ChangeName() {
            return dataEntityModel.Save(this.ModelID, this.ModelName);
        }
        public bool RemoveModel() {
            if (dataEntityModel.RemoveModel(this.ModelID))
            {
                foreach (EntityNode en in this.Entitynodes)
                {
                    en.Remove();
                }
                return true;
            }
            else {
                return false;
            }
        }
        public bool RemoveNode(int nodeID)
        {
            EntityNode en;
            en = Entitynodes.Where(a => a.NodeID == nodeID && a.ModeID == this.ModelID).FirstOrDefault();
            if (en != null)
            {
                if (en.Remove())
                {
                    return true;
                }
                else return false;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveNode(int entitykey,int nodekey) {
            EntityNode en ;
           en= Entitynodes.Where(a => a.Entitykey == entitykey && a.NodeKey == nodekey && a.ModeID == this.ModelID).FirstOrDefault();
            if (en != null)
            {
                if (en.Remove())
                {
                    return true;
                }
                else return false;                
            }
            else {
                return false;
            }
        }
        public bool Save() {            
                this._modelID = dataEntityModel.Save(this.ModelName, Sourcekey);
                if (this._modelID > 0)
                {
                  return true;
                }
                else
                {
                    return false;
                }         
        }        
        public void Init() {
           DataTable dt = new DataTable();
           DataTable dtRelation = new DataTable();
            DataTable dtModel = new DataTable();
           dt = dataEntityModel.GetModel(this.ModelID);
           
           dtRelation = dt.DefaultView.ToTable(true,"NodeID,ModelID,EntityKey,Nodekey,left,right,NodeJoints".Split(','));
            dtModel = dt.DefaultView.ToTable(true, "ModelID,ModelName".Split(','));
            foreach (DataRow dr in dtModel.Rows) {
                this.ModelName = dr["ModelName"] == DBNull.Value ? "" : dr["ModelName"].ToString();               
            }
            foreach (DataRow dr in dtRelation.Rows) {
                if (dr["nodeid"] != DBNull.Value) {
                    int _nodeid = dr["nodeid"] == DBNull.Value ? 0 : (int)dr["nodeid"];
                    int _entitykey = dr["EntityKey"] == DBNull.Value ? 0 : (int)dr["EntityKey"];
                    int _nodekey = dr["Nodekey"] == DBNull.Value ? 0 : (int)dr["Nodekey"];
                    int _left = dr["left"] == DBNull.Value ? 0 : (int)dr["left"];
                    int _right = dr["right"] == DBNull.Value ? 0 : (int)dr["right"];
                    string _joints = dr["NodeJoints"] == DBNull.Value ? "" : (string)dr["NodeJoints"];
                    var n = new EntityNode(this.ModelID, _nodeid, _entitykey, _nodekey);
                    n.LeftIndex = _left;
                    n.RightIndex = _right;
                    List<NodeJoint> nodeJoint = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NodeJoint>>(_joints);
                    n.Nodejoints = nodeJoint;
                    this.Entitynodes.Add(n);
                }
                
            }
        }
        public bool ChangeNode(int nodeID,int newNodeKey, List<NodeJoint> elementRelationNode) {
           var node =  Entitynodes.Where(a => a.NodeID == nodeID).FirstOrDefault();
            var childNode = Entitynodes.Where(a => a.LeftIndex >= node.LeftIndex && a.RightIndex <= node.RightIndex);                       
            node.UpdatePositionChangeNode(newNodeKey);
            node.NodeKey = newNodeKey;
            node.Nodejoints = elementRelationNode;
            node.Save();
                return true;
           // }
          //  else return false;           
        }
    }
    
    public interface IEntityNode {
        int Entitykey { get; set; }
        int LeftIndex { get; set; }
        int RightIndex { get; set; }
        int NodeKey { get; set; }
        int ModeID { get; set; }
        int NodeID { get; set; }
        int Depth { set; get; }
       
    }

    public class EntityNode:IEntityNode {

        public List<NodeJoint> Nodejoints { get; set; }
        private Data.EntityModel dataEntityModel;
        private int _entityKey;
        private int _leftIndex;
        private int _rightindex;
        private int _nodeKey;
        private int _modelID;
        private int _nodeID;
        private int _depth;
      //  private List<NodeJoint> _nodeJoints;
        public int Entitykey { get => _entityKey; set => _entityKey=value; }
        public int LeftIndex { get => _leftIndex; set => _leftIndex = value; }
        public int RightIndex { get => _rightindex; set => _rightindex = value; }
        public int NodeKey { get => _nodeKey; set => _nodeKey = value; }
        public int ModeID { get => _modelID; set => _modelID = value; }
        public int NodeID { get => _nodeID; set => _nodeID = value; }
        public int Depth { get => _depth; set => _depth = value; }
        
        public EntityNode(int modeID,int nodeID, int entitykey, int nodeKey)
        {
            Entitykey = entitykey;
            LeftIndex = 0;
            RightIndex = 1;
            NodeKey = nodeKey;
            ModeID = modeID;
            NodeID = nodeID;
            Nodejoints = new List<NodeJoint>();
            _depth = 0;
            dataEntityModel = new Data.EntityModel();
        }
        public EntityNode(int nodeID, int modeID) {
            NodeID = nodeID;
            ModeID = modeID;
            Nodejoints = new List<NodeJoint>();
            _depth = 0;
            dataEntityModel = new Data.EntityModel();
        }
        public void addJoint(string leftJoin, string rightJoin) {
            Nodejoints.Add(new NodeJoint(leftJoin, rightJoin));
        }
        public bool Save() {
            string nj= Newtonsoft.Json.JsonConvert.SerializeObject(this.Nodejoints);            
            if (this.NodeID > 0)
            {
                return dataEntityModel.SaveNode(this.ModeID, this.NodeID, this.Entitykey, this.NodeKey, nj,this.Depth);
            }
            else {                          
                this.NodeID= dataEntityModel.SaveNode(this.ModeID, this.Entitykey, this.NodeKey, nj,this.Depth);
                if (this.NodeID > 0)
                {
                    UpdatePosition();
                    return true;
                }
                else return false;
            }            
        }
        protected internal void UpdatePosition() {
            DataTable dtDepth = new DataTable();
            dtDepth = dataEntityModel.GetDepth(this.NodeKey);
            if (dtDepth.Rows.Count > 0)
            {
                _depth = dtDepth.Rows[0]["depth"] == DBNull.Value ? 0 : (int)dtDepth.Rows[0]["depth"];
                _depth = _depth + 1;
            }
            else _depth = 0;
            dataEntityModel.UpdatePosition(this.NodeID, this.ModeID, this.Entitykey, this.NodeKey, Depth);
        }
        protected internal void UpdateRemovePosition() {
            dataEntityModel.UpdateRemovedPosition(this.NodeID, this.Entitykey, this.NodeKey, this.ModeID);
        }
        protected internal void UpdatePositionChangeNode(int newNodekey) {
            dataEntityModel.UpdatePositionChangeNode(this.NodeID, this.Entitykey, newNodekey, this.ModeID);
        }
        public bool Remove()
        {
            return dataEntityModel.RemoveNode(this.NodeID,this.Entitykey,this.NodeKey,this.ModeID);
        }
    }

    public interface INodeJoint {
          string leftJoin { set; get; }
          string RightJoin { get; set; }
    }

    public class NodeJoint: INodeJoint
    {       
        public NodeJoint(string leftJoin, string rightJoin)
        {
            this.leftJoin = leftJoin ?? throw new ArgumentNullException(nameof(leftJoin));
            RightJoin = rightJoin ?? throw new ArgumentNullException(nameof(rightJoin));
        }
        private string _leftJoin;
        private string _rightJoin;
        public string leftJoin { get => _leftJoin; set => _leftJoin =value; }
        public string RightJoin { get => _rightJoin; set => _rightJoin=value; }
    }

}
