using System;
using System.Windows.Forms;

namespace Injector
{
    public partial class RndDialog : MacroWindow
    {
        private string _macroText = null;

        public RndDialog()
        {
            InitializeComponent();

            this.MacroName = "Rnd";
        }

        public string MacroText
        {
            get
            {
                return _macroText;
            }
            set
            {
                _macroText = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtRnd.Text != null && txtRnd.Text.Length > 0)
            {
                _macroText = txtRnd.Text;
                this.MacroArguments = txtRnd.Text;


                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MacroEx.MacroInterpreter.Translate(String.Format("$Rnd({0})",txtRnd.Text)));
        }

    }
}
