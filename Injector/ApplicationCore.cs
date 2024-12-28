using System;
using System.Diagnostics;
using System.Windows.Forms;

using Injector.Core.Logging;
using Injector.Core.PathManager;
using Injector.Core.Services.Logging;
using Injector.Core.Services.PathManager;
using Injector.Core.Services.Settings;
using Injector.Core.Settings;
using Injector.Core.Threading;
using Injector.Core.TaskScheduler;
using Injector.Core.Services.Messaging;
using Injector.Core.Messaging;
using Injector.Core;
using Injector.Utilities;
using Injector.Utilities.CommandLine;

namespace Injector
{
    public static class ApplicationCore
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(params string[] args)
        {
            System.Threading.Thread.CurrentThread.Name = "Konvict Main Thread";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Parse Command Line options
            CommandLineOptions kArgs = new CommandLineOptions();
            try
            {
                CommandLine.Parse(args, kArgs);
            }
            catch (ArgumentException)
            {
                kArgs.DisplayOptions();
                return;
            }


            using (new ServiceScope(true)) //This is the first servicescope
            {
                IPathManager pathManager = new PathManager();
                ServiceScope.Add<IPathManager>(pathManager);

                bool logMethods = kArgs.IsOption(CommandLineOptions.Option.LogMethods);
                LogLevel level = LogLevel.All;
                if (kArgs.IsOption(CommandLineOptions.Option.LogLevel))
                    level = (LogLevel)kArgs.GetOption(CommandLineOptions.Option.LogLevel);

                //// Check if user wants to override the default Application Data location.
                //if (kArgs.IsOption(CommandLineOptions.Option.Data))
                //    pathManager.ReplacePath("DATA", (string)kArgs.GetOption(CommandLineOptions.Option.Data));

#if DEBUG
                ILogger logger = new ConsoleLogger(level, logMethods);
#else
            FileLogger.DeleteLogFiles(pathManager.GetPath(@"<LOG>\"), "*.log");
            ILogger logger = FileLogger.CreateFileLogger(pathManager.GetPath(@"<LOG>\Konvict.log"), level, logMethods);
#endif
                logger.Info("ApplicationCore: Launching in AppDomain {0}...", AppDomain.CurrentDomain.FriendlyName);

                logger.Debug("ApplicationCore: Registering ILogger");
                ServiceScope.Add<ILogger>(logger);

                logger.Debug("ApplicationCore: Registering IThreadPool");
                Injector.Core.Services.Threading.ThreadPool pool = new Injector.Core.Services.Threading.ThreadPool();
                pool.ErrorLog += ServiceScope.Get<ILogger>().Error;
                pool.WarnLog += ServiceScope.Get<ILogger>().Warn;
                pool.InfoLog += ServiceScope.Get<ILogger>().Info;
                pool.DebugLog += ServiceScope.Get<ILogger>().Debug;
                ServiceScope.Add<IThreadPool>(pool);

                logger.Debug("ApplicationCore: Registering IMessageBroker");
                ServiceScope.Add<IMessageBroker>(new MessageBroker());

                logger.Debug("ApplicationCore: Registering ISettingsManager");
                ServiceScope.Add<ISettingsManager>(new SettingsManager());

                logger.Debug("ApplicationCore: Registering ICampaignManager");
                CampaignManager mgr = new CampaignManager();
                mgr.ErrorLog += ServiceScope.Get<ILogger>().Error;
                mgr.WarnLog += ServiceScope.Get<ILogger>().Warn;
                mgr.InfoLog += ServiceScope.Get<ILogger>().Info;
                mgr.DebugLog += ServiceScope.Get<ILogger>().Debug;
                ServiceScope.Add<ICampaignManager>(mgr);

                logger.Debug("ApplicationCore: Registering ITaskScheduler");
                ServiceScope.Add<ITaskScheduler>(new Injector.Core.Services.TaskScheduler.TaskScheduler());


#if !DEBUG
                // In Debug mode these will be left unhandled.
                try
                {
#endif
                    logger.Debug("ApplicationCore: Setting ErrorFilePath");
                    ErrorEx.ErrorFilePath = pathManager.GetPath(@"<APPLICATION_ROOT>\");

                    bool createdNew = true;
                    using (System.Threading.Mutex mutex = new System.Threading.Mutex(true, "Injector", out createdNew))
                    {
                        if (createdNew)
                        {
                            SystemMessaging.SendSystemMessage(SystemMessaging.MessageType.SystemStarted);

                            Application.Run(new MainForm());

                            SystemMessaging.SendSystemMessage(SystemMessaging.MessageType.SystemShutdown);

                            ApplicationCore.DisposeCoreServices();
                        }
                        else
                        {
                            Process current = Process.GetCurrentProcess();
                            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                            {
                                if (process.Id != current.Id)
                                {
                                    uint hWnd = (uint)process.MainWindowHandle.ToInt32();

                                    Win32API.WindowPlacement windowPlacement;
                                    Win32API.GetWindowPlacement(hWnd, out windowPlacement);

                                    switch (windowPlacement.showCmd)
                                    {
                                        case Win32API.SW_HIDE:           //Window is hidden
                                            Win32API.ShowWindow(hWnd, Win32API.SW_RESTORE);
                                            break;
                                        case Win32API.SW_SHOWMINIMIZED:  //Window is minimized
                                            // if the window is minimized, then we need to restore it to its 
                                            // previous size. we also take into account whether it was 
                                            // previously maximized. 
                                            int showCmd = (windowPlacement.flags == Win32API.WPF_RESTORETOMAXIMIZED) ? Win32API.SW_SHOWMAXIMIZED : Win32API.SW_SHOWNORMAL;
                                            Win32API.ShowWindow(hWnd, showCmd);
                                            break;
                                        default:
                                            // if it's not minimized, then we just call SetForegroundWindow to 
                                            // bring it to the front. 
                                            Win32API.SetForegroundWindow(hWnd);
                                            break;
                                    }

                                    //Win32API.SetForegroundWindow(process.MainWindowHandle);
                                    break;
                                }
                            }
                        }
                    }
#if !DEBUG
                }
                catch (Exception ex)
                {
                    CrashLogger crash = new CrashLogger(pathManager.GetPath("<LOG>"));
                    crash.CreateLog(ex);
                }
#endif
            }
        }

        public static void RegisterCoreServices()
        {



        }

        public static void DisposeCoreServices()
        {
            ILogger logger = ServiceScope.Get<ILogger>();

            logger.Debug("ApplicationCore: Removing IPathManager");
            ServiceScope.RemoveAndDispose<IPathManager>();

            logger.Debug("ApplicationCore: Removing ILogger");
            ServiceScope.RemoveAndDispose<ILogger>();

            logger.Debug("ApplicationCore: Removing IThreadPool");
            ServiceScope.RemoveAndDispose<IThreadPool>();

            logger.Debug("ApplicationCore: Removing IMessageBroker");
            ServiceScope.RemoveAndDispose<IMessageBroker>();

            logger.Debug("ApplicationCore: Removing ISettingsManager");
            ServiceScope.RemoveAndDispose<ISettingsManager>();

            logger.Debug("ApplicationCore: Removing ITaskScheduler");
            ServiceScope.RemoveAndDispose<ITaskScheduler>();

            logger.Debug("ApplicationCore: Removing ICampaignManager");
            ServiceScope.RemoveAndDispose<ICampaignManager>();
        }
    }
}
