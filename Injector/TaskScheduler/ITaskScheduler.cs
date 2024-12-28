#region usings
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Injector.Core.TaskScheduler
{
  /// <summary>
  /// Public representation of the global <see cref="TaskScheduler"/>.
  /// The task scheduler simplifies generating events at a particular time schedule. It sends out a message
  /// as soon as a task is due on the "taskscheduler" message queue. It sends out a <see cref="TaskMessage" />
  /// with the particular <see cref="Task"/> as a reference to the interested listeners on this message queue.
  /// Listeners then can act on the generate message to perform time or interval based tasks.
  /// </summary>
  public interface ITaskScheduler
  {
    /// <summary>
    /// Adds a task to the task scheduler
    /// </summary>
    /// <param name="newTask">task to add to the scheduler</param>
    /// <returns>ID assigned to the given task</returns>
    int AddTask(Task newTask);
    
    /// <summary>
    /// Updates an already registered task
    /// </summary>
    /// <param name="taskId">ID of the task to update</param>
    /// <param name="updatedTask">the updated task</param>
    void UpdateTask(int taskId, Task updatedTask);

    /// <summary>
    /// Removes a task from the task scheduler
    /// </summary>
    /// <param name="taskId">ID of the task to remove</param>
    void RemoveTask(int taskId);
    
    /// <summary>
    /// Gets a registered task from the task scheduler
    /// </summary>
    /// <param name="taskId">ID of the task to get</param>
    /// <returns>task with given ID</returns>
    Task GetTask(int taskId);
    
    /// <summary>
    /// Gets all registered tasks for the given owner from the task scheduler
    /// </summary>
    /// <param name="ownerId">owner ID to get a task list for</param>
    /// <returns>list of tasks for given owner</returns>
    IList<Task> GetTasks(string ownerId);
  }
}
