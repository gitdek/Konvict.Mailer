using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Injector
{
    public partial class MainFormClone : Form
    {
        private DummyPropertyWindow m_propertyWindow = new DummyPropertyWindow();
        //private DummyToolbox m_toolbox = new DummyToolbox();
        private DummyOutputWindow m_outputWindow = new DummyOutputWindow();
        private SolutionExplorer m_solutionWindow = new SolutionExplorer();
        private NavigationWindow m_navigationWindow = new NavigationWindow();
        private MessageWindow m_messageWindow = new MessageWindow(); // Explores all messages in a campaign.
        private RecipientWindow m_recipientWindow = new RecipientWindow();

        public MainFormClone()
        {
            InitializeComponent();

            m_propertyWindow.Show(dockPanel);
            m_navigationWindow.Show(dockPanel);
            m_messageWindow.Show(dockPanel);
            m_recipientWindow.Show(dockPanel);

            m_outputWindow.Show(dockPanel);
            //m_outputWindow.Show(m_propertyWindow.Pane, DockAlignment.Bottom, 0.35);

            //DummyDoc dummyDoc = CreateNewDocument();
            //if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            //{
            //    dummyDoc.MdiParent = this;
            //    dummyDoc.Show();
            //}
            //else
            //    dummyDoc.Show(dockPanel);

        }

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }


        private DummyDoc CreateNewDocument()
        {
            DummyDoc dummyDoc = new DummyDoc();

            int count = 1;
            //string text = "C:\\MADFDKAJ\\ADAKFJASD\\ADFKDSAKFJASD\\ASDFKASDFJASDF\\ASDFIJADSFJ\\ASDFKDFDA" + count.ToString();
            string text = "Document" + count.ToString();
            while (FindDocument(text) != null)
            {
                count++;
                //text = "C:\\MADFDKAJ\\ADAKFJASD\\ADFKDSAKFJASD\\ASDFKASDFJASDF\\ASDFIJADSFJ\\ASDFKDFDA" + count.ToString();
                text = "Document" + count.ToString();
            }
            //dummyDoc.Text = text;
            dummyDoc.TabText = text;
            //dummyDoc.Text = text;
            return dummyDoc;
        }


        private DummyDoc CreateNewDocument(string text)
        {
            DummyDoc dummyDoc = new DummyDoc();
            dummyDoc.Text = text;
            return dummyDoc;
        }


        private void CloseAllDocuments()
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                    content.DockHandler.Close();
            }
        }


        private void SetDocumentStyle(object sender, System.EventArgs e)
        {
            DocumentStyle oldStyle = dockPanel.DocumentStyle;
            DocumentStyle newStyle;
            //if (sender == menuItemDockingMdi)
            //    newStyle = DocumentStyle.DockingMdi;
            //else if (sender == menuItemDockingWindow)
            //    newStyle = DocumentStyle.DockingWindow;
            //else if (sender == menuItemDockingSdi)
            //    newStyle = DocumentStyle.DockingSdi;
            //else
                newStyle = DocumentStyle.SystemMdi;

            if (oldStyle == newStyle)
                return;

            if (oldStyle == DocumentStyle.SystemMdi || newStyle == DocumentStyle.SystemMdi)
                CloseAllDocuments();

            dockPanel.DocumentStyle = newStyle;
        }

        private void mnuViewProperty_Click(object sender, EventArgs e)
        {
            m_propertyWindow.Show(dockPanel);
        }

        private void mnuViewNavigation_Click(object sender, EventArgs e)
        {
            m_navigationWindow.Show(dockPanel);
        }

    }
}
