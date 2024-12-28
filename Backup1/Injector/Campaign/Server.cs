using System;
using System.Collections.Generic;
using System.Timers;
using System.Collections;
using Injector.Utilities.Collections;
using System.Threading;

namespace Injector
{
    [Serializable]
    public class Server : IDisposable
    {
        #region Fields
       
        #region Settings

        private string _host = "";
        private int _port = 25;
        private string _username = "";
        private string _password = "";
        private string _id = "";

        #endregion

        #region State

        private bool _enabled = true;
        private int _smtpSessions = 0;
        private int _relaySessions = 0;

        [NonSerialized]
        private IList<Hashtable> _sessions;

        [NonSerialized]
        private Session _session = null;

        //private int _connectionCount = 0;


        [NonSerialized]
        private System.Timers.Timer _timerClean;

        //[NonSerialized]
        //private object _serverMutex = new object();


        private MultiDictionary<object, Thread> _connections = null;

        #endregion

        #endregion

        #region Constructors

        public Server(string host, int port)
            : this(host, port, null, null)
        {
        }

        public Server(string host, int port, string username, string password)
            : this(host, port, username, password, null)
        {
        }

        public Server(string host, int port, string username, string password, string id)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;

            _timerClean = new System.Timers.Timer(2000);
            _timerClean.AutoReset = true;
            _timerClean.Elapsed += new ElapsedEventHandler(_timerClean_Elapsed);



            _sessions = new List<Hashtable>();

            Hashtable threads = Hashtable.Synchronized(new Hashtable());
            _sessions.Add(threads);
            
            // Each item in the _sessions list represents a connection to this Server object.
            // The first item is always the initializer(owner) of that connection.
            //
            // Each item is a hashtable => with the Thread object being the key and the owner (type=Campaign) of this thread the value.
            //
            // That is how we manage X connections per Server and, Y threads per connection.
        }

        #endregion

        #region Private methods

        void _timerClean_Elapsed(object sender, ElapsedEventArgs e)
        {
            // do nothing.
        }

        public void BeginSession()
        {
            _session = new Session(this);
        }

        public void AddConnection(object connection, Thread thread)
        {
            // New connection.
            if (_connections.ContainsKey(connection))
            {
                if (_connections.Values.Contains(thread))
                {
                    foreach (object key in _connections.Keys)
                        _connections.Remove(key, thread);
                }
                _connections[connection].Add(thread);
            }
            else
                _connections.Add(connection, thread);
        }

        private void StartConnection()
        {


        }

        #endregion

        #region Properties Implementation

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public int TotalConnections
        {
            get { return _connections.Count; }
        }

        //public int TotalThreads
        //{
        //}


        /// <summary>
        /// Gets all campaigns servers total Relay sessions.
        /// </summary>
        public int TotalRelaySessions
        {
            get
            {
                return _relaySessions;
            }
        }

        /// <summary>
        /// Gets all campaigns total SMTP sessions.
        /// </summary>
        public int TotalSmtpSessions
        {
            get
            {
                return _smtpSessions;
            }
        }

        public Session Session
        {
            get { return _session; }
        }

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Host
        {
            get { return _host; }
        }

        public int Port
        {
            get { return _port; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _host = null;
        }

        #endregion
    }
}
