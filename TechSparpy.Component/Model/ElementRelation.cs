using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Component.Model
{
    public class ElementRelation : Entitifier.Core.EntityNode
    {
        public List<ElementRelationNode> RelationNodes;

        public ElementRelation(int modelID, int Componentkey, int leftIndex, int rightIndex, int relatedComponentkey) :
            base(modelID, Componentkey, leftIndex, rightIndex, relatedComponentkey)
        {
            RelationNodes = new List<ElementRelationNode>();
        }
    }

    public class ElementRelationNode : Entitifier.Core.NodeJoint {
        public ElementRelationNode(string leftAttributeKey, string rightAttributeKey) : base(leftAttributeKey, rightAttributeKey) {

        }
    }
}
