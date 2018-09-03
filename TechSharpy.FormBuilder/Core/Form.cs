using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component;
using System.Data;
using TechSharpy.Component.Model;

namespace TechSharpy.FormBuilder
{

    public abstract class IForm {
       // private List<FormElement> _formElements;
        public int FormID;
        public string FormNumber;
        public string FormName;
        public string FormDescription;
        public string Category;
        public List<FormElement> FormElements;
        public IComponent FormComponent;
        public abstract bool   Save();
        public abstract bool Remove();
        public abstract FormElement Add(FormElement formElement);
        public abstract FormElement Add(int formID, ElementType type, string title, string code,
            FormMode mode);
        // need integrate component model in form builder.
        public IComponentModel ComponentModel;
    }
    
    public class Form:IForm        
    {        
        //private List<FormElement> _formElements;
        //public int FormID;
        //public string FormNumber;
        //public string FormName;
        //public string FormDescription;
        //public string Category;
        //public List<FormElement> FormElements { get => _formElements; }
        //public IComponent FormComponent;
        private Data.Form dataForm;

        public Form(int formComponent,string formname, string formDescription,string category) {
            FormComponent = Component.ComponentManager.Create(new ComponentHandlerFactory(), formComponent);
            FormName = formname;
            FormDescription = formDescription;
            Category = category;
            FormElements = new List<FormElement>();
            dataForm = new Data.Form();
        }
        public Form(int formID) {
            FormID = formID;
            dataForm = new Data.Form();
            LoadForm();
        }
        private void LoadForm() {
            DataTable dt,dtElements = new DataTable();
            int _componentID;
                dt= dataForm.GetForm(FormID);
            foreach (DataRow dr in dt.Rows) {
                FormNumber = dr.IsNull("Code") == true ? "" : dr.Field<string>("Code");
                FormName = dr.IsNull("Name") == true ? "" : dr.Field<string>("Name");
                FormDescription = dr.IsNull("Description") == true ? "" : dr.Field<string>("Description");
                Category = dr.IsNull("Category") == true ? "" : dr.Field<string>("Category");
                _componentID = dr.IsNull("FormComponentID") == true ? -1 : dr.Field<int>("FormComponentID");
            }
            dtElements= dt.DefaultView.ToTable(true, "elementid", "elementtype", "title", "code", "Mode", "elementattribute");           
            foreach (DataRow dr in dtElements.Rows)
            {
                int _elementid = dr.IsNull("elementid") == true ? -1 : dr.Field<int>("elementid");
                FormElement formElement = FormElement.Instance(FormID,_elementid);
                formElement.FormID  = this.FormID;
                formElement.ElementID = dr.IsNull("elementid") == true ? -1 : dr.Field<int>("elementid");
                formElement.Type = dr.IsNull("elementtype") == true ? ElementType._form : dr.Field<ElementType>("elementtype");
                formElement.Title = dr.IsNull("title") == true ? "" : dr.Field<string>("title");
                formElement.Code = dr.IsNull("code") == true ? "" : dr.Field<string>("code");
                formElement.Mode = dr.IsNull("Mode") == true ? FormMode._readonly : dr.Field<FormMode>("Mode");
                formElement.ComponentID = dr.IsNull("ComponentID") == true ? -1 : dr.Field<int>("ComponentID");
                List<ElementAttribute> _elementAttributes = dr.IsNull("elementattribute") == true ? new List<ElementAttribute>() :
                    Newtonsoft.Json.JsonConvert.DeserializeObject<List<ElementAttribute>>(dr.Field<string>("elementattribute"));
                foreach (ElementAttribute em in _elementAttributes) {
                    formElement.AddElementAttribute(em);
                }
                this.Add(formElement);
            }
        }                     
                          
        public override bool Save()
        {
            this.FormID = dataForm.Save(FormComponent.ComponentID, FormName, FormNumber, FormDescription, Category);
            foreach (FormElement fm in FormElements)
            {
                foreach (Component.Attributes.ComponentAttribute ca in FormComponent.ComponentAttributes)
                {
                    if (ca.IsKey == true)
                    {
                        ElementAttribute em = new ElementAttribute();
                        em = ca.CopyTo<ElementAttribute>();
                        em.IsReadonly = true;
                        em.ComponentKey = "-1";
                        em.RenderType = renderType.hide;
                        fm.AddElementAttribute(em);
                    }
                }
                if (fm.SaveElement())
                {

                }
            }
            return true;
        }
        public override bool Remove()
        {
            return true;
        }

        public override FormElement Add(FormElement formElement)
        {
            FormElements.Add(formElement);
            return formElement;
        }

        public override FormElement Add(int formID, ElementType type, string title, string code, FormMode mode)
        {
            FormElement fm = FormElement.Instance(formID, type, title, code, mode);
            FormElements.Add(fm);
            return fm;
        }
    }
}
