﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Injector.Net.MIME
{
    /// <summary>
    /// This is base class for MIME header fields. Defined in RFC 2045 3.
    /// </summary>
    public abstract class MIME_h
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MIME_h()
        {
        }

                
        #region method ToString

        /// <summary>
        /// Returns header field as string.
        /// </summary>
        /// <returns>Returns header field as string.</returns>
        public override string ToString()
        {
            return ToString(null,null);
        }

        /// <summary>
        /// Returns header field as string.
        /// </summary>
        /// <param name="wordEncoder">8-bit words ecnoder. Value null means that words are not encoded.</param>
        /// <param name="parmetersCharset">Charset to use to encode 8-bit characters. Value null means parameters not encoded.
        /// If encoding needed, UTF-8 is strongly reccomended if not sure.</param>
        /// <returns>Returns header field as string.</returns>
        public abstract string ToString(MIME_Encoding_EncodedWord wordEncoder,Encoding parmetersCharset);

        #endregion


        #region Properties implementation

        /// <summary>
        /// Gets if this header field is modified since it has loaded.
        /// </summary>
        /// <remarks>All new added header fields has <b>IsModified = true</b>.</remarks>
        /// <exception cref="ObjectDisposedException">Is riased when this class is disposed and this property is accessed.</exception>
        public abstract bool IsModified
        {
            get;
        }

        /// <summary>
        /// Gets header field name. For example "Content-Type".
        /// </summary>
        public abstract string Name
        {
            get;
        }

        #endregion

    }
}
