using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component.Model
{
    public class ComponentModelManager
    {
        public static IComponentModel Create(IComponentModelFactory factory, int modelID)
        {
            return factory.Create(modelID);
        }
        public static IComponentModel Create(IComponentModelFactory factory, string modelName, List<ElementRelation> elementRelations)
        {
            return factory.Create(modelName, elementRelations);
        }
    }
    public interface IComponentModelFactory
    {
        IComponentModel Create(int modelID);
        IComponentModel Create(string modelName, List<ElementRelation> elementRelations);
    }

    public class ComponentModelFactory : IComponentModelFactory
    {
        public IComponentModel Create(int modelID)
        {
            return new ComponentModel(modelID);
        }
        public IComponentModel Create(string modelName, List<ElementRelation> elementRelations)
        {
            return new ComponentModel(modelName, elementRelations);
        }
    }
    
    public interface IComponentModel
    {
        string ComponentModelName { get; }
        int ComponentModelID { get; }
        List<ElementRelation> ElementRelations { get; }
        bool SaveModel();
        bool Remove();
        bool RemoveElement(int nodekey, int entitykey);
        bool RemoveElement(int nodeID);
        void Load();
        bool NameChange();
        bool AddRelation(IElementRelation elementRelation);
        bool RelationChange(int NodeID, int nodeKey, List<ElementRelationNode> elementRelationNode);
    }

    public class ComponentModel : Entitifier.Core.EntityModel, IComponentModel
    {
        private List<ElementRelation> _elementRelations;
        private string _componentModelName;
        private int _componentModelID;
        public List<ElementRelation> ElementRelations { get => _elementRelations; }
        public string ComponentModelName { get => _componentModelName; }
        public int ComponentModelID { get => _componentModelID; }

        /// <summary>
        /// 
        /// </summary>
        public ComponentModel() : base()
        {
            _elementRelations = new List<ElementRelation>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelID"></param>
        public ComponentModel(int modelID) : base(modelID)
        {
            _elementRelations = new List<ElementRelation>();
            _componentModelID = modelID;
            base.Init();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="elementRelations"></param>
        public ComponentModel(string modelName, List<ElementRelation> elementRelations)
            : base(modelName, Param(elementRelations))
        {
            _componentModelName = modelName;
            _elementRelations = elementRelations;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementRelations"></param>
        /// <returns></returns>
        private static List<EntityNode> Param(List<ElementRelation> elementRelations)
        {
            List<IEntityNode> _er = elementRelations.Select(x => (IEntityNode)x).ToList();
            List<EntityNode> entityNode1 = _er.Select(x => (EntityNode)x).ToList();
            return entityNode1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementRelationNode"></param>
        /// <returns></returns>
        private static List<NodeJoint> Param(List<ElementRelationNode> elementRelationNode)
        {
            List<INodeJoint> _er = elementRelationNode.Select(x => (INodeJoint)x).ToList();
            List<NodeJoint> entityNode1 = _er.Select(x => (NodeJoint)x).ToList();
            return entityNode1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementRelation"></param>
        /// <returns></returns>
        public bool AddRelation(IElementRelation elementRelation)
        {
            elementRelation.ModeID = _componentModelID;
            
            if (base.Validation()) {
                if (_componentModelID > 0)
                {
                    this.ElementRelations.Add((ElementRelation)elementRelation);
                    var er = (ElementRelation)elementRelation;
                    er.ModeID = this.ModelID;
                    if (er.Save())
                    {
                        return true;
                    }
                    else return false;
                }
                else return true;        
                
            }
            return false;
        }
         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
        public bool NameChange()
        {
            return base.ChangeName();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodekey"></param>
        /// <param name="entitykey"></param>
        /// <returns></returns>
        public bool RemoveElement(int nodekey, int entitykey)
        {
            return base.RemoveNode(entitykey, nodekey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public bool RemoveElement(int nodeID)
        {
            return base.RemoveNode(nodeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            return base.RemoveModel();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveModel()
        {
            if (this.Validation())
            {
                if (this.ModelID > 0)
                {
                    ElementRelation element_r ;
                    foreach (ElementRelation er in this.ElementRelations)
                    {
                        if (er.NodeID <= 0) {
                            element_r = er;
                        }
                        er.ModeID = this.ModelID;
                        er.Save();                                
                    }
                    base.Init();                  
                    return true;
                }
                else if (base.Save())
                {
                    foreach (ElementRelation er in this.ElementRelations)
                    {
                        er.ModeID = base.ModelID;
                        er.Save();                        
                    }
                    base.Init();
                  //  base.UpdateModelHierarchy(this.ElementRelations[0].NodeID);
                    return true;
                }
                else
                {
                    return false;
                }               
            }
            else {
                return false;
            }           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="newNodeKey"></param>
        /// <param name="elementRelationNode"></param>
        /// <returns></returns>
        public bool RelationChange(int NodeID, int newNodeKey, List<ElementRelationNode> elementRelationNode)
        {
            List<NodeJoint> entityNode1 = new List<NodeJoint>();
            foreach (ElementRelationNode ern in elementRelationNode) {
                entityNode1.Add(new NodeJoint(ern.leftJoin, ern.RightJoin));
            }
            return ChangeNode(NodeID, newNodeKey, entityNode1);            
        } 
    }
}   

    

