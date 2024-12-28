using System;
using System.Collections.Generic;
using System.Text;

using Injector.Net;

namespace Injector.RateLimit
{
    /// <summary>
    /// The SMTP_Settings object represents SMTP settings in Campaign.
    /// </summary>
    public sealed class SMTP_Settings
    {
        private string _domain = "";
        private int _sessionIdleTimeOut = 0;
        private int _maxConnections = 0;
        private int _maxConnsPerIP = 0;
        private int _maxBadCommnads = 0;
        private int _maxRecipientPerMsg = 0;
        private int _maxMessageSize = 0;
        private int _maxTransactions = 0;
        private bool _requireAuth = false;
        private IPBindInfo[] _binds = null;
        private SMTP_PatternInfo[] _smtpPatterns = null;

        private int _maxMsgPerConn = 0;
        private int _connectTimeout = (2 * 60);
        private int _dataSendTimeout = (3 * 60);
        private int _smtpGreetingTimeout = (5 * 60);

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <param name="idleTimeOut">Session idle timeout seconds.</param>
        /// <param name="maxConnections">Maximum conncurent connections.</param>
        /// <param name="maxConnectionsPerIP">Maximum conncurent connections fro 1 IP address.</param>
        /// <param name="maxBadCommands">Maximum bad commands per session.</param>
        /// <param name="maxRecipients">Maximum recipients per message.</param>
        /// <param name="maxMessageSize">Maximum allowed message size.</param>
        /// <param name="maxTransactions">Maximum mail transactions per session.</param>
        /// <param name="requireAuth">Specifies if SMTP server is private server and requires authentication.</param>
        /// <param name="bindings">Specifies local IP info.</param>
        internal SMTP_Settings(string domain, int idleTimeOut, int maxConnections, int maxConnectionsPerIP, int maxBadCommands, int maxRecipients, int maxMessageSize, int maxTransactions, bool requireAuth, IPBindInfo[] bindings, SMTP_PatternInfo[] smtpPatterns)
        {
            _domain = domain;
            _sessionIdleTimeOut = idleTimeOut;
            _maxConnections = maxConnections;
            _maxConnsPerIP = maxConnectionsPerIP;
            _maxBadCommnads = maxBadCommands;
            _maxRecipientPerMsg = maxRecipients;
            _maxMessageSize = maxMessageSize;
            _maxTransactions = maxTransactions;
            _requireAuth = requireAuth;
            _binds = bindings;
            _smtpPatterns = smtpPatterns;
        }

        #region Properties Implementation

        /// <summary>
        /// Gets or sets email domain. Value of "*" or "" applies to all domains.
        /// </summary>
        public string Domain
        {
            get { return _domain; }
            set { if (_domain != value) _domain = value; }
        }

        /// <summary>
        /// Gets or sets how many seconds session can idle before timed out.
        /// </summary>
        public int SessionIdleTimeOut
        {
            get { return _sessionIdleTimeOut; }
            set { if (_sessionIdleTimeOut != value) _sessionIdleTimeOut = value; }
        }

        /// <summary>
        /// Gets or sets maximum conncurent connections server accepts.
        /// </summary>
        public int MaximumConnections
        {
            get { return _maxConnections; }
            set { if (_maxConnections != value) _maxConnections = value; }
        }

        /// <summary>
        /// Gets or sets maximum conncurent connections from 1 IP address. Value 0, means unlimited connections.
        /// </summary>
        public int MaximumConnectionsPerIP
        {
            get { return _maxConnsPerIP; }
            set { if (_maxConnsPerIP != value) _maxConnsPerIP = value; }
        }

        /// <summary>
        /// Gets or sets maximum bad commands can happen before server terminates connection.
        /// </summary>
        public int MaximumBadCommands
        {
            get { return _maxBadCommnads; }
            set { if (_maxBadCommnads != value) _maxBadCommnads = value; }
        }

        /// <summary>
        /// Gets or sets how many recipients are allowed per message.
        /// </summary>
        public int MaximumRecipientsPerMessage
        {
            get { return _maxRecipientPerMsg; }
            set { if (_maxRecipientPerMsg != value) _maxRecipientPerMsg = value; }
        }

        /// <summary>
        /// Gets or sets maximum message size in MB.
        /// </summary>
        public int MaximumMessageSize
        {
            get { return _maxMessageSize; }
            set { if (_maxMessageSize != value) _maxMessageSize = value; }
        }

        /// <summary>
        /// Gets or sets maximum mail transactions per session.
        /// </summary>
        public int MaximumTransactions
        {
            get { return _maxTransactions; }
            set { if (_maxTransactions != value) _maxTransactions = value; }
        }

        /// <summary>
        /// Gets or sets if server requires authentication for all incoming connections. If this value is true,
        /// that means smtp server isn't accessible to public.
        /// </summary>
        public bool RequireAuthentication
        {
            get { return _requireAuth; }
            set { if (_requireAuth != value) _requireAuth = value; }
        }

        /// <summary>
        /// Gets IP bindings.
        /// </summary>
        public IPBindInfo[] Binds
        {
            get { return _binds; }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("Binds");

                if (!Net_Utils.CompareArray(_binds, value))
                {
                    _binds = value;
                }
            }
        }

        public SMTP_PatternInfo[] SmtpPatterns
        {
            get { return _smtpPatterns; }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("SmtpPatterns");

                _smtpPatterns = value;
            }
        }
        #endregion

    }
}
