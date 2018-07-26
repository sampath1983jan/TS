using System;
using System.Collections.Generic;
using System.Data;
using TechSharpy.Entitifier.Data;
using TechSharpy.Entitifier.Core;
namespace TechSharpy.Entitifier.Core
{
    public enum LookUpType {
        _None=0,
        //_Year=1,
        _Month=1,
        _Gender=2,
        _Nationality=3,
        _Currency=4,
        _Quarter=5,
        _YesNo=6,
        _Relationship=7,
        _Color=8,
        _BoodGroup=9,
        _Country=10,
    }
    
   public class EnityInstanceLookUp
    {
        public int LookUpID;
        public string Name;
        public bool IsCore;
        public bool HaveChild;
        public List<LookUpItem> LookUpItems;
        public TechSharpy.Entitifier.Core.LookUpType lookupType;
        private Entitifier.Data.LookUp dataLookup; 
        public EnityInstanceLookUp(string name, bool isCore, bool haveChild, List<LookUpItem> lookUpItems, LookUpType lookUpType)
        {
           // LookUpID = lookUpID;
            Name = name;
            IsCore = isCore;
            HaveChild = haveChild;
            LookUpItems = lookUpItems ?? throw new ArgumentNullException(nameof(lookUpItems));
            lookupType = lookUpType;
            dataLookup = new LookUp();
        }

        public EnityInstanceLookUp(int lookUpID)
        {
            LookUpID = lookUpID;
            Name = "";
            IsCore = false;
            HaveChild = false;
            LookUpItems = new List<LookUpItem>();
            lookupType = LookUpType._None;
            dataLookup = new LookUp();
        }

        public void Init() {
            DataTable dt, dtLookUp = new DataTable();
            dt = dataLookup.GetLookUpItems(-1, LookUpID);
            dtLookUp = dt.DefaultView.ToTable(true, "ClientID", "LookUpId", "LookUPName", "IsCore", "HaveChild",  "LookUpType");
            foreach (DataRow dr in dtLookUp.Rows)
            {
                Name = dr["LookUPName"] == DBNull.Value ? "" : dr["LookUPName"].ToString();
                IsCore = dr["IsCore"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsCore"]);
                lookupType = dr["LookUpType"] == DBNull.Value ?  LookUpType._None :  (LookUpType)dr["LookUpType"];
                HaveChild = dr["HaveChild"] == DBNull.Value ? false : Convert.ToBoolean(dr["HaveChild"]);
            }
            foreach (DataRow dr in dt.Rows)
            {
                LookUpItem item = new LookUpItem{                   
                    ItemID = dr["LookupInstanceID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["LookupInstanceID"]),
                    LookUpID = this.LookUpID,
                    Name = dr["LookUpItemName"] == DBNull.Value ? "" : Convert.ToString(dr["LookUpItemName"]),
                    ShortName = dr["ShortName"] == DBNull.Value ? "" : Convert.ToString(dr["ShortName"]),
                    Order = dr["Order"] == DBNull.Value ? -1 : Convert.ToInt32(dr["Order"]),
                    ParentLookUpID = dr["ParentLookUpID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ParentLookUpID"]),
                };
                LookUpItems.Add(item);
            }
        }
        public int AddLookUpItem(int lookupID, LookUpItem lookUpItem) {
            int inext;
            if (lookUpItem.ItemID > 0)
            {
                if (dataLookup.SaveItem(-1, lookUpItem.ItemID, lookupID, lookUpItem.Name, lookUpItem.ShortName, lookUpItem.Order, lookUpItem.ParentLookUpID))
                {
                    inext = lookUpItem.ItemID;
                }
                else {
                    inext = -1;
                };
            }
            else {
                inext = dataLookup.SaveItem(-1, lookupID, lookUpItem.Name, lookUpItem.ShortName, lookUpItem.Order, lookUpItem.ParentLookUpID);
            }           
            return inext;
        }
        public void Clear() {
            this.LookUpID = -1;
            this.LookUpItems = new List<LookUpItem>();
            this.IsCore = false;
            this.HaveChild = false;
            this.lookupType = LookUpType._None;
            this.Name = "";
        }
        public bool RemoveLookUpItem(int itemID) {
            if (dataLookup.DeleteLookUpItem(-1, LookUpID, itemID))
            {
                return true;
            }
            else {
                return false;
            }
        }
        public bool Update() {
            return dataLookup.Save(-1, this.LookUpID, this.Name, this.IsCore, this.HaveChild, lookupType);
        }
        public bool Save() {
            if (dataLookup.GetLookUpByName(this.Name).Rows.Count > 0) {
                return false;
            }
            if (this.LookUpID > 0)
            {
                dataLookup.Save(-1, this.LookUpID, this.Name, this.IsCore, this.HaveChild, lookupType);
                foreach (LookUpItem item in this.LookUpItems)
                {
                    AddLookUpItem(this.LookUpID, item);
                }
            }
            else {
                this.LookUpID = dataLookup.Save(-1, this.Name, this.IsCore, this.HaveChild, lookupType);
                if (this.LookUpID > 0)
                {
                    foreach (LookUpItem item in this.LookUpItems)
                    {
                        AddLookUpItem(this.LookUpID, item);
                    }
                }
            }           
            return true;
        }
        public bool Remove() {
            if (dataLookup.Delete(-1, LookUpID)) {
                dataLookup.DeleteLookUpItems(-1, LookUpID);
            }
            return true;
        }
        protected internal DataTable GetLookUps() {
            return dataLookup. GetLookUps();
        }
    }

   public class LookUpItem {
        public int ItemID;
        public int LookUpID;
        public string Name;
        public string ShortName;
        public int Order;
        public int ParentLookUpID;
        public LookUpItem() {

        }
        public LookUpItem(int itemID, int lookUpID, string value, string shortName, int order, int parentLookUpID)
        {
            ItemID = itemID;
            LookUpID = lookUpID;
            Name = value ?? throw new ArgumentNullException(nameof(value));
            ShortName = shortName ?? throw new ArgumentNullException(nameof(shortName));
            Order = order;
            ParentLookUpID = parentLookUpID;
        }

        public LookUpItem(int lookUpID)
        {
            LookUpID = lookUpID;
            ItemID = -1;
            Name = "";
            ShortName = "";
            Order = 0;
            ParentLookUpID = -1;
        }

    }

}
