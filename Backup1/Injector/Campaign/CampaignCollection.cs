using System;
using System.Collections.Generic;

namespace Injector
{
    /// <summary>
    /// Maintains a collection of <see cref="Campaign"/>s.
    /// </summary>
    public class CampaignCollection
    {
        protected List<Campaign> _campaigns;
        protected CampaignComparer _comparer;

        public CampaignCollection()
        {
            _campaigns = new List<Campaign>();
            _comparer = new CampaignComparer();
        }

        /// <summary>
        /// Adds a campaign to the CampaignCollection. Maintains sorting order while adding.
        /// </summary>
        /// <param name="campaign">Campaign to add to the CampaignCollection</param>
        public void Add(Campaign campaign)
        {
            if (_campaigns.Contains(campaign))
                throw new ArgumentException("Campaign is already in campaign list!");
            else
            {
                int index = _campaigns.BinarySearch(campaign, _comparer);
                if (index < 0)
                    _campaigns.Insert(~index, campaign);
                else
                    _campaigns.Insert(index, campaign);
            }
        }


        /// <summary>
        /// Removes a campaign from the CampaignCollection
        /// </summary>
        /// <param name="campaign">Campaign to remove from the CampaignCollection</param>
        public void Remove(Campaign campaign)
        {
            if (_campaigns.Contains(campaign))
                _campaigns.Remove(campaign);
        }

        /// <summary>
        /// Replaces a campaign in the CampaignCollection
        /// </summary>
        /// <param name="campaignID">ID of the task to replace</param>
        /// <param name="campaign">new campaign to replace the old one with</param>
        public void Replace(string campaignID, Campaign campaign)
        {
            Campaign oldCampaign = null;
            foreach (Campaign c in _campaigns)
            {
                if (c.ID == campaignID)
                {
                    oldCampaign = c;
                    break;
                }
            }
            if (oldCampaign != null)
                Remove(oldCampaign);
            Add(campaign);
        }

        /// <summary>
        /// Explicitly sorts the CampaignCollection.
        /// </summary>
        public void Sort()
        {
            _campaigns.Sort(_comparer);
        }

        /// <summary>
        /// Creates a clone of the CampaignCollection.
        /// </summary>
        /// <returns>a list of campaigns currently in the CampaignCollection</returns>
        public List<Campaign> Clone()
        {
            List<Campaign> campaigns = new List<Campaign>();
            foreach (Campaign c in _campaigns)
                campaigns.Add(c.Clone() as Campaign);
            return campaigns;
        }

        /// <summary>
        /// Property returning a list of campaigns currently in the CampaignCollection.
        /// </summary>
        //public IList<Campaign> Campaigns
        //{
        //    get { return new List<Campaign>(_campaigns); }
        //}

        // <summary>
        // Get a list of campaigns currently in CampaignCollection.
        // </summary>
        public List<Campaign> Campaigns
        {
            get { return _campaigns; }
        }
    }


    /// <summary>
    /// Compares two <see cref="Campaign"/>s with each other.
    /// </summary>
    public class CampaignComparer : IComparer<Campaign>
    {
        #region Public methods
        /// <summary>
        /// Compares two <see cref="Campaign"/>s with each other. See <see cref="IComparer<T>"/> for more details.
        /// </summary>
        /// <param name="x">first Campaign</param>
        /// <param name="y">second Campaign</param>
        /// <returns>comparisation result</returns>
        public int Compare(Campaign x, Campaign y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            else if (x == null)
            {
                return -1;
            }
            else if (y == null)
            {
                return 1;
            }
            else
            {
                if (x.CampaignStatistics.Processed < y.CampaignStatistics.Processed)
                {
                    return -1;
                }
                else if (x.CampaignStatistics.Processed > y.CampaignStatistics.Processed)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
    }
}
