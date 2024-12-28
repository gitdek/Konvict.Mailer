
using System.Collections.Generic;
using Injector.Core.Messaging;

namespace Injector.Core.Services.Messaging
{
  public class MessageBroker : IMessageBroker
  {
    #region Protected fields

    protected IDictionary<string, IMessageQueue> _queues;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageBroker"/> class.
    /// </summary>
    public MessageBroker()
    {
      _queues = new Dictionary<string, IMessageQueue>();
    }

    #region IMessageBroker implementation


    public IMessageQueue GetOrCreate(string queueName)
    {
      if (!_queues.ContainsKey(queueName))
      {
        Queue q = new Queue();
        _queues[queueName] = q;
      }
      return _queues[queueName];
    }

    public IList<string> Queues
    {
      get 
      {
        List<string> queueNames = new List<string>();
        IEnumerator<KeyValuePair<string, IMessageQueue>> enumer = _queues.GetEnumerator();
        while (enumer.MoveNext())
          queueNames.Add(enumer.Current.Key);
        return queueNames;
      }
    }

    #endregion
  }
}
