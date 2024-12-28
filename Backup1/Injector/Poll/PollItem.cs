using System;
using System.Collections.Generic;
using Injector.Core.Logging;
using Injector.Core.Settings;
using System.Collections;
using Injector.Core.Services.Threading;
using Injector.Core.Threading;
using System.Diagnostics;

using Injector.Utilities.Collections;
using System.IO;

namespace Injector
{
    [Serializable]
    public class PollItem
    {

        public delegate int HookDelegate(int code, int wParam, IntPtr lParam);


        #region Fields

        #region Settings

        private PollCollection _owner;
        internal string _command = string.Empty;
        private uint _interval;
        private PollExportAction_enum _exportAction = PollExportAction_enum.Disabled;
        private string _id = string.Empty;
        private bool _enabled = true;

        [NonSerialized]
        private bool _startNew;
        #endregion

        #region State

        [NonSerialized]
        private bool _running = false;
        
        [NonSerialized]
        private System.Timers.Timer _pollTime;

        //[NonSerialized]
        //private System.Threading.Thread _pollThread;

        [NonSerialized]
        private TimeSpan _timeElapsed = TimeSpan.MinValue;

        [NonSerialized]
        internal string _sshClient = "";

        [NonSerialized]
        internal string _scpClient = "";

        [NonSerialized]
        internal string _sshKey = "";

        [NonSerialized]
        internal int _sshPort = 22;

        [NonSerialized]
        private List<Server> _servers;

        [NonSerialized]
        private Hashtable _procs = Hashtable.Synchronized(new Hashtable());

        [NonSerialized]
        private Hashtable _results = Hashtable.Synchronized(new Hashtable());

        [NonSerialized]
        string[] _buff = null;

        //private MultiDictionary<string, object> _mlist = null;


        [NonSerialized]
        private ThreadPool _pool;


        public event EventHandler<PollEventArgs> PollEvent;
        

        #endregion

        #endregion

        internal PollItem(PollCollection owner, string command, uint interval, PollExportAction_enum exportAction, string id)
        {
            _owner = owner;
            _command = command;
            _interval = interval;
            _exportAction = exportAction;
            _id = id;
        }

        internal virtual void OnPollEvent(PollEventArgs e)
        {
            lock (_results.SyncRoot)
            {
                _results[e.Server.ID] = e;
            }

            EventHandler<PollEventArgs> handler = PollEvent;
            if (handler != null)
                handler(this, e);
        }

        internal void Start(List<Server> servers)
        {
            _startNew = true;

            _servers = servers;

            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }


            if (coreSettings == null)
                coreSettings = new CoreSettings();


            _sshClient = coreSettings.Sshclient;
            _scpClient = coreSettings.Scpclient;
            _sshKey = coreSettings.KeyFile;
            _sshPort = coreSettings.Sshport;
            _running = true;
            _timeElapsed = new TimeSpan(0, 0, 1);

            if (!_startNew)
            {
                _pollTime = new System.Timers.Timer(_interval * 1000);

                startProcs();

                _pollTime.Elapsed += new System.Timers.ElapsedEventHandler(OnPollElapsed);
                _pollTime.Start();
            }
            else
            {
                ThreadPoolStartInfo tpsi = new ThreadPoolStartInfo(1, 25);
                _pool = new ThreadPool(tpsi);

                IntervalWork iWork = new IntervalWork(new DoWorkHandler(this.PerformPoll), new TimeSpan(0, 0, 0, (int)_interval));
                _pool.AddIntervalWork(iWork, true);
            }
        }

        internal void Stop()
        {
            if (_running)
            {
                if (_pool != null)
                {
                    _pool.Stop();

                    ILogger logger = ServiceScope.Get<ILogger>();
                    logger.Debug("PollItem: Stop called after {0} seconds.", _timeElapsed.Seconds);
                }
            }
        }


        private void startProcs()
        {
            _procs = Hashtable.Synchronized(new Hashtable());
            _buff = new string[_servers.Count];

            lock (_procs.SyncRoot)
            {
                foreach (Server server in _servers)
                {
                    string args = string.Format("-ssh -P {0} -i \"{1}\" root@{2}", _sshPort, _sshKey, server.Host);

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.Arguments = args;
                    startInfo.FileName = _sshClient;
                    startInfo.ErrorDialog = false;
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardInput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.CreateNoWindow = true;
                    
                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.EnableRaisingEvents = true;

                    //process.Exited += new EventHandler(process_Exited);
                    process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                    
                    try
                    {
                        process.Start();
                    }
                    catch
                    {
                        process = null;
                    }
                    finally
                    {
                        _procs[server.ID] = process;

                        if (process != null)
                        {
                            process.BeginOutputReadLine();
                        }
                    }
                }

            }

        }

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(e.Data))
            //{
            //    foreach (DictionaryEntry de in _procs)
            //    {
            //        if (de.Value.Equals(sender))
            //        {
            //            Server mailhost = ServiceScope.Get<ICampaignManager>().GetServer(de.Key.ToString());

            //            int idx = _servers.IndexOf(mailhost);
            //            if (idx > 0 && idx < _servers.Count)
            //            {
            //                _buff[idx] += e.Data;
            //                foreach (char c in e.Data)
            //                {
            //                    if (c == '\x1b')
            //                    {
            //                        EndBufferRecv(mailhost, idx);
            //                    }
            //                }
            //            }

            //        }
            //    }
            //}
        }

        private void EndBufferRecv(Server mailhost, int indexBuffer)
        {
            string bufferClean = _buff[indexBuffer].ToString();
            _buff[indexBuffer] = string.Empty;

            //int idxCommand = bufferClean.IndexOf("# " + _command);
            //int idxBell = bufferClean.IndexOf('\a', idxCommand);

            //bufferClean = bufferClean.Substring(idxCommand);

            PollEventArgs pea = new PollEventArgs(mailhost.ID, mailhost.Host, bufferClean, null, null, mailhost);
            OnPollEvent(pea);
        }

        private void process_Exited(object sender, EventArgs e)
        {
            //_procs.Remove(sender);
            //throw new NotImplementedException();
        }

        private void OnPollElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan tmpspan = new TimeSpan(0, 0, (int)_interval);
            _timeElapsed += tmpspan;

            //PerformPoll();

            //foreach (Server mailhost in _servers)
            //{
            //    // Start a new process...
            //    if (_startNew)
            //    {
            //        System.Threading.Thread t = new System.Threading.Thread(PerformPoll);
            //        t.IsBackground = true;
            //        t.Start(mailhost);
            //    }
            //    else
            //    {
            //        // Re-use existing process started earlier.
            //        Process proc = (Process)_procs[mailhost.ID];
            //        if (proc != null)
            //        {
            //            proc.StandardInput.WriteLine(_command);

            //            /** blocks? */
            //            //string output = string.Empty; // = proc.StandardOutput.ReadLine();
            //            //string buf = string.Empty;
            //            //while ((buf = proc.StandardOutput.ReadLine()) != null)
            //            //{
            //            //    output += buf;
            //            //}
            //            //string errorOutput = string.Empty; //proc.StandardError.ReadToEnd();

            //            //PollEventArgs pea = new PollEventArgs(_id, mailhost.Host, output, errorOutput, proc.StartInfo, mailhost);
            //            //OnPollEvent(pea);

            //        }
            //        else
            //        {
            //            ILogger logger = ServiceScope.Get<ILogger>();
            //            logger.Debug("PollItem: Process non-existant!");
            //        }
            //    }
            //}
        }

        #region Obsolete

        private void PollHost(Server server)
        {
            if (server == null)
                throw new ArgumentNullException("server");

            string arguments = string.Format("-batch -ssh -P {0} -i \"{1}\" root@{2} {3}", _sshPort, _sshKey, server.Host, _command);
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.Arguments = arguments;
            startInfo.FileName = _sshClient;
            startInfo.ErrorDialog = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = startInfo;
            process.Start();
            
            string output = process.StandardOutput.ReadToEnd();
            string errorOutput = process.StandardError.ReadToEnd();

            process.WaitForExit();
            process.Close();

            PollEventArgs e = new PollEventArgs(_id, server.Host, output, errorOutput, startInfo, server);
            this.OnPollEvent(e);
        }

        #endregion

        private void PerformPoll()
        {
            foreach (Server server in _servers)
            {
                PollWorker pWorker = new PollWorker(this, server);

                System.Threading.Thread thread = new System.Threading.Thread(pWorker.Perform);
                thread.Start();
            }
        }

        #region Properties Impelementation

        public string Command
        {
            get
            {
                return _command;
            }
            set
            {
                if (_command == value)
                    return;
                _command = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (_enabled == value)
                    return;
                _enabled = value;
            }
        }

        public PollExportAction_enum ExportAction
        {
            get
            {
                return _exportAction;
            }
            set
            {
                if (_exportAction == value)
                    return;
                _exportAction = value;
            }
        }

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                    return;
                _id = value;
            }
        }

        public uint Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                if (_interval == value)
                    return;
                _interval = value;
            }
        }

        public Hashtable Results
        {
            get
            {
                if (_results == null)
                {
                    _results = Hashtable.Synchronized(new Hashtable());
                }
                return _results;
            }
        }

        public bool Running
        {
            get
            {
                return _running;
            }
        }
        #endregion

    }

    internal class PollWorker
    {
        private Server _server;
        private PollItem _owner;

        public PollWorker(PollItem owner, Server server)
        {
            _owner = owner;
            _server = server;
        }

        public void Perform()
        {
            string arguments = string.Format("-batch -ssh -P {0} -i \"{1}\" root@{2} {3}", _owner._sshPort, _owner._sshKey, _server.Host, _owner._command);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.Arguments = arguments;
            startInfo.FileName = _owner._sshClient;
            startInfo.ErrorDialog = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            if (process.Start())
            {
                string output = process.StandardOutput.ReadToEnd();
                //string errorOutput = process.StandardError.ReadToEnd();

                if (process.WaitForExit(30 * 1000))
                {
                    process.Close();
                    process.Dispose();

                    PollEventArgs e = new PollEventArgs(_server.ID, _server.Host, output, null, startInfo, _server);
                    _owner.OnPollEvent(e);
                }
            }
        }

        #region Properties implementation


        public PollItem Owner
        {
            get
            {
                return _owner;
            }
        }

        public Server Server
        {
            get
            {
                return _server;
            }
        }

        #endregion


    }
}
