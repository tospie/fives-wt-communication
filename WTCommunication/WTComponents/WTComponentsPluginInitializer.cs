using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIVES;

namespace WTComponentsPlugin
{
    public class WTComponentsPluginInitializer : IPluginInitializer
    {
        public List<string> ComponentDependencies
        {
            get { throw new NotImplementedException(); }
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public List<string> PluginDependencies
        {
            get { throw new NotImplementedException(); }
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
