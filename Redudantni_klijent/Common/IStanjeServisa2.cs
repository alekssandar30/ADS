using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract(Name = "EStanjeServera2")]

    public enum EStanjeServera2
    {
        [EnumMemberAttribute]
        Nepoznato,
        [EnumMemberAttribute]
        Primarni,
        [EnumMemberAttribute]
        Sekundarni
    }
    [ServiceContract]
    public interface IStanjeServisa2
    {
        [OperationContract]
        [FaultContract(typeof(DataException))]
        EStanjeServera2 ProveraStanja();

        [OperationContract]
        [FaultContract(typeof(DataException))]
        void AzuriranjeStanja(EStanjeServera2 stanje);
    }
}
