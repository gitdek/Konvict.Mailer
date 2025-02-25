using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Injector.Net.Dns.Client
{
    /// <summary>
    /// DNS AAAA resource record.
    /// </summary>
    public class DNS_rr_AAAA : DNS_rr_base
    {
        private IPAddress m_IP  = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="ip">IP address.</param>
        /// <param name="ttl">Time to live in seconds.</param>
        public DNS_rr_AAAA(IPAddress ip,int ttl) : base(QTYPE.AAAA,ttl)
        {
            m_IP = ip;
        }


        #region static method Parse

        /// <summary>
        /// Parses resource record from reply data.
        /// </summary>
        /// <param name="reply">DNS server reply data.</param>
        /// <param name="offset">Current offset in reply data.</param>
        /// <param name="rdLength">Resource record data length.</param>
        /// <param name="ttl">Time to live in seconds.</param>
        public static DNS_rr_AAAA Parse(byte[] reply,ref int offset,int rdLength,int ttl)
        {
            // IPv6 = 16xbyte

			byte[] ip = new byte[rdLength];
			Array.Copy(reply,offset,ip,0,rdLength);
	
			return new DNS_rr_AAAA(new IPAddress(ip),ttl);	
        }

        #endregion


        #region Properties Implementation

        /// <summary>
		/// Gets host IP address.
		/// </summary>
		public IPAddress IP
		{
			get{ return m_IP; }
		}

        #endregion

    }
}
