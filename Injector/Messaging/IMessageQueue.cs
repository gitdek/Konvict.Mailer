
using System.Collections.Generic;

namespace Injector.Core.Messaging
{
  public delegate void MessageReceivedHandler(QueueMessage message);

  public interface IMessageQueue
  {
    event MessageReceivedHandler MessageReceived;

    /// <summary>
    /// Gets the information if this queue has subscribers.
    /// </summary>
    bool HasSubscribers { get;}

    /// <summary>
    /// Gets the message filters.
    /// </summary>
    IList<IMessageFilter> Filters { get;}

    /// <summary>
    /// Sends the specified <paramref name="message"/>.
    /// </summary>
    /// <param name="message">The message to send.</param>
    void Send(QueueMessage message);
  }
}
