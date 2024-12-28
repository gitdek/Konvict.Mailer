namespace Injector.Core.Messaging
{
  public interface IMessageFilter
  {
    /// <summary>
    /// Processes the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>transformed message (or null)</returns>
    QueueMessage Process(QueueMessage message);
  }
}
