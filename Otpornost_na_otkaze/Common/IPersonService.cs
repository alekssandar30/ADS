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
            [FaultContract(typeof(ServisStateException))]
            void AddPerson(Person person);

            [OperationContract]
            [FaultContract(typeof(DataException))]
            [FaultContract(typeof(ServisStateException))]
            void RemovePerson(long jmbg);

            [OperationContract]
            [FaultContract(typeof(ServisStateException))]
            List<Person> GetAllPersons(DateTime timeRequest);

            [OperationContract]
            [FaultContract(typeof(ServisStateException))]
            void AddAllPersons(List<Person> persons);        
    }
}
