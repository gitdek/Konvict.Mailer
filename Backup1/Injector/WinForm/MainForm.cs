using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;

using Injector.Core.PathManager;
using Injector.Core.Settings;
using Injector.Core.Logging;
using Injector.Core.Threading;
using Injector.Utilities;
using System.Data;

namespace Injector
{
    public partial class MainForm : Form
    {

        #region Fields

        private string _currentCampaignId = string.Empty;
        private string _currentMessageId = string.Empty;

        private string _hashTrayPwd = string.Empty;
        private bool _isActive = true;

        private System.Windows.Forms.Timer _statisticsTimer;

        private TreeNode _oldSelectNode = null;

        #endregion

        #region Default constructor

        public MainForm()
        {
            InitializeComponent();

#if (!DEBUG)
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif

            ILogger logger = ServiceScope.Get<ILogger>(false);
            IPathManager pathManager = ServiceScope.Get<IPathManager>();
            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();

            logger.Debug("Creating Campaigns Listview");
            lvCampaignCreate();

            lvRecipients.Sorting = SortOrder.None;

            lvServers.ItemCheck += new ItemCheckEventHandler(lvServers_ItemCheck);


            logger.Debug("MainForm: Loading Polls");
            try
            {
                if (File.Exists(pathManager.GetPath(@"<CONFIG>\Polls.dat")))
                {
                    PollCollection pollColl = null;
                    byte[] rawBytes = File.ReadAllBytes(pathManager.GetPath(@"<CONFIG>\Polls.dat"));
                    pollColl = (PollCollection)SerializeEx.Serializer.DeSerialize(rawBytes, SerializeEx.SerializedType.CompressedObject);
                    List<PollItem> allPolls = pollColl.Polls;

                    foreach (PollItem poll in allPolls)
                        mgr.ReplacePoll(poll.ID, poll);
                }
            }
            catch
            {
            }

            logger.Debug("MainForm: Loading Master Servers");
            try
            {
                if (File.Exists(pathManager.GetPath(@"<CONFIG>\Servers.dat")))
                {
                    ServerCollection serverColl = null;
                    byte[] rawBytes = File.ReadAllBytes(pathManager.GetPath(@"<CONFIG>\Servers.dat"));
                    serverColl = (ServerCollection)SerializeEx.Serializer.DeSerialize(rawBytes, SerializeEx.SerializedType.CompressedObject);
                    List<Server> allServers = serverColl.Servers;

                    foreach (Server server in allServers)
                    {
                        mgr.ReplaceServer(server.ID, server);

                        ListViewItem lvItem = new ListViewItem(server.Host);
                        lvItem.SubItems.Add(server.Port.ToString());
                        lvItem.SubItems.Add(server.Username);
                        lvItem.SubItems.Add(server.Password);
                        lvItem.Tag = server.ID.ToString();
                        lvMasterServers.Items.Add(lvItem);

                    }
                }
            }
            catch
            {
            }


            // Initialize default campaign instance
            Campaign initializer = new Campaign();
            initializer.Dispose();
            initializer = null;


            logger.Debug("MainForm: Loading Campaigns");
            LoadCampaigns();

            // Launch statistics timer
            _statisticsTimer = new Timer();
            _statisticsTimer.Interval = 2000;
            _statisticsTimer.Tick += new EventHandler(OnCampaignStatsUpdate);
            OnCampaignStatsUpdate(null, null);
            _statisticsTimer.Start();



            //string connString = @"connectionstring=server = sv2.zd6.net;database = campaign_manager;user id=root;password=;";
            //CampaignMngtDb mngt = new CampaignMngtDb(connString);

            //System.Data.DataView dv = mngt.GetFolders();

            //DatagridDialog dgDialog = new DatagridDialog("Redirects", dv);
            //dgDialog.Show();
        }

        void lvServers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Campaign campaign = selectedCampaign();
            for (int i = 0; i < campaign.Mailhosts.Count; i++)
            {
                if (string.Equals(campaign.Mailhosts[i].ID.ToString(), lvServers.Items[e.Index].Tag.ToString()))
                    campaign.Mailhosts[i].Enabled = e.NewValue == CheckState.Checked ? true : false;
            }

        }

        #endregion

        #region method tabsPollsCreate

        private void tabsPollsCreate()
        {
            ILogger logger = ServiceScope.Get<ILogger>();
            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();

            List<PollItem> polls = mgr.GetPolls();
            List<Server> servers = mgr.GetServers();

            logger.Debug("MainForm: Creating tabPolls");

            tabsPolls.TabPages.Clear();

            if (polls.Count == 0)
            {
                return;
            }

            if (servers.Count == 0)
            {
                return;
            }

            tabsPolls.ShowToolTips = true;
            //tabsPolls.Font = new Font("Tahoma", 8, FontStyle.Regular);
            //tabsPolls.Appearance = TabAppearance.Normal;

            foreach (PollItem poll in polls)
            {                
                TabPage pollTab = new TabPage(poll.Command);
                pollTab.Tag = poll.ID;

                string pollTip = string.Format("{0} {1}\n", "ID:", poll.ID);
                pollTip += string.Format("{0} {1}\n", "Interval:", poll.Interval);
                pollTip += string.Format("{0} {1}", "Command:", poll.Command);
                pollTab.ToolTipText = pollTip;

                TabControl serverTabs = new TabControl();
                serverTabs.ShowToolTips = true;
                serverTabs.Appearance = TabAppearance.Normal;
                
                foreach (Server server in servers)
                {
                    TabPage pageServer = new TabPage(server.Host);

                    string serverTip = pollTip;
                    serverTip += string.Format("\n\n{0} {1}\n", "ID:", server.ID);
                    serverTip += string.Format("{0} {1}\n", "Host:", server.Host);
                    serverTip += string.Format("{0} {1}", "Port:", server.Port);

                    pageServer.ToolTipText = serverTip;
                    pageServer.Tag = server.ID;
                    
                    TextBox textbox = new TextBox();
                    textbox.WordWrap = false;
                    textbox.Multiline = true;
                    textbox.ReadOnly = false;
                    textbox.ScrollBars = ScrollBars.Both;
                    textbox.AcceptsTab = true;
                    textbox.AcceptsReturn = true;

                    textbox.Font = new Font("Courier New", 8);
                    textbox.BackColor = Color.FromArgb(0xf0, 0xf0, 0xf0);
                    textbox.ForeColor = Color.FromArgb(0x00, 0x00, 0x00);

                    pageServer.Controls.Add(textbox);
                    pageServer.Controls[0].Dock = DockStyle.Fill;

                    serverTabs.TabPages.Add(pageServer);
                }

                pollTab.Controls.Add(serverTabs);
                pollTab.Controls[0].Dock = DockStyle.Fill;

                tabsPolls.TabPages.Add(pollTab);
            }
        }
        #endregion

        #region method lvCampaignCreate

        private void lvCampaignCreate()
        {
            lvCampaigns.BeginUpdate();
            lvCampaigns.Columns.Clear();

            lvCampaigns.Columns.Add("ID", "ID");
            lvCampaigns.Columns[0].Tag = "ID";

            lvCampaigns.Columns.Add("Name", "Name");
            lvCampaigns.Columns[1].Tag = "Name";

            lvCampaigns.Columns.Add("Status", "Status");
            lvCampaigns.Columns[2].Tag = "Status";

            lvCampaigns.Columns.Add("StartTime", "Start Time");
            lvCampaigns.Columns[3].Tag = "StartTime";

            lvCampaigns.Columns.Add("DeliveryMethod", "Delivery Method");
            lvCampaigns.Columns[4].Tag = "DeliveryMethod";

            lvCampaigns.Columns.Add("Recipients_Processed", "Processed");
            lvCampaigns.Columns[5].Tag = "Recipients_Processed";

            lvCampaigns.Columns.Add("Recipients_Failed", "Failed");
            lvCampaigns.Columns[6].Tag = "Recipients_Failed";

            lvCampaigns.Columns.Add("TimeElapsed", "Time Elapsed");
            lvCampaigns.Columns[7].Tag = "TimeElapsed";

            lvCampaigns.Columns.Add("MailingLists_Processed", "Lists Processed");
            lvCampaigns.Columns[8].Tag = "MailingLists_Processed";

            lvCampaigns.Columns.Add("Seeds_Processed", "Seeds Processed");
            lvCampaigns.Columns[9].Tag = "Seeds_Processed";

            lvCampaigns.Columns.Add("Scheduler_NextRun", "Next Run");
            lvCampaigns.Columns[10].Tag = "Scheduler_NextRun";

            lvCampaigns.Columns.Add("Scheduler_LastRun", "Last Run");
            lvCampaigns.Columns[11].Tag = "Scheduler_LastRun";

            lvCampaigns.Columns.Add("Total_Opens", "Total Opens");
            lvCampaigns.Columns[12].Tag = "Total_Opens";

            lvCampaigns.Columns.Add("Total_ClickThroughs", "Total Click-Throughs");
            lvCampaigns.Columns[13].Tag = "Total_ClickThroughs";

            lvCampaigns.Columns.Add("Unsubscribe_Requests", "Unsubscribe Requests");
            lvCampaigns.Columns[14].Tag = "Unsubscribe_Requests";


            for (int c = 0; c < lvCampaigns.Columns.Count; c++)
                lvCampaigns.Columns[c].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

            lvCampaigns.Groups.Add(new ListViewGroup("Running", "Running"));
            lvCampaigns.Groups[0].Tag = "Running";
            lvCampaigns.Groups[0].HeaderAlignment = HorizontalAlignment.Left;

            lvCampaigns.Groups.Add(new ListViewGroup("Idle", "Idle"));
            lvCampaigns.Groups[1].Tag = "Idle";
            lvCampaigns.Groups[1].HeaderAlignment = HorizontalAlignment.Left;

            lvCampaigns.Groups.Add(new ListViewGroup("Complete", "Complete"));
            lvCampaigns.Groups[2].Tag = "Complete";
            lvCampaigns.Groups[2].HeaderAlignment = HorizontalAlignment.Left;


            lvCampaigns.View = View.Details;
            lvCampaigns.MultiSelect = true;
            lvCampaigns.ShowGroups = true;
            
            lvCampaigns.EndUpdate();
        }

        #endregion

        #region MainForm_Load

        private void MainForm_Load(object sender, EventArgs e)
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();

            logger.Debug("MainForm: Reading Settings.");
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }


            if (coreSettings == null)
                coreSettings = new CoreSettings();

            Point location = new Point(coreSettings.MainformPosX, coreSettings.MainformPosY);
            if (location.X < 0 || location.X > Screen.PrimaryScreen.Bounds.Width || location.Y < 0 || location.Y > Screen.PrimaryScreen.Bounds.Height)
                this.StartPosition = FormStartPosition.CenterScreen;
            else
            {
                this.Location = location;
                this.StartPosition = FormStartPosition.Manual;
            }

            Size size = new Size(coreSettings.MainformDimX, coreSettings.MainformDimY);
            
            if (size.Width >= Screen.GetWorkingArea(this).Width || size.Height >= Screen.GetWorkingArea(this).Height)
                this.WindowState = FormWindowState.Maximized;

            //if (size.IsEmpty || size.Width < 300 || size.Height < 300)
            //    size = new Size(Screen.GetWorkingArea(this).Width * 25 / 100, Screen.GetWorkingArea(this).Height * 25 / 100);
            
            this.Size = size;
            this.splitContainer1.SplitterDistance = coreSettings.MainformSplitter;
            this.soundsToolStripMenuItem.Checked = coreSettings.PlayIntroSound;

            if (coreSettings.PlayIntroSound)
            {
                Stream stream = Properties.Resources.intro;
                if (stream != null)
                {
                    byte[] bStream = new Byte[stream.Length];
                    stream.Read(bStream, 0, (int)stream.Length);
                    Win32API.PlaySound(bStream, IntPtr.Zero, Win32API.SND_ASYNC | Win32API.SND_MEMORY);
                }
            }


            tabsPollsCreate();
            //tvwNavi.ExpandAll();

            pnlCampaigns.Dock = DockStyle.Fill;
            pnlCampaigns.BringToFront();


            //MainFormClone mainClone = new MainFormClone();
            //mainClone.Show();
        }

        #endregion

        #region MainForm_FormClosing

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IPathManager pathManager = ServiceScope.Get<IPathManager>();
            ILogger logger = ServiceScope.Get<ILogger>();
            ISettingsManager settingsMgr = ServiceScope.Get<ISettingsManager>();

            logger.Debug("MainForm_Closing: Saving Settings.");
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = settingsMgr.Load<CoreSettings>();
            }
            catch {}


            if (coreSettings == null)
                coreSettings = new CoreSettings();


            coreSettings.MainformPosX = this.Location.X;
            coreSettings.MainformPosY = this.Location.Y;
            coreSettings.MainformDimX = this.Size.Width;
            coreSettings.MainformDimY = this.Size.Height;
            coreSettings.MainformSplitter = splitContainer1.SplitterDistance;
            coreSettings.PlayIntroSound = soundsToolStripMenuItem.Checked;
            coreSettings.TotalRun = coreSettings.TotalRun + 1;
            settingsMgr.Save(coreSettings);
            
            //logger.Debug("MainForm_Closing: Saving Master Settings.");
            //SettingsEx settings = SettingsEx.Open();
            //settings.Master.LastPosition = this.Location;
            //settings.Master.FormSize = this.Size;
            //settings.Master.SoundEffects = soundsToolStripMenuItem.Checked;
            //settings.Master.Popularity++;
            //settings.Master.Splitter = splitContainer1.SplitterDistance;
            //settings.Save();

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            List<Campaign> campaigns = mgr.GetCampaigns();

            logger.Debug("MainForm_Closing: Serializing Polls.");

            PollCollection pollColl = new PollCollection();
            List<PollItem> allPolls = mgr.GetPolls();
            foreach (PollItem poll in allPolls)
                pollColl.Add(poll);

            byte[] rawBytes;
            SerializeEx.SerializedType type;

            if (pollColl.Polls.Count > 0)
            {
                rawBytes = SerializeEx.Serializer.Serialize(pollColl, out type, 0);
                File.WriteAllBytes(pathManager.GetPath(@"<CONFIG>\Polls.dat"), rawBytes);
            }
            else
                if (File.Exists(pathManager.GetPath(@"<CONFIG>\Polls.dat"))) File.Delete(pathManager.GetPath(@"<CONFIG>\Polls.dat"));


            logger.Debug("MainForm_Closing: Serializing Servers.");

            ServerCollection serverColl = new ServerCollection();
            List<Server> allServers = mgr.GetServers();
            foreach (Server server in allServers)
                serverColl.Add(server);

            if (serverColl.Servers.Count > 0)
            {
                rawBytes = SerializeEx.Serializer.Serialize(serverColl, out type, 0);
                File.WriteAllBytes(pathManager.GetPath(@"<CONFIG>\Servers.dat"), rawBytes);
            }
            else
                if (File.Exists(pathManager.GetPath(@"<CONFIG>\Servers.dat"))) File.Delete(pathManager.GetPath(@"<CONFIG>\Servers.dat"));


            logger.Debug("MainForm_Closing: Unloading Campaigns");

            foreach (Campaign campaign in campaigns)
            {
                if (campaign != null)
                {
                    if (!campaign.IsDisposed)
                    {
                        if (campaign.Running)
                            campaign.Stop();

                        campaign.Dispose();
                    }
                }
            }
        }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
//#if (!DEBUG)
            if (_isActive)
            {
                this.Opacity = 1.0;
            }
            else
            {
                this.Opacity = 0.75;
            }
//#endif
        }
        

        #endregion

        #region WndProc

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case Win32API.WM_ACTIVATEAPP:
                    _isActive = (((int)m.WParam != 0));
                    this.Invalidate();
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region method PathFix

        /// <summary>
        /// Fixes path separator, replaces / \ with platform separator char.
        /// </summary>
        /// <param name="path">Path to fix.</param>
        /// <returns></returns>
        public string PathFix(string path)
        {
            return path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
        }

        #endregion

        #region CurrentDomain_UnhandledException

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            logger.Error("MainForm: An unexpected error has been logged.");
            ErrorEx.DumpError((Exception)e.ExceptionObject, new StackTrace());
        }

        #endregion

        #region method CreateStartSummary

        public string CreateStartSummary(List<Campaign> campaigns)
        {
            int totalServers = 0;
            int totalThreads = 0;
            int totalConnection = 0;
            int totalDomains = 0;
            int totalMessages = 0;
            int queueItems = 0;
            long queueLines = 0;
            long queueBytes = 0;

            foreach (Campaign c in campaigns)
            {
                if (c.IsDisposed) continue;

                totalServers += c.Mailhosts.Count;
                totalThreads += c.ThreadsPerConnection * c.ConnectionsPerServer;
                totalConnection += c.ConnectionsPerServer * c.Mailhosts.Count;
                totalDomains += c.Domains.Count;
                totalMessages += c.MessageList.Count;
                queueItems += c.RecipientProviders.Count;

                foreach (Campaign.RecipientProvider rp in c.RecipientProviders)
                {
                    queueLines += rp.LineCount;
                    queueBytes += new FileInfo(rp.FileName).Length;
                }
            }

            string cleanQueueSize = Conversion.GetSizeString(queueBytes);
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Campaigns", campaigns.Count);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Servers", totalServers);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Queues", queueItems);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Queue Size", cleanQueueSize);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Queue Rcpts", queueLines);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Threads", totalThreads);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Connections", totalConnection);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Domains", totalDomains);
            sb.AppendFormat("{0,-16} {1,-15}\r\n", "Messages", totalMessages);

            return sb.ToString();
        }

        #endregion

        #region method InsertMacroText

        private void InsertMacroText(TextBox txtbox, int selStart, string text)
        {
            txtbox.Text = txtbox.Text.Insert(selStart, text);
            txtbox.SelectionStart = selStart + text.Length;
        }

        #endregion

        #region InitializeNewCampaign

        private void InitializeNewCampaign(string name)
        {
            InitializeNewCampaign(name, null);
        }

        private void InitializeNewCampaign(string name, Campaign toClone)
        {
            string nameClean = name;
            foreach (char c in Path.GetInvalidFileNameChars()) nameClean = nameClean.Replace(c.ToString(), "");
            foreach (char c in Path.GetInvalidPathChars()) nameClean = nameClean.Replace(c.ToString(), "");

            if (nameClean != name)
            {
                string message = "Certain characters have been stripped due to being invalid for a path or file name.\r\n\r\nSafe Name: " + nameClean + ".\r\n\r\nWould you like to continue with the newly modified name?";
                if (DialogResult.Yes != MessageBox.Show(message, "Konvict", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
                else
                    name = nameClean;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            // If we clone don't check for duplicate names.

            if (toClone == null)
            {
                List<Campaign> campaigns = ServiceScope.Get<ICampaignManager>().GetCampaigns();

                foreach (Campaign c in campaigns)
                {
                    if (c.Name.Equals(name))
                    {
                        MessageBox.Show("Please choose another name. This one is already in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            // Create node for treeview.
            TreeNode nodeRoot = new TreeNode(name, 18, 18);
            nodeRoot.Tag = "CampaignSettings\t" + name.ToString();
            //nodeRoot.ContextMenuStrip = cntxmnuCampaignRoot;
            nodeRoot.Nodes.Add(new TreeNode("Options", 1, 1));
            nodeRoot.Nodes.Add(new TreeNode("Messages", 4, 4));
            nodeRoot.Nodes.Add(new TreeNode("Recipients", 5, 5));
            nodeRoot.Nodes.Add(new TreeNode("Advanced", 6, 6));
            nodeRoot.Nodes.Add(new TreeNode("Servers", 11, 11));

            for (int i = 0; i < nodeRoot.Nodes.Count; i++)
                nodeRoot.Nodes[i].Tag = nodeRoot.Tag;

            Campaign campaign = null;

            // Clone the campaign

            if (toClone != null)
            {
                campaign = toClone.Clone() as Campaign;
                toClone.Dispose();
                toClone = null;
            }
            else
                campaign = new Campaign(name);


            ListViewItem lvItem = new ListViewItem(campaign.ID);
            lvItem.SubItems.Add(campaign.Name);

            for (int i = 0; i < lvCampaigns.Columns.Count - 2; i++) 
                lvItem.SubItems.Add(string.Empty);

            lvCampaigns.Items.Add(lvItem);
            lvCampaigns.Columns["Name"].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);


            // Mailhosts need reinitializing due to being nonserializable.

            Server[] hosts = campaign.Mailhosts.ToArray();
            campaign.Mailhosts.Clear();
            foreach (Server host in hosts)
                campaign.Mailhosts.Add(host);
            
            
            // RecipientProviders need reinitializing.

            //Campaign.RecipientProvider[] recipProviders = campaign.RecipientProviders.ToArray();
            //campaign.RecipientProviders.Clear();
            //foreach (Campaign.RecipientProvider rp in recipProviders)
            //{
            //    campaign.RecipientProviders.Add(new Campaign.RecipientProvider(rp.Close()));
            //}

            for (int i = 0; i < campaign.RecipientProviders.Count; )
            {
                if (!File.Exists(campaign.RecipientProviders[i].FileName))
                    campaign.RecipientProviders.RemoveAt(i);
                else
                    i++;
            }


            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            mgr.AddCampaign(campaign);


            //nodeRoot.ExpandAll();
            tvwNavi.BeginUpdate();
            tvwNavi.Nodes[0].Nodes.Add(nodeRoot);
            tvwNavi.Nodes[0].Expand();
            tvwNavi.EndUpdate();

            // Serialize the campaign and save to disk.

            string campaignFilename = ServiceScope.Get<IPathManager>().GetPath(@"<CAMPAIGN_ROOT>\") + campaign.Name + Path.DirectorySeparatorChar + campaign.Name + ".cmp";
            Directory.CreateDirectory(Path.GetDirectoryName(PathFix(campaignFilename)));
            byte[] bytes;
            SerializeEx.SerializedType type;
            bytes = SerializeEx.Serializer.Serialize(campaign, out type, 0);
            File.WriteAllBytes(campaignFilename, bytes);

        }
        #endregion

        #region LoadCampaign

        /// <summary>
        /// Load a <see cref="T:Campaign"/>.
        /// </summary>
        /// <param name="name">The name of the campaign to load.</param>
        private void LoadCampaign(string name)
        {
            Campaign campaign = null;

            try
            {
                string campaignFilename = ServiceScope.Get<IPathManager>().GetPath(@"<CAMPAIGN_ROOT>\") + name + Path.DirectorySeparatorChar + name + ".cmp";


                byte[] bytes = File.ReadAllBytes(campaignFilename);
                campaign = (Campaign)SerializeEx.Serializer.DeSerialize(bytes, SerializeEx.SerializedType.CompressedObject);

                if (campaign == null)
                    throw new FileLoadException("FileLoadException.");
            }
            catch (Exception x)
            {
                MessageBox.Show("Error reading campaign.\n\nMessage: " + x.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorEx.DumpError(x, new StackTrace());
            }

            if (campaign != null)
                InitializeNewCampaign(campaign.Name, campaign);
        }

        #endregion

        #region LoadCampaigns

        private void LoadCampaigns()
        {
            string campaignRoot = ServiceScope.Get<IPathManager>().GetPath(@"<CAMPAIGN_ROOT>\");
            if (!Directory.Exists(campaignRoot))
                return;

            string[] dirs = Directory.GetDirectories(campaignRoot);
            foreach (string dir in dirs)
                LoadCampaign(new FileInfo(dir).Name.ToString());
        }

        #endregion

        #region SaveCampaign

        /// <summary>
        /// Save campaign data to disk and update all properties.
        /// </summary>
        /// <remarks>
        /// The message data is NOT saved here.
        /// </remarks>
        /// <param name="campaignId">The ID of the campaign.</param>
        private void SaveCampaign(string campaignId)
        {
            ICampaignManager campaignMgr = ServiceScope.Get<ICampaignManager>();
            Campaign campaign = campaignMgr.GetCampaign(campaignId);

            if (campaign == null)
                throw new Exception("SaveCampaign(): Campaign ID '" + campaignId + "' not found!");

            try
            {
                int res = -1;
                long res64 = -1;

                if (int.TryParse(txtEmailPerSessionMin.Text, out res)) campaign.SessionMinimumEmails = res;
                if (int.TryParse(txtEmailPerSessionMax.Text, out res)) campaign.SessionMaximumEmails = res;
                if (int.TryParse(txtThreadsPerConnection.Text, out res)) campaign.ThreadsPerConnection = res;
                if (int.TryParse(txtConnectionsPerServer.Text, out res)) campaign.ConnectionsPerServer = res;

                campaign.VmtaPool = txtPoolName.Text;
                campaign.Suppression = chkSupression.Checked;
                campaign.SupressionFile = txtSupressionFileName.Text;
                campaign.Seeding = chkSeed.Checked;
                campaign.Seeds = txtSeedList.Lines;

                if (Int64.TryParse(txtSeedInterval.Text, out res64)) campaign.SeedInterval = res64;

                campaign.recipientField = (Campaign.RecipientField)cboAddRecipAction.SelectedIndex;
                campaign.EncodeFromMIME = chkMIMEFrom.Checked;
                campaign.EncodeSubjectMIME = chkMIMESubject.Checked;
                campaign.MIMECharsetFrom = txtFromsCharsets.Text.Split(new char[] { ',' });
                campaign.MIMECharsetSubject = txtSubjectsCharsets.Text.Split(new char[] { ',' });
                campaign.RotateMessages = chkRotateMessages.Checked;
                campaign.rotateMessageCondition = (Campaign.RotateMessageCondition)cboRotateKey.SelectedIndex;
                if (int.TryParse(txtRotateInterval.Text, out res)) campaign.RotateMessageInterval = res;


                foreach (ListViewItem lv in lvRotate.Items) campaign[lv.Text].InRotation = lv.Checked;

                // Save the domain list
                if (txtDomains.Lines.Length != 0)
                {
                    campaign.Domains.Clear();
                    campaign.Domains.AddRange(txtDomains.Lines);
                }

                // Save current displayed message only.
                string messageId = lblMessageID.Text.Split(':')[1].Trim().ToString();
                if (messageId.Length != 0)
                {
                    campaign[messageId].MessageText = txtMessageText.Text;
                    campaign[messageId].MessageHtml = txtMessageHTML.Text;
                    campaign[messageId].MessageSource = txtMessageSource.Text;
                    campaign[messageId].Subjects = txtSubjects.Lines;
                    campaign[messageId].FromPrefix = txtFromEmailPrefix.Lines;
                    campaign[messageId].FromAlias = txtFromAliases.Lines;
                    campaign[messageId].AttachmentList = txtAttachments.Lines;
                }

                IPathManager pathManager = ServiceScope.Get<IPathManager>();

                string campaignRoot = pathManager.GetPath(@"<CAMPAIGN_ROOT>\");
                if (!Directory.Exists(campaignRoot))
                    Directory.CreateDirectory(campaignRoot);

                string campaignPath = campaignRoot + campaign.Name;

                if (!Directory.Exists(campaignRoot + campaign.Name))
                    Directory.CreateDirectory(campaignRoot);


                string campaignFilename = ServiceScope.Get<IPathManager>().GetPath(@"<CAMPAIGN_ROOT>\") + campaign.Name + Path.DirectorySeparatorChar + campaign.Name + ".cmp";
                byte[] bytes;
                SerializeEx.SerializedType type;
                bytes = SerializeEx.Serializer.Serialize(campaign, out type, 0);
                File.WriteAllBytes(campaignFilename, bytes);
            }
            catch (Exception x)
            {
                ErrorEx.DumpError(x, new StackTrace());
            }

        }

        #endregion

        #region DisplayCampaignSettings

        private bool DisplayCampaignSettings(string campaignId)
        {
            try
            {
                Campaign campaign = ServiceScope.Get<ICampaignManager>().GetCampaign(campaignId);

                cboMessages.BeginUpdate();
                cboMessages.Items.Clear();
                cboMessages.Items.AddRange(campaign.GetMessageNames());
                cboMessages.EndUpdate();

                txtPoolName.Text = campaign.VmtaPool.ToString();
                txtEmailPerSessionMin.Text = campaign.SessionMinimumEmails.ToString();
                txtEmailPerSessionMax.Text = campaign.SessionMaximumEmails.ToString();
                txtThreadsPerConnection.Text = campaign.ThreadsPerConnection.ToString();
                txtConnectionsPerServer.Text = campaign.ConnectionsPerServer.ToString();

                chkSupression.Checked = campaign.Suppression;
                txtSupressionFileName.Text = campaign.SupressionFile.ToString();

                chkSeed.Checked = campaign.Seeding;
                if (campaign.Seeds != null) 
                    txtSeedList.Lines = campaign.Seeds;

                txtSeedInterval.Text = campaign.SeedInterval.ToString();
                cboAddRecipAction.SelectedIndex = (int)campaign.recipientField;
                
                chkMIMEFrom.Checked = campaign.EncodeFromMIME;
                chkMIMESubject.Checked = campaign.EncodeSubjectMIME;

                if (campaign.MIMECharsetFrom != null) 
                    txtFromsCharsets.Text = string.Join(",", campaign.MIMECharsetFrom);

                if (campaign.MIMECharsetSubject != null) 
                    txtSubjectsCharsets.Text = string.Join(",", campaign.MIMECharsetSubject);

                chkRotateMessages.Checked = campaign.RotateMessages;
                cboRotateKey.SelectedIndex = (int)campaign.rotateMessageCondition;
                txtRotateInterval.Text = campaign.RotateMessageInterval.ToString();

                
                // Message Rotation

                lvRotate.BeginUpdate();
                lvRotate.Items.Clear();

                List<Message> messages = campaign.MessageList;
                foreach (Message message in messages)
                {
                    ListViewItem item = new ListViewItem(message.ID.ToString());
                    item.SubItems.Add(message.Name.ToString());
                    item.Checked = message.InRotation;
                    lvRotate.Items.Add(item);
                }
                lvRotate.EndUpdate();


                // Domains.

                if (campaign.Domains != null) 
                    txtDomains.Lines = campaign.Domains.ToArray();


                // Scheduler

                lnkSchedulerStatus.Text = campaign.EnableSchedule ? "On" : "Off";

                // Select message.

                if (cboMessages.Items.Count > 0) 
                    cboMessages.SelectedIndex = 0;

            }
            catch (Exception exc)
            {
                ServiceScope.Get<ILogger>().Error("DisplayCampaignSettings()", exc);
                return false;
            }

            return true;
        }

        #endregion

        #region ShowListviewValues

        private void ShowListviewValues()
        {
            Campaign campaign = selectedCampaign();

            // Recipient Files

            lvRecipients.BeginUpdate();

            foreach (Campaign.RecipientProvider rp in campaign.RecipientProviders)
            {
                ListViewItem lvItem = new ListViewItem(rp.FileName);
                lvItem.SubItems.Add(Conversion.GetSizeString(new FileInfo(rp.FileName).Length));
                lvItem.SubItems.Add(rp.RecipientFieldIndex.ToString());
                lvItem.SubItems.Add(rp.HasHeaders.ToString());
                lvItem.SubItems.Add(rp.Delimiter.ToString());
                lvItem.SubItems.Add(rp.Quote.ToString());
                lvItem.SubItems.Add(rp.Escape.ToString());
                lvItem.SubItems.Add(rp.TrimSpaces.ToString());
                lvRecipients.Items.Add(lvItem);
            }

            foreach (ColumnHeader ch in lvRecipients.Columns)
                ch.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

            lvRecipients.EndUpdate();


            // Servers

            lvServers.BeginUpdate();

            foreach (Server mh in campaign.Mailhosts)
            {
                ListViewItem lvItem = new ListViewItem(mh.Host);
                lvItem.SubItems.Add(mh.Port.ToString());
                lvItem.SubItems.Add(mh.Username);
                lvItem.SubItems.Add(mh.Password);
                lvItem.Tag = mh.ID.ToString();
                lvItem.Checked = mh.Enabled;
                lvServers.Items.Add(lvItem);
            }

            foreach (ColumnHeader ch in lvServers.Columns)
                ch.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

            lvServers.EndUpdate();
        }
        
        #endregion

        #region ShowMessageValues

        private void ShowMessageValues()
        {
            Message msg = selectedMessage();

            lblMessageID.Text = "ID: " + msg.ID.ToString();
            txtMessageHTML.Text = msg.MessageHtml != null ? msg.MessageHtml : string.Empty;
            txtMessageText.Text = msg.MessageText != null ? msg.MessageText : string.Empty;
            txtMessageSource.Text = msg.MessageSource != null ? msg.MessageSource : string.Empty;

            txtSubjects.Lines = msg.Subjects;
            txtFromEmailPrefix.Lines = msg.FromPrefix;
            txtFromAliases.Lines = msg.FromAlias;
            txtAttachments.Lines = msg.AttachmentList;
        }
        
        #endregion

        #region ClearMessageOptions

        private void ClearMessageOptions()
        {
            cboMessages.Items.Clear();
            lblMessageID.Text = "ID: ";
            txtMessageHTML.Text = string.Empty;
            txtMessageText.Text = string.Empty;
            txtMessageSource.Text = string.Empty;
            txtSubjects.Text = string.Empty;
            txtFromEmailPrefix.Text = string.Empty;
            txtFromAliases.Text = string.Empty;
            txtAttachments.Text = string.Empty;
        }

        #endregion

        #region ClearOptionListviews

        private void ClearOptionListviews()
        {
            lvRecipients.BeginUpdate();
            lvRecipients.Items.Clear();
            lvRecipients.EndUpdate();

            lvServers.BeginUpdate();
            lvServers.Items.Clear();
            lvServers.EndUpdate();

            lvRotate.BeginUpdate();
            lvRotate.Items.Clear();
            lvRotate.EndUpdate();
        }

        #endregion

        #region GetControls

        public static Collection<ControlType> GetControls<ControlType>(ContainerControl Parent)
            where ControlType : Control
        {

            Collection<ControlType> coll = new Collection<ControlType>();
            FindControls<ControlType>(coll, Parent);
            return coll;
        }

        #endregion

        #region FindControls

        public static void FindControls<ControlType>(Collection<ControlType> coll, Control Parent)
            where ControlType : Control
        {
            foreach (Control c in Parent.Controls)
            {
                if (c is ControlType)
                    coll.Add(c as ControlType);
                if (c.HasChildren)
                    FindControls(coll, c);

            }
        }

        #endregion

        #region AddServer

        /// <summary>
        /// Add <paramref name="server"/> to selected <see cref="T:Campaign"/>.
        /// </summary>
        /// <param name="server">The server to add.</param>
        /// <exception cref="T:ArgumentNullException">
        ///		<paramref name="server"/> is a <see langword="null"/>.
        /// </exception>
        public void AddServer(Server server)
        {
            if (server == null)
                throw new ArgumentNullException("server");

            ListViewItem lvItem = new ListViewItem(server.Host);
            lvItem.SubItems.Add(server.Port.ToString());
            lvItem.SubItems.Add(server.Username);
            lvItem.SubItems.Add(server.Password);
            lvItem.Tag = server.ID.ToString();
            lvItem.Checked = server.Enabled;

            lvServers.BeginUpdate();
            lvServers.Items.Add(lvItem);
            lvServers.EndUpdate();

            Campaign campaign = selectedCampaign();
            campaign.Mailhosts.Add(server);
        }
        

        #endregion

        #region AddRecipientFile

        public void AddRecipientFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            Campaign.RecipientProvider recipientProvider = new Campaign.RecipientProvider(fileName);
            Campaign campaign = selectedCampaign();
            campaign.RecipientProviders.Add(recipientProvider);

            //Work w = new Work(new DoWorkHandler(recipientProvider.PerformLineCount));
            //IThreadPool pool = ServiceScope.Get<IThreadPool>();
            //pool.Add(w);

            this.Cursor = Cursors.WaitCursor;
            recipientProvider.PerformLineCount();
            this.Cursor = Cursors.Default;

            ListViewItem lvItem = new ListViewItem(fileName);
            lvItem.SubItems.Add(Conversion.GetSizeString(new FileInfo(fileName).Length));
            lvItem.SubItems.Add(Campaign.RecipientProvider.DefaultRecipientFieldIndex.ToString());
            lvItem.SubItems.Add(Campaign.RecipientProvider.DefaultHasHeaders.ToString());
            lvItem.SubItems.Add(Campaign.RecipientProvider.DefaultDelimiter.ToString());
            lvItem.SubItems.Add(Campaign.RecipientProvider.DefaultQuote.ToString());
            lvItem.SubItems.Add(Campaign.RecipientProvider.DefaultEscape.ToString());
            lvItem.SubItems.Add(Campaign.RecipientProvider.DefaultTrimSpaces.ToString());
            lvRecipients.Items.Add(lvItem);
        }

        #endregion

        #region Control Events

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (e.Node.Tag == null) 
            //    return;

            //if(e.Node.Tag.ToString().Contains("CampaignSettings"))
            //{
            //    // Get the campaign name.
            //    string name = e.Node.Tag.ToString().Split(new char[] { '\t' })[1];
            //    lblCampaignName.Text = name;

            //    _currentCampaignId = ServiceScope.Get<ICampaignManager>().GetCampaignByName(name).ID;

            //    // Update selected campaign index.
            //    //_campaignNameIndexes.TryGetValue(name, out _currentCampaignIndex);
            //    //if (_currentCampaignIndex < 0) return;

            //    // Clear then populate GUI controls.
            //    ClearOptionListviews();
            //    ClearMessageOptions();
            //    DisplayCampaignSettings(selectedCampaign().ID);
            //    ShowListviewValues();

            //    // Bring the panel to the front and select the subcategory
            //    pnlCampaignSettings.Dock = DockStyle.Fill;
            //    pnlCampaignSettings.BringToFront();

            //    if (e.Node.Nodes.Count == 0) 
            //        tabControl1.SelectTab(e.Node.Text);

            //    return;
            //}

            //// Display other panels.
            //foreach (Panel panel in GetControls<Panel>(this))
            //{
            //    if (panel.Tag != null && (panel.Tag == e.Node.Tag))
            //    {
            //        panel.Dock = DockStyle.Fill;
            //        panel.BringToFront();
            //        return;
            //    }
            //}
        }

        private void newCampaignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBoxResult res = InputBox.Show("Enter the name of the new campaign:", "New Campaign", "");
            if (res.ReturnCode == DialogResult.OK)
                InitializeNewCampaign(res.Text.ToString());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newCampaignToolStripMenuItem.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvwNavi.Nodes[0].Nodes)
            {
                if (node.IsSelected)
                {
                    DialogResult res = MessageBox.Show("Are you sure you want to delete this campaign?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
                        Campaign campaign = mgr.GetCampaignByName(node.Text);
                        if (campaign != null)
                        {
                            campaign.Dispose();
                            mgr.RemoveCampaign(campaign.ID);

                            ListViewItem lvItem = lvCampaigns.FindItemWithText(campaign.Name);

                            if (lvItem != null) lvCampaigns.Items.Remove(lvItem);
                            node.Remove();
                            tvwNavi.SelectedNode = null;

                            try
                            {
                                Directory.Delete(ServiceScope.Get<IPathManager>().GetPath(@"<CAMPAIGN_ROOT>\") + campaign.Name, true);
                            }
                            catch (Exception exc)
                            {
                                ErrorEx.DumpError(exc, new StackTrace());
                                MessageBox.Show("[ERROR]: Message: " + exc.Message);
                            }

                            //tvwNavi.SelectedNode = tvwNavi.Nodes[0];
                            return;
                        }
                    }
                }
            }
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            normalToolStripMenuItem1.Checked = true;
            buttonsToolStripMenuItem1.Checked = (!normalToolStripMenuItem1.Checked);
            flatButtonsToolStripMenuItem1.Checked = (!normalToolStripMenuItem1.Checked);
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.Refresh();
        }

        private void buttonsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buttonsToolStripMenuItem1.Checked = true;
            normalToolStripMenuItem1.Checked = (!buttonsToolStripMenuItem1.Checked);
            flatButtonsToolStripMenuItem1.Checked = (!buttonsToolStripMenuItem1.Checked);
            tabControl1.Appearance = TabAppearance.Buttons;
        }

        private void flatButtonsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            flatButtonsToolStripMenuItem1.Checked = true;
            normalToolStripMenuItem1.Checked = (!flatButtonsToolStripMenuItem1.Checked);
            buttonsToolStripMenuItem1.Checked = (!flatButtonsToolStripMenuItem1.Checked);
            tabControl1.Appearance = TabAppearance.FlatButtons;
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            topToolStripMenuItem.Checked = true;
            bottomToolStripMenuItem.Checked = (!topToolStripMenuItem.Checked);
            tabControl1.Alignment = TabAlignment.Top;
        }

        private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bottomToolStripMenuItem.Checked = true;
            topToolStripMenuItem.Checked = (!bottomToolStripMenuItem.Checked);
            tabControl1.Alignment = TabAlignment.Bottom;
        }

        private void btnRcptAddFiles_Click(object sender, EventArgs e)
        {
//            DEK
//[5/5/2009 8:44:33 PM] smev_nk: Dim realdate as Date = Today
//Today = Date.Parse("3/3/2009")
//Shell("devenv.exe", AppWinStyle.NormalFocus, False, -1)
//Thread.Sleep(3000)
//Today = realdate

            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fn in openFileDialog1.FileNames)
                {
                    FileInfo fi = new FileInfo(fn);
                    if (fi.Length == 0 || !fi.Exists) continue;
                    AddRecipientFile(fn);
                }
            }
        }

        private void btnRcptDeleteFiles_Click(object sender, EventArgs e)
        {
            if (lvRecipients.Items.Count == 0 || lvRecipients.SelectedItems.Count == 0) 
                return;

            if (MessageBox.Show("You have selected " + lvRecipients.SelectedItems.Count + " to be removed.\nDo you want to continue?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            
            lvRecipients.BeginUpdate();

            Campaign campaign = selectedCampaign();

            foreach (ListViewItem lvItem in lvRecipients.SelectedItems)
            {
                for (int i = 0; i < campaign.RecipientProviders.Count; )
                {
                    if (string.Equals(campaign.RecipientProviders[i].FileName.ToString(), lvItem.Text.ToString()))
                    {
                        campaign.RecipientProviders.RemoveAt(i);
                        lvItem.Remove();
                    }
                    else
                        i++;
                }
            }

            //foreach (ListViewItem lvItem in lvRecipients.Items)
            //    if (lvItem.Selected)
            //    {
            //        Campaign.RecipientProvider rp = campaign.RecipientProviders.Find(delegate(Campaign.RecipientProvider r) { return r.FileName == lvItem.Text; });
            //        if (rp != null)
            //        {
            //            campaign.RecipientProviders.Remove(rp);
            //            lvItem.Remove();
            //        }
            //    }
        
            lvRecipients.EndUpdate();
        }

        private void btnRcptRemoveAll_Click(object sender, EventArgs e)
        {
            lvRecipients.BeginUpdate();
            Campaign campaign = selectedCampaign();
            campaign.RecipientProviders.Clear();
            lvRecipients.Items.Clear();
            lvRecipients.EndUpdate();
        }

        private void btnNewMessage_Click(object sender, EventArgs e)
        {
            InputBoxResult result = InputBox.Show("Enter a name for the new message:", "New message", "New");
            if (result.ReturnCode != DialogResult.OK) 
                return;

            if (string.IsNullOrEmpty(result.Text))
            {
                MessageBox.Show("Invalid message name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Campaign campaign = selectedCampaign();
            campaign.AddMessage(result.Text);
            cboMessages.Items.Add(result.Text);
            cboMessages.SelectedIndex = cboMessages.Items.IndexOf(result.Text);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Campaign campaign = selectedCampaign();
            if (campaign != null)
            {
                SaveCampaign(campaign.ID);
            }
        }

        private void cboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Campaign campaign = selectedCampaign();
            List<Message> messages = campaign.MessageList;

            foreach (Message message in messages)
            {
                if (message.Name == cboMessages.Items[cboMessages.SelectedIndex].ToString())
                    _currentMessageId = message.ID;   
            }

            ShowMessageValues();
        }

        private void btnServersAdd_Click(object sender, EventArgs e)
        {
            AddServerDialog addServer = new AddServerDialog();
            if (addServer.ShowDialog(this) == DialogResult.OK)
                AddServer(addServer.Server);

        }

        private void btnServersDeleteSelected_Click(object sender, EventArgs e)
        {
            if (lvServers.Items.Count == 0 || lvServers.SelectedItems.Count == 0) return;

            if (MessageBox.Show("You have selected " + lvServers.SelectedItems.Count + " to be removed.\nDo you want to continue?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            lvServers.BeginUpdate();

            Campaign campaign = selectedCampaign();

            foreach (ListViewItem li in lvServers.SelectedItems)
            {
                for (int i = 0; i < campaign.Mailhosts.Count; )
                {
                    if (string.Equals(campaign.Mailhosts[i].ID.ToString(), li.Tag.ToString()))
                    {
                        campaign.Mailhosts.RemoveAt(i);
                        li.Remove();
                    }
                    else
                        i++;
                }
            }

            lvServers.EndUpdate();
        }

        private void btnServersRemoveAll_Click(object sender, EventArgs e)
        {
            if (lvServers.Items.Count == 0)
                return;

            if (MessageBox.Show("Are you sure?", "Remove All", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            Campaign campaign = selectedCampaign();
            campaign.Mailhosts.Clear();
            lvServers.Items.Clear();
        }

        private void btnServersAddFiles_Click(object sender, EventArgs e)
        {
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fn in openFileDialog1.FileNames)
                {
                    FileInfo fi = new FileInfo(fn);
                    if (!fi.Exists || fi.Length == 0)
                        continue;
                   
                    FileStream fs = fi.Open(FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    while (!sr.EndOfStream)
                    {
                        string l = sr.ReadLine();
                        string[] fields = l.Split(new char[] { ':' });
                        if (fields.Length < 2)
                            continue;

                        Server mailhost = new Server(fields[0], Convert.ToInt32(fields[1]));
                        
                        if (fields.Length >= 3)
                            mailhost.Username = fields[2];

                        if (fields.Length >= 4)
                            mailhost.Password = fields[3];

                        AddServer(mailhost);
                        
                    }
                    sr.Close();
                    sr.Dispose();
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private void btnModifyRecipientFile_Click(object sender, EventArgs e)
        {
            if (lvRecipients.SelectedItems.Count == 0)
                return;

            Campaign campaign = selectedCampaign();

            foreach (ListViewItem lvItem in lvRecipients.Items)
                if (lvItem.Selected)
                {
                    Campaign.RecipientProvider rp = campaign.RecipientProviders.Find(delegate(Campaign.RecipientProvider r) { return r.FileName == lvItem.Text; });
                    if (rp != null)
                    {
                        txtRecipientColumn.Enabled = true;
                        txtDelimiter.Enabled = true;
                        txtQuote.Enabled = true;
                        txtEscape.Enabled = true;
                        cboTrimSpaces.Enabled = true;
                        cboHasHeaders.Enabled = true;

                        // Store the item in the tag so we can submit changes.
                        txtRecipientColumn.Tag = rp.FileName.ToString();

                        // Store the listitem index in the tag so we can submit changes.
                        txtDelimiter.Tag = lvItem.Index.ToString();

                        txtRecipientColumn.Text = rp.RecipientFieldIndex.ToString();
                        txtDelimiter.Text = rp.Delimiter.ToString();
                        txtQuote.Text = rp.Quote.ToString();
                        txtEscape.Text = rp.Escape.ToString();

                        cboHasHeaders.SelectedIndex = (rp.HasHeaders ? 0 : 1);
                        cboTrimSpaces.SelectedIndex = (rp.TrimSpaces ? 0 : 1);

                        return;
                    }
                }
        }

       private void btnRcptSaveChanges_Click(object sender, EventArgs e)
        {
            if (!txtRecipientColumn.Enabled)
                return;

            Campaign campaign = selectedCampaign();
            Campaign.RecipientProvider rp = campaign.RecipientProviders.Find(delegate(Campaign.RecipientProvider r) { return r.FileName == txtRecipientColumn.Tag.ToString(); });

            if (rp != null)
            {
                try
                {
                    rp.RecipientFieldIndex = Convert.ToInt32(txtRecipientColumn.Text);
                    rp.Escape = Convert.ToChar(txtEscape.Text);
                    rp.Delimiter = Convert.ToChar(txtDelimiter.Text);
                    rp.Quote = Convert.ToChar(txtQuote.Text);


                    rp.HasHeaders = (cboHasHeaders.SelectedIndex == 0 ? true : false);
                    rp.TrimSpaces = (cboTrimSpaces.SelectedIndex == 0 ? true : false);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error editing.\n\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                lvRecipients.BeginUpdate();

                ListViewItem lvItem = lvRecipients.Items[Convert.ToInt32(txtDelimiter.Tag.ToString())];
                lvItem.SubItems[2].Text = rp.RecipientFieldIndex.ToString();
                lvItem.SubItems[3].Text = rp.HasHeaders.ToString();
                lvItem.SubItems[4].Text = rp.Delimiter.ToString();
                lvItem.SubItems[5].Text = rp.Quote.ToString();
                lvItem.SubItems[6].Text = rp.Escape.ToString();
                lvItem.SubItems[7].Text = rp.TrimSpaces.ToString();

                lvRecipients.Items[Convert.ToInt32(txtDelimiter.Tag.ToString())] = lvItem;

                lvRecipients.EndUpdate();


                txtRecipientColumn.Enabled = false;
                txtDelimiter.Enabled = false;
                txtQuote.Enabled = false;
                txtEscape.Enabled = false;
                cboTrimSpaces.Enabled = false;
                cboHasHeaders.Enabled = false;

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutInfo aboutInfo = new AboutInfo();
            aboutInfo.StartPosition = FormStartPosition.CenterParent;
            aboutInfo.ShowDialog(this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MacroInfo macroInfo = new MacroInfo();
            macroInfo.StartPosition = FormStartPosition.CenterParent;
            macroInfo.ShowDialog(this);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuCampaignStart_Click(object sender, EventArgs e)
        {
            if (lvCampaigns.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a campaign.", "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            List<Campaign> selected = new List<Campaign>();

            foreach (ListViewItem item in lvCampaigns.SelectedItems)
                selected.Add(mgr.GetCampaign(item.Text));


            StartCampaignDialog startDialog = new StartCampaignDialog(selected, this);

            if (startDialog.ShowDialog(this) == DialogResult.OK)
            {
                List<Campaign> campaigns = selected;
                mgr.StartCampaign(campaigns);
            }
        }

        #region ToolStrip

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageText.SelectionStart;
            RndDialog rndDialog = new RndDialog();
            if (rndDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageText, selPos, String.Format("$Rnd({0})", rndDialog.MacroText.ToString()));

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageText.SelectionStart;
            SpecialTagDialog specialDialog = new SpecialTagDialog();
            if (specialDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageText, selPos, String.Format("${0}()", specialDialog.SelectedTag.ToString()));

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageText.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageText, selPos, String.Format("$Include({0})", openFileDialog1.FileName.ToString()));

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageText.SelectionStart;
            RotateDialog rotateDialog = new RotateDialog();
            if (rotateDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < rotateDialog.Phrases.Count; i++)
                {
                    sb.Append(rotateDialog.Phrases[i].ToString());
                    if (i != (rotateDialog.Phrases.Count - 1)) sb.Append("|");
                }
                InsertMacroText(txtMessageText, selPos, String.Format("$Rot({0})", sb.ToString()));
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageText.SelectionStart;
            InputBoxResult res = InputBox.Show("Enter uri to retrieve:", "", "http://www.google.com");
            if (res.ReturnCode == DialogResult.OK)
            {
                try
                {
                    Uri uri = new Uri(res.Text);
                }
                catch (Exception)
                {
                    return;
                }
                InsertMacroText(txtMessageText, selPos, "$DownloadData(" + res.Text + ")");
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageHTML.SelectionStart;
            SpecialTagDialog specialDialog = new SpecialTagDialog();
            if (specialDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageHTML, selPos, String.Format("${0}()", specialDialog.SelectedTag.ToString()));

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageText.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageText, selPos, String.Format("$Include({0})", openFileDialog1.FileName.ToString()));

        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageHTML.SelectionStart;
            RndDialog rndDialog = new RndDialog();
            if (rndDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageHTML, selPos, String.Format("$Rnd({0})", rndDialog.MacroText.ToString()));

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageHTML.SelectionStart;
            RotateDialog rotateDialog = new RotateDialog();
            if (rotateDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < rotateDialog.Phrases.Count; i++)
                {
                    sb.Append(rotateDialog.Phrases[i].ToString());
                    if (i != (rotateDialog.Phrases.Count - 1)) sb.Append("|");
                }
                InsertMacroText(txtMessageHTML, selPos, String.Format("$Rot({0})", sb.ToString()));
            }

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageHTML.SelectionStart;
            InputBoxResult res = InputBox.Show("Enter uri to retrieve:", "", "http://www.google.com");
            if (res.ReturnCode == DialogResult.OK)
            {
                try
                {
                    Uri uri = new Uri(res.Text);
                }
                catch (Exception)
                {
                    return;
                }
                InsertMacroText(txtMessageHTML, selPos, "$DownloadData(" + res.Text + ")");
            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageHTML.SelectionStart;
            TextToImageDialog txtimgDialog = new TextToImageDialog();
            if (txtimgDialog.ShowDialog() == DialogResult.OK)
            {
                InsertMacroText(txtMessageHTML, selPos, String.Format("$TextToImage({0})", txtimgDialog.MacroText));
            }

        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageHTML.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "Image Files (*.bmp;*.gif;*.jpg;*.jpeg)|*.bmp;*.gif;*.jpg;*.jpeg|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Campaign campaign = selectedCampaign();
                Message.Attachment attach = new Message.Attachment(openFileDialog1.FileName);
                attach.contentid = campaign.Rnd("<l10>");
                attach.Inline = true;
                attach.SkipFileData = false;
                campaign[_currentMessageId].EmbeddedImages.Add(attach);
                InsertMacroText(txtMessageHTML, selPos, "<IMG src=\"cid:" + attach.contentid + "\" alt=\"\">");
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            int selPos = txtSubjects.SelectionStart;
            SpecialTagDialog specialDialog = new SpecialTagDialog();
            if (specialDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtSubjects, selPos, String.Format("${0}()", specialDialog.SelectedTag.ToString()));

        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            int selPos = txtSubjects.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtSubjects, selPos, String.Format("$Include({0})", openFileDialog1.FileName.ToString()));

        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            int selPos = txtSubjects.SelectionStart;
            RndDialog rndDialog = new RndDialog();
            if (rndDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtSubjects, selPos, String.Format("$Rnd({0})", rndDialog.MacroText.ToString()));


        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            int selPos = txtSubjects.SelectionStart;
            RotateDialog rotateDialog = new RotateDialog();
            if (rotateDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < rotateDialog.Phrases.Count; i++)
                {
                    sb.Append(rotateDialog.Phrases[i].ToString());
                    if (i != (rotateDialog.Phrases.Count - 1)) sb.Append("|");
                }
                InsertMacroText(txtSubjects, selPos, String.Format("$Rot({0})", sb.ToString()));
            }

        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            int selPos = txtSubjects.SelectionStart;
            InputBoxResult res = InputBox.Show("Enter uri to retrieve:", "", "http://www.google.com");
            if (res.ReturnCode == DialogResult.OK)
            {
                try
                {
                    Uri uri = new Uri(res.Text);
                }
                catch (Exception)
                {
                    return;
                }
                InsertMacroText(txtSubjects, selPos, "$DownloadData(" + res.Text + ")");
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            int selPos = txtFromEmailPrefix.SelectionStart;
            SpecialTagDialog specialDialog = new SpecialTagDialog();
            if (specialDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtFromEmailPrefix, selPos, String.Format("${0}()", specialDialog.SelectedTag.ToString()));

        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            int selPos = txtFromAliases.SelectionStart;
            SpecialTagDialog specialDialog = new SpecialTagDialog();
            if (specialDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtFromAliases, selPos, String.Format("${0}()", specialDialog.SelectedTag.ToString()));

        }

        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            int selPos = txtAttachments.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";

            // TODO - Add attachments when the campaign is started.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtAttachments.Text += openFileDialog1.FileName + Environment.NewLine;
                Campaign campaign = selectedCampaign();
                campaign[_currentMessageId].Attachments.Clear();
                foreach (string fileName in txtAttachments.Lines)
                {
                    if (fileName != null && fileName.Length > 0)
                    {
                        if (File.Exists(fileName))
                        {
                            campaign[_currentMessageId].AddAttachment(new Message.Attachment(fileName.ToString()));
                        }
                    }
                }
            }
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            SpecialTagDialog specialDialog = new SpecialTagDialog();
            if (specialDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageSource, selPos, String.Format("${0}()", specialDialog.SelectedTag.ToString()));

        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageSource, selPos, String.Format("$Include({0})", openFileDialog1.FileName.ToString()));

        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            RndDialog rndDialog = new RndDialog();
            if (rndDialog.ShowDialog() == DialogResult.OK)
                InsertMacroText(txtMessageSource, selPos, String.Format("$Rnd({0})", rndDialog.MacroText.ToString()));

        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            RotateDialog rotateDialog = new RotateDialog();
            if (rotateDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < rotateDialog.Phrases.Count; i++)
                {
                    sb.Append(rotateDialog.Phrases[i].ToString());
                    if (i != (rotateDialog.Phrases.Count - 1)) sb.Append("|");
                }
                InsertMacroText(txtMessageSource, selPos, String.Format("$Rot({0})", sb.ToString()));
            }

        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            InputBoxResult res = InputBox.Show("Enter uri to retrieve:", "", "http://www.google.com");
            if (res.ReturnCode == DialogResult.OK)
            {
                try
                {
                    Uri uri = new Uri(res.Text);
                }
                catch (Exception)
                {
                    return;
                }
                InsertMacroText(txtMessageSource, selPos, "$DownloadData(" + res.Text + ")");
            }
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            TextToImageDialog txtimgDialog = new TextToImageDialog();
            if (txtimgDialog.ShowDialog() == DialogResult.OK)
            {
                InsertMacroText(txtMessageSource, selPos, String.Format("$TextToImage({0})", txtimgDialog.MacroText));
            }

        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            int selPos = txtMessageSource.SelectionStart;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "Image Files (*.bmp;*.gif;*.jpg;*.jpeg)|*.bmp;*.gif;*.jpg;*.jpeg|All files(*.*)|*.*";

            // TODO - Need to add appropriate headers for this embedded image.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Create an attachment from this generated image.
                Message.Attachment attach = new Message.Attachment(openFileDialog1.FileName);
                Campaign campaign = selectedCampaign();
                attach.contentid = campaign.Rnd("<l10>");
                attach.Inline = true;
                attach.SkipFileData = true;
                campaign[_currentMessageId].EmbeddedImages.Add(attach);
                InsertMacroText(txtMessageSource, selPos, "<img src='cid:" + attach.contentid + "' alt='' />");
            }

        }




        #endregion

        private void chkMessageSourceEdit_CheckedChanged(object sender, EventArgs e)
        {
            txtMessageSource.Enabled = chkMessageSourceEdit.Checked;
            btnRefreshSource.Enabled = chkMessageSourceEdit.Checked;

            selectedMessage().UseCustomSource = chkMessageSourceEdit.Checked;
        }

        private void btnRefreshSource_Click(object sender, EventArgs e)
        {
            Campaign campaign = selectedCampaign();
            Message message = selectedMessage();
            txtMessageSource.Text = campaign.BuildMessageSource(campaign.MessageList.IndexOf(message));
        }

        private void btnPreviewOutlook_Click(object sender, EventArgs e)
        {
            Campaign campaign = selectedCampaign();
            Message message = selectedMessage();

            string previewFile = ServiceScope.Get<IPathManager>().GetPath(@"<APPLICATION_ROOT>\") + "preview" + campaign.Name + ".eml";
            string msgSource = campaign.BuildMessageSource(campaign.MessageList.IndexOf(message));
            txtMessageSource.Text = msgSource != null ? msgSource : string.Empty;

            StreamWriter sr = new StreamWriter(previewFile);
            sr.Write(msgSource);
            sr.Close();
            sr.Dispose();
            Process.Start(previewFile);
        }

        private void txtAttachments_TextChanged(object sender, EventArgs e)
        {
            //if (_currentCampaignIndex < 0 || _currentCampaignIndex > _campaignCount)
            //    return;

            //if (_currentMessageIndex < 0 || _currentMessageIndex > _campaigns[_currentCampaignIndex].MessageList.Count)
            //    return;

            Campaign campaign = selectedCampaign();
            campaign[_currentMessageId].AttachmentList = txtAttachments.Lines;

            if (txtAttachments.Lines.Length > 0)
            {
                foreach (string attachment in txtAttachments.Lines)
                {
                    if (attachment != null && attachment.Length > 0)
                    {
                        FileInfo fi = new FileInfo(attachment);
                        if (fi.Exists && fi.Length > 0)
                            campaign[_currentMessageId].AddAttachment(new Message.Attachment(attachment));
                    }
                }
            }
        }

        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            //if (_currentCampaignIndex < 0 || _currentCampaignIndex > _campaignCount)
            //    return;

            //if (_currentMessageIndex < 0 || _currentMessageIndex > _campaigns[_currentCampaignIndex].MessageList.Count)
            //    return;

            Campaign campaign = selectedCampaign();
            string msgId = lblMessageID.Text.Split(new char[] { ':' })[1].Trim().ToString();
            campaign.RemoveMessage(msgId);

            ClearOptionListviews();
            ClearMessageOptions();
            DisplayCampaignSettings(campaign.ID);
            ShowListviewValues();
        }

        private void toTrayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (notifyIcon1 != null && !notifyIcon1.Visible)
            {
                notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_Normal);
                notifyIcon1.Text = "Konvict is running.";
                notifyIcon1.Visible = true;
                this.Visible = false;
            }
        }

        private void lockToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (notifyIcon1 != null && !notifyIcon1.Visible)
            {
                InputBoxResult res = InputBox.Show("Choose a password:", "Lock To Tray", "");

                if (res.ReturnCode == DialogResult.OK)
                {
                    if (res.Text != null && res.Text.Length > 0)
                    {
                        _hashTrayPwd = CryptoEx.ComputeHash(res.Text, "SHA1", null);

                        this.Visible = false;
                        notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_Locked);
                        notifyIcon1.Text = "Konvict is running.";
                        notifyIcon1.Visible = true;
                        notifyIcon1.ShowBalloonTip(10000, "Konvict", "Locked to tray.", ToolTipIcon.Info);
                    }
                }
            }
        }

        void notifyIcon1_Normal(object sender, MouseEventArgs e)
        {
            if (this.Visible == false)
            {
                notifyIcon1.Visible = false;
                this.Visible = true;
            }
        }

        void notifyIcon1_Locked(object sender, MouseEventArgs e)
        {
            if (this.Visible == false)
            {
                if (_hashTrayPwd != null && _hashTrayPwd.Length > 0)
                {
                    InputBoxResult res = InputBox.Show("Enter password:", Application.ProductName, "");
                    if (res.ReturnCode == DialogResult.OK)
                    {
                        if (CryptoEx.VerifyHash(res.Text, "SHA1", _hashTrayPwd))
                        {
                            _hashTrayPwd = null;
                            notifyIcon1.Visible = false;
                            this.Visible = true;
                        }
                        else
                            notifyIcon1.ShowBalloonTip(4000, "Konvict", "The password does not match!", ToolTipIcon.Warning);
                    }
                }
            }
        }

        private void minimizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void mnuCampaignStop_Click(object sender, EventArgs e)
        {
            if (lvCampaigns.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a campaign.", "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            List<Campaign> selected = new List<Campaign>();

            foreach (ListViewItem item in lvCampaigns.SelectedItems)
                selected.Add(mgr.GetCampaign(item.Text));

            mgr.StopCampaign(selected); 
        }

        private void mnuCampaignPause_Click(object sender, EventArgs e)
        {
            if (lvCampaigns.Items.Count == 0) return;

            foreach (ListViewItem lvItem in lvCampaigns.Items)
            {
                if (lvItem.Selected)
                {
                    try
                    {
                        throw new NotImplementedException("Pause/Resume not implemented.");
                        //_campaigns[GetCampaignIndex(lvItem.Text)].Pause();
                    }
                    catch (Exceptions.InvalidStateException exc)
                    {
                        MessageBox.Show("Invalid state:\n\n" + exc.Message.ToString());
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Unhandled exception:\n\n" + exc.Message.ToString());
                    }
                }
            }
        }

        #endregion

        #region OnCampaignStatsUpdate

        private void OnCampaignStatsUpdate(object sender, EventArgs e) //object sender, System.Timers.ElapsedEventArgs e
        {
            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            DataSet ds = mgr.GetCampaignsStatus(false);

            //lvCampaigns.BeginUpdate();

            foreach (DataRow dr in ds.Tables["Campaign"].Rows)
            {
                ListViewItem lvItem = lvCampaigns.FindItemWithText(dr["ID"].ToString());
                for (int k = 0; k < lvCampaigns.Columns.Count; k++)
                {
                    string key = lvCampaigns.Columns[k].Tag.ToString();
                    int idx = lvCampaigns.Columns[key].Index;
                    lvItem.SubItems[idx].Text = dr[key].ToString();
                }

                // Right now, just "Running" and "Idle" exist.
                // Make sure to add another group if adding status like "Complete"
                lvItem.Group = lvCampaigns.Groups[dr["Status"].ToString()];
            }

            //lvCampaigns.EndUpdate();

            // Retreive poll results

            List<PollItem> allPolls = mgr.GetPolls();
            List<Server> allServers = mgr.GetServers();

            foreach (PollItem poll in allPolls)
            {
                if (!poll.Running)
                    continue;

                Hashtable results = poll.Results;

                foreach (Server server in allServers)
                {
                    if (results.ContainsKey(server.ID))
                    {
                        PollEventArgs eventArgs = (PollEventArgs)results[server.ID];

                        if (eventArgs.StandardOutput == null || eventArgs.StandardOutput.Length == 0)
                        {
                            continue;
                        }

                        foreach (TabPage tabpage in tabsPolls.TabPages)
                        {
                            if (tabpage.Tag.ToString() == poll.ID.ToString())
                            {

                                TabControl serverTabs = (TabControl)tabpage.Controls[0];

                                foreach (TabPage serverPage in serverTabs.TabPages)
                                {
                                    if (serverPage.Tag.ToString() == server.ID)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        sb.Append(eventArgs.StandardOutput.Replace("\n", "\r\n"));
                                        TextBox textbox = (TextBox)serverPage.Controls[0];
                                        textbox.Text = sb.ToString();
                                    }
                                }
                            }
                        }

                        //Console.WriteLine("\n----------\nID:" + eventArgs.Id + " Host: " + eventArgs.Server.Host);
                    }
                }
            }


        }


        #endregion

        private void configureKLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            List<PollItem> allPolls = mgr.GetPolls();
            PollCollection tmpPollCol = new PollCollection();

            foreach (PollItem poll in allPolls)
                tmpPollCol.Add(poll);

            ConfigDialog configDialog = new ConfigDialog(tmpPollCol);
            if (configDialog.ShowDialog(this) == DialogResult.OK)
            {
                List<PollItem> polls = configDialog.PollCollection.Polls;


                //MessageBox.Show(configDialog.PollCollection.Polls.Count.ToString());

                configDialog.Close();
                configDialog.Dispose();
                configDialog = null;


                mgr.RemoveAllPoll();

                //MessageBox.Show(mgr.GetPolls().Count.ToString());

                //foreach (PollItem oldPoll in tmpPollCol.Polls)
                //    mgr.RemovePoll(oldPoll.ID);

                foreach (PollItem poll in polls)
                    mgr.AddPoll(poll);

                tabsPollsCreate();
            }
        }

        private void btnMasterServersAddFiles_Click(object sender, EventArgs e)
        {
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fn in openFileDialog1.FileNames)
                {
                    FileInfo fi = new FileInfo(fn);
                    if (!fi.Exists || fi.Length == 0)
                        continue;

                    FileStream fs = fi.Open(FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    while (!sr.EndOfStream)
                    {
                        string l = sr.ReadLine();
                        string[] fields = l.Split(new char[] { ':' });
                        if (fields.Length < 2)
                            continue;

                        Server server = new Server(fields[0], Convert.ToInt32(fields[1]));

                        if (fields.Length >= 3)
                            server.Username = fields[2];

                        if (fields.Length >= 4)
                            server.Password = fields[3];


                        ServiceScope.Get<ICampaignManager>().AddServer(server);

                        ListViewItem lvItem = new ListViewItem(server.Host);
                        lvItem.SubItems.Add(server.Port.ToString());
                        lvItem.SubItems.Add(server.Username);
                        lvItem.SubItems.Add(server.Password);
                        lvItem.Tag = server.ID.ToString();

                        lvMasterServers.Items.Add(lvItem);
                    }
                    sr.Close();
                    sr.Dispose();
                    fs.Close();
                    fs.Dispose();
                }
            }

        }

        private void btnMasterAddServer_Click(object sender, EventArgs e)
        {
            AddServerDialog addServer = new AddServerDialog();
            if (addServer.ShowDialog(this) == DialogResult.OK)
            {
                ServiceScope.Get<ICampaignManager>().AddServer(addServer.Server);

                ListViewItem lvItem = new ListViewItem(addServer.Server.Host);
                lvItem.SubItems.Add(addServer.Server.Port.ToString());
                lvItem.SubItems.Add(addServer.Server.Username);
                lvItem.SubItems.Add(addServer.Server.Password);
                lvItem.Tag = addServer.Server.ID.ToString();
                lvMasterServers.Items.Add(lvItem);
            }
        }

        private void btnMasterServersDelete_Click(object sender, EventArgs e)
        {
            if (lvMasterServers.Items.Count == 0) return;

            if (MessageBox.Show("You have selected " + lvMasterServers.SelectedItems.Count + " to be removed.\nDo you want to continue?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            lvMasterServers.BeginUpdate();

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();

            foreach (ListViewItem lvItem in lvMasterServers.SelectedItems)
            {
                for (int i = 0; i < mgr.GetServers().Count; )
                {
                    if (string.Equals(mgr.GetServers()[i].ID.ToString(), lvItem.Tag.ToString()))
                    {
                        mgr.RemoveServer(mgr.GetServers()[i].ID);
                        lvItem.Remove();
                    }
                    else
                        i++;
                }
            }


            lvMasterServers.EndUpdate();
        }

        private void btnMasterServersRemoveAll_Click(object sender, EventArgs e)
        {
            if (lvMasterServers.Items.Count == 0) 
                return;

            if (MessageBox.Show("Are you sure?", "Remove All", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            List<Server> allServers = mgr.GetServers();

            foreach (Server server in allServers)
                mgr.RemoveServer(server.ID);

            lvMasterServers.Items.Clear();
        }

        private void btnInheritServers_Click(object sender, EventArgs e)
        {
            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            List<Server> allServers = mgr.GetServers();

            foreach (Server server in allServers)
                AddServer(server);
        }

        private void lnkSchedulerStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CampaignScheduleDialog campaignSchedule = new CampaignScheduleDialog();

                if (campaignSchedule.ShowDialog(this) == DialogResult.OK)
                {
                    selectedCampaign().Occurrence = campaignSchedule.Occurrence;
                    selectedCampaign().Schedule = campaignSchedule.Schedule;
                    selectedCampaign().Expiration = campaignSchedule.Expires;
                    selectedCampaign().EnableSchedule = true;

                    DisplayCampaignSettings(selectedCampaign().ID);
                }
            }
            else
            {
                selectedCampaign().EnableSchedule = !(selectedCampaign().EnableSchedule);
                DisplayCampaignSettings(selectedCampaign().ID);
            }
        }

        private void lnkSelectSupression_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Filter = "All Text Files (*.txt;*.csv;*.tdf)|*.txt;*.csv;*.tdf|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);
                if (!fileInfo.Exists || fileInfo.Length == 0)
                    return;

                Campaign campaign = selectedCampaign();
                campaign.SupressionFile = openFileDialog1.FileName;
                txtSupressionFileName.Text = openFileDialog1.FileName;
                chkSupression.Checked = true;
            }

        }

        private void chkSupression_CheckedChanged(object sender, EventArgs e)
        {
            Campaign campaign = selectedCampaign();
            campaign.Suppression = chkSupression.Checked;
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void startPollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //ILogger logger = ServiceScope.Get<ILogger>();

            //using (Campaign campaign = new Campaign("benchmark"))
            //{
            //    string mixed100 = "<m100>";
            //    string lower100 = "<l100>";


            //    string result = string.Empty;

            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < 100; i++)
            //    {
            //        result = campaign.Rnd(mixed100);
            //    }
            //    stopwatch.Stop();

            //    logger.Debug("m100 - 100 iterations : Elapsed: {0} ticks, {1} milliseconds", stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);


            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        result = campaign.Rnd(mixed100);
            //    }
            //    stopwatch.Stop();

            //    logger.Debug("m100 - 1000 iterations : Elapsed: {0} ticks, {1} milliseconds", stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);


            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < 50000; i++)
            //    {
            //        result = campaign.Rnd(mixed100);
            //    }
            //    stopwatch.Stop();

            //    logger.Debug("m100 - 50000 iterations : Elapsed: {0} ticks, {1} milliseconds", stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);



            //    // <l100>

            //    stopwatch.Reset();
            //    stopwatch.Start();
            //    for (int i = 0; i < 50000; i++)
            //    {
            //        result = campaign.Rnd(lower100);
            //    }
            //    stopwatch.Stop();

            //    logger.Debug("l100 - 50000 iterations : Elapsed: {0} ticks, {1} milliseconds", stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds);

            //}

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            bool polling = startPollingToolStripMenuItem.Checked;

            if (!polling)
            {
                //System.Threading.Thread thread = new System.Threading.Thread(mgr.StartAllPoll);
                //thread.IsBackground = true;
                //thread.Start();

                mgr.StartAllPoll();
                polling = !polling;
            }
            else
            {
                mgr.StopAllPoll();
                polling = !polling;
            }
            startPollingToolStripMenuItem.Checked = polling;
        }

        private void extentionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainFormClone mainClone = new MainFormClone();
            mainClone.Show();
        }


        private Campaign selectedCampaign()
        {
            if (string.IsNullOrEmpty(_currentCampaignId))
                return new Campaign("DummyCampaign");

            ICampaignManager mgr = ServiceScope.Get<ICampaignManager>();
            Campaign campaign = mgr.GetCampaign(_currentCampaignId);
            return campaign;
        }

        private Message selectedMessage()
        {
            Campaign campaign = selectedCampaign();

            if (string.IsNullOrEmpty(_currentMessageId))
                return new Message("DummyMessage");

            int index = campaign.GetMessageIndex(_currentMessageId);
            if (index < 0)
                throw new ArgumentOutOfRangeException("_currentMessageId");

            return campaign[index];
        }

        #region method DisplayRedirects

        private void DisplayRedirects()
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            if (coreSettings.MngtDbHost == null || coreSettings.MngtDbHost.Length == 0)
                throw new ArgumentNullException("MngtDbHost");

            if (coreSettings.MngtDbPort < 1 || coreSettings.MngtDbPort > 0xFFFF)
                throw new ArgumentNullException("MngtDbPort");

            if (coreSettings.MngtDbUser == null || coreSettings.MngtDbUser.Length == 0)
                throw new ArgumentNullException("MngtDbUser");

            if (coreSettings.MngtDbName == null || coreSettings.MngtDbName.Length == 0)
                throw new ArgumentNullException("MngtDbName");

            if (coreSettings.MngtConnectionString == null || coreSettings.MngtConnectionString.Length == 0)
                throw new ArgumentNullException("MngtConnectionString");


            CampaignMngtDb campaignMngt;
            Exception ex = null;
            DataView dv = null;

            try
            {
                campaignMngt = new CampaignMngtDb(coreSettings.MngtConnectionString);
                dv = campaignMngt.GetFolders();
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                if (ex == null && dv != null)
                {
                    DatagridDialog dgDialog = new DatagridDialog("Redirects", dv);
                    dgDialog.StartPosition = FormStartPosition.CenterParent;
                    dgDialog.ShowDialog(this);
                }
            }
        }

        #endregion

        #region method DisplayHits

        private void DisplayHits(string campaignId)
        {
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            if (coreSettings.MngtDbHost == null || coreSettings.MngtDbHost.Length == 0)
                throw new ArgumentNullException("MngtDbHost");

            if (coreSettings.MngtDbPort < 1 || coreSettings.MngtDbPort > 0xFFFF)
                throw new ArgumentNullException("MngtDbPort");

            if (coreSettings.MngtDbUser == null || coreSettings.MngtDbUser.Length == 0)
                throw new ArgumentNullException("MngtDbUser");

            if (coreSettings.MngtDbName == null || coreSettings.MngtDbName.Length == 0)
                throw new ArgumentNullException("MngtDbName");

            if (coreSettings.MngtConnectionString == null || coreSettings.MngtConnectionString.Length == 0)
                throw new ArgumentNullException("MngtConnectionString");

            CampaignMngtDb campaignMngt;
            Exception ex = null;
            DataView dv = null;

            try
            {
                campaignMngt = new CampaignMngtDb(coreSettings.MngtConnectionString);
                dv = campaignMngt.GetCampaignHits(campaignId);
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                if (ex == null && dv != null)
                {
                    string name = ServiceScope.Get<ICampaignManager>().GetCampaign(campaignId).Name;
                    string description = "Campaign Name: " + name + "; ID: '" + campaignId + "'.";
                    DatagridDialog dgDialog = new DatagridDialog("Hits", dv, description);
                    dgDialog.StartPosition = FormStartPosition.CenterParent;
                    dgDialog.Show();
                }
            }
        }

        #endregion

        #region method DisplayOpens

        private void DisplayOpens(string campaignId)
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            if (coreSettings.MngtDbHost == null || coreSettings.MngtDbHost.Length == 0)
                throw new ArgumentNullException("MngtDbHost");

            if (coreSettings.MngtDbPort < 1 || coreSettings.MngtDbPort > 0xFFFF)
                throw new ArgumentNullException("MngtDbPort");

            if (coreSettings.MngtDbUser == null || coreSettings.MngtDbUser.Length == 0)
                throw new ArgumentNullException("MngtDbUser");

            if (coreSettings.MngtDbName == null || coreSettings.MngtDbName.Length == 0)
                throw new ArgumentNullException("MngtDbName");

            if (coreSettings.MngtConnectionString == null || coreSettings.MngtConnectionString.Length == 0)
                throw new ArgumentNullException("MngtConnectionString");


            CampaignMngtDb campaignMngt;
            Exception ex = null;
            DataView dv = null;

            try
            {
                campaignMngt = new CampaignMngtDb(coreSettings.MngtConnectionString);
                dv = campaignMngt.GetCampaignOpens(campaignId);
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                if (ex == null && dv != null)
                {
                    string name = ServiceScope.Get<ICampaignManager>().GetCampaign(campaignId).Name;
                    string description = "Campaign Name: " + name + "; ID: '" + campaignId + "'.";
                    DatagridDialog dgDialog = new DatagridDialog("Opens", dv, description);
                    dgDialog.StartPosition = FormStartPosition.CenterParent;
                    dgDialog.Show();
                }
            }
        }

        #endregion



        #region cntxmnuCampaignRoot methods


        private void mnuCreateDatabaseEntry_Click(object sender, EventArgs e)
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();

            CoreSettings coreSettings = new CoreSettings();
            
            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            string message = string.Empty;

            if (string.IsNullOrEmpty(coreSettings.MngtDbHost) || string.IsNullOrEmpty(coreSettings.MngtDbName) || string.IsNullOrEmpty(coreSettings.MngtDbUser))
            {
                message = "Invalid database settings !";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Campaign campaign = selectedCampaign();

            if (campaign == null)
            {
                return;
            }

            message = string.Format("Campaign Name: {0}\nCampaign ID: {1}\n\nAre you sure you would like to add the database entry for this campaign?", campaign.Name, campaign.ID);
            if (MessageBox.Show(message, "Konvict", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            CampaignMngtDb campaignMngt;

            message = "Would you like to view the existing entries in the database?";
            DialogResult result = MessageBox.Show(message, "Konvict", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            
            if (result == DialogResult.Cancel)
                return;
            else if (result == DialogResult.Yes)
            {
                try
                {
                    DisplayRedirects();
                }
                catch
                {
                }
            }

            InputBoxResult inputResult = InputBox.Show("Please enter the URL you would like to redirect to:", "Konvict", "http://www.");
            
            if (inputResult.ReturnCode != DialogResult.OK || string.IsNullOrEmpty(inputResult.Text))
                return;

            string redirectTo = inputResult.Text.Trim();

            Exception ex = null;
            try
            {
                Uri uri = new Uri(redirectTo);
            }
            catch (Exception x)
            {
                ex = x;
            }

            if (ex != null)
            {
                message = "The URI '" + redirectTo + "' is invalid!";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                campaignMngt = new CampaignMngtDb(coreSettings.MngtConnectionString);
                int id  = campaignMngt.AddRedirect(campaign.ID, redirectTo);
                if (id == -1)
                    MessageBox.Show("Could not create entry!", "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
            }
        }

        private void mnuDeleteDatabaseEntry_Click(object sender, EventArgs e)
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();

            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            string message = string.Empty;

            if (string.IsNullOrEmpty(coreSettings.MngtDbHost) || string.IsNullOrEmpty(coreSettings.MngtDbName) || string.IsNullOrEmpty(coreSettings.MngtDbUser))
            {
                message = "Invalid database settings !";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Campaign campaign = selectedCampaign();

            if (campaign == null)
            {
                return;
            }

            message = string.Format("Campaign Name: {0}\nCampaign ID: {1}\n\nAre you sure you would like to delete the database entry for this campaign?", campaign.Name, campaign.ID);
            if (MessageBox.Show(message, "Konvict", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }


            string connString = string.Empty;

            connString += "connectionstring=";
            connString += "server = ";
            connString += coreSettings.MngtDbHost;
            connString += ";port = ";
            connString += coreSettings.MngtDbPort.ToString();
            connString += ";database = ";
            connString += coreSettings.MngtDbName;
            connString += ";user id=";
            connString += coreSettings.MngtDbUser;
            connString += ";password=";
            connString += coreSettings.MngtDbPassword;
            connString += ";";


            try
            {
                CampaignMngtDb campaignMngt = new CampaignMngtDb(connString);

                int result;
                campaignMngt.DeleteRedirect(campaign.ID, out result);
            }
            catch
            {
            }
        }

        private void mnuViewAllEntries_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayRedirects();
            }
            catch
            {
            }

        }

        private void mnuUpdateRedirectUrl_Click(object sender, EventArgs e)
        {
            ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();

            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            string message = string.Empty;

            if (string.IsNullOrEmpty(coreSettings.MngtDbHost) || string.IsNullOrEmpty(coreSettings.MngtDbName) || string.IsNullOrEmpty(coreSettings.MngtDbUser))
            {
                message = "Invalid database settings !";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Campaign campaign = selectedCampaign();

            if (campaign == null)
            {
                return;
            }


            InputBoxResult inputResult = InputBox.Show("Please enter the URL you would like to redirect to:", "Konvict", "http://www.");

            if (inputResult.ReturnCode != DialogResult.OK || string.IsNullOrEmpty(inputResult.Text))
                return;


            string redirectTo = inputResult.Text.Trim();

            Exception ex = null;
            try
            {
                Uri uri = new Uri(redirectTo);
            }
            catch (Exception x)
            {
                ex = x;
            }

            if (ex != null)
            {
                message = "The URI '" + redirectTo + "' is invalid!";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                CampaignMngtDb campaignMngt = new CampaignMngtDb(coreSettings.MngtConnectionString);
                campaignMngt.UpdateRedirectTo(campaign.ID, redirectTo);
            }
            catch
            {
            }
        }

        private void mnuViewHits_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCampaign() != null && selectedCampaign().ID != null && selectedCampaign().ID.Length > 0)
                {
                    DisplayHits(selectedCampaign().ID);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Could not retreive hits from the specified campaign.\n\nMessage: " + x.Message,"Konvict",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void mnuViewOpens_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedCampaign() != null && selectedCampaign().ID != null && selectedCampaign().ID.Length > 0)
                {
                    DisplayOpens(selectedCampaign().ID);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Could not retreive opens from the specified campaign.\n\nMessage: " + x.Message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void tvwNavi_MouseUp(object sender, MouseEventArgs e)
        {
                Point point = new Point(e.X, e.Y);

                TreeNode node = tvwNavi.GetNodeAt(point);

                if (node != null)
                {
                    _oldSelectNode = node;
                    tvwNavi.SelectedNode = node;

                    string tagString = Convert.ToString(_oldSelectNode.Tag);

                    if (tagString.Contains("CampaignSettings"))
                    {
                        string campaignname = tagString.Split(new char[] { '\t' })[1];
                        _currentCampaignId = ServiceScope.Get<ICampaignManager>().GetCampaignByName(campaignname).ID;
                        lblCampaignName.Text = campaignname;

                        // Clear then populate GUI controls.
                        ClearOptionListviews();
                        ClearMessageOptions();
                        DisplayCampaignSettings(selectedCampaign().ID);
                        ShowListviewValues();

                        // Bring the panel to the front and select the subcategory
                        pnlCampaignSettings.Dock = DockStyle.Fill;
                        pnlCampaignSettings.BringToFront();

                        if (_oldSelectNode.Nodes.Count == 0)
                            tabControl1.SelectTab(_oldSelectNode.Text);

                        // Display menu for campaign root
                        if (e.Button == MouseButtons.Right)
                        {
                            if (node.Nodes.Count > 0)
                                cntxmnuCampaignRoot.Show(tvwNavi, point);
                        }

                    }
                    else
                    {
                        // Display other panels
                        foreach (Panel panel in GetControls<Panel>(this))
                        {
                            if (panel.Tag != null && (panel.Tag == _oldSelectNode.Tag))
                            {
                                panel.Dock = DockStyle.Fill;
                                panel.BringToFront();
                                break;
                            }
                        }
                    }
                }
        }

        private void mnuRefreshClickThrough_Click(object sender, EventArgs e)
        {
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();
            ICampaignManager campaignMgr = ServiceScope.Get<ICampaignManager>();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            string message = string.Empty;

            if (string.IsNullOrEmpty(coreSettings.MngtDbHost) || string.IsNullOrEmpty(coreSettings.MngtDbName) || string.IsNullOrEmpty(coreSettings.MngtDbUser))
            {
                message = "Invalid database settings !";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvCampaigns.SelectedItems.Count == 0)
            {
                message = "Please select a campaign.";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<Campaign> selected = new List<Campaign>();

            foreach (ListViewItem item in lvCampaigns.SelectedItems)
                selected.Add(campaignMgr.GetCampaign(item.Text));


            CampaignMngtDb mngt = new CampaignMngtDb(coreSettings.MngtConnectionString);

            foreach (Campaign campaign in selected)
            {
                campaign._campaignStats.TrackingClickThrough = mngt.GetCampaignHitsCount(campaign.ID);
            }
        }

        private void mnuRefreshOpens_Click(object sender, EventArgs e)
        {
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();
            ICampaignManager campaignMgr = ServiceScope.Get<ICampaignManager>();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }

            if (coreSettings == null)
                coreSettings = new CoreSettings();


            string message = string.Empty;

            if (string.IsNullOrEmpty(coreSettings.MngtDbHost) || string.IsNullOrEmpty(coreSettings.MngtDbName) || string.IsNullOrEmpty(coreSettings.MngtDbUser))
            {
                message = "Invalid database settings !";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvCampaigns.SelectedItems.Count == 0)
            {
                message = "Please select a campaign.";
                MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<Campaign> selected = new List<Campaign>();

            foreach (ListViewItem item in lvCampaigns.SelectedItems)
                selected.Add(campaignMgr.GetCampaign(item.Text));


            CampaignMngtDb mngt = new CampaignMngtDb(coreSettings.MngtConnectionString);

            foreach (Campaign campaign in selected)
            {
                campaign._campaignStats.TrackingOpens = mngt.GetCampaignOpensCount(campaign.ID);
            }
        }

        private delegate byte[] DownloadDataDelegate(Uri address);

        private delegate int AddRedirectDelegate(string folderName, string redirectTo);

        private void convertMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Campaign campaign = selectedCampaign();
            Message message = selectedMessage();

            if (message.Name == "DummyMessage")
                return;

            string html = message.MessageHtml;

            Regex reHref = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex reImg = new Regex(@"<img\s[^>]*>", RegexOptions.IgnoreCase);
            Regex reSrc = new Regex(@"src=(?:(['""])(?<src>(?:(?!\1).)*)\1|(?<src>[^\s>]+))", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Dictionary<string, string> images = new Dictionary<string, string>();
            Dictionary<string, string> links = new Dictionary<string, string>();

            DataSet ds = new DataSet("dsConvertMessage");
            ds.Tables.Add("Redirects");
            ds.Tables["Redirects"].Columns.Add("Type");
            ds.Tables["Redirects"].Columns.Add("ID");
            ds.Tables["Redirects"].Columns.Add("Target");

            foreach (Match imgmatch in reImg.Matches(html))
            {
                if (reSrc.IsMatch(imgmatch.Groups[0].ToString()))
                {
                    Match mSrc = reSrc.Match(imgmatch.Groups[0].ToString());
                    ServiceScope.Get<ILogger>().Debug("src is: {0}", mSrc.Groups["src"].ToString());
                    string imguid = campaign.ID + RandomEx.RandomString(2, 3, true, false) + RandomEx.RandomInt(1, 9);
                    images.Add(imguid, mSrc.Groups["src"].ToString());

                    DataRow dr = ds.Tables["Redirects"].NewRow();
                    dr["Type"] = "Image";
                    dr["ID"] = imguid;
                    dr["Target"] = mSrc.Groups["src"].ToString();
                    ds.Tables["Redirects"].Rows.Add(dr);
                }
            }

            foreach (Match hrefmatch in reHref.Matches(html))
            {
                ServiceScope.Get<ILogger>().Debug("   href is: {0}", hrefmatch.Groups["url"].ToString());
                string linkuid = campaign.ID + RandomEx.RandomString(2, 5, true, false);
                links.Add(linkuid, hrefmatch.Groups["url"].ToString());

                DataRow dr = ds.Tables["Redirects"].NewRow();
                dr["Type"] = "Link";
                dr["ID"] = linkuid;
                dr["Target"] = hrefmatch.Groups["url"].ToString();
                ds.Tables["Redirects"].Rows.Add(dr);
            }

            if (DialogResult.Yes == MessageBox.Show("View the items to be processed?", "Convert campaign", MessageBoxButtons.YesNo))
            {
                DatagridDialog dd = new DatagridDialog("Campaign Converter", ds.Tables["Redirects"].DefaultView);
                dd.StartPosition = FormStartPosition.CenterParent;
                dd.ShowDialog();
            }

            if (DialogResult.OK == MessageBox.Show("Continue?", "Convert campaign", MessageBoxButtons.OK,MessageBoxIcon.Question))
            {
                string imageDir = ServiceScope.Get<IPathManager>().GetPath(@"<CAMPAIGN_ROOT>\") + campaign.Name + Path.DirectorySeparatorChar + "Image" + Path.DirectorySeparatorChar;
                string errorDlg = "";

                if (!Directory.Exists(imageDir)) Directory.CreateDirectory(imageDir);

                //--- read settings ---------------------------------------------------------------------------------------------
                ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
                CoreSettings coreSettings = new CoreSettings();

                try
                {
                    coreSettings = mgr.Load<CoreSettings>();
                }
                catch { }

                if (coreSettings == null)
                    coreSettings = new CoreSettings();


                CampaignMngtDb db = new CampaignMngtDb(coreSettings.MngtConnectionString);


                //--- grab and insert images ------------------------------------------------------------------------------------
                foreach (KeyValuePair<string, string> kvp in images)
                {
                    try
                    {
                        Uri address = new Uri(kvp.Value);
                        string filename = Path.Combine(imageDir, kvp.Key + Path.GetExtension(address.LocalPath));
                        //System.Net.WebClient wc = new System.Net.WebClient();
                        byte[] buffer; //= wc.DownloadData(address);

                        DownloadDataDelegate del = new System.Net.WebClient().DownloadData;
                        IAsyncResult ar = del.BeginInvoke(address, null, null);
                        buffer = del.EndInvoke(ar); // asyncOP later.

                        if (buffer.Length > 0)
                        {
                            if (File.Exists(filename)) File.Delete(filename);
                            File.WriteAllBytes(filename, buffer);

                            if (0 > db.AddRedirect(kvp.Key, "/img/" + kvp.Key + Path.GetExtension(address.LocalPath)))
                                errorDlg += "Error adding redirect '" + kvp.Value + "' to db.\r\n";
                            else
                                ServiceScope.Get<ILogger>().Info("Redirect added to db. key: {0}, value: {1}", kvp.Key, kvp.Value);
                        }
                        else
                            errorDlg += "Downloading of image " + kvp.Value + " was unsuccessful\r\n";
                    }
                    catch
                    {
                        errorDlg += "Downloading of image " + kvp.Value + " was unsuccessful\r\n";
                    }
                }

                if (errorDlg != null && errorDlg.Length > 0)
                    if (DialogResult.OK != MessageBox.Show(errorDlg + "\r\nWould you like to continue?", "Error report"))
                        return;
                errorDlg = "";



                //--- insert links ----------------------------------------------------------------------------------------------
                foreach (KeyValuePair<string, string> kvp in links)
                {
                    if (0 > db.AddRedirect(kvp.Key, kvp.Value))
                        errorDlg += "Error adding redirect '" + kvp.Value + "' to db.\r\n";
                    else
                        ServiceScope.Get<ILogger>().Info("Redirect added to db. key: {0}, value: {1}", kvp.Key, kvp.Value);
                }

                if (errorDlg != null && errorDlg.Length > 0)
                    if (DialogResult.OK != MessageBox.Show(errorDlg + "\r\nYou must enter these manually!\r\n", "Error report", MessageBoxButtons.OKCancel))
                        return;


                //--- scp over the images ---------------------------------------------------------------------------------------
                if ((coreSettings.Scpclient != null && coreSettings.Scpclient.Length > 0) && File.Exists(coreSettings.Scpclient))
                {
                    if ((coreSettings.KeyFile != null && coreSettings.KeyFile.Length > 0) && File.Exists(coreSettings.KeyFile))
                    {
                        string srcImg = imageDir + "*";
                        string destImg = "/var/www/html/cmp/img/";
                        string args = string.Format("-scp -P {0} -i \"{1}\" {2} root@{3}:{4}",
                            coreSettings.Sshport, coreSettings.KeyFile, srcImg, coreSettings.MngtDbHost, destImg);

                        ProcessStartInfo startInfo = new ProcessStartInfo(coreSettings.Scpclient, args);
                        startInfo.ErrorDialog = false;
                        startInfo.UseShellExecute = false;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.RedirectStandardInput = true;
                        startInfo.RedirectStandardError = true;
                        startInfo.CreateNoWindow = true;

                        Process process = new Process();
                        process.StartInfo = startInfo;
                        process.Start();

                        if (!process.WaitForExit(new TimeSpan(0, 0, 60).Milliseconds))
                        {
                            // true if the associated process has exited; otherwise, false.
                            process.Kill();
                            process.Dispose();

                            ServiceScope.Get<ILogger>().Info("ConvertMessage :: Process(scp) was killed, maximum wait time reached.");
                            MessageBox.Show("There was an unexpected error with scp.\r\nThe process was killed, leaving...");

                            return;
                        }

                        process.Close();
                        process.Dispose();


                        //--- alter message -------------------------------------------------------------------------------------
                        InputBoxResult result;
                        do
                        {
                            result = InputBox.Show("Enter the domain you wish to use:\r\n[fromdomain]\r\nmydomain.com\r\n$FromDomain()", "Enter a domain", "http://");

                        } while (result.ReturnCode == DialogResult.OK && result.Text.TrimToNull() == null);


                        string domain = result.Text;
                        string trackBase = "/[user_id]/[camp_id]";
                        string trackOptional = "/[list_id]/[msg_id]/[subj_id]";

                        /* [user_id]/[camp_id]/[list_id]/[msg_id]/[subj_id]/type[o,c,r,i]
                            -------------------------
                            subtypes
                            -------------------------
                            o	log as open
                            c	log as hit (default)
                            r	remove user
                            i	display open image
                            s   display the redirect as an image
                        */

                        foreach (KeyValuePair<string, string> kvp in links)
                            html = html.Replace(kvp.Value, domain + trackBase.Replace("[camp_id]", kvp.Key) + trackOptional);

                        foreach (KeyValuePair<string, string> kvp in images)
                            html = html.Replace(kvp.Value, domain + trackBase.Replace("[camp_id]", kvp.Key) + trackOptional + "/s");

                        message.MessageHtml = html;
                        this.ShowMessageValues();
                        this.SaveCampaign(campaign.ID);
                    }
                }
            }
        }







    }
}
