namespace Injector
{
    partial class MessageWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageWindow));
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.msgbarText = new Injector.UI.Controls.MessageToolbar.MessageToolbar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richMessageHTML = new Injector.UI.Controls.WRichEditBox();
            this.txtMessageHTML = new System.Windows.Forms.TextBox();
            this.checkRawEdit = new System.Windows.Forms.CheckBox();
            this.msgbarHtml = new Injector.UI.Controls.MessageToolbar.MessageToolbar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtSubjects = new System.Windows.Forms.TextBox();
            this.msgbarSubjects = new Injector.UI.Controls.MessageToolbar.MessageToolbar();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton41 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton28 = new System.Windows.Forms.ToolStripButton();
            this.txtAttachments = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFromEmailPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFromAliases = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtMessageSource = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMessageSourceEdit = new System.Windows.Forms.CheckBox();
            this.btnRefreshSource = new System.Windows.Forms.Button();
            this.msgbarSource = new Injector.UI.Controls.MessageToolbar.MessageToolbar();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnPreviewOutlook = new System.Windows.Forms.Button();
            this.menuTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.enabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.toolStrip7.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.menuTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Location = new System.Drawing.Point(0, 3);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(348, 21);
            this.comboBox.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(348, 285);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMessageText);
            this.tabPage1.Controls.Add(this.msgbarText);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(340, 259);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtMessageText
            // 
            this.txtMessageText.AcceptsReturn = true;
            this.txtMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageText.Location = new System.Drawing.Point(3, 28);
            this.txtMessageText.Multiline = true;
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageText.Size = new System.Drawing.Size(334, 228);
            this.txtMessageText.TabIndex = 6;
            this.txtMessageText.Tag = "CampaignMessages";
            this.txtMessageText.WordWrap = false;
            // 
            // msgbarText
            // 
            this.msgbarText.BackColor = System.Drawing.Color.Transparent;
            this.msgbarText.Dock = System.Windows.Forms.DockStyle.Top;
            this.msgbarText.Location = new System.Drawing.Point(3, 3);
            this.msgbarText.Name = "msgbarText";
            this.msgbarText.Size = new System.Drawing.Size(334, 25);
            this.msgbarText.TabIndex = 5;
            this.msgbarText.Target = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richMessageHTML);
            this.tabPage2.Controls.Add(this.txtMessageHTML);
            this.tabPage2.Controls.Add(this.checkRawEdit);
            this.tabPage2.Controls.Add(this.msgbarHtml);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(344, 261);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richMessageHTML
            // 
            this.richMessageHTML.DrawBorder = true;
            this.richMessageHTML.Lines = new string[] {
        "hello there"};
            this.richMessageHTML.Location = new System.Drawing.Point(8, 120);
            this.richMessageHTML.Name = "richMessageHTML";
            this.richMessageHTML.ReadOnly = false;
            this.richMessageHTML.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}" +
                "\r\n\\viewkind4\\uc1\\pard\\lang1033\\f0\\fs17 hello there\\par\r\n}\r\n";
            this.richMessageHTML.Size = new System.Drawing.Size(376, 112);
            this.richMessageHTML.TabIndex = 10;
            this.richMessageHTML.UseStaticViewStyle = true;
            // 
            // txtMessageHTML
            // 
            this.txtMessageHTML.Location = new System.Drawing.Point(8, 40);
            this.txtMessageHTML.Multiline = true;
            this.txtMessageHTML.Name = "txtMessageHTML";
            this.txtMessageHTML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageHTML.Size = new System.Drawing.Size(360, 76);
            this.txtMessageHTML.TabIndex = 9;
            this.txtMessageHTML.Tag = "CampaignMessages";
            this.txtMessageHTML.WordWrap = false;
            // 
            // checkRawEdit
            // 
            this.checkRawEdit.AutoSize = true;
            this.checkRawEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkRawEdit.Location = new System.Drawing.Point(3, 241);
            this.checkRawEdit.Name = "checkRawEdit";
            this.checkRawEdit.Size = new System.Drawing.Size(338, 17);
            this.checkRawEdit.TabIndex = 7;
            this.checkRawEdit.Text = "Raw editor";
            this.checkRawEdit.UseVisualStyleBackColor = true;
            // 
            // msgbarHtml
            // 
            this.msgbarHtml.BackColor = System.Drawing.Color.Transparent;
            this.msgbarHtml.Dock = System.Windows.Forms.DockStyle.Top;
            this.msgbarHtml.Location = new System.Drawing.Point(3, 3);
            this.msgbarHtml.Name = "msgbarHtml";
            this.msgbarHtml.Size = new System.Drawing.Size(342, 25);
            this.msgbarHtml.TabIndex = 3;
            this.msgbarHtml.Target = null;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtSubjects);
            this.tabPage3.Controls.Add(this.msgbarSubjects);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(344, 261);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Subjects";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtSubjects
            // 
            this.txtSubjects.AcceptsReturn = true;
            this.txtSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubjects.Location = new System.Drawing.Point(3, 28);
            this.txtSubjects.Multiline = true;
            this.txtSubjects.Name = "txtSubjects";
            this.txtSubjects.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubjects.Size = new System.Drawing.Size(338, 230);
            this.txtSubjects.TabIndex = 6;
            this.txtSubjects.Tag = "CampaignMessages";
            this.txtSubjects.WordWrap = false;
            // 
            // msgbarSubjects
            // 
            this.msgbarSubjects.BackColor = System.Drawing.Color.Transparent;
            this.msgbarSubjects.Dock = System.Windows.Forms.DockStyle.Top;
            this.msgbarSubjects.Location = new System.Drawing.Point(3, 3);
            this.msgbarSubjects.Name = "msgbarSubjects";
            this.msgbarSubjects.Size = new System.Drawing.Size(342, 25);
            this.msgbarSubjects.TabIndex = 5;
            this.msgbarSubjects.Target = null;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.toolStrip7);
            this.tabPage4.Controls.Add(this.toolStrip1);
            this.tabPage4.Controls.Add(this.toolStrip5);
            this.tabPage4.Controls.Add(this.txtAttachments);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.txtFromEmailPrefix);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.txtFromAliases);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(344, 261);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FROMs and Attachments";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // toolStrip7
            // 
            this.toolStrip7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip7.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip7.CanOverflow = false;
            this.toolStrip7.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip7.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton41});
            this.toolStrip7.Location = new System.Drawing.Point(317, 174);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.Size = new System.Drawing.Size(26, 25);
            this.toolStrip7.TabIndex = 11;
            this.toolStrip7.Text = "toolStrip7";
            // 
            // toolStripButton41
            // 
            this.toolStripButton41.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton41.Image = global::Injector.Properties.Resources.attach;
            this.toolStripButton41.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton41.Name = "toolStripButton41";
            this.toolStripButton41.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton41.Text = "toolStripButton41";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(288, 8);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(49, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Insert special tag";
            this.toolStripButton1.ToolTipText = "Insert special tag";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Custom macros";
            this.toolStripButton2.ToolTipText = "Custom macros";
            // 
            // toolStrip5
            // 
            this.toolStrip5.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip5.CanOverflow = false;
            this.toolStrip5.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton11,
            this.toolStripButton28});
            this.toolStrip5.Location = new System.Drawing.Point(128, 8);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(49, 25);
            this.toolStrip5.TabIndex = 9;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "Insert special tag";
            this.toolStripButton11.ToolTipText = "Insert special tag";
            // 
            // toolStripButton28
            // 
            this.toolStripButton28.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton28.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton28.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton28.Name = "toolStripButton28";
            this.toolStripButton28.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton28.Text = "Custom macros";
            this.toolStripButton28.ToolTipText = "Custom macros";
            // 
            // txtAttachments
            // 
            this.txtAttachments.AcceptsReturn = true;
            this.txtAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAttachments.Location = new System.Drawing.Point(8, 206);
            this.txtAttachments.Multiline = true;
            this.txtAttachments.Name = "txtAttachments";
            this.txtAttachments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAttachments.Size = new System.Drawing.Size(332, 49);
            this.txtAttachments.TabIndex = 4;
            this.txtAttachments.Tag = "CampaignMessages";
            this.txtAttachments.WordWrap = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(8, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "Attachments:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "FROM Email Prefix:";
            // 
            // txtFromEmailPrefix
            // 
            this.txtFromEmailPrefix.AcceptsReturn = true;
            this.txtFromEmailPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFromEmailPrefix.Location = new System.Drawing.Point(8, 40);
            this.txtFromEmailPrefix.Multiline = true;
            this.txtFromEmailPrefix.Name = "txtFromEmailPrefix";
            this.txtFromEmailPrefix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFromEmailPrefix.Size = new System.Drawing.Size(171, 134);
            this.txtFromEmailPrefix.TabIndex = 2;
            this.txtFromEmailPrefix.Tag = "CampaignMessages";
            this.txtFromEmailPrefix.WordWrap = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(165, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "FROM Aliases:";
            // 
            // txtFromAliases
            // 
            this.txtFromAliases.AcceptsReturn = true;
            this.txtFromAliases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromAliases.Location = new System.Drawing.Point(166, 40);
            this.txtFromAliases.Multiline = true;
            this.txtFromAliases.Name = "txtFromAliases";
            this.txtFromAliases.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFromAliases.Size = new System.Drawing.Size(171, 134);
            this.txtFromAliases.TabIndex = 3;
            this.txtFromAliases.Tag = "CampaignMessages";
            this.txtFromAliases.WordWrap = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtMessageSource);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Controls.Add(this.msgbarSource);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(344, 261);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Message Source";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtMessageSource
            // 
            this.txtMessageSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageSource.Enabled = false;
            this.txtMessageSource.Location = new System.Drawing.Point(3, 76);
            this.txtMessageSource.Multiline = true;
            this.txtMessageSource.Name = "txtMessageSource";
            this.txtMessageSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageSource.Size = new System.Drawing.Size(338, 182);
            this.txtMessageSource.TabIndex = 9;
            this.txtMessageSource.Tag = "CampaignMessages";
            this.txtMessageSource.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMessageSourceEdit);
            this.groupBox1.Controls.Add(this.btnRefreshSource);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 48);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // chkMessageSourceEdit
            // 
            this.chkMessageSourceEdit.AutoSize = true;
            this.chkMessageSourceEdit.Location = new System.Drawing.Point(8, 16);
            this.chkMessageSourceEdit.Name = "chkMessageSourceEdit";
            this.chkMessageSourceEdit.Size = new System.Drawing.Size(144, 17);
            this.chkMessageSourceEdit.TabIndex = 5;
            this.chkMessageSourceEdit.Text = "Custom Message Source";
            this.chkMessageSourceEdit.UseVisualStyleBackColor = true;
            // 
            // btnRefreshSource
            // 
            this.btnRefreshSource.Enabled = false;
            this.btnRefreshSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshSource.Location = new System.Drawing.Point(160, 16);
            this.btnRefreshSource.Name = "btnRefreshSource";
            this.btnRefreshSource.Size = new System.Drawing.Size(72, 23);
            this.btnRefreshSource.TabIndex = 6;
            this.btnRefreshSource.Text = "Refresh";
            this.btnRefreshSource.UseVisualStyleBackColor = true;
            // 
            // msgbarSource
            // 
            this.msgbarSource.BackColor = System.Drawing.Color.Transparent;
            this.msgbarSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.msgbarSource.Location = new System.Drawing.Point(3, 3);
            this.msgbarSource.Name = "msgbarSource";
            this.msgbarSource.Size = new System.Drawing.Size(342, 25);
            this.msgbarSource.TabIndex = 7;
            this.msgbarSource.Target = null;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnPreviewOutlook);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(344, 261);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Preview";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnPreviewOutlook
            // 
            this.btnPreviewOutlook.Location = new System.Drawing.Point(8, 8);
            this.btnPreviewOutlook.Name = "btnPreviewOutlook";
            this.btnPreviewOutlook.Size = new System.Drawing.Size(160, 24);
            this.btnPreviewOutlook.TabIndex = 0;
            this.btnPreviewOutlook.Text = "Preview in Outlook";
            this.btnPreviewOutlook.UseVisualStyleBackColor = true;
            // 
            // menuTab
            // 
            this.menuTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMessageToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.cloneToolStripMenuItem,
            this.toolStripMenuItem1,
            this.enabledToolStripMenuItem});
            this.menuTab.Name = "menuTab";
            this.menuTab.Size = new System.Drawing.Size(153, 98);
            // 
            // newMessageToolStripMenuItem
            // 
            this.newMessageToolStripMenuItem.Name = "newMessageToolStripMenuItem";
            this.newMessageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newMessageToolStripMenuItem.Text = "New &Message";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cloneToolStripMenuItem.Text = "Clone";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // enabledToolStripMenuItem
            // 
            this.enabledToolStripMenuItem.Checked = true;
            this.enabledToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledToolStripMenuItem.Name = "enabledToolStripMenuItem";
            this.enabledToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.enabledToolStripMenuItem.Text = "Enabled";
            // 
            // MessageWindow
            // 
            this.ClientSize = new System.Drawing.Size(348, 312);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comboBox);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.TabPageContextMenuStrip = this.menuTab;
            this.TabText = "Messages";
            this.Text = "Message Window";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.menuTab.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtAttachments;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFromEmailPrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFromAliases;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnRefreshSource;
        private System.Windows.Forms.CheckBox chkMessageSourceEdit;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnPreviewOutlook;
        private System.Windows.Forms.TextBox txtMessageText;
        private Injector.UI.Controls.MessageToolbar.MessageToolbar msgbarText;
        private Injector.UI.Controls.MessageToolbar.MessageToolbar msgbarHtml;
        private System.Windows.Forms.TextBox txtSubjects;
        private Injector.UI.Controls.MessageToolbar.MessageToolbar msgbarSubjects;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton28;
        private System.Windows.Forms.ToolStrip toolStrip7;
        private System.Windows.Forms.ToolStripButton toolStripButton41;
        private Injector.UI.Controls.MessageToolbar.MessageToolbar msgbarSource;
        private System.Windows.Forms.TextBox txtMessageSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip menuTab;
        private System.Windows.Forms.ToolStripMenuItem newMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enabledToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.CheckBox checkRawEdit;
        private System.Windows.Forms.TextBox txtMessageHTML;
        private Injector.UI.Controls.WRichEditBox richMessageHTML;
    }
}