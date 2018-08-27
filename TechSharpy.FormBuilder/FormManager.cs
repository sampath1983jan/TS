using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.FormBuilder
{

  public  abstract class IFormManagerFactory

    {
        public abstract IForm CreateForm(int formID);
        public abstract IForm CreateForm(int formComponent, string formname, string formDescription, string category);
    }

    public class FormManager
    {                        
        public static IForm Create(FormManagerFactory factory,int formID)
        {
            return  factory.CreateForm(formID );

        }
        public static IForm Create(FormManagerFactory factory, int formComponent, string formname, string formDescription, string category)
        {
            return factory.CreateForm(formComponent,formname,formDescription,category );
        }
    } 

    public class FormManagerFactory : IFormManagerFactory
    {
        public override IForm CreateForm(int formID)
        {
            return new Form(formID ); 
        }

        public override IForm CreateForm(int formComponent, string formname, string formDescription, string category)
        {
            return new Form(formComponent, formname, formDescription, category);
        }
    }     
}

