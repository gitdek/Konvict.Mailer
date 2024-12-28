using System;
using System.Diagnostics;

namespace Injector
{
    /// <summary>
    /// Base event for poll events.
    /// </summary>
    public class PollEventArgs : EventArgs
    {

        #region Fields

        private string _standardOutput;
        private string _errorOutput;
        private ProcessStartInfo _processStartInfo;
        private string _remoteHost;
        private string _id;
        private Server _server;

        #endregion

        #region Constructor

        public PollEventArgs(string id, string remoteHost, string stdout, string stderr, System.Diagnostics.ProcessStartInfo processStartInfo, Server server)
        {
            _id = id;
            _remoteHost = remoteHost;
            _standardOutput = stdout;
            _errorOutput = stderr;
            _processStartInfo = processStartInfo;
            _server = server;
        }

        #endregion

        #region Properties Implementation

        public string ErrorOutput
        {
            get { return _errorOutput; }
        }

        public string Id
        {
            get { return _id; }
        }

        public System.Diagnostics.ProcessStartInfo ProcessStartInfo
        {
            get { return _processStartInfo; }
        }

        public string RemoteHost
        {
            get { return _remoteHost; }
        }

        public Server Server
        {
            get { return _server; }
        }

        public string StandardOutput
        {
            get { return _standardOutput; }
        }

        #endregion

    }
}
