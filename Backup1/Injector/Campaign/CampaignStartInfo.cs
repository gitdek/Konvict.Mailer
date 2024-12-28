using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Injector
{

    public enum CampaignDeliveryMethod
    {
        KLocalDirect,
        KLocalPickup,
        MTA,
    }

    [Serializable]
    public class CampaignStartInfo
    {
        private Campaign _owner;
        private CampaignDeliveryMethod _deliveryMethod;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="deliveryMethod"></param>
        public CampaignStartInfo(Campaign owner, CampaignDeliveryMethod deliveryMethod)
        {
            _owner = owner;
            _deliveryMethod = deliveryMethod;
        }

        #region Properties Implementation

        public CampaignDeliveryMethod DeliveryMethod
        {
            get
            {
                return _deliveryMethod;
            }
            set
            {
                _deliveryMethod = value;
            }
        }

        public Campaign Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        #endregion

    }
}
