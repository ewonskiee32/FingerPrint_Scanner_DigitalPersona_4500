using System;
using System.Windows.Forms;
using DPUruNet;

namespace APPBIOMETRIC
{
    public partial class PLATFORM : Form
    {
        private ReaderCollection readers;
        private bool scannerConnected = false;
        public static PLATFORM Instance = new PLATFORM();
        public PLATFORM()
        {
            InitializeComponent();
            // Start a timer to check scanner status periodically
            Timer scannerTimer = new Timer();
            scannerTimer.Interval = 1000;
            scannerTimer.Tick += ScannerTimer_Tick;
            scannerTimer.Start();

        }
        private void ScannerTimer_Tick(object sender, EventArgs e)
        {
            InitializeScanner();
        }
        private void InitializeScanner()
        {
            try
            {
                readers = ReaderCollection.GetReaders();
                if (readers.Count > 0)
                {
                    labelStatus.Text = "The Scanner is Connected";
                    Register.Enabled = true;
                    Attend.Enabled = true;
                    scannerConnected = true;
                }
                else
                {
                    labelStatus.Text = "Please Connect your Digital Persona 4500 Scanner";
                    Register.Enabled = false;
                    Attend.Enabled = false;
                    scannerConnected = false;
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Error: " + ex.Message;
            }
        }


        private void PLATFORM_Load(object sender, EventArgs e)
        {

        }


        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PLATFORM_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            RegistrationExcel registra =  new RegistrationExcel();
            registra.Show();
            this.Hide();
        }

        private void Attend_Click(object sender, EventArgs e)
        {
            AttendanceExcel attendance = new AttendanceExcel();
            attendance.Show();
            this.Hide();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Microsoft.Win32.SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (e.Mode == Microsoft.Win32.PowerModes.StatusChange)
            {
                InitializeScanner();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Microsoft.Win32.SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
            }
            base.Dispose(disposing);
        }
    }
}
