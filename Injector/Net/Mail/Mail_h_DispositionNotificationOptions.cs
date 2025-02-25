﻿using System;
using System.Collections.Generic;
using System.Text;

using Injector.Net.MIME;

namespace Injector.Net.Mail
{
    /// <summary>
    /// Represents "Disposition-Notification-Options:" header. Defined in RFC 2298 2.2.
    /// </summary>
    public class Mail_h_DispositionNotificationOptions : MIME_h
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Mail_h_DispositionNotificationOptions()
        {
        }

        /*
            Disposition-Notification-Options    = "Disposition-Notification-Options" ":" disposition-notification-parameters
            disposition-notification-parameters = parameter *(";" parameter)

            parameter  = attribute "=" importance "," 1#value
            importance = "required" / "optional"
        */

        
        #region override method ToString

        /// <summary>
        /// Returns header field as string.
        /// </summary>
        /// <param name="wordEncoder">8-bit words ecnoder. Value null means that words are not encoded.</param>
        /// <param name="parmetersCharset">Charset to use to encode 8-bit characters. Value null means parameters not encoded.</param>
        /// <returns>Returns header field as string.</returns>
        public override string ToString(MIME_Encoding_EncodedWord wordEncoder,Encoding parmetersCharset)
        {
            return "TODO:";
        }

        #endregion


        #region Properties implementation

        /// <summary>
        /// Gets if this header field is modified since it has loaded.
        /// </summary>
        /// <remarks>All new added header fields has <b>IsModified = true</b>.</remarks>
        /// <exception cref="ObjectDisposedException">Is riased when this class is disposed and this property is accessed.</exception>
        public override bool IsModified
        {
            get{ return true; } //m_pAddresses.IsModified; }
        }

        /// <summary>
        /// Gets header field name. For example "Sender".
        /// </summary>
        public override string Name
        {
            get{ return "Disposition-Notification-Options"; }
        }

        /// <summary>
        /// Gets or sets mailbox address.
        /// </summary>
        public string Address
        {
            get{ return "TODO:"; }
        }

        #endregion
    }
}
