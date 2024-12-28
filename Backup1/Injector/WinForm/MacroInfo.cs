using System.Data;
using Injector.MacroEx;

namespace Injector
{
    public partial class MacroInfo : System.Windows.Forms.Form
    {

        public MacroInfo()
        {
            InitializeComponent();

            DataTable data = new DataTable("macros");
            DataColumn col = data.Columns.Add("Name", typeof(string));
            data.PrimaryKey = new DataColumn[] { col };
            data.Columns.Add("Caption", typeof(string));
            data.Columns.Add("Category", typeof(string));

            dataGrid.PreferredColumnWidth = 150;
            dataGrid.DataSource = data;
            
            Macro[] macros = MacroManager.Macros;
            foreach (Macro m in macros)
            {
                DataRow row = data.NewRow();
                row[0] = m.Identifier; //+ " (" + ai.level + ")";
                row[1] = m.Caption;
                row[2] = m.Category;
                data.Rows.Add(row);
            }


        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
