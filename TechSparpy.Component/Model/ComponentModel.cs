using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<ElementRelation> elementRelations;

        public ComponentModel() : base() {

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
