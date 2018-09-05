using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Replikator
{
    class Program
    {
        static void Main(string[] args)
        {
            Replicate();          
        }
        
        static void Replicate()
        {
            // na svakih 5000ms pozivamo replikaciju
            DateTime timeOfLastReplication = new DateTime();
            while (true)
            {
                try
                {
                    ChannelFactory<IPersonService> sourceFactory
                        = new ChannelFactory<IPersonService>("source");
                    ChannelFactory<IPersonService> destinationFactory
                        = new ChannelFactory<IPersonService>("destination");

                    IPersonService sourceProxy = sourceFactory.CreateChannel();
                    IPersonService destProxy = destinationFactory.CreateChannel();

                    List<Person> persons = sourceProxy.GetAllPersons(timeOfLastReplication);
                    // pamtimo vreme replikacije
                    timeOfLastReplication = DateTime.Now;
                    destProxy.AddAllPersons(persons);

                    Console.WriteLine("Number of replicated data is {0}", persons.Count);
                    
                    Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
