﻿using System;
using System.Collections.Generic;
using System.Text;

using Injector.Net.IO;

namespace Injector.Net.MIME
{
    /// <summary>
    /// This class represents MIME application/xxx bodies. Defined in RFC 2046 4.2.
    /// </summary>
    /// <remarks>
    /// The "application" media type is to be used for discrete data which do
    /// not fit in any of the other categories, and particularly for data to
    /// be processed by some type of application program.
    /// </remarks>
    public class MIME_b_Application : MIME_b_SinglepartBase
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="mediaType">MIME media type.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>mediaType</b> is null reference.</exception>
        public MIME_b_Application(string mediaType) : base(mediaType)
        {
        }


        #region static method Parse

        /// <summary>
        /// Parses body from the specified stream
        /// </summary>
        /// <param name="owner">Owner MIME entity.</param>
        /// <param name="mediaType">MIME media type. For example: text/plain.</param>
        /// <param name="stream">Stream from where to read body.</param>
        /// <returns>Returns parsed body.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <b>stream</b>, <b>mediaType</b> or <b>strean</b> is null reference.</exception>
        /// <exception cref="ParseException">Is raised when any parsing errors.</exception>
        protected static new MIME_b Parse(MIME_Entity owner,string mediaType,SmartStream stream)
        {
            if(owner == null){
                throw new ArgumentNullException("owner");
            }
            if(mediaType == null){
                throw new ArgumentNullException("mediaType");
            }
            if(stream == null){
                throw new ArgumentNullException("stream");
            }

            MIME_b_Application retVal = new MIME_b_Application(mediaType);

            Net_Utils.StreamCopy(stream,retVal.EncodedStream,32000);

            return retVal;
        }

        #endregion
        

        #region Properties implementation
                
        #endregion
    }
}
