
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subnetscan2nd
{
    public partial class RemoteConnection : Form
    {
        public RemoteConnection()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rdp.Server = txtServer.Text;
            rdp.UserName = txtUserName.Text;
            MSTSCLib.IMsTscNonScriptable secured = (MSTSCLib.IMsTscNonScriptable)rdp.GetOcx();
            secured.ClearTextPassword = txtPassword.Text;
            rdp.Connect();
        }
    }
}
