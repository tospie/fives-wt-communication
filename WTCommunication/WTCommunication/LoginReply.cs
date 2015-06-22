using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTCommunicationPlugin
{
    public struct LoginReply
    {
        public bool Success;
        public UInt32 ConnectionID;
        public string ReplyData;
    }
}
