// comment out to disable cyclic macros.
// uncomment for release build!
#define _CYCLIC_MACRO

using System;
using System.IO;
using System.Text;
using System.Drawing;
using Injector.MacroEx;
using System.Threading;
using System.Collections;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using LumenWorks.Framework.IO.Csv;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using pmta = port25.pmta.api.submitter;
using System.ComponentModel;
using System.Xml.Serialization;


using Injector.Core.Logging;
using Injector.Core.TaskScheduler;

using Injector.Net.Mail;
using Injector.Net.MIME;
using Injector.Net.SMTP;

namespace Injector
{

    #region Delegates

    /// <summary>
    /// General delegate for delegating to a different class.
    /// </summary>
    /// <param name="id">id of the campaign</param>
    /// <param name="args"></param>
    public delegate void CampaignDelegate(string id, params object[] args);

    #endregion


    [MacroSuite(), Serializable()]
    public partial class Campaign
        : IDisposable, ICloneable, IEnumerable<Message>
    {

        #region Structs

        [Serializable]
        public struct CampaignStats
        {
            private uint _processed;
            private uint _successful;
            private uint _failed;
            private uint _ratepersecond;
            private TimeSpan _interval;
            private uint _rcptlistsProcessed;
            private uint _processedseed;
            private int _trackingClickThrough;
            private int _trackingOpens;
            private int _trackingUnsubscribes;

            public uint Processed
            {
                get { return _processed; }
                set { _processed = value; }
            }

            public uint Processedseed
            {
                get { return _processedseed; }
                set { _processedseed = value; }
            }

            public uint Successful
            {
                get { return _successful; }
                set { _successful = value; }
            }

            public uint Failed
            {
                get { return _failed; }
                set { _failed = value; }
            }

            public uint Ratepersecond
            {
                get { return _ratepersecond; }
                set { _ratepersecond = value; }
            }

            public uint RecipientListProcessed
            {
                get { return _rcptlistsProcessed; }
                set { _rcptlistsProcessed = value; }
            }

            [XmlIgnore]
            public TimeSpan Interval
            {
                get { return _interval; }
                set { _interval = value; }
            }

            [XmlElement("Interval", DataType = "duration")]
            public string TimeSpanInterval
            {
                get
                {
                    return TypeDescriptor.GetConverter(_interval).ConvertTo(_interval, typeof(string)) as string;
                }
                set
                {
                    object interval = TypeDescriptor.GetConverter(_interval).ConvertFrom(value);
                    if (interval is TimeSpan)
                    {
                        Interval = (TimeSpan)interval;
                    }
                }
            }

            public int TrackingClickThrough
            {
                get { return _trackingClickThrough; }
                set { _trackingClickThrough = value; }
            }

            public int TrackingOpens
            {
                get { return _trackingOpens; }
                set { _trackingOpens = value; }
            }

            public int TrackingUnsubscribes
            {
                get { return _trackingUnsubscribes; }
                set { _trackingUnsubscribes = value; }
            }



        }

        #endregion

        #region Enums

        [Flags]
        public enum RecipientField
        {
            BCC = 0,
            CC = 1,
            TO = 2,
            RANDOM = 3,
        }

        [Flags]
        public enum RotateMessageCondition
        {
            Minutes = 0,
            EmailsProcessed = 1,
        }

        [Obsolete]
        public enum ReadAction
        {
            FirstToLast = 0,
            LastToFirst = 1,
            RandomOrder = 2,
            Parallel = 3,
        }

        #endregion

        #region Constants

        /// <summary>
        /// Defines the default hashing algorithm.
        /// </summary>
        public const string DefaultHashAlgo = "MD5";

        /// <summary>
        /// Defines the default charset used for encoding of emails.
        /// </summary>
        public const string DefaultCharset = "us-ascii";

        /// <summary>
        /// Defines the default minimum emails per session.
        /// </summary>
        public const int DefaultMinSessionEmails = 0x02;

        /// <summary>
        /// Defines the default maximum emails per session.
        /// </summary>
        public const int DefaultMaxSessionEmails = 0x05;

        /// <summary>
        /// Defines the default threads per connection.
        /// </summary>
        public const int DefaultThreadsPerConnection = 0x10;

        /// <summary>
        /// Defines the default connections per servers.
        /// </summary>
        public const int DefaultConnectionsPerServer = 0x05;

        /// <summary>
        /// Defines the default value for the amount of emails to send before checking the minutes elapsed until the next rotate.
        /// </summary>
        /// <example>
        /// Every 1000 messages processed, check if the time elapsed since started of using the current
        /// message has elapsed past the rotate message interval. Only when RotateMessageCondition is set.
        /// </example>
        public const int DefaultRotateMinuteCheckInterval = 0x1000;

        /// <summary>
        /// Defines the ucase characters which are used to build a random string with macros.
        /// </summary>
        private const string RandomStringCharsUCase = "ABCDEFGHJKLMNPQRSTWXYZ";

        /// <summary>
        /// Defines the lcase characters which are used to build a random string with macros.
        /// </summary>
        private const string RandomStringCharsLCase = "abcdefgijkmnopqrstwxyz";

        /// <summary>
        /// Defines the numeric characters which are used to build a random string with macros.
        /// </summary>
        private const string RandomStringCharsNumeric = "0123456789";

        #endregion

        #region Fields

        #region Settings

        //[NonSerialized]
        //private bool _mailmerge = false;

        private string _id = string.Empty;
        private string _name = string.Empty;
        private RotateMessageCondition _rotateMessageCondition;
        private RecipientField _recipientField = RecipientField.TO;
        private int _minSessionEmails;
        private int _maxSessionEmails;
        private string _supressionFile = string.Empty;
        private bool _suppress;
        private bool _sendSeeds;
        private string[] _seeds;
        private long _seedInterval;
        private bool _rotateMessages;
        private long _rotateMessageInterval;
        private bool _encodeSubjectMIME;
        private bool _encodeFromMIME;
        private string[] _mimeCharsetFrom;
        private string[] _mimeCharsetSubject;
        private List<string> _domains;
        private int _connectionsPerServer;
        private int _threadsPerConnection;
        private string _vmtaPool = string.Empty;


        private string _owner = string.Empty;

        private bool _enableSchedule = false;
        private Schedule _schedule = new Schedule();
        private DateTime _expiration = DateTime.MaxValue;
        private Occurrence _occurrence = Occurrence.Once;

        #endregion

        #region State

        // decoupled logging delegates

        [NonSerialized]
        public CampaignDelegate OnCampaignCompleted;

        private bool _valuesChanged = false;

        /// <summary>
        /// Indicates if the end of the recipient files has been reached.
        /// </summary>
        private bool _eor;

        /// <summary>
        /// Contains the dictionary of message indexes by campaignId. The key is the message campaignId and the value is its index.
        /// </summary>
        private Dictionary<string, int> _messageIdIndexes = null;

        /// <summary>
        /// Contains a tiny suppression list.
        /// This is not implemented yet
        /// </summary>
        //private string[] _suppressionList;

        /// <summary>
        /// Contains the current message index.
        /// </summary>
        private int _currentMessageIndex;

        /// <summary>
        /// Contains the current RecipientProvider index.
        /// </summary>
        private int _currentRecipientList;

        /// <summary>
        /// Contains the remaining time or emails processed since the last <see cref="RotateMessageInterval"/>.
        /// When this reaches 0 or below, the rotation occurs.
        /// </summary>
        private long _nextRotate;

        /// <summary>
        /// Contains the remaining emails to process until the <see cref="RotateMessageInterval"/> is evaluated.
        /// Only used when <see cref="RotateMessageCondition"/> is set to use time.
        /// </summary>
        /// <remarks>
        /// The point of having an interval which then evaluates another interval is due to the fact that when 
        /// the <see cref="T:RotateMessageCondition"/> is set to minutes, evaluating the <see cref="RotateMessageInterval"/> every 
        /// time an email is processed to get the elapsed time can hurt performance.
        /// </remarks>
        private long _nextCheckRotateMinutes;
        
        /// <summary>
        /// Contains the count of <see cref="Message"/> this instance contains.
        /// </summary>
        private int _messageCount;

        /// <summary>
        /// Contains the value when the next seeds will be processed.
        /// </summary>
        private long _nextSeedIndex;

        /// <summary>
        /// Indicates if the campaign is currently running.
        /// </summary>
        [field: NonSerialized()]
        private bool _running;

        /// <summary>
        /// Contains the list of messages.
        /// </summary>
        private List<Message> _messageList = null;

        /// <summary>
        /// Contains the list of servers.
        /// </summary>
        private List<Server> _servers = null;

        /// <summary>
        /// Contains the list of worker threads.
        /// </summary>
        [field: NonSerialized()]
        private List<Thread> _threads = null;

        ///// <summary>
        ///// Contains the list of connections.
        ///// </summary>
        //[NonSerialized]
        //private List<pmta.Connection> _connections = null;

        /// <summary>
        /// Contains the list of <see cref="RecipientProvider"/>.
        /// </summary>
        private List<RecipientProvider> _recipientList = null;

        /// <summary>
        /// Contains the thread master.
        /// </summary>
        [NonSerialized]
        private Thread _master = null;

        /// <summary>
        /// Contains the campaign statistics.
        /// </summary>
        //[NonSerialized]
        internal CampaignStats _campaignStats;

        /// <summary>
        /// Contains the date and time for when the campaign was started.
        /// </summary>
        private DateTime _startTime = DateTime.MinValue;

        /////// <summary>
        /////// Contains the session list for the current running.
        /////// </summary>
        ////[NonSerialized]
        ////private Dictionary<Server, pmta.Connection> _sessions = null;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Campaign class.
        /// </summary>
        public Campaign()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Campaign class.
        /// </summary>
        /// <param name="name">The friendly name for this campaign.</param>
        public Campaign(string name)
            : this(name, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Campaign class.
        /// </summary>
        /// <param name="name">The friendly name for this campaign.</param>
        /// <param name="messageName">The name for the Message class initialized with this Campaign.</param>
        public Campaign(string name, string messageName)
        {
#if DEBUG
            _allocStack = new System.Diagnostics.StackTrace();
#endif
            

            _domains = new List<string>();
            _threads = new List<Thread>();
            _recipientList = new List<RecipientProvider>();
            _servers = new List<Server>();
            _messageList = new List<Message>();
            _messageIdIndexes = new Dictionary<string, int>();
            _defsRnd = new Dictionary<char, char>();
            _campaignStats = new CampaignStats();
            
            // When the 'Name' property is set, 'ID' is generated.
            this.Name = name != null ? name : RandomEx.RandomString(6);

            _currentMessageIndex = 0;
            _currentRecipientList = 0;
            _threadsPerConnection = DefaultThreadsPerConnection;
            _connectionsPerServer = DefaultConnectionsPerServer;
            _maxSessionEmails = DefaultMaxSessionEmails;
            _minSessionEmails = DefaultMinSessionEmails;

            PopulateRndDefs();

            MacroManager.DefineSuite(this);

            if (!String.IsNullOrEmpty(messageName)) AddMessage(messageName);

        }

        #endregion

        #region Properties

        public bool EnableSchedule
        {
            get { return _enableSchedule; }

            set
            {
                if (_enableSchedule == value)
                    return;
                _enableSchedule = value;
                _valuesChanged = true;
            }
        }

        public DateTime Expiration
        {
            get { return _expiration; }
            set
            {
                if (_expiration == value)
                    return;
                _expiration = value;
                _valuesChanged = true;
            }
        }

        public Occurrence Occurrence
        {
            get { return _occurrence; }
            set
            {
                if (_occurrence == value)
                    return;
                _occurrence = value;
                _valuesChanged = true;
            }
        }

        public Schedule Schedule
        {
            get { return _schedule; }
            set
            {
                _schedule = value;
                _valuesChanged = true;
            }
        }

        /// <summary>
        /// Gets the current message index in the Message array.
        /// </summary>
        public int CurrentMessageIndex
        {
            get { return _currentMessageIndex; }
        }

        /// <summary>
        /// Gets the statistics structure.
        /// </summary>
        public CampaignStats CampaignStatistics
        {
            get { return _campaignStats; }
            set { _campaignStats = value; }
        }

        /// <summary>
        /// Indicates if the campaign is currently running.
        /// </summary>
        public bool Running
        {
            get { return _running; }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
        }

        /// <summary>
        /// Gets if this campaign object has changes.
        /// </summary>
        public bool HasChanges
        {
            get { return _valuesChanged; }
        }

        public string VmtaPool
        {
            get { return _vmtaPool; }
            set
            {
                if (_vmtaPool == value)
                    return;
                _vmtaPool = value;
                _valuesChanged = true;
            }
        }

        public int ConnectionsPerServer
        {
            get { return _connectionsPerServer; }
            set
            {
                if (_connectionsPerServer == value)
                    return;
                _connectionsPerServer = value;
                _valuesChanged = true;
            }
        }

        public List<string> Domains
        {
            get { return _domains; }
            set
            {
                if (_domains == value)
                    return;
                _domains = value;
                _valuesChanged = true;
            }
        }

        public int ThreadsPerConnection
        {
            get { return _threadsPerConnection; }
            set
            {
                if (_threadsPerConnection == value)
                    return;
                _threadsPerConnection = value;
                _valuesChanged = true;
            }
        }

        public string ID
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                _valuesChanged = true;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                _id = GetId(_name);
                _valuesChanged = true;
            }
        }

        public List<Campaign.RecipientProvider> RecipientProviders
        {
            get { return _recipientList; }
        }

        public List<Server> Mailhosts
        {
            get
            {
                // Not created yet, create it. We use late binding here, 
                // create objects and bind them when we need them.
                if (_servers == null)
                    _servers = new List<Server>();

                return _servers;
            }
            set
            {
                if (_servers == value)
                    return;
                _servers = value;
                _valuesChanged = true;
            }
        }

        public int SessionMinimumEmails
        {
            get { return _minSessionEmails; }
            set
            {
                if (_minSessionEmails == value)
                    return;
                _minSessionEmails = value;
                _valuesChanged = true;
            }
        }

        public int SessionMaximumEmails
        {
            get { return _maxSessionEmails; }
            set
            {
                if (_maxSessionEmails == value)
                    return;
                _maxSessionEmails = value;
                _valuesChanged = true;
            }
        }

        public string SupressionFile
        {
            get { return _supressionFile; }
            set
            {
                if (_supressionFile == value)
                    return;
                _supressionFile = value;
                _valuesChanged = true;
            }
        }

        public bool Suppression
        {
            get { return _suppress; }
            set
            {
                if (_suppress == value)
                    return;
                _suppress = value;
                _valuesChanged = true;
            }
        }

        public bool Seeding
        {
            get { return _sendSeeds; }
            set
            {
                if (_sendSeeds == value)
                    return;
                _sendSeeds = value;
                _valuesChanged = true;
            }
        }

        public string[] Seeds
        {
            get { return _seeds; }
            set
            {
                if (_seeds == value)
                    return;
                _seeds = value;
                _valuesChanged = true;
            }
        }


        public long SeedInterval
        {
            get { return _seedInterval; }
            set
            {
                if (_seedInterval == value)
                    return;
                _seedInterval = value;
                _valuesChanged = true;
            }
        }

        public bool RotateMessages
        {
            get { return _rotateMessages; }
            set
            {
                if (_rotateMessages == value)
                    return;
                _rotateMessages = value;
                _valuesChanged = true;
            }
        }

        public long RotateMessageInterval
        {
            get { return _rotateMessageInterval; }
            set
            {
                if (_rotateMessageInterval == value)
                    return;
                _rotateMessageInterval = value;
                _valuesChanged = true;
            }
        }

        public RotateMessageCondition rotateMessageCondition
        {
            get { return _rotateMessageCondition; }
            set
            {
                if (_rotateMessageCondition == value)
                    return;
                _rotateMessageCondition = value;
                _valuesChanged = true;
            }
        }

        public RecipientField recipientField
        {
            get { return _recipientField; }
            set
            {
                if (_recipientField == value)
                    return;
                _recipientField = value;
                _valuesChanged = true;
            }
        }

        public bool EncodeSubjectMIME
        {
            get { return _encodeSubjectMIME; }
            set
            {
                if (_encodeSubjectMIME == value)
                    return;
                _encodeSubjectMIME = value;
                _valuesChanged = true;
            }
        }

        public bool EncodeFromMIME
        {
            get { return _encodeFromMIME; }
            set
            {
                if (_encodeFromMIME == value)
                    return;
                _encodeFromMIME = value;
                _valuesChanged = true;
            }
        }

        public string[] MIMECharsetFrom
        {
            get { return _mimeCharsetFrom; }
            set
            {
                if (value != null && _mimeCharsetFrom != value)
                {
                    foreach (string encName in value)
                    {
                        Exception ex = null;
                        Encoding encoding = null;

                        try
                        {
                            encoding = Encoding.GetEncoding(encName);
                        }
                        catch (Exception x)
                        {
                            ex = x;
                        }

                        if (ex == null || encoding == null || encoding.WebName.ToUpper() != encName.ToUpper())
                        {
                            return;
                        }
                    }

                    _mimeCharsetFrom = value;
                    _valuesChanged = true;
                }
            }
        }
        public string[] MIMECharsetSubject
        {
            get { return _mimeCharsetSubject; }
            set
            {
                if (value != null && _mimeCharsetSubject != value)
                {
                    foreach (string encName in value)
                    {
                        Exception ex = null;
                        Encoding encoding = null;

                        try
                        {
                            encoding = Encoding.GetEncoding(encName);
                        }
                        catch (Exception x)
                        {
                            ex = x;
                        }

                        if (ex == null || encoding == null || encoding.WebName.ToUpper() != encName.ToUpper())
                        {
                            return;
                        }
                    }

                    _mimeCharsetSubject = value;
                    _valuesChanged = true;
                }
            }
        }

        public List<Message> MessageList
        {
            get
            {
                // Not created yet, create it. We use late binding here, 
                // create objects and bind them when we need them.
                if (_messageList == null)
                    _messageList = new List<Message>();

                return _messageList;
            }
            set
            {
                if (_messageList == value)
                    return;
                _messageList = value;
                _valuesChanged = true;
            }
        }



        #endregion

        #region Methods

        private void OnCompleted(string id, params object[] args)
        {
            if (OnCampaignCompleted != null)
                OnCampaignCompleted(id, args);
        }

        #region Stop

        /// <summary>
        /// Stop the campaign.
        /// </summary>
        public void Stop()
        {
            ILogger logger = ServiceScope.Get<ILogger>();
            logger.Debug("Campaign {0} stopping...", _name);

            if (!_running)
                throw new Exceptions.InvalidStateException("Stop() called when current campaign is not in progress.");

            CheckDisposed();

            //_running = false;

            //if (_threads != null)
            //{
            //    lock (_threads)
            //    {
            //        if (_threads != null)
            //        {
            //            foreach (Thread thread in _threads)
            //            {
            //                if (thread != null)
            //                {
            //                    lock (_lock)
            //                    {
            //                        if (thread.IsAlive)
            //                        {
            //                            try
            //                            {
            //                                thread.Abort();
            //                            }
            //                            catch (Exception)
            //                            {
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    //_threads = null;
            //}

            if (_master != null)
            {
                lock (_master)
                {
                    if (_master != null)
                    {
                        //if (_master.IsAlive)
                            _master.Abort();
                    }
                }
                //_master = null;
            }


            if (_threads != null)
            {
                lock (_threads)
                {
                    if (_threads != null)
                    {
                        for (int i = 0; i < _threads.Count; i++)
                        {
                            Thread thread = (Thread)_threads[i];
                            if (thread != null) //&& thread.IsAlive
                                thread.Abort();
                        }
                    }
                }
            }

            while (_threads.Count > 0)
            {
                for (int i = 0; i < _threads.Count; )
                {
                    Thread thread = (Thread)_threads[i];
                    if (thread.ThreadState == ThreadState.Stopped || thread.ThreadState == ThreadState.Aborted)
                        _threads.RemoveAt(i);
                    else
                        i++;
                }
                Thread.Sleep(0);
            }

            _running = false;
        }

        #endregion

        #region Start

        /// <summary>
        /// Start the campaign.
        /// </summary>
        public void Start()
        {
            if (_running)
                throw new Exceptions.InvalidStateException("Start() called when current campaign is already in progress.");

            if (_recipientList != null && _recipientList.Count > 0)
                _currentRecipientList = 0;
            else
                throw new ArgumentNullException("Recipient lists.");

            if (_messageList != null && _messageList.Count > 0)
                _currentMessageIndex = 0;
            else
                throw new ArgumentNullException("Messages.");

            if (_domains == null || _domains.Count == 0)
                throw new ArgumentNullException("Domains.");
            
            if (_servers == null || _servers.Count == 0)
                throw new ArgumentNullException("Servers.");

            if (_sendSeeds && _seeds != null && _seeds.Length > 0 && _seedInterval > 0)
                _nextSeedIndex = _seedInterval;

            if (_encodeFromMIME && (_mimeCharsetFrom == null || _mimeCharsetFrom.Length == 0))
                _encodeFromMIME = false;

            if (_encodeSubjectMIME && (_mimeCharsetSubject == null || _mimeCharsetSubject.Length == 0))
                _encodeSubjectMIME = false;

            if (_rotateMessages)
            {
                int cnt = 0;
                foreach (Message msg in _messageList) if (msg.InRotation) cnt++;

                if (cnt > 0)
                {
                    _nextRotate = _rotateMessageInterval;
                    _nextCheckRotateMinutes = DefaultRotateMinuteCheckInterval;
                }
                else
                {
                    _nextRotate = 0;
                    _nextCheckRotateMinutes = 0;
                    _rotateMessages = false;
                }
            }

            if (_minSessionEmails > _maxSessionEmails)
                _minSessionEmails = _maxSessionEmails;
            else if (_minSessionEmails <= 0 && _maxSessionEmails > 0)
                _minSessionEmails = 1;

            // Cleanup a bit before we start.

            //if (_sendSeeds) _seeds = String.Join(String.Empty, _seeds).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //if (_encodeFromMIME) _mimeCharsetFrom = String.Join(String.Empty, _mimeCharsetFrom).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //if (_encodeSubjectMIME) _mimeCharsetSubject = String.Join(String.Empty, _mimeCharsetSubject).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //_domains = string.Join(string.Empty, _domains).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int d = 0; d < _domains.Count - 1; d++)
                _domains[d] = _domains[d].Replace(" ", "");

            // Initialize Server's session.
            foreach (Server mailHost in _servers)
                mailHost.BeginSession();


            _eor = false;
            _recipientList[_currentRecipientList].Eof = false;
            _recipientList[_currentRecipientList].BeginRead();


            _running = true;
            _campaignStats = new CampaignStats();
            _startTime = DateTime.Now;
            
            _master = new Thread(new ThreadStart(RunThreads));
            _master.Start();
        }

        #endregion

        #region Pause


        public void Pause()
        {

            throw new NotImplementedException("Pause not implemented yet.");

        }

        #endregion

        #region Resume

        public void Resume()
        {

            throw new NotImplementedException("Resume not implemented yet.");

        }

        #endregion

        #region Commit

        public void Commit()
        {
            _valuesChanged = false;
        }

        #endregion

        #region RotateToNextMessage

        private void RotateToNextMessage()
        {
            for (int i = 0; i < _messageList.Count; i++)
            {
                if (i == _messageList.Count)
                    i = 0;

                if (this[i].InRotation && i != _currentMessageIndex)
                {
                    _currentMessageIndex = _messageIdIndexes[this[i].ID];
                    _nextRotate = _rotateMessageInterval;

                    if (_rotateMessageCondition == RotateMessageCondition.Minutes)
                        this[_currentMessageIndex].LastRotateTime = DateTime.Now.Ticks;

                    return;
                }
            }
        }

        #endregion

        #region RotateToRandomMessage

        private void RotateToRandomMessage()
        {
            List<Message> msgs = _messageList.FindAll(delegate(Message m) { return m.InRotation; });
            _nextRotate = _rotateMessageInterval;

            lock (this)
            {

                _currentMessageIndex = new Random(unchecked((int)DateTime.Now.Ticks)).Next(0, msgs.Count);
                if (_rotateMessageCondition == RotateMessageCondition.Minutes) this[_currentMessageIndex].LastRotateTime = DateTime.Now.Ticks;
            
            }

        }

        #endregion

        #region ReadNextMessage

        protected virtual bool ReadNextMessage()
        {

            CheckDisposed();

            if (_currentMessageIndex > _messageCount)
                return false;

            _currentMessageIndex++;
            return true;
        }

        #endregion

        #region CopyCurrentMessageTo

        /// <summary>
        /// Copies the message array of the current index to a message object.
        /// </summary>
        /// <param name="message">The message that is the destination of the current message index.</param>
        /// <exception cref="InvalidOperationException">
        ///	No current message.
        /// </exception>
        public void CopyCurrentMessageTo(Message message)
        {
            CopyCurrentMessageTo(message, false);
        }

        /// <summary>
        /// Copies the message array of the current index to a message object.
        /// </summary>
        /// <param name="message">The message that is the destination of the current message index.</param>
        /// <param name="deepCopy">Indicates a Deep Copy of the current record.</param>
        /// <exception cref="InvalidOperationException">
        ///	No current message.
        /// </exception>
        public void CopyCurrentMessageTo(Message message, bool deepCopy)
        {

            if (_currentMessageIndex < 0 || _currentMessageIndex > _messageCount)
                throw new InvalidOperationException("No current message.");

            //if (deepCopy)
            //{
            message = (Message)this[_currentMessageIndex].Clone();
            //}
            //else
            //{
            //    message = _messageList[_currentMessageIndex].ShallowCopy();
            //}
        }


        #endregion

        #region MoveTo

        public virtual void MoveTo(int record)
        {
            if (record < 0)
                throw new ArgumentOutOfRangeException("record");

            if (record > _messageCount)
                throw new ArgumentOutOfRangeException("record");

            // Should be doing ReadNextMessage()!
            _currentMessageIndex = record;
        }
        #endregion

        #region Indexers

        /// <summary>
        /// Gets the message with the specified campaignId.
        /// </summary>
        /// <value>
        /// The message at the specified index.
        /// </value>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="campaignId"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="T:ArgumentException">
        ///		<paramref name="campaignId"/> not found.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public Message this[int index]
        {
            get
            {
                return _messageList[index];
            }
        }

        /// <summary>
        /// Gets the message with the specified campaignId.
        /// </summary>
        /// <value>
        /// The message with the specified campaignId.
        /// </value>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="campaignId"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="T:ArgumentException">
        ///		<paramref name="campaignId"/> not found.
        /// </exception>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public Message this[string id]
        {
            get
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("campaignId");

                int index = GetMessageIndex(id);
                if (index < 0)
                    throw new ArgumentException("campaignId");

                return this[index];
            }
        }

        #endregion

        #region Message Modifiers

        public void AddMessage(string name)
        {
            Message message = new Message(name);
            _messageList.Add(message);
            _messageIdIndexes.Add(message.ID, _messageCount);
            _messageCount = _messageList.Count;
        }

        public void RemoveMessage(string id)
        {
            int index = GetMessageIndex(id);
            this[index].Dispose();
            _messageList.RemoveAt(index);

            // Re-index the messages.
            _messageIdIndexes.Clear();
            _messageCount = 0;

            foreach (Message m in _messageList)
            {
                _messageIdIndexes.Add(m.ID, _messageCount);
                _messageCount = _messageIdIndexes.Count;
            }
        }

        public void RenameMessage(string id, string name)
        {
            this[id].Name = name;
        }

        #endregion

        #region GetMessageIndex

        /// <summary>
        /// Get the message index for the provided campaignId.
        /// </summary>
        /// <param name="campaignId">campaignId to look for.</param>
        /// <returns>The message index for the provided campaignId. -1 if not found.</returns>
        public int GetMessageIndex(string id)
        {
            int index;

            if (_messageIdIndexes != null && _messageIdIndexes.TryGetValue(id, out index))
                return index;
            else
                return -1;

        }

        #endregion

        #region GetMessageNames

        public string[] GetMessageNames()
        {
            string[] names = new string[_messageCount];
            for (int i = 0; i < _messageCount; i++)
                names[i] = this[i].Name.ToString();
            return names;
        }

        #endregion

        #region GetMessageCount

        /// <summary>
        /// Gets the current Message count.
        /// </summary>
        public int GetMessageCount
        {
            get { return _messageCount; }
        }

        #endregion

        #region ParseBetween

        private string ParseBetween(string basestring, string string1, string string2, int offset)
        {
            if (basestring == null) return null;

            if (basestring.Contains(string1) == true)
            {
                string result = basestring.Substring(basestring.IndexOf(string1) + offset);
                if (result.Contains(string2) == true)
                    if (string2 != "")
                        result = result.Substring(0, result.IndexOf(string2));

                return result;
            }

            return null;
        }

        #endregion

        #region ParseBetweenRev

        private string ParseBetweenRev(string basestring, string string1, string string2, int offset)
        {
            if (basestring == null) return null;

            if (basestring.Contains(string1) == true)
            {
                int index1 = basestring.IndexOf(string1);
                string tmp = basestring.Substring(0, basestring.IndexOf(string1));

                if (tmp.Contains(string2) == true)
                    if (string2 != "")
                    {
                        string result = tmp.Substring(tmp.LastIndexOf(string2) + offset);
                        return result;
                    }

                return tmp;
            }

            return null;
        }

        #endregion

        #region GenId

        /// <summary>
        /// Get a unique ID from this campaigns name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetId(string name)
        {
            string hash = CryptoEx.ComputeHash(name, DefaultHashAlgo, null);
            byte[] hashBytes = Convert.FromBase64String(hash);
            StringBuilder id = new StringBuilder();

            for (int k = 0; k < hashBytes.Length; k++)
                id.Append(hashBytes[k].ToString("x2"));


            string randomID = RandomEx.RandomString(7, 9, true, false);
            return randomID.ToLower();
            //return id.ToString();
        }

        #endregion

        #region PopulateRndDefs

        private void PopulateRndDefs()
        {
            if (_defsRnd == null)
            {
                _defsRnd = new Dictionary<char, char>();
            }
            _defsRnd.Add('d', '#');
            _defsRnd.Add('l', '*');
            _defsRnd.Add('L', '^');
            _defsRnd.Add('m', '?');
        }

        #endregion

        #region RandomFromArray

        private string RandomFromArray(string[] array)
        {
            string value = null;
            lock (array)
            {
                value = (string)array[new Random(unchecked((int)DateTime.Now.Ticks)).Next(0, array.Length - 1)];
            }
            return value;
        }

        #endregion


        #endregion

        #region Macros

        /// <summary>
        /// For internal use of macros!
        /// </summary>
        private Random _r = null;
        private Dictionary<char, char> _defsRnd = null;

        #region CheckRandom

        /// <summary>
        /// If our random field is not initialized, do so.
        /// </summary>
        private void CheckRandom()
        {
            if (_r == null)
            {
                //byte[] seed = new byte[4];
                //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                //rng.GetBytes(seed);
                //_r = new Random(BitConverter.ToInt32(seed, 0));

                _r = new Random(~unchecked((int)DateTime.Now.Ticks));
            }
        }

        #endregion

        #region Variable

        [Macro(Category = "Variable", Caption = "Current DateTime in UTC format")]
        public string DateUTC(string arg)
        {
            try
            {
                return DateTime.Now.ToUniversalTime().ToString("R");
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Macro(Category = "Variable", Caption = "Random Subject")]
        public string Subject(string arg)
        {
            try
            {
                CheckRandom();
                string subject = this[_currentMessageIndex].Subjects[_r.Next(0, this[_currentMessageIndex].Subjects.Length - 1)].ToString();
                return MacroInterpreter.Translate(subject);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Macro(Category = "Variable", Caption = "Random MailFrom")]
        public string MailFrom(string arg)
        {
            try
            {
                //CheckRandom();
                string fromEmail = this[_currentMessageIndex].FromPrefix[RandomEx.RandomInt(0, this[_currentMessageIndex].FromPrefix.Length)].ToString();
                fromEmail += "@" + _domains[RandomEx.RandomInt(0, _domains.Count - 1)].ToString();
                return MacroInterpreter.Translate(fromEmail);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Macro(Category = "Variable", Caption = "Random Domain")]
        public string FromDomain(string arg)
        {
            try
            {
                //CheckRandom();
                return this.Domains[RandomEx.RandomInt(0, this.Domains.Count - 1)].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Macro(Category = "Variable", Caption = "Random From Alias")]
        public string FromAlias(string arg)
        {
            try
            {
                //CheckRandom();
                string fromAlias = this[_currentMessageIndex].FromAlias[RandomEx.RandomInt(0, this[_currentMessageIndex].FromAlias.Length)].ToString();
                return MacroInterpreter.Translate(fromAlias);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //[Macro(Category = "Variable", Caption = "Current Message Text")]
        //public string Text(string arg)
        //{
        //    return MacroInterpreter.Translate(this[_currentMessageIndex].MessageText);
        //}

        //[Macro(Category = "Variable", Caption = "Current Message HTML")]
        //public string HTML(string arg)
        //{
        //    return MacroInterpreter.Translate(this[_currentMessageIndex].MessageHtml);
        //}

        #endregion

        #region Function

        #region Deflate methods
        
        [Macro(Category = "Function", Caption = "Deflate Compress")]
        public string DeflateCompress(string fileName)
        {

            return null;
        }

        [Macro(Category = "Function", Caption = "Deflate Decompress")]
        public string DeflateDecompress(string fileName)
        {

            return null;
        }

        #endregion

        #region Exec

        [Macro(Category = "Function", Caption = "Execute")]
        public string Exec(string args)
        {
            // Exec [filename],[arguments],([timeout]),([redirectOutput])

            string[] argArray = args.Split(',');
            if (argArray.Length < 1) return null;

            string fileName = argArray[0];
            string arguments = argArray[1];
            string timeout = argArray.Length > 2 ? argArray[2] : string.Empty;
            string redirect = argArray.Length > 3 ? argArray[3] : string.Empty;


            System.Diagnostics.ProcessStartInfo procinfo = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process proc = null;
            procinfo.ErrorDialog = false;
            procinfo.CreateNoWindow = true;
            procinfo.UseShellExecute = false;
            
            procinfo.Arguments = arguments;
            procinfo.FileName = fileName;

            if (!string.IsNullOrEmpty(redirect))
            {
                procinfo.RedirectStandardOutput = true;
                
            }

            try
            {
                proc = System.Diagnostics.Process.Start(procinfo);
                if (!string.IsNullOrEmpty(timeout))
                {
                    if (proc.WaitForExit(Convert.ToInt32(timeout)))
                    {

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (proc != null)
                {
                    proc.Close();
                    proc.Dispose();
                    proc = null;
                }
            }

            return null;
        }

        #endregion

        #region IfEmpty
        //public string IfEmpty(string arg)
        //{
        //    string tmp = arg.Split(new char[] { ',' });
        //    if (tmp.Length < 2)
        //        return null;

        //    tmp[0] = MacroInterpreter.Translate(tmp[0]);
        //    tmp[1] = MacroInterpreter.Translate(tmp[1]);

        //    if (string.IsNullOrEmpty(tmp[0]))
        //        return tmp[1];

        //    return tmp[0];
        //}

        #endregion

        #region TextToImage

        [Macro(Category = "Function", Caption = "Text to Image, returns a filename")]
        public string TextToImage(string imageText)
        {
#if (_CYCLIC_MACRO)
            imageText = MacroInterpreter.Translate(imageText);
#endif // _CYCLIC_MACRO

            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Default values, incase of parsing error.
            string fontName = "Arial";
            string emSize = "20";
            string textcolor = "#000000";
            string bgcolor = "#FFFFFF";

            // Parse attributes
            if (imageText.Contains("[font="))
            {
                fontName = ParseBetween(imageText, "[font=", " size=", 6);
                emSize = ParseBetween(imageText, "size=", "]", 5);

                if (imageText.Contains("bgcolor"))
                    bgcolor = ParseBetween(imageText, "bgcolor=", "]", 8);

                if (imageText.Contains("textcolor"))
                    textcolor = ParseBetween(imageText, "textcolor=", "]", 10);

                imageText = imageText.Substring(imageText.IndexOf("bgcolor=") + 16); // #XXXXXX] == 8 + 'bgcolor='
            }

            Font objFont = new Font(fontName, (float)Convert.ToDouble(emSize), System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            intWidth = (int)objGraphics.MeasureString(imageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(imageText, objFont).Height;

            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));
            objGraphics = Graphics.FromImage(objBmpImage);

            Color foreground = ColorTranslator.FromHtml(textcolor);
            Color background = ColorTranslator.FromHtml(bgcolor);

            // Set Background color
            objGraphics.Clear(background);
            objGraphics.SmoothingMode = SmoothingMode.HighSpeed; // AntiAlias
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(imageText, objFont, new SolidBrush(foreground), 0, 0);
            objGraphics.Flush();

            // If image output directory does not exist, create it
            string imagePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + Path.DirectorySeparatorChar + "TextToImage" + Path.DirectorySeparatorChar;
            //Console.WriteLine(imagePath);
            if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);

            // Save the bitmap
            string fnImg = imagePath + Guid.NewGuid().ToString() + ".bmp";
            objBmpImage.Save(fnImg, System.Drawing.Imaging.ImageFormat.Bmp);

            return fnImg;

            // Create an attachment from this generated image.
            //Message.Attachment attach = new Message.Attachment(fnImg);
            //attach.contentid = this.Rnd("<l10>");
            //attach.contentid = Path.GetFileNameWithoutExtension(fnImg);
            //attach.Inline = true;

            //if (_running)
            //{
            //    if (_currentMessageIndex >= 0 && _currentMessageIndex <= _messageList.Count)
            //    {
            //        this[_currentMessageIndex].EmbeddedImages.Add(attach);
            //    }
            //}

            //Console.WriteLine("<img src=\"cid:" + attach.contentid + "\">");
            //return "<img src=\"cid:" + attach.contentid + "\">";

            //Console.WriteLine("<img src='cid:" + attach.contentid + "' alt='" + attach.contentid + "' />");
            //return "<img src='cid:" + attach.contentid + "' alt='" + attach.contentid + "' />";
        }

        #endregion

        #region RndFixedLen

        /// <summary>
        /// Generate a random string.
        /// </summary>
        /// <param name="size">The size of the string to generate.</param>
        /// <returns></returns>
        [Macro(Category = "Function", Caption = "Random String")]
        public string RndFixedLen(string length)
        {
            CheckRandom();
            int size = Convert.ToInt32(length);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _r.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        #endregion

        #region RndF

        [Macro(Category = "Function", Caption = "Random line from a given text file")]
        public string RndF(string fileName)
        {
            CheckRandom();

#if (_CYCLIC_MACRO)
            fileName = MacroInterpreter.Translate(fileName);
#endif // _CYCLIC_MACRO

            try
            {
                StreamReader fIn = new StreamReader(fileName);
                string tmp = fIn.ReadToEnd();
                string[] lines = Regex.Split(tmp, "\r\n");
                tmp = null;
                fIn.Close();
                return RandomFromArray(lines);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Rnd

        [Macro(Category = "Function", Caption = "Random String")]
        public string Rnd(string characterSet)
        {
            char[] charDefs = null;
            if (characterSet.Contains("<") && characterSet.Contains(">"))
            {
                string seq = ParseBetween(characterSet, "<", ">", 1);
                if (seq != null && seq.Length > 1)
                {
                    int cnt = -1;
                    if (int.TryParse(seq.Substring(1), out cnt))
                    {

                        string lstr = characterSet.Substring(0, characterSet.IndexOf('<'));
                        string rstr = characterSet.Substring(characterSet.IndexOf('>') + 1);
                        int lstrSize = lstr.Length;
                        int rstrSize = rstr.Length;
                        charDefs = new char[cnt + lstrSize + rstrSize];

                        unsafe
                        {
                            fixed (char* charSeq = charDefs)
                            {
                                for (int j = 0; j < lstrSize; j++) *(charSeq + j) = lstr[j];
                                for (int k = lstrSize; k < cnt + lstrSize; k++) *(charSeq + k) = _defsRnd[seq[0]];
                                for (int e = 0; e < rstrSize; e++) *(charSeq + e + (cnt + lstrSize)) = rstr[e];
                            }
                        }
                    }
                }
            }

            if (charDefs == null || charDefs.Length == 0) charDefs = characterSet.ToCharArray();
            char[][] charGroups = new char[][] { RandomStringCharsLCase.ToCharArray(), RandomStringCharsUCase.ToCharArray(), RandomStringCharsNumeric.ToCharArray() };
            StringBuilder sb = new StringBuilder();

            unsafe
            {
                fixed (char* pfixed = charDefs)
                {
                    for (char* p = pfixed; *p != 0; p++)
                    {
                        switch (*p)
                        {
                            case '*':
                                sb.Append(RandomStringCharsLCase[RandomEx.RandomInt(RandomStringCharsLCase.Length)]);
                                break;
                            case '^':
                                sb.Append(RandomStringCharsUCase[RandomEx.RandomInt(RandomStringCharsUCase.Length)]);
                                break;
                            case '#':
                                sb.Append(RandomStringCharsNumeric[RandomEx.RandomInt(RandomStringCharsNumeric.Length)]);
                                break;
                            case '?':
                                int groupIndex = RandomEx.RandomInt(charGroups.Length);
                                sb.Append(charGroups[groupIndex][RandomEx.RandomInt(charGroups[groupIndex].Length)].ToString());
                                break;
                            default:
                                sb.Append(*p);
                                break;
                        }
                    }
                }
            }
            return sb.ToString();
        }

        #endregion

        #region ROT

        [Macro(Category = "Function", Caption = "Rotate Arguments")]
        public string Rot(string args)
        {
            string result = string.Empty;

            try
            {
                string[] arg = args.Split('|');
                int idx = RandomEx.RandomInt(0, arg.Length);
                result = arg[idx];
            }
            catch (Exception)
            {
                
            }

#if (_CYCLIC_MACRO)
            result = MacroInterpreter.Translate(result);
#endif // _CYCLIC_MACRO

            return result;
        }

        #endregion

        #region RandomInteger

        [Macro(Category = "Function", Caption = "Random Integer. RandomInteger(1-5) or RandomInteger(5).")]
        public string RandomInteger(string length)
        {
            int lo, hi;

            CheckRandom();

            if (length.Contains("-"))
            {
                lo = Convert.ToInt32(length.Split(new char[] { '-' })[0]);
                hi = Convert.ToInt32(length.Split(new char[] { '-' })[1]);
            }
            else
            {
                hi = Convert.ToInt32(length.ToString());
                lo = hi;
            }


            return _r.Next(lo, hi).ToString();
        }

        #endregion

        #region URL

        [Macro(Category = "Function", Caption = "Given URL with some characters are replaced with their %NN codes")]
        public static string URL(string instring)
        {
            try
            {
#if (_CYCLIC_MACRO)
                instring = MacroInterpreter.Translate(instring);
#endif // _CYCLIC_MACRO

                StringReader strRdr = new StringReader(instring);
                StringWriter strWtr = new StringWriter();
                int charValue = strRdr.Read();
                while (charValue != -1)
                {
                    if (((charValue >= 48) && (charValue <= 57)) // 0-9
                      || ((charValue >= 65) && (charValue <= 90)) // A-Z
                      || ((charValue >= 97) && (charValue <= 122))) // a-z
                    {
                        strWtr.Write((char)charValue);
                    }
                    else if (charValue == 32)  // Space
                    {
                        strWtr.Write("+");
                    }
                    else
                    {
                        strWtr.Write("%{0:x2}", charValue);
                    }

                    charValue = strRdr.Read();
                }

                return strWtr.ToString();
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion

        #region Field

        //[Macro(Category = "Function", Caption = "Retrieves current field by header name.")]
        //public string Field(string arg)
        //{
        //    try
        //    {
        //        return this._recipientList[_currentRecipientList].CsvRead[arg.ToString()].ToString();
        //    }
        //    catch (ArgumentException)
        //    {
        //        return null;
        //    }
        //}

        #endregion

        #region DownloadData

        [Macro(Category = "Function", Caption = "Download resource from uri")]
        public string DownloadData(string url)
        {
            try
            {
#if (_CYCLIC_MACRO)
                url = MacroInterpreter.Translate(url);
#endif // _CYCLIC_MACRO

                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] bytContent = wc.DownloadData(url);
                return System.Text.Encoding.Default.GetString(bytContent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Include_Base64

        [Macro(Category = "Function", Caption = "Contents of a given file in Base64")]
        public string Include_Base64(string fileName)
        {
            try
            {
#if (_CYCLIC_MACRO)
                fileName = MacroInterpreter.Translate(fileName);
#endif // _CYCLIC_MACRO

                StreamReader reader = new StreamReader(fileName);
                string tmp = reader.ReadToEnd().ToString();
                reader.Close();
                reader.Dispose();
                return MailEncoder.ConvertToBase64(tmp);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Include

        [Macro(Category = "Function", Caption = "Contents of a given text file")]
        public string Include(string fileName)
        {
            try
            {
#if (_CYCLIC_MACRO)
                fileName = MacroInterpreter.Translate(fileName);
#endif // _CYCLIC_MACRO

                StreamReader reader = new StreamReader(fileName);
                string tmp = reader.ReadToEnd().ToString();
                reader.Close();
                reader.Dispose();
                return tmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region CHR

        [Macro(Category = "Function", Caption = "Character with given ASCII code")]
        public string CHR(string asciiCode)
        {
            try
            {
                return char.ConvertFromUtf32(Convert.ToInt32(asciiCode)).ToString();
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Interfaces

        #region ICloneable members

        public object Clone()
        {
            object clone = null;

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                clone = formatter.Deserialize(stream);
            }

            return clone;
        }

        #endregion

        #region IDisposable members

#if DEBUG
        /// <summary>
        /// Contains the stack when the object was allocated.
        /// </summary>
        private System.Diagnostics.StackTrace _allocStack;
#endif

        /// <summary>
        /// Contains the disposed status flag.
        /// </summary>
        private bool _isDisposed = false;

        /// <summary>
        /// Contains the locking object for multi-threading purpose.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Occurs when the instance is disposed of.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating whether the instance has been disposed of.
        /// </summary>
        /// <value>
        /// 	<see langword="true"/> if the instance has been disposed of; otherwise, <see langword="false"/>.
        /// </value>
        [System.ComponentModel.Browsable(false)]
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// Raises the <see cref="M:Disposed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected virtual void OnDisposed(EventArgs e)
        {
            EventHandler handler = Disposed;

            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Checks if the instance has been disposed of, and if it has, throws an <see cref="T:System.ComponentModel.ObjectDisposedException"/>; otherwise, does nothing.
        /// </summary>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        /// 	The instance has been disposed of.
        /// </exception>
        /// <remarks>
        /// 	Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="M:Dispose()"/>.
        /// </remarks>
        protected void CheckDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
        }

        /// <summary>
        /// Releases all resources used by the instance.
        /// </summary>
        /// <remarks>
        /// 	Calls <see cref="M:Dispose(Boolean)"/> with the disposing parameter set to <see langword="true"/> to free unmanaged and managed resources.
        /// </remarks>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by this instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// 	<see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Refer to http://www.bluebytesoftware.com/blog/PermaLink,guid,88e62cdf-5919-4ac7-bc33-20c06ae539ae.aspx
            // Refer to http://www.gotdotnet.com/team/libraries/whitepapers/resourcemanagement/resourcemanagement.aspx

            // No exception should ever be thrown except in critical scenarios.
            // Unhandled exceptions during finalization will tear down the process.
            if (!_isDisposed)
            {
                try
                {
                    // Dispose-time code should call Dispose() on all owned objects that implement the IDisposable interface. 
                    // "owned" means objects whose lifetime is solely controlled by the container. 
                    // In cases where ownership is not as straightforward, techniques such as HandleCollector can be used.  
                    // Large managed object fields should be nulled out.

                    // Dispose-time code should also set references of all owned objects to null, after disposing them. This will allow the referenced objects to be garbage collected even if not all references to the "parent" are released. It may be a significant memory consumption win if the referenced objects are large, such as big arrays, collections, etc. 
                    if (disposing)
                    {
                        if (_messageList != null)
                        {
                            foreach (Message msg in _messageList)
                            {
                                if (msg != null)
                                {
                                    lock (_lock)
                                    {
                                        msg.Dispose();
                                    }
                                }
                            }
                            _messageList = null;
                        }

                        if (_threads != null)
                        {
                            foreach (Thread thread in _threads)
                            {
                                if (thread != null)
                                {
                                    lock (_lock)
                                    {
                                        if (thread.IsAlive)
                                        {
                                            thread.Abort();
                                        }
                                    }
                                }
                            }
                            _threads = null;
                        }


                        if (_recipientList != null)
                        {
                            foreach (RecipientProvider rp in _recipientList)
                            {
                                if (rp != null)
                                {
                                    lock (_lock)
                                    {
                                        rp.Close();
                                    }
                                }
                            }
                            _recipientList = null;
                        }

                        if (_master != null)
                        {
                            lock (_master)
                            {
                                if (_master.IsAlive)
                                {
                                    _master.Abort();
                                    _master.Join();
                                }
                            }
                        }

                        
                        _master = null;
                        _messageIdIndexes = null;
                        _servers = null;
                        _supressionFile = null;
                        _servers = null;

                    }
                }
                finally
                {
                    // Ensure that the flag is set
                    _isDisposed = true;

                    // Catch any issues about firing an event on an already disposed object.
                    try
                    {
                        OnDisposed(EventArgs.Empty);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the instance is reclaimed by garbage collection.
        /// </summary>
        ~Campaign()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("FinalizableObject was not disposed" + _allocStack.ToString());
#endif

            Dispose(false);
        }

        #endregion

        #region IEnumerable<Message> Members

        /// <summary>
        /// Returns an <see cref="T:MessageEnumerator"/>  that can iterate through Messages.
        /// </summary>
        /// <returns>An <see cref="T:MessageEnumerator"/>  that can iterate through Messages.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        public Campaign.MessageEnumerator GetEnumerator()
        {
            return new Campaign.MessageEnumerator(this);
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through Messages.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.Generics.IEnumerator"/>  that can iterate through Messages.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        IEnumerator<Message> IEnumerable<Message>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IEnumerator"/>  that can iterate through Messages.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/>  that can iterate through Messages.</returns>
        /// <exception cref="T:System.ComponentModel.ObjectDisposedException">
        ///	The instance has been disposed of.
        /// </exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #endregion

        #region class RecipientProvider

        
        /// <summary>
        /// Represents the recipient reader file reader.
        /// </summary>
        /// <remarks>
        /// When DeSerializing this, it is only useful for reading the properties.
        /// Everything else will not serialize properly!
        /// </remarks>
        [Serializable]
        public class RecipientProvider
        {

            #region Constants

            /// <summary>
            /// Defines the default index representing the recipient field.
            /// </summary>
            public const int DefaultRecipientFieldIndex = 0;

            /// <summary>
            /// Defines the default delimiter character separating each field.
            /// </summary>
            public const char DefaultDelimiter = ',';

            /// <summary>
            /// Defines the default quote character wrapping every field.
            /// </summary>
            public const char DefaultQuote = '"';

            /// <summary>
            /// Defines the default escape character letting insert quotation characters inside a quoted field.
            /// </summary>
            public const char DefaultEscape = '"';

            /// <summary>
            /// Defines the default comment character indicating that a line is commented out.
            /// </summary>
            public const char DefaultComment = '\0';

            /// <summary>
            /// Defines the default value indicating if spaces should be trimmed.
            /// </summary>
            public const bool DefaultTrimSpaces = false;

            /// <summary>
            /// Defines the default value indicating if the recipient file has headers.
            /// </summary>
            public const bool DefaultHasHeaders = false;


            #endregion

            #region Fields

            #region Settings

            private int _recipientFieldIndex; // This has to be defined. No way around it.
            private bool _hasHeaders = false;
            private char _delimiter;
            private char _quote;
            private char _escape;
            private char _comment;
            private bool _trimSpaces = false;
            private bool _supportsMultiline = true;
            private bool _skipEmptyLines = true;

            #endregion

            #region State

            private bool _eof;
            private bool _initialized;
            private string _fileName;
            private int _fieldCount;
            private string[] _fieldHeaders;
            private string[] _fields;
            private string _parts = "1";
            private long _lineCount;

            [field: NonSerialized()]
            private CsvReader _csv = null;

            [field: NonSerialized()]
            private TextReader _reader = null;

            #endregion

            #endregion

            #region Constructors

            public RecipientProvider()
                : this(null)
            {
            }

            public RecipientProvider(string fileName)
            {
                if (!string.IsNullOrEmpty(fileName))
                    _fileName = fileName;

                _fields = new string[0];
                _fieldHeaders = new string[0];

                _hasHeaders = DefaultHasHeaders;
                _delimiter = DefaultDelimiter;
                _escape = DefaultEscape;
                _comment = DefaultComment;
                _quote = DefaultQuote;
                _recipientFieldIndex = DefaultRecipientFieldIndex;
            }

            #endregion

            #region Methods

            public bool BeginRead()
            {
                if (_initialized)
                    this.Close();

                try
                {
                    _reader = new StreamReader(_fileName);
                    _csv = new CsvReader(_reader, _hasHeaders, _delimiter, _quote, _escape, _comment, _trimSpaces);

                    _csv.SupportsMultiline = _supportsMultiline;
                    _csv.SkipEmptyLines = _skipEmptyLines;
                    _csv.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine;
                    _csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;


                    if (_hasHeaders)
                    {
                        _fieldCount = _csv.FieldCount;
                        _fields = new string[_fieldCount];
                        _fieldHeaders = new string[_fieldCount];
                        _fieldHeaders = _csv.GetFieldHeaders();
                    }
                    else
                    {
                        _fieldCount = -1;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                _initialized = true;
                return true;
            }

            public pmta.Recipient ReadNext()
            {
                if (!_initialized)
                {
                    if (!BeginRead())
                        throw new ApplicationException("BeginRead() failed.");
                }
                lock (this)
                {
                    if (!_csv.EndOfStream)
                    {
                        if (_csv.ReadNextRecord())
                        {
                            pmta.Recipient rcpt = null;

                            if (_csv.MissingFieldFlag)
                            {
                                ILogger logger = ServiceScope.Get<ILogger>();
                                logger.Debug("Campaign: Missing one or more fields for the current record.");
                            }

                            if (_fieldCount != -1)
                            {
                                _fields = new string[_fieldCount];
                                _csv.CopyCurrentRecordTo(_fields);

                                try
                                {
                                    rcpt = new pmta.Recipient(_fields[_recipientFieldIndex]);
                                    for (int i = 0; i < _fieldCount; i++)
                                    {
                                        if (!String.IsNullOrEmpty(_fields[i]))
                                            rcpt[_fieldHeaders[i]] = _fields[i];

                                    }

                                    _fields = null;
                                }
                                catch (pmta.VariableSyntaxException) // Syntax error defining recipient variable
                                {
                                    // If we return null, that marks the current list as complete.
#if (DEBUG)
                                    throw;
#else
                                    return ReadNext();
#endif
                                }
                                catch (Exception)
                                {
#if (DEBUG)
                                    throw;
#else
                                    return ReadNext();
#endif
                                }
                            }
                            else
                                rcpt = new pmta.Recipient(_csv[_recipientFieldIndex].ToString());


                            //rcpt["*parts"] = _parts;
                            //rcpt.Notify = pmta.Notify.Never;

                            return rcpt;
                        }
                    }
                    else
                    {
                        _fields = null;
                        _eof = true;
                    }
                    return null;
                }
            }

            public void Close()
            {
                if (_csv != null)
                {
                    _csv.Dispose();
                    _csv = null;
                }

                if (_reader != null)
                {
                    _reader.Close();
                    _reader.Dispose();
                    _reader = null;
                }

                _initialized = false;
                _fieldCount = -1;
            }

            public void PerformLineCount()
            {
                try
                {
                    using (StreamReader sr = new StreamReader(_fileName))
                        _lineCount = countLines(sr);
                }
                catch
                {
                    _lineCount = 0;
                }

            }

            private long countLines(TextReader reader)
            {
                char[] buffer = new char[32 * 1024];

                long lines = 1; // All files have at least one line!

                int read;
                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < read; i++)
                    {
                        if (buffer[i] == '\n')
                            lines++;
                    }
                }
                return lines;
            }

            #endregion

            #region Properties

            public string[] Headers
            {
                get { return _csv.GetFieldHeaders(); }
            }

            public string[] Current
            {
                get { return _fields; }
            }

            public CsvReader CsvRead
            {
                get { return _csv; }
            }

            public char Comment
            {
                get { return _comment; }
                set { _comment = value; }
            }

            public char Delimiter
            {
                get { return _delimiter; }
                set { _delimiter = value; }
            }

            public bool Eof
            {
                get { return _eof; }
                set { _eof = value; }
            }

            public char Escape
            {
                get { return _escape; }
                set { _escape = value; }
            }

            public string FileName
            {
                get { return _fileName; }
                set
                {
                    if (!File.Exists(value))
                        throw new FileNotFoundException("FileName", value);

                    _fileName = value;
                }
            }

            public bool HasHeaders
            {
                get { return _hasHeaders; }
                set { _hasHeaders = value; }
            }

            public long LineCount
            {
                get { return _lineCount; }
            }

            public string Parts
            {
                get { return _parts; }
                set { _parts = value; }
            }

            public char Quote
            {
                get { return _quote; }
                set { _quote = value; }
            }

            public int RecipientFieldIndex
            {
                get { return _recipientFieldIndex; }
                set { _recipientFieldIndex = value; }
            }

            public bool SkipEmptyLines
            {
                get { return _skipEmptyLines; }
                set { _skipEmptyLines = value; }
            }

            public bool SupportsMultiline
            {
                get { return _supportsMultiline; }
                set { _supportsMultiline = value; }
            }

            public bool TrimSpaces
            {
                get { return _trimSpaces; }
                set { _trimSpaces = value; }
            }


            #endregion

        }

        #endregion

        #region class Server

        //[Serializable]
        //public class Server : IDisposable
        //{
        //    private string _host = string.Empty;
        //    private int _port = 25;
        //    private string _username = string.Empty;
        //    private string _password = string.Empty;
        //    private Guid _guid = Guid.Empty;

        //    // State
        //    [NonSerialized]
        //    private Session _session = null;

        //    [NonSerialized]
        //    private bool _initialized = false;

        //    public Server(string host, int port)
        //        : this(host, port, null, null)
        //    {
        //    }

        //    public Server(string host, int port, string username, string password)
        //        : this(host, port, username, password, Guid.Empty)
        //    {
        //    }

        //    public Server(string host, int port, string username, string password, Guid guid)
        //    {
        //        _host = host;
        //        _port = port;
        //        _username = username;
        //        _password = password;
        //        _guid = guid;
                
        //        if (guid == null || guid == Guid.Empty)
        //            _guid = Guid.NewGuid();

        //    }

        //    public void BeginSession()
        //    {
        //        _session = new Session(this);
        //        _initialized = true;
        //    }

        //    #region Properties Implementation

        //    public bool Initialized
        //    {
        //        get { return _initialized; }
        //    }

        //    public Session Session
        //    {
        //        get { return _session; }
        //    }

        //    public Guid Guid
        //    {
        //        get { return _guid; }
        //        set { _guid = value; }
        //    }

        //    public string Host
        //    {
        //        get { return _host; }
        //    }

        //    public int Port
        //    {
        //        get { return _port; }
        //    }

        //    public string Username
        //    {
        //        get { return _username; }
        //        set { _username = value; }
        //    }

        //    public string Password
        //    {
        //        get { return _password; }
        //        set { _password = value; }
        //    }

        //    #endregion

        //    #region IDisposable Members

        //    public void Dispose()
        //    {
        //        _host = null;
        //    }

        //    #endregion
        //}

        #endregion

        #region struct WorkItem

            //if DEBUG
            //        public class WorkItem : IWork
            //else
            //        public struct WorkItem : IWork 
            //endif
        public struct WorkItem //: IWork
        {

            // Using struct avoids a heap allocation.
            // debug: class so that we can add a finalizer in order to detect when the object is not freed.)

            #region Fields

            private pmta.Connection _connection;
            private Campaign _campaign;
            private string _description;
            private Exception _exception;

            #endregion

            #region Constructors

            ///// <summary>
            ///// Initializes a new instance of the WorkItem class.
            ///// </summary>
            ///// <param name="campaign">The owner of this class.</param>
            //public WorkItem(Campaign campaign)
            //    : this(campaign, null)
            //{
            //}

            ///// <summary>
            ///// Initializes a new instance of the WorkItem class.
            ///// </summary>
            ///// <param name="campaign">The owner of this class.</param>
            ///// <param name="connection">The Connection to use.</param>
            //public WorkItem(Campaign campaign, pmta.Connection connection)
            //{
            //    //if (campaign == null)
            //    //    throw new ArgumentNullException("parent");

            //    _random = new Random(unchecked((int)DateTime.Now.Ticks));
            //    _connection = connection;
            //}

            #endregion

            #region Properties

            public Campaign Campaign
            {
                get { return _campaign; }
                set { _campaign = value; }
            }

            public pmta.Connection Connection
            {
                get { return _connection; }
                set { _connection = value; }
            }

            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }

            public Exception Exception
            {
                get { return _exception; }
                set { _exception = value; }
            }

            #endregion

            #region Methods

            public void Process()
            {

                if (_connection == null) 
                    return;

                pmta.Recipient rcpt;

                while (!_campaign._eor && (rcpt = _campaign._recipientList[_campaign._currentRecipientList].ReadNext()) != null)
                {
                    if (_campaign._rotateMessages)
                    {
                        if (_campaign._rotateMessageCondition == RotateMessageCondition.Minutes)
                        {
                            _campaign._nextCheckRotateMinutes--;
                            if (_campaign._nextCheckRotateMinutes <= 0)
                            {
                                _campaign._nextCheckRotateMinutes = DefaultRotateMinuteCheckInterval;
                                TimeSpan msgTime = new DateTime(DateTime.Now.Ticks).Subtract(new DateTime(_campaign[_campaign._currentMessageIndex].LastRotateTime));

                                if (msgTime.Minutes >= _campaign._rotateMessageInterval)
                                    _campaign.RotateToRandomMessage();
                            }
                        }
                        else // Emails Processed.
                        {
                            _campaign._nextRotate--;
                            if (_campaign._nextRotate <= 0) _campaign.RotateToRandomMessage();
                        }
                    }
                    else
                        _campaign._currentMessageIndex = RandomEx.RandomInt(0, _campaign._messageList.Count - 1);

                    int rcptCount = 0;
                    pmta.Message msg = _campaign.BuildMessage(rcpt, out rcptCount);
                    if (!String.IsNullOrEmpty(_campaign._vmtaPool)) msg.VirtualMTA = _campaign._vmtaPool;

                    // used to add extra recipients here, moved to BuildMessage().


                    // Copy recipient data for our seeds, then add to queue.

                    //if (_campaign._sendSeeds && _campaign._seedInterval > 0)
                    //{
                    //    //_campaign._nextSeedIndex = (_campaign._nextSeedIndex + _campaign._seedInterval - rcptCount) % _campaign._seedInterval;
                    //    _campaign._nextSeedIndex -= rcptCount;

                    //    if (_campaign._nextSeedIndex < 0)
                    //    {
                    //        _campaign._nextSeedIndex = _campaign._seedInterval;
                    //        _campaign._campaignStats.Processedseed += (uint)_campaign.Seeds.LongLength;
                    //        _campaign.InsertSeedData(ref msg);
                    //    }
                    //}

                    /** Submit the message. */

                    int retryLimit = 5;
                    bool connectionExpired = false;

                    while (retryLimit > 0)
                    {
                        Exception x = null;

                        try
                        {
                            _connection.Submit(msg);
                        }
                        catch (pmta.ServiceException e1)
                        {
                            x = e1;
                            //ErrorEx.DumpError(e1, new System.Diagnostics.StackTrace());
                        }
                        catch (System.Net.Sockets.SocketException e2)
                        {
                            x = e2;

                            // System.Net.Sockets.SocketException: No connection could be made because the target machine actively refused it 68.169.18.3:2525
                        }
                        catch (System.IO.IOException e3)
                        {
                            x = e3;

                            connectionExpired = true;
                            // Unable to write data to the transport connection: An existing connection was forcibly closed by the remote host.
                        }
                        catch (InvalidOperationException e4)
                        {
                            x = e4;

                            connectionExpired = e4.Message.Contains("The operation is not allowed on non-connected sockets.");
                        }
                        catch (ThreadAbortException)
                        {
                            _connection = null;
                            return;
                        }
                        catch (Exception e5)
                        {
                            ErrorEx.DumpError(e5, new System.Diagnostics.StackTrace());
                        }
                        finally
                        {
                            if (x == null)
                            {
                                if (retryLimit <= 0)
                                    _campaign._campaignStats.Failed++;

                                retryLimit = 0;
                            }
                            else
                            {
                                retryLimit--;
                            }
                        }

                        if (connectionExpired)
                        {
                            //_connection.Close();
                            _connection = null;
                            return;
                        }
                    }
                }

                lock (_campaign._recipientList)
                {
                    _campaign._recipientList[_campaign._currentRecipientList].Eof = true;
                }
            }

            #endregion

            #region IDisposable Members

            ///// <summary>
            ///// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            ///// </summary>
            //public void Dispose()
            //{
            //    _campaign = null;
            //    _random = null;
            //}

            #endregion

        }

        #endregion

        #region SendThread

        #region Fields

        private string _mixedBoundary;
        private string _altBoundary;
        private string _relatedBoundary;
        private string _charset = "ISO-8859-1"; //"us-ascii";
        private object _queueMutex = new object();

        [field: NonSerialized()]
        private pmta.Connection _con = null;

        /// <summary>
        /// Should only be for use with SendThread methods!
        /// </summary>
        [field: NonSerialized()]
        private Random _random = null;

        #endregion

        #region Constructors

        public void SendThread(object data) //object data
        {
            _random = new Random(Environment.TickCount);
            _con = (pmta.Connection)data;

            _mixedBoundary = generateMixedMimeBoundary();
            _altBoundary = generateAltMimeBoundary();
            _relatedBoundary = generateRelatedMimeBoundary();

            Run();
        }

        #endregion

        #region Run

        private void Run()
        {
            //pmta.Recipient r;
            //while (!_eor && (r = _recipientList[_currentRecipientList].ReadNext()) != null)
            //{
            //    if (_rotateMessages)
            //    {
            //        if (_rotateMessageCondition == RotateMessageCondition.Minutes)
            //        {
            //            _nextCheckRotateMinutes--;
            //            if (_nextCheckRotateMinutes <= 0)
            //            {
            //                _nextCheckRotateMinutes = DefaultRotateMinuteCheckInterval;
            //                TimeSpan elapsed = new DateTime(DateTime.Now.Ticks).Subtract(new DateTime(this[_currentMessageIndex].LastRotateTime));
            //                if (elapsed.Minutes >= _rotateMessageInterval)
            //                {
            //                    lock (_queueMutex)
            //                    {
            //                        RotateToRandomMessage();
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            _nextRotate--;
            //            if (_nextRotate <= 0)
            //            {
            //                lock (_queueMutex)
            //                {
            //                    RotateToRandomMessage();
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        lock (_queueMutex)
            //        {
            //            _currentMessageIndex = _random.Next(0, _messageList.Count);
            //        }
            //    }

            //    pmta.Message msg = BuildMessage(r);

            //    // Additional message options added
            //    msg.VirtualMTA = _vmtaPool;

            //    /*
            //     * TODO: Add setting for this option.
            //     * 
            //     * PowerMTA will encode
            //     * both the originator and recipient addresses in the originator address used for delivering
            //     * the message. This may make tracking any bounces easier, since you can always derive
            //     * the original addresses from the address where the bounce is sent.
            //     */

            //    msg.Verp = true;


            //    // Add extra recipients to this message.
            //    if (_maxSessionEmails > 0)
            //    {
            //        try
            //        {
            //            AddRecipients(new Random(unchecked((int)DateTime.Now.Ticks)).Next(_minSessionEmails, _maxSessionEmails), msg);
            //        }
            //        catch (EndOfStreamException)
            //        {
            //            break; // end of stream.
            //        }
            //        catch (Exception exc)
            //        {
            //            throw new Exception(exc.Message, exc);
            //        }
            //    }


            //    // Copy recipient data for our seeds, then add to queue.
            //    if (_sendSeeds && _seedInterval > 0)
            //    {
            //        _nextSeedIndex--;
            //        if (_nextSeedIndex <= 0)
            //        {
            //            lock (_queueMutex)
            //            {
            //                _nextSeedIndex = _seedInterval;
            //                InsertSeedData(msg);
            //            }
            //        }
            //    }

            //    // Submit the message.
            //    try
            //    {
            //        _campaignStats.Processed++;
            //        _con.Submit(msg);
            //    }
            //    catch (Exception exc)
            //    {
            //        Debug.WriteLine(exc.Message.ToLower());
            //    }
            //}

            //lock (_queueMutex)
            //{
            //    _recipientList[_currentRecipientList].Eof = true;
            //}
        }

        #endregion

        #region AddRecipients

        /// <summary>
        /// Add recipients to the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="count">Number of recipients to add.</param>
        /// <param name="message">Message to add recipients to.</param>
        /// <exception cref="T:EndOfStreamException">
        ///     Cannot read from <see cref="T:RecipientProvider"/>.
        /// </exception>
        private void AddRecipients(int count, pmta.Message message)
        {
            for (int i = 0; i < count; i++)
            {
                pmta.Recipient rcpt = _recipientList[_currentRecipientList].ReadNext();
                if (rcpt != null)
                {
                    try
                    {
                        message.AddRecipient(rcpt);
                        _campaignStats.Processed++;
                        rcpt = null;
                    }
                    catch (Exception ex)
                    {
                        ErrorEx.DumpError(ex, new System.Diagnostics.StackTrace());
                    }
                }
            }
        }

        #endregion

        #region InsertSeedData

        /// <summary>
        /// Insert seed data into specified message.
        /// </summary>
        /// <param name="message">The message to add the seeds.</param>
        private void InsertSeedData(ref pmta.Message message)
        {
            string[] headers = null;
            string[] fields = null;

            if (_recipientList[_currentRecipientList].HasHeaders)
            {
                headers = new string[_recipientList[_currentRecipientList].CsvRead.FieldCount];
                headers = _recipientList[_currentRecipientList].CsvRead.GetFieldHeaders();
                fields = new string[_recipientList[_currentRecipientList].CsvRead.FieldCount];
                _recipientList[_currentRecipientList].CsvRead.CopyCurrentRecordTo(fields);
            }

            foreach (string seed in _seeds)
            {
                pmta.Recipient r = new pmta.Recipient(seed);

                if (_recipientList[_currentRecipientList].HasHeaders)
                {
                    for (int i = 0; i < headers.Length; i++)
                        r[headers[i]] = fields[i];
                }

                r["*parts"] = _recipientList[_currentRecipientList].Parts;
                //r.Notify = pmta.Notify.Never;

                message.AddRecipient(r);
            }
        }

        #endregion


        #region BuildMessageSourceTemplate

        /// <summary>
        /// Builds message source template of specified message index.
        /// </summary>
        /// <param name="messageIndex">Index in message list.</param>
        /// <returns></returns>
        public string BuildMessageSourceTemplate(int messageIndex)
        {


            return null;
        }


        #endregion

        #region BuildMessageSource

        /// <summary>
        /// Used for design-time generation of message source.
        /// </summary>
        /// <param name="messageIndex">Message to generate source for.</param>
        /// <returns></returns>
        public string BuildMessageSource(int messageIndex)
        {
            Message msg = this[_currentMessageIndex];

            string fromEmail = this.MailFrom(null);
            string fromAlias = this.FromAlias(null);
            string fromDomain = fromEmail.Split('@')[1];
            int subjectIdx = RandomEx.RandomInt(msg.Subjects.Length);
            string subject = MacroInterpreter.Translate(msg.Subjects[subjectIdx]);

            // We'll add an option for this later on, but for now
            // our default value is 8bit and for bcc quotedprintable.
            string mimeEncoding = _recipientField == RecipientField.BCC ? MIME_TransferEncodings.QuotedPrintable : MIME_TransferEncodings.EightBit;

            /* DWQRPOQWKROQKWR */
            mimeEncoding = MIME_TransferEncodings.EightBit;

            Encoding iso_8859_1 = Encoding.GetEncoding("iso-8859-1");
            Encoding utf_8 = System.Text.Encoding.UTF8;

            //--- headers -------------------------------------------------------------------------------------------------------
            Mail_Message mmsg = new Mail_Message();
            mmsg.MimeVersion = "1.0";
            mmsg.From = new Mail_t_MailboxList();
            mmsg.From.Add(new Mail_t_Mailbox(fromAlias, fromEmail));

            if (_recipientField != RecipientField.BCC)
            {
                Mail_t_AddressList addrList = new Mail_t_AddressList();
                addrList.Add(new Mail_t_Mailbox("Hi Thre", "ow@domain.com"));
                mmsg.Header.Add(new Mail_h_AddressList("To", addrList));



                //mmsg.To = new Mail_h_AddressList("To", null);
                //mmsg.To.Add(new Mail_t_Mailbox("[*to]", "[*to]"));
                //mmsg.To.Add(new Mail_t_Mailbox("Hi There", "blah@domain.com"));
            }
            else
            {
                // If we don't add this header, we'll have in the headers 'undisclosed recipients'
                string msgTo = string.Format("{0}@{1}", Guid.NewGuid().ToString().Replace("-", "").Substring(16), fromDomain);
                mmsg.To = new Mail_t_AddressList();
                mmsg.To.Add(new Mail_t_Mailbox(null, msgTo));
            }

            MIME_Encoding_EncodedWord encodedWord = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.Q, iso_8859_1);
            
            //mmsg.Subject = encodedWord.Encode(subject);
            mmsg.Date = DateTime.Now;
            // If we add msgid, pmta won't overwrite
            mmsg.MessageID = MIME_Utils.CreateMessageID();

            //--- multipart/alternative -----------------------------------------------------------------------------------------
            MIME_h_ContentType contentType_multipartAlternative = new MIME_h_ContentType(MIME_MediaTypes.Multipart.alternative);
            contentType_multipartAlternative.Param_Boundary = Guid.NewGuid().ToString().Replace('-', '.');
            MIME_b_MultipartAlternative multipartAlternative = new MIME_b_MultipartAlternative(contentType_multipartAlternative);
            //mmsg.Body = multipartAlternative;

            //--- text/plain ----------------------------------------------------------------------------------------------------
            MIME_Entity entity_text_plain = new MIME_Entity();
            MIME_b_Text text_plain = new MIME_b_Text(MIME_MediaTypes.Text.plain);
            entity_text_plain.Body = text_plain;
            text_plain.SetText(mimeEncoding, Encoding.GetEncoding("ISO-8859-1"), MacroInterpreter.Translate(msg.MessageText));
            multipartAlternative.BodyParts.Add(entity_text_plain);

            //--- text/html ------------------------------------------------------------------------------------------------------
            MIME_Entity entity_text_html = new MIME_Entity();
            MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
            entity_text_html.Body = text_html;
            text_html.SetText(mimeEncoding, iso_8859_1, MacroInterpreter.Translate(msg.MessageHtml));
            //multipartAlternative.BodyParts.Add(entity_text_html);

            mmsg.Body = text_html;
            //mmsg.ContentTransferEncoding = mimeEncoding;


            using (MemoryStream ms = new MemoryStream())
            {
                //MIME_Encoding_EncodedWord encodedWord = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.Q, Encoding.GetEncoding("iso-8859-1"));
                mmsg.ToStream(ms, encodedWord, Encoding.GetEncoding("iso-8859-1"));
                mmsg.ToFile("D:\\MAIL.EML", new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.Q, Encoding.GetEncoding("iso-8859-1")), Encoding.GetEncoding("iso-8859-1"));
                
                
                byte[] memBytes = ms.ToArray();
                string cleanStr = Encoding.UTF8.GetString(memBytes);
                return cleanStr;
                //return ms.ToString();
            }
        }

        #endregion

        #region BuildMessage

        private pmta.Message BuildMessage(pmta.Recipient recipient, out int rcptCount)
        {
            Message msg = this[_currentMessageIndex];

            string fromEmail = this.MailFrom(null);
            string fromAlias = this.FromAlias(null);
            string fromDomain = fromEmail.Split('@')[1];
            int subjectIdx = RandomEx.RandomInt(msg.Subjects.Length);
            string subject = MacroInterpreter.Translate(msg.Subjects[subjectIdx]);

            MIME_Encoding_EncodedWord mimeEncode = new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.B, Encoding.GetEncoding("ISO-8859-1"));

            // We'll add an option for this later on, but for now
            // our default value is 8bit and for bcc quotedprintable.
            string mimeEncoding = _recipientField == RecipientField.BCC ? MIME_TransferEncodings.QuotedPrintable : MIME_TransferEncodings.EightBit;

            /* DWQRPOQWKROQKWR */
            mimeEncoding = MIME_TransferEncodings.Base64;

            if (_recipientField != RecipientField.BCC)
            {
                //--- mail-merge ----------------------------------------------------------------------------------------------------
                recipient["user_id"] = string.Format("{0:x8}", recipient.EmailAddress.GetHashCode());
                recipient["user_email"] = recipient.EmailAddress;
                recipient["camp_id"] = this._id;
                recipient["list_id"] = string.Format("{0:x8}", _recipientList[_currentRecipientList].FileName.GetHashCode());
                recipient["msg_id"] = this[_currentMessageIndex].ID;
                recipient["subj_id"] = subjectIdx.ToString();
                recipient["fromdomain"] = fromDomain;
                recipient["*parts"] = "1";
            }

            //--- headers -------------------------------------------------------------------------------------------------------
            Mail_Message mmsg = new Mail_Message();
            mmsg.MimeVersion = "1.0";
            mmsg.From = new Mail_t_MailboxList();
            mmsg.From.Add(new Mail_t_Mailbox(fromAlias, fromEmail));
            //mmsg.Header.Add(new MIME_h_Unstructured("From", new Mail_t_Mailbox(fromAlias, fromEmail).ToString(mimeEncode)));

            if (_recipientField != RecipientField.BCC)
            {
                mmsg.To = new Mail_t_AddressList();
                mmsg.To.Add(new Mail_t_Mailbox("[*to]", "[*to]"));
            }
            else
            {
                // If we don't add this header, we'll have in the headers 'undisclosed recipients'
                string msgTo = string.Format("{0}@{1}", Guid.NewGuid().ToString().Replace("-", "").Substring(16), fromDomain);
                mmsg.To = new Mail_t_AddressList();
                mmsg.To.Add(new Mail_t_Mailbox(null, msgTo));
            }


            //mmsg.Subject = MIME_Encoding_EncodedWord.EncodeS(MIME_EncodedWordEncoding.B, Encoding.UTF8, subject);
            mmsg.Subject = mimeEncode.Encode(subject);
            mmsg.Date = DateTime.Now;
            mmsg.MessageID = MIME_Utils.CreateMessageID();

            byte[] byteHtml = Encoding.UTF8.GetBytes(MacroInterpreter.Translate(msg.MessageHtml));
            string b64Html = System.Convert.ToBase64String(byteHtml); //Injector.Net.IO.Base64Stream

            string trackboundary = Guid.NewGuid().ToString().Replace('-', '.');
            trackboundary = trackboundary.Remove(trackboundary.LastIndexOf('.'), trackboundary.Length) + "[user_id]";

            //--- multipart/alternative -----------------------------------------------------------------------------------------
            MIME_h_ContentType contentType_multipartAlternative = new MIME_h_ContentType(MIME_MediaTypes.Multipart.alternative);
            contentType_multipartAlternative.Param_Boundary = trackboundary; //Guid.NewGuid().ToString().Replace('-', '.');
            MIME_b_MultipartAlternative multipartAlternative = new MIME_b_MultipartAlternative(contentType_multipartAlternative);
            mmsg.Body = multipartAlternative;

            //--- text/plain ----------------------------------------------------------------------------------------------------
            MIME_Entity entity_text_plain = new MIME_Entity();
            MIME_b_Text text_plain = new MIME_b_Text(MIME_MediaTypes.Text.plain);
            entity_text_plain.Body = text_plain;
            text_plain.SetText(mimeEncoding, Encoding.GetEncoding("ISO-8859-1"), MacroInterpreter.Translate(msg.MessageText));
            multipartAlternative.BodyParts.Add(entity_text_plain);

            //--- text/html ------------------------------------------------------------------------------------------------------
            MIME_Entity entity_text_html = new MIME_Entity();
            MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
            entity_text_html.Body = text_html;
            text_html.SetText(mimeEncoding, Encoding.GetEncoding("ISO-8859-1"), b64Html);
            multipartAlternative.BodyParts.Add(entity_text_html);

            //Encoding.GetEncoding("ISO-8859-1")

            //mmsg.Body = text_html;
            //mmsg.ContentTransferEncoding = mimeEncoding;

            pmta.Message pmsg = new pmta.Message(fromEmail);
            pmsg.AddRecipient(recipient);
            _campaignStats.Processed++;
            rcptCount = 1;

            if (_maxSessionEmails > 0)
            {
                pmta.Recipient rcpt;

                int sessionMax;
                int sessionCur = 0;

                if (_minSessionEmails == _maxSessionEmails)
                    sessionMax = _maxSessionEmails;
                else
                    sessionMax = RandomEx.RandomInt(_minSessionEmails, _maxSessionEmails);

                sessionMax--; // Recipient added previously.

                try
                {
                    while ((rcpt = _recipientList[_currentRecipientList].ReadNext()) != null)
                    {
                        sessionCur++;

                        if (sessionCur > sessionMax)
                            break;

                        if (_recipientField != RecipientField.BCC)
                        {
                            //--- mail-merge ----------------------------------------------------------------------------------------------------
                            rcpt["user_id"] = string.Format("{0:x8}", rcpt.EmailAddress.GetHashCode());
                            rcpt["user_email"] = rcpt.EmailAddress;
                            rcpt["camp_id"] = _id;
                            rcpt["list_id"] = string.Format("{0:x8}", _recipientList[_currentRecipientList].FileName.GetHashCode());
                            rcpt["msg_id"] = this[_currentMessageIndex].ID;
                            rcpt["subj_id"] = subjectIdx.ToString();
                            rcpt["fromdomain"] = fromDomain;
                            rcpt["*parts"] = "1";
                        }

                        if (rcpt != null)
                        {
                            _campaignStats.Processed++;
                            pmsg.AddRecipient(rcpt);
                            rcptCount++;
                            //rcpt = null;
                        }

                        //--- seed ----------------------------------------------------------------------------------------------------------
                        if (_sendSeeds && _seedInterval > 0)
                        {
                            _nextSeedIndex--;
                            if (_nextSeedIndex < 0)
                            {
                                _nextSeedIndex = _seedInterval;
                                _campaignStats.Processedseed += (uint)_seeds.LongLength;

                                foreach (string seed in _seeds)
                                {
                                    pmta.Recipient rcptSeed = new port25.pmta.api.submitter.Recipient(seed);
                                    if (_recipientField != RecipientField.BCC)
                                    {
                                        rcptSeed["user_id"] = string.Format("{0:x8}", rcptSeed.EmailAddress.GetHashCode());
                                        rcptSeed["user_email"] = rcptSeed.EmailAddress;
                                        rcptSeed["camp_id"] = _id;
                                        rcptSeed["list_id"] = string.Format("{0:x8}", _recipientList[_currentRecipientList].FileName.GetHashCode());
                                        rcptSeed["msg_id"] = this[_currentMessageIndex].ID;
                                        rcptSeed["subj_id"] = subjectIdx.ToString();
                                        rcptSeed["fromdomain"] = fromDomain;
                                        rcptSeed["*parts"] = "1";
                                    }

                                    pmsg.AddRecipient(rcptSeed);
                                    //rcptSeed = null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorEx.DumpError(ex, new System.Diagnostics.StackTrace());
                    //rcpt = null;
                }
            }

            // automatically handled?
            //pmsg.Encoding = port25.pmta.api.submitter.Encoding.EightBit;
            
            using (MemoryStream ms = new MemoryStream())
            {
                mmsg.ToStream(ms, mimeEncode, Encoding.GetEncoding("iso-8859-1"));
                //mmsg.ToFile("D:\\test.txt", mimeEncode, Encoding.UTF8);
                
                //string temp = mmsg.ToString();
                //Console.WriteLine(temp);
                //Console.WriteLine("....");

                if (_recipientField != RecipientField.BCC)
                {
                    pmsg.AddMergeData(ms.ToArray());
                }
                else
                {
                    //pmsg.Encoding = port25.pmta.api.submitter.Encoding.Base64;
                    pmsg.AddData(ms.ToArray());
                }
            }

            return pmsg;
        }

        #endregion

        #region AddData

        private void AddData(pmta.Message msg, String data)
        {
            AddData(msg, data, null);
        }

        private void AddData(pmta.Message msg, String data, string encodingName)
        {
            try
            {
                if (string.IsNullOrEmpty(encodingName))
                {
                    msg.AddData(data);
                    return;
                }


                EncoderReplacementFallback erf = new EncoderReplacementFallback("(unknown)");
                DecoderReplacementFallback drf = new DecoderReplacementFallback("(error)");
                Encoding encoding = Encoding.GetEncoding(encodingName, erf, drf);
                msg.AddData(encoding.GetBytes(data));

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GetMessageBody

        /// <summary>
        /// Determines the format of the message and adds the
        /// appropriate mime containers.
        /// 
        /// This will return the html and/or text and/or 
        /// embedded images and/or attachments.
        /// </summary>
        /// <returns></returns>
        private string GetMessageBody()
        {
            StringBuilder sb = new StringBuilder();

            if (this[_currentMessageIndex].Attachments.Count > 0)
            {
                sb.Append("Content-Type: multipart/mixed; ");
                sb.Append("boundary=\"" + _mixedBoundary + "\"");
                sb.Append("\r\n\r\nThis is a multi-part message in MIME format.");
                sb.Append("\r\n\r\n--" + _mixedBoundary + "\r\n");
            }

            sb.Append(GetInnerMessageBody());

            if (this[_currentMessageIndex].Attachments.Count > 0)
            {
                Console.WriteLine("Adding attachment...");
                foreach (Message.Attachment attachment in this[_currentMessageIndex].Attachments)
                {
                    sb.Append("\r\n\r\n--" + _mixedBoundary + "\r\n");
                    sb.Append(attachment.ToMime());
                }
                sb.Append("\r\n\r\n--" + _mixedBoundary + "--\r\n");
            }
            return sb.ToString();
        }

        #endregion

        #region GetInnerMessageBody

        /// <summary>
        /// Get the html and/or text and/or images.
        /// </summary>
        /// <returns></returns>
        private string GetInnerMessageBody()
        {
            StringBuilder sb = new StringBuilder();
            if (this[_currentMessageIndex].EmbeddedImages.Count > 0)
            {
                sb.Append("Content-Type: multipart/related; ");
                sb.Append("boundary=\"" + _relatedBoundary + "\"");
                sb.Append("\r\n\r\n--" + _relatedBoundary + "\r\n");

            }

            sb.Append(GetReadableMessageBody());

            if (this[_currentMessageIndex].EmbeddedImages.Count > 0)
            {
                foreach (Message.Attachment embed in this[_currentMessageIndex].EmbeddedImages)
                {
                    if (this[_currentMessageIndex].MessageHtml.Contains(embed.contentid))
                    {
                        sb.Append("\r\n\r\n--" + _relatedBoundary + "\r\n");
                        sb.Append(embed.ToMime());
                    }
                }
                sb.Append("\r\n\r\n--" + _relatedBoundary + "--\r\n");
            }

            if (this[_currentMessageIndex].Images.Count > 0)
            {
                foreach (Message.Attachment image in this[_currentMessageIndex].Images)
                {
                    sb.Append("\r\n\r\n--" + _relatedBoundary + "\r\n");
                    sb.Append(image.ToMime());
                }
                sb.Append("\r\n\r\n--" + _relatedBoundary + "--\r\n");
            }

            return sb.ToString();
        }

        #endregion

        #region GetReadableMessageBody

        private string GetReadableMessageBody()
        {

            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(this[_currentMessageIndex].MessageHtml))
            {
                sb.Append(GetTextMessageBody(this[_currentMessageIndex].MessageText, "text/plain"));
            }
            else if (string.IsNullOrEmpty(this[_currentMessageIndex].MessageText))
            {
                sb.Append(GetTextMessageBody(this[_currentMessageIndex].MessageHtml, "text/html"));
            }
            else
            {
                sb.Append(GetAltMessageBody(
                    GetTextMessageBody(this[_currentMessageIndex].MessageText, "text/plain"),
                    GetTextMessageBody(this[_currentMessageIndex].MessageHtml, "text/html")));
            }

            return sb.ToString();
        }

        private string GetReadableMessageBody(string text, string html)
        {

            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrEmpty(html))
            {
                sb.Append(GetTextMessageBody(text, "text/plain"));
            }
            else if (string.IsNullOrEmpty(text))
            {
                sb.Append(GetTextMessageBody(text, "text/html"));
            }
            else
            {
                sb.Append(GetAltMessageBody(
                    GetTextMessageBody(text, "text/plain"),
                    GetTextMessageBody(html, "text/html")));
            }

            return sb.ToString();

        }


        #endregion

        #region GetTextMessageBody

        private string GetTextMessageBody(string messageBody, string textType)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append("Content-Type: " + textType + ";\r\n");
            //sb.Append(" charset=\"" + _charset + "\"\r\n");
            sb.Append("Content-Type: " + textType + "; ");
            sb.Append("charset=\"" + _charset + "\"\r\n");

            sb.Append("Content-Transfer-Encoding: quoted-printable\r\n\r\n");
            sb.Append(MailEncoder.ConvertToQP(MacroInterpreter.Translate(messageBody), _charset));
            return sb.ToString();
        }

        #endregion

        #region GetFileAsString

        private string GetFileAsString(string filePath)
        {
            FileStream fin = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] bin = new byte[fin.Length];
            long rdlen = 0;
            int len;

            StringBuilder sb = new StringBuilder();

            while (rdlen < fin.Length)
            {
                len = fin.Read(bin, 0, (int)fin.Length);
                sb.Append(Encoding.UTF7.GetString(bin, 0, len));
                rdlen = (rdlen + len);
            }

            fin.Close();
            return sb.ToString();
        }

        #endregion

        #region GetAltMessageBody

        private string GetAltMessageBody(string messageBody1, string messageBody2)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Content-Type: multipart/alternative; ");
            sb.Append("boundary=\"" + _altBoundary + "\"");

            sb.Append("\r\n\r\nThis is a multi-part message in MIME format.");

            sb.Append("\r\n\r\n--" + _altBoundary + "\r\n");
            sb.Append(messageBody1);
            sb.Append("\r\n\r\n--" + _altBoundary + "\r\n");
            sb.Append(messageBody2);
            sb.Append("\r\n\r\n--" + _altBoundary + "--\r\n");

            return sb.ToString();
        }

        #endregion

        #region Generate Boundary

        private static string generateMixedMimeBoundary()
        {
            return "Part." + Convert.ToString(new Random(unchecked((int)DateTime.Now.Ticks)).Next()) + "." + Convert.ToString(new Random(~unchecked((int)DateTime.Now.Ticks)).Next());
        }

        private static string generateAltMimeBoundary()
        {
            return "Part." + Convert.ToString(new Random(~unchecked((int)DateTime.Now.AddDays(1).Ticks)).Next()) + "." + Convert.ToString(new Random(unchecked((int)DateTime.Now.AddDays(1).Ticks)).Next());
        }

        private static string generateRelatedMimeBoundary()
        {
            return "Part." + Convert.ToString(new Random(~unchecked((int)DateTime.Now.AddDays(2).Ticks)).Next()) + "." + Convert.ToString(new Random(unchecked((int)DateTime.Now.AddDays(1).Ticks)).Next());
        }

        #endregion

        #region GetEmbeddedImage

        private string GetEmbeddedImage(Message.Attachment img, string boundary)
        {
#if (DEBUG)
            Console.WriteLine("GetEmbeddedImage()\r\nFilePath:{0}\r\nMimeType:{1}", img.filePath, img.mimeType);
#endif

            //StringBuilder header = new StringBuilder();

            //header.Append("\r\r\n");
            //header.Append("\r\r\n--" + boundary + "\r\r\n");
            //header.Append("Content-ID: <" + img.contentid + ">\r\r\n");
            //header.Append("Content-Type: " + img.mimeType + "\r\r\n");
            //header.Append("Content-Transfer-Encoding: base64\r\r\n");
            //header.Append("Content-Disposition: inline; filename=" + img.name + "\r\r\n\r\r\n");

            StringBuilder data = new StringBuilder();

            //FileStream fin = new FileStream(img.encodedFilePath, FileMode.Open, FileAccess.Read);
            //byte[] bin;

            //try
            //{

            //    fs = new FileStream(img.filePath, FileMode.Open, FileAccess.Read,
            //                                                     FileShare.Read);
            //    byte[] imageData = new byte[fs.Length];
            //    fs.Read(imageData, 0, imageData.Length);
            //    data.Append(MailEncoder.ConvertToBase64(Encoding.GetEncoding(_charset).GetString(imageData)));
            //    //data.Append(Encoding.GetEncoding(_charset).GetString(imageData));
            //}
            //finally
            //{
            //    if (fs != null)
            //        fs.Close();
            //}

            img.inline = true;
            img.skipFileData = false;
            return img.ToMime();

            //return header.ToString() + data.ToString();
        }

        #endregion

        #region EmbedAllImages

        private void EmbedAllImages(pmta.Message msg)
        {
            if (this[_currentMessageIndex].EmbeddedImages.Count > 0)
            {
                foreach (Message.Attachment attachment in this[_currentMessageIndex].EmbeddedImages)
                {
                    if (this[_currentMessageIndex].MessageHtml.Contains(attachment.contentid))
                    {
                        if (attachment.skipFileData)
                            throw new NotImplementedException("EmbedAllImages()");
                        else
                            EmbedImage(msg, attachment);
                    }
                }
            }
        }

        #endregion

        #region EmbedImage

        private void EmbedImage(pmta.Message msg, Message.Attachment img)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.Append("\r\n--" + _relatedBoundary + "\r\n");
            sb.Append("Content-ID: <" + img.contentid + ">\r\n");
            sb.Append("Content-Type: " + img.mimeType + "\r\n");
            sb.Append("Content-Transfer-Encoding: base64");
            sb.Append("Content-Disposition: inline; filename=" + img.filePath + "\r\n\r\n");


            this.AddData(msg, sb.ToString());

            //msg.Encoding = pmta.Encoding.Base64;

            FileStream fs = null;
            try
            {
                fs = new FileStream(img.filePath, FileMode.Open, FileAccess.Read,
                                                                 FileShare.Read);
                byte[] imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);
                this.AddData(msg, Encoding.GetEncoding(_charset).GetString(imageData));
                //msg.AddData(imageData);
                
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            //msg.Encoding = pmta.Encoding.SevenBit;
            this.AddData(msg, "\r\n--" + _relatedBoundary + "--\r\n"); // _charset
        }

        #endregion

        #endregion

        #region ThreadMaster

        //private string _lastDebugMsg = null;

        public void RunThreads()
        {
            try
            {
                StartThreads();
            }
            finally
            {
                //WaitForAllThreads();
                //CloseAllConnections();
                //_campaignStats.Interval.Add(new TimeSpan(DateTime.Now.Ticks));
            }
        }

        private void WaitForAllThreads()
        {
            foreach (Thread t in _threads)
            {
                t.Join();
            }
        }

        private void StartThreads()
        {
            _threads = new List<Thread>();
            
            //_connections = new List<pmta.Connection>();

            while (true)
            {
                if (_recipientList[_currentRecipientList].Eof)
                {
                    lock (this)
                    {
                        _recipientList[_currentRecipientList].Close();
                        _campaignStats.RecipientListProcessed++;

                        if (_currentRecipientList >= _recipientList.Count - 1)
                        {
                            _eor = true;
                            break;
                        }

                        _currentRecipientList++;
                        _recipientList[_currentRecipientList].BeginRead();
                    }
                }

                if (_eor) break;

                if (_threads.Count < (_servers.Count * (_connectionsPerServer * _threadsPerConnection)))
                {
                    foreach (Server server in _servers)
                    {
                        if (!server.Enabled)
                            continue;

                        if (server.Session.Connections.Count < _connectionsPerServer)
                        {
                            pmta.Connection connection = null;
                            try
                            {
                                // Try to re-use an existing connection(if one exists..)
                                if (server.Session.Connections.Count > 0 && server.Session.Threads.Count < _threadsPerConnection)
                                    connection = server.Session.Connections[RandomEx.RandomInt(0, server.Session.Connections.Count)];
                                else
                                {
                                    try
                                    {
                                        connection = new pmta.Connection(server.Host, server.Port, server.Username, server.Password);
                                    }
                                    catch //(System.Net.Sockets.SocketException)
                                    {
                                        ILogger logger = ServiceScope.Get<ILogger>();
                                        logger.Debug("Campaign.StartThreads(): Error connecting to {0}", server.Host);
                                        continue;
                                    }

                                    server.Session.Connections.Add(connection);
                                }

                                if (connection == null)
                                {
                                    continue;
                                }
                            }
                            catch (ThreadAbortException)
                            {
                                Thread.ResetAbort();

                                if (connection != null)
                                {
                                    try
                                    {
                                        connection.Close();
                                    }
                                    catch 
                                    {
                                    }

                                    server.Session.Connections.Remove(connection);
                                    connection = null;
                                }

                                return;
                            }
                            catch (Exception ex)
                            {
                                ErrorEx.DumpError(ex, new System.Diagnostics.StackTrace());
                                continue;
                            }

                            WorkItem workItem = new WorkItem();
                            workItem.Campaign = this;
                            workItem.Connection = connection;
                            workItem.Description = String.Format("h:{0}:c:{1}:t:{2}@{3}", server.Host, server.Session.Connections.Count, server.Session.Threads.Count, DateTime.Now);

                            Thread thread = new Thread(new ThreadStart(workItem.Process));
                            thread.Name = workItem.Description;
                            _threads.Add(thread);
                            server.Session.Threads.Add(thread);

                            thread.Start();
                        }
                    }
                }

                // Check all threads every cycle?
                for (int k = 0; k < _threads.Count; )
                {
                    if (_threads[k].ThreadState == ThreadState.Aborted || _threads[k].ThreadState == ThreadState.Stopped)
                    {
                        RemoveSessionThread(_threads[k]);
                        _threads.RemoveAt(k);
                    }
                    else
                        k++;
                }

                Thread.Sleep(500);
                RemoveSessionNullConnections();
            }

            // This will run once break is fired above, and then this will cleanup.

            while (_threads.Count > 0)
            {
                for (int k = 0; k < _threads.Count; )
                {
                    if (_threads[k].ThreadState == ThreadState.Aborted || _threads[k].ThreadState == ThreadState.Stopped)
                    {
                        //RemoveSessionThread(_threads[k]);
                        _threads.RemoveAt(k);
                    }
                    else
                        k++;
                }
                Thread.Sleep(100);
            }

            // We can call this now, since we let the threads finish safely.
            _running = false;
            //this.OnCompleted(_id, null);
            //this.Stop();
        }

        private void RemoveSessionNullConnections()
        {
            if (_servers != null && _servers.Count > 0)
            {
                lock (_servers) // this
                {
                    foreach (Server host in _servers)
                    {
                        for (int i = 0; i < host.Session.Connections.Count; )
                        {
                            if (host.Session.Connections[i] == null)
                            {
                                host.Session.Connections.RemoveAt(i);
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
            }
        }

        private void RemoveSessionThread(Thread thread)
        {
            if (_servers != null && _servers.Count > 0)
            {
                foreach (Server host in _servers)
                {
                    if (host.Session.Threads.Contains(thread))
                    {
                        lock (this)
                        {
                            host.Session.Threads.Remove(thread);
                            return; 
                        }
                    }
                }
            }
        }

        private void OnThreadMasterCompleted()
        {
        }


        #endregion

    }
}

