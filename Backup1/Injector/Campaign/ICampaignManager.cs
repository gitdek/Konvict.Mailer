using System;
using System.Collections.Generic;

namespace Injector
{
    interface ICampaignManager
    {
        string AddCampaign(Injector.Campaign newCampaign);
        string AddPoll(Injector.PollItem newPoll);
        string AddServer(Injector.Server newServer);
        Injector.Campaign GetCampaign(string campaignId);
        Injector.Campaign GetCampaignByName(string campaignName);
        System.Collections.Generic.List<Injector.Campaign> GetCampaigns();
        System.Collections.Generic.List<Injector.Campaign> GetCampaigns(bool cloned);
        System.Data.DataSet GetCampaignsStatus(bool runningOnly);
        System.Data.DataSet GetCampaignsStatus();
        Injector.PollItem GetPoll(string pollId);
        System.Collections.Generic.List<Injector.PollItem> GetPolls();
        System.Collections.Generic.List<Injector.PollItem> GetPolls(bool cloned);
        System.Collections.Generic.List<Injector.Campaign> GetRunning();
        System.Collections.Generic.List<Injector.Campaign> GetScheduledTasks();
        Injector.Server GetServer(string serverId);
        System.Collections.Generic.List<Injector.Server> GetServers();
        System.Collections.Generic.List<Injector.Server> GetServers(bool cloned);
        void OnPollResult(Injector.PollEventArgs e);
        void RemoveCampaign(string campaignId);
        void RemovePoll(string pollId);
        void RemoveServer(string serverId);
        void ReplaceCampaign(string campaignId, Injector.Campaign newCampaign);
        void ReplacePoll(string ID, Injector.PollItem newPoll);
        void ReplaceServer(string ID, Injector.Server newServer);
        void StartAll();
        void StartAllPoll();
        void StartCampaign(string campaignId);
        void StartCampaign(System.Collections.Generic.List<Injector.Campaign> campaigns);
        void StartPoll(string pollId);
        void StartPoll(System.Collections.Generic.List<Injector.PollItem> polls);
        void StopAll();
        void StopAllPoll();
        void StopCampaign(System.Collections.Generic.List<Injector.Campaign> campaigns);
        void StopCampaign(string campaignId);
        void StopPoll(System.Collections.Generic.List<Injector.PollItem> polls);
        void StopPoll(string pollId);
        void UpdateCampaign(string campaignId, Injector.Campaign updatedCampaign);
        void RemovePolls(List<PollItem> polls);
        void RemoveAllPoll();

    }
}
