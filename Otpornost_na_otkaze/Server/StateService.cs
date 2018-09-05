using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server
{
    public class StateService : IStateService
    {
        private static ConfigurationService config = new ConfigurationService();

        public static ЕStateServer State
        {
            get { return config.StateService; }
        }
        public ЕStateServer GetState()
        {
            return config.StateService;
        }

        public void SetState(ЕStateServer state)
        {
            config.StateService = state;
        }
    }
}
