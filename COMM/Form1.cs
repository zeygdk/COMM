using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace COMM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte[] tx_data ={ 0, 0, 0, 0, 0, 0 };
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
      

        int StartCol = 1;
        int StartRow = 1;


        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cboPort.Items.AddRange(ports);
            cboPort.SelectedIndex = 0;
            btnClose.Enabled = false;
      }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try
            {
                serialPort1.PortName = cboPort.Text;
                serialPort1.Open();
                timer1.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
            try
            {
                if(serialPort1.IsOpen)
                {
                    serialPort1.WriteLine("+" + Environment.NewLine);
                    txtMessage.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try
            {
                
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        Excel.Application excel = new Excel.Application();
        private object sheet1;

        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    txtReceive.Text = serialPort1.ReadExisting();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            serialPort1.WriteLine("+");
            txtReceive.Text = serialPort1.ReadExisting();
           serialPort1.Read(tx_data,0,6);
             for(int x=0; x<6;x++)
            {
                textBox1.Text += Convert.ToString(tx_data[x]) + Environment.NewLine;
            }
        
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
         
        }
       
    }
}
