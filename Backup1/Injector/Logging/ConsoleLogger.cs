
using System;
using Injector.Core.Logging;

namespace Injector.Core.Services.Logging
{
    /// <summary>
    /// A <see cref="ILogger"/> implementation that writes messages to the application console.
    /// </summary>
    public class ConsoleLogger : DefaultLogger
    {
        /// <summary>
        /// Creates a new <see cref="ConsoleLogger"/> instance and initializes it with the given
        /// <paramref name="level"/>.
        /// </summary>
        /// <param name="level">The minimum level messages must have to be written to the file.</param>
        /// <param name="logMethodNames">Indicates whether to log the calling method's name.</param>
        public ConsoleLogger(LogLevel level, bool logMethodNames) :
            base(Console.Out, level, logMethodNames) { }
    }
}
