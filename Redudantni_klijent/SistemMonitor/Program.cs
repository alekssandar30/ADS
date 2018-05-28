using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SistemMonitor
{
    class Program
    {
        static IStanjeServisa Primarni = null;
        static IStanjeServisa Sekundarni = null;
        static Dictionary<string, IStanjeServisa> proxy = new Dictionary<string, IStanjeServisa>()
        {
            { "Primarni", Primarni},
            { "Sekundarni", Sekundarni},

        };

        static void Main(string[] args)
        {
            proxy.Keys.ToList().ForEach(x => Connect(x));

            while (true)
            {
                try
                {
                    Connect("Primarni");
                    if (proxy["Primarni"].ProveraStanja() == EStanjeServera.Nepoznato)
                        proxy["Primarni"].AzuriranjeStanja(EStanjeServera.Primarni);
                }
                catch (CommunicationException)
                {
                    if(proxy["Sekundarni"].ProveraStanja() != EStanjeServera.Primarni)
                        proxy["Sekundarni"].AzuriranjeStanja(EStanjeServera.Primarni);
                }
                try
                {
                    Connect("Sekundarni");
                    if (proxy["Primarni"].ProveraStanja() == EStanjeServera.Primarni && proxy["Sekundarni"].ProveraStanja() != EStanjeServera.Sekundarni)
                        proxy["Sekundarni"].AzuriranjeStanja(EStanjeServera.Sekundarni);

                }
                catch (CommunicationException ){}

                Console.Clear();

                try
                {
                    Console.WriteLine(proxy["Primarni"].ProveraStanja());
                }
                catch (Exception) { Console.WriteLine("Nepoznat"); }

                try
                {
                    Console.WriteLine(proxy["Sekundarni"].ProveraStanja());
                }
                catch (Exception) { Console.WriteLine("Nepoznat"); }
                Thread.Sleep(1000);
            }

        }

        private static void Connect(string s)
        {
            ChannelFactory<IStanjeServisa> cfPrimarni= new ChannelFactory<IStanjeServisa>(s);
            proxy[s] = cfPrimarni.CreateChannel();
        }


    
    }
}
