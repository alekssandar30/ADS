using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public enum ERights { Add, Delete, Replicate}

    class SystemUser
    {
        string _username;
        string _password;
        bool _authenticated;
        HashSet<ERights> _rights = new HashSet<ERights>();

        public SystemUser(string user, string pass)
        {
            _username = user;
            _password = pass;
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public bool Authenticated
        {
            get { return _authenticated; }
            set { _authenticated = value; }
        }

        public void AddRight(ERights right)
        {
            if (!_rights.Contains(right))
            {
                _rights.Add(right);
            }
        }

        public bool HasRight(ERights right)
        {
            return _rights.Contains(right);
        }
    }
}
