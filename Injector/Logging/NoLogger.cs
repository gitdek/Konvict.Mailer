using System;

namespace Injector.Core.Logging
{
    /// <summary>
    /// Default <see cref="ILogger"/> implementation that does absolutely nothing.
    /// </summary>
    internal class NoLogger : ILogger
    {
        #region ILogger Members

        public bool LogMethodNames
        {
            get { return false; }
            set { }
        }

        public LogLevel Level
        {
            get { return LogLevel.None; }
            set { }
        }

        public void Debug(string format, params object[] args) { }
        public void Info(string format, params object[] args) { }
        public void Warn(string format, Exception ex, params object[] args) { }
        public void Warn(string format, params object[] args) { }
        public void Error(string format, params object[] args) { }
        public void Error(string format, Exception ex, params object[] args) { }
        public void Error(Exception ex) { }
        public void Critical(Exception ex) { }
        public void Critical(string format, params object[] args) { }

        #endregion
    }
}
