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
       
        static void Main(string[] args)
        {
            ChannelFactory<IPersonService> personFactory = new ChannelFactory<IPersonService>(typeof(IPersonService).ToString());            
            _personService = personFactory.CreateChannel();

            Task4();

        }

        static void Task4()
        {
            int i = 0, j = 0;
            while (true)
            {
                j += 5;
                for (; i < j; i++)
                {
                    try
                    {
                        Person p = new Person(i, "name " + i, "lastName" + i);
                        Console.WriteLine(p);
                        _personService.AddPerson(p);
                    }
                    catch (FaultException<DataException> ex)
                    {
                        Console.WriteLine("Error : " + ex.Detail.Message);
                    }
                }
                j--;
                Thread.Sleep(3000);
            }
        }
    }
}
