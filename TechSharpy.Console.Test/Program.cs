using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier.Entity.Core;
using TechSharpy.FormBuilder;
using TechSharpy.Workflow.Business;
namespace TechSharpy.Console.Test
{
    class Program
    {
        public delegate string mydel(int a, ref int b);
        static void Main(string[] args)
        {
            int ab = 3;
            
            mydel a=  Program.Function;
            a += Program.Fun2;
            
           System.Console.WriteLine( a(12,  ref ab));
            System.Console.ReadLine();

        }
          static string Function(int c, ref int ab) {
           ab= ab* c;
            return "calling from delegate function 1:" + ab;
        }
          static string Fun2(int d, ref int abc) {
            abc = abc * d;
            return "calling from delegate function 2:" + abc;
        }

}

    }
    
 


//var workflow=  Workflow.Core.WorkflowManager.Create(new Workflow.Core.WorkflowManagerFactory(), 1);
//  workflow.Save();

//  RequestComment requestAction = new RequestComment(-1,-1,-1,"");  

//var h = Component.ComponentManager.Create(new ComponentHandlerFactory(),
//    "Employee Qualification", "testing data", Component.ComponentType._CoreComponent, "UserID","sss",9,1);

//Component.Attributes.ComponentAttribute ca = new Component.Attributes.ComponentAttribute();

//ca.Name = "UserID";
//ca.IsKey = true;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "User ID";
//ca.DefaultValue = "";
//ca.IsKey = true;
//ca.enableContentLimit = true;
//ca.Max = "255";
//ca.Type = Component.Attributes.AttributeType._Number;
//ca.Min = "1";
//ca.DisplayOrder = 1;
//h.AddComponentAttribute(ca);

//ca = new Component.Attributes.ComponentAttribute();

//ca.Name = "QualificationID";
//ca.IsKey = true;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "Qualification ID";
//ca.DefaultValue = "";
//ca.IsKey = true;
//ca.enableContentLimit = true;
//ca.Max = "255";
//ca.Type = Component.Attributes.AttributeType._Number;
//ca.DisplayOrder = 2;
//ca.Min = "1";
//h.AddComponentAttribute(ca);

//ca = new Component.Attributes.ComponentAttribute();

//ca.Name = "Major";
//ca.IsKey = true;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "Major";
//ca.DefaultValue = "";
//ca.IsKey = false;
//ca.enableContentLimit = true;
//ca.Max = "255";
//ca.Type = Component.Attributes.AttributeType._Text;
//ca.DisplayOrder = 3;
//ca.Min = "1";
//h.AddComponentAttribute(ca);
//ca = new Component.Attributes.ComponentAttribute();
//ca.Name = "decipline";
//ca.IsKey = true;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "decipline";
//ca.DefaultValue = "";
//ca.IsKey = false;
//ca.enableContentLimit = true;
//ca.Max = "255";
//ca.Type = Component.Attributes.AttributeType._Text;
//ca.DisplayOrder = 4;
//ca.Min = "1";
//h.AddComponentAttribute(ca);
//h.ComponentSave();

//var h = Component.ComponentManager.Create(new ComponentHandlerFactory(),
//"sys_Configuration", "testing data", Component.ComponentType._GlobalComponent, "ConfigurationID");
//h.ComponentSave();


//var er = Component.Model.ElementRelationManager.Create(new Component.Model.ElementRelationFactory(), 105, 104);
//er.AddRelationNode(new Component.Model.ElementRelationNode("UserID", "UserID"));

//List<Component.Model.ElementRelationNode> elementRelationNodes = new List<Component.Model.ElementRelationNode>();
//elementRelationNodes.Add(new Component.Model.ElementRelationNode("UserID", "UserID"));

//var emodel = Component.Model.ComponentModelManager.Create(new Component.Model.ComponentModelFactory(), 1);
//emodel.RelationChange(62,102, elementRelationNodes);
//   emodel.AddRelation(er);
//  emodel.RemoveElement(60);

//if (emodel.AddRelation(er))
//{
//   // emodel.SaveModel();               
//}
//else {
//    //emodel.RemoveElement(er.NodeKey, er.Entitykey);
// //   emodel.Remove();
//    System.Console.WriteLine("This relationship exist");
//};       

//var h = Component.ComponentManager.Create(new ComponentHandlerFactory(),
//"Employee", "testing data", Component.ComponentType._CoreComponent, "UserID");
//Component.Attributes.ComponentAttribute ca = new Component.Attributes.ComponentAttribute();
//ca.Type = Component.Attributes.AttributeType._None;
//ca.Name = "UserID";
//ca.IsKey = true;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "User ID";
//ca.DefaultValue = "";
//ca.IsKey = true;
//ca.enableContentLimit = true;
//ca.Max = "255";
//ca.Type = Component.Attributes.AttributeType._Number;
//ca.Min = "1";
//h.AddComponentAttribute(ca);
//ca = new Component.Attributes.ComponentAttribute();
//ca.Type = Component.Attributes.AttributeType._None;
//ca.Name = "EmployeeName";
//ca.IsKey = false;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "Employee Name";
//ca.DefaultValue = "";            
//ca.enableContentLimit = true;
//ca.Max = "555";
//ca.Type = Component.Attributes.AttributeType._Text;
//ca.Min = "1";
//h.AddComponentAttribute(ca);
//ca = new Component.Attributes.ComponentAttribute();
//ca.Type = Component.Attributes.AttributeType._None;
//ca.Name = "EmployeeNumber";
//ca.IsKey = false;
//ca.IsUnique = true;
//ca.EntityKey = -1;
//ca.DisplayName = "Employee Number";
//ca.DefaultValue = "";
//ca.enableContentLimit = true;
//ca.Max = "255";
//ca.Type = Component.Attributes.AttributeType._Text;
//ca.Min = "1";
//h.AddComponentAttribute(ca);
//h.ComponentSave();


//var lookup= Component.LookUp.LookUpManager.Create(new Component.LookUp.LookUpFactory(),
//    "Gender",true, false, new List<Entitifier.Core.LookUpItem>(), Entitifier.Core.LookUpType._None);
//lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Male", "M", 1, -1));
//lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Female", "F", 2, -1));
//lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "None", "N", 0, -1));
//lookup.SaveLookUp();
//lookup.Dispose();

//lookup = Component.LookUp.LookUpManager.Create(new Component.LookUp.LookUpFactory(),
//    "Status", true, false, new List<Entitifier.Core.LookUpItem>(), Entitifier.Core.LookUpType._None);
//lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Active", "A", 1, -1));
//lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "Inactive", "InA", 2, -1));
//lookup.AddLookUpItem(new Entitifier.Core.LookUpItem(-1, -1, "None", "N", 0, -1));
//lookup.SaveLookUp();
//List<Component.LookUp.Proxy.ComponentLookUp> cs= lookup.GetLookUpList(); 



//var lookup1 = Component.LookUp.LookUpManager.Create(new Component.LookUp.LookUpFactory(), lookup.GetLookUpID());
//List<Component.LookUp.Proxy.ComponentLookUpItem> cli= lookup1.GetLookUpItem();
//var ab= cli.Where(a => a.ItemID == cli.First().ItemID).FirstOrDefault();
//ab.LookUpName = "Updated " + ab.LookUpName;
//lookup1.UpdateLookUpItem(ab);
//lookup1.DeleteLookUpItem(cli.First().ItemID);

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

