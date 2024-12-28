using System;
using System.Windows.Forms;
using Injector.Core.Logging;
using System.Collections.Generic;
using Injector.Core.Settings;

namespace Injector
{
    public partial class ConfigDialog : Form
    {
        private PollCollection _polls;
        private string _privateKeyFile;
        private string _sshClient;
        private string _scpClient;
        private int _sshPort;
        private string _currentEditId;

        private string _mngtDbHost;
        private int _mngtDbPort;
        private string _mngtDbUser;
        private string _mngtDbPassword;
        private string _mngtDbName;

        public ConfigDialog()
            :this(null)
        {
        }

        public ConfigDialog(PollCollection pollCollection)
        {
            InitializeComponent();

            if (pollCollection != null && pollCollection.Polls.Count > 0)
            {
                _polls = pollCollection;

                foreach (PollItem pollItem in _polls.Polls)
                {
                    lstPolls.Items.Add(pollItem.ID);
                    lstPolls.SetItemChecked(lstPolls.Items.IndexOf(pollItem.ID), pollItem.Enabled);
                }
            }
            else
                _polls = new PollCollection();


            cboExportAction.SelectedIndex = 0;

            //ILogger logger = ServiceScope.Get<ILogger>(false);
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }


            if (coreSettings == null)
                coreSettings = new CoreSettings();


            _privateKeyFile = coreSettings.KeyFile;
            _sshClient = coreSettings.Sshclient;
            _scpClient = coreSettings.Scpclient;
            _sshPort = (coreSettings.Sshport > 0) ? coreSettings.Sshport : 22;


            _mngtDbHost = coreSettings.MngtDbHost;
            _mngtDbPort = coreSettings.MngtDbPort;
            _mngtDbUser = coreSettings.MngtDbUser;
            _mngtDbPassword = coreSettings.MngtDbPassword;
            _mngtDbName = coreSettings.MngtDbName;

            txtDbHost.Text = _mngtDbHost;
            txtDbPort.Text = _mngtDbPort.ToString();
            txtDbUser.Text = _mngtDbUser;
            txtDbPassword.Text = _mngtDbPassword;
            txtDbName.Text = _mngtDbName;

            lblPrivKey.Text = _privateKeyFile;
            lblSSHClient.Text = _sshClient;
            lblSCPClient.Text = _scpClient;
            txtPort.Text = _sshPort.ToString();
        //    txtUploadPath.Text = (!string.IsNullOrEmpty(settings.Master.KLocalUploadPath)) ? settings.Master.KLocalUploadPath : SettingsEx.Default.Master.KLocalUploadPath;
            txtUploadPath.Text = (!string.IsNullOrEmpty(coreSettings.RemoteKlocalPath)) ? coreSettings.RemoteKlocalPath : "/etc/klocal";
        
        }

        private void lnkSelectPubKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "PuTTy Private Key File (*.ppk)|*.ppk|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _privateKeyFile = openFileDialog1.FileName;
                lblPrivKey.Text = openFileDialog1.FileName;
            }
        }

        private void btnAddPoll_Click(object sender, EventArgs e)
        {
            uint interval;
            if (!uint.TryParse(txtPollInterval.Text, out interval) || interval <= 0 )
                return;

            string command = txtPollCommand.Text;
            if (string.IsNullOrEmpty(command))
                return;

            
            PollExportAction_enum exportAction = (PollExportAction_enum) cboExportAction.SelectedIndex;

            //PollItem pollItem = new PollItem(command, interval, exportAction, Guid.NewGuid().ToString());
            //_polls.Add(pollItem);

            PollItem pollItem = _polls.Add(command, interval, exportAction);

            ILogger logger = ServiceScope.Get<ILogger>(false);

            logger.Debug("ConfigDialog : PollCollection Count: {0}", _polls.Polls.Count);

            lstPolls.Items.Add(pollItem.ID);
            lstPolls.SetItemChecked(lstPolls.Items.IndexOf(pollItem.ID), pollItem.Enabled);
        }

        private void editPoll_OnClick(object sender, EventArgs e)
        {
            PollItem pollItem = _polls[_currentEditId];
            pollItem.Command = txtPollCommand.Text;
            pollItem.ExportAction = (PollExportAction_enum)cboExportAction.SelectedIndex;
            pollItem.Interval = Convert.ToUInt32(txtPollInterval.Text);

            btnAddPoll.Click += new EventHandler(btnAddPoll_Click);
            btnAddPoll.Click -= editPoll_OnClick;

            _currentEditId = "";
            btnAddPoll.Text = "Add";
            btnEditCancel.Visible = false;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPolls.SelectedIndex == -1)
                return;

            string id = lstPolls.Items[lstPolls.SelectedIndex].ToString();

            PollItem pollItem = _polls[id.ToString()];

            if (id != null && id.Length > 0)
            {
                _polls.Remove(pollItem);
                
                lstPolls.Items.Remove(id);
            }

        }

        private void lstPolls_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string id = lstPolls.Items[e.Index].ToString();
            if (id != null && id.Length > 0)
                _polls[id].Enabled = e.NewValue != CheckState.Checked ? false : true;

        }

        private void removeAllFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _polls = new PollCollection();
            lstPolls.Items.Clear();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPolls.SelectedIndex == -1)
                return;

            string id = lstPolls.Items[lstPolls.SelectedIndex].ToString();
            if (id != null && id.Length > 0)
            {
                PollItem pollItem = _polls[id];

                txtPollCommand.Text = pollItem.Command;
                txtPollInterval.Text = pollItem.Interval.ToString();
                cboExportAction.SelectedIndex = (int)pollItem.ExportAction;

                _currentEditId = pollItem.ID;

                btnEditCancel.Visible = true;
                btnAddPoll.Text = "Done";

                btnAddPoll.Click -= btnAddPoll_Click;
                btnAddPoll.Click += new EventHandler(editPoll_OnClick);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            Console.WriteLine(_polls.Polls.Count);

            ILogger logger = ServiceScope.Get<ILogger>();
            ISettingsManager mgr = ServiceScope.Get<ISettingsManager>();
            CoreSettings coreSettings = new CoreSettings();

            try
            {
                coreSettings = mgr.Load<CoreSettings>();
            }
            catch { }


            if (coreSettings == null)
                coreSettings = new CoreSettings();

            if (!int.TryParse(txtPort.Text, out _sshPort))
                _sshPort = coreSettings.Sshport;

            coreSettings.Sshport = _sshPort;
            coreSettings.Sshclient = _sshClient;
            coreSettings.Scpclient = _scpClient;
            coreSettings.KeyFile = _privateKeyFile;
            coreSettings.RemoteKlocalPath = txtUploadPath.Text;

            if (!int.TryParse(txtDbPort.Text, out _mngtDbPort))
                _mngtDbPort = coreSettings.MngtDbPort;

            coreSettings.MngtDbHost = txtDbHost.Text;
            coreSettings.MngtDbPort = _mngtDbPort;
            coreSettings.MngtDbUser = txtDbUser.Text;
            coreSettings.MngtDbPassword = txtDbPassword.Text;
            coreSettings.MngtDbName = txtDbName.Text;

            mgr.Save(coreSettings);

            this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        private void lnkSelectSSHClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Supported SSH Clients (plink.exe;ssh.exe)|plink.exe;ssh.exe|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _sshClient = openFileDialog1.FileName;
                lblSSHClient.Text = openFileDialog1.FileName;
            }
        }

        private void lnkSelectSCPClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Supported SCP Clients (pscp.exe;scp.exe)|pscp.exe;scp.exe|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _scpClient = openFileDialog1.FileName;
                lblSCPClient.Text = openFileDialog1.FileName;
            }
        }

        #region Properties Implementation


        public PollCollection PollCollection
        {
            get
            {
                return _polls;
            }
        }

        #endregion

        private void btnEditCancel_Click(object sender, EventArgs e)
        {
            btnAddPoll.Click -= editPoll_OnClick;
            btnAddPoll.Click += new EventHandler(btnAddPoll_Click);

            _currentEditId = "";
            btnAddPoll.Text = "Add";
            btnEditCancel.Visible = false;
        }

        private void lstPolls_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTestPollItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_privateKeyFile) && !string.IsNullOrEmpty(_sshClient) && _sshPort > 0 && _sshPort < int.MaxValue)
            {
                if (!string.IsNullOrEmpty(txtPollCommand.Text))
                {

                    InputBoxResult result = InputBox.Show("Enter a hostname or IP to test:", "Konvict", "");

                    if (result.ReturnCode != DialogResult.OK || string.IsNullOrEmpty(result.Text))
                    {
                        return;
                    }

                    btnTestPollItem.Enabled = false;
                    
                    string host = result.Text;

                    string arguments = string.Format("-batch -ssh -P {0} -i \"{1}\" root@{2} {3}", _sshPort, _privateKeyFile, host, txtPollCommand.Text);
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.Arguments = arguments;
                    startInfo.FileName = _sshClient;
                    startInfo.ErrorDialog = false;
                    startInfo.UseShellExecute = false ;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardInput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.CreateNoWindow = true;

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo = startInfo;
                    //process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
                    //process.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_ErrorDataReceived);
                    //process.Exited += new EventHandler(process_Exited);
                    process.Start();
                    //process.BeginOutputReadLine();
                    //process.BeginErrorReadLine();
                    process.EnableRaisingEvents = true;

                    string output = process.StandardOutput.ReadToEnd();
                    string errOutput = process.StandardError.ReadToEnd();

                    process.WaitForExit();
                    process.Close();
                    process = null;

                    if (errOutput != null && errOutput.Length > 0)
                        output += "\r\n[ERROR: " + errOutput + "]";

                    MessageBox.Show("Result:\r\n" + output, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    btnTestPollItem.Enabled = true;
                }
            }
        }

        void process_Exited(object sender, EventArgs e)
        {
            //
        }

        void process_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }

        void process_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }

        private void lstPolls_MouseUp(object sender, MouseEventArgs e)
        {
            if (lstPolls.SelectedIndex == -1)
                return;

            string id = lstPolls.Items[lstPolls.SelectedIndex].ToString();
            if (id != null && id.Length > 0)
            {
                PollItem pollItem = _polls[id];
                if (pollItem != null)
                {
                    txtPollCommand.Text = pollItem.Command;
                    txtPollInterval.Text = pollItem.Interval.ToString();
                    cboExportAction.SelectedIndex = (int)pollItem.ExportAction;
                }
            }
        }

    }
}
