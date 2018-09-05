using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
        [DataContract(Name = "ЕStateServer")]
        public enum ЕStateServer
        {
            [EnumMemberAttribute]
            Unknown,
            [EnumMemberAttribute]
            Primary,
            [EnumMemberAttribute]
            Secondary
        }

        [ServiceContract]
        public interface IStateService
        {
            [OperationContract]
            [FaultContract(typeof(DataException))]
            ЕStateServer GetState();

            [OperationContract]
            [FaultContract(typeof(DataException))]
            void SetState(ЕStateServer сtate);
        
    }
}
