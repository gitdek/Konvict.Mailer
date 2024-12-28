namespace Injector
{
    partial class ConfigDialog
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkSelectSCPClient = new System.Windows.Forms.LinkLabel();
            this.lblSCPClient = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lnkSelectSSHClient = new System.Windows.Forms.LinkLabel();
            this.lblSSHClient = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkSelectPrivKey = new System.Windows.Forms.LinkLabel();
            this.lblPrivKey = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUploadPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTestPollItem = new System.Windows.Forms.Button();
            this.btnEditCancel = new System.Windows.Forms.Button();
            this.lnkPollHelp = new System.Windows.Forms.LinkLabel();
            this.btnAddPoll = new System.Windows.Forms.Button();
            this.cboExportAction = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lstPolls = new System.Windows.Forms.CheckedListBox();
            this.txtPollCommand = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPollInterval = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDbUser = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDbPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDbHost = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(696, 24);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Configuration";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(534, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(622, 398);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lnkSelectSCPClient);
            this.groupBox1.Controls.Add(this.lblSCPClient);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lnkSelectSSHClient);
            this.groupBox1.Controls.Add(this.lblSSHClient);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lnkSelectPrivKey);
            this.groupBox1.Controls.Add(this.lblPrivKey);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 128);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SSH/SCP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(80, 96);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(48, 21);
            this.txtPort.TabIndex = 10;
            this.txtPort.Text = "22";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Port:";
            // 
            // lnkSelectSCPClient
            // 
            this.lnkSelectSCPClient.AutoSize = true;
            this.lnkSelectSCPClient.BackColor = System.Drawing.Color.Transparent;
            this.lnkSelectSCPClient.Location = new System.Drawing.Point(344, 72);
            this.lnkSelectSCPClient.Name = "lnkSelectSCPClient";
            this.lnkSelectSCPClient.Size = new System.Drawing.Size(36, 13);
            this.lnkSelectSCPClient.TabIndex = 8;
            this.lnkSelectSCPClient.TabStop = true;
            this.lnkSelectSCPClient.Text = "Select";
            this.lnkSelectSCPClient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectSCPClient_LinkClicked);
            // 
            // lblSCPClient
            // 
            this.lblSCPClient.AutoEllipsis = true;
            this.lblSCPClient.BackColor = System.Drawing.Color.Transparent;
            this.lblSCPClient.Location = new System.Drawing.Point(80, 72);
            this.lblSCPClient.Name = "lblSCPClient";
            this.lblSCPClient.Size = new System.Drawing.Size(256, 16);
            this.lblSCPClient.TabIndex = 7;
            this.lblSCPClient.Text = "none";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "SCP Client:";
            // 
            // lnkSelectSSHClient
            // 
            this.lnkSelectSSHClient.AutoSize = true;
            this.lnkSelectSSHClient.BackColor = System.Drawing.Color.Transparent;
            this.lnkSelectSSHClient.Location = new System.Drawing.Point(344, 48);
            this.lnkSelectSSHClient.Name = "lnkSelectSSHClient";
            this.lnkSelectSSHClient.Size = new System.Drawing.Size(36, 13);
            this.lnkSelectSSHClient.TabIndex = 5;
            this.lnkSelectSSHClient.TabStop = true;
            this.lnkSelectSSHClient.Text = "Select";
            this.lnkSelectSSHClient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectSSHClient_LinkClicked);
            // 
            // lblSSHClient
            // 
            this.lblSSHClient.AutoEllipsis = true;
            this.lblSSHClient.BackColor = System.Drawing.Color.Transparent;
            this.lblSSHClient.Location = new System.Drawing.Point(80, 48);
            this.lblSSHClient.Name = "lblSSHClient";
            this.lblSSHClient.Size = new System.Drawing.Size(256, 16);
            this.lblSSHClient.TabIndex = 4;
            this.lblSSHClient.Text = "none";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "SSH2 Client:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Private Key:";
            // 
            // lnkSelectPrivKey
            // 
            this.lnkSelectPrivKey.AutoSize = true;
            this.lnkSelectPrivKey.BackColor = System.Drawing.Color.Transparent;
            this.lnkSelectPrivKey.Location = new System.Drawing.Point(344, 24);
            this.lnkSelectPrivKey.Name = "lnkSelectPrivKey";
            this.lnkSelectPrivKey.Size = new System.Drawing.Size(36, 13);
            this.lnkSelectPrivKey.TabIndex = 1;
            this.lnkSelectPrivKey.TabStop = true;
            this.lnkSelectPrivKey.Text = "Select";
            this.lnkSelectPrivKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectPubKey_LinkClicked);
            // 
            // lblPrivKey
            // 
            this.lblPrivKey.AutoEllipsis = true;
            this.lblPrivKey.BackColor = System.Drawing.Color.Transparent;
            this.lblPrivKey.Location = new System.Drawing.Point(80, 24);
            this.lblPrivKey.Name = "lblPrivKey";
            this.lblPrivKey.Size = new System.Drawing.Size(256, 16);
            this.lblPrivKey.TabIndex = 0;
            this.lblPrivKey.Text = "none";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtUploadPath);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 56);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "KLocal Defaults";
            // 
            // txtUploadPath
            // 
            this.txtUploadPath.Location = new System.Drawing.Point(88, 24);
            this.txtUploadPath.Name = "txtUploadPath";
            this.txtUploadPath.Size = new System.Drawing.Size(256, 21);
            this.txtUploadPath.TabIndex = 11;
            this.txtUploadPath.Text = "/etc/klocal/";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Upload Path:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeToolStripMenuItem,
            this.removeAllFromListToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 76);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.editToolStripMenuItem.Text = "Edit Selected";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // removeAllFromListToolStripMenuItem
            // 
            this.removeAllFromListToolStripMenuItem.Name = "removeAllFromListToolStripMenuItem";
            this.removeAllFromListToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.removeAllFromListToolStripMenuItem.Text = "Remove all from list";
            this.removeAllFromListToolStripMenuItem.Click += new System.EventHandler(this.removeAllFromListToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTestPollItem);
            this.groupBox3.Controls.Add(this.btnEditCancel);
            this.groupBox3.Controls.Add(this.lnkPollHelp);
            this.groupBox3.Controls.Add(this.btnAddPoll);
            this.groupBox3.Controls.Add(this.cboExportAction);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.lstPolls);
            this.groupBox3.Controls.Add(this.txtPollCommand);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPollInterval);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(8, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(392, 184);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Polling";
            // 
            // btnTestPollItem
            // 
            this.btnTestPollItem.Location = new System.Drawing.Point(144, 152);
            this.btnTestPollItem.Name = "btnTestPollItem";
            this.btnTestPollItem.Size = new System.Drawing.Size(75, 23);
            this.btnTestPollItem.TabIndex = 23;
            this.btnTestPollItem.Text = "Test";
            this.btnTestPollItem.UseVisualStyleBackColor = true;
            this.btnTestPollItem.Click += new System.EventHandler(this.btnTestPollItem_Click);
            // 
            // btnEditCancel
            // 
            this.btnEditCancel.Location = new System.Drawing.Point(232, 152);
            this.btnEditCancel.Name = "btnEditCancel";
            this.btnEditCancel.Size = new System.Drawing.Size(64, 24);
            this.btnEditCancel.TabIndex = 22;
            this.btnEditCancel.Text = "Cancel";
            this.btnEditCancel.UseVisualStyleBackColor = true;
            this.btnEditCancel.Visible = false;
            this.btnEditCancel.Click += new System.EventHandler(this.btnEditCancel_Click);
            // 
            // lnkPollHelp
            // 
            this.lnkPollHelp.AutoSize = true;
            this.lnkPollHelp.BackColor = System.Drawing.Color.Transparent;
            this.lnkPollHelp.Location = new System.Drawing.Point(352, 16);
            this.lnkPollHelp.Name = "lnkPollHelp";
            this.lnkPollHelp.Size = new System.Drawing.Size(28, 13);
            this.lnkPollHelp.TabIndex = 21;
            this.lnkPollHelp.TabStop = true;
            this.lnkPollHelp.Text = "Help";
            // 
            // btnAddPoll
            // 
            this.btnAddPoll.Location = new System.Drawing.Point(304, 152);
            this.btnAddPoll.Name = "btnAddPoll";
            this.btnAddPoll.Size = new System.Drawing.Size(64, 24);
            this.btnAddPoll.TabIndex = 20;
            this.btnAddPoll.Text = "Add";
            this.btnAddPoll.UseVisualStyleBackColor = true;
            this.btnAddPoll.Click += new System.EventHandler(this.btnAddPoll_Click);
            // 
            // cboExportAction
            // 
            this.cboExportAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExportAction.FormattingEnabled = true;
            this.cboExportAction.Items.AddRange(new object[] {
            "Disabled",
            "Display datagrid (XML Only)",
            "Display plain text",
            "Update Campaign Statistics",
            "Save To File ..."});
            this.cboExportAction.Location = new System.Drawing.Point(144, 120);
            this.cboExportAction.Name = "cboExportAction";
            this.cboExportAction.Size = new System.Drawing.Size(216, 21);
            this.cboExportAction.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(144, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Export Action:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(272, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "seconds";
            // 
            // lstPolls
            // 
            this.lstPolls.ContextMenuStrip = this.contextMenuStrip1;
            this.lstPolls.FormattingEnabled = true;
            this.lstPolls.Location = new System.Drawing.Point(8, 16);
            this.lstPolls.Name = "lstPolls";
            this.lstPolls.Size = new System.Drawing.Size(120, 164);
            this.lstPolls.TabIndex = 0;
            this.lstPolls.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstPolls_MouseUp);
            // 
            // txtPollCommand
            // 
            this.txtPollCommand.Location = new System.Drawing.Point(144, 72);
            this.txtPollCommand.Name = "txtPollCommand";
            this.txtPollCommand.Size = new System.Drawing.Size(216, 21);
            this.txtPollCommand.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(144, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Command:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Poll Interval:";
            // 
            // txtPollInterval
            // 
            this.txtPollInterval.Location = new System.Drawing.Point(224, 32);
            this.txtPollInterval.Name = "txtPollInterval";
            this.txtPollInterval.Size = new System.Drawing.Size(40, 21);
            this.txtPollInterval.TabIndex = 13;
            this.txtPollInterval.Text = "30";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtDbName);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtDbPassword);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txtDbUser);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtDbPort);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtDbHost);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(408, 40);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(296, 128);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Campaign Management Database";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(80, 96);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(120, 21);
            this.txtDbName.TabIndex = 19;
            this.txtDbName.Text = "campaign_manager";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Database:";
            // 
            // txtDbPassword
            // 
            this.txtDbPassword.Location = new System.Drawing.Point(80, 72);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.Size = new System.Drawing.Size(120, 21);
            this.txtDbPassword.TabIndex = 17;
            this.txtDbPassword.UseSystemPasswordChar = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "&Password:";
            // 
            // txtDbUser
            // 
            this.txtDbUser.Location = new System.Drawing.Point(80, 48);
            this.txtDbUser.Name = "txtDbUser";
            this.txtDbUser.Size = new System.Drawing.Size(120, 21);
            this.txtDbUser.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Username:";
            // 
            // txtDbPort
            // 
            this.txtDbPort.Location = new System.Drawing.Point(248, 24);
            this.txtDbPort.Name = "txtDbPort";
            this.txtDbPort.Size = new System.Drawing.Size(40, 21);
            this.txtDbPort.TabIndex = 13;
            this.txtDbPort.Text = "3306";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(208, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Port:";
            // 
            // txtDbHost
            // 
            this.txtDbHost.Location = new System.Drawing.Point(80, 24);
            this.txtDbHost.Name = "txtDbHost";
            this.txtDbHost.Size = new System.Drawing.Size(120, 21);
            this.txtDbHost.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Server &Host:";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(712, 429);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Konvict";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkSelectPrivKey;
        private System.Windows.Forms.Label lblPrivKey;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkSelectSCPClient;
        private System.Windows.Forms.Label lblSCPClient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lnkSelectSSHClient;
        private System.Windows.Forms.Label lblSSHClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtUploadPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeAllFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTestPollItem;
        private System.Windows.Forms.Button btnEditCancel;
        private System.Windows.Forms.LinkLabel lnkPollHelp;
        private System.Windows.Forms.Button btnAddPoll;
        private System.Windows.Forms.ComboBox cboExportAction;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckedListBox lstPolls;
        private System.Windows.Forms.TextBox txtPollCommand;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPollInterval;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDbHost;
        private System.Windows.Forms.TextBox txtDbPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDbUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label14;

    }
}