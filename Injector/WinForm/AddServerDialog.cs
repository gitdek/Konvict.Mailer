using System;
using System.Windows.Forms;

namespace Injector
{
    public partial class AddServerDialog : Form
    {
        private bool _portValidated;
        private Server _server;

        public AddServerDialog()
        {
            InitializeComponent();

            mtxtPort.ValidatingType = typeof(System.Int32);
            mtxtPort.TypeValidationCompleted += new TypeValidationEventHandler(mtxtPort_TypeValidationCompleted);
        }

        public void mtxtPort_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                _portValidated = false;
                e.Cancel = true;
                return;
            }
            _portValidated = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!_portValidated)
            {
                MessageBox.Show("Please enter a valid port.", "Error");
                return;
            }
            if (string.IsNullOrEmpty(txtHost.Text))
            {
                MessageBox.Show("Please enter a valid host.", "Error");
                return;
            }

            _server = new Server(txtHost.Text, Convert.ToInt32(mtxtPort.Text), txtUser.Text, txtPass.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _server = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        #region Properties Implementation

        public Server Server
        {
            get { return _server; }
        }


        #endregion

    }
}
