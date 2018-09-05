using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    class Program
    {
        private static ServiceHost _personServiceHost = null;
        private static ServiceHost _stanjeServiceHost = null;

        public static void Main(string[] args)
        {
            Start();
            while (true)
            {
                Console.WriteLine("Current state service is {0}.", StateService.State);
                Thread.Sleep(5000);
            }
            //Stop();
        }

        private static void Start()
        {
            _personServiceHost = new ServiceHost(typeof(PersonService));    
            _personServiceHost.Open();

            _stanjeServiceHost = new ServiceHost(typeof(StateService));
            _stanjeServiceHost.Open();

            Console.WriteLine("WCF servers ready on adresses \n1.{0} \n2.{1} \nand waiting for requests.", GetBaseAddresses()[0], GetBaseAddresses()[1]);
        }

        private static void Stop()
        {
            _personServiceHost.Close();
            _stanjeServiceHost.Close();

            Console.WriteLine("WCF servers stopped.");
        }

        private static List<string> GetBaseAddresses()
        {
            List<string> retValues = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            xmlDoc.SelectNodes("/configuration/system.serviceModel/services/service/host/baseAddresses/add")
                .Cast<XmlNode>().ToList()
                .ForEach(o => retValues.Add(o.Attributes["baseAddress"].Value));

            return retValues;
        }    
    }
}
