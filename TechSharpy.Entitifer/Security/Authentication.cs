using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TechSharpy.Entitifier.Security
{
  public  class User
    {
        public enum UserType {
            _superadmin=0,
            _admin=1,
            _superUser=2,
            _User=3
        }

        public string UserName;
        public string Password;
        public int UserID;
        public UserType Type;
        public string DataSourcekey;
        private Data.User dataAuthentication;

        public User(string userName, string password, UserType type,string datasourcekey)
        {
            UserName = userName;
            Password = password;
            Type = type;
            DataSourcekey = datasourcekey;
            dataAuthentication = new Data.User();
        }

        public User(string datasourcekey, string userName, string password)
        {
            UserName = userName;
            Password = password;
            DataSourcekey = datasourcekey;
            dataAuthentication = new Data.User();
        }
        public User() { }

        public User(int userID)
        {
            UserID = userID;
            dataAuthentication = new Data.User();
        }

        public bool CheckUser() {
            DataTable dt = new DataTable();
            dt = dataAuthentication.GetUserInfo(this.UserName,this.Password);
            if (dt.Rows.Count > 0)
            {
                var e = dt.AsEnumerable().Select(g => new User
                {
                    UserID = g.IsNull("UserID") ? 0 : g.Field<int>("UserID"),
                    UserName = g.IsNull("UserName") ? "" : g.Field<string>("UserName"),
                    DataSourcekey = g.IsNull("DataSourcekey") ? "" : g.Field<string>("DataSourcekey"),
                    Type = g.IsNull("UserType") ? UserType._User : g.Field<UserType>("UserType"),
                }).First();
                UserID = e.UserID;
                UserName = e.UserName;
                DataSourcekey = e.DataSourcekey;
                Type = e.Type;
                return true;
            }
            else {
                return false;
                throw new Exception("Authentication Failed");
            }            
        }
        private void Init() {
            DataTable dt = new DataTable();
            dt= dataAuthentication.GetUserInfo(UserID);
            var e = dt.AsEnumerable().Select(g => new User
            {
                UserID = g.IsNull("UserID") ? 0 : g.Field<int>("UserID"),
                UserName = g.IsNull("UserName") ? "" : g.Field<string>("UserName"),
                DataSourcekey = g.IsNull("DataSourcekey") ? "" : g.Field<string>("DataSourcekey"),
                Type = g.IsNull("UserType") ? UserType._User : g.Field<UserType>("UserType"),              
            }).First();
            UserID = e.UserID;
            UserName = e.UserName;
            DataSourcekey = e.DataSourcekey;
            Type = e.Type;
        }

        public bool Save() {            
            if (dataAuthentication.Save(this.UserName, this.Password, this.Type, this.DataSourcekey))
            {
                return true;
            }
            else {
                return false;
            }                        
        }
        public bool ChangePassword(string password,string confirmPassword) {
            if (password == confirmPassword)
            {
                if (dataAuthentication.ChangePassword(this.UserID, password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }            
        }
        public bool ChangeUserType(UserType userType)
        {
           
                if (dataAuthentication.ChangeUserType(this.UserID, userType))
                {
                    return true;
                }
                else
                {
                    return false;
                }             
            
        }

    }
}
