using System.Collections.Generic;
using System.Collections;

using Injector.Core.Services;
using Injector.Core.Threading;
using Injector.Core.TaskScheduler;
using System.Data;
using Injector.Core.Services.Threading;
using System;
using Injector.Core.Logging;

namespace Injector
{
    public class CampaignManager : ICampaignManager
    {
        #region Delegates
        /// <summary>
        /// General delegate for delegating informational, warning, error and debug logging to a different class
        /// </summary>
        /// <param name="format">message to log</param>
        /// <param name="args">objects to format into the message</param>
        public delegate void LoggerDelegate(string format, params object[] args);

        #endregion

        #region Fields

        private PollCollection _polls = new PollCollection();

        private CampaignCollection _campaigns = new CampaignCollection();

        private ServerCollection _servers = new ServerCollection();



        /// <summary>
        /// mutex object to serialize access to the registered campaigns
        /// </summary>
        private object _campaignMutex = new object();



        // decoupled logging delegates
        /// <summary>
        /// Logging delegate for information log messages
        /// </summary>
        public LoggerDelegate InfoLog;
        /// <summary>
        /// Logging delegate for warning log messages
        /// </summary>
        public LoggerDelegate WarnLog;
        /// <summary>
        /// Logging delegate for error log messages
        /// </summary>
        public LoggerDelegate ErrorLog;
        /// <summary>
        /// Logging delegate for debug log messages
        /// </summary>
        public LoggerDelegate DebugLog;

        #endregion

        #region Constructor

        public CampaignManager()
        {

        }

        #endregion

        #region Public methods

        #region Campaign handling methods

        public List<Campaign> GetScheduledTasks()
        {
            List<Campaign> allCampaigns;
            List<Campaign> campaigns = new List<Campaign>();

            allCampaigns = this.GetCampaigns(true);

            foreach (Campaign campaign in allCampaigns)
                if (campaign.EnableSchedule)
                    campaigns.Add(campaign);

            return campaigns;
        }

        #region method GetCampaignStatus

        public System.Data.DataSet GetCampaignsStatus()
        {
            return GetCampaignsStatus(true);
        }
        
        public System.Data.DataSet GetCampaignsStatus(bool runningOnly)
        {
            List<Campaign> campaigns = runningOnly ? this.GetRunning() : this.GetCampaigns(false);

            DataSet ds = new DataSet("dsCampaignStatus");
            ds.Tables.Add("Campaign");
            ds.Tables["Campaign"].Columns.Add("ID");
            ds.Tables["Campaign"].Columns.Add("Name");
            ds.Tables["Campaign"].Columns.Add("Status");
            ds.Tables["Campaign"].Columns.Add("StartTime");
            ds.Tables["Campaign"].Columns.Add("DeliveryMethod");
            ds.Tables["Campaign"].Columns.Add("Recipients_Processed");
            ds.Tables["Campaign"].Columns.Add("Recipients_Failed");
            ds.Tables["Campaign"].Columns.Add("TimeElapsed");
            ds.Tables["Campaign"].Columns.Add("MailingLists_Processed");
            ds.Tables["Campaign"].Columns.Add("Seeds_Processed");
            ds.Tables["Campaign"].Columns.Add("Scheduler_Enabled");
            ds.Tables["Campaign"].Columns.Add("Scheduler_NextRun");
            ds.Tables["Campaign"].Columns.Add("Scheduler_LastRun");
            ds.Tables["Campaign"].Columns.Add("Total_Opens");
            ds.Tables["Campaign"].Columns.Add("Total_OpenRate");
            ds.Tables["Campaign"].Columns.Add("Total_ClickThroughs");
            ds.Tables["Campaign"].Columns.Add("Total_ClickThroughRate");
            ds.Tables["Campaign"].Columns.Add("Unsubscribe_Requests");

            foreach (Campaign campaign in campaigns)
            {
                int proccessed = (int)campaign.CampaignStatistics.Processed;
                int ttlOpen = campaign.CampaignStatistics.TrackingOpens;
                int ttlCT = campaign.CampaignStatistics.TrackingClickThrough;
                int ttlUnsub = campaign.CampaignStatistics.TrackingUnsubscribes;

                double rateOpen = 0.0;
                double rateCT = 0.0;
                double rateUnsub = 0.0; // not being utilized.
                
                if (proccessed > 0)
                {
                    rateOpen = (ttlOpen * 100.0) / proccessed;
                    rateCT = (ttlCT * 100.0) / proccessed;
                    rateUnsub = (ttlUnsub * 100.0) / proccessed;
                }

                DataRow dr = ds.Tables["Campaign"].NewRow();
                dr["ID"] = campaign.ID;
                dr["Name"] = campaign.Name;
                dr["Status"] = campaign.Running ? "Running" : "Idle";
                dr["StartTime"] = campaign.Running ? campaign.StartTime.ToString() : string.Empty;
                dr["DeliveryMethod"] = "MTA";
                dr["Recipients_Processed"] = string.Format("{0:N0}", campaign.CampaignStatistics.Processed);
                dr["Recipients_Failed"] = string.Format("{0:N0}", campaign.CampaignStatistics.Failed);
                dr["TimeElapsed"] = string.Format("{0}", campaign.Running ? System.DateTime.Now.Subtract(campaign.StartTime).ToString() : "None");
                dr["MailingLists_Processed"] = string.Format("{0:N0}", campaign.CampaignStatistics.RecipientListProcessed);
                dr["Seeds_Processed"] = string.Format("{0:N0}", campaign.CampaignStatistics.Processedseed);
                dr["Scheduler_Enabled"] = campaign.EnableSchedule ? "Yes" : "No";
                dr["Scheduler_NextRun"] = campaign.Running && campaign.EnableSchedule ? campaign.StartTime.Add(campaign.Schedule.Interval).ToString() : "Disabled";
                dr["Scheduler_LastRun"] = string.Empty;
                dr["Total_Opens"] = string.Format("{0:N0} ({1:N2}%)", ttlOpen, rateOpen);
                dr["Total_OpenRate"] = string.Format("{0:N2}", rateOpen);
                dr["Total_ClickThroughs"] = string.Format("{0:N0} ({1:N2}%)", ttlCT, rateCT);
                dr["Total_ClickThroughRate"] = string.Format("{0:N2}", rateCT);
                dr["Unsubscribe_Requests"] = string.Format("{0:N0} ({1:N2}%)", ttlUnsub, rateUnsub);
                ds.Tables["Campaign"].Rows.Add(dr);
            }
            return ds;
        }

        #endregion


        /// <summary>
        /// Gets a list of campaigns in the CampaignCollection which are running.
        /// </summary>
        /// <returns></returns>
        public List<Campaign> GetRunning()
        {
            List<Campaign> allCampaigns;
            List<Campaign> campaigns = new List<Campaign>();

            allCampaigns = this.GetCampaigns(false);

            foreach (Campaign campaign in allCampaigns)
                if (campaign.Running) campaigns.Add(campaign);

            return campaigns;
        }

        public void StopAll()
        {
            List<Campaign> allCampaigns = _campaigns.Campaigns;

            foreach (Campaign campaign in allCampaigns)
                this.StopCampaign(campaign.ID);
        }

        public void StartAll()
        {
            List<Campaign> allCampaigns = _campaigns.Campaigns;

            foreach (Campaign campaign in allCampaigns)
                this.StartCampaign(campaign.ID);
        }

        public void StartCampaign(string campaignId)
        {
            Campaign campaign = GetCampaign(campaignId);
            if (!campaign.Running)
            {
                campaign.OnCampaignCompleted += this.OnCampaignCompleted;

                //ITaskScheduler taskScheduler = ServiceScope.Get<ITaskScheduler>();
                //Task task = new Task(campaign.ID, campaign.Schedule, campaign.Occurrence, campaign.Expiration, false);
                //taskScheduler.AddTask(task);

                ThreadPoolStartInfo tpsi = new ThreadPoolStartInfo(campaign.ID, 5, 100, 30);
                tpsi.DefaultThreadPriority = System.Threading.ThreadPriority.AboveNormal;
                IThreadPool pool = new ThreadPool(tpsi);

                //IThreadPool pool = ServiceScope.Get<IThreadPool>();

                if (campaign.EnableSchedule)
                {
                    IntervalWork iWork = new IntervalWork(new DoWorkHandler(campaign.Start), campaign.Schedule.Interval);
                    pool.AddIntervalWork(iWork, true);
                }
                else
                {
                    //Work work = new Work(new DoWorkHandler(campaign.Start));
                    //pool.Add(work);
                    System.Threading.Thread thread = new System.Threading.Thread(campaign.Start);
                    thread.Priority = System.Threading.ThreadPriority.AboveNormal;
                    thread.Start();
                }
            }
        }

        public void StopCampaign(string campaignId)
        {
            Campaign campaign = GetCampaign(campaignId);
            if (campaign.Running)
                campaign.Stop();
        }

        public void StartCampaign(List<Campaign> campaigns)
        {
            foreach (Campaign campaign in campaigns)
                this.StartCampaign(campaign.ID);
        }

        public void StopCampaign(List<Campaign> campaigns)
        {
            foreach (Campaign campaign in campaigns)
                this.StopCampaign(campaign.ID);
        }


        /// <summary>
        /// Add a new campaign to the CampaignCollection.
        /// </summary>
        /// <param name="newCampaign"></param>
        /// <returns>The campaign ID.</returns>
        public string AddCampaign(Campaign newCampaign)
        {
            _campaigns.Add(newCampaign);
            return newCampaign.ID;
        }


        public void UpdateCampaign(string campaignId, Campaign updatedCampaign)
        {
            updatedCampaign.ID = campaignId;
            _campaigns.Replace(campaignId, updatedCampaign);
            //SendMessage(new TaskMessage(updatedTask, TaskMessageType.CHANGED));
        }

        public void RemoveCampaign(string campaignId)
        {
                Campaign campaign = null;
                foreach (Campaign c in _campaigns.Campaigns)
                {
                    if (c.ID == campaignId)
                        campaign = c;
                }
                if (campaign != null)
                {
                    _campaigns.Campaigns.Remove(campaign);
                    //SendMessage(new TaskMessage(campaign, TaskMessageType.DELETED));
                }
        }

        public Campaign GetCampaign(string campaignId)
        {
            foreach (Campaign campaign in _campaigns.Campaigns)
                if (campaign.ID == campaignId)
                    return campaign;
            return null;
        }

        public Campaign GetCampaignByName(string campaignName)
        {
            foreach (Campaign campaign in _campaigns.Campaigns)
                if (campaign.Name.Equals(campaignName))
                    return campaign;
            return null;
        }

        /// <summary>
        /// Gets a list of campaigns currently in the CampaignCollection.
        /// </summary>
        /// <returns></returns>
        public List<Campaign> GetCampaigns()
        {
            return GetCampaigns(false);
        }

        /// <summary>
        /// Gets a list of campaigns currently in the CampaignCollection.
        /// </summary>
        /// <param name="cloned"><see langword="true"/> to return a cloned list, otherwise <see langword="false"/>(default).</param>
        /// <returns></returns>
        public List<Campaign> GetCampaigns(bool cloned)
        {
            List<Campaign> allCampaigns;
            List<Campaign> campaigns = new List<Campaign>();

            allCampaigns = cloned ? _campaigns.Clone() : _campaigns.Campaigns;
            
            foreach (Campaign campaign in allCampaigns)
                campaigns.Add(campaign);

            return campaigns;
        }

        /// <summary>
        /// Replaces the Campaign of a specified ID with the new Campaign. If the Campaign does not exist, it is added.
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="newCampaign"></param>
        public void ReplaceCampaign(string campaignId, Campaign newCampaign)
        {
            _campaigns.Replace(campaignId, newCampaign);
        }

        #endregion


        #region Server handling methods


        public List<Server> GetServers()
        {
            return GetServers(false);
        }


        public List<Server> GetServers(bool cloned)
        {
            //List<Server> allServers;
            //List<Server> servers = new List<Server>();

            ////allPolls = cloned ? _polls.Clone() : _polls.Polls;
            //allServers = _servers.Servers;

            //foreach (Server server in allServers)
            //    servers.Add(server);

            //return servers;

            return _servers.Servers;
        }

        /// <summary>
        /// Adds a new Server to the ServerCollection.
        /// </summary>
        /// <param name="newServer"></param>
        /// <returns></returns>
        public string AddServer(Server newServer)
        {
            _servers.Add(newServer);
            return newServer.ID;
        }

        public void RemoveServer(string serverId)
        {
            Server server = null;

            for (int k = 0; k < _servers.Servers.Count; k++)
            {
                if (_servers.Servers[k].ID.ToString() == serverId.ToString())
                    server = _servers.Servers[k];
            }

            if (server != null)
            {
                _servers.Servers.Remove(server);
                //SendMessage(new TaskMessage(campaign, TaskMessageType.DELETED));
            }
        }

        /// <summary>
        /// Gets the Server with the specified ID.
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public Server GetServer(string serverId)
        {
            List<Server> allServers = _servers.Servers;

            foreach (Server server in allServers)
                if (server.ID == serverId)
                    return server;

            return null;
        }

        /// <summary>
        /// Replaces the server of a specified ID with the new server. If the server does not exist, it is added.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="newServer"></param>
        public void ReplaceServer(string ID, Server newServer)
        {
            _servers.Replace(ID, newServer);
        }

        #endregion


        #region Poll handling methods

        public void StartAllPoll()
        {
            List<Server> allServers = this.GetServers();
            //List<PollItem> allPolls = this.GetPolls();

             _polls.PollResult += OnPollResult;
            _polls.Start(allServers); // this will set the delegate, so all information is passed forward.
        }

        public void StopAllPoll()
        {
            _polls.Stop();
        }

        public void StartPoll(string pollId)
        {
            PollItem poll = GetPoll(pollId);
            List<Server> allServers = this.GetServers();
            if (!poll.Running)
                poll.Start(allServers);
        }

        public void StopPoll(string pollId)
        {
            PollItem poll = GetPoll(pollId);
            if (poll.Running)
                poll.Stop();
        }

        public void StartPoll(List<PollItem> polls)
        {
            foreach (PollItem poll in polls)
                this.StartPoll(poll.ID);
        }

        public void StopPoll(List<PollItem> polls)
        {
            foreach (PollItem poll in polls)
                this.StopPoll(poll.ID);
        }

        public List<PollItem> GetPolls()
        {
            return GetPolls(false);
        }

        public List<PollItem> GetPolls(bool cloned)
        {
            //List<PollItem> allPolls;
            //List<PollItem> polls = new List<PollItem>();

            ////allPolls = cloned ? _polls.Clone() : _polls.Polls;
            //allPolls = _polls.Polls;

            //foreach (PollItem poll in allPolls)
            //    polls.Add(poll);

            //return polls;

            return _polls.Polls;
        }

        /// <summary>
        /// Adds a new PollItem to the PollCollection.
        /// </summary>
        /// <param name="newPoll"></param>
        /// <returns></returns>
        public string AddPoll(PollItem newPoll)
        {
            _polls.Add(newPoll);
            return newPoll.ID;
        }

        public void RemovePoll(string pollId)
        {
            PollItem poll = null;
            //List<PollItem> allPolls = _polls.Polls;
            lock (_polls)
            {
                foreach (PollItem p in _polls.Polls)
                {
                    if (p.ID == pollId)
                        poll = p;
                }
                if (poll != null)
                {
                    _polls.Polls.Remove(poll);
                    //SendMessage(new TaskMessage(campaign, TaskMessageType.DELETED));
                }
            }
        }

        public void RemovePolls(List<PollItem> polls)
        {
            foreach (PollItem poll in polls)
            {
                _polls.Remove(poll);
            }
        }

        public void RemoveAllPoll()
        {
            _polls.Polls.Clear();
        }

        /// <summary>
        /// Gets the PollItem with the specified ID.
        /// </summary>
        /// <param name="pollId"></param>
        /// <returns></returns>
        public PollItem GetPoll(string pollId)
        {
            List<PollItem> allPolls = _polls.Polls;

            foreach (PollItem poll in allPolls)
                if (poll.ID == pollId)
                    return poll;

            return null;
        }

        /// <summary>
        /// Replaces the poll of a specified ID with the new poll. If the poll does not exist, it is added.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="newPoll"></param>
        public void ReplacePoll(string ID, PollItem newPoll)
        {
            _polls.Replace(ID, newPoll);
        }

        public void OnPollResult(PollEventArgs e)
        {
            //ILogger logger = ServiceScope.Get<ILogger>();

            //PollItem poll = this.GetPoll(e.Id);

            //if (e.ErrorOutput != null && e.ErrorOutput.Length > 0)
            //    logger.Debug("PollItem: Error occured on {0}.", e.RemoteHost);

            //if (e.StandardOutput != null && e.StandardOutput.Length > 0)
            //    logger.Debug("PollItem: Receive OK from {0}.", e.RemoteHost);

            //logger.Debug("\n----\n" + e.StandardOutput);
        }


        #endregion

        #endregion

        #region Private methods

        private void OnCampaignCompleted(string id, params object[] args)
        {
            ILogger logger = ServiceScope.Get<ILogger>();
            logger.Debug("Campaign '{0}' completed !", id);

            Campaign campaign = this.GetCampaign(id);
            if (campaign != null)
            {
                

            }

        }

        private void ProcessCampaign(Campaign campaign)
        {
            if (!campaign.Running)
            {
                campaign.Start();
            }
        }

        private void SaveChanges()
        {
        }

        #region Logging methods

        private void LogInfo(string format, params object[] args)
        {
            if (InfoLog != null)
                InfoLog(format, args);
        }

        private void LogWarn(string format, params object[] args)
        {
            if (WarnLog != null)
                WarnLog(format, args);
        }

        private void LogError(string format, params object[] args)
        {
            if (ErrorLog != null)
                ErrorLog(format, args);
        }

        private void LogDebug(string format, params object[] args)
        {
            if (DebugLog != null)
                DebugLog(format, args);
        }

        #endregion

        #endregion

    }
}
