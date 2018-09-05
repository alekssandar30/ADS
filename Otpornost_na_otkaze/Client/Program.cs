using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static IPersonService _personService;
        private static ChannelFactory<IPersonService> _personFactory;

        static void Main(string[] args)
        {
            _personFactory = null;
            _personService = null;

            for (int i = 0; i <= 1; i++)
            {
                try
                {
                    _personFactory = new ChannelFactory<IPersonService>("PersonService_" + i);
                    _personService = _personFactory.CreateChannel();
                    _personService.GetAllPersons(DateTime.Now); // ovu metodu pozivamo kao test metodu
                    Console.WriteLine("Service PersonService_{0} is running.", i);
                }
                catch
                {
                    _personFactory = null;
                    _personService = null;
                    Console.WriteLine("PersonService_{0} is not running " + i);
                }
            }
        }
    }
}
