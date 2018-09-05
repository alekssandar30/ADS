using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ConfigurationService
    {
        private ЕStateServer stateService;
        
        public ЕStateServer StateService
        {
            get
            {
                return stateService;
            }

            set
            {
                stateService = value;
            }
        }

        public ConfigurationService()
        {
            StateService = Properties.Settings.Default.StateServer;
            Console.WriteLine("Current state of server is {0}", StateService);
        }

    }
}
