#region Usings

using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;

#endregion

namespace Injector.Core.Threading
{

  #region enums

  public enum QueuePriority
  {
    Low,
    Normal,
    High
  }

  #endregion

  public interface IThreadPool
  {
    #region Methods to add work
    IWork Add(DoWorkHandler work);
    IWork Add(DoWorkHandler work, QueuePriority queuePriority);
    IWork Add(DoWorkHandler work, string description);
    IWork Add(DoWorkHandler work, ThreadPriority threadPriority);
    IWork Add(DoWorkHandler work, WorkEventHandler workCompletedHandler);
    IWork Add(DoWorkHandler work, string description, QueuePriority queuePriority);
    IWork Add(DoWorkHandler work, string description, QueuePriority queuePriority, ThreadPriority threadPriority);
    IWork Add(DoWorkHandler work, string description, QueuePriority queuePriority, ThreadPriority threadPriority, WorkEventHandler workCompletedHandler);
    void Add(IWork work);
    void Add(IWork work, QueuePriority queuePriority);
    #endregion

    #region Methods to manage interval-based work
    void AddIntervalWork(IWorkInterval intervalWork, bool runNow);
    void RemoveIntervalWork(IWorkInterval intervalWork);
    #endregion

    #region Methods to control the threadpool
    void Stop();
    #endregion

    #region Threadpool status properties
    int ThreadCount { get; }
    int BusyThreadCount { get; }
    long WorkItemsProcessed { get; }
    int QueueLength { get; }
    int MinimumThreads { get; }
    int MaximumThreads { get; }
    #endregion
  }
}
