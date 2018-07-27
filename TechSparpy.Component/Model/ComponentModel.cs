using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component.Model
{


    public interface IComponentModel {
        bool SaveModel();
        bool RemoveModel();
        bool RemoveElement();
        void Load();

    }
    public class ComponentModel : Entitifier.Core.EntityModel, IComponentModel
    {

        public List<ElementRelation> ElementRelations;

        public ComponentModel() : base() {
            ElementRelations = new List<ElementRelation>();
            
        }
        public ComponentModel(int modelID) : base(modelID) {
            ElementRelations = new List<ElementRelation>();
        }
        public ComponentModel(string modelName, List<ElementRelation> entitynodes)
            :base(modelName, Param(entitynodes)) {
           
            
        }
        private static List<EntityNode> Param(List<ElementRelation> entitynodes) {            
            List<IEntityNode> entityNode = entitynodes.Select(x => (IEntityNode)x).ToList();
            List<EntityNode> entityNode1 = entityNode.Select(x => (EntityNode)x).ToList();
            return entityNode1;
        }



        public void AddRelation(ElementRelation elementRelation) {
            elementRelation.ModeID = this.ModelID;
            this.ElementRelations.Add(elementRelation);
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public bool RemoveElement()
        {
            throw new NotImplementedException();
        }

        public bool RemoveModel()
        {
            throw new NotImplementedException();
        }

        public bool SaveModel()
        {
            throw new NotImplementedException();
        }
    }
}
