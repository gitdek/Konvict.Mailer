namespace Injector.Core.TaskScheduler
{
  /// <summary>
  /// Specifies the type of <see cref="TaskMessage"/>
  /// </summary>
  public enum TaskMessageType
  {
    DUE,
    CHANGED,
    DELETED,
    EXPIRED
  }

  /// <summary>
  /// Messages to be sent by the <see cref="TaskScheduler"/>.
  /// </summary>
  public class TaskMessage
  {
    #region Private fields

    private Task _task;
    private TaskMessageType _type;

    #endregion

    #region Ctor

    public TaskMessage(Task task, TaskMessageType type)
    {
      _task = task;
      _type = type;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The <see cref="Task"/> which triggered this message.
    /// </summary>
    public Task Task
    {
      get { return _task; }
      set { _task = value; }
    }

    /// <summary>
    /// Type of message.
    /// </summary>
    public TaskMessageType Type
    {
      get { return _type; }
      set { _type = value; }
    }

    #endregion
  }
}
