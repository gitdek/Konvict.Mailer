using System;
using System.Globalization;

namespace Injector.Exceptions
{
    /// <summary>
    /// Fatal exception, will be thrown if something happened what must not happen. The reason
    /// of this exception is always a coding problem.
    /// </summary>
    [Serializable()]
    public class FatalException : ApplicationException
    {
        public FatalException(string msg, params object[] args) :
            base(string.Format(msg, args)) { }
        public FatalException(string msg, Exception ex, params object[] args) :
            base(string.Format(msg, args), ex) { }
    }

    /// <summary>
    /// Thrown if a module or instance is in an invalid state for the current operation.
    /// </summary>
    [Serializable()]
    public class InvalidStateException : ApplicationException
    {
        public InvalidStateException(string msg, params object[] args) :
            base(string.Format(msg, args)) { }
        public InvalidStateException(string msg, Exception ex, params object[] args) :
            base(string.Format(msg, args), ex) { }
    }


    /// <summary>
    /// Occurs when requested service is not found in the current- or one of its parent <see cref="ServiceScope"/>s.
    /// </summary>
    [Serializable]
    public class ServiceNotFoundException : Exception
    {
        private Type serviceType;

        public ServiceNotFoundException() { }

        public ServiceNotFoundException(string message)
            : base(message) { }

        public ServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Creates a new <see cref="ServiceNotFoundException"/> instance, and initializes it with the given type.
        /// </summary>
        /// <param name="serviceType">the type of service that was not found.</param>
        public ServiceNotFoundException(Type serviceType)
            : base(string.Format(CultureInfo.InvariantCulture, "Could not find the {0} service", serviceType))
        {
            this.serviceType = serviceType;
        }

        /// <summary>
        /// Returns the type of the service that was not found.
        /// </summary>
        public Type ServiceType
        {
            get { return serviceType; }
        }
    }
}
