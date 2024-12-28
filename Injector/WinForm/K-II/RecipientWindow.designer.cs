namespace Injector
{
    partial class RecipientWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipientWindow));
            this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRcptRemoveAll = new System.Windows.Forms.Button();
            this.btnRcptDeleteFiles = new System.Windows.Forms.Button();
            this.btnRcptAddFiles = new System.Windows.Forms.Button();
            this.btnRcptAddDir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvRecipients = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dlgFolderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuTabPage
            // 
            this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
            this.contextMenuTabPage.Name = "contextMenuTabPage";
            this.contextMenuTabPage.Size = new System.Drawing.Size(128, 70);
            // 
            // menuItem3
            // 
            this.menuItem3.Name = "menuItem3";
            this.menuItem3.Size = new System.Drawing.Size(127, 22);
            this.menuItem3.Text = "Option &1";
            // 
            // menuItem4
            // 
            this.menuItem4.Name = "menuItem4";
            this.menuItem4.Size = new System.Drawing.Size(127, 22);
            this.menuItem4.Text = "Option &2";
            // 
            // menuItem5
            // 
            this.menuItem5.Name = "menuItem5";
            this.menuItem5.Size = new System.Drawing.Size(127, 22);
            this.menuItem5.Text = "Option &3";
            // 
            // btnRcptRemoveAll
            // 
            this.btnRcptRemoveAll.Location = new System.Drawing.Point(8, 104);
            this.btnRcptRemoveAll.Name = "btnRcptRemoveAll";
            this.btnRcptRemoveAll.Size = new System.Drawing.Size(88, 23);
            this.btnRcptRemoveAll.TabIndex = 9;
            this.btnRcptRemoveAll.Text = "Remove All";
            this.btnRcptRemoveAll.UseVisualStyleBackColor = true;
            // 
            // btnRcptDeleteFiles
            // 
            this.btnRcptDeleteFiles.Location = new System.Drawing.Point(8, 72);
            this.btnRcptDeleteFiles.Name = "btnRcptDeleteFiles";
            this.btnRcptDeleteFiles.Size = new System.Drawing.Size(88, 23);
            this.btnRcptDeleteFiles.TabIndex = 8;
            this.btnRcptDeleteFiles.Text = "Delete from list";
            this.btnRcptDeleteFiles.UseVisualStyleBackColor = true;
            // 
            // btnRcptAddFiles
            // 
            this.btnRcptAddFiles.Location = new System.Drawing.Point(8, 8);
            this.btnRcptAddFiles.Name = "btnRcptAddFiles";
            this.btnRcptAddFiles.Size = new System.Drawing.Size(88, 23);
            this.btnRcptAddFiles.TabIndex = 7;
            this.btnRcptAddFiles.Text = "&Add Files...";
            this.btnRcptAddFiles.UseVisualStyleBackColor = true;
            // 
            // btnRcptAddDir
            // 
            this.btnRcptAddDir.Location = new System.Drawing.Point(8, 40);
            this.btnRcptAddDir.Name = "btnRcptAddDir";
            this.btnRcptAddDir.Size = new System.Drawing.Size(88, 23);
            this.btnRcptAddDir.TabIndex = 10;
            this.btnRcptAddDir.Text = "&Add Dir...";
            this.btnRcptAddDir.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRcptAddFiles);
            this.panel1.Controls.Add(this.btnRcptAddDir);
            this.panel1.Controls.Add(this.btnRcptDeleteFiles);
            this.panel1.Controls.Add(this.btnRcptRemoveAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(327, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 243);
            this.panel1.TabIndex = 11;
            // 
            // lvRecipients
            // 
            this.lvRecipients.CheckBoxes = true;
            this.lvRecipients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20});
            this.lvRecipients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRecipients.FullRowSelect = true;
            this.lvRecipients.HideSelection = false;
            this.lvRecipients.Location = new System.Drawing.Point(0, 4);
            this.lvRecipients.Name = "lvRecipients";
            this.lvRecipients.Size = new System.Drawing.Size(327, 243);
            this.lvRecipients.TabIndex = 12;
            this.lvRecipients.UseCompatibleStateImageBehavior = false;
            this.lvRecipients.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "File Name";
            this.columnHeader8.Width = 89;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Size";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Recipient Column";
            this.columnHeader11.Width = 118;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Headers";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Delimiter";
            this.columnHeader17.Width = 66;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Quote";
            this.columnHeader18.Width = 50;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "Escape";
            this.columnHeader19.Width = 52;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Trim Spaces";
            this.columnHeader20.Width = 76;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "dlgOpen";
            // 
            // RecipientWindow
            // 
            this.ClientSize = new System.Drawing.Size(431, 247);
            this.Controls.Add(this.lvRecipients);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecipientWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.TabPageContextMenuStrip = this.contextMenuTabPage;
            this.TabText = "Recipients";
            this.Text = "Recipient Window";
            this.contextMenuTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
		private System.Windows.Forms.ToolStripMenuItem menuItem3;
		private System.Windows.Forms.ToolStripMenuItem menuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuItem5;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnRcptRemoveAll;
        private System.Windows.Forms.Button btnRcptDeleteFiles;
        private System.Windows.Forms.Button btnRcptAddFiles;
        private System.Windows.Forms.Button btnRcptAddDir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvRecipients;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowse;
    }
}