using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using Injector.UI.Controls.MessageToolbar;

namespace Injector
{
    /// <summary>
    /// 
    /// </summary>
    public delegate void MessageIndexChangedEventHandler(string selectedText, object item);


    public partial class MessageWindow : DockContent
    {

        private bool _valuesChanged = false;

        /// <summary>
        /// 
        /// </summary>
        public event MessageIndexChangedEventHandler MessageIndexChanged = null;

        public MessageWindow()
        {
            InitializeComponent();

            msgbarHtml.Target = txtMessageHTML;
            msgbarSource.Target = txtMessageSource;
            msgbarSubjects.Target = txtSubjects;
            msgbarText.Target = txtMessageText;
            checkRawEdit.Checked = false;
            txtMessageHTML.Visible = false;
            richMessageHTML.Visible = true;
            richMessageHTML.Dock = DockStyle.Fill;
            txtMessageHTML.Dock = DockStyle.Fill;

            msgbarHtml.ItemClicked += new ItemClickedEventHandler(OnMessageToolbarClicked);
            msgbarSource.ItemClicked += new ItemClickedEventHandler(OnMessageToolbarClicked);
            msgbarSubjects.ItemClicked += new ItemClickedEventHandler(OnMessageToolbarClicked);
            msgbarText.ItemClicked += new ItemClickedEventHandler(OnMessageToolbarClicked);
            checkRawEdit.CheckedChanged += new EventHandler(checkRawEdit_CheckedChanged);
            comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MessageIndexChanged != null)
                this.MessageIndexChanged(comboBox.SelectedText, comboBox.SelectedItem);
        }

        void checkRawEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRawEdit.Checked)
            {
                string html = RtfToHtml();
                txtMessageHTML.Visible = true;
                txtMessageHTML.Text = html;
                richMessageHTML.Visible = false;
            }
            else
                richMessageHTML.Visible = true;
        }

        void OnMessageToolbarClicked(object sender, ItemClicked_EventArgs e)
        {
            if (e.Target == null)
            {
                return;
            }

            int startIdx = e.Target.SelectionStart;
            bool dummyWindow = false;

            MacroWindow dialogWindow = null;

            switch (e.Item)
            {
                case MessageToolbarButtons.Special:
                    dialogWindow = new SpecialTagDialog();
                    break;

                case MessageToolbarButtons.Include:
                    dummyWindow = true;
                    dialogWindow = new MacroWindow();
                    dlgOpen.CheckFileExists = true;
                    //dlgOpen.Multiselect = true; // TODO!
                    dlgOpen.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";

                    if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                        return;

                    dialogWindow.MacroName = "Include";
                    dialogWindow.MacroArguments = dlgOpen.FileName;
                    break;

                case MessageToolbarButtons.Custom:
                    break;

                case MessageToolbarButtons.Rnd:
                    dialogWindow = new RndDialog();
                    break;
                case MessageToolbarButtons.ROT:
                    dialogWindow = new RotateDialog();
                    break;

                case MessageToolbarButtons.DownloadData:
                    InputBoxResult res = InputBox.Show("Enter the url:", "", "http://www.google.com");
                    if (res.ReturnCode == DialogResult.OK)
                    {
                        if (res.Text != null && res.Text.Length > 0)
                        {
                            dummyWindow = true;
                            dialogWindow = new MacroWindow();
                            dialogWindow.MacroName = "DownloadData";
                            dialogWindow.MacroArguments = res.Text;
                        }
                    }
                    break;

                case MessageToolbarButtons.TextToImage:
                    dialogWindow = new TextToImageDialog();
                    break;

                case MessageToolbarButtons.Embedded:
                    dummyWindow = true;
                    dialogWindow = new MacroWindow();
                    dlgOpen.CheckFileExists = true;
                    dlgOpen.Filter = "Image Files (*.bmp;*.gif;*.jpg;*.jpeg)|*.bmp;*.gif;*.jpg;*.jpeg|All files(*.*)|*.*";

                    if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                        return;

                    dialogWindow.MacroName = "Inline";
                    dialogWindow.MacroArguments = dlgOpen.FileName;
                    break;

                default:
                    break;
            }

            if (dialogWindow != null)
            {
                if (!dummyWindow && dialogWindow.ShowDialog() != DialogResult.OK)
                    return;

                // set a bool for dialogs expecting arguments
                // then check if checking is required...
                if (dialogWindow.MacroName != null && dialogWindow.MacroName.Length > 0)
                {
                    InsertMacroText(e.Target, startIdx, String.Format("${0}({1})", dialogWindow.MacroName, dialogWindow.MacroArguments));
                }

            }
        }


        #region method InsertMacroText

        private void InsertMacroText(TextBox txtbox, int selStart, string text)
        {
            txtbox.Text = txtbox.Text.Insert(selStart, text);
            txtbox.SelectionStart = selStart + text.Length;
        }

        #endregion



        #region method RtfToHtml

        /// <summary>
        /// Converts RTF text to HTML text.
        /// </summary>
        /// <returns></returns>
        private string RtfToHtml()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("<html>\r\n");

            //richMessageHTML.SuspendLayout = true;
            // Go to text start.

            richMessageHTML.RichTextBox.SelectionStart = 0;
            richMessageHTML.RichTextBox.SelectionLength = 1;

            Font currentFont = richMessageHTML.RichTextBox.SelectionFont;
            Color currentSelectionColor = richMessageHTML.RichTextBox.SelectionColor;
            Color currentBackColor = richMessageHTML.RichTextBox.SelectionBackColor;

            int numberOfSpans = 0;
            int startPos = 0;
            while (richMessageHTML.RichTextBox.Text.Length > richMessageHTML.RichTextBox.SelectionStart)
            {
                richMessageHTML.RichTextBox.SelectionStart++;
                richMessageHTML.RichTextBox.SelectionLength = 1;

                // Font style or size or color or back color changed
                if (richMessageHTML.RichTextBox.Text.Length == richMessageHTML.RichTextBox.SelectionStart || (currentFont.Name != richMessageHTML.RichTextBox.SelectionFont.Name || currentFont.Size != richMessageHTML.RichTextBox.SelectionFont.Size || currentFont.Style != richMessageHTML.RichTextBox.SelectionFont.Style || currentSelectionColor != richMessageHTML.RichTextBox.SelectionColor || currentBackColor != richMessageHTML.RichTextBox.SelectionBackColor))
                {
                    string currentTextBlock = richMessageHTML.RichTextBox.Text.Substring(startPos, richMessageHTML.RichTextBox.SelectionStart - startPos);

                    //--- Construct text bloxh html -----------------------------------------------------------------//
                    // Make colors to html color syntax: #hex(r)hex(g)hex(b)
                    string htmlSelectionColor = "#" + currentSelectionColor.R.ToString("X2") + currentSelectionColor.G.ToString("X2") + currentSelectionColor.B.ToString("X2");
                    string htmlBackColor = "#" + currentBackColor.R.ToString("X2") + currentBackColor.G.ToString("X2") + currentBackColor.B.ToString("X2");
                    string textStyleStartTags = "";
                    string textStyleEndTags = "";
                    if (currentFont.Bold)
                    {
                        textStyleStartTags += "<b>";
                        textStyleEndTags += "</b>";
                    }
                    if (currentFont.Italic)
                    {
                        textStyleStartTags += "<i>";
                        textStyleEndTags += "</i>";
                    }
                    if (currentFont.Underline)
                    {
                        textStyleStartTags += "<u>";
                        textStyleEndTags += "</u>";
                    }
                    retVal.Append("<span style=\"color:" + htmlSelectionColor + "; font-size:" + currentFont.Size + "pt; font-family:" + currentFont.FontFamily.Name + "; background-color:" + htmlBackColor + ";\">" + textStyleStartTags + currentTextBlock.Replace("\r\n", "</br>") + textStyleEndTags);
                    //-----------------------------------------------------------------------------------------------//

                    startPos = richMessageHTML.RichTextBox.SelectionStart;
                    currentFont = richMessageHTML.RichTextBox.SelectionFont;
                    currentSelectionColor = richMessageHTML.RichTextBox.SelectionColor;
                    currentBackColor = richMessageHTML.RichTextBox.SelectionBackColor;
                    numberOfSpans++;
                }
            }

            for (int i = 0; i < numberOfSpans; i++)
            {
                retVal.Append("</span>");
            }

            retVal.Append("\r\n</html>\r\n");
            //richMessageHTML.RichTextBox.SuspendPaint = false;

            return retVal.ToString();
        }

        #endregion


        #region Properties implementation


        public string Attachments
        {
            get
            {
                return txtAttachments.Text;
            }
            set
            {
                if (txtAttachments.Text == value)
                    return;
                txtAttachments.Text = value;
                _valuesChanged = true;
            }
        }

        public List<string> ComboItems
        {
            get
            {
                List<string> items = new List<string>();
                for (int i = 0; i < comboBox.Items.Count - 1; i++)
                {
                    items.Add(comboBox.Items[i].ToString());
                }
                return items;
            }
            set
            {
                if (value == null)
                {
                    comboBox.Items.Clear();
                    _valuesChanged = true;
                    return;
                }
                _valuesChanged = true;
                comboBox.Items.AddRange(value.ToArray());
            }
        }

        public string FromAlias
        {
            get
            {
                return txtFromAliases.Text;
            }
            set
            {
                if (txtFromAliases.Text == value)
                    return;
                _valuesChanged = true;
                txtFromAliases.Text = value;
            }
        }
        public string FromPrefix
        {
            get
            {
                return txtFromEmailPrefix.Text;
            }
            set
            {
                if (txtFromEmailPrefix.Text == value)
                    return;
                _valuesChanged = true;
                txtFromEmailPrefix.Text = value;
            }
        }
        public string Html
        {
            get
            {
                return txtMessageHTML.Text;
            }
            set
            {
                if (txtMessageHTML.Text == value)
                    return;
                _valuesChanged = true;
                txtMessageHTML.Text = value;
            }
        }

        public string Plaintext
        {
            get
            {
                return txtMessageText.Text;
            }
            set
            {
                if (txtMessageText.Text == value)
                    return;
                _valuesChanged = true;
                txtMessageText.Text = value;
            }
        }

        public string Source
        {
            get
            {
                return txtMessageSource.Text;
            }
            set
            {
                if (txtMessageSource.Text == value)
                    return;
                _valuesChanged = true;
                txtMessageSource.Text = value;
            }
        }
        public string Subjects
        {
            get
            {
                return txtSubjects.Text;
            }
            set
            {
                if (txtSubjects.Text == value)
                    return;
                _valuesChanged = true;
                txtSubjects.Text = value;
            }
        }
        public string TabTitle
        {
            get
            {
                return this.TabTitle;
            }
            set
            {
                if (this.TabTitle == value)
                    return;
                _valuesChanged = true;
                this.TabTitle = value;
            }
        }

        public bool ValuesChanged
        {
            get
            {
                return _valuesChanged;
            }
            set
            {
                if (_valuesChanged == value)
                    return;
                _valuesChanged = value;
            }
        }

        #endregion

    }
}