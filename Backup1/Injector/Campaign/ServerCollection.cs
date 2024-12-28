using System;
using System.Collections.Generic;

namespace Injector
{
    [Serializable]
    public class ServerCollection
    {
        private List<Server> _servers;

        public ServerCollection()
        {
            _servers = new List<Server>();
        }

        public Server Add(string host, int port)
        {
            Server mailHost = new Server(host, port);
            _servers.Add(mailHost);

            return mailHost;
        }

        public Server Add(string host, int port, string username, string password)
        {
            Server mailHost = new Server(host, port, username, password);
            _servers.Add(mailHost);

            return mailHost;
        }

        public Server Add(Server server)
        {
            _servers.Add(server);
            return server;
        }

        public void Remove(Server server)
        {
            if (_servers.Contains(server))
                _servers.Remove(server);
        }

        public void Replace(string serverId, Server server)
        {
            Server oldServer = null;
            foreach (Server host in _servers)
            {
                if (host.ID.ToString() == serverId)
                {
                    oldServer = host;
                    break;
                }
            }
            if (oldServer != null)
                Remove(oldServer);
            Add(server);
        }


        /// <summary>
        /// Property returning a list of Mailhosts currently in the ServerCollection.
        /// </summary>
        public List<Server> Servers
        {
            //get { return new List<Server>(_servers); }
            get
            {
                return _servers;
            }
        }

        public Server this[int index]
        {
            get { return _servers[index]; }
        }

        public Server this[string serverId]
        {
            get
            {
                foreach (Server host in _servers)
                {
                    if (host.ID.ToString() == serverId)
                    {
                        return host;
                    }
                }

#if (DEBUG)
                throw new Exception("Server with specified Guid '" + serverId + "' doesn't exist !");
#else
                return null;
#endif
            }
        }

        public int Count
        {
            get { return _servers.Count; }
        }
    }
}
