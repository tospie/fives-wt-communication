using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIVES;

namespace WTCommunication
{
    public class WTCommunicationPluginInitalizer : IPluginInitializer
    {
        public List<string> ComponentDependencies
        {
            get { return new List<string>(); }
        }

        public void Initialize()
        {
        }

        public string Name
        {
            get { return "WebTundraCommunication"; }
        }

        public List<string> PluginDependencies
        {
            get
            {
                return new List<string>
                {
                    "WebTundraComponents"  // Needed to load all WT specific components and IDL
                };
            }
        }

        public void Shutdown()
        {
        }
    }
}
