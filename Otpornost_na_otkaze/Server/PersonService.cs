using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class PersonService : IPersonService
    {
        public void AddPerson(Person person)
        {
            if (StateService.State.Equals(ЕStateServer.Primary))
            {
                if (!Database.persons.ContainsKey(person.Jmbg))
                {
                    Console.WriteLine("{1} : Added person with JMBG : {0}", person.Jmbg, DateTime.Now);
                }
                else
                {
                    Console.WriteLine("{1} : Modified person with JMBG : {0}", person.Jmbg, DateTime.Now);
                }

                person.TimeOfEntry = DateTime.Now;
                Database.persons[person.Jmbg] = person; // istovremeno treba da se vrsi i dodavanje i izmena   
            }
            else
            {
                ServisStateException ex = new ServisStateException() { Reason = "Server is not in Primary state" };
                throw new FaultException<ServisStateException>(ex);
            }            
        }       

        public void RemovePerson(long jmbg)
        {
            if (StateService.State.Equals(ЕStateServer.Primary))
            {
                if (Database.persons.ContainsKey(jmbg))
                {
                    Console.WriteLine("{1} : Deleted person with JMBG : {0}", jmbg, DateTime.Now);
                    Database.persons.Remove(jmbg);
                }
                else
                {
                    DataException ex = new DataException() { Message = "Idem with ID this cannot be found." };
                    throw new FaultException<DataException>(ex);
                }
            }
            else
            {
                ServisStateException ex = new ServisStateException() { Reason = "Server is not in Primary state" };
                throw new FaultException<ServisStateException>(ex);
            }
        }     

        public List<Person> GetAllPersons(DateTime timeRequest)
        {
            if (StateService.State.Equals(ЕStateServer.Primary))
            {
                List<Person> retList = new List<Person>();
                foreach (Person p in Database.persons.Values)
                {
                    if (p.TimeOfEntry > timeRequest)
                    {
                        retList.Add(p);
                    }
                }
                Console.WriteLine("{0} : All person for replication send.", DateTime.Now);
                return retList;
            }
            else
            {
                ServisStateException ex = new ServisStateException() { Reason = "Server is not in Primary state" };
                throw new FaultException<ServisStateException>(ex);
            }
        }

        public void AddAllPersons(List<Person> persons)
        {
            // posto je ovo metoda vezana za replikator, ona moze da se poziva samo kod strane servisa koji je sekundarni
            if (StateService.State.Equals(ЕStateServer.ServiceModelondary))
            {
                foreach (Person f in persons)
                    Database.persons[f.Jmbg] = f; // ukoliko ne postoji dodace se, a ukoliko postoji azurirace se

                Console.WriteLine("{0} : All person replicated.", DateTime.Now);
            }
            else
            {
                ServisStateException ex = new ServisStateException() { Reason = "Server is not in Secondary state" };
                throw new FaultException<ServisStateException>(ex);
            }
        }
    }
}
