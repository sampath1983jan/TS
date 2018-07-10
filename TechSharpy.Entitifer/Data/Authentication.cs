using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    class Authentication:DataAccess
    {
        DataTable dtResult;
        public Authentication()
        {
            try
            {
                this.Init();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetUserInfo(string UserName, string Password) {
            return dtResult;
        }

        public DataTable GetUserInfo(int UserID) {
            return dtResult;
        }

        public bool Save(string UserName, string Password, Security.Authentication.UserType ut) {
            return true;
        }

        public bool Delete(int UserID) {
            return true;
        }

    }
}
