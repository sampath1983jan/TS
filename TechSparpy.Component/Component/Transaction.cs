using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component
{
    public class Transaction : Component, IComponent
    {
        #region Member variables and properties
        private string _category = "";
        private string _componentDescription = "";
        private List<ComponentAttribute> _componentAttributes;
        private string _componentName = "";
        /// <summary>
        /// 
        /// </summary>
        public string Category { get => _category; set => _category = value; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentDescription { get => _componentDescription; set => _componentDescription = value; }
        /// <summary>
        /// 
        /// </summary>
        public List<ComponentAttribute> ComponentAttributes { get => _componentAttributes; set => _componentAttributes = value; }
        /// <summary>
        /// 
        /// </summary>
        public string ComponentName { get => _componentName; set => _componentName = value; }
        /// <summary>
        /// 
        /// </summary>
        public string TitlePattern;
        /// <summary>
        /// 
        /// </summary>
        public int ComponentID => this.ID;

        private Data.BusinessComponent databusinessComponent;
        private Services.ErrorHandling.ErrorInfoCollection Errors;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Transaction() : base()
        {
            ComponentAttributes = new List<ComponentAttribute>();
            this.Type = ComponentType._ComponentTransaction;
            databusinessComponent = new Data.BusinessComponent();
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public Transaction(int Id) : base(Id)
        {
            Errors = new Services.ErrorHandling.ErrorInfoCollection();
            this.ID = Id;
            ComponentAttributes = new List<ComponentAttribute>();
            DataTable dt;
            databusinessComponent = new Data.BusinessComponent();
            dt = databusinessComponent.GetComponentByID(this.ID);
            var bc = dt.AsEnumerable().Select(g => new Business
            {
                //   EntityKey = g.IsNull("entityKey") ? -1 : g.Field<int>("entityKey"),
                ID = g.IsNull("ComponentID") ? -1 : g.Field<int>("ComponentID"),
                TitlePattern = g.IsNull("TitlePattern") ? "" : g.Field<string>("TitlePattern"),
                ComponentName = g.IsNull("componentName") ? "" : g.Field<string>("componentName"),
                ComponentDescription = g.IsNull("componentDescription") ? "" : g.Field<string>("componentDescription"),

            }).FirstOrDefault();
            this.ComponentName = bc.ComponentName;
            this.ComponentDescription = bc.ComponentDescription;
            this.TitlePattern = bc.TitlePattern;
            this.ID = bc.ID;
            InitComponentAttribute();
        }
        #endregion  
        #region Internal methods
        /// <summary>
        /// 
        /// </summary>
        private void InitComponentAttribute()
        {
            DataTable dt = new DataTable();
            Data.ComponentAttribute datacomponentAttribute = new Data.ComponentAttribute();
            dt = datacomponentAttribute.GetComponentAttributes(ID.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                int InstanceID = dr.IsNull("FieldInstanceID") ? 0 : dr.Field<int>("FieldInstanceID");
                var ca = new ComponentAttribute();
                ca = (ComponentAttribute)GetFieldInstanceByID(InstanceID).CopyTo<ComponentAttribute>();
                ca.Type = dr.IsNull("Attributetype") ? AttributeType._None : dr.Field<AttributeType>("Attributetype");
                ca.ComponentKey = dr.IsNull("componentKey") ? "" : dr.Field<string>("componentKey");
                ca.Cryptography = dr.IsNull("cryptography") ? 0 : dr.Field<int>("cryptography");
                ca.RegExpression = dr.IsNull("regExpression") ? "" : dr.Field<string>("regExpression");
                ca.ParentComponentKey = dr.IsNull("parentComponent") ? "" : dr.Field<string>("parentComponent");
                ca.ParentAttribute = dr.IsNull("parentAttribute") ? "" : dr.Field<string>("parentAttribute");
                this.ComponentAttributes.Add(ca);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentAttribute"></param>
        private void AddAttribute(ComponentAttribute componentAttribute)
        {
            componentAttribute.setEntityFieldType();
            EntityInstances.Add(componentAttribute);
            ComponentAttributes.Add(componentAttribute);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        private FieldAttribute GetFieldInstanceByID(int instanceID)
        {
            return (FieldAttribute)this.EntityInstances.Where(a => a.InstanceID == instanceID).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool BaseRemove()
        {
            bool isBaseRemoved;
            isBaseRemoved = base.Remove();
            if (isBaseRemoved)
                isBaseRemoved = base.Delete();
            return isBaseRemoved;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CreateBase()
        {
            base.TableName = this.ComponentName.Replace(" ", "_");
            setEntityType();
            if (!base.Save().HasCriticalError())
            {
                if (base.Create())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ComponentHide()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ComponentRemove()
        {
            if (BaseRemove())
            {
                if (databusinessComponent.Delete(this.ID, this.EntityKey))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool ComponentSave()
        {
            if (base.EntityKey > 0 && base.ID > 0)
            {

                if (!base.Save().HasCriticalError())
                {
                    if (databusinessComponent.Update(this.ID, this.EntityKey, this.Category, this.Type, this.ComponentName, this.ComponentDescription, this.TitlePattern))
                    {
                        foreach (ComponentAttribute ca in this.ComponentAttributes)
                        {
                            ca.SaveAttribute();
                        }
                        return true;
                    }
                    else return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (CreateBase())
                {
                    if (databusinessComponent.Save(this.ID, this.Category, this.Type, this.ComponentName, this.ComponentDescription, TitlePattern))
                    {
                        foreach (ComponentAttribute ca in this.ComponentAttributes)
                        {
                            ca.ComponentKey = ID.ToString();
                            ca.EntityKey = base.EntityKey;
                            ca.SaveAttribute();
                        }
                        return true;
                    }
                    else return false;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void ComponentInit()
        {
            // return ComponentHide();

        }
        #region Implimentation methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentAttribute"></param>
        void IComponent.AddComponentAttribute(ComponentAttribute componentAttribute)
        {
            AddAttribute(componentAttribute);
            AddEntityField(componentAttribute);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IComponent.ComponentSave()
        {
            return ComponentSave();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IComponent.ComponentRemove()
        {
            return ComponentRemove();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IComponent.ComponentHide()
        {
            return ComponentHide();
        }
        /// <summary>
        /// 
        /// </summary>
        void IComponent.ComponentInit()
        {
            ComponentInit();
        }
        public bool RemoveComponentAttribute(int AttributeID)
        {
            var attr = this.ComponentAttributes.Where(a => a.AttributeID == AttributeID).FirstOrDefault();
            if (base.RemoveField(attr.InstanceID))
            {
                if (attr.RemoveAttribute())
                {
                    return base.RemoveEntityField(attr);
                }
                else return false;
            }
            else return false;
        }

        public bool UpdateComponentAttribute(ComponentAttribute ca)
        {
            EntityField ef = this.EntityInstances.Where(a => a.InstanceID == ca.InstanceID).FirstOrDefault();
            if (ef.FieldType != ca.FieldType)
            {
                // update fieldtype
                UpdateComponentAttribute(ca);
            }
            if (base.AddField(-1, ca.InstanceID, ca.EntityKey, ca.Name, ca.Description, ca.FieldType, ca.LookUpID, ca.IsRequired,
                     ca.IsUnique, ca.IsKey, ca.enableContentLimit, ca.Min, ca.Max, "", ca.IsCore, ca.IsReadOnly, ca.EnableEncription,
                     true, ca.DisplayName, ca.Value, ca.IsReadOnly, ca.DefaultValue, ca.DisplayOrder, ca.MaxLength, ca.DisplayName, ca.AutoIncrement, ca.Incrementfrom,
                     ca.Incrementby))
            {
                ca.SaveAttribute();
                return true;

            }
            else return false;
        }
        #endregion


    }
}
