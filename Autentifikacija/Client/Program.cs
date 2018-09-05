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
        private static ISystemUserService _userSystemService;

        static void Main(string[] args)
        {
            ChannelFactory<IPersonService> personFactory = new ChannelFactory<IPersonService>(typeof(IPersonService).ToString());
            ChannelFactory<ISystemUserService> userSystemFactory = new ChannelFactory<ISystemUserService>(typeof(ISystemUserService).ToString());

            _userSystemService = userSystemFactory.CreateChannel();
            _personService = personFactory.CreateChannel();

            //MeniZaRad();

            Zadatak4();

        }

        static void MeniZaRad()
        {
            int input;
            do
            {
                input = 0;
                PrintMenu();
                int.TryParse(Console.ReadLine(), out input);
                switch (input)
                {
                    case 1:
                        LogIn();
                        break;
                    case 2:
                        AddPerson();
                        break;
                    case 3:
                        RemovePerson();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("You need to insert option from 1 to 4 !");
                        break;
                }
            } while (true);
        }

        static void PrintMenu()
        {
            Console.WriteLine("Insert option from 1 to 4");
            Console.WriteLine("1. Log in to system");
            Console.WriteLine("2. Add Person");
            Console.WriteLine("3. Remove Person");
            Console.WriteLine("4. Exit");
        }

        static void LogIn()
        {
            Console.WriteLine("username :");
            string username = Console.ReadLine();
            Console.WriteLine("password:");
            string pass = Console.ReadLine();
            try
            {
                _userSystemService.Authenticate(username, pass);
            }
            catch (FaultException<SecurityException> ex)
            {
                Console.WriteLine("Error : " + ex.Detail.Reason);
            }
        }

        static void AddPerson()
        {
            Console.WriteLine("------ ADD PERSON ------");
            Console.WriteLine("username :");
            string username = Console.ReadLine();
            Console.WriteLine("name:");
            string name = Console.ReadLine();
            Console.WriteLine("last name:");
            string lasteName = Console.ReadLine();
            Console.WriteLine("jmbg:");
            long jmbg = 0;
            if (long.TryParse(Console.ReadLine(), out jmbg))
            {
                Person f = new Person(jmbg, name, lasteName);
                try
                {
                    _personService.AddPerson(f, username);
                }
                catch (FaultException<SecurityException> ex)
                {
                    Console.WriteLine("Error : " + ex.Detail.Reason);
                }
            }
            else
            {
                Console.WriteLine("Error : Wrong format of jmbg");
            }
        }

        static void RemovePerson()
        {
            Console.WriteLine("------ REMOVE PERSON ------");
            Console.WriteLine("username :");
            string username = Console.ReadLine();
            Console.WriteLine("jmbg:");
            long jmbg = 0;
            if (long.TryParse(Console.ReadLine(), out jmbg))
            {
                try
                {
                    _personService.RemovePerson(jmbg, username);
                }
                catch (FaultException<SecurityException> ex)
                {
                    Console.WriteLine("Error : " + ex.Detail.Reason);
                }
                catch (FaultException<DataException> ex)
                {
                    Console.WriteLine("Error : " + ex.Detail.Message);
                }
            }
            else
            {
                Console.WriteLine("Error : Wrong format of jmbg");
            }
        }

        static void Zadatak4()
        {
            // pre nego sto radimo bilo koju operaciju moramo da autentifikujemo korisnika
            try
            {
                // autentifikujemo se sa korisnikom koji ima pravo pisanja
                _userSystemService.Authenticate("jova", "JoviCaX8");
            }
            catch (FaultException<SecurityException> ex)
            {
                Console.WriteLine("Error : " + ex.Detail.Reason);
            }

            int i = 0, j = 0;
            while (true)
            {

                j = j + 5;
                for (;i < j;i++)
                {
                    try
                    {
                        _personService.AddPerson(new Person(i, "name" + i, "last name"+i), "jova");
                    }
                    catch (FaultException<SecurityException> ex)
                    {
                        Console.WriteLine("Error : " + ex.Detail.Reason);
                    }                   
                }
                j--;
                Thread.Sleep(3000);
            }
        }
    }
}
