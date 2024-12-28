using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Data;

namespace Injector
{
    public partial class AboutInfo : System.Windows.Forms.Form
    {

        public AboutInfo()
        {
            InitializeComponent();

            DataTable data = new DataTable("assemblies");
            DataColumn col = data.Columns.Add("Name", typeof(string));
            data.PrimaryKey = new DataColumn[] { col };
            data.Columns.Add("Version", typeof(string));
            data.Columns.Add("Product", typeof(string));
            data.Columns.Add("Copyright", typeof(string));
            data.Columns.Add("Path", typeof(string));
            data.Columns.Add("Title", typeof(string));
            data.Columns.Add("Description", typeof(string));
            data.Columns.Add("CLR Version", typeof(string));
            dataGrid.PreferredColumnWidth = 150;
            dataGrid.DataSource = data;

            Assembly assembly = Assembly.GetExecutingAssembly();
            SortedList ht = new SortedList(31);
            RecursiveGetReferences(ht, assembly.GetName(), 0);
            foreach (DictionaryEntry entry in ht)
            {
                AssemblyInfo ai = (AssemblyInfo)entry.Value;
                DataRow row = data.NewRow();
                row[0] = ai.Name.Name + " (" + ai.level + ")";
                row[1] = ai.Name.Version.ToString();
                row[2] = ai.Info.ProductName;
                row[3] = ai.Info.LegalCopyright;
                row[4] = ai.Assembly.Location;
                row[5] = ai.Info.Comments;
                row[6] = ai.Info.FileDescription;
                row[7] = ai.Assembly.ImageRuntimeVersion;
                data.Rows.Add(row);
            }

            

        }

        private void RecursiveGetReferences(SortedList table, AssemblyName name, int level)
        {
            if (table.ContainsKey(name.Name))
            {
                AssemblyInfo t = (AssemblyInfo)table[name.Name];
                if (t.level > level)
                {
                    t.level = level;
                    table[name.Name] = t;
                }
                return;
            }
            AssemblyInfo ai = new AssemblyInfo();
            ai.Assembly = Assembly.Load(name);
            ai.level = level;
            ai.Name = name;
            ai.Info = FileVersionInfo.GetVersionInfo(ai.Assembly.Location);
            table.Add(name.Name, ai);
            AssemblyName[] names = ai.Assembly.GetReferencedAssemblies();
            for (int i = 0; i < names.Length; i++)
                RecursiveGetReferences(table, names[i], level + 1);
        }

        private struct AssemblyInfo
        {
            public int level;
            public Assembly Assembly;
            public AssemblyName Name;
            public FileVersionInfo Info;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
