using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var h = Component.ComponentManager.Create(new ComponentHandlerFactory(), "sys_User3", "testing data", Component.ComponentType._CoreComponent, "UserID");

            Component.Attributes.ComponentAttribute ca = new Component.Attributes.ComponentAttribute();
            ca.Type = Component.Attributes.AttributeType._None;
            ca.Name = "UserID";
            ca.IsKey = true;
            ca.IsUnique = true;
            ca.EntityKey = -1;
            ca.DisplayName = "User ID";
            ca.DefaultValue = "312";
            ca.IsKey = true;
            ca.enableContentLimit = true;
            ca.Max = "255";
            ca.Type = Component.Attributes.AttributeType._Number;
            ca.Min = "1";
            h.AddComponentAttribute(ca);
            ca = new Component.Attributes.ComponentAttribute();
            ca.Type = Component.Attributes.AttributeType._None;
            ca.Name = "Password";
            ca.IsKey = false;
            ca.IsUnique = true;
            ca.EntityKey = -1;
            ca.DisplayName = "Password";
            ca.DefaultValue = "312";
            ca.enableContentLimit = true;
            ca.EnableEncription = true;
            ca.Max = "255";
            ca.Min = "1";
            ca.Type = Component.Attributes.AttributeType._Text;
            h.AddComponentAttribute(ca);
            ca = new Component.Attributes.ComponentAttribute();
            ca.Type = Component.Attributes.AttributeType._None;
            ca.Name = "UserName";
            ca.IsKey = false;
            ca.IsUnique = true;
            ca.EntityKey = -1;
            ca.DisplayName = "User Name";
            ca.DefaultValue = "312";
            ca.enableContentLimit = true;
            ca.Max = "255";
            ca.Min = "1";
            ca.Type = Component.Attributes.AttributeType._Text;
            h.AddComponentAttribute(ca);
          //  h.ComponentSave();
            

            var h1 = Component.ComponentManager.Create(new ComponentHandlerFactory(), 5);
          h1.ComponentInit();
            

            System. Console.ReadLine();
        }
    }
}
