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
        public static IComponentModel Create(IComponentModelFactory factory, string modelName, List<ComponentRelation> elementRelations)
        {
            return factory.Create(modelName, elementRelations);
        }
    }
    public interface IComponentModelFactory
    {
        IComponentModel Create(int modelID);
        IComponentModel Create(string modelName, List<ComponentRelation> elementRelations);
    }

    public class ComponentModelFactory : IComponentModelFactory
    {
        public IComponentModel Create(int modelID)
        {
            return new ComponentModel(modelID);
        }
        public IComponentModel Create(string modelName, List<ComponentRelation> elementRelations)
        {
            return new ComponentModel(modelName, elementRelations);
        }
    }
    
    public interface IComponentModel
    {
        string ComponentModelName { get; }
        int ComponentModelID { get; }
        List<ComponentRelation> ComponentRelations { get; }
        bool SaveModel();
        bool Remove();
        bool RemoveElement(int nodekey, int entitykey);
        bool RemoveElement(int nodeID);
        void Load();
        bool NameChange();
        bool AddRelation(IComponentRelation elementRelation);
        bool RelationChange(int NodeID, int nodeKey, List<ComponentRelationNode> elementRelationNode);
    }

    public class ComponentModel :  IComponentModel
    {
        private List<ComponentRelation> _componentRelations;
        private string _componentModelName;
        private int _componentModelID;
        public List<ComponentRelation> ComponentRelations { get => _componentRelations; }
        public string ComponentModelName { get => _componentModelName; }
        public int ComponentModelID { get => _componentModelID; }
        private  EntityModel entityModel;

        /// <summary>
        /// 
        /// </summary>
        public ComponentModel() : base()
        {
            _componentRelations = new List<ComponentRelation>();
            entityModel = new EntityModel();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelID"></param>
        public ComponentModel(int modelID)
        {
            _componentRelations = new List<ComponentRelation>();
            _componentModelID = modelID;
            entityModel = new EntityModel(modelID);
            entityModel.Init();            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="elementRelations"></param>
        public ComponentModel(string modelName, List<ComponentRelation> elementRelations)           
        {
            _componentModelName = modelName;
            _componentRelations = elementRelations;
            entityModel = new EntityModel(modelName, Param(elementRelations));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementRelations"></param>
        /// <returns></returns>
        private static List<EntityNode> Param(List<ComponentRelation> elementRelations)
        {
            List<IEntityNode> _er = elementRelations.Select(x => (IEntityNode)x).ToList();
            List<EntityNode> entityNode1 = _er.Select(x => (EntityNode)x).ToList();
            return entityNode1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentRelationNode"></param>
        /// <returns></returns>
        private static List<NodeJoint> Param(List<ComponentRelationNode> componentRelationNode)
        {
            List<INodeJoint> _er = componentRelationNode.Select(x => (INodeJoint)x).ToList();
            List<NodeJoint> entityNode1 = _er.Select(x => (NodeJoint)x).ToList();
            return entityNode1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementRelation"></param>
        /// <returns></returns>
        public bool AddRelation(IComponentRelation elementRelation)
        {
            elementRelation.ModelID = _componentModelID;
            
            if (entityModel.Validation()) {
                if (_componentModelID > 0)
                {
                    this.ComponentRelations.Add((ComponentRelation)elementRelation);
                    var er = (ComponentRelation)elementRelation;
                    er.ModelID = entityModel.ModelID;
                   
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
            return entityModel.ChangeName();
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
            return entityModel.RemoveNode(entitykey, nodekey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public bool RemoveElement(int nodeID)
        {
            return entityModel.RemoveNode(nodeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            return entityModel.RemoveModel();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveModel()
        {
            if (entityModel.Validation())
            {
                if (entityModel.ModelID > 0)
                {
                    ComponentRelation element_r ;
                    foreach (ComponentRelation er in this.ComponentRelations)
                    {
                        if (er.NodeID <= 0) {
                            element_r = er;
                        }
                        er.ModelID = entityModel.ModelID;
                        er.Save();                                
                    }
                    entityModel.Init();                  
                    return true;
                }
                else if (entityModel.Save())
                {
                    foreach (ComponentRelation er in this.ComponentRelations)
                    {
                        er.ModelID = entityModel.ModelID;
                        er.Save();                        
                    }
                    entityModel.Init();
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
        public bool RelationChange(int NodeID, int newNodeKey, List<ComponentRelationNode> elementRelationNode)
        {
            List<NodeJoint> entityNode1 = new List<NodeJoint>();
            foreach (ComponentRelationNode ern in elementRelationNode) {
                entityNode1.Add(new NodeJoint(ern.leftJoin, ern.RightJoin));
            }
            return entityModel.ChangeNode(NodeID, newNodeKey, entityNode1);            
        } 
    }
}   

    

