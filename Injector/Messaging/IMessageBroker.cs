

using System.Collections.Generic;

namespace Injector.Core.Messaging
{
  /// <summary>
  /// Registration for all system message queues.
  /// </summary>
  public interface IMessageBroker
  {
    /// <summary>
    /// Get the message queue with the specified name.
    /// </summary>
    /// <param name="queueName">The name of the queue to return.</param>
    /// <returns>The queue with the specified name.</returns>
    IMessageQueue GetOrCreate(string queueName);

    /// <summary>
    /// Gets the names of all queues registered in this message broker.
    /// </summary>
    /// <returns>List of queue names.</returns>
    IList<string> Queues { get;}
  }
}
