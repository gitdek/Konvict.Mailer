using System;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Drawing;

namespace Injector
{
    public partial class DatagridDialog : Form
    {
        public DatagridDialog()
            : this(string.Empty, null)
        {
        }

        public DatagridDialog(string title, DataView dataview)
            : this(title, dataview, string.Empty)
        {
        }

        public DatagridDialog(string title, DataView dataview, string description)
        {
            InitializeComponent();

            lblTitle.Text = title;
            lblDescription.Text = description;

            if (dataview != null)
            {
                DataTable dt = dataview.Table.Clone();

                int idx = 0;
                string[] strColNames = new string[dt.Columns.Count];
                foreach (DataColumn col in dt.Columns)
                {
                    strColNames[idx++] = col.ColumnName;
                }


                dataGrid.DataSource = dt;


                IEnumerator viewEnumerator = dataview.GetEnumerator();
                while (viewEnumerator.MoveNext())
                {
                    DataRowView drv = (DataRowView)viewEnumerator.Current;
                    DataRow dr = dt.NewRow();
                    try
                    {
                        foreach (string strName in strColNames)
                        {
                            dr[strName] = drv[strName];
                        }
                    }
                    catch
                    {
                    }
                    dt.Rows.Add(dr);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AutoSizeColumn(int col)
        {
            float width = 0;
            int numRows = ((DataTable)dataGrid.DataSource).Rows.Count;

            Graphics g = Graphics.FromHwnd(dataGrid.Handle);
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
            SizeF size;

            for (int i = 0; i < numRows; ++i)
            {
                size = g.MeasureString(dataGrid[i, col].ToString(), dataGrid.Font, 500, sf);
                if (size.Width > width)
                    width = size.Width;
            }

            g.Dispose();

            dataGrid.TableStyles[0].GridColumnStyles[col].Width = (int)width + 8; // 8 is for leading and trailing padding
        }

    }
}
