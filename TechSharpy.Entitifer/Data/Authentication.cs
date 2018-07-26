using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Entitifier.Data
{
    class User
    {
        DataTable dtResult;
        Query iQuery;
        DataBase rd = new DataBase();
        public User()
        {
            try
            {
               
                dtResult = new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetUserInfo(string UserName, string Password) {
            iQuery = new QueryBuilder(QueryType._Select).AddField("*", "s_SecurityUser")
                .AddWhere(0, "s_SecurityUser", "username", FieldType._Text, Operator._Equal, UserName, Condition._And)
                .AddWhere(0, "s_SecurityUser", "password", FieldType._Text, Operator._Equal, Password, Condition._None);
            dtResult = rd.ExecuteQuery(iQuery).GetResult;
            return dtResult;
        }

        public DataTable GetUserInfo(int UserID) {
            iQuery = new QueryBuilder(QueryType._Select).AddField("*", "s_SecurityUser")
               .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);             
            dtResult = rd.ExecuteQuery(iQuery).GetResult;
            return dtResult;  
        }

        public bool Save(string UserName, string Password, Security.User.UserType ut,string datasourcekey) {
            int nextid =  rd.getNextID("User");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("s_SecurityUser", "UserID", FieldType._String, "", nextid.ToString())
                .AddField("s_SecurityUser", "UserName", FieldType._String, "", UserName)
                .AddField("s_SecurityUser", "Password", FieldType._String, "", Password)
                .AddField("s_SecurityUser", "UserType", FieldType._String, "", ((int)ut).ToString())
                .AddField("s_SecurityUser", "datasourcekey", FieldType._String, "", datasourcekey);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else {
                return false;
            }            
        }
        public bool ChangeUserType(int UserID, Security.User.UserType UserType)
        {
            iQuery = new QueryBuilder(QueryType._Update)
                .AddField("s_SecurityUser", "UserType", FieldType._String, "", ((int)UserType).ToString())
                .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangePassword(int UserID, string Password) {
            iQuery = new QueryBuilder(QueryType._Update)
                .AddField("s_SecurityUser", "Password", FieldType._String, "", Password)
                .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result) {
                return true;
            } else {
                return false;
            }            
        }
        public bool Delete(int UserID) {
            iQuery = new QueryBuilder(QueryType._Delete).AddField("*", "s_SecurityUser")
              .AddWhere(0, "s_SecurityUser", "UserID", FieldType._Number, Operator._Equal, UserID.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;                    
            
        }

    }
}
