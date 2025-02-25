﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.IO;

namespace Injector
{
    public partial class Campaign
    {

        /// <summary>
        /// This Type is used to encode and decode files and strings using 
        /// MIME compatible encoding methods such as Base64 and quoted-printable
        /// </summary>
        internal class MailEncoder
        {

            private MailEncoder()
            { }
            /// <summary>Encodes a String using Base64 (see RFC 1521)</summary>
            /// <param name="s">string to be encoded</param>
            /// <example>
            /// <code>
            /// string base64EncodedText = MailEncoder.ConvertQP("貿澱");
            /// </code>
            /// </example>
            /// <returns>Base64 encoded string</returns>
            internal static string ConvertToBase64(String s)
            {
                byte[] from = Encoding.ASCII.GetBytes(s.ToCharArray());
                string returnMsg = Convert.ToBase64String(from);

                return returnMsg;
            }


            /// <summary> Encodes a FileStream using Base64 (see RFC 1521)</summary>
            /// <param name="inputFilePath">UNC path to file that needs to be encoded</param>
            /// <param name="outputFilePath">UNC path to file will store Base64 encoded ASCII text</param>
            /// <example>
            /// <code>
            /// MailEncoder.ConvertBase64("file.jpg", "file.txt");
            /// </code>
            /// </example>
            internal static void ConvertToBase64(string inputFilePath, string outputFilePath)
            {
                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
                ConvertToBase64(fin, outputFilePath);
                fin.Close();
            }

            /// <summary> Encodes a FileStream using Base64 (see RFC 1521)</summary>
            /// <param name="inputStream">The stream that needs to be encoded</param>
            /// <param name="outputFilePath">UNC path to file will store Base64 encoded ASCII text</param>
            /// <example>
            /// <code>
            /// MailEncoder.ConvertBase64(Stream, "file.txt");
            /// </code>
            /// </example>
            internal static void ConvertToBase64(Stream inputStream, string outputFilePath)
            {
                //Create the file streams to handle the input and output files.
                FileStream fout = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                ToBase64Transform transformer = new ToBase64Transform();

                //Create variables to help with read and write below.
                //This is intermediate storage for the encryption:
                byte[] bin = new byte[inputStream.Length / transformer.InputBlockSize * transformer.OutputBlockSize];
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = inputStream.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                CryptoStream encStream = new CryptoStream(fout, transformer, CryptoStreamMode.Write);

                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = inputStream.Read(bin, 0, (int)inputStream.Length);
                    encStream.Write(bin, 0, len);
                    //inputBlock size(3)
                    rdlen = (rdlen + ((len / transformer.InputBlockSize) * transformer.OutputBlockSize));
                }
                
                
                encStream.Close();
                fout.Close();
            }

            internal static string ConvertFromBase64(string s)
            {
                byte[] ret = Convert.FromBase64String(s);
                return Encoding.ASCII.GetString(ret, 0, ret.Length);
            }

            internal static void ConvertFromBase64(string inputFilePath, string outputFilePath)
            {
                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                FromBase64Transform transformer = new FromBase64Transform();

                //Create variables to help with read and write below.
                //This is intermediate storage for the decryption:
                byte[] bin = new byte[fin.Length / transformer.InputBlockSize * transformer.OutputBlockSize];
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                CryptoStream encStream = new CryptoStream(fout, transformer, CryptoStreamMode.Write);

                //Read from the input file, then decrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, (int)fin.Length);
                    encStream.Write(bin, 0, len);
                    rdlen = (rdlen + ((len / transformer.InputBlockSize) * transformer.OutputBlockSize));
                }

                encStream.Close();
                fout.Close();
                fin.Close();
            }

            /// <summary> Encodes a string using Quoted-Printable encoding (see RFC 1521)</summary>
            /// <param name="s">string that needs to be encoded</param>
            /// <param name="charset">charset of string that needs to be encoded</param>
            /// <example>
            /// <code>
            /// string qpEncodedText = MailEncoder.ConvertQP("貿澱");
            /// </code>
            /// </example>
            /// <returns>Quoted-Printable encoded string</returns>
            internal static string ConvertToQP(string s, string charset)
            {
                // TURNER.BSD MODIFIED 9/10/2004 GKW to fix NUMEROUS bugs
                // Conforms to rules described in http://www.fourmilab.ch/webtools/qprint/rfc1521.html

                if (s == null) { return null; }
                if (charset == null) charset = "ISO-8859-1"; // default charset

                // environment newline char
                char[] nl = Environment.NewLine.ToCharArray();

                // source char array
                char[] source = s.ToCharArray();
                char ch;

                StringBuilder sb = new StringBuilder();
                StringBuilder currLine = new StringBuilder();
                bool bNewline = false;

                Encoding oEncoding = Encoding.GetEncoding(charset);

                for (int sidx = 0; sidx < s.Length; sidx++)
                {
                    ch = source[sidx];

                    // RULE # 4: LINE BREAKS
                    if (ch == nl[0] && sidx <= (s.Length - nl.Length))
                    {
                        // peek ahead make sure the "whole" newline is present
                        if (s.Substring(sidx, nl.Length) == Environment.NewLine)
                        {
                            // RULE #3: ENCODE WHITESPACE PRECEEDING A HARD BREAK
                            if (currLine.Length > 0)
                            {
                                if (source[sidx - 1] == ' ')
                                {   // if char is preceded by space char add =20
                                    currLine.Remove(currLine.Length - 1, 1);
                                    currLine.Append("=20");
                                }
                                else if (source[sidx - 1] == '\t')
                                {   // if char is preceded by tab char add =09
                                    currLine.Remove(currLine.Length - 1, 1);
                                    currLine.Append("=09");
                                }
                            }

                            // flag for new line
                            bNewline = true;
                            sidx += nl.Length - 1;  // jump ahead 

                        }
                        else
                        {	// not actually a newline.  Encode per Rule #1
                            currLine.Append("=0" + Convert.ToString((byte)ch, 16).ToUpper());
                        }
                    }
                    // RULE #1 and #2
                    // Optional characters are: !"#$@[\]^`{|}~
                    else if (ch > 126 || (ch < 32 && ch != '\t') || ch == '=')
                    {
                        byte[] outByte = new byte[10];
                        int iCount = oEncoding.GetBytes("" + ch, 0, 1, outByte, 0);

                        for (int i = 0; i < iCount; i++)
                        {
                            if (outByte[i] < 16)
                                currLine.Append("=0" + Convert.ToString(outByte[i], 16).ToUpper());
                            else
                                currLine.Append("=" + Convert.ToString(outByte[i], 16).ToUpper());
                        }
                    }
                    else
                    {
                        currLine.Append(ch);
                    }

                    // Rule #5: MAXIMUM length 76 characters per line
                    if (currLine.Length >= 76)
                    {
                        // just make sure not to split an encoded char
                        string cLine = currLine.ToString();
                        int breakAt = cLine.LastIndexOf("=");
                        if (breakAt < 70) breakAt = 74;
                        sb.Append(cLine.Substring(0, breakAt) + "=\r\n");
                        currLine = new StringBuilder(cLine.Substring(breakAt));
                    }

                    if (bNewline)
                    {
                        // RFC 822 linebreak = CRLF
                        sb.Append(currLine.ToString() + "\r\n");
                        currLine = new StringBuilder();
                        bNewline = false;
                    }
                }

                // add last line
                sb.Append(currLine.ToString());

                return sb.ToString();
            }

            /// <summary>
            /// Convert to Quoted Printable if necessary
            /// </summary>
            /// <param name="s"></param>
            /// <param name="charset"></param>
            /// <param name="forceconversion">force a conversion</param>
            /// <returns></returns>
            internal static string ConvertHeaderToQP(string s, string charset, bool forceconversion)
            {
                if (!forceconversion)
                {
                    bool needsconversion = false;
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] > 126 || s[i] < 32)
                        {
                            needsconversion = true;
                            break;
                        }
                    }
                    if (!needsconversion)
                    {
                        return s;
                    }
                }
                return "=?" + charset + "?Q?" + ConvertToQP(s, charset) + "?=";
            }

            /// <summary>
            /// Convert to Quoted printable if necessary.
            /// </summary>
            /// <param name="s"></param>
            /// <param name="charset"></param>
            /// <returns></returns>
            internal static string ConvertHeaderToQP(string s, string charset)
            {
                return ConvertHeaderToQP(s, charset, false);
            }

            internal static string ConvertFromQP(string s)
            {
                if (s == null) return null;

                // source char array:
                char[] source = s.ToCharArray();
                // result char array:
                char[] result = new char[(int)(s.Length * 1.1)];
                // environment newline char:
                char[] nl = Environment.NewLine.ToCharArray();

                int last = 0,
                    didx = 0,
                    slen = (int)s.Length;

                for (int sidx = 0; sidx < slen; )
                {
                    char ch = source[sidx++];

                    if (ch == '=')
                    {

                        // Premature EOF
                        if (sidx >= slen - 1)
                        { throw new ApplicationException("Premature EOF"); }

                        // RULE # 5
                        if (source[sidx] == '\n' || source[sidx] == '\r')
                        {
                            sidx++;
                            if (source[sidx - 1] == '\r' && source[sidx] == '\n')
                            {
                                sidx++;
                            }
                        }
                        // RULE # 1
                        else
                        {
                            char repl;
                            int hi = Int32.Parse(Convert.ToString(source[sidx]), NumberStyles.HexNumber);
                            int lo = Int32.Parse(Convert.ToString(source[sidx + 1]), NumberStyles.HexNumber);

                            if ((hi | lo) < 0)
                            {
                                throw new ApplicationException(new String(source, sidx - 1, 3) + " is an invalid code");
                            }
                            else
                            {
                                repl = (char)(hi << 4 | lo);
                                sidx += 2;
                            }

                            result[didx++] = repl;
                        }

                        last = didx;

                        // RULE # 4
                        if (ch == '\n' || ch == '\r')
                        {
                            if (ch == '\r' && sidx < slen && source[sidx] == '\n')
                            {
                                sidx++;

                                for (int idx = 0; idx < nl.Length; idx++)
                                {
                                    result[last++] = nl[idx];
                                    didx = last;
                                }
                            }
                            else
                            {
                                result[didx++] = ch;
                                // RULE # 3
                                if (ch != ' ' && ch != '\t')
                                {
                                    last = didx;
                                }
                            }

                            if (didx > result.Length - nl.Length - 2)
                            {
                                char[] newCharArray = new char[(int)(result.Length + 500)];
                                Array.Copy(result, newCharArray, result.Length);
                                result = newCharArray;
                            }
                        }

                    }
                }

                return new String(result, 0, didx);
            }

            internal static bool IsAscii(string s)
            {
                // source char array
                char[] source = s.ToCharArray();
                for (int sidx = 0; sidx < s.Length; sidx++)
                {
                    char ch = source[sidx];
                    if (Convert.ToInt32(ch) > 127)
                    {
                        return false;
                    }
                }

                return true;
            }

        }


    }
}
