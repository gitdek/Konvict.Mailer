

using System.Collections.Generic;
using Injector.Core.Messaging;

namespace Injector.Core.Services.Messaging
{
  public class Queue : IMessageQueue
  {
    #region Protected fields

    protected IList<IMessageFilter> _filters;

    #endregion

    public Queue()
    {
      _filters = new List<IMessageFilter>();
    }

    #region IMessageQueue implementation

    public event MessageReceivedHandler MessageReceived;

    public IList<IMessageFilter> Filters
    {
      get { return _filters; }
    }

    public void Send(QueueMessage message)
    {
      message.MessageQueue = this;
      foreach (IMessageFilter filter in _filters)
      {
        message = filter.Process(message);
        if (message == null) return;
      }
      if (MessageReceived != null)
      {
        MessageReceived(message);
      }
    }

    public bool HasSubscribers
    {
      get { return (MessageReceived != null); }
    }

    #endregion
  }
}
