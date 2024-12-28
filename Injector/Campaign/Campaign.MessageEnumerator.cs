using System;
using System.Collections;
using System.Collections.Generic;

namespace Injector
{
    public partial class Campaign
    {
        /// <summary>
        /// Supports a simple iteration over the messages of a <see cref="T:Campaign"/>.
        /// </summary>
        public struct MessageEnumerator
            : IEnumerator<Message>, IEnumerator
        {
            #region Fields

            /// <summary>
            /// Contains the enumerated <see cref="T:Campaign"/>.
            /// </summary>
            private Campaign _reader;

            /// <summary>
            /// Contains the current record.
            /// </summary>
            private Message _current;

            /// <summary>
            /// Contains the current record index.
            /// </summary>
            private long _currentMessageIndex;

            #endregion

            #region Constructors

            public MessageEnumerator(Campaign reader)
            {
                if (reader == null)
                    throw new ArgumentNullException("reader");

                _reader = reader;
                _current = null;

                _currentMessageIndex = reader._currentMessageIndex;
            }

            #endregion

            #region IEnumerator<Message> Members

            /// <summary>
            /// Gets the current message.
            /// </summary>
            public Message Current
            {
                get { return _current; }
            }

            /// <summary>
            /// Advances the enumerator to the next record of the Messages.
            /// </summary>
            /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next record, <see langword="false"/> if the enumerator has passed the end of the Messages.</returns>
            public bool MoveNext()
            {
                if (_reader._currentMessageIndex != _currentMessageIndex)
                    throw new InvalidOperationException("Enumeration version check failed.");
                
                if (_reader.ReadNextMessage())
                {

                    _current = new Message();
                    _reader.CopyCurrentMessageTo(_current);
                    _currentMessageIndex = _reader._currentMessageIndex;

                    return true;
                }
                else
                {
                    _current = null;
                    _currentMessageIndex = _reader._currentMessageIndex;

                    return false;
                }
            }

            #endregion

            #region IEnumerator Members

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first record.
            /// </summary>
            public void Reset()
            {
                if (_reader._currentMessageIndex != _currentMessageIndex)
                    throw new InvalidOperationException("EnumerationVersionCheckFailed");

                _reader.MoveTo(-1);

                _current = null;
                _currentMessageIndex = _reader._currentMessageIndex;
            }

            /// <summary>
            /// Gets the current record.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (_reader._currentMessageIndex != _currentMessageIndex)
                        throw new InvalidOperationException("Current");

                    return this.Current;
                }
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                _reader = null;
                _current = null;
            }

            #endregion
        }
    }
}