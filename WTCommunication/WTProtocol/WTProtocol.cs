using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTProtocol
{
    public class WTProtocol : IProtocol
    {
        public IMessage DeserializeMessage(object message)
        {
            throw new NotImplementedException();
        }

        public string MimeType
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public object SerializeMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
