using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component;
using TechSharpy.Component.Attributes;
using TechSharpy.Data;

namespace TechSharpy.Component
{
    public enum ComponentType
    {
        _CoreComponent = 1,
        _SubComponent = 2,
        _ComponentAttribute = 3,
        _ComponentTransaction = 4,
        _SecurityComponent = 5,
        _GlobalComponent = 6,
    }

    public interface IComponent
    {
        string ComponentName { set; get; }
        string Category { set; get; }
        string ComponentDescription { set; get; }
        List<ComponentAttribute> ComponentAttributes { set; get; }
        int ComponentID { get; }
        bool ComponentSave();
        bool ComponentRemove();
        bool ComponentHide();
        void ComponentInit();
        void AddComponentAttribute(ComponentAttribute componentAttribute);
        bool RemoveComponentAttribute(int AttributeID);
        bool UpdateComponentAttribute(ComponentAttribute componentAttribute);
    }
    public class Component: Entitifier.Core.EntitySchema
    {          
        public int ID;        
        public ComponentType Type;             
        private Data.Component dataComponent;        
        public Component(int iD) 
        {
            DataTable dt;
            dataComponent = new Data.Component();
            ID = iD;            
            dt =dataComponent.GetComponentByID(this.ID);
            foreach (DataRow dr in dt.Rows) {
                this.ID = dr.IsNull("ComponentID") ? -1 : dr.Field<int>("ComponentID");
                this.EntityKey = dr.IsNull("entityKey") ? -1 : dr.Field<int>("entityKey");
                this.Type = dr.IsNull("componentType") ? ComponentType._CoreComponent : dr.Field<ComponentType>("componentType");
            }
            base.Init();
        }
        public Component():base() {          
            EntityInstances = new List<Entitifier.Core.EntityField>();
            dataComponent = new Data.Component();
        }      
        protected void setEntityType() {
            if (this.Type == ComponentType._CoreComponent)
            {
                base.EntityType = Entitifier.Core.EntityType._Master;
            }
            else if (this.Type == ComponentType._ComponentTransaction)
            {
                base.EntityType = Entitifier.Core.EntityType._Transaction;
            }
            else if (this.Type == ComponentType._ComponentAttribute)
            {
                base.EntityType = Entitifier.Core.EntityType._MasterAttribute;
            }
            else if (this.Type == ComponentType._GlobalComponent)
            {
                base.EntityType = Entitifier.Core.EntityType._Sudo;
            }
            else if (this.Type == ComponentType._SecurityComponent)
            {
                base.EntityType = Entitifier.Core.EntityType._System;
            }
            else if (this.Type == ComponentType._SubComponent) {
                base.EntityType = Entitifier.Core.EntityType._RelatedMaster;
            }
        }         
        protected bool Create() {
            this.ID = dataComponent.Save(this.EntityKey, this.Type);
            if (ID > 0)
            {
                return true;
            }
            else {
                return false;
            }            
        }
        protected bool Delete() {
          return  dataComponent.Delete(this.ID, this.EntityKey);
        }
        protected bool AddEntityField(ComponentAttribute componentAttribute) {

            Data.Component c = new Data.Component();
            TQueryBuilder tq;
            tq = new TQueryBuilder(TQueryType._AlterColumnName);
            tq.TableName(this.TableName.Replace(" ", ""));
            if (componentAttribute.InstanceID > 0)
            {
                //  tq.AddField(fd.Name, fd.IsKey, fd.IsUnique, fd.FieldType, true, ""); 
                tq.AddField(componentAttribute.Name, componentAttribute.IsKey, componentAttribute.IsUnique, base.getDataType(componentAttribute.FieldType), true, "");
                c.ExecuteNonQuery(tq);
                return true;
            }
            else return true;
        }
        protected bool RemoveEntityField(ComponentAttribute attr) {
            Data.Component c = new Data.Component();
            TQueryBuilder tq;
            tq = new TQueryBuilder(TQueryType._RemoveTableColumn);
            tq.TableName(this.TableName.Replace(" ", ""));
            if (attr.InstanceID > 0)
            {
                tq.AddField(attr.Name, attr.IsKey, attr.IsUnique, base.getDataType(attr.FieldType), true, "");
                c.ExecuteNonQuery(tq);
                return true;
            }
            else {
                return true;
            }
        }

        protected bool UpdateEntityFieldType(ComponentAttribute attr)
        {
            Data.Component c = new Data.Component();
            TQueryBuilder tq;
            tq = new TQueryBuilder(TQueryType._AlterTableColumnDataType);
            tq.TableName(this.TableName.Replace(" ", ""));
            if (attr.InstanceID > 0)
            {
                tq.AddField(attr.Name, attr.IsKey, attr.IsUnique, base.getDataType(attr.FieldType), true, "");
                c.ExecuteNonQuery(tq);
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}

public interface ICompnentFactory
{
    IComponent Create(ComponentType componentType);
    IComponent Create(int ComponentID);
    IComponent Create(string ComponentName, string ComponentDescription, ComponentType componentType,
                      string primarykeys, string titlePattern);
    IComponent Create(string ComponentName, string ComponentDescription, ComponentType componentType,
                      string primarykeys, string titlePattern, int parentComponentID, int RelatedAttributeID);
    IComponent Create(string ComponentName, string ComponentDescription, ComponentType componentType,
                      string primarykeys, string titlePattern, int parentComponentID, int RelatedAttributeID,int UserComponentID,int UserKey);
    IComponent Create(string ComponentName, string ComponentDescription, ComponentType componentType,
                      string primarykeys, string titlePattern, int parentComponentID, int RelatedAttributeID, 
                      int UserComponentID, int UserKey, int linkComponentID, int linkAttributeID);

    
}

public class ComponentHandlerFactory : ICompnentFactory
{
    public IComponent Create(ComponentType componentType)
    {
        if (componentType == ComponentType._CoreComponent)
        {
            return new Business();
        }
        else if (componentType == ComponentType._GlobalComponent)
        {
            return new GlobalSetting();
        }
        else if (componentType == ComponentType._ComponentAttribute) {
           // return new BusinessAttribute();
        }
        {
            return null;
        }
    }
    public IComponent Create(int ComponentID)
    {
         Component cmp = new Component(ComponentID);
        if (cmp.Type == ComponentType._CoreComponent)
        {
            return new Business(cmp.ID);
        }
        else if (cmp.Type == ComponentType._GlobalComponent)
        {
            return new GlobalSetting(cmp.ID);
        }
        else if (cmp.Type == ComponentType._ComponentAttribute)
        {
            return new BusinessAttribute(cmp.ID);
        }
        else if (cmp.Type == ComponentType._SecurityComponent)
        {
            return new Security(cmp.ID);
        }
        else if (cmp.Type == ComponentType._SubComponent) {
            return new BusinessLink(cmp.ID);
        }
        else
        {
            return null;
        }
    }
    public IComponent Create(string ComponentName, string ComponentDescription, 
                                ComponentType componentType, string primarykeys,string titlePattern)
    {
        if (componentType == ComponentType._CoreComponent)
        {
            Business bc = new Business();
            bc.ComponentName = ComponentName;
            bc.ComponentDescription = ComponentDescription;
            bc.Type = componentType;
            bc.TitlePattern = titlePattern;
            bc.PrimaryKeys = primarykeys.Split(',').ToList();
            return bc;
        }
        else if (componentType == ComponentType._GlobalComponent)
        {
            GlobalSetting gb = new GlobalSetting();
            gb.ComponentName = ComponentName;
            gb.TitlePattern = titlePattern;
            gb.ComponentDescription = ComponentDescription;
            gb.Type = ComponentType._GlobalComponent;
            gb.PrimaryKeys = primarykeys.Split(',').ToList();
            return gb;
        }
        else if (componentType == ComponentType._ComponentAttribute)
        {
            throw new Exception("Cannot find parentcomponent missing");
        }
        else if (componentType == ComponentType._ComponentTransaction) {
            Transaction bc = new Transaction();
            bc.ComponentName = ComponentName;
            bc.ComponentDescription = ComponentDescription;
            bc.Type = componentType;
            bc.TitlePattern = titlePattern;
            bc.PrimaryKeys = primarykeys.Split(',').ToList();
            return bc;
        }
        else
        {
            return null;
        }
    }
    public IComponent Create(string ComponentName, string ComponentDescription,
                                ComponentType componentType, string primarykeys, string titlePattern, int
                                parentComponentID, int RelatedAttributeID)
    {
        return new BusinessAttribute(ComponentName, ComponentDescription, componentType, primarykeys,
            titlePattern, parentComponentID, RelatedAttributeID);
    }

    public IComponent Create(string ComponentName, string ComponentDescription, ComponentType componentType, string primarykeys, string titlePattern, int parentComponentID, int RelatedAttributeID, int UserComponentID, int UserKey)
    {
        return new Security(ComponentName, ComponentDescription, componentType, primarykeys,
            titlePattern, parentComponentID, RelatedAttributeID, UserComponentID, UserKey);
    }

    public IComponent Create(string ComponentName, string ComponentDescription, ComponentType componentType, string primarykeys, string titlePattern, int parentComponentID, int RelatedAttributeID, int UserComponentID, int UserKey, int linkComponentID, int linkAttributeID)
    {
        return new BusinessLink(ComponentName, ComponentDescription, componentType, primarykeys, 
            titlePattern, parentComponentID, RelatedAttributeID, linkComponentID, linkAttributeID);
    }
}


 


 


