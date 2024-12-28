using Injector.Core;
using Injector.Core.Messaging;

namespace Injector.Core
{
    /// <summary>
    /// This class provides an interface for the messages sent by the system.
    /// </summary>
    public class SystemMessaging
    {
        // Message Queue name
        public const string QUEUE = "System";

        public enum MessageType
        {
            /// <summary>
            /// All initialization has been done, all plugins are loaded.
            /// </summary>
            SystemStarted,

            /// <summary>
            /// The system will go down now.
            /// </summary>
            SystemShutdown,
        }

        // Message data
        public const string MESSAGE_TYPE = "MessagType"; // Message type stored as MessageType

        /// <summary>
        /// Sends a parameterless system message.
        /// </summary>
        /// <param name="type">Type of the message.</param>
        public static void SendSystemMessage(MessageType type)
        {
            IMessageQueue queue = ServiceScope.Get<IMessageBroker>().GetOrCreate(QUEUE);
            QueueMessage msg = new QueueMessage();
            msg.MessageData[MESSAGE_TYPE] = type;
            queue.Send(msg);
        }
    }
}
