using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Database
    {
        public static Dictionary<long, Person> persons = new Dictionary<long, Person>();
        public static Dictionary<string, SystemUser> systemUsers = new Dictionary<string, SystemUser>();
        static Database()
        {
            Console.WriteLine("Adding users to server...");

            SystemUser pera = new SystemUser("pera", "P3rA");
            pera.AddRight(ERights.Add);
            systemUsers.Add(pera.Username, pera);

            SystemUser jova = new SystemUser("jova", "JoviCaX8");
            jova.AddRight(ERights.Add);
            jova.AddRight(ERights.Delete);
            systemUsers.Add(jova.Username, jova);

            SystemUser replikator = new SystemUser("rep", "Rep123");
            replikator.AddRight(ERights.Replicate);
            systemUsers.Add(replikator.Username, replikator);
        }

    }
}
