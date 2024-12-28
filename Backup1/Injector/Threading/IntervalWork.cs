using System;
using Injector.Core.Threading;

namespace Injector.Core.Threading
{
  /// <summary>
  /// Default implementation of an <see cref="IWorkInterval"/>.
  /// </summary>
  public class IntervalWork : Work, IWorkInterval
  {
    #region Variables

    TimeSpan _interval;
    DateTime _lastRun = DateTime.Now;
    bool _running = false;

    #endregion

    #region Contructor

    public IntervalWork(DoWorkHandler work, TimeSpan interval)
    {
      WorkLoad = work;
      _interval = interval;
    }

    #endregion

    #region IWorkInterval implementation

    public IWork Work
    {
      get { return this; }
    }

    public TimeSpan WorkInterval
    {
      get { return _interval; }
    }

    public DateTime LastRun
    {
      get { return _lastRun; }
      set { _lastRun = value; }
    }

    public bool Running
    {
      get { return _running; }
      set { _running = value; }
    }

    public void OnThreadPoolStopped()
    {
    }

    public void ResetWorkState()
    {
      _running = false;
      State = WorkState.INIT;
    }

    #endregion

    #region Work overrides

    public override void Process()
    {
      // don't perform canceled work
      if (State == WorkState.CANCELED)
        return;
      // don't perform work which is in an invalid state
      if (State != WorkState.INQUEUE)
        throw new InvalidOperationException(String.Format("WorkState for work {0} not INQUEUE, but {1}", Description, State));
      State = WorkState.INPROGRESS;

      // perform work 
      if (WorkLoad != null)
        WorkLoad();

      // mark work as finished and fire work completion delegate
      State = WorkState.FINISHED;
      if (WorkCompleted != null)
        WorkCompleted(EventArgs);
      _running = false;
    }

    #endregion
  }
}