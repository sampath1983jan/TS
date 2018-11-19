using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component.Model
{

    public interface IComponentRelation {

        int Entitykey { get; set; }
        int LeftIndex { get; set; }
        int RightIndex { get; set; }
        int NodeKey { get; set; }
        int ModelID { get; set; }
        int NodeID { get; set; }

        void AddRelation();
        void AddRelationNode(ComponentRelationNode elementRelationNode);        
    }

    public interface IComponentRelationFactory {
        IComponentRelation Create(int NodeID,int modelID);
        IComponentRelation Create(int modelID, int Componentkey, int relatedComponentkey);
    }

    public class ElementRelationManager {
        public static IComponentRelation Create(IComponentRelationFactory componentRelationFactory,int nodeID, int modelID) {
            return componentRelationFactory.Create(nodeID, modelID);
        }
        public static IComponentRelation Create(IComponentRelationFactory componentRelationFactory, int modelID, int Componentkey, 
            int relatedComponentkey) {
            return componentRelationFactory.Create(modelID,Componentkey,  relatedComponentkey);
        }
    }

    public class ElementRelationFactory : IComponentRelationFactory
    {
        public IComponentRelation Create(int NodeID,int modelID)
        {
            return new ComponentRelation(NodeID, modelID);    
        }

        public IComponentRelation Create(int modelID,int Componentkey, int relatedComponentkey)
        {
            return new ComponentRelation(Componentkey,relatedComponentkey);
        }
    }

    public class ComponentRelation : IComponentRelation
    {
        private int _entityKey;
        private int leftIndex;
        private int rightIndex;
        private int nodeKey;
        private int modeID;
        private int nodeID;

        private EntityNode entityNode;
        public List<ComponentRelationNode> RelationNodes;

        public int Entitykey { get => _entityKey; set => _entityKey=value; }
        public int LeftIndex { get => leftIndex; set => leftIndex=value; }
        public int RightIndex { get => rightIndex; set => rightIndex=value; }
        public int NodeKey { get => nodeKey; set => nodeKey = value; }
        public int ModelID { get => modeID; set => modeID=value; }
        public int NodeID { get => nodeID; set => nodeID=value; }

        public ComponentRelation(int nodeID,int modelID)
        {
            NodeKey = nodeID;
            ModelID = modelID;
            entityNode = new EntityNode(nodeID, modelID);
        }
        public ComponentRelation(int modeID, int Componentkey, int relatedComponentkey)             
        {
            RelationNodes = new List<ComponentRelationNode>();
        }
        public ComponentRelation(int nodeID, int modeID, int componentkey, int relatedComponentkey)            
        {
            RelationNodes = new List<ComponentRelationNode>();
            NodeKey = nodeID;
        }
        public void AddRelation()
        {
           
        }
        public bool Save() {
        return    entityNode.Save();
        }
        public void AddRelationNode(ComponentRelationNode elementRelationNode)
        {
            entityNode.addJoint(elementRelationNode.leftJoin, elementRelationNode.RightJoin);          
        }
    }

    public class ComponentRelationNode : INodeJoint
    {
        private string _leftJoin;
        private string _rightJoin;
        public string leftJoin { get => _leftJoin; set => _leftJoin = value; }
        public string RightJoin { get => _rightJoin; set => _rightJoin = value; }

        public ComponentRelationNode(string leftAttributeKey, string rightAttributeKey) {
            this._leftJoin = leftAttributeKey;
            this._rightJoin = rightAttributeKey;
        }
        
    }
}
