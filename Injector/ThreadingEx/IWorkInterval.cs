#region Usings

using System;
using System.Text;
using System.Collections.Generic;

#endregion

namespace ThreadingEx
{
    public interface IWorkInterval
    {
        /// <summary>
        /// Work to perform at a specified interval
        /// </summary>
        IWork Work { get; }

        /// <summary>
        /// Interval to perform work in
        /// Note: Interval can never be lower than the ThreadPool's thread idle timeout
        /// </summary>
        TimeSpan WorkInterval { get; }

        /// <summary>
        /// Last time the work interval was started
        /// (used to determine when to run the schedule again; updated by the ThreadPool)
        /// </summary>
        DateTime LastRun { get; set; }

        /// <summary>
        /// Indicator whether or not the work interval is being run currently.
        /// (used to avoid running it concurrently if workload time exceeds the given interval)
        /// Make sure to set it to false again after Work has been performed.
        /// </summary>
        bool Running { get; set; }

        /// <summary>
        /// Method that gets called when the ThreadPool is stopped
        /// </summary>
        void OnThreadPoolStopped();
        void ResetWorkState();
    }
}
