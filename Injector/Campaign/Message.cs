using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Injector
{
    [Serializable()]
    public partial class Message 
        : IDisposable, ICloneable
    {
        #region Constants

        /// <summary>
        /// Defines the default hashing algorithm.
        /// </summary>
        public const string DefaultHashAlgo = "MD5";

        /// <summary>
        /// Defines the default message name.
        /// </summary>
        public const string DefaultMessageName = "New Message";

        #endregion

        #region Fields

        #region Settings

        private string _id;
        private string _name;
        private string _msgText;
        private string _msgHtml;
        private string _msgSource;
        private string[] _subjects;
        private string[] _fromPrefix;
        private string[] _fromAlias;
        private string[] _attachList;
        private bool _inRotation;
        private ArrayList _attachments;
        private ArrayList _images;
        private ArrayList _embeddedImages;
        private bool _hasSource;
        #endregion

        #region State

        /// <summary>
        /// Contains the start time in <see cref="System.DateTime.Ticks"/> which the message was rotated to.
        /// </summary>
        private long _startTime;

        private bool _valuesChanged = false;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initialized a new instance of the Message class.
        /// </summary>
        public Message()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Message class.
        /// </summary>
        /// <param name="name">Friendly name for this class.</param>
        public Message(string name)
        {
#if DEBUG
            _allocStack = new System.Diagnostics.StackTrace();
#endif
            

            // When the 'Name' property is set, 'ID' is generated.
            this.Name = name != null ? name : DefaultMessageName;
            _attachments = new ArrayList();
            _images = new ArrayList();
            _embeddedImages = new ArrayList();
        }


        #endregion

        #region Properties

        public bool HasChanges
        {
            get
            {
                return _valuesChanged;
            }
        }

        public ArrayList EmbeddedImages
        {
            get
            {
                // Not created yet, create it. We use late binding here, 
                // create objects and bind them when we need them.
                if (_embeddedImages == null)
                    _embeddedImages = new ArrayList();

                return _embeddedImages;
            }
            set
            {
                if (_embeddedImages == value)
                    return;
                _embeddedImages = value;
                _valuesChanged = true;
            }
        }

        public ArrayList Attachments
        {
            get
            {
                // Not created yet, create it. We use late binding here, 
                // create objects and bind them when we need them.
                if (_attachments == null)
                    _attachments = new ArrayList();

                return _attachments;
            }
            set
            {
                if (_attachments == value)
                    return;
                _attachments = value;
                _valuesChanged = true;
            }
        }

        public bool UseCustomSource
        {
            get
            {
                return _hasSource;
            }
            set
            {
                if (_hasSource == value)
                    return;
                _hasSource = value;
                _valuesChanged = true;
            }
        }

        public ArrayList Images
        {
            get
            {
                // Not created yet, create it. We use late binding here, 
                // create objects and bind them when we need them.
                if (_images == null)
                    _images = new ArrayList();

                return _images;
            }
            set
            {
                if (_images == value)
                    return;
                _images = value;
                _valuesChanged = true;
            }
        }

        public string ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                _id = GetId(_name);
                _valuesChanged = true;

            }
        }

        public string MessageText
        {
            get { return _msgText; }
            set
            {
                if (_msgText == value)
                    return;
                _msgText = value;
                _valuesChanged = true;
            }
        }

        public string MessageHtml
        {
            get { return _msgHtml; }
            set
            {
                if (_msgHtml == value)
                    return;
                _msgHtml = value;
                _valuesChanged = true;
            }
        }

        public string MessageSource
        {
            get { return _msgSource; }
            set
            {
                if (_msgSource == value)
                    return;
                _msgSource = value;
                _valuesChanged = true;
            }
        }

        /// <summary>
        /// Contains the start time(<see cref="System.DateTime.Now.Ticks"/>) which the message was rotated to.
        /// </summary>
        public long LastRotateTime
        {
            get { return _startTime; }
            set
            {
                if (_startTime == value)
                    return;
                _startTime = value;
                _valuesChanged = true;
            }
        }

        public string[] Subjects
        {
            get { return _subjects; }
            set
            {
                if (_subjects == value)
                    return;
                _subjects = value;
                _valuesChanged = true;
            }
        }

        public string[] FromPrefix
        {
            get { return _fromPrefix; }
            set
            {
                if (_fromPrefix == value)
                    return;
                _fromPrefix = value;
                _valuesChanged = true;
            }
        }

        public string[] FromAlias
        {
            get { return _fromAlias; }
            set
            {
                if (_fromAlias == value)
                    return;
                _fromAlias = value;
                _valuesChanged = true;
            }
        }

        public string[] AttachmentList
        {
            get { return _attachList; }
            set
            {
                if (_attachList == value)
                    return;
                _attachList = value;
                _valuesChanged = true;
            }
        }

        public bool InRotation
        {
            get { return _inRotation; }
            set
            {
                if (_inRotation == value)
                    return;
                _inRotation = value;
                _valuesChanged = true;
            }
        }

        #endregion

        #region Methods

        #region Commit

        public void Commit()
        {
            _valuesChanged =  false;
        }

        #endregion

        #region GenId

        /// <summary>
        /// Get a unique ID from this campaigns name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetId(string name)
        {
            string hash = CryptoEx.ComputeHash(name, DefaultHashAlgo, null);
            byte[] hashBytes = Convert.FromBase64String(hash);
            StringBuilder id = new StringBuilder();

            for (int k = 0; k < hashBytes.Length; k++)
                id.Append(hashBytes[k].ToString("x2"));

            return id.ToString();
        }

        #endregion

        #region RandomString

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        #endregion

        #region ShallowCopy

        /// <summary>
        /// Create a Shallow Copy of this class.
        /// </summary>
        /// <returns></returns>
        public Message ShallowCopy()
        {
            return (Message)this.MemberwiseClone();
        }

        #endregion

        #region AddImage

        /// <summary>Adds an included image to the MailMessage using a file path</summary>
        /// <param name="filepath">File path to the file you want to attach to the MailMessage</param>
        /// <example>
        /// <code>
        /// 	MailMessage msg = new MailMessage("support@OpenSmtp.com", "recipient@OpenSmtp.com");
        ///		msg.AddImage(@"C:\file.jpg");
        /// </code>
        /// </example>
        // start added/modified by mb
        public void AddImage(string filepath)
        {
            AddImage(filepath, NewCid());
        }

        public void AddImage(string filepath, string cid)
        {
            if (filepath != null)
            {
                Attachment image = new Attachment(filepath);
                image.contentid = cid;
                _images.Add(image);
            }
        }

        #endregion

        #region NewCid

        /// <summary>
        /// Generate a content campaignId
        /// </summary>
        /// <returns></returns>
        private string NewCid()
        {
            int attachmentid = _attachments.Count + _images.Count + 1;
            return "att" + attachmentid;
        }

        #endregion

        #region AddAttachment

        public void AddAttachment(string filepath, string cid)
        {
            if (filepath != null)
            {
                Attachment attachment = new Attachment(filepath);
                attachment.contentid = cid;
                _attachments.Add(attachment);
            }
        }

        /// <summary>Adds an Attachment to the MailMessage using an Attachment instance</summary>
        /// <param name="attachment">Attachment you want to attach to the MailMessage</param>
        /// <example>
        /// <code>
        ///		Attachment att = new Attachment(@"C:\file.jpg");
        ///		msg.AddAttachment(att);
        /// </code>
        /// </example>
        public void AddAttachment(Attachment attachment)
        {
            if (attachment != null)
            {
                _attachments.Add(attachment);
            }
        }

        /// <summary>Adds an Attachment to the MailMessage using a provided Stream</summary>
        /// <param name="stream">stream you want to attach to the MailMessage</param>
        /// <example>
        /// <code>
        ///		Attachment att = new Attachment(new FileStream(@"C:\File.jpg", FileMode.Open, FileAccess.Read), "Test Name");
        ///		msg.AddAttachment(att);
        /// </code>
        /// </example>
        public void AddAttachment(Stream stream)
        {
            if (stream != null)
            {
                _attachments.Add(stream);
            }
        }

        #endregion

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Perform a Deep Copy clone of this object.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Message msg = new Message(_name + " clone");
            msg._attachList = (string[])_attachList.Clone();
            msg._fromAlias = (string[])_fromAlias.Clone();
            msg._fromPrefix = (string[])_fromPrefix.Clone();
            msg._msgHtml = (string)_msgHtml.Clone();
            msg._msgSource = (string)_msgSource.Clone();
            msg._msgText = (string)_msgText.Clone();
            msg._name = (string)_name.Clone();
            msg._subjects = (string[])_subjects.Clone();

            return msg;
        }

        #endregion

        #region IDisposable members

#if DEBUG
        /// <summary>
		/// Contains the stack when the object was allocated.
		/// </summary>
		private System.Diagnostics.StackTrace _allocStack;
#endif

		/// <summary>
		/// Contains the disposed status flag.
		/// </summary>
		private bool _isDisposed = false;

		/// <summary>
		/// Contains the locking object for multi-threading purpose.
		/// </summary>
		private readonly object _lock = new object();

		/// <summary>
		/// Occurs when the instance is disposed of.
		/// </summary>
		public event EventHandler Disposed;

		/// <summary>
		/// Gets a value indicating whether the instance has been disposed of.
		/// </summary>
		/// <value>
		/// 	<see langword="true"/> if the instance has been disposed of; otherwise, <see langword="false"/>.
		/// </value>
		[System.ComponentModel.Browsable(false)]
		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		/// <summary>
		/// Raises the <see cref="M:Disposed"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected virtual void OnDisposed(EventArgs e)
		{
			EventHandler handler = Disposed;

			if (handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Checks if the instance has been disposed of, and if it has, throws an <see cref="T:System.ComponentModel.ObjectDisposedException"/>; otherwise, does nothing.
		/// </summary>
		/// <exception cref="T:System.ComponentModel.ObjectDisposedException">
		/// 	The instance has been disposed of.
		/// </exception>
		/// <remarks>
		/// 	Derived classes should call this method at the start of all methods and properties that should not be accessed after a call to <see cref="M:Dispose()"/>.
		/// </remarks>
		protected void CheckDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(this.GetType().FullName);
		}

		/// <summary>
		/// Releases all resources used by the instance.
		/// </summary>
		/// <remarks>
		/// 	Calls <see cref="M:Dispose(Boolean)"/> with the disposing parameter set to <see langword="true"/> to free unmanaged and managed resources.
		/// </remarks>
		public void Dispose()
		{
			if (!_isDisposed)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Releases the unmanaged resources used by this instance and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// 	<see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			// Refer to http://www.bluebytesoftware.com/blog/PermaLink,guid,88e62cdf-5919-4ac7-bc33-20c06ae539ae.aspx
			// Refer to http://www.gotdotnet.com/team/libraries/whitepapers/resourcemanagement/resourcemanagement.aspx

			// No exception should ever be thrown except in critical scenarios.
			// Unhandled exceptions during finalization will tear down the process.
			if (!_isDisposed)
			{
				try
				{
					// Dispose-time code should call Dispose() on all owned objects that implement the IDisposable interface. 
					// "owned" means objects whose lifetime is solely controlled by the container. 
					// In cases where ownership is not as straightforward, techniques such as HandleCollector can be used.  
					// Large managed object fields should be nulled out.

					// Dispose-time code should also set references of all owned objects to null, after disposing them. This will allow the referenced objects to be garbage collected even if not all references to the "parent" are released. It may be a significant memory consumption win if the referenced objects are large, such as big arrays, collections, etc. 
					if (disposing)
					{
						// Acquire a lock on the object while disposing
                        _attachList = null;
                        _msgHtml = null;
                        _msgSource = null;
                        _msgText = null;
                        _subjects = null;
                        _fromPrefix = null;
                        _fromAlias = null;
                        _id = null;
                        _name = null;
                        _attachments = null;
                        _embeddedImages = null;
					}
				}
				finally
				{
					// Ensure that the flag is set
					_isDisposed = true;

					// Catch any issues about firing an event on an already disposed object.
					try
					{
						OnDisposed(EventArgs.Empty);
					}
					catch { }
				}
			}
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the instance is reclaimed by garbage collection.
		/// </summary>
		~Message()
		{
#if DEBUG
            System.Diagnostics.Debug.WriteLine("FinalizableObject was not disposed" + _allocStack.ToString());
#endif

			Dispose(false);
		}

        #endregion

    }
}
