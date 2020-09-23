using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Net.Sockets;
using System.Collections;

namespace subnetscan2nd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "Idle";
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        Thread myThread = null;
        public void scan(string subnet) 
        {
            Ping myPing;
            PingReply reply;
            IPAddress addr;
            IPHostEntry host;

            progressBar1.Maximum = 254;
            progressBar1.Value = 0;
            listVAddr.Items.Clear();

            for (int i = 1; i < 255; i++)
            {
                string subnetn = "." + i.ToString();
                myPing = new Ping();

                byte[] buffer = Encoding.ASCII.GetBytes(subnet + subnetn);
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                reply = myPing.Send(subnet+subnetn, 1000,buffer, options);

                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Scanning: " + subnet + subnetn;

                if (reply.Status == IPStatus.Success)
                {
                    try 
                     {
                         addr = IPAddress.Parse(subnet + subnetn);
                         host = Dns.GetHostEntry(addr);

                         listVAddr.Items.Add(new ListViewItem(new String[] { subnet + subnetn, host.HostName, "Up", GetMacAddress(subnet + subnetn) }));
                     }
                     catch(SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.HostNotFound)
                        {
                            continue;
                        }
                        //    MessageBox.Show("Couldnt retrieve hostname for "+subnet+subnetn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                 }
                 progressBar1.Value += 1;
            }                    
            cmdScan.Enabled = true;
            cmdStop.Enabled = false;
            txtIP.Enabled = true;
            lblStatus.Text = "Done!";
            int count = listVAddr.Items.Count;
            MessageBox.Show("Scanning done!\nFound " + count.ToString() + " hosts.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        public void query(string host) 
        {

            //string temp = null;
            //string[] _searchClass = {"Win32_ComputerSystem", "Win32_OperatingSystem", "Win32_BaseBoard", "Win32_BIOS",
            //                         //"Win32_DiskDrive" ,
            //                         "Win32_Processor"/*, "Win32_ProgramGroup","Win32_SystemDevices","Win32_StartupCommand" */};
            //string[] param = { "UserName", "Caption", "Product", "Description", "Name" };

            //lblStatus.ForeColor = System.Drawing.Color.Green; 
            ////----------------------------------------------------------------------
            //for (int i = 0; i <= _searchClass.Length-1; i++)
            //{

            //    lblStatus.Text = "Getting information.";
            //    try
            //    {
            //        ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT *FROM "+_searchClass[i]);
            //        foreach (ManagementObject obj in searcher.Get())
            //        {
            //            lblStatus.Text = "Getting information. .";
                    
            //            temp += obj.GetPropertyValue(param[i]).ToString() + "\n";
            //            if (i == _searchClass.Length - 1)
            //            {
            //                lblStatus.Text = "Done!";
            //                MessageBox.Show(temp, "Hostinfo: " + host, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                break;
            //            }
            //            lblStatus.Text = "Getting information. . .";
            //        }
            //    }
            //    catch (Exception ex) {
                   
                    
            //        MessageBox.Show("Error in WMI query.\n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
            //    } 
            //}
        }

        public void controlSys(string host, int flags)
        {
            #region 
            /*
             *Flags:
             *  0 (0x0)Log Off
             *  4 (0x4)Forced Log Off (0 + 4)
             *  1 (0x1)Shutdown
             *  5 (0x5)Forced Shutdown (1 + 4)
             *  2 (0x2)Reboot
             *  6 (0x6)Forced Reboot (2 + 4)
             *  8 (0x8)Power Off
             *  12 (0xC)Forced Power Off (8 + 4)
             */
            #endregion

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT *FROM Win32_OperatingSystem");

                foreach (ManagementObject obj in searcher.Get())
                {
                    ManagementBaseObject inParams = obj.GetMethodParameters("Win32Shutdown");

                    inParams["Flags"] = flags;

                    ManagementBaseObject outParams = obj.InvokeMethod("Win32Shutdown", inParams, null);
                }
            }
            catch (ManagementException manex) { MessageBox.Show("Error:\n\n"+manex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (UnauthorizedAccessException unaccex) { MessageBox.Show("Authorized?\n\n"+unaccex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cmdScan_Click(object sender, EventArgs e)
        {
            if (txtIP.Text == string.Empty)
            {
                MessageBox.Show("No IP address entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                myThread = new Thread(() => scan(txtIP.Text));
                myThread.Start();

                if (myThread.IsAlive == true)
                {
                    cmdStop.Enabled = true;
                    cmdScan.Enabled = false;
                    txtIP.Enabled = false;
                }
            }      
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            myThread.Suspend();
            cmdScan.Enabled = true;
            cmdStop.Enabled = false;
            txtIP.Enabled = true;
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "Idle";
        }

        private void listVAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(listVAddr.FocusedItem.Bounds.Contains(e.Location)==true)
                {
                    conMenuStripIP.Show(Cursor.Position);
                }
            }
            //else if(e.Button == MouseButtons.Left)
            //{
            //    if (listVAddr.FocusedItem.Bounds.Contains(e.Location) == true)
            //    {
            //        if (listVAddr.SelectedItems.Count > 0)
            //        {
            //            string host = listVAddr.SelectedItems[0].Text.ToString();
            //            Thread qThread = new Thread(() => query(host));
            //            qThread.Start();
            //        }
            //    }
            //}
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Forminfo = new FormInfo();
            Forminfo.HostName.Text = listVAddr.SelectedItems[0].Text.ToString();
            Forminfo.Show();
            
            //string host = listVAddr.SelectedItems[0].Text.ToString();
            //Thread qThread = new Thread(() => query(host));
            //qThread.Start();
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string host = listVAddr.SelectedItems[0].Text.ToString();
            controlSys(host, 5);
        }

        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var RemoteConnection = new RemoteConnection();
            //Forminfo.HostName.Text = listVAddr.SelectedItems[0].Text.ToString();
            RemoteConnection.Show();
            // string host = listVAddr.SelectedItems[0].Text.ToString();
            // controlSys(host, 6);
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string host = listVAddr.SelectedItems[0].Text.ToString();
            controlSys(host, 12);
        }



        public string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            System.Diagnostics.Process Process = new System.Diagnostics.Process();
            Process.StartInfo.FileName = "arp";
            Process.StartInfo.Arguments = "-a " + ipAddress;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.Start();
            string strOutput = Process.StandardOutput.ReadToEnd();
            string[] substrings = strOutput.Split('-');
            if (substrings.Length >= 8)
            {
                macAddress = substrings[3].Substring(Math.Max(0, substrings[3].Length - 2))
                         + "-" + substrings[4] + "-" + substrings[5] + "-" + substrings[6]
                         + "-" + substrings[7] + "-"
                         + substrings[8].Substring(0, 2);
                return macAddress;
            }

            else
            {
                return "OWN Machine";
            }
        }
    }
}
