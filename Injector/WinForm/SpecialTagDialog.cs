using System;
using System.Windows.Forms;
using Injector.MacroEx;

namespace Injector
{
    public partial class SpecialTagDialog : MacroWindow
    {
        private string _selectedTag = null;

        public SpecialTagDialog()
        {
            InitializeComponent();

            lvMacros.BeginUpdate();
            lvMacros.Columns.Add("Tag");
            lvMacros.Columns.Add("Caption");
            lvMacros.Columns.Add("Category");
            lvMacros.Columns[0].Width = (int)lvMacros.Width / 3 - 20;
            lvMacros.Columns[1].Width = (int)lvMacros.Width / 3 - 20;
            lvMacros.Columns[2].Width = (int)lvMacros.Width / 3 - 20;
            lvMacros.View = View.Details;

            Macro[] macros = MacroManager.Macros;
            foreach (Macro m in macros)
            {
                ListViewItem row = new ListViewItem();
                row.Text = m.Identifier;
                row.SubItems.Add(m.Caption);
                row.SubItems.Add(m.Category);
                lvMacros.Items.Add(row);
            }
            lvMacros.EndUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string SelectedTag
        {
            get
            {
                return _selectedTag;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (lvMacros.SelectedItems.Count > 0)
            {
                ListView.SelectedIndexCollection items = lvMacros.SelectedIndices;
                if (items.Count > 0)
                {
                    
                    _selectedTag = lvMacros.Items[items[0]].Text;
                    this.MacroName = _selectedTag;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

    }
}
