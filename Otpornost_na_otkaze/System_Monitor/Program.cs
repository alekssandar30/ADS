using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;
using System.Threading;

namespace SystemMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            IStateService serviceOne = null;
            IStateService serviceTwo = null;

            // set primary state
            if (ConnectToService("first", ЕStateServer.Primary, out serviceOne))
            {
                ConnectToService("second", ЕStateServer.Secondary, out serviceTwo);
            }
            else 
                ConnectToService("second", ЕStateServer.Primary, out serviceTwo);
           
            while (true)
            {
                ЕStateServer stateOne = ЕStateServer.Unknown;
                ЕStateServer stateTwo = ЕStateServer.Unknown;

                try
                {
                    stateOne = serviceOne.GetState();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error [first] : {0}", ex.Message);
                }

                try
                {
                    stateTwo = serviceTwo.GetState();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error [second] : {0}", ex.Message);
                }

                Console.WriteLine("States of services : ");
                Console.WriteLine("Service one - {0}.", stateOne);
                Console.WriteLine("Service two - {0}.", stateTwo);

                // ako su oba servisa pokrenuta nema potreba da se bilo sta radi, samo u slucaju da je jedan od njih pao
                if (stateOne == ЕStateServer.Unknown || stateTwo == ЕStateServer.Unknown) 
                {
                    if (stateOne.Equals(ЕStateServer.Primary)) // u slucaju da je sekundarni pao
                    {
                        ConnectToService("second", ЕStateServer.Secondary, out serviceTwo);
                    }
                    else if (stateTwo.Equals(ЕStateServer.Primary)) // u slucaju da je sekundarni pao
                    {
                        ConnectToService("first", ЕStateServer.Secondary, out serviceOne);
                    }
                    else if (stateTwo == ЕStateServer.Secondary) // u slucaju da je primarni pao sekundarni se svicuje da bude primarni
                    {
                        serviceTwo.SetState(ЕStateServer.Primary);
                        Console.WriteLine("Service two is now primary,");
                        ConnectToService("first", ЕStateServer.Secondary, out serviceOne);
                    }
                    else if (stateOne == ЕStateServer.Secondary)  // u slucaju da je primarni pao sekundarni se svicuje da bude primarni
                    {
                        serviceOne.SetState(ЕStateServer.Primary);
                        Console.WriteLine("Service one is now primary,");
                        ConnectToService("second", ЕStateServer.Secondary, out serviceTwo);
                    }
                    else // u slucaju da su oba pala
                    {
                        if (ConnectToService("first", ЕStateServer.Primary, out serviceOne))
                            ConnectToService("second", ЕStateServer.Secondary, out serviceTwo);
                        else
                            ConnectToService("second", ЕStateServer.Primary, out serviceTwo);
                    }

                }
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        ///  Method for creating channel from Monitor to Service. Return if server is connected or is faild to create channel.
        /// </summary>
        /// <param name="endpointName"> Name from endpoint configuration</param>
        /// <param name="serverState"> In wich state server need to be in moment of creating </param>
        /// <param name="service"> Out parametar for service</param>
        /// <returns> if service is created properly </returns>
        static bool ConnectToService(string endpointName, ЕStateServer serverState, out IStateService service)
        {
            service = null;
            try
            {
                ChannelFactory<IStateService> serviceFactory = new ChannelFactory<IStateService>(endpointName);
                service = serviceFactory.CreateChannel();
                service.SetState(serverState);
                return true;
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("Error [0]: {1}", endpointName, ex.Message);
                return false;
            }
        }
    }
}
