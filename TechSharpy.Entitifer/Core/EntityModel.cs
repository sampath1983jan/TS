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
        public EntityModel(int modelID,string sourcekey, string modelName, List<EntityNode> entitynodes)
        {
            ModelID = modelID;
            ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
            Entitynodes = entitynodes ?? throw new ArgumentNullException(nameof(entitynodes));
            dataEntityModel = new Data.EntityModel();
            Sourcekey = sourcekey;
        }
        public EntityModel(int modelID, string sourcekey,string modelName)
        {
            ModelID = modelID;
            ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
            dataEntityModel = new Data.EntityModel();
        }
        public EntityModel(int modelID,string sourcekey)
        {
            ModelID = modelID;
            dataEntityModel = new Data.EntityModel();
            Entitynodes = new List<EntityNode>();
            EntityNodeFields = new List<EntityField>();
            Sourcekey = sourcekey;
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

   

    public class EntityNode {
        public int Entitykey;
        public int LeftIndex;
        public int RightIndex;
        public int NodeKey;
        public int ModeID;
        public List<NodeJoint> Nodejoints;
        private Data.EntityModel dataEntityModel;
        public EntityNode(int modelID,int entitykey, int leftIndex, int rightIndex, int nodeKey)
        {
            Entitykey = entitykey;
            LeftIndex = leftIndex;
            RightIndex = rightIndex;
            NodeKey = nodeKey;
            ModeID = modelID;
            Nodejoints = new List<NodeJoint>();
        }
        public void addJoint(string leftJoin, string rightJoin) {
            Nodejoints.Add(new NodeJoint(leftJoin, rightJoin));
        }
        public bool Save() {
            return dataEntityModel.SaveNode(this.ModeID, this.Entitykey, this.LeftIndex, this.RightIndex, this.NodeKey);
        }
        public bool Remove()
        {
            return dataEntityModel.RemoveNode(this.ModeID,this.Entitykey,this.NodeKey);
        }
    }

    public class NodeJoint {
        public string leftJoin;
        public string RightJoin;

        public NodeJoint(string leftJoin, string rightJoin)
        {
            this.leftJoin = leftJoin ?? throw new ArgumentNullException(nameof(leftJoin));
            RightJoin = rightJoin ?? throw new ArgumentNullException(nameof(rightJoin));
        }

    }

}
