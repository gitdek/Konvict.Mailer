namespace Injector
{
    partial class CampaignScheduleDialog
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboScheduleType = new System.Windows.Forms.ComboBox();
            this.cboOccurence = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTimeBased = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cboMinute = new System.Windows.Forms.ComboBox();
            this.cboHour = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDay = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNextRun = new System.Windows.Forms.LinkLabel();
            this.pnlInterval = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.nudMins = new System.Windows.Forms.NumericUpDown();
            this.nudHours = new System.Windows.Forms.NumericUpDown();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkExpires = new System.Windows.Forms.CheckBox();
            this.dtExpiration = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clndrExpire = new System.Windows.Forms.MonthCalendar();
            this.lnkAboutForceRun = new System.Windows.Forms.LinkLabel();
            this.chkForceRun = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlTimeBased.SuspendLayout();
            this.pnlInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(465, 24);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Scheduler Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Schedule Type:";
            // 
            // cboScheduleType
            // 
            this.cboScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScheduleType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboScheduleType.FormattingEnabled = true;
            this.cboScheduleType.Items.AddRange(new object[] {
            "Time based (Set time)",
            "Interval based (Last run + interval)"});
            this.cboScheduleType.Location = new System.Drawing.Point(104, 80);
            this.cboScheduleType.Name = "cboScheduleType";
            this.cboScheduleType.Size = new System.Drawing.Size(136, 21);
            this.cboScheduleType.TabIndex = 9;
            // 
            // cboOccurence
            // 
            this.cboOccurence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOccurence.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOccurence.FormattingEnabled = true;
            this.cboOccurence.Items.AddRange(new object[] {
            "Once",
            "Repeat",
            "Every Startup"});
            this.cboOccurence.Location = new System.Drawing.Point(104, 56);
            this.cboOccurence.Name = "cboOccurence";
            this.cboOccurence.Size = new System.Drawing.Size(136, 21);
            this.cboOccurence.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Occurrence:";
            // 
            // pnlTimeBased
            // 
            this.pnlTimeBased.Controls.Add(this.label8);
            this.pnlTimeBased.Controls.Add(this.cboMinute);
            this.pnlTimeBased.Controls.Add(this.cboHour);
            this.pnlTimeBased.Controls.Add(this.label7);
            this.pnlTimeBased.Controls.Add(this.cboDay);
            this.pnlTimeBased.Controls.Add(this.label6);
            this.pnlTimeBased.Controls.Add(this.lblNextRun);
            this.pnlTimeBased.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTimeBased.Location = new System.Drawing.Point(16, 112);
            this.pnlTimeBased.Name = "pnlTimeBased";
            this.pnlTimeBased.Size = new System.Drawing.Size(224, 112);
            this.pnlTimeBased.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Minute:";
            // 
            // cboMinute
            // 
            this.cboMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinute.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMinute.FormattingEnabled = true;
            this.cboMinute.Location = new System.Drawing.Point(72, 56);
            this.cboMinute.Name = "cboMinute";
            this.cboMinute.Size = new System.Drawing.Size(136, 21);
            this.cboMinute.TabIndex = 13;
            // 
            // cboHour
            // 
            this.cboHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHour.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHour.FormattingEnabled = true;
            this.cboHour.Location = new System.Drawing.Point(72, 32);
            this.cboHour.Name = "cboHour";
            this.cboHour.Size = new System.Drawing.Size(136, 21);
            this.cboHour.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Hour:";
            // 
            // cboDay
            // 
            this.cboDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDay.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDay.FormattingEnabled = true;
            this.cboDay.Location = new System.Drawing.Point(72, 8);
            this.cboDay.Name = "cboDay";
            this.cboDay.Size = new System.Drawing.Size(136, 21);
            this.cboDay.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Day:";
            // 
            // lblNextRun
            // 
            this.lblNextRun.AutoSize = true;
            this.lblNextRun.Location = new System.Drawing.Point(112, 88);
            this.lblNextRun.Name = "lblNextRun";
            this.lblNextRun.Size = new System.Drawing.Size(99, 13);
            this.lblNextRun.TabIndex = 1;
            this.lblNextRun.TabStop = true;
            this.lblNextRun.Text = "Calculate Next Run";
            this.lblNextRun.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblNextRun_LinkClicked);
            // 
            // pnlInterval
            // 
            this.pnlInterval.Controls.Add(this.linkLabel1);
            this.pnlInterval.Controls.Add(this.nudMins);
            this.pnlInterval.Controls.Add(this.nudHours);
            this.pnlInterval.Controls.Add(this.nudDays);
            this.pnlInterval.Controls.Add(this.label5);
            this.pnlInterval.Controls.Add(this.label4);
            this.pnlInterval.Controls.Add(this.label3);
            this.pnlInterval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlInterval.Location = new System.Drawing.Point(16, 232);
            this.pnlInterval.Name = "pnlInterval";
            this.pnlInterval.Size = new System.Drawing.Size(224, 112);
            this.pnlInterval.TabIndex = 12;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(112, 88);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(99, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Calculate Next Run";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // nudMins
            // 
            this.nudMins.Location = new System.Drawing.Point(72, 56);
            this.nudMins.Name = "nudMins";
            this.nudMins.Size = new System.Drawing.Size(136, 21);
            this.nudMins.TabIndex = 8;
            this.nudMins.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // nudHours
            // 
            this.nudHours.Location = new System.Drawing.Point(72, 32);
            this.nudHours.Name = "nudHours";
            this.nudHours.Size = new System.Drawing.Size(136, 21);
            this.nudHours.TabIndex = 7;
            this.nudHours.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // nudDays
            // 
            this.nudDays.Location = new System.Drawing.Point(72, 8);
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(136, 21);
            this.nudDays.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Minutes:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Hours:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Days:";
            // 
            // chkExpires
            // 
            this.chkExpires.AutoSize = true;
            this.chkExpires.Checked = true;
            this.chkExpires.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExpires.Location = new System.Drawing.Point(8, 72);
            this.chkExpires.Name = "chkExpires";
            this.chkExpires.Size = new System.Drawing.Size(78, 17);
            this.chkExpires.TabIndex = 13;
            this.chkExpires.Text = "Expiration:";
            this.chkExpires.UseVisualStyleBackColor = true;
            // 
            // dtExpiration
            // 
            this.dtExpiration.CustomFormat = "";
            this.dtExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtExpiration.Location = new System.Drawing.Point(8, 96);
            this.dtExpiration.Name = "dtExpiration";
            this.dtExpiration.ShowUpDown = true;
            this.dtExpiration.Size = new System.Drawing.Size(208, 21);
            this.dtExpiration.TabIndex = 14;
            this.dtExpiration.Value = new System.DateTime(2009, 5, 2, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clndrExpire);
            this.groupBox1.Controls.Add(this.lnkAboutForceRun);
            this.groupBox1.Controls.Add(this.chkForceRun);
            this.groupBox1.Controls.Add(this.chkExpires);
            this.groupBox1.Controls.Add(this.dtExpiration);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(256, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 296);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced";
            // 
            // clndrExpire
            // 
            this.clndrExpire.BackColor = System.Drawing.SystemColors.Window;
            this.clndrExpire.Location = new System.Drawing.Point(8, 128);
            this.clndrExpire.MaxSelectionCount = 1;
            this.clndrExpire.Name = "clndrExpire";
            this.clndrExpire.TabIndex = 17;
            this.clndrExpire.TitleBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            // 
            // lnkAboutForceRun
            // 
            this.lnkAboutForceRun.AutoSize = true;
            this.lnkAboutForceRun.Location = new System.Drawing.Point(88, 32);
            this.lnkAboutForceRun.Name = "lnkAboutForceRun";
            this.lnkAboutForceRun.Size = new System.Drawing.Size(12, 13);
            this.lnkAboutForceRun.TabIndex = 16;
            this.lnkAboutForceRun.TabStop = true;
            this.lnkAboutForceRun.Text = "?";
            this.lnkAboutForceRun.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAboutForceRun_LinkClicked);
            // 
            // chkForceRun
            // 
            this.chkForceRun.AutoSize = true;
            this.chkForceRun.Location = new System.Drawing.Point(8, 32);
            this.chkForceRun.Name = "chkForceRun";
            this.chkForceRun.Size = new System.Drawing.Size(75, 17);
            this.chkForceRun.TabIndex = 15;
            this.chkForceRun.Text = "Force Run";
            this.chkForceRun.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(306, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(394, 369);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 24);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // CampaignScheduleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(481, 404);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlInterval);
            this.Controls.Add(this.pnlTimeBased);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboScheduleType);
            this.Controls.Add(this.cboOccurence);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CampaignScheduleDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Konvict";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CampaignScheduleDialog_Load);
            this.pnlTimeBased.ResumeLayout(false);
            this.pnlTimeBased.PerformLayout();
            this.pnlInterval.ResumeLayout(false);
            this.pnlInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboScheduleType;
        private System.Windows.Forms.ComboBox cboOccurence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTimeBased;
        private System.Windows.Forms.LinkLabel lblNextRun;
        private System.Windows.Forms.Panel pnlInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMins;
        private System.Windows.Forms.NumericUpDown nudHours;
        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.CheckBox chkExpires;
        private System.Windows.Forms.DateTimePicker dtExpiration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkForceRun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboMinute;
        private System.Windows.Forms.ComboBox cboHour;
        private System.Windows.Forms.LinkLabel lnkAboutForceRun;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MonthCalendar clndrExpire;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}