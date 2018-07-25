using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
namespace TechSharpy.ConsoleApp
{
    class Program

    {
        public static void Get() {
            MySql.Data.MySqlClient.MySqlConnection cn = new MySqlConnection("SslMode=none;persistsecurityinfo=True;SERVER=localhost;UID=root;DATABASE=tshris;PASSWORD=admin312;");
            cn.Open();
            cn.Close();

        }

        static void Main(string[] args)
        {

            // List<string> pkey = new List<string>();
            // pkey.Add("UserID");
            // TechSharpy.Component.Component component = new Component.BusinessComponent(Component.ComponentType._CoreComponent);
            // component.ID = -1;
            // component.ComponentName = "sys_User";
            // component.PrimaryKeys = pkey;
            //// component.ComponentDescription = "User information please share to user";
            // component.Type = Component.ComponentType._CoreComponent;

            // TechSharpy.Component.ComponentManager componentManager = new Component.ComponentManager(Component.ComponentType._CoreComponent,component );

            // Component.Attributes.ComponentAttribute ca = new Component.Attributes.ComponentAttribute();
            // ca.Type = Component.Attributes.AttributeType._None;
            // ca.Name = "UserID";
            // ca.IsKey = true;
            // ca.IsUnique = true;
            // ca.EntityKey = -1;
            // ca.DisplayName = "User ID";
            // ca.DefaultValue = "312";
            // ca.IsKey = true;
            // ca.enableContentLimit = true;
            // ca.Max = "255";
            // ca.Type = Component.Attributes.AttributeType._Number;
            // ca.Min = "1";            
            // componentManager.AddAttribute(ca);
            // ca = new Component.Attributes.ComponentAttribute();
            // ca.Type = Component.Attributes.AttributeType._None;
            // ca.Name = "Password";
            // ca.IsKey = false;
            // ca.IsUnique = true;
            // ca.EntityKey = -1;
            // ca.DisplayName = "Password";
            // ca.DefaultValue = "312";
            // ca.enableContentLimit = true;
            // ca.EnableEncription = true;
            // ca.Max = "255";
            // ca.Min = "1";
            // ca.Type = Component.Attributes.AttributeType._Text;
            // componentManager.AddAttribute(ca);

            // ca = new Component.Attributes.ComponentAttribute();
            // ca.Type = Component.Attributes.AttributeType._None;
            // ca.Name = "UserName";
            // ca.IsKey = false;
            // ca.IsUnique = true;
            // ca.EntityKey = -1;
            // ca.DisplayName = "User Name";
            // ca.DefaultValue = "312";
            // ca.enableContentLimit = true;
            // ca.Max = "255";
            // ca.Min = "1";
            // ca.Type = Component.Attributes.AttributeType._Text;
            // componentManager.AddAttribute(ca);
            // componentManager.Save();
         
          var h=  Component.ComponentManager.Create(new ComponentHandlerFactory(), "sys_User1","testing data",Component.ComponentType._CoreComponent,"UserID");

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
            h.ComponentSave();


            var h1 = Component.ComponentManager.Create(new ComponentHandlerFactory(),4);
            h1.ComponentInit();
            Console.ReadKey();

            Console.ReadLine();
        }


    }
     

}
