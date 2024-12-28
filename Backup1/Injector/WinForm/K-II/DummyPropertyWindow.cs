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
    public partial class DummyPropertyWindow : ToolWindow
    {
        public DummyPropertyWindow()
        {
            InitializeComponent();

            PropertyPageExample ppe = new PropertyPageExample();
            propertyGrid.SelectedObject = ppe;
            comboBox.Items.Add("Properties");
            comboBox.SelectedIndex = 0;
        }
    }


    [DefaultPropertyAttribute("UrlGetIPs")]
    public class PropertyPageExample
    {
        private bool _cluster = false;
        private int _clusterSize = 3;
        private bool _clusterCustomSettings;
        private string[] _clusterSettings;
        private string _urlGetIPs = "http://sv1.zd6.net:8080/getlocalips";
        private string _poolRange = "68.169.21.0/24";
        private string _poolName = "vm-train1";
        private bool _poolComment = true;


        public PropertyPageExample()
        {
            //_clusterSettings = "";
        }

        [Category("Clustering")]
        [Description("Cluster MTA's")]
        [DisplayName("Clustering")]
        public bool Cluster
        {
            get
            {
                return _cluster;
            }
            set
            {
                _cluster = value;
            }
        }

        [Category("Clustering")]
        [Description("Custom cluster settings")]
        [DisplayName("Custom Settings")]
        public bool ClusterCustomSettings
        {
            get
            {
                return _clusterCustomSettings;
            }
            set
            {
                _clusterCustomSettings = value;
            }
        }

        [Category("Clustering")]
        [Description("Custom cluster settings")]
        [DisplayName("Custom Settings")]
        public string[] ClusterSettings
        {
            get
            {
                return _clusterSettings;
            }
            set
            {
                _clusterSettings = value;
            }
        }

        [Category("Clustering")]
        [Description("MTA per cluster")]
        [DisplayName("Size")]
        public int ClusterSize
        {
            get
            {
                return _clusterSize;
            }
            set
            {
                _clusterSize = value;
            }
        }

        [Category("Pool")]
        [Description("Comment the pool")]
        [DisplayName("Comments")]
        public bool PoolComment
        {
            get
            {
                return _poolComment;
            }
            set
            {
                _poolComment = value;
            }
        }

        [Category("Pool")]
        [Description("Name of the pool")]
        [DisplayName("Pool Name")]
        public string PoolName
        {
            get
            {
                return _poolName;
            }
            set
            {
                _poolName = value;
            }
        }

        [Category("Pool")]
        [Description("IP range of the pool")]
        [DisplayName("IP Range")]
        public string PoolRange
        {
            get
            {
                return _poolRange;
            }
            set
            {
                _poolRange = value;
            }
        }

        [Category("Master")]
        [Description("PMTA getlocalips URL")]
        [DisplayName("PMTA URL")]
        public string UrlGetIPs
        {
            get
            {
                return _urlGetIPs;
            }
            set
            {
                _urlGetIPs = value;
            }
        }
    }
}