using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using port25.pmta.api.submitter;

namespace Injector
{
    public class Session
    {
        private Server _owner = null;
        private List<Thread> _threads = new List<Thread>();
        private List<Connection> _connections = new List<Connection>(); // All the same!

        public Session(Server owner)
        {
            _owner = owner;
        }


        #region Properties Implementation

        public List<Connection> Connections
        {
            get { return _connections; }
        }

        public Server Owner
        {
            get { return _owner; }
        }

        public List<Thread> Threads
        {
            get { return _threads; }
        }

        #endregion

    }
}
