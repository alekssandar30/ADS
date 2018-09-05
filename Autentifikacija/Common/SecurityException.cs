using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class SecurityException
    {
        private string _reason;

        [DataMember]
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
    }
}
