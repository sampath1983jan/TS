using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TechSharpy.Entitifier;
using System.Linq;

namespace TechSharpy.Entitifier.Core
{
    public class EntityModel
    {
        public int ModelID;
        public string Sourcekey;
        public string ModelName;
        public List<EntityNode> Entitynodes;        
        public List<EntityField> EntityNodeFields;
        private Data.EntityModel dataEntityModel;
        public EntityModel() {

        }
        public EntityModel(string modelName, List<EntityNode> entitynodes)
        {
            ModelID = -1;
            ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
            Entitynodes = entitynodes ?? throw new ArgumentNullException(nameof(entitynodes));
            dataEntityModel = new Data.EntityModel();
            Sourcekey = "";
        }
        public EntityModel(int modelID, string sourcekey,string modelName)
        {
            ModelID = modelID;
            ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
            dataEntityModel = new Data.EntityModel();
        }
        public EntityModel(int modelID)
        {
            ModelID = modelID;
            dataEntityModel = new Data.EntityModel();
            Entitynodes = new List<EntityNode>();
            EntityNodeFields = new List<EntityField>();
            Sourcekey = "";
        }
        private bool Validation() {
            // Existing Node validation
            // Parent Node validation
            return true;
        }
        public bool ChangeName() {
            return dataEntityModel.Save(this.ModelID, this.ModelName);
        }
        public bool RemoveNode(int entitykey,int nodekey) {
            EntityNode en ;
           en= Entitynodes.Where(a => a.Entitykey == entitykey && a.NodeKey == nodekey && a.ModeID == this.ModelID).FirstOrDefault();
            if (en != null)
            {
                en.Remove();
                return true;
            }
            else {
                return false;
            }
        }
        public bool Save() {
            
                int key = dataEntityModel.Save(this.ModelName, Sourcekey);
                if (key > 0)
                {
                    foreach (EntityNode en in Entitynodes)
                    {
                        en.Save();
                    }
                    return true;
                }
                else
                {
                    return false;
                }         
          }
        
        public void Init() {

        }
    }


    public interface IEntityNode {
        int Entitykey { get; set; }
        int LeftIndex { get; set; }
        int RightIndex { get; set; }
        int NodeKey { get; set; }
        int ModeID { get; set; }
        int NodeID { get; set; }
       
    }

    public class EntityNode:IEntityNode {

        List<NodeJoint> Nodejoints { get; set; }
        private Data.EntityModel dataEntityModel;
        private int _entityKey;
        private int _leftIndex;
        private int _rightindex;
        private int _nodeKey;
        private int _modelID;
        private int _nodeID;
      //  private List<NodeJoint> _nodeJoints;
        public int Entitykey { get => _entityKey; set => _entityKey=value; }
        public int LeftIndex { get => _leftIndex; set => _leftIndex = value; }
        public int RightIndex { get => _rightindex; set => _rightindex = value; }
        public int NodeKey { get => _nodeKey; set => _nodeKey = value; }
        public int ModeID { get => _modelID; set => _modelID = value; }
        public int NodeID { get => _nodeID; set => _nodeID = value; }
        //public List<NodeJoint> Nodejoints { get => _nodeJoints; set => _nodeJoints = value; }

        public EntityNode(int entitykey, int leftIndex, int rightIndex, int nodeKey)
        {
            Entitykey = entitykey;
            LeftIndex = leftIndex;
            RightIndex = rightIndex;
            NodeKey = nodeKey;
            ModeID = -1;
            NodeID = -1;
            Nodejoints = new List<NodeJoint>();
            dataEntityModel = new Data.EntityModel();
        }
        public EntityNode(int nodeID, int modeID) {
            NodeID = nodeID;
            ModeID = modeID;
            Nodejoints = new List<NodeJoint>();
            dataEntityModel = new Data.EntityModel();
        }
        public void addJoint(string leftJoin, string rightJoin) {
            Nodejoints.Add(new NodeJoint(leftJoin, rightJoin));
        }
        public bool Save() {
            string nj= Newtonsoft.Json.JsonConvert.SerializeObject(this.Nodejoints);
            if (this.NodeID > 0)
            {
                return dataEntityModel.SaveNode(this.ModeID, this.NodeID, this.Entitykey, this.LeftIndex, this.RightIndex, this.NodeKey, nj);
            }
            else {
                this.NodeID= dataEntityModel.SaveNode(this.ModeID, this.Entitykey, this.LeftIndex, this.RightIndex, this.NodeKey, nj);
                if (this.NodeID > 0)
                {
                    return true;
                }
                else return false;
            }            
        }
        public bool Remove()
        {
            return dataEntityModel.RemoveNode(this.ModeID,this.Entitykey,this.NodeKey);
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
