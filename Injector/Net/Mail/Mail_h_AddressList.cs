﻿using System;
using System.Collections.Generic;
using System.Text;

using Injector.Net.MIME;

namespace Injector.Net.Mail
{
    /// <summary>
    /// This class represent generic <b>address-list</b> header fields. For example: To header.
    /// </summary>
    /// <example>
    /// <code>
    /// RFC 5322.
    ///     header       = "FiledName:" address-list CRLF
    ///     address-list = (address *("," address))
    ///     address      = mailbox / group
    /// </code>
    /// </example>
    public class Mail_h_AddressList : MIME_h
    {
        private string             m_ParseValue = null;
        private string             m_Name       = null;
        private Mail_t_AddressList m_pAddresses = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="fieldName">Header field name. For example: "To".</param>
        /// <param name="values">Addresses collection.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>filedName</b> or <b>values</b> is null reference.</exception>
        /// <exception cref="ArgumentException">Is raised when any of the arguments has invalid value.</exception>
        public Mail_h_AddressList(string fieldName,Mail_t_AddressList values)
        {
            if(fieldName == null){
                throw new ArgumentNullException("fieldName");
            }
            if(fieldName == string.Empty){
                throw new ArgumentException("Argument 'fieldName' value must be specified.");
            }
            if(values == null){
                throw new ArgumentNullException("values");
            }

            m_Name       = fieldName;
            m_pAddresses = values;
        }


        #region static method Parse

        /// <summary>
        /// Parses header field from the specified value.
        /// </summary>
        /// <param name="value">Header field value. Header field name must be included. For example: 'Content-Type: text/plain'.</param>
        /// <returns>Returns parsed header field.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <b>value</b> is null reference.</exception>
        /// <exception cref="ParseException">Is raised when header field parsing errors.</exception>
        public static Mail_h_AddressList Parse(string value)
        {
            if(value == null){
                throw new ArgumentNullException("value");
            }

            string[] name_value = value.Split(new char[]{':'},2);
            if(name_value.Length != 2){
                throw new ParseException("Invalid header field value '" + value + "'.");
            }

            MIME_Reader r = new MIME_Reader(name_value[1]);

            /* RFC 5322 3.4.
                address         =   mailbox / group

                mailbox         =   name-addr / addr-spec

                name-addr       =   [display-name] angle-addr

                angle-addr      =   [CFWS] "<" addr-spec ">" [CFWS] / obs-angle-addr

                group           =   display-name ":" [group-list] ";" [CFWS]

                display-name    =   phrase

                mailbox-list    =   (mailbox *("," mailbox)) / obs-mbox-list

                address-list    =   (address *("," address)) / obs-addr-list

                group-list      =   mailbox-list / CFWS / obs-group-list
            */

            Mail_h_AddressList retVal = new Mail_h_AddressList(name_value[0],new Mail_t_AddressList());

            while(true){
                string word = r.QuotedReadToDelimiter(new char[]{',','<',':'});
                // We processed all data.
                if(word == null && r.Available == 0){
                    break;
                }
                // group
                else if(r.Peek(true) == ':'){
                    Mail_t_Group group = new Mail_t_Group(word != null ? MIME_Encoding_EncodedWord.DecodeS(TextUtils.UnQuoteString(word)) : null);
                    // Consume ':'
                    r.Char(true);
                 
                    while(true){
                        word = r.QuotedReadToDelimiter(new char[]{',','<',':',';'});
                        // We processed all data.
                        if(word == null && r.Available == 0){
                            break;
                        }
                        // name-addr
                        else if(r.Peek(true) == '<'){  
                            group.Members.Add(new Mail_t_Mailbox(word != null ? MIME_Encoding_EncodedWord.DecodeS(TextUtils.UnQuoteString(word)) : null,r.ReadParenthesized()));                    
                        }
                        // addr-spec
                        else{
                            group.Members.Add(new Mail_t_Mailbox(null,word));
                        }
                       
                        // We reached at the end of group.
                        if(r.Peek(true) == ';'){
                            r.Char(true);
                            break;
                        }
                        // We have more addresses.
                        if(r.Peek(true) == ','){
                            r.Char(false);
                        }
                    }

                    retVal.m_pAddresses.Add(group);
                }
                // name-addr
                else if(r.Peek(true) == '<'){
                    retVal.m_pAddresses.Add(new Mail_t_Mailbox(word != null ? MIME_Encoding_EncodedWord.DecodeS(TextUtils.UnQuoteString(word)) : null,r.ReadParenthesized()));                    
                }
                // addr-spec
                else{
                    retVal.m_pAddresses.Add(new Mail_t_Mailbox(null,word));
                }

                // We have more addresses.
                if(r.Peek(true) == ','){
                    r.Char(false);
                }
            }

            retVal.m_ParseValue = value;
            retVal.m_pAddresses.AcceptChanges();

            return retVal;
        }

        #endregion


        #region override method ToString

        /// <summary>
        /// Returns header field as string.
        /// </summary>
        /// <param name="wordEncoder">8-bit words ecnoder. Value null means that words are not encoded.</param>
        /// <param name="parmetersCharset">Charset to use to encode 8-bit characters. Value null means parameters not encoded.</param>
        /// <returns>Returns header field as string.</returns>
        public override string ToString(MIME_Encoding_EncodedWord wordEncoder,Encoding parmetersCharset)
        {            
            if(this.IsModified){
                StringBuilder retVal = new StringBuilder();
                retVal.Append(this.Name + ": ");
                for(int i=0;i<m_pAddresses.Count;i++){
                    if(i > 0){
                        retVal.Append("\t");
                    }
 
                    // Don't add ',' for last item.
                    if(i == (m_pAddresses.Count - 1)){
                        retVal.Append(m_pAddresses[i].ToString(wordEncoder) + "\r\n");
                    }
                    else{
                        retVal.Append(m_pAddresses[i].ToString(wordEncoder) + ",\r\n");
                    }
                }

                return retVal.ToString();
            }
            else{
                return m_ParseValue;
            }            
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
            get{ return m_pAddresses.IsModified; }
        }

        /// <summary>
        /// Gets header field name. For example "To".
        /// </summary>
        public override string Name
        {
            get{ return m_Name; }
        }

        /// <summary>
        /// Gets addresses collection.
        /// </summary>
        public Mail_t_AddressList Addresses
        {
            get{ return m_pAddresses; }
        }

        #endregion
    }
}
