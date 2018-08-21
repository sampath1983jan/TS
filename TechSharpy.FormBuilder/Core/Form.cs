using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component;

namespace TechSharpy.FormBuilder
{
    public class Form
    {
        private List<FormElement> _formElements;
        public int FormID;
        public string FormNumber;
        public string FormName;
        public string FormDescription;
        public string Category;
        public List<FormElement> FormElements { get => _formElements; }
        public IComponent FormComponent;
        private Data.Form dataForm;

        public Form(int formComponent,string formname, string formDescription,string category) {
            FormComponent = Component.ComponentManager.Create(new ComponentHandlerFactory(), formComponent);
            FormName = formname;
            FormDescription = formDescription;
            Category = category;
            _formElements = new List<FormElement>();
            dataForm = new Data.Form();
        }
        public Form(int formID) {
            FormID = formID;
            dataForm = new Data.Form();
        }
        public FormElement Add(FormElement formElement ) {
            _formElements.Add(formElement);
            return formElement;
        }
        public FormElement Add(int formID,  ElementType type, string title, string code,
            FormMode mode,int componentID)
        {
            FormElement fm = new FormElement(formID,  type, title, code, mode, componentID);
            _formElements.Add(fm);
            return fm;
        }                       

        public bool  Save() {
            this.FormID = dataForm.Save(FormComponent.ComponentID, FormName, FormNumber, FormDescription, Category);
            foreach (FormElement fm in _formElements) {
                foreach (Component.Attributes.ComponentAttribute ca in FormComponent.ComponentAttributes) {
                    if (ca.IsKey == true) {
                        ElementAttribute em = new ElementAttribute();                        
                        em=ca.CopyTo<ElementAttribute>();
                        em.IsReadonly = true;
                        em.ComponentKey = "-1";
                        em.RenderType = renderType.hide;
                        fm.AddElementAttribute(em);
                    }
                }
                if (fm.SaveElement()) {

                }                
            }
            return true;
        }

        public bool Remove() {
            return true;
        }              
        
    }
}
