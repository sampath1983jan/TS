using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Security
{
    class Authentication
    {
        public enum UserType {
            _superadmin=0,
            _admin=1,
            _superUser=2,
            _User=3
        }

        public string UserName;
        public string Password;
        public string UserID;
        public UserType Type;

        public Authentication(string userName, string password, UserType type)
        {
            UserName = userName;
            Password = password;
            Type = type;
        }

        public Authentication(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public Authentication(string userID)
        {
            UserID = userID;
        }
    }
}
