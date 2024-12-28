using System;
using System.Collections.Generic;
using System.Text;
using Injector.Core.Services.Settings;
using System.Drawing;
using Injector.Core.Settings;

namespace Injector
{
    public class CoreSettings
    {
        //private List<int> _alist = new List<int>();
        private int _totalRuns;
        private int _mainformPosX;
        private int _mainformPosY;
        private int _mainformDimX;
        private int _mainformDimY;
        private int _mainformSplitter;
        private bool _playIntroSound;
        private string _sshclient;
        private int _sshport;
        private string _scpclient;
        private string _keyFile;
        private string _remoteKlocalPath;

        private string _mngtDbHost = string.Empty;
        private int _mngtDbPort = 3306;
        private string _mngtDbUser = string.Empty;
        private string _mngtDbPassword = string.Empty;
        private string _mngtDbName = string.Empty;

        private string _mngtConnectionString;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CoreSettings()
        {
        }

        [Setting(SettingScope.User)]
        public string KeyFile
        {
            get { return _keyFile; }
            set { _keyFile = value; }
        }

        [Setting(SettingScope.User)]
        public int MainformPosX
        {
            get { return _mainformPosX; }
            set { _mainformPosX = value; }
        }

        [Setting(SettingScope.User)]
        public int MainformPosY
        {
            get { return _mainformPosY; }
            set { _mainformPosY = value; }
        }

        [Setting(SettingScope.User, DefaultValue = 1200)]
        public int MainformDimX
        {
            get { return _mainformDimX; }
            set { _mainformDimX = value; }
        }

        [Setting(SettingScope.User, DefaultValue = 750)]
        public int MainformDimY
        {
            get { return _mainformDimY; }
            set { _mainformDimY = value; }
        }

        [Setting(SettingScope.User, DefaultValue = 220)]
        public int MainformSplitter
        {
            get { return _mainformSplitter; }
            set { _mainformSplitter = value; }
        }

        public string MngtConnectionString
        {
            get { return _mngtConnectionString; }
            set
            {
                if (_mngtConnectionString == value)
                    return;
                _mngtConnectionString = value;
            }
        }

        [Setting(SettingScope.User)]
        public string MngtDbHost
        {
            get { return _mngtDbHost; }
            set
            {
                if (_mngtDbHost == value)
                    return;
                _mngtDbHost = value;
                _mngtConnectionString = "connectionstring=server=" + _mngtDbHost + ";database = " + _mngtDbName + ";user id=" + _mngtDbUser + ";password=" + _mngtDbPassword + ";";
            }
        }

        [Setting(SettingScope.User, DefaultValue = 3306)]
        public int MngtDbPort
        {
            get { return _mngtDbPort; }
            set
            {
                if (_mngtDbPort == value)
                    return;
                _mngtDbPort = value;
                _mngtConnectionString = "connectionstring=server=" + _mngtDbHost + ";database = " + _mngtDbName + ";user id=" + _mngtDbUser + ";password=" + _mngtDbPassword + ";";
            }
        }

        [Setting(SettingScope.User)]
        public string MngtDbUser
        {
            get { return _mngtDbUser; }
            set
            {
                if (_mngtDbUser == value)
                    return;
                _mngtDbUser = value;
                _mngtConnectionString = "connectionstring=server=" + _mngtDbHost + ";database = " + _mngtDbName + ";user id=" + _mngtDbUser + ";password=" + _mngtDbPassword + ";";
            }
        }

        [Setting(SettingScope.User)]
        public string MngtDbPassword
        {
            get { return _mngtDbPassword; }
            set
            {
                if (_mngtDbPassword == value)
                    return;
                _mngtDbPassword = value;
                _mngtConnectionString = "connectionstring=server=" + _mngtDbHost + ";database = " + _mngtDbName + ";user id=" + _mngtDbUser + ";password=" + _mngtDbPassword + ";";
            }
        }

        [Setting(SettingScope.User, DefaultValue = "campaign_manager")]
        public string MngtDbName
        {
            get { return _mngtDbName; }
            set
            {
                if (_mngtDbName == value)
                    return;
                _mngtDbName = value;
                _mngtConnectionString = "connectionstring=server=" + _mngtDbHost + ";database = " + _mngtDbName + ";user id=" + _mngtDbUser + ";password=" + _mngtDbPassword + ";";
            }
        }

        [Setting(SettingScope.User, DefaultValue = true)]
        public bool PlayIntroSound
        {
            get { return _playIntroSound; }
            set { _playIntroSound = value; }
        }

        [Setting(SettingScope.User, DefaultValue = "/etc/klocal")]
        public string RemoteKlocalPath
        {
            get { return _remoteKlocalPath; }
            set { _remoteKlocalPath = value; }
        }

        [Setting(SettingScope.User)]
        public string Scpclient
        {
            get { return _scpclient; }
            set { _scpclient = value; }
        }

        [Setting(SettingScope.User)]
        public string Sshclient
        {
            get { return _sshclient; }
            set { _sshclient = value; }
        }

        [Setting(SettingScope.User, DefaultValue = 22)]
        public int Sshport
        {
            get { return _sshport; }
            set { _sshport = value; }
        }

        [Setting(SettingScope.User, DefaultValue = 0)]
        public int TotalRun
        {
            get { return _totalRuns; }
            set { _totalRuns = value; }
        }

    }
}
