using System;
using System.Drawing;
using System.Windows.Forms;

using Injector.Core.TaskScheduler;

namespace Injector
{
    public partial class CampaignScheduleDialog : Form
    {
        private TimeSpan _interval = TimeSpan.MinValue;
        private string _dateFormat = "ddd, MMMM d, yyyy 'at' h:mm tt";

        private DateTime _expires = DateTime.MaxValue;
        private Occurrence _occurrence = Occurrence.Once;
        private Schedule _schedule = new Schedule();

        public CampaignScheduleDialog()
        {
            InitializeComponent();

            cboDay.Items.Add("Every Day");
            foreach (string name in Enum.GetNames(typeof(DayOfWeek))) cboDay.Items.Add(name);

            cboHour.Items.Add("Every Hour");
            for (int h = 0; h < 24; h++) cboHour.Items.Add(h.ToString());

            cboMinute.Items.Add("Every Minute");
            for (int s = 0; s < 60; s++) cboMinute.Items.Add(s.ToString());

            nudDays.ValueChanged += new EventHandler(OnIntervalChanged);
            nudHours.ValueChanged += new EventHandler(OnIntervalChanged);
            nudMins.ValueChanged += new EventHandler(OnIntervalChanged);
            chkExpires.CheckedChanged += new EventHandler(chkExpires_CheckedChanged);
            clndrExpire.DateChanged += new DateRangeEventHandler(OnExpirationChanged);
            dtExpiration.ValueChanged += new EventHandler(OnExpirationChanged);

            cboScheduleType.SelectedIndexChanged += new EventHandler(cboScheduleType_SelectedIndexChanged);
            nudDays.Value = 0;
            nudHours.Value = 1;
            nudMins.Value = 30;

            dtExpiration.ShowUpDown = true;
            dtExpiration.Format = DateTimePickerFormat.Time;
            dtExpiration.Value = DateTime.Now;

            chkExpires.Checked = false;
            chkForceRun.Checked = false;

            pnlTimeBased.Location = new Point(16, 112);
            pnlInterval.Location = new Point(16, 112);

            cboOccurence.SelectedIndex = 1;
            cboScheduleType.SelectedIndex = 1;
        }

        void cboScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboScheduleType.SelectedIndex == 0)
                pnlTimeBased.BringToFront();
            else
                pnlInterval.BringToFront();
        }

        void OnExpirationChanged(object sender, EventArgs e)
        {
            DateTime date = clndrExpire.SelectionStart;
            DateTime time = dtExpiration.Value;
            DateTime expires = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            _expires = expires;
            chkExpires.Text = "Expiration: " + _expires.ToString(_dateFormat);
        }

        void chkExpires_CheckedChanged(object sender, EventArgs e)
        {
            dtExpiration.Enabled = chkExpires.Checked;
            clndrExpire.Enabled = chkExpires.Checked;
        }

        void OnIntervalChanged(object sender, EventArgs e)
        {
            Exception x = null;
            try
            {
                _interval = new TimeSpan((int)nudDays.Value, (int)nudHours.Value, (int)nudMins.Value, 0);
            }
            catch (Exception ex)
            {
                x = ex;
            }
            finally
            {
                if (x != null)
                    _interval = TimeSpan.MinValue;
            }
        }

        private void CampaignScheduleDialog_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DateTime startTime = DateTime.Now;

            string message = "";
            message += "Last Run: " + startTime.ToString() + " (Now)";
            message += "\r\nScheduler Interval: " + _interval.ToString();
            message += "\r\n\r\nNext Run: " + startTime.Add(_interval).ToString(_dateFormat);

            MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lnkAboutForceRun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "This specifies whether a campaign should be triggered to start forcefully if the system was not running when the schedule was due.";
            MessageBox.Show(message, "Konvict", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void lblNextRun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            _schedule.Type = cboScheduleType.SelectedIndex == 0 ? ScheduleType.TimeBased : ScheduleType.IntervalBased;

            switch (cboOccurence.SelectedIndex)
            {
                case 0:
                    _occurrence = Occurrence.Once;
                    break;
                case 1:
                    _occurrence = Occurrence.Repeat;
                    break;
                case 2:
                    _occurrence = Occurrence.EveryStartUp;
                    break;
                default:
                    _occurrence = Occurrence.Once;
                    break;
            }

            if (_schedule.Type == ScheduleType.IntervalBased)
                _schedule.Interval = _interval;
            else
            {
                _schedule.Day = cboDay.SelectedIndex - 1;
                _schedule.Hour = cboHour.SelectedIndex - 1;
                _schedule.Minute = cboMinute.SelectedIndex - 1;
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        public DateTime Expires
        {
            get
            {
                return _expires;
            }
        }

        public Occurrence Occurrence
        {
            get
            {
                return _occurrence;
            }
        }

        public Schedule Schedule
        {
            get
            {
                return _schedule;
            }
        }





    }
}
