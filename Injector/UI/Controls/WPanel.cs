using System.Drawing;
using System.Windows.Forms;

namespace Injector.UI.Controls
{
    /// <summary>
    /// Impelemnts Panel control with ViewStyle support.
    /// </summary>
    public class WPanel : Panel
    {
        private ViewStyle m_pViewStyle = null;
        private bool      m_DrawBorder = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WPanel()
        {
            this.BorderStyle = BorderStyle.None;

            SetStyle(ControlStyles.ResizeRedraw,true);
        }


        #region method OnPaint

        /// <summary>
        /// Paitns panel.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(m_DrawBorder){
                Color borderColor = Color.DarkGray;
                if(m_pViewStyle != null){
                    borderColor = m_pViewStyle.BorderColor;
                }

                ControlPaint.DrawBorder(
                    e.Graphics,
                    this.ClientRectangle,
                    borderColor,
                    1,
                    ButtonBorderStyle.Solid,
                    borderColor,
                    1,
                    ButtonBorderStyle.Solid,
                    borderColor,
                    1,
                    ButtonBorderStyle.Solid,
                    borderColor,
                    1,
                    ButtonBorderStyle.Solid
                );
            }
        }

        #endregion


        #region Properties Implementation

        /// <summary>
        /// Gets or sets view-style to use.
        /// </summary>
        public ViewStyle ViewStyle
        {
            get{ return m_pViewStyle; }

            set{ m_pViewStyle = value; }
        }

        /// <summary>
        /// Gets or sets if panel will draw border.
        /// </summary>
        public bool DrawBorder
        {
            get{ return m_DrawBorder; }

            set{ m_DrawBorder = value; }
        }

        #endregion

    }
}
