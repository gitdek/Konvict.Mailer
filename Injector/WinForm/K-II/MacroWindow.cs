using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Injector
{
    public partial class MacroWindow : Form
    {

        public MacroWindow()
        {
            InitializeComponent();
        }

        public string MacroName
        {
            get;
            set;
        }

        public string MacroArguments
        {
            get;
            set;
        }
    }
}
