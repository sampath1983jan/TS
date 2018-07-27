﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component.Model
{

    public interface IElementRelation {
        void AddRelation();
        void AddRelationNode(ElementRelationNode elementRelationNode);        
    }

    public interface IElementRelationFactory {
        IElementRelation Create(int NodeID);
        IElementRelation Create(int Componentkey, int leftIndex, int rightIndex, int relatedComponentkey);
    }

    public class ElementRelationManager {
        public static IElementRelation Create(IElementRelationFactory elementRelationFactory,int nodeID) {
            return elementRelationFactory.Create(nodeID);
        }
        public static IElementRelation Create(IElementRelationFactory elementRelationFactory,int Componentkey, int leftIndex, int rightIndex, int relatedComponentkey) {
            return elementRelationFactory.Create(Componentkey, leftIndex, rightIndex, relatedComponentkey);
        }
    }

    public class ElementRelationFactory : IElementRelationFactory
    {
        public IElementRelation Create(int NodeID)
        {
            return new ElementRelation(NodeID);    
        }

        public IElementRelation Create(int Componentkey, int leftIndex, int rightIndex, int relatedComponentkey)
        {
            return new ElementRelation(Componentkey, leftIndex, rightIndex, relatedComponentkey);
        }
    }

    public class ElementRelation : Entitifier.Core.EntityNode,IElementRelation,IEntityNode
    {
        
        public List<ElementRelationNode> RelationNodes;
        public ElementRelation(int nodeID):base(nodeID,-1)
        {
            NodeID = nodeID;
        }
        public ElementRelation(int Componentkey, int leftIndex, int rightIndex, int relatedComponentkey) :
            base( Componentkey, leftIndex, rightIndex, relatedComponentkey)
        {
            RelationNodes = new List<ElementRelationNode>();
        }
        public void AddRelation()
        {
           // throw new NotImplementedException();
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

        }
        
    }
}
