using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ISystemUserService
    {
        [OperationContract]
        [FaultContract(typeof(SecurityException))]
        string Authenticate(string user, string pass);
    }
}
