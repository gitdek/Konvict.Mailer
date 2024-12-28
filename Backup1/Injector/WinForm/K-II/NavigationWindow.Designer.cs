namespace Injector
{
    partial class NavigationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationWindow));
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tvwNavi = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
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
            // tvwNavi
            // 
            this.tvwNavi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwNavi.ImageIndex = 0;
            this.tvwNavi.ImageList = this.imageList1;
            this.tvwNavi.Location = new System.Drawing.Point(0, 0);
            this.tvwNavi.Name = "tvwNavi";
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
            this.tvwNavi.Size = new System.Drawing.Size(350, 443);
            this.tvwNavi.TabIndex = 6;
            // 
            // NavigationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 443);
            this.Controls.Add(this.tvwNavi);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.HideOnClose = true;
            this.Name = "NavigationWindow";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.TabText = "Navigation";
            this.Text = "Navigation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView tvwNavi;


    }
}