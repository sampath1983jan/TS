using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component;

namespace TechSharpy.FormBuilder
{
    public enum ElementType
    {
        _form=0,
        _formlist=1,
        _derivedform=2
    }
    public enum FormMode {
        _readonly=0,
        _readwrite=1
    }

  public  class FormElement 
    {
        private List<ElementAttribute> _elementAttributes;
        public int FormID;
        public int ElementID;
        public ElementType Type;
        public string Title;
        public string Code;
        public FormMode Mode;
        public int ComponentID;
        public List<ElementAttribute> ElementAttributes { get => _elementAttributes; }

        private Data.FormElement dataFormElement;
        public FormElement() {
            _elementAttributes = new List<ElementAttribute>();
            Title = "";
            Code = "";
            Mode = FormMode._readonly;
            Type = ElementType._form;
            dataFormElement = new Data.FormElement();
        }
        public FormElement(int formID, ElementType type, string title, string code, FormMode mode,int componentID)         
        {
            FormID = formID;
            Type = type;
            Title = title;
            Code = code;
            Mode = mode;
            ComponentID = componentID;
            //base.Name = title;
            //base.Description = title;
            //base.TableName = title.Replace(" ", "_");
            _elementAttributes = new List<ElementAttribute>();
            dataFormElement = new Data.FormElement();
        }

        public FormElement(int formID, int elementID) {
            FormID = formID;
            ElementID = elementID;
            _elementAttributes = new List<ElementAttribute>();
        }

        public void LoadElement() {

        }

        public FormElement AddElementAttribute(ElementAttribute elementAttribute) {
                ElementAttributes.Add(elementAttribute);
            return this;
        }
        private bool SaveComponent() {
            string primarykeys = "";
            foreach (ElementAttribute em in this.ElementAttributes) {
                if (em.IsKey == true) {
                    primarykeys = primarykeys + "," + em.Name ;
                }
                if (primarykeys.StartsWith(",")) {
                    primarykeys = primarykeys.Substring(1);
                }
            }
            IComponent icomp = Component.ComponentManager.Create(new ComponentHandlerFactory(), Title, Title,
                ComponentType._ComponentTransaction, primarykeys, "");
            Component.Attributes.ComponentAttribute ca = new Component.Attributes.ComponentAttribute();
            foreach (ElementAttribute em in this.ElementAttributes)
            {
                ca= em.CopyTo<Component.Attributes.ComponentAttribute>();
                icomp.AddComponentAttribute(ca);
            }                           
            return icomp.ComponentSave();
        }
        public bool SaveElement() {
            this.ElementID = dataFormElement.Save(FormID, (int)Type, Title, Code, (int)Mode,
                Newtonsoft.Json.JsonConvert.SerializeObject(this.ElementAttributes), ComponentID);
            if (ElementID >0) {
                SaveComponent();
            }            
            return true;
        }
        public bool RemoveElementAttribute(int attributeID)
        {
            return true;
        }
        
    }
}
