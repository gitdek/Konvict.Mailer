using System.Collections.Generic;

namespace Injector.Core.Messaging
{
  /// <summary>
  /// Message to be used in <see cref="IMessageQueue"/>
  /// </summary>
  public class QueueMessage
  {
    #region Protected fields

    protected IMessageQueue _queue;
    protected IDictionary<string, object> _metaData = new Dictionary<string, object>();

    #endregion

    #region Public properties

    /// <summary>
    /// Gets or sets the queue this message will be send in.
    /// </summary>
    /// <value>The queue.</value>
    public IMessageQueue MessageQueue 
    {
      get { return _queue; }
      set { _queue = value; }
    }

    /// <summary>
    /// Gets or sets the message data. The message data is a generic dictionary special
    /// data entries defined by the message queue.
    /// </summary>
    /// <value>The meta data.</value>
    public IDictionary<string, object> MessageData
    {
      get { return _metaData; }
      set { _metaData = value; }
    }

    #endregion
  }
}
