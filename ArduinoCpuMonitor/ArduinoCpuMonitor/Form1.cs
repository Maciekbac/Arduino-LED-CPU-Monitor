//github.com/Maciekbac
using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace ArduinoCpuMonitor
{
    public partial class Form1 : Form
    {
        const int baudRate = 19200;

        SerialPort port;
        PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        public Form1()
        {
            
            InitializeComponent();
            String[] portsList = SerialPort.GetPortNames();
            cPorts.DataSource = portsList;                
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            string selPort = cPorts.SelectedItem.ToString(); 
            port = new SerialPort(selPort);
            port.BaudRate = baudRate;
            port.Open();

            if (port.IsOpen)
            {
                timer1.Enabled = true;
                bConnect.Enabled = false;
                bDisconnect.Enabled = true;
            }
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            port.Close();
            bConnect.Enabled = true;
            bDisconnect.Enabled = false;
            progressBar.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int cpuUsage = (int)CPUCounter.NextValue();
            progressBar.Value = cpuUsage; 
            port.WriteLine(cpuUsage.ToString());
        }
    }
}
