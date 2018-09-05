using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class SystemUserService : ISystemUserService
    {
        public string Authenticate(string user, string pass)
        {
            Console.WriteLine("Authenticaticating...");
            if (Database.systemUsers.ContainsKey(user))
            {
                if (Database.systemUsers[user].Password == pass)
                {
                    Database.systemUsers[user].Authenticated = true;
                    return "Success"; // value from dictionary
                }
                else
                {
                    SecurityException ex = new SecurityException();
                    ex.Reason = "Invalid password.";
                    throw new FaultException<SecurityException>(ex);
                }
            }
            else
            {
                SecurityException ex = new SecurityException();
                ex.Reason = "Invalid username.";
                throw new FaultException<SecurityException>(ex);
            }
        }

        public static bool IsUserAuthenticated(string username)
        {
            if (Database.systemUsers.ContainsKey(username))
            {
                return Database.systemUsers[username].Authenticated;
            }
            else
            {
                return false;
            }
        }

        public static bool IsUserAuthorized(string username, ERights right)
        {
            if (Database.systemUsers.ContainsKey(username))
            {
                return Database.systemUsers[username].HasRight(right);
            }
            else
            {
                return false;
            }
        }
    }
}
