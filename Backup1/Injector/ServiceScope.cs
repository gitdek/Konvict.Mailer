using System;
using System.Collections.Generic;
using Injector.Exceptions;

public interface IStatus
{
    /// <summary>
    /// Returns a list of strings containing status information for all interesting parameters of
    /// this instance.
    /// </summary>
    IList<string> GetStatus();
}

/// <summary>
/// The Service Scope class.  It is used to keep track of the scope of services.
/// The moment you create a new ServiceScope instance, any service instances you
/// add to it will be automtically used by code that is called while the the 
/// ServiceScope instance remains in scope (i.e. is not Disposed)
/// </summary>
/// <remarks>
/// A ServiceScope is some kind of repository that holds a reference to 
/// services that other components could need.Instead of making
/// this class a static with static properties for all types of services we 
/// choose to create a mechanism that is more flexible.  The biggest advantage of
/// this implemtentation is that you can create different ServiceScope instances
/// that will be "stacked" upon one another.This way you can (temporarily) 
/// override a certain service by adding another implementation of the service
/// interface, which fits you better.  While your new ServiceScope instance 
/// remains in scope, all code that is executed will automatically (if it is
/// written correctly of course) use this new service implementation.
/// 
/// <b>A service scope is only valid in the same thread it was created.</b> 
/// 
/// The recommended way of passing the current <see cref="ServiceScope"/> to 
/// another thread is by passing <see cref="ServiceScope.Current"/> with the 
/// delegate used to start the thread and then use 
/// <code>ServiceScope.Current = passedContect;</code> to restore it in the 
/// thread.If you do not pass the current ServiceScope to the 
/// background thread, it will automatically fallback to the <b>global</b> 
/// ServiceScope.  This is the current ServiceScope of the application thread.
/// This ServiceScope can be another instance than the one you expect... 
/// </remarks>
/// <example> This example creates a new ServiceScope and adds its own implementation
/// of a certain service to it.
/// <code>
/// //SomeMethod will log to the old logger here.
/// SomeMethod();
/// using(new ServiceScope())
/// {
///   ServiceScope.Add&lt;ILogger&gt;(new FileLogger("blabla.txt"))
///   {
///     //SomeMethod will now log to our new logger (which will log to blabla.txt)
///     SomeMethod();
///   }
/// }
/// .
/// .
/// .
/// private void SomeMethod()
/// {
///    ILogger logger = ServiceScope.Get&lt;ILogger&gt;();
///    logger.Debug("Logging to whatever file our calling method decides");
/// }
/// </code></example>
/// <example>This is an example of how to pass the current ServiceScope to a
/// timer thread.
/// <code>
/// using(Timer timer = new Timer(TimerTick, ServiceScope.Current, 0000, 3000))
/// {
///   //do something useful here while the timer is busy
/// }
/// .
/// .
/// .
/// private void TimerTick(object passedScope)
/// {
///   ServiceScope.Current = passedScope as ServiceScope;
///   ServiceScope.Get&lt;ILogger&gt;().Info("Timer tick");
/// }
/// </code>
/// </example>
public sealed class ServiceScope : IDisposable
{
    public delegate T ServiceCreatorCallback<T>(ServiceScope scope);

    private static readonly object syncObject = new object();

    /// <summary>
    /// Pointer to the current <see cref="ServiceScope"/>.
    /// </summary>
    /// <remarks>
    /// This pointer is only static for the current thread.
    /// </remarks>
    [ThreadStatic]
    private static ServiceScope current;

    /// <summary>
    /// Pointer to the global <see cref="ServiceScope"/>.  This is the 
    /// </summary>
    private static ServiceScope global;

    private static bool isRunning = false;

    /// <summary>
    /// Pointer to the previous <see cref="ServiceScope"/>.  We need this pointer 
    /// to be able to restore the previous ServiceScope when the <see cref="Dispose()"/>
    /// method is called, and to ask it for services that we do not contain ourselves.
    /// </summary>
    private readonly ServiceScope oldInstance;

    /// <summary>
    /// Holds the list of services.
    /// </summary>
    private readonly Dictionary<Type, object> services;

    /// <summary>
    /// Keeps track whether the instance is already disposed
    /// </summary>
    private bool isDisposed = false;


    public ServiceScope(bool isFirst)
    {
        lock (syncObject)
        {
            bool updateGlobal = global == current;
            oldInstance = current;
            services = new Dictionary<Type, object>();
            current = this;
            if (updateGlobal)
            {
                global = this;
            }
            if (isFirst)
            {
                isRunning = true;
            }
        }
    }

    /// <summary>
    /// Creates a new <see cref="ServiceScope"/> instance and initialize it.
    /// </summary>
    public ServiceScope() : this(false) { }

    /// <summary>
    /// Gets or sets the current <see cref="ServiceScope"/>
    /// </summary>
    public static ServiceScope Current
    {
        get
        {
            if (current == null)
            {
                if (global == null)
                {
                    new ServiceScope();
                }
                current = global;
            }
            return current;
        }
        set { current = value; }
    }

    internal static bool IsRunning
    {
        get { return isRunning; }
    }

    #region IDisposable Members

    /// <summary>
    /// Restores the previous service context.
    /// </summary>
    /// <remarks>
    /// Use the using keyword to automatically call this method when the 
    /// service context goes out of scope.
    /// </remarks>
    public void Dispose()
    {
        Dispose(true);
    }

    #endregion

    ~ServiceScope()
    {
        Dispose(false);
    }

    /// <summary>
    /// Adds a new Service to the <see cref="ServiceScope"/>
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of service to add.</typeparam>
    /// <param name="service">The service implementation to add.</param>
    public static void Add<T>(T service) where T : class
    {
        Current.AddService<T>(service);
    }

    public static void Replace<T>(T service) where T : class
    {
        Current.ReplaceService<T>(service);
    }

    public static void Remove<T>() where T : class
    {
        Current.RemoveService<T>();
    }

    public static void RemoveAndDispose<T>() where T : class
    {
        T service = Get<T>(false);
        if (service != null)
        {
            ServiceScope.Remove<T>();
            IDisposable disposableService = service as IDisposable;
            if (disposableService != null)
                disposableService.Dispose();
        }
    }

    public static bool IsRegistered<T>() where T : class
    {
        return Current.IsServiceRegistered<T>();
    }

    /// <summary>
    /// Gets a service from the current <see cref="ServiceScope"/>
    /// </summary>
    /// <typeparam name="T">the type of the service to get.  This is typically
    /// (but not necessarily) an interface</typeparam>
    /// <returns>the service implementation.</returns>
    /// <exception cref="ServiceNotFoundException">when the requested service type is not found.</exception>
    public static T Get<T>() where T : class
    {
        return Current.GetService<T>(true);
    }

    /// <summary>
    /// Gets a service from the current <see cref="ServiceScope"/>
    /// </summary>
    /// <typeparam name="T">The type of the service to get. This is typically
    /// (but not necessarily) an interface</typeparam>
    /// <param name="throwIfNotFound">a <b>bool</b> indicating whether to throw a
    /// <see cref="ServiceNotFoundException"/> when the requested service is not found</param>
    /// <returns>the service implementation or <b>null</b> if the service is not available
    /// and <paramref name="throwIfNotFound"/> is false.</returns>
    /// <exception cref="ServiceNotFoundException">when <paramref="throwIfNotFound"/>
    /// is <b>true</b> andthe requested service type is not found.</exception>
    public static T Get<T>(bool throwIfNotFound) where T : class
    {
        return Current.GetService<T>(throwIfNotFound);
    }

    private void AddService<T>(T service) where T : class
    {
        services[typeof(T)] = service;
    }

    private void RemoveService<T>() where T : class
    {
        services.Remove(typeof(T));
    }

    private bool IsServiceRegistered<T>() where T : class
    {
        Type type = typeof(T);
        if (services.ContainsKey(type))
        {
            return true;
        }
        return false;
    }

    private T GetService<T>(bool throwIfNotFound) where T : class
    {
        Type type = typeof(T);
        if (services.ContainsKey(type))
        {
            ServiceCreatorCallback<T> callback = services[type] as ServiceCreatorCallback<T>;
            if (callback != null)
            {
                return callback(this);
            }
            return (T)services[type];
        }
        if (oldInstance == null)
        {
            //if (!(type == typeof(IPluginManager)))
            //{
            //    object newService = Get<IPluginManager>().RequestPluginItem<T>("/Services", type.Name,
            //        new FixedItemStateTracker(string.Format("ServiceScope.GetService<{0}>()", type.Name)));
            //    if (newService != null)
            //    {
            //        Add<T>((T)newService);
            //        return (T)newService;
            //    }
            //}

            if (throwIfNotFound)
            {
                throw new ServiceNotFoundException(type);
            }
            return null;
        }
        return oldInstance.GetService<T>(throwIfNotFound);
    }

    #region IDisposable implementation

    private void Dispose(bool alsoManaged)
    {
        if (isDisposed) //already disposed?
        {
            return;
        }
        if (alsoManaged)
        {
            bool updateGlobal = current == global;
            current = oldInstance; //set current scope to previous one
            if (updateGlobal)
            {
                global = current;
            }
        }
        isDisposed = true;
    }

    #endregion

    private void ReplaceService<T>(T service) where T : class
    {
        RemoveService<T>();
        AddService<T>(service);
    }

    internal static void Reset()
    {
        current = null;
        global = null;
        isRunning = false;
    }

    #region IStatus implementation

    public IList<string> GetStatus()
    {
        List<string> status = new List<string>();
        status.Add("== ServiceScope List Start");
        foreach (KeyValuePair<Type, object> service in services)
        {
            status.Add(String.Format("=== Service = {0}, {1}", service.Key.Name, service.Value.ToString()));
            IStatus info = service.Value as IStatus;
            if (info != null)
            {
                status.AddRange(info.GetStatus());
            }
        }
        status.Add("== ServiceScope List End");
        return status;
    }

    #endregion
}

