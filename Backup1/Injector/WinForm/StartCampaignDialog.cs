using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Injector.Core.Settings;
using Injector.Core.Logging;
using System.Linq;
using System.IO;

namespace Injector
{
    public partial class StartCampaignDialog : Form
    {
        private MainForm _owner;

        private List<Campaign> _campaigns;

        public StartCampaignDialog(List<Campaign> campaigns, MainForm owner)
        {
            InitializeComponent();

            _campaigns = campaigns;
            _owner = owner;

            cboCommand.SelectedIndexChanged += new EventHandler(cboCommand_SelectedIndexChanged);
            cboFilterCommand.SelectedIndexChanged += new EventHandler(cboFilterCommand_SelectedIndexChanged);
            cboFilterCommand.Items.Clear();
            cboFilterCommand.Items.Add("All Campaigns");
            cboFilterCommand.Items.Add("----------");

            bool showWarning = false;

            foreach (Campaign c in campaigns)
            {
                cboFilterCommand.Items.Add(c.Name);
                if (c.SupressionFile != null && c.SupressionFile.Length > 0 && c.Suppression)
                    showWarning = true;
            }

            cboCommand.SelectedIndex = 0;
            cboFilterCommand.SelectedIndex = 0;
            radioMTA.Checked = true;

            txtSummary.Text = _owner.CreateStartSummary(_campaigns);


            if (showWarning)
            {
                string message = "One or more of the campaigns has the supression file enabled. This can take some time, depending on the size of your recipient and supression lists, but usually it won't.";
                message += "\n\nA temporary copy of each cleaned recipient list will now be created.";
                message += "\n\nIf you would like to disable supression and skip this, click CANCEL, otherwise OK";
                if (MessageBox.Show(message, "Konvict", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                {
                    foreach (Campaign c in campaigns)
                        c.Suppression = false;
                }
            }

            foreach (Campaign c in campaigns)
            {
                if (c.Suppression && c.SupressionFile != null && System.IO.File.Exists(c.SupressionFile))
                {
                    string[] supression = System.IO.File.ReadAllLines(c.SupressionFile);

                    foreach (Campaign.RecipientProvider rctp in c.RecipientProviders)
                    {
                        string[] recipients = File.ReadAllLines(rctp.FileName);

                        IEnumerable<string> differenceQuery =
                          recipients.Except(supression);

                        string pathRcpt = Path.Combine(Path.GetDirectoryName(rctp.FileName), "TEMP");

                        if (!Directory.Exists(pathRcpt))
                            Directory.CreateDirectory(pathRcpt);

                        string tempList = Path.Combine(pathRcpt, Path.GetRandomFileName());
                        File.WriteAllLines(tempList, differenceQuery.ToArray());
                        long byteDiff = (new System.IO.FileInfo(rctp.FileName).Length) - (new System.IO.FileInfo(tempList).Length);
                       
                        if (byteDiff > 0)
                        {
                            File.Move(rctp.FileName, rctp.FileName + ".bak");
                            File.Move(tempList, rctp.FileName);

                            ILogger logger = ServiceScope.Get<ILogger>();
                            logger.Info("Supression completed on {0} and removed {1} of data.", Path.GetFileName(rctp.FileName), Utilities.Conversion.GetSizeString(byteDiff));
                        }
                        else
                            if (File.Exists(tempList)) 
                                File.Delete(tempList);
                    }
                }
            }
        }

        void cboFilterCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFilterCommand.SelectedIndex != 1)
            {
                btnOk.Enabled = true;
                return;
            }

            btnOk.Enabled = false;
        }

        void cboCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCommand.SelectedIndex != 2 || cboCommand.SelectedIndex != 5)
            {
                btnOk.Enabled = true;
                return;
            }

            btnOk.Enabled = false;
        }

        private void radioDirect_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();


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



            //SettingsEx settings = SettingsEx.Open();

            if (string.IsNullOrEmpty(coreSettings.KeyFile))
            {
                errorProvider.SetError(radioDirect, "Private key not configured.");
                return;
            }

            if (string.IsNullOrEmpty(coreSettings.RemoteKlocalPath))
            {
                errorProvider.SetError(radioDirect, "KLocal upload path not configured.");
                return;
            }
            if (string.IsNullOrEmpty(coreSettings.Scpclient))
            {
                errorProvider.SetError(radioDirect, "SCP Client not configured.");
                return;
            }

            if (string.IsNullOrEmpty(coreSettings.Sshclient))
            {
                errorProvider.SetError(radioDirect, "SSH Client not configured.");
                return;
            }

            if (simpleCheckConfig(radioDirect))
            {
                btnOk.Enabled = true;
                return;
            }
            btnOk.Enabled = false;
        }

        private void radioMTA_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (simpleCheckConfig(radioMTA))
            {
                btnOk.Enabled = true;
                return;
            }

            btnOk.Enabled = false;
        }


        private bool simpleCheckConfig(Control errctl)
        {
            errorProvider.Clear();

            if (_campaigns.Count == 0) return false;

            foreach (Campaign c in _campaigns)
            {
                if (c.IsDisposed) continue;

                string msgHead = "Campaign: " + c.Name + "\r\n\r\n";

                if (c.Domains.Count == 0)
                {
                    errorProvider.SetError(errctl, msgHead + "Domains not configured.");
                    return false;
                }

                if (c.Mailhosts.Count == 0)
                {
                    errorProvider.SetError(errctl, msgHead + "Servers not configured.");
                    return false;
                }

                if (c.MessageList.Count == 0)
                {
                    errorProvider.SetError(errctl, msgHead + "Messages not configured.");
                    return false;
                }

                if (c.RecipientProviders.Count == 0)
                {
                    errorProvider.SetError(errctl, msgHead + "Recipients not configured.");
                    return false;
                }
            }

            return true;
        }

        private void radioPickup_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(radioPickup, "Not supported in this version.");
            btnOk.Enabled = false;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            List<Server> filtered = new List<Server>();


            if (cboFilterCommand.SelectedIndex == 0)
            {
                foreach (Campaign c in _campaigns)
                {
                    foreach (Server server in c.Mailhosts)
                        filtered.Add(server);
                }
            }
            else
            {
                foreach (Campaign campaign in _campaigns)
                {
                    if (campaign.Name == cboFilterCommand.SelectedText)
                    {
                        foreach (Server mailhost in campaign.Mailhosts)
                            filtered.Add(mailhost);
                    }
                }
            }

            string message = "";
            message += "Command: '" + cboCommand.SelectedText + "'\r\n";
            message += "This command will be executed on " + filtered.Count + " server(s).\r\n\r\n";
            message += "Are you sure you want to continue?";

            if (MessageBox.Show(message, "Konvict", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes) return;



            // All commands will perform a similiar routine.
            // 1) Create a dialog with either a) listview - b) datagrid - c) textbox, aligned text. 
            // 2) Row for each server.
            // 3) Display the results!

            // ----------------------------------------------------
            // Campaign         |  Server      |             Result
            //
            // dbtwipe1         |  69.3.11.9   |      KL-unix v0.3b
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
