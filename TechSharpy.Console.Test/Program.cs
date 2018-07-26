using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Entity.Core;
namespace TechSharpy.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {

          
            
            var lookup= Component.LookUp.LookUpManager.Create(new Component.LookUp.LookUpFactory(),
                "Gender",true, false, new List<Entitifier.Core.LookUpItem>(), Entitifier.Core.LookUpType._None);
            lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Male", "M", 1, -1));
            lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Female", "F", 2, -1));
            lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "None", "N", 0, -1));
            lookup.SaveLookUp();
            lookup.Dispose();

            lookup = Component.LookUp.LookUpManager.Create(new Component.LookUp.LookUpFactory(),
                "Status", true, false, new List<Entitifier.Core.LookUpItem>(), Entitifier.Core.LookUpType._None);
            lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Active", "A", 1, -1));
            lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Inactive", "InA", 2, -1));
            lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "None", "N", 0, -1));
            lookup.SaveLookUp();
            List<Component.LookUp.Proxy.ComponentLookUp> cs= lookup.GetLookUpList(); 

            
            //var lookup1 = Component.LookUp.LookUpManager.Create(new Component.LookUp.LookUpFactory(), lookup.GetLookUpID());
            //List<Component.LookUp.Proxy.ComponentLookUpItem> cli= lookup1.GetLookUpItem();
            //var ab= cli.Where(a => a.ItemID == cli.First().ItemID).FirstOrDefault();
            //ab.LookUpName = "Updated " + ab.LookUpName;
            //lookup1.UpdateLookUpItem(ab);
            //lookup1.DeleteLookUpItem(cli.First().ItemID);




            System.Console.ReadLine();


            //lookup.DeleteLookUp();

            //  var h = Component.ComponentManager.Create(new ComponentHandlerFactory(), "sys_User3", "testing data", Component.ComponentType._CoreComponent, "UserID");

            //  Component.Attributes.ComponentAttribute ca = new Component.Attributes.ComponentAttribute();
            //  ca.Type = Component.Attributes.AttributeType._None;
            //  ca.Name = "UserID";
            //  ca.IsKey = true;
            //  ca.IsUnique = true;
            //  ca.EntityKey = -1;
            //  ca.DisplayName = "User ID";
            //  ca.DefaultValue = "312";
            //  ca.IsKey = true;
            //  ca.enableContentLimit = true;
            //  ca.Max = "255";
            //  ca.Type = Component.Attributes.AttributeType._Number;
            //  ca.Min = "1";
            //  h.AddComponentAttribute(ca);
            //  ca = new Component.Attributes.ComponentAttribute();
            //  ca.Type = Component.Attributes.AttributeType._None;
            //  ca.Name = "Password";
            //  ca.IsKey = false;
            //  ca.IsUnique = true;
            //  ca.EntityKey = -1;
            //  ca.DisplayName = "Password";
            //  ca.DefaultValue = "312";
            //  ca.enableContentLimit = true;
            //  ca.EnableEncription = true;
            //  ca.Max = "255";
            //  ca.Min = "1";
            //  ca.Type = Component.Attributes.AttributeType._Text;
            //  h.AddComponentAttribute(ca);
            //  ca = new Component.Attributes.ComponentAttribute();
            //  ca.Type = Component.Attributes.AttributeType._None;
            //  ca.Name = "UserName";
            //  ca.IsKey = false;
            //  ca.IsUnique = true;
            //  ca.EntityKey = -1;
            //  ca.DisplayName = "User Name";
            //  ca.DefaultValue = "312";
            //  ca.enableContentLimit = true;
            //  ca.Max = "255";
            //  ca.Min = "1";
            //  ca.Type = Component.Attributes.AttributeType._Text;
            //  h.AddComponentAttribute(ca);
            ////  h.ComponentSave();


            //  var h1 = Component.ComponentManager.Create(new ComponentHandlerFactory(), 5);
            //h1.ComponentInit();



        }
    }
}
