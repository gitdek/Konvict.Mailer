﻿using System;
using System.Collections.Generic;
using System.Text;

using Injector.Net.IO;

namespace Injector.Net.MIME
{
    /// <summary>
    /// This class represents MIME multipart/report body. Defined in RFC 3462.
    /// </summary>
    /// <remarks>
    /// The Multipart/Report Multipurpose Internet Mail Extensions (MIME)
    /// content-type is a general "family" or "container" type for electronic
    /// mail reports of any kind.
    /// </remarks>
    public class MIME_b_MultipartReport : MIME_b_Multipart
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="contentType">Content type.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>contentType</b> is null reference.</exception>
        /// <exception cref="ArgumentException">Is raised when any of the arguments has invalid value.</exception>
        public MIME_b_MultipartReport(MIME_h_ContentType contentType) : base(contentType)
        {
            if(!string.Equals(contentType.TypeWithSubype,"multipart/report",StringComparison.CurrentCultureIgnoreCase)){
                throw new ArgumentException("Argument 'contentType.TypeWithSubype' value must be 'multipart/report'.");
            }
        }

        #region static method Parse

        /// <summary>
        /// Parses body from the specified stream
        /// </summary>
        /// <param name="owner">Owner MIME entity.</param>
        /// <param name="mediaType">MIME media type. For example: text/plain.</param>
        /// <param name="stream">Stream from where to read body.</param>
        /// <returns>Returns parsed body.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <b>stream</b>, <b>mediaType</b> or <b>stream</b> is null reference.</exception>
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
            if(owner.ContentType == null || owner.ContentType.Param_Boundary == null){
                throw new ParseException("Multipart entity has not required 'boundary' paramter.");
            }
            
            MIME_b_MultipartReport retVal = new MIME_b_MultipartReport(owner.ContentType);
            ParseInternal(owner,mediaType,stream,retVal);

            return retVal;
        }

        #endregion
        

        #region Properties implementation
                
        #endregion
    }
}
