using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Injector.UI.Controls.MessageToolbar
{
    /// <summary>
    /// 
    /// </summary>
    public delegate void ItemClickedEventHandler(object sender, ItemClicked_EventArgs e);

    [Flags]
    public enum MessageToolbarButtons
    {
        Special = 0x0,
        Include = 0x1,
        Custom = 0x2,
        Rnd = 0x3,
        ROT = 0x4,
        DownloadData = 0x5,
        TextToImage = 0x6,
        Embedded = 0x7
    }

    [DefaultEvent("ItemClicked"),]
    public partial class MessageToolbar : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public event ItemClickedEventHandler ItemClicked = null;

        private TextBox _target = null;

        public MessageToolbar()
        {
            InitializeComponent();

            this.toolStrip.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip_ItemClicked);
            this.Resize += new EventHandler(MessageToolbar_Resize);
            this.Size = toolStrip.Size;
        }

        void MessageToolbar_Resize(object sender, EventArgs e)
        {
            this.Size = toolStrip.Size;
        }

        void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ItemClicked != null)
            {
                MessageToolbarButtons button = (MessageToolbarButtons)toolStrip.Items.IndexOf(e.ClickedItem);
                ItemClicked_EventArgs args = new ItemClicked_EventArgs(button, e.ClickedItem, _target);
                this.ItemClicked(this, args);
            }
        }

        [Category("UI")]
        [DisplayName("Target")]
        public TextBox Target
        {
            get { return _target; }
            set
            {
                if (_target == value)
                    return;
                _target = value;
            }
        }
    }


    /// <summary>
    /// Holds ItemClicked_EventArgs.
    /// </summary>
    public class ItemClicked_EventArgs
    {
        private ToolStripItem _toolstripItem = null;
        private MessageToolbarButtons _item;
        private TextBox _target = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="item"></param>
        public ItemClicked_EventArgs(MessageToolbarButtons item, ToolStripItem toolstripItem, TextBox target)
        {
            _item = item;
            _toolstripItem = toolstripItem;
            _target = target;
        }


        #region Properties Implementation

        /// <summary>
        /// Gets item which was clicked.
        /// </summary>
        public MessageToolbarButtons Item
        {
            get { return _item; }
        }

        /// <summary>
        /// Gets the intended target.
        /// </summary>
        public TextBox Target
        {
            get { return _target; }
        }

        /// <summary>
        /// Gets toolstrip item which was clicked.
        /// </summary>
        public ToolStripItem ToolstripItem
        {
            get
            {
                return _toolstripItem;
            }
        }

        #endregion

    }



}
