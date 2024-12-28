using System;
using System.Collections.Generic;
using System.Text;

namespace Injector.Net.IO
{
    /// <summary>
    /// The exception that is thrown when maximum allowed line size has exceeded.
    /// </summary>
    public class LineSizeExceededException : Exception
    {
        /// <summary>
        /// Default coonstructor.
        /// </summary>
        public LineSizeExceededException() : base()
        {
        }
    }
}
