using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Injector
{
    public partial class NavigationWindow : ToolWindow
    {
        public NavigationWindow()
        {
            InitializeComponent();
        }

        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            tvwNavi.RightToLeftLayout = RightToLeftLayout;
        }
    }
}
