using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;

namespace TechSharpy.Component
{
    public abstract class IComponent: Entitifier.Core.EntitySchema
    {
        private int id;

        protected IComponent(int id)
        {
            this.id = id;
        }
        protected IComponent()
        {
           
        }
        public abstract bool ComponentSave();
         public abstract bool ComponentRemove();
         public abstract bool ComponentHide();
         public abstract void ComponentInit();
    }
    public class Component: IComponent
    {
        public string ComponentName;
        public int ID;
        public string Category;
        public string ComponentDescription;
        public ComponentType Type;
        public List<ComponentAttribute> ComponentAttributes;
        public Data.Component dataComponent;
        public Component(int iD):base(iD)
        {
            DataTable dt;
            dataComponent = new Data.Component();
            ID = iD;
            ComponentAttributes = new List<ComponentAttribute>();            
            dt =dataComponent.GetComponentByID(this.ID);
            foreach (DataRow dr in dt.Rows) {
                this.ID = dr.IsNull("ComponentID") ? -1 : dr.Field<int>("ComponentID");
                this.EntityKey = dr.IsNull("entityKey") ? -1 : dr.Field<int>("entityKey");
                this.Type = dr.IsNull("componentType") ? ComponentType._CoreComponent : dr.Field<ComponentType>("componentType");
            }
            base.Init();
        }     

        public Component():base() {
            ComponentAttributes = new List<ComponentAttribute>();
            EntityInstances = new List<Entitifier.Core.EntityField>();
        }

        public void AddAttribute(ComponentAttribute componentAttribute) {
            componentAttribute.setEntityFieldType();
            EntityInstances.Add(componentAttribute);
            ComponentAttributes.Add(componentAttribute);
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

        public override bool ComponentSave()
        {
            throw new NotImplementedException();
        }

        public override bool ComponentRemove()
        {
            throw new NotImplementedException();
        }

        public override bool ComponentHide()
        {
            throw new NotImplementedException();
        }

        public override void ComponentInit()
        {
            throw new NotImplementedException();
        }
    }
}


interface VehicleFactory
{
    Bike GetBike(string Bike);
     
}

class HondaFactory : VehicleFactory
{
    public Bike GetBike(string Bike)
    {
        switch (Bike)
        {
            case "Sports":
                return new SportsBike();
            case "Regular":
                return new RegularBike();
            default:
                throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Bike));
        }

    }

     
}


public abstract class Bike
{
   public abstract string Name();
public abstract string Speed { set; get; }
}
 

class RegularBike : Bike
{
    string _speed;   
    public override string Speed { get => _speed; set => _speed=value; }        
    public override string Name()
    {
        return "Regular Bike- Name";
    }
}

class SportsBike : Bike
{
    public string myspeed;
    string _speed;
    public override string Speed { get => _speed; set => _speed = value; }


    public override string Name()
    {
        return "Sports Bike- Name";
    }

   
}


class VehicleClient
{
    Bike bike;
     
    public VehicleClient(VehicleFactory factory, string type)
    {
      bike = factory.GetBike(type);
      string ab=  bike.Speed;
      var spk = (SportsBike)bike;
        
        


    }

    public string GetBikeName()
    {
        return bike.Name();
    }

   

}

public class ccs{
    public ccs() {
    VehicleFactory honda = new HondaFactory();
    VehicleClient hondaclient = new VehicleClient(honda, "Regular");
}
}




