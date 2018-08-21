using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.Attributes;

namespace TechSharpy.FormBuilder
{
    public enum renderType {
        hide=0,

    }
   public class ElementAttribute: ComponentAttribute
    {
        
        public bool IsReadonly;
        public int RowIndex;
        public int ColumnIndex;
        public renderType RenderType;
        public ElementAttribute() {
           // base.Save();
        }
        protected override bool Save()
        {
            return base.Save();
        }
        protected override bool Remove()
        {
            return base.Remove();
        }
        protected override void Load()
        {            
            base.Load();
        }
    }
}
