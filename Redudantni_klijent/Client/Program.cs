using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Client
{
    class Program
    {
        private static IPersonService _personService = null;
        private static ChannelFactory<IPersonService> cfLica = null;
        static void Main(string[] args)
        {
            

            for (uint i = 0; i < GetNumberOfServers(); i++)
            {
                try
                {
                    cfLica = new ChannelFactory<IPersonService>("ServisLica_" + i.ToString());
                    _personService = cfLica.CreateChannel();
                    Person p = new Person(1, "name 1", "lastName1");
                    Console.WriteLine(p);
                    _personService.AddPerson(p);
                    Console.WriteLine("Uspesno povezivanje na ServisLica"+i.ToString());
                 
                }
                catch (FaultException<DataException> ex)
                {
                    Console.WriteLine("Error : " + ex.Detail.Message);
                }
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Neuspelo povezivanje na ServisLica_"
                //        + i.ToString());
                //    cfLica = null; _personService = null;
                //}
            }


            if   (_personService is null)
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Pritisnite [Enter] za zaustavljanje klijenta.");
            Console.ReadLine();


        }

        static void Task4()
        {
            int i = 0;
                    try
                    {
                        Person p = new Person(++i, "name " + i, "lastName" + i);
                        Console.WriteLine(p);
                        _personService.AddPerson(p);
                    }
                    catch (FaultException<DataException> ex)
                    {
                        Console.WriteLine("Error : " + ex.Detail.Message);
                    }
               
            
        }

        private static int GetNumberOfServers()
        {
           

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

           return xmlDoc.SelectNodes("/configuration/system.serviceModel/client/endpoint")
                .Cast<XmlNode>().ToList().Count();
        
            
        }
    }
}
