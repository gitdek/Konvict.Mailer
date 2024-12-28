namespace Injector
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Campaigns");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Servers");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Details");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Logging");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Domain Throttles");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Send", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Options");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Report", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Configuration");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Restart");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("MTAs", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Analysis");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newCampaignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.importCampaignsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toTrayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lockToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.minimizeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.configureKLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.extentionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPollingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.convertMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tvwNavi = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.pnlCampaignSettings = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Options = new System.Windows.Forms.TabPage();
            this.txtFromsCharsets = new System.Windows.Forms.TextBox();
            this.chkMIMEFrom = new System.Windows.Forms.CheckBox();
            this.txtSubjectsCharsets = new System.Windows.Forms.TextBox();
            this.chkMIMESubject = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDomains = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSeedList = new System.Windows.Forms.TextBox();
            this.txtSeedInterval = new System.Windows.Forms.TextBox();
            this.chkSeed = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkSelectSupression = new System.Windows.Forms.LinkLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.lnkSchedulerStatus = new System.Windows.Forms.LinkLabel();
            this.txtThreadsPerConnection = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtConnectionsPerServer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSessionPerConnectionMax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailPerSessionMax = new System.Windows.Forms.TextBox();
            this.cboAddRecipAction = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkSupression = new System.Windows.Forms.CheckBox();
            this.txtSupressionFileName = new System.Windows.Forms.TextBox();
            this.txtSessionPerConnectionMin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmailPerSessionMin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Messages = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtMessageHTML = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtSubjects = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton20 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton21 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton22 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton23 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton24 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton25 = new System.Windows.Forms.ToolStripButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton41 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton26 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton29 = new System.Windows.Forms.ToolStripButton();
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
            this.btnRefreshSource = new System.Windows.Forms.Button();
            this.chkMessageSourceEdit = new System.Windows.Forms.CheckBox();
            this.txtMessageSource = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip8 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton30 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton31 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton32 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton33 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton34 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton35 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton36 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton37 = new System.Windows.Forms.ToolStripButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnPreviewOutlook = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cboMessages = new System.Windows.Forms.ComboBox();
            this.lblMessageID = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewMessage = new System.Windows.Forms.Button();
            this.btnDeleteMessage = new System.Windows.Forms.Button();
            this.Recipients = new System.Windows.Forms.TabPage();
            this.btnRcptSaveChanges = new System.Windows.Forms.Button();
            this.btnModifyRecipientFile = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboTrimSpaces = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEscape = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtQuote = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboHasHeaders = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRecipientColumn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRcptRemoveAll = new System.Windows.Forms.Button();
            this.btnRcptDeleteFiles = new System.Windows.Forms.Button();
            this.btnRcptAddFiles = new System.Windows.Forms.Button();
            this.lvRecipients = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.Advanced = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtPoolName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboRotateKey = new System.Windows.Forms.ComboBox();
            this.txtRotateInterval = new System.Windows.Forms.TextBox();
            this.lvRotate = new System.Windows.Forms.ListView();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
            this.chkRotateMessages = new System.Windows.Forms.CheckBox();
            this.Servers = new System.Windows.Forms.TabPage();
            this.btnInheritServers = new System.Windows.Forms.Button();
            this.btnServersRemoveAll = new System.Windows.Forms.Button();
            this.btnServersDeleteSelected = new System.Windows.Forms.Button();
            this.btnServersAdd = new System.Windows.Forms.Button();
            this.btnServersAddFiles = new System.Windows.Forms.Button();
            this.lvServers = new System.Windows.Forms.ListView();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.lblCampaignName = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.pnlAnalysis = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pbDeliverability = new System.Windows.Forms.ProgressBar();
            this.pbProcessedTtl = new System.Windows.Forms.ProgressBar();
            this.pbProcessedSegment = new System.Windows.Forms.ProgressBar();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.imglstAnalysis = new System.Windows.Forms.ImageList(this.components);
            this.label21 = new System.Windows.Forms.Label();
            this.pnlServers = new System.Windows.Forms.Panel();
            this.lvMasterServers = new System.Windows.Forms.ListView();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
            this.btnMasterServersRemoveAll = new System.Windows.Forms.Button();
            this.btnMasterServersDelete = new System.Windows.Forms.Button();
            this.btnMasterAddServer = new System.Windows.Forms.Button();
            this.btnMasterServersAddFiles = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlCampaigns = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvCampaigns = new System.Windows.Forms.ListView();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCampaignStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCampaignPause = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCampaignStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRefreshClickThrough = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefreshOpens = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefreshUnsubs = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabsPolls = new System.Windows.Forms.TabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.appearanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.flatButtonsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.alignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripButton27 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton42 = new System.Windows.Forms.ToolStripButton();
            this.cntxmnuCampaignRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCreateDatabaseEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuUpdateRedirectUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDeleteDatabaseEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewHits = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewOpens = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewAllEntries = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlCampaignSettings.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Options.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Messages.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.toolStrip7.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.toolStrip8.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.Recipients.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.Advanced.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Servers.SuspendLayout();
            this.pnlAnalysis.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.pnlServers.SuspendLayout();
            this.pnlCampaigns.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabsPolls.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.cntxmnuCampaignRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCampaignToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.importCampaignsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 54);
            // 
            // newCampaignToolStripMenuItem
            // 
            this.newCampaignToolStripMenuItem.Name = "newCampaignToolStripMenuItem";
            this.newCampaignToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.newCampaignToolStripMenuItem.Text = "Add Campaign";
            this.newCampaignToolStripMenuItem.Click += new System.EventHandler(this.newCampaignToolStripMenuItem_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(167, 6);
            // 
            // importCampaignsToolStripMenuItem
            // 
            this.importCampaignsToolStripMenuItem.Name = "importCampaignsToolStripMenuItem";
            this.importCampaignsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.importCampaignsToolStripMenuItem.Text = "Import Campaign";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(918, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toTrayToolStripMenuItem1,
            this.lockToTrayToolStripMenuItem,
            this.toolStripMenuItem1,
            this.minimizeToolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // toTrayToolStripMenuItem1
            // 
            this.toTrayToolStripMenuItem1.Name = "toTrayToolStripMenuItem1";
            this.toTrayToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toTrayToolStripMenuItem1.Text = "To Tray";
            this.toTrayToolStripMenuItem1.Click += new System.EventHandler(this.toTrayToolStripMenuItem1_Click);
            // 
            // lockToTrayToolStripMenuItem
            // 
            this.lockToTrayToolStripMenuItem.Name = "lockToTrayToolStripMenuItem";
            this.lockToTrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.lockToTrayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lockToTrayToolStripMenuItem.Text = "Lock To Tray";
            this.lockToTrayToolStripMenuItem.Click += new System.EventHandler(this.lockToTrayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // minimizeToolStripMenuItem1
            // 
            this.minimizeToolStripMenuItem1.Name = "minimizeToolStripMenuItem1";
            this.minimizeToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.minimizeToolStripMenuItem1.Text = "Minimize";
            this.minimizeToolStripMenuItem1.Click += new System.EventHandler(this.minimizeToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundsToolStripMenuItem,
            this.toolStripMenuItem5,
            this.configureKLocalToolStripMenuItem,
            this.toolStripMenuItem6,
            this.extentionsToolStripMenuItem,
            this.startPollingToolStripMenuItem,
            this.toolStripMenuItem11,
            this.convertMessageToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // soundsToolStripMenuItem
            // 
            this.soundsToolStripMenuItem.Checked = true;
            this.soundsToolStripMenuItem.CheckOnClick = true;
            this.soundsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.soundsToolStripMenuItem.Name = "soundsToolStripMenuItem";
            this.soundsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.soundsToolStripMenuItem.Text = "Sounds";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(166, 6);
            // 
            // configureKLocalToolStripMenuItem
            // 
            this.configureKLocalToolStripMenuItem.Name = "configureKLocalToolStripMenuItem";
            this.configureKLocalToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.configureKLocalToolStripMenuItem.Text = "Configuration";
            this.configureKLocalToolStripMenuItem.Click += new System.EventHandler(this.configureKLocalToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(166, 6);
            // 
            // extentionsToolStripMenuItem
            // 
            this.extentionsToolStripMenuItem.Name = "extentionsToolStripMenuItem";
            this.extentionsToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.extentionsToolStripMenuItem.Text = "Extensions";
            this.extentionsToolStripMenuItem.Click += new System.EventHandler(this.extentionsToolStripMenuItem_Click);
            // 
            // startPollingToolStripMenuItem
            // 
            this.startPollingToolStripMenuItem.Name = "startPollingToolStripMenuItem";
            this.startPollingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.startPollingToolStripMenuItem.Text = "Start Polling";
            this.startPollingToolStripMenuItem.Click += new System.EventHandler(this.startPollingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(166, 6);
            // 
            // convertMessageToolStripMenuItem
            // 
            this.convertMessageToolStripMenuItem.Name = "convertMessageToolStripMenuItem";
            this.convertMessageToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.convertMessageToolStripMenuItem.Text = "&Convert Message";
            this.convertMessageToolStripMenuItem.Click += new System.EventHandler(this.convertMessageToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(174, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem2.Text = "Macro Information";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toTrayToolStripMenuItem,
            this.toolStripSeparator1,
            this.minimizeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toTrayToolStripMenuItem
            // 
            this.toTrayToolStripMenuItem.Name = "toTrayToolStripMenuItem";
            this.toTrayToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.toTrayToolStripMenuItem.Text = "To Tray";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.minimizeToolStripMenuItem.Text = "Minimize";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.windowsToolStripMenuItem.Text = "View";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlCampaignSettings);
            this.splitContainer1.Panel2.Controls.Add(this.pnlAnalysis);
            this.splitContainer1.Panel2.Controls.Add(this.pnlServers);
            this.splitContainer1.Panel2.Controls.Add(this.pnlCampaigns);
            this.splitContainer1.Size = new System.Drawing.Size(918, 641);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tvwNavi, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.930966F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.06903F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(171, 641);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tvwNavi
            // 
            this.tvwNavi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvwNavi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwNavi.ImageIndex = 0;
            this.tvwNavi.ImageList = this.imageList1;
            this.tvwNavi.Location = new System.Drawing.Point(3, 34);
            this.tvwNavi.Name = "tvwNavi";
            treeNode1.ContextMenuStrip = this.contextMenuStrip1;
            treeNode1.ImageKey = "stock_mail-druid.png";
            treeNode1.Name = "Node4";
            treeNode1.SelectedImageKey = "stock_mail-druid.png";
            treeNode1.Tag = "Campaigns";
            treeNode1.Text = "Campaigns";
            treeNode2.ImageKey = "network-config.png";
            treeNode2.Name = "Node6";
            treeNode2.SelectedImageKey = "network-config.png";
            treeNode2.Tag = "Servers";
            treeNode2.Text = "Servers";
            treeNode3.ImageKey = "eyes.png";
            treeNode3.Name = "Node14";
            treeNode3.SelectedImageKey = "eyes.png";
            treeNode3.Tag = "Send_Details";
            treeNode3.Text = "Details";
            treeNode4.ImageKey = "logviewer.png";
            treeNode4.Name = "Node11";
            treeNode4.SelectedImageKey = "logviewer.png";
            treeNode4.Tag = "Send_Logging";
            treeNode4.Text = "Logging";
            treeNode5.ImageKey = "start-here.png";
            treeNode5.Name = "Node12";
            treeNode5.SelectedImageKey = "start-here.png";
            treeNode5.Tag = "Send_Domain_Throttles";
            treeNode5.Text = "Domain Throttles";
            treeNode6.ImageKey = "mail-send-receive.png";
            treeNode6.Name = "Node8";
            treeNode6.SelectedImageKey = "mail-send-receive.png";
            treeNode6.Tag = "Send";
            treeNode6.Text = "Send";
            treeNode7.ImageKey = "preferences-system.png";
            treeNode7.Name = "Node16";
            treeNode7.SelectedImageKey = "preferences-system.png";
            treeNode7.Tag = "Report_Options";
            treeNode7.Text = "Options";
            treeNode8.ImageKey = "utilities-system-monitor.png";
            treeNode8.Name = "Node15";
            treeNode8.SelectedImageKey = "utilities-system-monitor.png";
            treeNode8.Tag = "Report";
            treeNode8.Text = "Report";
            treeNode9.ImageKey = "emblem-new.png";
            treeNode9.Name = "Node0";
            treeNode9.SelectedImageKey = "emblem-new.png";
            treeNode9.Text = "Configuration";
            treeNode10.ImageKey = "emblem-new.png";
            treeNode10.Name = "Node1";
            treeNode10.SelectedImageKey = "emblem-new.png";
            treeNode10.Text = "Restart";
            treeNode11.ImageKey = "mta.png";
            treeNode11.Name = "Node0";
            treeNode11.SelectedImageKey = "mta.png";
            treeNode11.Text = "MTAs";
            treeNode12.ImageKey = "analysis.png";
            treeNode12.Name = "Node0";
            treeNode12.SelectedImageKey = "analysis.png";
            treeNode12.Tag = "Analysis";
            treeNode12.Text = "Analysis";
            this.tvwNavi.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode6,
            treeNode8,
            treeNode11,
            treeNode12});
            this.tvwNavi.SelectedImageIndex = 0;
            this.tvwNavi.Size = new System.Drawing.Size(165, 604);
            this.tvwNavi.TabIndex = 5;
            this.tvwNavi.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwNavi_MouseUp);
            this.tvwNavi.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "preferences-system-session.png");
            this.imageList1.Images.SetKeyName(1, "preferences-desktop-locale.png");
            this.imageList1.Images.SetKeyName(2, "stock_mail-druid.png");
            this.imageList1.Images.SetKeyName(3, "stock_mail.png");
            this.imageList1.Images.SetKeyName(4, "internet-mail.png");
            this.imageList1.Images.SetKeyName(5, "system-users.png");
            this.imageList1.Images.SetKeyName(6, "emblem-system.png");
            this.imageList1.Images.SetKeyName(7, "emblem-important.png");
            this.imageList1.Images.SetKeyName(8, "emblem-readonly.png");
            this.imageList1.Images.SetKeyName(9, "emblem-symbolic-link.png");
            this.imageList1.Images.SetKeyName(10, "emblem-unreadable.png");
            this.imageList1.Images.SetKeyName(11, "network-config.png");
            this.imageList1.Images.SetKeyName(12, "mail-send-receive.png");
            this.imageList1.Images.SetKeyName(13, "eyes.png");
            this.imageList1.Images.SetKeyName(14, "logviewer.png");
            this.imageList1.Images.SetKeyName(15, "start-here.png");
            this.imageList1.Images.SetKeyName(16, "utilities-system-monitor.png");
            this.imageList1.Images.SetKeyName(17, "preferences-system.png");
            this.imageList1.Images.SetKeyName(18, "stock_multiple_file.png");
            this.imageList1.Images.SetKeyName(19, "mta.png");
            this.imageList1.Images.SetKeyName(20, "green_dot.png");
            this.imageList1.Images.SetKeyName(21, "emblem-new.png");
            this.imageList1.Images.SetKeyName(22, "analysis.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(171, 31);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Injector.Properties.Resources.page_white_add;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 28);
            this.toolStripButton1.Text = "Add Campaign";
            this.toolStripButton1.ToolTipText = "Add Campaign";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Injector.Properties.Resources.page_add;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 28);
            this.toolStripButton2.Text = "Import Campaign";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Injector.Properties.Resources.cross;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 28);
            this.toolStripButton3.Text = "Remove";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // pnlCampaignSettings
            // 
            this.pnlCampaignSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCampaignSettings.Controls.Add(this.tableLayoutPanel3);
            this.pnlCampaignSettings.Location = new System.Drawing.Point(8, 16);
            this.pnlCampaignSettings.Name = "pnlCampaignSettings";
            this.pnlCampaignSettings.Size = new System.Drawing.Size(720, 424);
            this.pnlCampaignSettings.TabIndex = 1;
            this.pnlCampaignSettings.Tag = "CampaignSettings";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblCampaignName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonApply, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(716, 420);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Options);
            this.tabControl1.Controls.Add(this.Messages);
            this.tabControl1.Controls.Add(this.Recipients);
            this.tabControl1.Controls.Add(this.Advanced);
            this.tabControl1.Controls.Add(this.Servers);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(3, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 345);
            this.tabControl1.TabIndex = 7;
            // 
            // Options
            // 
            this.Options.Controls.Add(this.txtFromsCharsets);
            this.Options.Controls.Add(this.chkMIMEFrom);
            this.Options.Controls.Add(this.txtSubjectsCharsets);
            this.Options.Controls.Add(this.chkMIMESubject);
            this.Options.Controls.Add(this.groupBox4);
            this.Options.Controls.Add(this.groupBox3);
            this.Options.Controls.Add(this.groupBox1);
            this.Options.ImageIndex = 1;
            this.Options.Location = new System.Drawing.Point(4, 39);
            this.Options.Name = "Options";
            this.Options.Padding = new System.Windows.Forms.Padding(3);
            this.Options.Size = new System.Drawing.Size(702, 302);
            this.Options.TabIndex = 0;
            this.Options.Text = "Options";
            this.Options.UseVisualStyleBackColor = true;
            // 
            // txtFromsCharsets
            // 
            this.txtFromsCharsets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromsCharsets.Location = new System.Drawing.Point(312, 280);
            this.txtFromsCharsets.Name = "txtFromsCharsets";
            this.txtFromsCharsets.Size = new System.Drawing.Size(225, 20);
            this.txtFromsCharsets.TabIndex = 6;
            this.txtFromsCharsets.Text = "ISO-8859-1,US-ASCII,windows-1252";
            // 
            // chkMIMEFrom
            // 
            this.chkMIMEFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMIMEFrom.Location = new System.Drawing.Point(8, 280);
            this.chkMIMEFrom.Name = "chkMIMEFrom";
            this.chkMIMEFrom.Size = new System.Drawing.Size(290, 17);
            this.chkMIMEFrom.TabIndex = 9;
            this.chkMIMEFrom.Text = "MIME-encoded froms, charsets (comma seperated):";
            this.chkMIMEFrom.UseVisualStyleBackColor = true;
            // 
            // txtSubjectsCharsets
            // 
            this.txtSubjectsCharsets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubjectsCharsets.Location = new System.Drawing.Point(312, 256);
            this.txtSubjectsCharsets.Name = "txtSubjectsCharsets";
            this.txtSubjectsCharsets.Size = new System.Drawing.Size(225, 20);
            this.txtSubjectsCharsets.TabIndex = 7;
            this.txtSubjectsCharsets.Text = "ISO-8859-1,US-ASCII,windows-1252";
            // 
            // chkMIMESubject
            // 
            this.chkMIMESubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMIMESubject.Location = new System.Drawing.Point(8, 256);
            this.chkMIMESubject.Name = "chkMIMESubject";
            this.chkMIMESubject.Size = new System.Drawing.Size(290, 17);
            this.chkMIMESubject.TabIndex = 8;
            this.chkMIMESubject.Text = "MIME-encoded subjects, charsets (comma seperated):";
            this.chkMIMESubject.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtDomains);
            this.groupBox4.Location = new System.Drawing.Point(272, 128);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(220, 115);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Domains";
            // 
            // txtDomains
            // 
            this.txtDomains.Location = new System.Drawing.Point(6, 19);
            this.txtDomains.Multiline = true;
            this.txtDomains.Name = "txtDomains";
            this.txtDomains.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDomains.Size = new System.Drawing.Size(208, 89);
            this.txtDomains.TabIndex = 2;
            this.txtDomains.Tag = "CampaignOptions";
            this.txtDomains.WordWrap = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSeedList);
            this.groupBox3.Controls.Add(this.txtSeedInterval);
            this.groupBox3.Controls.Add(this.chkSeed);
            this.groupBox3.Location = new System.Drawing.Point(272, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 115);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Seeds";
            // 
            // txtSeedList
            // 
            this.txtSeedList.Location = new System.Drawing.Point(6, 42);
            this.txtSeedList.Multiline = true;
            this.txtSeedList.Name = "txtSeedList";
            this.txtSeedList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSeedList.Size = new System.Drawing.Size(208, 67);
            this.txtSeedList.TabIndex = 2;
            this.txtSeedList.Tag = "CampaignOptions";
            this.txtSeedList.WordWrap = false;
            // 
            // txtSeedInterval
            // 
            this.txtSeedInterval.Location = new System.Drawing.Point(123, 16);
            this.txtSeedInterval.Name = "txtSeedInterval";
            this.txtSeedInterval.Size = new System.Drawing.Size(66, 20);
            this.txtSeedInterval.TabIndex = 1;
            this.txtSeedInterval.Tag = "CampaignOptions";
            this.txtSeedInterval.Text = "10000";
            // 
            // chkSeed
            // 
            this.chkSeed.AutoSize = true;
            this.chkSeed.Location = new System.Drawing.Point(6, 19);
            this.chkSeed.Name = "chkSeed";
            this.chkSeed.Size = new System.Drawing.Size(106, 17);
            this.chkSeed.TabIndex = 0;
            this.chkSeed.Text = "Send seed every";
            this.chkSeed.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkSelectSupression);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.lnkSchedulerStatus);
            this.groupBox1.Controls.Add(this.txtThreadsPerConnection);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtConnectionsPerServer);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtSessionPerConnectionMax);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEmailPerSessionMax);
            this.groupBox1.Controls.Add(this.cboAddRecipAction);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.chkSupression);
            this.groupBox1.Controls.Add(this.txtSupressionFileName);
            this.groupBox1.Controls.Add(this.txtSessionPerConnectionMin);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtEmailPerSessionMin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 250);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mailing Options";
            // 
            // lnkSelectSupression
            // 
            this.lnkSelectSupression.AutoSize = true;
            this.lnkSelectSupression.Location = new System.Drawing.Point(216, 224);
            this.lnkSelectSupression.Name = "lnkSelectSupression";
            this.lnkSelectSupression.Size = new System.Drawing.Size(37, 13);
            this.lnkSelectSupression.TabIndex = 22;
            this.lnkSelectSupression.TabStop = true;
            this.lnkSelectSupression.Text = "Select";
            this.lnkSelectSupression.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectSupression_LinkClicked);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 168);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(58, 13);
            this.label20.TabIndex = 21;
            this.label20.Text = "Scheduler:";
            // 
            // lnkSchedulerStatus
            // 
            this.lnkSchedulerStatus.AutoSize = true;
            this.lnkSchedulerStatus.Location = new System.Drawing.Point(80, 168);
            this.lnkSchedulerStatus.Name = "lnkSchedulerStatus";
            this.lnkSchedulerStatus.Size = new System.Drawing.Size(21, 13);
            this.lnkSchedulerStatus.TabIndex = 11;
            this.lnkSchedulerStatus.TabStop = true;
            this.lnkSchedulerStatus.Text = "Off";
            this.lnkSchedulerStatus.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSchedulerStatus_LinkClicked);
            // 
            // txtThreadsPerConnection
            // 
            this.txtThreadsPerConnection.Location = new System.Drawing.Point(134, 99);
            this.txtThreadsPerConnection.Name = "txtThreadsPerConnection";
            this.txtThreadsPerConnection.Size = new System.Drawing.Size(33, 20);
            this.txtThreadsPerConnection.TabIndex = 20;
            this.txtThreadsPerConnection.Tag = "CampaignOptions";
            this.txtThreadsPerConnection.Text = "2";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 106);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Threads per connection:";
            // 
            // txtConnectionsPerServer
            // 
            this.txtConnectionsPerServer.Location = new System.Drawing.Point(134, 75);
            this.txtConnectionsPerServer.Name = "txtConnectionsPerServer";
            this.txtConnectionsPerServer.Size = new System.Drawing.Size(33, 20);
            this.txtConnectionsPerServer.TabIndex = 18;
            this.txtConnectionsPerServer.Tag = "CampaignOptions";
            this.txtConnectionsPerServer.Text = "5";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Connections per server:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(173, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "to";
            // 
            // txtSessionPerConnectionMax
            // 
            this.txtSessionPerConnectionMax.Location = new System.Drawing.Point(195, 53);
            this.txtSessionPerConnectionMax.Name = "txtSessionPerConnectionMax";
            this.txtSessionPerConnectionMax.Size = new System.Drawing.Size(33, 20);
            this.txtSessionPerConnectionMax.TabIndex = 15;
            this.txtSessionPerConnectionMax.Tag = "CampaignOptions";
            this.txtSessionPerConnectionMax.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "to";
            // 
            // txtEmailPerSessionMax
            // 
            this.txtEmailPerSessionMax.Location = new System.Drawing.Point(195, 24);
            this.txtEmailPerSessionMax.Name = "txtEmailPerSessionMax";
            this.txtEmailPerSessionMax.Size = new System.Drawing.Size(33, 20);
            this.txtEmailPerSessionMax.TabIndex = 13;
            this.txtEmailPerSessionMax.Tag = "CampaignOptions";
            this.txtEmailPerSessionMax.Text = "5";
            // 
            // cboAddRecipAction
            // 
            this.cboAddRecipAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAddRecipAction.FormattingEnabled = true;
            this.cboAddRecipAction.Items.AddRange(new object[] {
            "BCC",
            "CC",
            "TO",
            "<Random>"});
            this.cboAddRecipAction.Location = new System.Drawing.Point(136, 128);
            this.cboAddRecipAction.Name = "cboAddRecipAction";
            this.cboAddRecipAction.Size = new System.Drawing.Size(106, 21);
            this.cboAddRecipAction.TabIndex = 12;
            this.cboAddRecipAction.Tag = "CampaignOptions";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Add session emails to:";
            // 
            // chkSupression
            // 
            this.chkSupression.AutoSize = true;
            this.chkSupression.Location = new System.Drawing.Point(8, 200);
            this.chkSupression.Name = "chkSupression";
            this.chkSupression.Size = new System.Drawing.Size(153, 17);
            this.chkSupression.TabIndex = 10;
            this.chkSupression.Text = "Don\'t send to those emails:";
            this.chkSupression.UseVisualStyleBackColor = true;
            this.chkSupression.CheckedChanged += new System.EventHandler(this.chkSupression_CheckedChanged);
            // 
            // txtSupressionFileName
            // 
            this.txtSupressionFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSupressionFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtSupressionFileName.Location = new System.Drawing.Point(8, 224);
            this.txtSupressionFileName.Name = "txtSupressionFileName";
            this.txtSupressionFileName.Size = new System.Drawing.Size(200, 20);
            this.txtSupressionFileName.TabIndex = 8;
            this.txtSupressionFileName.Tag = "CampaignOptions";
            // 
            // txtSessionPerConnectionMin
            // 
            this.txtSessionPerConnectionMin.Location = new System.Drawing.Point(134, 49);
            this.txtSessionPerConnectionMin.Name = "txtSessionPerConnectionMin";
            this.txtSessionPerConnectionMin.Size = new System.Drawing.Size(33, 20);
            this.txtSessionPerConnectionMin.TabIndex = 3;
            this.txtSessionPerConnectionMin.Tag = "CampaignOptions";
            this.txtSessionPerConnectionMin.Text = "2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Sessions per connection:";
            // 
            // txtEmailPerSessionMin
            // 
            this.txtEmailPerSessionMin.Location = new System.Drawing.Point(134, 23);
            this.txtEmailPerSessionMin.Name = "txtEmailPerSessionMin";
            this.txtEmailPerSessionMin.Size = new System.Drawing.Size(33, 20);
            this.txtEmailPerSessionMin.TabIndex = 1;
            this.txtEmailPerSessionMin.Tag = "CampaignOptions";
            this.txtEmailPerSessionMin.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Emails per session:";
            // 
            // Messages
            // 
            this.Messages.Controls.Add(this.tabControl2);
            this.Messages.Controls.Add(this.tableLayoutPanel4);
            this.Messages.ImageIndex = 4;
            this.Messages.Location = new System.Drawing.Point(4, 39);
            this.Messages.Name = "Messages";
            this.Messages.Padding = new System.Windows.Forms.Padding(3);
            this.Messages.Size = new System.Drawing.Size(702, 302);
            this.Messages.TabIndex = 1;
            this.Messages.Text = "Messages";
            this.Messages.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 48);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(696, 251);
            this.tabControl2.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMessageText);
            this.tabPage1.Controls.Add(this.tableLayoutPanel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(688, 225);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtMessageText
            // 
            this.txtMessageText.AcceptsReturn = true;
            this.txtMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageText.Location = new System.Drawing.Point(3, 32);
            this.txtMessageText.Multiline = true;
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageText.Size = new System.Drawing.Size(682, 190);
            this.txtMessageText.TabIndex = 1;
            this.txtMessageText.Tag = "CampaignMessages";
            this.txtMessageText.WordWrap = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(682, 29);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton10});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(682, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Insert special tag";
            this.toolStripButton4.ToolTipText = "Insert special tag";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Injector.Properties.Resources.page_white_put;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Insert INCLUDE tag";
            this.toolStripButton5.ToolTipText = "Insert INCLUDE tag";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "Custom macros";
            this.toolStripButton6.ToolTipText = "Custom macros";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::Injector.Properties.Resources.asterisk_orange;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "Insert RND tag (random)";
            this.toolStripButton7.ToolTipText = "Insert RND tag (random)";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::Injector.Properties.Resources.arrow_rotate_clockwise;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "Insert ROT tag";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = global::Injector.Properties.Resources.world_add;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "Insert DownloadData Tag";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtMessageHTML);
            this.tabPage2.Controls.Add(this.tableLayoutPanel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(688, 225);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HTML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtMessageHTML
            // 
            this.txtMessageHTML.AcceptsReturn = true;
            this.txtMessageHTML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageHTML.Location = new System.Drawing.Point(3, 32);
            this.txtMessageHTML.Multiline = true;
            this.txtMessageHTML.Name = "txtMessageHTML";
            this.txtMessageHTML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageHTML.Size = new System.Drawing.Size(682, 190);
            this.txtMessageHTML.TabIndex = 2;
            this.txtMessageHTML.Tag = "CampaignMessages";
            this.txtMessageHTML.WordWrap = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.toolStrip3, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(682, 29);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton15,
            this.toolStripButton16,
            this.toolStripButton17,
            this.toolStripButton18,
            this.toolStripButton19});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(682, 25);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "Insert special tag";
            this.toolStripButton12.ToolTipText = "Insert special tag";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = global::Injector.Properties.Resources.page_white_put;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton13.Text = "Insert INCLUDE tag";
            this.toolStripButton13.ToolTipText = "Insert INCLUDE tag";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton14.Text = "Custom macros";
            this.toolStripButton14.ToolTipText = "Custom macros";
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton15.Image = global::Injector.Properties.Resources.asterisk_orange;
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton15.Text = "Insert RND tag (random)";
            this.toolStripButton15.ToolTipText = "Insert RND tag (random)";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton16.Image = global::Injector.Properties.Resources.arrow_rotate_clockwise;
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton16.Text = "Insert ROT tag";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = global::Injector.Properties.Resources.world_add;
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton17.Text = "Insert DownloadData Tag";
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = global::Injector.Properties.Resources.vector;
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton18.Text = "Insert TextToImage Tag";
            this.toolStripButton18.ToolTipText = "Insert TextToImage Tag";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click);
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton19.Image = global::Injector.Properties.Resources.picture;
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton19.Text = "Insert embedded picture";
            this.toolStripButton19.Click += new System.EventHandler(this.toolStripButton19_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtSubjects);
            this.tabPage3.Controls.Add(this.tableLayoutPanel7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(688, 225);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Subjects";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtSubjects
            // 
            this.txtSubjects.AcceptsReturn = true;
            this.txtSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubjects.Location = new System.Drawing.Point(3, 32);
            this.txtSubjects.Multiline = true;
            this.txtSubjects.Name = "txtSubjects";
            this.txtSubjects.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubjects.Size = new System.Drawing.Size(682, 190);
            this.txtSubjects.TabIndex = 4;
            this.txtSubjects.Tag = "CampaignMessages";
            this.txtSubjects.WordWrap = false;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.toolStrip4, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(682, 29);
            this.tableLayoutPanel7.TabIndex = 3;
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton20,
            this.toolStripButton21,
            this.toolStripButton22,
            this.toolStripButton23,
            this.toolStripButton24,
            this.toolStripButton25});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(682, 25);
            this.toolStrip4.TabIndex = 5;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripButton20
            // 
            this.toolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton20.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton20.Name = "toolStripButton20";
            this.toolStripButton20.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton20.Text = "Insert special tag";
            this.toolStripButton20.ToolTipText = "Insert special tag";
            this.toolStripButton20.Click += new System.EventHandler(this.toolStripButton20_Click);
            // 
            // toolStripButton21
            // 
            this.toolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton21.Image = global::Injector.Properties.Resources.page_white_put;
            this.toolStripButton21.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton21.Name = "toolStripButton21";
            this.toolStripButton21.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton21.Text = "Insert INCLUDE tag";
            this.toolStripButton21.ToolTipText = "Insert INCLUDE tag";
            this.toolStripButton21.Click += new System.EventHandler(this.toolStripButton21_Click);
            // 
            // toolStripButton22
            // 
            this.toolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton22.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton22.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton22.Name = "toolStripButton22";
            this.toolStripButton22.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton22.Text = "Custom macros";
            this.toolStripButton22.ToolTipText = "Custom macros";
            // 
            // toolStripButton23
            // 
            this.toolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton23.Image = global::Injector.Properties.Resources.asterisk_orange;
            this.toolStripButton23.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton23.Name = "toolStripButton23";
            this.toolStripButton23.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton23.Text = "Insert RND tag (random)";
            this.toolStripButton23.ToolTipText = "Insert RND tag (random)";
            this.toolStripButton23.Click += new System.EventHandler(this.toolStripButton23_Click);
            // 
            // toolStripButton24
            // 
            this.toolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton24.Image = global::Injector.Properties.Resources.arrow_rotate_clockwise;
            this.toolStripButton24.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton24.Name = "toolStripButton24";
            this.toolStripButton24.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton24.Text = "Insert ROT tag";
            this.toolStripButton24.Click += new System.EventHandler(this.toolStripButton24_Click);
            // 
            // toolStripButton25
            // 
            this.toolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton25.Image = global::Injector.Properties.Resources.world_add;
            this.toolStripButton25.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton25.Name = "toolStripButton25";
            this.toolStripButton25.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton25.Text = "Insert DownloadData Tag";
            this.toolStripButton25.Click += new System.EventHandler(this.toolStripButton25_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.toolStrip7);
            this.tabPage4.Controls.Add(this.toolStrip6);
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
            this.tabPage4.Size = new System.Drawing.Size(688, 225);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FROMs and Attachments";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // toolStrip7
            // 
            this.toolStrip7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip7.AutoSize = false;
            this.toolStrip7.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip7.CanOverflow = false;
            this.toolStrip7.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip7.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton41});
            this.toolStrip7.Location = new System.Drawing.Point(655, 129);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.Size = new System.Drawing.Size(24, 25);
            this.toolStrip7.TabIndex = 8;
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
            this.toolStripButton41.Click += new System.EventHandler(this.toolStripButton41_Click);
            // 
            // toolStrip6
            // 
            this.toolStrip6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip6.AutoSize = false;
            this.toolStrip6.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip6.CanOverflow = false;
            this.toolStrip6.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip6.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton26,
            this.toolStripButton29});
            this.toolStrip6.Location = new System.Drawing.Point(631, 8);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(49, 25);
            this.toolStrip6.TabIndex = 7;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // toolStripButton26
            // 
            this.toolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton26.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton26.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton26.Name = "toolStripButton26";
            this.toolStripButton26.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton26.Text = "Insert special tag";
            this.toolStripButton26.ToolTipText = "Insert special tag";
            this.toolStripButton26.Click += new System.EventHandler(this.toolStripButton26_Click);
            // 
            // toolStripButton29
            // 
            this.toolStripButton29.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton29.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton29.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton29.Name = "toolStripButton29";
            this.toolStripButton29.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton29.Text = "Custom macros";
            this.toolStripButton29.ToolTipText = "Custom macros";
            // 
            // toolStrip5
            // 
            this.toolStrip5.AutoSize = false;
            this.toolStrip5.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip5.CanOverflow = false;
            this.toolStrip5.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton11,
            this.toolStripButton28});
            this.toolStrip5.Location = new System.Drawing.Point(248, 8);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(49, 25);
            this.toolStrip5.TabIndex = 6;
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
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
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
            this.txtAttachments.Location = new System.Drawing.Point(8, 161);
            this.txtAttachments.Multiline = true;
            this.txtAttachments.Name = "txtAttachments";
            this.txtAttachments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAttachments.Size = new System.Drawing.Size(671, 53);
            this.txtAttachments.TabIndex = 4;
            this.txtAttachments.Tag = "CampaignMessages";
            this.txtAttachments.WordWrap = false;
            this.txtAttachments.TextChanged += new System.EventHandler(this.txtAttachments_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(6, 139);
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
            this.txtFromEmailPrefix.Size = new System.Drawing.Size(288, 80);
            this.txtFromEmailPrefix.TabIndex = 2;
            this.txtFromEmailPrefix.Tag = "CampaignMessages";
            this.txtFromEmailPrefix.WordWrap = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(391, 16);
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
            this.txtFromAliases.Location = new System.Drawing.Point(391, 40);
            this.txtFromAliases.Multiline = true;
            this.txtFromAliases.Name = "txtFromAliases";
            this.txtFromAliases.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFromAliases.Size = new System.Drawing.Size(288, 80);
            this.txtFromAliases.TabIndex = 3;
            this.txtFromAliases.Tag = "CampaignMessages";
            this.txtFromAliases.WordWrap = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnRefreshSource);
            this.tabPage5.Controls.Add(this.chkMessageSourceEdit);
            this.tabPage5.Controls.Add(this.txtMessageSource);
            this.tabPage5.Controls.Add(this.tableLayoutPanel9);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(688, 225);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Message Source";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnRefreshSource
            // 
            this.btnRefreshSource.Enabled = false;
            this.btnRefreshSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshSource.Location = new System.Drawing.Point(152, 38);
            this.btnRefreshSource.Name = "btnRefreshSource";
            this.btnRefreshSource.Size = new System.Drawing.Size(56, 23);
            this.btnRefreshSource.TabIndex = 6;
            this.btnRefreshSource.Text = "Refresh";
            this.btnRefreshSource.UseVisualStyleBackColor = true;
            this.btnRefreshSource.Click += new System.EventHandler(this.btnRefreshSource_Click);
            // 
            // chkMessageSourceEdit
            // 
            this.chkMessageSourceEdit.AutoSize = true;
            this.chkMessageSourceEdit.Location = new System.Drawing.Point(8, 40);
            this.chkMessageSourceEdit.Name = "chkMessageSourceEdit";
            this.chkMessageSourceEdit.Size = new System.Drawing.Size(144, 17);
            this.chkMessageSourceEdit.TabIndex = 5;
            this.chkMessageSourceEdit.Text = "Custom Message Source";
            this.chkMessageSourceEdit.UseVisualStyleBackColor = true;
            this.chkMessageSourceEdit.CheckedChanged += new System.EventHandler(this.chkMessageSourceEdit_CheckedChanged);
            // 
            // txtMessageSource
            // 
            this.txtMessageSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageSource.Enabled = false;
            this.txtMessageSource.Location = new System.Drawing.Point(8, 64);
            this.txtMessageSource.Multiline = true;
            this.txtMessageSource.Name = "txtMessageSource";
            this.txtMessageSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageSource.Size = new System.Drawing.Size(671, 225);
            this.txtMessageSource.TabIndex = 4;
            this.txtMessageSource.Tag = "CampaignMessages";
            this.txtMessageSource.WordWrap = false;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.toolStrip8, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(682, 29);
            this.tableLayoutPanel9.TabIndex = 3;
            // 
            // toolStrip8
            // 
            this.toolStrip8.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip8.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton30,
            this.toolStripButton31,
            this.toolStripButton32,
            this.toolStripButton33,
            this.toolStripButton34,
            this.toolStripButton35,
            this.toolStripButton36,
            this.toolStripButton37});
            this.toolStrip8.Location = new System.Drawing.Point(0, 0);
            this.toolStrip8.Name = "toolStrip8";
            this.toolStrip8.Size = new System.Drawing.Size(682, 25);
            this.toolStrip8.TabIndex = 5;
            this.toolStrip8.Text = "toolStrip8";
            // 
            // toolStripButton30
            // 
            this.toolStripButton30.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton30.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton30.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton30.Name = "toolStripButton30";
            this.toolStripButton30.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton30.Text = "Insert special tag";
            this.toolStripButton30.ToolTipText = "Insert special tag";
            this.toolStripButton30.Click += new System.EventHandler(this.toolStripButton30_Click);
            // 
            // toolStripButton31
            // 
            this.toolStripButton31.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton31.Image = global::Injector.Properties.Resources.page_white_put;
            this.toolStripButton31.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton31.Name = "toolStripButton31";
            this.toolStripButton31.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton31.Text = "Insert INCLUDE tag";
            this.toolStripButton31.ToolTipText = "Insert INCLUDE tag";
            this.toolStripButton31.Click += new System.EventHandler(this.toolStripButton31_Click);
            // 
            // toolStripButton32
            // 
            this.toolStripButton32.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton32.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton32.Name = "toolStripButton32";
            this.toolStripButton32.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton32.Text = "Custom macros";
            this.toolStripButton32.ToolTipText = "Custom macros";
            // 
            // toolStripButton33
            // 
            this.toolStripButton33.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton33.Image = global::Injector.Properties.Resources.asterisk_orange;
            this.toolStripButton33.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton33.Name = "toolStripButton33";
            this.toolStripButton33.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton33.Text = "Insert RND tag (random)";
            this.toolStripButton33.ToolTipText = "Insert RND tag (random)";
            this.toolStripButton33.Click += new System.EventHandler(this.toolStripButton33_Click);
            // 
            // toolStripButton34
            // 
            this.toolStripButton34.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton34.Image = global::Injector.Properties.Resources.arrow_rotate_clockwise;
            this.toolStripButton34.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton34.Name = "toolStripButton34";
            this.toolStripButton34.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton34.Text = "Insert ROT tag";
            this.toolStripButton34.Click += new System.EventHandler(this.toolStripButton34_Click);
            // 
            // toolStripButton35
            // 
            this.toolStripButton35.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton35.Image = global::Injector.Properties.Resources.world_add;
            this.toolStripButton35.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton35.Name = "toolStripButton35";
            this.toolStripButton35.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton35.Text = "Insert DownloadData Tag";
            this.toolStripButton35.Click += new System.EventHandler(this.toolStripButton35_Click);
            // 
            // toolStripButton36
            // 
            this.toolStripButton36.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton36.Enabled = false;
            this.toolStripButton36.Image = global::Injector.Properties.Resources.vector;
            this.toolStripButton36.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton36.Name = "toolStripButton36";
            this.toolStripButton36.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton36.Text = "Insert TextToImage Tag";
            this.toolStripButton36.ToolTipText = "Insert TextToImage Tag";
            this.toolStripButton36.Click += new System.EventHandler(this.toolStripButton36_Click);
            // 
            // toolStripButton37
            // 
            this.toolStripButton37.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton37.Image = global::Injector.Properties.Resources.picture;
            this.toolStripButton37.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton37.Name = "toolStripButton37";
            this.toolStripButton37.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton37.Text = "Insert embedded picture";
            this.toolStripButton37.Click += new System.EventHandler(this.toolStripButton37_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnPreviewOutlook);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(688, 225);
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
            this.btnPreviewOutlook.Click += new System.EventHandler(this.btnPreviewOutlook_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.cboMessages, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblMessageID, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(696, 45);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // cboMessages
            // 
            this.cboMessages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMessages.FormattingEnabled = true;
            this.cboMessages.Location = new System.Drawing.Point(3, 3);
            this.cboMessages.Name = "cboMessages";
            this.cboMessages.Size = new System.Drawing.Size(166, 21);
            this.cboMessages.TabIndex = 3;
            this.cboMessages.SelectedIndexChanged += new System.EventHandler(this.cboMessages_SelectedIndexChanged);
            // 
            // lblMessageID
            // 
            this.lblMessageID.AutoSize = true;
            this.lblMessageID.Location = new System.Drawing.Point(175, 0);
            this.lblMessageID.Name = "lblMessageID";
            this.lblMessageID.Size = new System.Drawing.Size(21, 13);
            this.lblMessageID.TabIndex = 4;
            this.lblMessageID.Text = "ID:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnNewMessage, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteMessage, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(531, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(162, 31);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // btnNewMessage
            // 
            this.btnNewMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewMessage.Image = global::Injector.Properties.Resources.email_add;
            this.btnNewMessage.Location = new System.Drawing.Point(3, 3);
            this.btnNewMessage.Name = "btnNewMessage";
            this.btnNewMessage.Size = new System.Drawing.Size(75, 25);
            this.btnNewMessage.TabIndex = 3;
            this.btnNewMessage.Text = "New";
            this.btnNewMessage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewMessage.UseVisualStyleBackColor = true;
            this.btnNewMessage.Click += new System.EventHandler(this.btnNewMessage_Click);
            // 
            // btnDeleteMessage
            // 
            this.btnDeleteMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteMessage.Image = global::Injector.Properties.Resources.cross;
            this.btnDeleteMessage.Location = new System.Drawing.Point(84, 3);
            this.btnDeleteMessage.Name = "btnDeleteMessage";
            this.btnDeleteMessage.Size = new System.Drawing.Size(75, 25);
            this.btnDeleteMessage.TabIndex = 4;
            this.btnDeleteMessage.Text = "Delete";
            this.btnDeleteMessage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteMessage.UseVisualStyleBackColor = true;
            this.btnDeleteMessage.Click += new System.EventHandler(this.btnDeleteMessage_Click);
            // 
            // Recipients
            // 
            this.Recipients.Controls.Add(this.btnRcptSaveChanges);
            this.Recipients.Controls.Add(this.btnModifyRecipientFile);
            this.Recipients.Controls.Add(this.groupBox5);
            this.Recipients.Controls.Add(this.btnRcptRemoveAll);
            this.Recipients.Controls.Add(this.btnRcptDeleteFiles);
            this.Recipients.Controls.Add(this.btnRcptAddFiles);
            this.Recipients.Controls.Add(this.lvRecipients);
            this.Recipients.ImageIndex = 5;
            this.Recipients.Location = new System.Drawing.Point(4, 39);
            this.Recipients.Name = "Recipients";
            this.Recipients.Padding = new System.Windows.Forms.Padding(3);
            this.Recipients.Size = new System.Drawing.Size(702, 302);
            this.Recipients.TabIndex = 2;
            this.Recipients.Text = "Recipients";
            this.Recipients.UseVisualStyleBackColor = true;
            // 
            // btnRcptSaveChanges
            // 
            this.btnRcptSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRcptSaveChanges.Location = new System.Drawing.Point(579, 120);
            this.btnRcptSaveChanges.Name = "btnRcptSaveChanges";
            this.btnRcptSaveChanges.Size = new System.Drawing.Size(114, 23);
            this.btnRcptSaveChanges.TabIndex = 6;
            this.btnRcptSaveChanges.Text = "Save Changes";
            this.btnRcptSaveChanges.UseVisualStyleBackColor = true;
            this.btnRcptSaveChanges.Click += new System.EventHandler(this.btnRcptSaveChanges_Click);
            // 
            // btnModifyRecipientFile
            // 
            this.btnModifyRecipientFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyRecipientFile.Location = new System.Drawing.Point(579, 96);
            this.btnModifyRecipientFile.Name = "btnModifyRecipientFile";
            this.btnModifyRecipientFile.Size = new System.Drawing.Size(114, 23);
            this.btnModifyRecipientFile.TabIndex = 5;
            this.btnModifyRecipientFile.Text = "Modify Selected";
            this.btnModifyRecipientFile.UseVisualStyleBackColor = true;
            this.btnModifyRecipientFile.Click += new System.EventHandler(this.btnModifyRecipientFile_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboTrimSpaces);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtEscape);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtQuote);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.txtDelimiter);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.cboHasHeaders);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.txtRecipientColumn);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(3, 200);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(696, 99);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Read Options:";
            // 
            // cboTrimSpaces
            // 
            this.cboTrimSpaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrimSpaces.Enabled = false;
            this.cboTrimSpaces.FormattingEnabled = true;
            this.cboTrimSpaces.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboTrimSpaces.Location = new System.Drawing.Point(331, 71);
            this.cboTrimSpaces.Name = "cboTrimSpaces";
            this.cboTrimSpaces.Size = new System.Drawing.Size(100, 21);
            this.cboTrimSpaces.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(232, 75);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Trim Spaces:";
            // 
            // txtEscape
            // 
            this.txtEscape.Enabled = false;
            this.txtEscape.Location = new System.Drawing.Point(331, 45);
            this.txtEscape.Name = "txtEscape";
            this.txtEscape.Size = new System.Drawing.Size(100, 20);
            this.txtEscape.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(232, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "Escape:";
            // 
            // txtQuote
            // 
            this.txtQuote.Enabled = false;
            this.txtQuote.Location = new System.Drawing.Point(331, 19);
            this.txtQuote.Name = "txtQuote";
            this.txtQuote.Size = new System.Drawing.Size(100, 20);
            this.txtQuote.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(232, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Quote:";
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Enabled = false;
            this.txtDelimiter.Location = new System.Drawing.Point(106, 72);
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size(100, 20);
            this.txtDelimiter.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Delimiter:";
            // 
            // cboHasHeaders
            // 
            this.cboHasHeaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHasHeaders.Enabled = false;
            this.cboHasHeaders.FormattingEnabled = true;
            this.cboHasHeaders.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboHasHeaders.Location = new System.Drawing.Point(105, 45);
            this.cboHasHeaders.Name = "cboHasHeaders";
            this.cboHasHeaders.Size = new System.Drawing.Size(100, 21);
            this.cboHasHeaders.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Headers:";
            // 
            // txtRecipientColumn
            // 
            this.txtRecipientColumn.Enabled = false;
            this.txtRecipientColumn.Location = new System.Drawing.Point(105, 19);
            this.txtRecipientColumn.Name = "txtRecipientColumn";
            this.txtRecipientColumn.Size = new System.Drawing.Size(100, 20);
            this.txtRecipientColumn.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Recipient Column:";
            // 
            // btnRcptRemoveAll
            // 
            this.btnRcptRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRcptRemoveAll.Location = new System.Drawing.Point(579, 56);
            this.btnRcptRemoveAll.Name = "btnRcptRemoveAll";
            this.btnRcptRemoveAll.Size = new System.Drawing.Size(114, 23);
            this.btnRcptRemoveAll.TabIndex = 3;
            this.btnRcptRemoveAll.Text = "Remove all files";
            this.btnRcptRemoveAll.UseVisualStyleBackColor = true;
            this.btnRcptRemoveAll.Click += new System.EventHandler(this.btnRcptRemoveAll_Click);
            // 
            // btnRcptDeleteFiles
            // 
            this.btnRcptDeleteFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRcptDeleteFiles.Location = new System.Drawing.Point(579, 32);
            this.btnRcptDeleteFiles.Name = "btnRcptDeleteFiles";
            this.btnRcptDeleteFiles.Size = new System.Drawing.Size(114, 23);
            this.btnRcptDeleteFiles.TabIndex = 2;
            this.btnRcptDeleteFiles.Text = "Delete from list";
            this.btnRcptDeleteFiles.UseVisualStyleBackColor = true;
            this.btnRcptDeleteFiles.Click += new System.EventHandler(this.btnRcptDeleteFiles_Click);
            // 
            // btnRcptAddFiles
            // 
            this.btnRcptAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRcptAddFiles.Location = new System.Drawing.Point(579, 8);
            this.btnRcptAddFiles.Name = "btnRcptAddFiles";
            this.btnRcptAddFiles.Size = new System.Drawing.Size(114, 23);
            this.btnRcptAddFiles.TabIndex = 1;
            this.btnRcptAddFiles.Text = "Add Files";
            this.btnRcptAddFiles.UseVisualStyleBackColor = true;
            this.btnRcptAddFiles.Click += new System.EventHandler(this.btnRcptAddFiles_Click);
            // 
            // lvRecipients
            // 
            this.lvRecipients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20});
            this.lvRecipients.FullRowSelect = true;
            this.lvRecipients.HideSelection = false;
            this.lvRecipients.Location = new System.Drawing.Point(6, 6);
            this.lvRecipients.Name = "lvRecipients";
            this.lvRecipients.Size = new System.Drawing.Size(570, 186);
            this.lvRecipients.TabIndex = 0;
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
            // Advanced
            // 
            this.Advanced.Controls.Add(this.label19);
            this.Advanced.Controls.Add(this.groupBox6);
            this.Advanced.Controls.Add(this.txtPoolName);
            this.Advanced.Controls.Add(this.groupBox2);
            this.Advanced.ImageIndex = 6;
            this.Advanced.Location = new System.Drawing.Point(4, 39);
            this.Advanced.Name = "Advanced";
            this.Advanced.Padding = new System.Windows.Forms.Padding(3);
            this.Advanced.Size = new System.Drawing.Size(702, 302);
            this.Advanced.TabIndex = 3;
            this.Advanced.Text = "Advanced";
            this.Advanced.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(417, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Pool name:";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Location = new System.Drawing.Point(8, 593);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(194, 61);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Virtual MTA";
            // 
            // txtPoolName
            // 
            this.txtPoolName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoolName.Location = new System.Drawing.Point(487, 8);
            this.txtPoolName.Multiline = true;
            this.txtPoolName.Name = "txtPoolName";
            this.txtPoolName.Size = new System.Drawing.Size(208, 24);
            this.txtPoolName.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.cboRotateKey);
            this.groupBox2.Controls.Add(this.txtRotateInterval);
            this.groupBox2.Controls.Add(this.lvRotate);
            this.groupBox2.Controls.Add(this.chkRotateMessages);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 266);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message Rotation";
            // 
            // cboRotateKey
            // 
            this.cboRotateKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRotateKey.FormattingEnabled = true;
            this.cboRotateKey.Items.AddRange(new object[] {
            "minutes",
            "emails processed"});
            this.cboRotateKey.Location = new System.Drawing.Point(264, 24);
            this.cboRotateKey.Name = "cboRotateKey";
            this.cboRotateKey.Size = new System.Drawing.Size(120, 21);
            this.cboRotateKey.TabIndex = 4;
            // 
            // txtRotateInterval
            // 
            this.txtRotateInterval.Location = new System.Drawing.Point(152, 24);
            this.txtRotateInterval.Name = "txtRotateInterval";
            this.txtRotateInterval.Size = new System.Drawing.Size(100, 20);
            this.txtRotateInterval.TabIndex = 3;
            this.txtRotateInterval.Text = "200";
            // 
            // lvRotate
            // 
            this.lvRotate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRotate.CheckBoxes = true;
            this.lvRotate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader25});
            this.lvRotate.FullRowSelect = true;
            this.lvRotate.HideSelection = false;
            this.lvRotate.Location = new System.Drawing.Point(6, 51);
            this.lvRotate.Name = "lvRotate";
            this.lvRotate.Size = new System.Drawing.Size(378, 205);
            this.lvRotate.TabIndex = 1;
            this.lvRotate.UseCompatibleStateImageBehavior = false;
            this.lvRotate.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Message ID:";
            this.columnHeader10.Width = 88;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "Name:";
            this.columnHeader25.Width = 130;
            // 
            // chkRotateMessages
            // 
            this.chkRotateMessages.AutoSize = true;
            this.chkRotateMessages.Location = new System.Drawing.Point(8, 24);
            this.chkRotateMessages.Name = "chkRotateMessages";
            this.chkRotateMessages.Size = new System.Drawing.Size(140, 17);
            this.chkRotateMessages.TabIndex = 0;
            this.chkRotateMessages.Text = "Switch messages every:";
            this.chkRotateMessages.UseVisualStyleBackColor = true;
            // 
            // Servers
            // 
            this.Servers.Controls.Add(this.btnInheritServers);
            this.Servers.Controls.Add(this.btnServersRemoveAll);
            this.Servers.Controls.Add(this.btnServersDeleteSelected);
            this.Servers.Controls.Add(this.btnServersAdd);
            this.Servers.Controls.Add(this.btnServersAddFiles);
            this.Servers.Controls.Add(this.lvServers);
            this.Servers.ImageIndex = 11;
            this.Servers.Location = new System.Drawing.Point(4, 39);
            this.Servers.Name = "Servers";
            this.Servers.Padding = new System.Windows.Forms.Padding(3);
            this.Servers.Size = new System.Drawing.Size(702, 302);
            this.Servers.TabIndex = 4;
            this.Servers.Text = "Servers";
            this.Servers.UseVisualStyleBackColor = true;
            // 
            // btnInheritServers
            // 
            this.btnInheritServers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInheritServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInheritServers.Location = new System.Drawing.Point(571, 108);
            this.btnInheritServers.Name = "btnInheritServers";
            this.btnInheritServers.Size = new System.Drawing.Size(114, 23);
            this.btnInheritServers.TabIndex = 6;
            this.btnInheritServers.Text = "Inherit Master";
            this.btnInheritServers.UseVisualStyleBackColor = true;
            this.btnInheritServers.Click += new System.EventHandler(this.btnInheritServers_Click);
            // 
            // btnServersRemoveAll
            // 
            this.btnServersRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServersRemoveAll.Location = new System.Drawing.Point(571, 80);
            this.btnServersRemoveAll.Name = "btnServersRemoveAll";
            this.btnServersRemoveAll.Size = new System.Drawing.Size(114, 23);
            this.btnServersRemoveAll.TabIndex = 5;
            this.btnServersRemoveAll.Text = "Remove all servers";
            this.btnServersRemoveAll.UseVisualStyleBackColor = true;
            this.btnServersRemoveAll.Click += new System.EventHandler(this.btnServersRemoveAll_Click);
            // 
            // btnServersDeleteSelected
            // 
            this.btnServersDeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServersDeleteSelected.Location = new System.Drawing.Point(571, 56);
            this.btnServersDeleteSelected.Name = "btnServersDeleteSelected";
            this.btnServersDeleteSelected.Size = new System.Drawing.Size(114, 23);
            this.btnServersDeleteSelected.TabIndex = 4;
            this.btnServersDeleteSelected.Text = "Delete from list";
            this.btnServersDeleteSelected.UseVisualStyleBackColor = true;
            this.btnServersDeleteSelected.Click += new System.EventHandler(this.btnServersDeleteSelected_Click);
            // 
            // btnServersAdd
            // 
            this.btnServersAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServersAdd.Location = new System.Drawing.Point(571, 32);
            this.btnServersAdd.Name = "btnServersAdd";
            this.btnServersAdd.Size = new System.Drawing.Size(114, 23);
            this.btnServersAdd.TabIndex = 3;
            this.btnServersAdd.Text = "Add Server";
            this.btnServersAdd.UseVisualStyleBackColor = true;
            this.btnServersAdd.Click += new System.EventHandler(this.btnServersAdd_Click);
            // 
            // btnServersAddFiles
            // 
            this.btnServersAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServersAddFiles.Location = new System.Drawing.Point(571, 8);
            this.btnServersAddFiles.Name = "btnServersAddFiles";
            this.btnServersAddFiles.Size = new System.Drawing.Size(114, 23);
            this.btnServersAddFiles.TabIndex = 2;
            this.btnServersAddFiles.Text = "Add Files";
            this.btnServersAddFiles.UseVisualStyleBackColor = true;
            this.btnServersAddFiles.Click += new System.EventHandler(this.btnServersAddFiles_Click);
            // 
            // lvServers
            // 
            this.lvServers.AllowColumnReorder = true;
            this.lvServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvServers.CheckBoxes = true;
            this.lvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.lvServers.FullRowSelect = true;
            this.lvServers.HideSelection = false;
            this.lvServers.Location = new System.Drawing.Point(6, 6);
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(557, 282);
            this.lvServers.TabIndex = 0;
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Host";
            this.columnHeader13.Width = 94;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Port";
            this.columnHeader14.Width = 90;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Username";
            this.columnHeader15.Width = 99;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Password";
            this.columnHeader16.Width = 97;
            // 
            // lblCampaignName
            // 
            this.lblCampaignName.AutoSize = true;
            this.lblCampaignName.BackColor = System.Drawing.Color.SlateGray;
            this.lblCampaignName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCampaignName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCampaignName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCampaignName.ForeColor = System.Drawing.Color.White;
            this.lblCampaignName.Location = new System.Drawing.Point(3, 0);
            this.lblCampaignName.Name = "lblCampaignName";
            this.lblCampaignName.Size = new System.Drawing.Size(710, 28);
            this.lblCampaignName.TabIndex = 16;
            this.lblCampaignName.Text = "CampaignName";
            this.lblCampaignName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.AutoSize = true;
            this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApply.Image = global::Injector.Properties.Resources.accept;
            this.buttonApply.Location = new System.Drawing.Point(623, 393);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(90, 24);
            this.buttonApply.TabIndex = 13;
            this.buttonApply.Text = "Apply";
            this.buttonApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlAnalysis
            // 
            this.pnlAnalysis.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAnalysis.Controls.Add(this.tabControl3);
            this.pnlAnalysis.Controls.Add(this.label21);
            this.pnlAnalysis.Location = new System.Drawing.Point(8, 600);
            this.pnlAnalysis.Name = "pnlAnalysis";
            this.pnlAnalysis.Size = new System.Drawing.Size(88, 32);
            this.pnlAnalysis.TabIndex = 6;
            this.pnlAnalysis.Tag = "Analysis";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage7);
            this.tabControl3.Controls.Add(this.tabPage11);
            this.tabControl3.Controls.Add(this.tabPage8);
            this.tabControl3.Controls.Add(this.tabPage9);
            this.tabControl3.Controls.Add(this.tabPage10);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.ImageList = this.imglstAnalysis;
            this.tabControl3.Location = new System.Drawing.Point(0, 29);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(84, 0);
            this.tabControl3.TabIndex = 3;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.panel1);
            this.tabPage7.Controls.Add(this.label25);
            this.tabPage7.Controls.Add(this.label24);
            this.tabPage7.Controls.Add(this.pbDeliverability);
            this.tabPage7.Controls.Add(this.pbProcessedTtl);
            this.tabPage7.Controls.Add(this.pbProcessedSegment);
            this.tabPage7.Controls.Add(this.label23);
            this.tabPage7.Controls.Add(this.label22);
            this.tabPage7.ImageKey = "web-0.png";
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(76, 0);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "General";
            this.tabPage7.UseVisualStyleBackColor = true;
            this.tabPage7.Click += new System.EventHandler(this.tabPage7_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(8, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(64, 0);
            this.panel1.TabIndex = 7;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(16, 40);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(49, 13);
            this.label25.TabIndex = 6;
            this.label25.Text = "100.0 %";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "100.0 %";
            // 
            // pbDeliverability
            // 
            this.pbDeliverability.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDeliverability.Location = new System.Drawing.Point(88, 32);
            this.pbDeliverability.Maximum = 10000;
            this.pbDeliverability.Name = "pbDeliverability";
            this.pbDeliverability.Size = new System.Drawing.Size(0, 23);
            this.pbDeliverability.Step = 1;
            this.pbDeliverability.TabIndex = 4;
            // 
            // pbProcessedTtl
            // 
            this.pbProcessedTtl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProcessedTtl.Location = new System.Drawing.Point(88, 8);
            this.pbProcessedTtl.Maximum = 10000;
            this.pbProcessedTtl.Name = "pbProcessedTtl";
            this.pbProcessedTtl.Size = new System.Drawing.Size(0, 8);
            this.pbProcessedTtl.Step = 1;
            this.pbProcessedTtl.TabIndex = 3;
            // 
            // pbProcessedSegment
            // 
            this.pbProcessedSegment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProcessedSegment.Location = new System.Drawing.Point(88, 16);
            this.pbProcessedSegment.Maximum = 10000;
            this.pbProcessedSegment.Name = "pbProcessedSegment";
            this.pbProcessedSegment.Size = new System.Drawing.Size(0, 14);
            this.pbProcessedSegment.Step = 1;
            this.pbProcessedSegment.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(72, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "Deliverability:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "Processed:";
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.listView1);
            this.tabPage11.ImageKey = "web-34.png";
            this.tabPage11.Location = new System.Drawing.Point(4, 29);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(76, 0);
            this.tabPage11.TabIndex = 4;
            this.tabPage11.Text = "MTAs";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader26,
            this.columnHeader27,
            this.columnHeader28,
            this.columnHeader29,
            this.columnHeader30});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(76, 0);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "#";
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "IP";
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "Server";
            // 
            // columnHeader29
            // 
            this.columnHeader29.Text = "%";
            // 
            // tabPage8
            // 
            this.tabPage8.ImageKey = "web-44.png";
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(76, 0);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "Recipient Files";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.ImageKey = "web-39.png";
            this.tabPage9.Location = new System.Drawing.Point(4, 29);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(76, 0);
            this.tabPage9.TabIndex = 2;
            this.tabPage9.Text = "Speed";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabPage10
            // 
            this.tabPage10.ImageKey = "web-20.png";
            this.tabPage10.Location = new System.Drawing.Point(4, 29);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(76, 0);
            this.tabPage10.TabIndex = 3;
            this.tabPage10.Text = "Logger";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // imglstAnalysis
            // 
            this.imglstAnalysis.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstAnalysis.ImageStream")));
            this.imglstAnalysis.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstAnalysis.Images.SetKeyName(0, "web-0.png");
            this.imglstAnalysis.Images.SetKeyName(1, "web-1.png");
            this.imglstAnalysis.Images.SetKeyName(2, "web-2.png");
            this.imglstAnalysis.Images.SetKeyName(3, "web-3.png");
            this.imglstAnalysis.Images.SetKeyName(4, "web-4.png");
            this.imglstAnalysis.Images.SetKeyName(5, "web-5.png");
            this.imglstAnalysis.Images.SetKeyName(6, "web-6.png");
            this.imglstAnalysis.Images.SetKeyName(7, "web-7.png");
            this.imglstAnalysis.Images.SetKeyName(8, "web-8.png");
            this.imglstAnalysis.Images.SetKeyName(9, "web-9.png");
            this.imglstAnalysis.Images.SetKeyName(10, "web-10.png");
            this.imglstAnalysis.Images.SetKeyName(11, "web-11.png");
            this.imglstAnalysis.Images.SetKeyName(12, "web-12.png");
            this.imglstAnalysis.Images.SetKeyName(13, "web-13.png");
            this.imglstAnalysis.Images.SetKeyName(14, "web-14.png");
            this.imglstAnalysis.Images.SetKeyName(15, "web-15.png");
            this.imglstAnalysis.Images.SetKeyName(16, "web-16.png");
            this.imglstAnalysis.Images.SetKeyName(17, "web-17.png");
            this.imglstAnalysis.Images.SetKeyName(18, "web-18.png");
            this.imglstAnalysis.Images.SetKeyName(19, "web-19.png");
            this.imglstAnalysis.Images.SetKeyName(20, "web-20.png");
            this.imglstAnalysis.Images.SetKeyName(21, "web-21.png");
            this.imglstAnalysis.Images.SetKeyName(22, "web-22.png");
            this.imglstAnalysis.Images.SetKeyName(23, "web-23.png");
            this.imglstAnalysis.Images.SetKeyName(24, "web-24.png");
            this.imglstAnalysis.Images.SetKeyName(25, "web-25.png");
            this.imglstAnalysis.Images.SetKeyName(26, "web-26.png");
            this.imglstAnalysis.Images.SetKeyName(27, "web-27.png");
            this.imglstAnalysis.Images.SetKeyName(28, "web-28.png");
            this.imglstAnalysis.Images.SetKeyName(29, "web-29.png");
            this.imglstAnalysis.Images.SetKeyName(30, "web-30.png");
            this.imglstAnalysis.Images.SetKeyName(31, "web-31.png");
            this.imglstAnalysis.Images.SetKeyName(32, "web-32.png");
            this.imglstAnalysis.Images.SetKeyName(33, "web-33.png");
            this.imglstAnalysis.Images.SetKeyName(34, "web-34.png");
            this.imglstAnalysis.Images.SetKeyName(35, "web-35.png");
            this.imglstAnalysis.Images.SetKeyName(36, "web-36.png");
            this.imglstAnalysis.Images.SetKeyName(37, "web-37.png");
            this.imglstAnalysis.Images.SetKeyName(38, "web-38.png");
            this.imglstAnalysis.Images.SetKeyName(39, "web-39.png");
            this.imglstAnalysis.Images.SetKeyName(40, "web-40.png");
            this.imglstAnalysis.Images.SetKeyName(41, "web-41.png");
            this.imglstAnalysis.Images.SetKeyName(42, "web-42.png");
            this.imglstAnalysis.Images.SetKeyName(43, "web-43.png");
            this.imglstAnalysis.Images.SetKeyName(44, "web-44.png");
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Orange;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 29);
            this.label21.TabIndex = 2;
            this.label21.Text = "Analysis";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlServers
            // 
            this.pnlServers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlServers.Controls.Add(this.lvMasterServers);
            this.pnlServers.Controls.Add(this.btnMasterServersRemoveAll);
            this.pnlServers.Controls.Add(this.btnMasterServersDelete);
            this.pnlServers.Controls.Add(this.btnMasterAddServer);
            this.pnlServers.Controls.Add(this.btnMasterServersAddFiles);
            this.pnlServers.Controls.Add(this.label6);
            this.pnlServers.Location = new System.Drawing.Point(336, 480);
            this.pnlServers.Name = "pnlServers";
            this.pnlServers.Size = new System.Drawing.Size(712, 144);
            this.pnlServers.TabIndex = 5;
            this.pnlServers.Tag = "Servers";
            // 
            // lvMasterServers
            // 
            this.lvMasterServers.AllowColumnReorder = true;
            this.lvMasterServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMasterServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24});
            this.lvMasterServers.FullRowSelect = true;
            this.lvMasterServers.HideSelection = false;
            this.lvMasterServers.Location = new System.Drawing.Point(8, 32);
            this.lvMasterServers.Name = "lvMasterServers";
            this.lvMasterServers.Size = new System.Drawing.Size(568, 104);
            this.lvMasterServers.TabIndex = 11;
            this.lvMasterServers.UseCompatibleStateImageBehavior = false;
            this.lvMasterServers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Host";
            this.columnHeader21.Width = 94;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Port";
            this.columnHeader22.Width = 90;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "Username";
            this.columnHeader23.Width = 99;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "Password";
            this.columnHeader24.Width = 97;
            // 
            // btnMasterServersRemoveAll
            // 
            this.btnMasterServersRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMasterServersRemoveAll.Location = new System.Drawing.Point(584, 104);
            this.btnMasterServersRemoveAll.Name = "btnMasterServersRemoveAll";
            this.btnMasterServersRemoveAll.Size = new System.Drawing.Size(114, 23);
            this.btnMasterServersRemoveAll.TabIndex = 10;
            this.btnMasterServersRemoveAll.Text = "Remove all servers";
            this.btnMasterServersRemoveAll.UseVisualStyleBackColor = true;
            this.btnMasterServersRemoveAll.Click += new System.EventHandler(this.btnMasterServersRemoveAll_Click);
            // 
            // btnMasterServersDelete
            // 
            this.btnMasterServersDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMasterServersDelete.Location = new System.Drawing.Point(584, 80);
            this.btnMasterServersDelete.Name = "btnMasterServersDelete";
            this.btnMasterServersDelete.Size = new System.Drawing.Size(114, 23);
            this.btnMasterServersDelete.TabIndex = 9;
            this.btnMasterServersDelete.Text = "Delete from list";
            this.btnMasterServersDelete.UseVisualStyleBackColor = true;
            this.btnMasterServersDelete.Click += new System.EventHandler(this.btnMasterServersDelete_Click);
            // 
            // btnMasterAddServer
            // 
            this.btnMasterAddServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMasterAddServer.Location = new System.Drawing.Point(584, 56);
            this.btnMasterAddServer.Name = "btnMasterAddServer";
            this.btnMasterAddServer.Size = new System.Drawing.Size(114, 23);
            this.btnMasterAddServer.TabIndex = 8;
            this.btnMasterAddServer.Text = "Add Server";
            this.btnMasterAddServer.UseVisualStyleBackColor = true;
            this.btnMasterAddServer.Click += new System.EventHandler(this.btnMasterAddServer_Click);
            // 
            // btnMasterServersAddFiles
            // 
            this.btnMasterServersAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMasterServersAddFiles.Location = new System.Drawing.Point(584, 32);
            this.btnMasterServersAddFiles.Name = "btnMasterServersAddFiles";
            this.btnMasterServersAddFiles.Size = new System.Drawing.Size(114, 23);
            this.btnMasterServersAddFiles.TabIndex = 7;
            this.btnMasterServersAddFiles.Text = "Add Files";
            this.btnMasterServersAddFiles.UseVisualStyleBackColor = true;
            this.btnMasterServersAddFiles.Click += new System.EventHandler(this.btnMasterServersAddFiles_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.SteelBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(708, 29);
            this.label6.TabIndex = 2;
            this.label6.Text = "Servers";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCampaigns
            // 
            this.pnlCampaigns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCampaigns.Controls.Add(this.splitContainer2);
            this.pnlCampaigns.Controls.Add(this.label1);
            this.pnlCampaigns.Location = new System.Drawing.Point(16, 456);
            this.pnlCampaigns.Name = "pnlCampaigns";
            this.pnlCampaigns.Size = new System.Drawing.Size(256, 96);
            this.pnlCampaigns.TabIndex = 0;
            this.pnlCampaigns.Tag = "Campaigns";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 29);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvCampaigns);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(252, 63);
            this.splitContainer2.SplitterDistance = 28;
            this.splitContainer2.TabIndex = 4;
            // 
            // lvCampaigns
            // 
            this.lvCampaigns.AllowColumnReorder = true;
            this.lvCampaigns.ContextMenuStrip = this.contextMenuStrip3;
            this.lvCampaigns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCampaigns.FullRowSelect = true;
            this.lvCampaigns.HideSelection = false;
            this.lvCampaigns.Location = new System.Drawing.Point(0, 0);
            this.lvCampaigns.Name = "lvCampaigns";
            this.lvCampaigns.Size = new System.Drawing.Size(252, 28);
            this.lvCampaigns.TabIndex = 3;
            this.lvCampaigns.UseCompatibleStateImageBehavior = false;
            this.lvCampaigns.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCampaignStart,
            this.mnuCampaignPause,
            this.mnuCampaignStop,
            this.toolStripMenuItem10,
            this.mnuRefreshClickThrough,
            this.mnuRefreshOpens,
            this.mnuRefreshUnsubs});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(229, 142);
            // 
            // mnuCampaignStart
            // 
            this.mnuCampaignStart.Name = "mnuCampaignStart";
            this.mnuCampaignStart.Size = new System.Drawing.Size(228, 22);
            this.mnuCampaignStart.Text = "Start";
            this.mnuCampaignStart.Click += new System.EventHandler(this.mnuCampaignStart_Click);
            // 
            // mnuCampaignPause
            // 
            this.mnuCampaignPause.Name = "mnuCampaignPause";
            this.mnuCampaignPause.Size = new System.Drawing.Size(228, 22);
            this.mnuCampaignPause.Text = "Pause";
            this.mnuCampaignPause.Click += new System.EventHandler(this.mnuCampaignPause_Click);
            // 
            // mnuCampaignStop
            // 
            this.mnuCampaignStop.Name = "mnuCampaignStop";
            this.mnuCampaignStop.Size = new System.Drawing.Size(228, 22);
            this.mnuCampaignStop.Text = "Stop";
            this.mnuCampaignStop.Click += new System.EventHandler(this.mnuCampaignStop_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(225, 6);
            // 
            // mnuRefreshClickThrough
            // 
            this.mnuRefreshClickThrough.Name = "mnuRefreshClickThrough";
            this.mnuRefreshClickThrough.Size = new System.Drawing.Size(228, 22);
            this.mnuRefreshClickThrough.Text = "Refresh Total Click-Throughs";
            this.mnuRefreshClickThrough.Click += new System.EventHandler(this.mnuRefreshClickThrough_Click);
            // 
            // mnuRefreshOpens
            // 
            this.mnuRefreshOpens.Name = "mnuRefreshOpens";
            this.mnuRefreshOpens.Size = new System.Drawing.Size(228, 22);
            this.mnuRefreshOpens.Text = "Refresh Total Opens";
            this.mnuRefreshOpens.Click += new System.EventHandler(this.mnuRefreshOpens_Click);
            // 
            // mnuRefreshUnsubs
            // 
            this.mnuRefreshUnsubs.Name = "mnuRefreshUnsubs";
            this.mnuRefreshUnsubs.Size = new System.Drawing.Size(228, 22);
            this.mnuRefreshUnsubs.Text = "Refresh Total Unsubscribes";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.tabsPolls);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 31);
            this.panel2.TabIndex = 0;
            // 
            // tabsPolls
            // 
            this.tabsPolls.Controls.Add(this.tabPage12);
            this.tabsPolls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsPolls.Location = new System.Drawing.Point(0, 0);
            this.tabsPolls.Name = "tabsPolls";
            this.tabsPolls.SelectedIndex = 0;
            this.tabsPolls.Size = new System.Drawing.Size(252, 31);
            this.tabsPolls.TabIndex = 0;
            // 
            // tabPage12
            // 
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(244, 5);
            this.tabPage12.TabIndex = 0;
            this.tabPage12.Text = "tabPage12";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SlateGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Campaigns";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appearanceToolStripMenuItem,
            this.alignmentToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(143, 48);
            // 
            // appearanceToolStripMenuItem
            // 
            this.appearanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem1,
            this.buttonsToolStripMenuItem1,
            this.flatButtonsToolStripMenuItem1});
            this.appearanceToolStripMenuItem.Name = "appearanceToolStripMenuItem";
            this.appearanceToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.appearanceToolStripMenuItem.Text = "Appearance";
            // 
            // normalToolStripMenuItem1
            // 
            this.normalToolStripMenuItem1.Checked = true;
            this.normalToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalToolStripMenuItem1.Name = "normalToolStripMenuItem1";
            this.normalToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.normalToolStripMenuItem1.Text = "Normal";
            this.normalToolStripMenuItem1.Click += new System.EventHandler(this.normalToolStripMenuItem1_Click);
            // 
            // buttonsToolStripMenuItem1
            // 
            this.buttonsToolStripMenuItem1.Name = "buttonsToolStripMenuItem1";
            this.buttonsToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.buttonsToolStripMenuItem1.Text = "Buttons";
            this.buttonsToolStripMenuItem1.Click += new System.EventHandler(this.buttonsToolStripMenuItem1_Click);
            // 
            // flatButtonsToolStripMenuItem1
            // 
            this.flatButtonsToolStripMenuItem1.Name = "flatButtonsToolStripMenuItem1";
            this.flatButtonsToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.flatButtonsToolStripMenuItem1.Text = "Flat Buttons";
            this.flatButtonsToolStripMenuItem1.Click += new System.EventHandler(this.flatButtonsToolStripMenuItem1_Click);
            // 
            // alignmentToolStripMenuItem
            // 
            this.alignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topToolStripMenuItem,
            this.bottomToolStripMenuItem});
            this.alignmentToolStripMenuItem.Name = "alignmentToolStripMenuItem";
            this.alignmentToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.alignmentToolStripMenuItem.Text = "Alignment";
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Checked = true;
            this.topToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.topToolStripMenuItem.Text = "Top";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.topToolStripMenuItem_Click);
            // 
            // bottomToolStripMenuItem
            // 
            this.bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            this.bottomToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.bottomToolStripMenuItem.Text = "Bottom";
            this.bottomToolStripMenuItem.Click += new System.EventHandler(this.bottomToolStripMenuItem_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "world_add.png");
            this.imageList2.Images.SetKeyName(1, "textfield_add.png");
            this.imageList2.Images.SetKeyName(2, "table_add.png");
            this.imageList2.Images.SetKeyName(3, "tab_add.png");
            this.imageList2.Images.SetKeyName(4, "plugin_add.png");
            this.imageList2.Images.SetKeyName(5, "page_white_add.png");
            this.imageList2.Images.SetKeyName(6, "page_add.png");
            this.imageList2.Images.SetKeyName(7, "layout_add.png");
            this.imageList2.Images.SetKeyName(8, "email_add.png");
            this.imageList2.Images.SetKeyName(9, "cross.png");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            // 
            // toolStripButton27
            // 
            this.toolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton27.Image = global::Injector.Properties.Resources.script;
            this.toolStripButton27.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton27.Name = "toolStripButton27";
            this.toolStripButton27.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton27.Text = "Insert special tag";
            this.toolStripButton27.ToolTipText = "Insert special tag";
            // 
            // toolStripButton42
            // 
            this.toolStripButton42.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton42.Image = global::Injector.Properties.Resources.script_gear;
            this.toolStripButton42.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton42.Name = "toolStripButton42";
            this.toolStripButton42.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton42.Text = "Custom macros";
            this.toolStripButton42.ToolTipText = "Custom macros";
            // 
            // cntxmnuCampaignRoot
            // 
            this.cntxmnuCampaignRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreateDatabaseEntry,
            this.toolStripMenuItem7,
            this.mnuUpdateRedirectUrl,
            this.toolStripMenuItem9,
            this.mnuDeleteDatabaseEntry,
            this.toolStripMenuItem8,
            this.mnuViewHits,
            this.mnuViewOpens,
            this.toolStripMenuItem4,
            this.mnuViewAllEntries});
            this.cntxmnuCampaignRoot.Name = "contextMenuStrip4";
            this.cntxmnuCampaignRoot.Size = new System.Drawing.Size(194, 160);
            // 
            // mnuCreateDatabaseEntry
            // 
            this.mnuCreateDatabaseEntry.Name = "mnuCreateDatabaseEntry";
            this.mnuCreateDatabaseEntry.Size = new System.Drawing.Size(193, 22);
            this.mnuCreateDatabaseEntry.Text = "Create database entry";
            this.mnuCreateDatabaseEntry.Click += new System.EventHandler(this.mnuCreateDatabaseEntry_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuUpdateRedirectUrl
            // 
            this.mnuUpdateRedirectUrl.Name = "mnuUpdateRedirectUrl";
            this.mnuUpdateRedirectUrl.Size = new System.Drawing.Size(193, 22);
            this.mnuUpdateRedirectUrl.Text = "Update redirect url";
            this.mnuUpdateRedirectUrl.Click += new System.EventHandler(this.mnuUpdateRedirectUrl_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuDeleteDatabaseEntry
            // 
            this.mnuDeleteDatabaseEntry.Name = "mnuDeleteDatabaseEntry";
            this.mnuDeleteDatabaseEntry.Size = new System.Drawing.Size(193, 22);
            this.mnuDeleteDatabaseEntry.Text = "Delete database entry";
            this.mnuDeleteDatabaseEntry.Click += new System.EventHandler(this.mnuDeleteDatabaseEntry_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuViewHits
            // 
            this.mnuViewHits.Name = "mnuViewHits";
            this.mnuViewHits.Size = new System.Drawing.Size(193, 22);
            this.mnuViewHits.Text = "View hits";
            this.mnuViewHits.Click += new System.EventHandler(this.mnuViewHits_Click);
            // 
            // mnuViewOpens
            // 
            this.mnuViewOpens.Name = "mnuViewOpens";
            this.mnuViewOpens.Size = new System.Drawing.Size(193, 22);
            this.mnuViewOpens.Text = "View opens";
            this.mnuViewOpens.Click += new System.EventHandler(this.mnuViewOpens_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(190, 6);
            // 
            // mnuViewAllEntries
            // 
            this.mnuViewAllEntries.Name = "mnuViewAllEntries";
            this.mnuViewAllEntries.Size = new System.Drawing.Size(193, 22);
            this.mnuViewAllEntries.Text = "View all entries";
            this.mnuViewAllEntries.Click += new System.EventHandler(this.mnuViewAllEntries_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 665);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Konvict";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlCampaignSettings.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Messages.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.toolStrip8.ResumeLayout(false);
            this.toolStrip8.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.Recipients.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.Advanced.ResumeLayout(false);
            this.Advanced.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Servers.ResumeLayout(false);
            this.pnlAnalysis.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.pnlServers.ResumeLayout(false);
            this.pnlCampaigns.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabsPolls.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.cntxmnuCampaignRoot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toTrayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel pnlCampaigns;
        private System.Windows.Forms.Panel pnlCampaignSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlServers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newCampaignToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCampaignsToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.TreeView tvwNavi;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ListView lvCampaigns;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem appearanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem buttonsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem flatButtonsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem alignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Options;
        private System.Windows.Forms.TabPage Messages;
        private System.Windows.Forms.TabPage Recipients;
        private System.Windows.Forms.TabPage Advanced;
        private System.Windows.Forms.Label lblCampaignName;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox cboMessages;
        private System.Windows.Forms.Label lblMessageID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnNewMessage;
        private System.Windows.Forms.Button btnDeleteMessage;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox txtMessageText;
        private System.Windows.Forms.TextBox txtMessageHTML;
        private System.Windows.Forms.TextBox txtSubjects;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txtFromAliases;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFromEmailPrefix;
        private System.Windows.Forms.TextBox txtAttachments;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMessageSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.ListView lvRecipients;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button btnRcptRemoveAll;
        private System.Windows.Forms.Button btnRcptDeleteFiles;
        private System.Windows.Forms.Button btnRcptAddFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSessionPerConnectionMin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmailPerSessionMin;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSeedList;
        private System.Windows.Forms.TextBox txtSeedInterval;
        private System.Windows.Forms.CheckBox chkSeed;
        private System.Windows.Forms.CheckBox chkSupression;
        private System.Windows.Forms.TextBox txtSupressionFileName;
        private System.Windows.Forms.CheckBox chkMIMEFrom;
        private System.Windows.Forms.CheckBox chkMIMESubject;
        private System.Windows.Forms.TextBox txtSubjectsCharsets;
        private System.Windows.Forms.TextBox txtFromsCharsets;
        private System.Windows.Forms.ComboBox cboAddRecipAction;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkRotateMessages;
        private System.Windows.Forms.ListView lvRotate;
        private System.Windows.Forms.ComboBox cboRotateKey;
        private System.Windows.Forms.TextBox txtRotateInterval;
        private System.Windows.Forms.TextBox txtEmailPerSessionMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSessionPerConnectionMax;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.TextBox txtConnectionsPerServer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtThreadsPerConnection;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDomains;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage Servers;
        private System.Windows.Forms.ToolStripMenuItem soundsToolStripMenuItem;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Button btnServersAddFiles;
        private System.Windows.Forms.Button btnServersRemoveAll;
        private System.Windows.Forms.Button btnServersDeleteSelected;
        private System.Windows.Forms.Button btnServersAdd;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cboHasHeaders;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRecipientColumn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtQuote;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboTrimSpaces;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtEscape;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnRcptSaveChanges;
        private System.Windows.Forms.Button btnModifyRecipientFile;
        private System.Windows.Forms.Button btnMasterServersRemoveAll;
        private System.Windows.Forms.Button btnMasterServersDelete;
        private System.Windows.Forms.Button btnMasterAddServer;
        private System.Windows.Forms.Button btnMasterServersAddFiles;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton28;
        private System.Windows.Forms.ToolStripButton toolStripButton27;
        private System.Windows.Forms.ToolStripButton toolStripButton42;
        private System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.ToolStripButton toolStripButton26;
        private System.Windows.Forms.ToolStripButton toolStripButton29;
        private System.Windows.Forms.ToolStrip toolStrip7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem mnuCampaignStart;
        private System.Windows.Forms.ToolStripMenuItem mnuCampaignPause;
        private System.Windows.Forms.ToolStripMenuItem mnuCampaignStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem lockToTrayToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtPoolName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ToolStripButton toolStripButton41;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripButton toolStripButton19;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton toolStripButton20;
        private System.Windows.Forms.ToolStripButton toolStripButton21;
        private System.Windows.Forms.ToolStripButton toolStripButton22;
        private System.Windows.Forms.ToolStripButton toolStripButton23;
        private System.Windows.Forms.ToolStripButton toolStripButton24;
        private System.Windows.Forms.ToolStripButton toolStripButton25;
        private System.Windows.Forms.ToolStrip toolStrip8;
        private System.Windows.Forms.ToolStripButton toolStripButton30;
        private System.Windows.Forms.ToolStripButton toolStripButton31;
        private System.Windows.Forms.ToolStripButton toolStripButton32;
        private System.Windows.Forms.ToolStripButton toolStripButton33;
        private System.Windows.Forms.ToolStripButton toolStripButton34;
        private System.Windows.Forms.ToolStripButton toolStripButton35;
        private System.Windows.Forms.ToolStripButton toolStripButton36;
        private System.Windows.Forms.ToolStripButton toolStripButton37;
        private System.Windows.Forms.Button btnRefreshSource;
        private System.Windows.Forms.CheckBox chkMessageSourceEdit;
        private System.Windows.Forms.Button btnPreviewOutlook;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem configureKLocalToolStripMenuItem;
        private System.Windows.Forms.ListView lvMasterServers;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.Button btnInheritServers;
        private System.Windows.Forms.LinkLabel lnkSchedulerStatus;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem extentionsToolStripMenuItem;
        private System.Windows.Forms.LinkLabel lnkSelectSupression;
        private System.Windows.Forms.Panel pnlAnalysis;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.ImageList imglstAnalysis;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ProgressBar pbProcessedSegment;
        private System.Windows.Forms.ProgressBar pbDeliverability;
        private System.Windows.Forms.ProgressBar pbProcessedTtl;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader28;
        private System.Windows.Forms.ColumnHeader columnHeader29;
        private System.Windows.Forms.ColumnHeader columnHeader30;
        private System.Windows.Forms.ToolStripMenuItem startPollingToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cntxmnuCampaignRoot;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteDatabaseEntry;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateDatabaseEntry;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem mnuViewHits;
        private System.Windows.Forms.ToolStripMenuItem mnuViewOpens;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuViewAllEntries;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateRedirectUrl;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshClickThrough;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshOpens;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshUnsubs;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabsPolls;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem convertMessageToolStripMenuItem;

    }
}

