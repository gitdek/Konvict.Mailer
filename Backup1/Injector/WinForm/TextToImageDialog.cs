using System;
using System.Drawing;
using System.Windows.Forms;

namespace Injector
{
    public partial class TextToImageDialog : MacroWindow
    {
        private Font _font;
        private System.Drawing.Color _background;
        private System.Drawing.Color _foreground;
        private string _macroText = null;

        public TextToImageDialog()
        {
            InitializeComponent();

            this.MacroName = "TextToImage";

            _font = new Font("Arial", 20);
            _foreground = Color.Black;
            _background = Color.White;
        }

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            fontDialog1.AllowScriptChange = false;
            fontDialog1.AllowVectorFonts = false;
            fontDialog1.AllowVerticalFonts = true;
            fontDialog1.FontMustExist = true;
            fontDialog1.ShowColor = true;
            fontDialog1.ShowEffects = true;
            fontDialog1.Color = _foreground;
            fontDialog1.Font = _font;

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _font = fontDialog1.Font;
                _foreground = fontDialog1.Color;

                picForeground.BackColor = _foreground;
                lblFont.Text = _font.Name + "," + _font.Size.ToString() + "; " + _font.Style.ToString();
                lblForeground.Text = ColorTranslator.ToHtml(_foreground);
                
            }
        }

        private void btnSelectBackground_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _background;
            colorDialog1.AllowFullOpen = true;
            colorDialog1.AnyColor = true;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _background = colorDialog1.Color;

                picBackground.BackColor = _background;
                lblBackground.Text = ColorTranslator.ToHtml(_background);
            }
        }

        private void btnSelectForeground_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = _foreground;
            colorDialog1.AllowFullOpen = true;
            colorDialog1.AnyColor = true;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _foreground = colorDialog1.Color;

                picForeground.BackColor = _foreground;
                lblForeground.Text = ColorTranslator.ToHtml(_foreground);
            }

        }

        private void picBackground_Click(object sender, EventArgs e)
        {
            btnSelectBackground.PerformClick();
        }

        private void picForeground_Click(object sender, EventArgs e)
        {
            btnSelectFont.PerformClick();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtImageText.Text != null && txtImageText.Text.Length > 0)
            {
                string cleanText = txtImageText.Text;
                cleanText = cleanText.Replace("\r\n", "\n");
                cleanText = cleanText.Replace("\n", "$NL()");
                cleanText = cleanText.Replace("\t", "$TAB()");

                string fcolorHtml = String.Format("#{0}", _foreground.ToArgb().ToString("X").Substring(2));
                string bcolorHtml = String.Format("#{0}", _background.ToArgb().ToString("X").Substring(2));

                _macroText = String.Format("[font={0} size={1}][textcolor={2}][bgcolor={3}]{4}", _font.Name, _font.Size.ToString(), fcolorHtml, bcolorHtml, cleanText);

                this.MacroArguments = _macroText;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        public string MacroText
        {
            get
            {
                return _macroText;
            }
        }


    }
}
