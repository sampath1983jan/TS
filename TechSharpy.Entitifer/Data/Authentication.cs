using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    class User:DataAccess
    {
        DataTable dtResult;
        Query iQuery;
        public User()
        {
            try
            {
                this.Init();
                dtResult = new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetUserInfo(string UserName, string Password) {
            iQuery = new Query(QueryType._Select).AddField("*", "s_SecurityUser")
                .AddWhere(0, "s_SecurityUser", "username", FieldType._Text, Operator._Equal, UserName, Condition._And)
                .AddWhere(0, "s_SecurityUser", "password", FieldType._Text, Operator._Equal, Password, Condition._None);
            dtResult = this.GetData(iQuery);
            return dtResult;
        }

        public DataTable GetUserInfo(int UserID) {
            iQuery = new Query(QueryType._Select).AddField("*", "s_SecurityUser")
               .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);             
            dtResult = this.GetData(iQuery);
            return dtResult;  
        }

        public bool Save(string UserName, string Password, Security.User.UserType ut,string datasourcekey) {
            int nextid = getNextID("User");
            iQuery = new Query(QueryType._Insert)
                .AddField("s_SecurityUser", "UserID", FieldType._String, "", nextid.ToString())
                .AddField("s_SecurityUser", "UserName", FieldType._String, "", UserName)
                .AddField("s_SecurityUser", "Password", FieldType._String, "", Password)
                .AddField("s_SecurityUser", "UserType", FieldType._String, "", ((int)ut).ToString())
                .AddField("s_SecurityUser", "datasourcekey", FieldType._String, "", datasourcekey);
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else {
                return false;
            }            
        }
        public bool ChangeUserType(int UserID, Security.User.UserType UserType)
        {
            iQuery = new Query(QueryType._Update)
                .AddField("s_SecurityUser", "UserType", FieldType._String, "", ((int)UserType).ToString())
                .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangePassword(int UserID, string Password) {
            iQuery = new Query(QueryType._Update)
                .AddField("s_SecurityUser", "Password", FieldType._String, "", Password)
                .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);
            if (this.ExecuteQuery(iQuery) > 0) {
                return true;
            } else {
                return false;
            }            
        }
        public bool Delete(int UserID) {
            iQuery = new Query(QueryType._Delete).AddField("*", "s_SecurityUser")
              .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);
            if (this.ExecuteQuery(iQuery) >0)
            {
                return true;
            }
            else return false;                    
            
        }

    }
}
