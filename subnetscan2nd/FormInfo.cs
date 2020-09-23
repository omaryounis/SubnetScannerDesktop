using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subnetscan2nd
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            dgvWMI.DataSource = GetInformation(comboBoxWin32API.Text);
        }

        private ArrayList GetInformation(string qry)
        {
            ManagementObjectSearcher searcher;
            int i = 0;
            ArrayList arrayListInformationCollactor = new ArrayList();
            try
            {
                searcher = new ManagementObjectSearcher("\\\\" + HostName.Text + "\\root\\CIMV2", "SELECT *FROM " + qry);// new ManagementObjectSearcher("SELECT * FROM " + qry);
                searcher.Scope.Options.EnablePrivileges = true;
                var x = searcher.Get();
                foreach (ManagementObject mo in searcher.Get())
                {
                    mo.Scope.Options.EnablePrivileges = true;
                    i++;
                    PropertyDataCollection searcherProperties = mo.Properties;
                    foreach (PropertyData sp in searcherProperties)
                    {
                        arrayListInformationCollactor.Add(sp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return arrayListInformationCollactor;
        }
    }
}
