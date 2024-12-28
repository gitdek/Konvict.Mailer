using System;
using System.Windows.Forms;
using System.Collections;
using System.Text;

namespace Injector
{
    public partial class RotateDialog : MacroWindow
    {
        private ArrayList _phrases = null;

        public RotateDialog()
        {
            InitializeComponent();

            _phrases = new ArrayList();

            this.MacroName = "Rot";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPhrases.Lines.Length > 0)
            {
                Array.ForEach(txtPhrases.Lines, delegate(string p) { _phrases.Add(p.ToString()); });

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < _phrases.Count; i++)
                {
                    sb.Append(_phrases[i].ToString());
                    if (i != (_phrases.Count - 1)) sb.Append("|");
                }

                this.MacroArguments = sb.ToString();
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public ArrayList Phrases
        {
            get
            {
                return _phrases;
            }
        }
    }
}
