using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Injector.Core.Logging;
using Injector.Utilities.CommandLine;

namespace Injector.Utilities.CommandLine
{
    [Serializable]
    public class CommandLineOptions : ICommandLineOptions
    {
        #region Enums
        public enum Option
        {
            LogLevel,
            LogMethods,
            Data,
            // put options here
        }
        #endregion

        #region Variables
        private readonly Dictionary<Option, object> _options;
        #endregion

        #region Constructors/Destructors
        public CommandLineOptions()
        {
            _options = new Dictionary<Option, object>();
        }
        #endregion

        #region Public Methods
        public bool IsOption(Option option)
        {
            return _options.ContainsKey(option);
        }

        public int Count
        {
            get { return _options.Count; }
        }

        public object GetOption(Option option)
        {
            return _options[option];
        }
        #endregion

        #region ICommandLineOptiosn Implementations
        public void SetOption(string optionName, string argument)
        {
            Option option = (Option)Enum.Parse(typeof(Option), optionName, true);
            object value = argument;
            if (option == Option.LogLevel)
            {
                value = (LogLevel)Enum.Parse(typeof(LogLevel), argument, true);
            }
            _options.Add(option, value);
        }

        public void DisplayOptions()
        {
            string[] logLevelNames = Enum.GetNames(typeof(LogLevel));
            StringBuilder logLevels = new StringBuilder();
            foreach (string level in logLevelNames)
            {
                if (logLevels.Length > 0)
                    logLevels.Append(", ");
                logLevels.Append(level);
            }

            string options = "Valid options:\r\n";
            options += "/help\t\tShows this screen\r\n";
            options += "/LogMethod\tInstructs the logger to also log the name of its calling method\r\n";
            options += "/LogLevel=<level>\tSets the log level.  <level> should be one of the following values:\r\n\t\t" +
              logLevels;
            //InfoScreen form = new InfoScreen("Konvict", "Command Line Options", options, InfoScreen.Image.info);
            //form.Size = new Size(714, 326);
            //Application.Run(form);
        }
        #endregion
    }
}
