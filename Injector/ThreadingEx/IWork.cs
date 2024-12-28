using System;
using System.Threading;

namespace ThreadingEx
{
    public enum WorkState
    {
        INIT,
        INQUEUE,
        CANCELED,
        INPROGRESS,
        FINISHED,
        ERROR
    }

    public delegate void DoWorkHandler();
    public delegate void WorkEventHandler(WorkEventArgs args);

    public interface IWork
    {
        /// <summary>
        /// The current state the work is in
        /// (set by the threadpool and used to cancel work which is still in the queue)
        /// </summary>
        WorkState State { get; set; }

        /// <summary>
        /// Description for this work (optional)
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Placeholder for any exception thrown by this the workload code
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Specifies the scheduling priority for this work
        /// </summary>
        ThreadPriority ThreadPriority { get; set; }

        /// <summary>
        /// Method which contains the work that should be performed by the ThreadPool
        /// </summary>
        void Process();
    }
}
