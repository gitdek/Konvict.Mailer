using System;
using System.Net;

namespace Injector.Net.Dns.Client
{
	/// <summary>
	/// A record class.
	/// </summary>
	[Serializable]
	public class DNS_rr_A : DNS_rr_base
	{
		private IPAddress m_IP  = null;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="ip">IP address.</param>
		/// <param name="ttl">TTL value.</param>
		public DNS_rr_A(IPAddress ip,int ttl) : base(QTYPE.A,ttl)
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
        public static DNS_rr_A Parse(byte[] reply,ref int offset,int rdLength,int ttl)
        {
            // IPv4 = byte byte byte byte

			byte[] ip = new byte[rdLength];
			Array.Copy(reply,offset,ip,0,rdLength);
            offset += rdLength;
            
			return new DNS_rr_A(new IPAddress(ip),ttl);
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
