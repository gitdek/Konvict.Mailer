using System;
using System.Collections.Generic;

namespace Injector
{
    #region Delegates

    /// <summary>
    /// General delegate for delegating to a different class.
    /// </summary>
    public delegate void PollDelegate(PollEventArgs e);

    #endregion


    [Serializable]
    public class PollCollection
    {
        private List<PollItem> _polls; //= new List<PollItem>();

        // Will throw an exception if we try to serialize
        // We can stop all running PollItem's first
        // and then set this to null to avoid that.
        [NonSerialized]
        public PollDelegate PollResult;

        public PollCollection()
        {
            _polls = new List<PollItem>();
        }

        public PollItem Add(string command, uint interval, PollExportAction_enum exportAction)
        {
            PollItem pollItem = new PollItem(this, command, interval, exportAction, Guid.NewGuid().ToString());
            _polls.Add(pollItem);
            return pollItem;
        }

        public PollItem Add(PollItem pollItem)
        {
            _polls.Add(pollItem);
            return pollItem;
        }

        public void Remove(PollItem pollItem)
        {
            if (_polls.Contains(pollItem))
                _polls.Remove(pollItem);
        }

        public void Replace(string ID, PollItem pollItem)
        {
            PollItem oldPoll = null;
            foreach (PollItem p in _polls)
            {
                if (p.ID == ID)
                {
                    oldPoll = p;
                    //break;
                }
            }
            if (oldPoll != null)
                Remove(oldPoll);
            Add(pollItem);
        }

        /// <summary>
        /// Creates a clone of the PollCollection.
        /// </summary>
        /// <returns>a list of polls currently in the PollCollection</returns>
        public IList<PollItem> Clone()
        {
            throw new NotImplementedException("PollCollection().Clone");
            //List<PollItem> polls = new List<PollItem>();
            //foreach (PollItem p in _polls)
            //    polls.Add(p.Clone() as PollItem);
            //return polls;
        }

        public void Start(List<Server> mailhosts)
        {
            for (int i = 0; i < _polls.Count; i++)
            {
                if (_polls[i].Enabled)
                {
                    //_polls[i].PollEvent += new EventHandler<PollEventArgs>(OnPollResult);
                    _polls[i].Start(mailhosts);
                }
            }
        }

        public void Stop()
        {
            for (int i = 0; i < _polls.Count; i++)
            {
                if (_polls[i].Running)
                {
                    _polls[i].Stop();
                }
            }
        }

        private void OnPollResult(object sender, PollEventArgs e)
        {
            if (this.PollResult != null)
                this.PollResult(e);
        }

        #region Properties Implementation

        /// <summary>
        /// Property returning a list of PollItems currently in the PollCollection.
        /// </summary>
        public List<PollItem> Polls
        {
            //get
            //{
            //    return _polls;

            //}
            get { return _polls; }
            set
            {
                if (_polls == value)
                    return;
                _polls = value;
            }
            //get { return new List<PollItem>(_polls); }
        }

        public PollItem this[int index]
        {
            get { return _polls[index]; }
        }

        public PollItem this[string ID]
        {
            get
            {
                foreach (PollItem p in _polls)
                {
                    if (p.ID == ID)
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        #endregion

    }
}
