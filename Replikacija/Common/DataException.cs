using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class DataException
    {
        private string _message;

        [DataMember]
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }
    }
}
