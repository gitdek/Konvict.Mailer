﻿using System;
using System.Collections.Generic;
using System.Text;

using Injector.Net.MIME;

namespace Injector.Net.Mail
{    
    /// <summary>
    /// This class represents "mailbox" address. Defined in RFC 5322 3.4.
    /// </summary>
    /// <example>
    /// <code>
    /// RFC 5322 3.4.
    ///     mailbox    = name-addr / addr-spec
    ///     name-addr  = [display-name] angle-addr
    ///     angle-addr = [CFWS] "&lt;" addr-spec "&gt;" [CFWS]
    /// </code>
    /// </example>
    public class Mail_t_Mailbox : Mail_t_Address
    {
        private string m_DisplayName = null;
        private string m_Address     = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="displayName">Display name. Value null means not specified.</param>
        /// <param name="address">Email address.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>address</b> is null reference.</exception>
        public Mail_t_Mailbox(string displayName,string address)
        {
            if(address == null){
                throw new ArgumentNullException("address");
            }

            m_DisplayName = displayName;
            m_Address     = address;
        }


        #region method override ToString

        /// <summary>
        /// Returns mailbox as string.
        /// </summary>
        /// <returns>Returns mailbox as string.</returns>
        public override string ToString()
        {
            return ToString(null);
        }

        /// <summary>
        /// Returns address as string value.
        /// </summary>
        /// <param name="wordEncoder">8-bit words ecnoder. Value null means that words are not encoded.</param>
        /// <returns>Returns address as string value.</returns>
        public override string ToString(MIME_Encoding_EncodedWord wordEncoder)
        {
            if(string.IsNullOrEmpty(m_DisplayName)){
                return "<" + m_Address + ">";
            }
            else{
                if(MIME_Encoding_EncodedWord.MustEncode(m_DisplayName)){
                    return wordEncoder.Encode(m_DisplayName) + " " + "<" + m_Address + ">";
                }
                else{
                    return TextUtils.QuoteString(m_DisplayName) + " " + "<" + m_Address + ">";
                }
            }
        }

        #endregion


        #region Properties implementation

        /// <summary>
        /// Gets display name. Value null means not specified.
        /// </summary>
        public string DisplayName
        {
            get{ return m_DisplayName; }
        }

        /// <summary>
        /// Gets address.
        /// </summary>
        public string Address
        {
            get{ return m_Address; }
        }

        #endregion
    }
}
