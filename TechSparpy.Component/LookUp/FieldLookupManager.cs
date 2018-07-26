using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Component.LookUp.Proxy;
using TechSharpy.Entitifier;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Component.LookUp
{

    public interface IFieldLookup {
        bool SaveLookUp();
        bool DeleteLookUp();
        bool Hide();
        void Load();
        void AddLookUpItem(LookUpItem lookUpItem);
        bool DeleteLookUpItem(int LookUpItemID);
        List<ComponentLookUpItem> GetLookUpItem();
        bool UpdateLookUpItem(ComponentLookUpItem lookUpItem);
        List<Proxy.ComponentLookUp> GetLookUpList();
        int GetLookUpID();
        bool ChangeName();
        bool ChangeCoreStatus();
        bool ChangeLookUpType();
        void Dispose();
    }

    public interface ILookUpFactory {
        IFieldLookup Create(int LookUpID);
        IFieldLookup Create(string name, bool isCore, bool haveChild,
            List<LookUpItem> lookUpItems, LookUpType lookUpType);
    }

    public class LookUpManager {

        public static IFieldLookup Create(ILookUpFactory factory, int LookupID)
        {
            return factory.Create(LookupID);
        }
        public static IFieldLookup Create(ILookUpFactory factory, string name, bool isCore, bool haveChild,
            List<LookUpItem> lookUpItems, LookUpType lookUpType)
        {
            return factory.Create(name, isCore, haveChild, lookUpItems, lookUpType);
        }
    }

    public class LookUpFactory : ILookUpFactory
    {
        public IFieldLookup Create(int LookUpID)
        {
            return new ComponentLookUp(LookUpID);
        }

        public IFieldLookup Create(string name, bool isCore, bool haveChild, List<LookUpItem> lookUpItems, LookUpType lookUpType)
        {
            return new ComponentLookUp(name, isCore,
                haveChild, lookUpItems, lookUpType);            
        }
    }

   


    public class ComponentLookUp:Entitifier.Core.EnityInstanceLookUp, IFieldLookup
    {
        internal ComponentLookUp(int lookUpID) : base(lookUpID) {
            base.Init();
        }
        internal ComponentLookUp(string name, bool isCore, bool haveChild,
            List<LookUpItem> lookUpItems, LookUpType lookUpType) : base(name, isCore,
                haveChild, lookUpItems, lookUpType)
        {            
        }        

        public int GetLookUpID() {
            return base.LookUpID;
        }

        public bool ChangeName()
        {
         return   base.Update();
        }

        public bool ChangeCoreStatus()
        {
            return base.Update();
        }

        public bool ChangeLookUpType()
        {
            return base.Update();
        }
        public void Dispose() {
            base.Clear();
        }
        public List<Proxy.ComponentLookUp> GetLookUpList() {
            DataTable dt = new DataTable();
            dt =base.GetLookUps();
            List<Proxy.ComponentLookUp> lookups = new List<Proxy.ComponentLookUp>();
            foreach (DataRow dr in dt.Rows) {
                Proxy.ComponentLookUp _lookup = new Proxy.ComponentLookUp();
                _lookup.Name = dr.IsNull("LookUPName") == true ? "" : dr["LookUPName"].ToString();
                _lookup.LookUpID = dr.IsNull("LookUpId") == true ? -1 : (int)dr["LookUpId"];
                _lookup.IsCore = dr.IsNull("IsCore") == true ? false : (object)dr["IsCore"].ToString() =="1" ? true:false;
                _lookup.HaveChild = dr.IsNull("HaveChild") == true ? false : (object)dr["HaveChild"].ToString() == "1" ? true : false;
                _lookup.lookupType= dr.IsNull("lookupType") == true ?  LookUpType._None : (LookUpType)dr["lookupType"];
                lookups.Add(_lookup);
            }
            return lookups;
        }
        public bool UpdateLookUpItem(ComponentLookUpItem lookUpItem) {
            var litem= base.LookUpItems.Where(a => a.LookUpID == base.LookUpID && a.ItemID == lookUpItem.ItemID).FirstOrDefault();            
            litem.Order = lookUpItem.Order;
            litem.ShortName = lookUpItem.ShortName;
            litem.Name = lookUpItem. LookUpName;
            litem.ParentLookUpID = lookUpItem. ParentLookUpId;
            if (base.AddLookUpItem(base.LookUpID, litem)>0)
            {
                return true;
            }
            else return false;
        }

        public List<ComponentLookUpItem> GetLookUpItem() {
            List<ComponentLookUpItem> citems = new List<ComponentLookUpItem>();
            foreach (LookUpItem litm in base.LookUpItems)
            {
                ComponentLookUpItem citem = new ComponentLookUpItem();
                citem.LookUpID = this.LookUpID;
                citem.ItemID = litm.ItemID;
                citem.Order = litm.Order;
                citem.ShortName = litm.ShortName;
                citem.LookUpName = litm.Name;
                citem.ParentLookUpId = litm.ParentLookUpID;
                citems.Add(citem);
            }
            return citems;
        }
      
        public void AddLookUpItem(LookUpItem lookUpItem)
        {
            this.LookUpItems.Add(lookUpItem);
        }

        public bool DeleteLookUp()
        {
            return base.Remove();
        }

        public bool DeleteLookUpItem(int LookUpItemID)
        {
            return base.RemoveLookUpItem(LookUpItemID);
        }

        public bool Hide()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
          
        }

        public bool SaveLookUp()
        {
           return base.Save();
        }
 
    }
}

namespace TechSharpy.Component.LookUp.Proxy {

    public class ComponentLookUpItem
    {
        public int LookUpID;
        public int ItemID;
        public string LookUpName;
        public string ShortName;
        public int Order;
        public int ParentLookUpId;

    }

    public class ComponentLookUp
    {
        public int LookUpID;
        public string Name;
        public bool IsCore;
        public bool HaveChild;
        public List<ComponentLookUpItem> LookUpItems;
        public TechSharpy.Entitifier.Core.LookUpType lookupType;
    }
}