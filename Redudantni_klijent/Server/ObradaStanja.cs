using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ObradaStanja : IStanjeServisa
    {
        public static KonfiguracijaServera konfiguracija
        = new KonfiguracijaServera();

       

        public void AzuriranjeStanja(EStanjeServera stanje)
        {
            konfiguracija.StanjeServera = stanje;
            Console.WriteLine(stanje);
        }
        public EStanjeServera ProveraStanja()
        {
            return ObradaStanja.konfiguracija.StanjeServera;
        }
    }

}
