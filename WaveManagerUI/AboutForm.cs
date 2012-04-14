using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WaveManagerBusiness;
using System.Diagnostics;

namespace WaveManagerUI
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void OnAboutFormLoad(object sender, EventArgs e)
        {
            _productNameLabel.Text = CompanyInfo.ProductName;
            _productVersionLabel.Text = "Version: " + CompanyInfo.ProductVersion;
            _copyrightLabel.Text = "© " + CompanyInfo.ProductAuthor;
            _companyNameLabel.Text = CompanyInfo.Name;
            _aboutProductLabel.Text = "About " + CompanyInfo.ProductName;
            _aboutProductText.Text = CompanyInfo.ProductDescription;
            _linkURL.Text = CompanyInfo.CompanyURL;
            _aboutProductText.ReadOnly = true;
        }

        private void OnCloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        // adapted from: http://msdn.microsoft.com/en-us/library/aa983660(v=vs.71).aspx
        private void OnLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                _linkURL.LinkVisited = true;
                Process.Start(CompanyInfo.CompanyURL);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to visit Company Website at this time. Please Try again later.");
            }
        }
    }
}
