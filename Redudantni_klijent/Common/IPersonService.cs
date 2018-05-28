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
            [FaultContract(typeof(DataException))]
            void AddPerson(Person person);

            [OperationContract]
            [FaultContract(typeof(DataException))]
            void RemovePerson(long jmbg);

            [OperationContract]
            List<Person> GetAllPersons(DateTime timeRequest);

            [OperationContract]
            void AddAllPersons(List<Person> persons);        
    }
}
