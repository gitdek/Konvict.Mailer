#region usings
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
#endregion

using Injector.Core.Settings;
using Injector.Core.TaskScheduler;

namespace Injector.Core.Services.TaskScheduler
{
  /// <summary>
  /// The TaskSchedulerSettings class is responsible for storing all settings of the <see cref="TaskScheduler"/>.
  /// It holds the next TaskID to be dealt and all <see cref="Task"/>s registered with the <see cref="TaskScheduler"/>.
  /// All <see cref="Task"/>s are stored in a <see cref="TaskCollection"/>.
  /// </summary>
  [Serializable]
  public class TaskSchedulerSettings
  {

    #region Protected fields

    protected int _taskID = 0;
    protected TaskCollection _taskCollection = new TaskCollection();

    #endregion

    #region Ctor
    public TaskSchedulerSettings()
    {
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Gets the next TaskID to be assigned to a new <see cref="Task"/>.
    /// </summary>
    /// <returns></returns>
    public int GetNextTaskID()
    {
      if (_taskID == Int32.MaxValue)
      {
        _taskID = 0;
      }
      return _taskID++;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Property which is used by the <see cref="ISettingsManager"/> to retrieve/set the last dealt TaskID.
    /// </summary>
    [Setting(SettingScope.Global, 0)]
    public int TaskID
    {
      get
      {
        return _taskID;
      }
      set
      {
        _taskID = value;
      }
    }

    /// <summary>
    /// Property which is used by the <see cref="ISettingsManager"/> to retrieve/set list of registered <see cref="Task"/>s.
    /// Also used by the <see cref="TaskScheduler"/> to access the <see cref="TaskCollection"/>.
    /// </summary>
    [Setting(SettingScope.Global)]
    public TaskCollection TaskCollection
    {
      get
      {
        return _taskCollection;
      }
      set
      {
        _taskCollection = value;
      }
    }
    #endregion
  }
}
