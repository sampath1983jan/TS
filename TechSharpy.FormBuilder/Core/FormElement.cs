using System.Collections.Generic;
using TechSharpy.Component;
using System.Data;

namespace TechSharpy.FormBuilder
{
    public enum ElementType
    {
        _form=0,
        _formlist=1,
        _derivedform=2 //custom readonly forms
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
        private IComponent icomp;

        protected  FormElement() {
            _elementAttributes = new List<ElementAttribute>();
            Title = "";
            Code = "";
            Mode = FormMode._readonly;
            Type = ElementType._form;
            dataFormElement = new Data.FormElement();
        }
        protected FormElement(int formID, ElementType type, string title, string code, FormMode mode)         
        {
            FormID = formID;
            Type = type;
            Title = title;
            Code = code;
            Mode = mode;
            ComponentID = -1;           
            _elementAttributes = new List<ElementAttribute>();
            dataFormElement = new Data.FormElement();
        }

        protected  FormElement(int formID, int elementID) {
            FormID = formID;
            ElementID = elementID;
            Load();
        }
        public static FormElement Instance(int formID, ElementType type, string title, string code, FormMode mode) {
            return new FormElement(formID,type,title,code,mode);
        }
        public static FormElement Instance()
        {
            return new FormElement();
        }
        public static FormElement Instance(int formID, int elementID)
        {
            return new FormElement(formID, elementID);            
        }        
        private FormElement  LoadFormElementInformation() {
            icomp = ComponentManager.Create(new ComponentHandlerFactory(), (int)ComponentID);
            return this;
        }

        public void Load() {
            DataTable dt = new DataTable();
            dt= dataFormElement.GetElement(ElementID);
            foreach (DataRow dr in dt.Rows) {
                FormID = dr.IsNull("FormID") == true ? -1 : dr.Field<int>("FormID");
                Type = dr.IsNull("elementtype") == true ? ElementType._form : dr.Field<ElementType>("elementtype");
                Title = dr.IsNull("title") == true ? "" : dr.Field<string>("title");
                Code = dr.IsNull("code") == true ? "" : dr.Field<string>("code");
                Mode = dr.IsNull("Mode") == true ? FormMode._readonly : dr.Field<FormMode>("Mode");
                ComponentID = dr.IsNull("ComponentID") == true ? -1 : dr.Field<int>("ComponentID");
                List< ElementAttribute > ea= dr.IsNull("elementattribute") == true ? new List<ElementAttribute>():
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<ElementAttribute>>(dr.Field<string>("elementattribute"));
                LoadFormElementInformation();
            }
        }
        public FormElement AddElementAttribute(ElementAttribute elementAttribute) {
            ElementAttributes.Add(elementAttribute);
            icomp.AddComponentAttribute(elementAttribute.CopyTo<Component.Attributes.ComponentAttribute>());
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
            if (this.ElementID <= 0)
            {
                this.ElementID = dataFormElement.Save(FormID, (int)Type, Title, Code, (int)Mode,
                Newtonsoft.Json.JsonConvert.SerializeObject(this.ElementAttributes), ComponentID);
                if (ElementID > 0)
                {
                    SaveComponent();
                }
            }
            else {
                SaveComponent();
                return dataFormElement.Save(this.FormID, ElementID, (int)Type, Title, Code, (int)Mode,
                    Newtonsoft.Json.JsonConvert.SerializeObject(this.ElementAttributes));
            }            
            return true;
        }
        public bool RemoveElementAttribute(int attributeID)
        {            
            foreach (ElementAttribute em in _elementAttributes ) {
                if (em.AttributeID == attributeID) {
                    _elementAttributes.Remove(em);
                    SaveElement();                     
                    icomp.RemoveComponentAttribute(attributeID);
                    return true;
                }
            }
            return false;
        }
        
    }
}
