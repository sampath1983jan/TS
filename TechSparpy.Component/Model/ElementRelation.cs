using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component.Model
{

    public interface IElementRelation {

        int Entitykey { get; set; }
        int LeftIndex { get; set; }
        int RightIndex { get; set; }
        int NodeKey { get; set; }
        int ModeID { get; set; }
        int NodeID { get; set; }

        void AddRelation();
        void AddRelationNode(ElementRelationNode elementRelationNode);        
    }

    public interface IElementRelationFactory {
        IElementRelation Create(int NodeID);
        IElementRelation Create(int Componentkey, int relatedComponentkey);
    }

    public class ElementRelationManager {
        public static IElementRelation Create(IElementRelationFactory elementRelationFactory,int nodeID) {
            return elementRelationFactory.Create(nodeID);
        }
        public static IElementRelation Create(IElementRelationFactory elementRelationFactory,int Componentkey, int relatedComponentkey) {
            return elementRelationFactory.Create(Componentkey,  relatedComponentkey);
        }
    }

    public class ElementRelationFactory : IElementRelationFactory
    {
        public IElementRelation Create(int NodeID)
        {
            return new ElementRelation(NodeID);    
        }

        public IElementRelation Create(int Componentkey, int relatedComponentkey)
        {
            return new ElementRelation(Componentkey,relatedComponentkey);
        }
    }

    public class ElementRelation : Entitifier.Core.EntityNode,IElementRelation
    {        
        public List<ElementRelationNode> RelationNodes;
        public ElementRelation(int nodeID):base(nodeID,-1)
        {
            NodeID = nodeID;
        }
        public ElementRelation(int Componentkey, int relatedComponentkey) :
            base( -1,-1,Componentkey, relatedComponentkey)
        {
            RelationNodes = new List<ElementRelationNode>();
        }
        public ElementRelation(int nodeID,int componentkey, int relatedComponentkey) :
           base( componentkey, relatedComponentkey)
        {
            RelationNodes = new List<ElementRelationNode>();
            NodeID = nodeID;
        }
        public void AddRelation()
        {
           
        }
        public void AddRelationNode(ElementRelationNode elementRelationNode)
        {
            this.addJoint(elementRelationNode.leftJoin, elementRelationNode.RightJoin);          
        }
    }

    public class ElementRelationNode : INodeJoint
    {
        private string _leftJoin;
        private string _rightJoin;
        public string leftJoin { get => _leftJoin; set => _leftJoin = value; }
        public string RightJoin { get => _rightJoin; set => _rightJoin = value; }

        public ElementRelationNode(string leftAttributeKey, string rightAttributeKey) {
            this._leftJoin = leftAttributeKey;
            this._rightJoin = rightAttributeKey;
        }
        
    }
}
