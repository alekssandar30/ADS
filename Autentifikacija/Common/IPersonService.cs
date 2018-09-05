using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
        [ServiceContract]
        public interface IPersonService
        {
            [OperationContract]
            [FaultContract(typeof(SecurityException))]
            void AddPerson(Person person, string username);

            [OperationContract]
            [FaultContract(typeof(DataException))]
            [FaultContract(typeof(SecurityException))]
            void RemovePerson(long jmbg, string username);

            [OperationContract]
            [FaultContract(typeof(SecurityException))]
            List<Person> GetAllPersons(string username, DateTime timeRequest);

            [OperationContract]
            [FaultContract(typeof(SecurityException))]
            void AddAllPersons(List<Person> persons, string username);        
    }
}
