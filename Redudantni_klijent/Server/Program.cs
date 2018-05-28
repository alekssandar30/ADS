using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    class Program
    {
        private static ServiceHost _personServiceHost = null;

        public static void Main(string[] args)
        {
            Start();
            

            Console.ReadKey(true);
            Stop();
        }

        private static void Start()
        {
            _personServiceHost = new ServiceHost(typeof(PersonService));    
            _personServiceHost.Open();
            ServiceHost svcStanja = new ServiceHost(typeof(ObradaStanja));
            svcStanja.Open();
            Console.WriteLine("WCF servers ready on adresses \n1.{0} \nand waiting for requests.", GetBaseAddresses());
            Console.WriteLine("Pritisnite [Enter] za zaustavljanje servisa.");


            
        }

        private static void Stop()
        {
            _personServiceHost.Close();

            Console.WriteLine("WCF servers stopped.");
        }

        private static string GetBaseAddresses()
        {
            List<string> retValues = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            xmlDoc.SelectNodes("/configuration/system.serviceModel/services/service/host/baseAddresses/add")
                .Cast<XmlNode>().ToList()
                .ForEach(o => retValues.Add(o.Attributes["baseAddress"].Value));

            return retValues[0];
        }    
    }
}
