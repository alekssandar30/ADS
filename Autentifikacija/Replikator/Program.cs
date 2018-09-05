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
            // da bismo mogli da izvrsavamo replikaciju prvo moramo da autentifikujemo reprilikatora kako bi mogao da poziva odredjene funkcije
            // moramo ga autentifikovati kod oba servera (izvora i odredista) kako bismo mogli pozivati njihove funkcije
            Authenticate();
            Replicate();          
        }

        static void Authenticate()
        {
            try
            {
                ChannelFactory<ISystemUserService> userSystemSource
                    = new ChannelFactory<ISystemUserService>("userSystemSource");
                ISystemUserService userSystemSourceService = userSystemSource.CreateChannel();

                ChannelFactory<ISystemUserService> userSystemDest
                   = new ChannelFactory<ISystemUserService>("userSystemDest");
                ISystemUserService userSystemDestService = userSystemDest.CreateChannel();

                try
                {
                    userSystemSourceService.Authenticate("rep", "Rep123");
                }
                catch (FaultException<SecurityException> ex)
                {
                    Console.WriteLine("Error[Source Service] : {0}", ex.Detail.Reason);
                }

                try
                {
                    userSystemDestService.Authenticate("rep", "Rep123");
                }
                catch (FaultException<SecurityException> ex)
                {
                    Console.WriteLine("Error [Destination Service]  : {0}", ex.Detail.Reason);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

                    List<Person> persons = sourceProxy.GetAllPersons("rep", timeOfLastReplication);
                    // pamtimo vreme replikacije
                    timeOfLastReplication = DateTime.Now;
                    destProxy.AddAllPersons(persons, "rep");

                    Console.WriteLine("Number of replicated data is {0}", persons.Count);
                    
                    Thread.Sleep(5000);
                }
                catch (FaultException<SecurityException> ex)
                {
                    Console.WriteLine(ex.Detail.Reason);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
