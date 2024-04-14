using DPUruNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using static DPUruNet.Constants;

namespace APPBIOMETRIC
{
    public partial class AttendanceExcel : Form
    {
        int index;
        public string NAME = "___";
        public string POS = "___";
        public string IDN = "___";
        public string TIME = " Time ";
        public string Stat = "---";
        private string excelFilePath = "";
        public string imageFilePath = "no";
        private System.Data.DataTable excelData;
        System.Data.DataTable table = new System.Data.DataTable("table");
        public AttendanceExcel()
        {
            InitializeComponent();
        }
        private Reader currentReader;
        public Reader CurrentReader
        {
            get { return currentReader; }
            set
            {
                currentReader = value;
                SendMessage(Action.UpdateReaderState, value);
            }
        }
        private ReaderCollection _readers;
        private void LoadScanners()
        {
            cboReaders.Text = string.Empty;
            cboReaders.Items.Clear();
            cboReaders.SelectedIndex = -1;

            try
            {
                _readers = ReaderCollection.GetReaders();

                foreach (Reader Reader in _readers)
                {
                    cboReaders.Items.Add(Reader.Description.Name);
                }

                if (cboReaders.Items.Count > 0)
                {
                    cboReaders.SelectedIndex = 0;
                    //btnCaps.Enabled = true;
                    //btnSelect.Enabled = true;
                }
                else
                {
                    //btnSelect.Enabled = false;
                    //btnCaps.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //message box:
                String text = ex.Message;
                text += "\r\n\r\nPlease check if DigitalPersona service has been started";
                String caption = "Cannot access readers";
                MessageBox.Show(text, caption);
            }
        }
        private const int PROBABILITY_ONE = 0x7fffffff;
        private DPUruNet.Fmd firstFinger;

        public bool OpenReader()
        {
            using (Tracer tracer = new Tracer("Form_Main::OpenReader"))
            {
                reset = false;
                DPUruNet.Constants.ResultCode result = DPUruNet.Constants.ResultCode.DP_DEVICE_FAILURE;

                if (currentReader == null)
                {
                    MessageBox.Show("Error: currentReader is null.");
                    reset = true;
                    return false;
                }

                try
                {
                    // Open reader
                    result = currentReader.Open(DPUruNet.Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                    if (result != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                    {
                        MessageBox.Show("Error: " + result);
                        reset = true;
                        return false;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening reader: " + ex.Message);
                    reset = true;
                    return false;
                }
            }
        }



        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            using (Tracer tracer = new Tracer("Form_Main::CheckCaptureResult"))
            {
                if (captureResult.Data == null || captureResult.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                {
                    if (captureResult.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                    {
                        reset = true;
                        throw new Exception(captureResult.ResultCode.ToString());
                    }

                    // Send message if quality shows fake finger
                    if ((captureResult.Quality != DPUruNet.Constants.CaptureQuality.DP_QUALITY_CANCELED))
                    {
                        throw new Exception("Quality - " + captureResult.Quality);
                    }
                    return false;
                }

                return true;
            }
        }


        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Form_Main::StartCaptureAsync"))
            {
                if (currentReader == null)
                {
                    MessageBox.Show("Error: currentReader is null.");
                    return false;
                }

                // Activate capture handler
                currentReader.On_Captured += new Reader.CaptureCallback(OnCaptured);

                // Call capture
                if (!CaptureFingerAsync())
                {
                    return false;
                }

                return true;
            }
        }


        public void GetStatus()
        {
            using (Tracer tracer = new Tracer("Form_Main::GetStatus"))
            {
                DPUruNet.Constants.ResultCode result = currentReader.GetStatus();

                if ((result != DPUruNet.Constants.ResultCode.DP_SUCCESS))
                {
                    reset = true;
                    throw new Exception("" + result);
                }

                if ((currentReader.Status.Status == DPUruNet.Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);
                }
                else if ((currentReader.Status.Status == DPUruNet.Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    currentReader.Calibrate();
                }
                else if ((currentReader.Status.Status != DPUruNet.Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    throw new Exception("Reader Status - " + currentReader.Status.Status);
                }
            }
        }

        public bool CaptureFingerAsync()
        {
            using (Tracer tracer = new Tracer("Form_Main::CaptureFingerAsync"))
            {
                try
                {
                    GetStatus();

                    DPUruNet.Constants.ResultCode captureResult = currentReader.CaptureAsync(DPUruNet.Constants.Formats.Fid.ANSI, DPUruNet.Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                    if (captureResult != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                    {
                        reset = true;
                        throw new Exception("" + captureResult);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:  " + ex.Message);
                    return false;
                }
            }
        }

        private Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i < bytes.Length; i++)
            {
                rgbBytes[i * 3] = bytes[i];
                rgbBytes[i * 3 + 1] = bytes[i];
                rgbBytes[i * 3 + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i < bmp.Height; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        public void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                // Check capture quality and throw an error if bad.
                if (!CheckCaptureResult(captureResult)) return;

                // Create bitmap
                foreach (DPUruNet.Fid.Fiv fiv in captureResult.Data.Views)
                {
                    SendMessage(Action.SendBitmap, CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }

                // Verification Code
                try
                {
                    DataResult<DPUruNet.Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, DPUruNet.Constants.Formats.Fmd.ANSI);
                    if (resultConversion.ResultCode != ResultCode.DP_SUCCESS)
                    {
                        if (resultConversion.ResultCode != ResultCode.DP_TOO_SMALL_AREA)
                        {
                            reset = true;
                        }
                        throw new Exception(resultConversion.ResultCode.ToString());
                    }

                    firstFinger = resultConversion.Data;

                }
                catch (Exception ex)
                {
                    SendMessage(Action.SendMessage, "Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                SendMessage(Action.SendMessage, "Error: " + ex.Message);
            }
        }

        public Dictionary<int, DPUruNet.Fmd> Fmds
        {
            get { return fmds; }
            set { fmds = value; }
        }
        private Dictionary<int, DPUruNet.Fmd> fmds = new Dictionary<int, DPUruNet.Fmd>();


        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        private bool reset;


        private enum Action
        {
            UpdateReaderState,
            SendBitmap,
            SendMessage
        }
        private delegate void SendMessageCallback(Action state, object payload);
        private void SendMessage(Action action, object payload)
        {
            try
            {
                if (this.pbFingerprint.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            MessageBox.Show((string)payload);
                            break;
                        case Action.SendBitmap:
                            pbFingerprint.Image = (Bitmap)payload;
                            pbFingerprint.Refresh();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
            }
        }

        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Form_Main::CancelCaptureAndCloseReader"))
            {
                if (currentReader != null)
                {
                    currentReader.CancelCapture();

                    // Dispose of reader handle and unhook reader events.
                    currentReader.Dispose();

                    if (reset)
                    {
                        CurrentReader = null;
                    }
                }
            }
        }
        private void AttendanceExcel_Load(object sender, EventArgs e)
        {
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Position", typeof(string));
            table.Columns.Add("Timein", typeof(string));
            table.Columns.Add("Timeout", typeof(string));
            dataGridView1.DataSource = table;
            LoadDataFromFile();
            // Reset variables
            LoadScanners();
            firstFinger = null;
            //resultEnrollment = null;
            //preenrollmentFmds = new List<Fmd>();
            pbFingerprint.Image = null;
            if (CurrentReader != null)
            {
                CurrentReader.Dispose();
                CurrentReader = null;
            }
            CurrentReader = _readers[cboReaders.SelectedIndex];
            if (!OpenReader())
            {
                //this.Close();
            }

            if (!StartCaptureAsync(this.OnCaptured))
            {
                //this.Close();
            }
        }
        private void LoadDataFromFile()
        {
            string dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data1.json");
            if (File.Exists(dataFilePath))
            {
                string jsonData = File.ReadAllText(dataFilePath);
                var loadedData = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                if (loadedData != null)
                {
                    table.Clear();
                    foreach (var item in loadedData)
                    {
                        table.Rows.Add(item.ID, item.Name, item.Position, item.Timein, item.Timeout);
                    }
                }
            }
        }
        private System.Data.DataTable LoadExcelData(string filePath)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(filePath);
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = excelWorkbook.Sheets[1]; // Assuming the data is in the first worksheet

                System.Data.DataTable dataTable = new System.Data.DataTable();

                int rows = excelWorksheet.UsedRange.Rows.Count;
                int cols = excelWorksheet.UsedRange.Columns.Count;

                // Add columns to DataTable
                for (int col = 1; col <= cols; col++)
                {
                    string columnName = excelWorksheet.Cells[1, col].Value.ToString();
                    dataTable.Columns.Add(columnName);
                }

                // Add rows to DataTable
                for (int row = 2; row <= rows; row++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int col = 1; col <= cols; col++)
                    {
                        dataRow[col - 1] = excelWorksheet.Cells[row, col].Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }

                excelWorkbook.Close();
                excelApp.Quit();

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Excel data: " + ex.Message);
                return null;
            }
        }

        private bool isLooping = false;
        private async void Button3_Click(object sender, EventArgs e)
        {
            Start.BackColor = Color.Green;
            MessageBox.Show("Starting Scanning, Press OK");
            isLooping = true;

            try
            {
                while (isLooping) // Continuously loop until the flag is set to false
                {
                    if (firstFinger != null)
                    {
                        // Load Excel data if file path is set
                        if (!string.IsNullOrEmpty(excelFilePath))
                        {
                            excelData = LoadExcelData(excelFilePath);
                        }

                        if (excelData != null && excelData.Rows.Count > 0)
                        {
                            foreach (DataRow row in excelData.Rows)
                            {
                                // Extract data from Excel rows
                                string id = row["ID"].ToString();
                                string name = row["Name"].ToString();
                                string position = row["Position"].ToString();
                                string thumbFingerData = row["ThumbFinger"].ToString();
                                string indexFingerData = row["IndexFinger"].ToString();
                                string midFingerData = row["MiddleFinger"].ToString();

                                // Deserialize finger data
                                DPUruNet.Fmd thumbVal = DPUruNet.Fmd.DeserializeXml(thumbFingerData);
                                DPUruNet.Fmd indexVal = DPUruNet.Fmd.DeserializeXml(indexFingerData);
                                DPUruNet.Fmd midVal = DPUruNet.Fmd.DeserializeXml(midFingerData);

                                // Perform fingerprint comparison for each finger
                                CompareResult compareThumb = Comparison.Compare(firstFinger, 0, thumbVal, 0);
                                CompareResult compareIndex = Comparison.Compare(firstFinger, 0, indexVal, 0);
                                CompareResult compareMid = Comparison.Compare(firstFinger, 0, midVal, 0);

                                // Check if any comparison failed
                                if (compareThumb.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS ||
                                    compareIndex.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS ||
                                    compareMid.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                                {
                                    Reset = true;
                                    UpdateStatus("Error", Color.Red);
                                    Stat = "Error";
                                    pbFingerprint.Image = null;
                                    firstFinger = null;
                                    break; // Exit the loop if any comparison fails
                                }

                                // Handle comparison results
                                bool thumbMatch = Convert.ToDouble(compareThumb.Score.ToString()) == 0;
                                bool indexMatch = Convert.ToDouble(compareIndex.Score.ToString()) == 0;
                                bool midMatch = Convert.ToDouble(compareMid.Score.ToString()) == 0;

                                // Handle comparison result
                                if (thumbMatch || indexMatch || midMatch)
                                {
                                    // Check if the TimeIn checkbox is checked
                                    if (timein.Checked)
                                    {
                                        bool recordExists = false;
                                        foreach (DataRow rowToUpdate in table.Rows)
                                        {
                                            if (name == rowToUpdate["Name"].ToString())
                                            {
                                                recordExists = true;
                                                break; // Exit the loop if a record with the same name is found
                                            }
                                        }

                                        if (!recordExists)
                                        {
                                            // Perform actions for accepted fingerprint
                                            NAME = name;
                                            IDN = id;
                                            POS = position;
                                            Stat = "Accepted";
                                            SendDataToDisplayAttendance();
                                            UpdateStatus("Accepted", Color.Green);
                                            table.Rows.Add(id, name, position);
                                            DateTime currentTime = DateTime.Now;
                                            string timeIn = currentTime.ToString("hh:mm tt"); // 12-hour format
                                            table.Rows[table.Rows.Count - 1]["Timein"] = timeIn;
                                            TIME = "Time In: " + timeIn;
                                            pbFingerprint.Image = null;
                                            firstFinger = null;
                                        }
                                        else
                                        {
                                            Stat = "Already Recorded!";
                                            UpdateStatus("Already Recorded!", Color.Green);
                                            pbFingerprint.Image = null;
                                            firstFinger = null;
                                        }
                                    }

                                    // Check if the Timeout checkbox is checked
                                    if (timeout.Checked)
                                    {
                                        foreach (DataRow rowToUpdate in table.Rows)
                                        {
                                            if (name == rowToUpdate["Name"].ToString() && !string.IsNullOrEmpty(rowToUpdate["Timeout"].ToString()))
                                            {
                                                Stat = "Already Recorded!";
                                                UpdateStatus("Already Recorded!", Color.Green);
                                                pbFingerprint.Image = null;
                                                firstFinger = null;
                                                break;
                                            }
                                            if (name == rowToUpdate["Name"].ToString())
                                            {
                                                // Perform actions for accepted fingerprint
                                                NAME = name;
                                                IDN = id;
                                                POS = position;
                                                Stat = "Accepted";
                                                SendDataToDisplayAttendance();
                                                UpdateStatus("Accepted", Color.Green);
                                                DateTime currentTime1 = DateTime.Now;
                                                string timeoutValue = currentTime1.ToString("hh:mm tt"); // 12-hour format
                                                rowToUpdate["Timeout"] = timeoutValue;
                                                TIME = "Time Out: " + timeoutValue;
                                                pbFingerprint.Image = null;
                                                firstFinger = null;
                                                break;
                                            }
                                        }
                                    }

                                    break; // Exit the loop after processing the fingerprint
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: Excel data not loaded or empty.");
                        }
                        await Task.Delay(1000); // Delay for 1 second before the next iteration
                    }
                    else
                    {
                        await Task.Delay(1000); // Delay for 1 second before the next iteration
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateStatus(string text, Color color)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Status.Text = text;
                Status.ForeColor = color;
                Task.Delay(2000).ContinueWith(t =>
                {
                    Status.Invoke((MethodInvoker)delegate
                    {
                        Status.Text = "";
                    });
                }); 
            });
        }

        private void SHOW_Click(object sender, EventArgs e)
        {
            SHOW.BackColor = Color.Yellow;
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Excel Files Only | *.xlsx; *.xls";
                op.Title = " Choose the File";
                if (op.ShowDialog() == DialogResult.OK)
                    dataload.Text = op.FileName;
                    excelFilePath = op.FileName; // Set Excel file path
            }
        }

        private void timein_CheckedChanged(object sender, EventArgs e)
        {
            if (timein.Checked == true) 
            {
                timeout.Checked = false;
            }
        }

        private void timeout_CheckedChanged(object sender, EventArgs e)
        {
            if (timeout.Checked == true)
            {
                timein.Checked = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];

        }

        private void stop_Click(object sender, EventArgs e)
        {
            Start.BackColor = Color.DarkGoldenrod;
            isLooping = false;
            MessageBox.Show("Stop Scanning, Press OK");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Worksheet worksheet = workbook.ActiveSheet as Worksheet;

                // Set the sheet name to the current date
                worksheet.Name = DateTime.Now.ToString("yyyy-MM-dd");

                // Add column headers to Excel
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                // Add data rows to Excel
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            // Handle the case when the cell value is null, such as assigning a default value or skipping the cell
                            worksheet.Cells[i + 2, j + 1] = "N/A"; // Example: Assigning "N/A" for null values
                        }
                    }
                }

                // Save the Excel file
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                    saveFileDialog.Title = "Save Excel File";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        workbook.SaveAs(filePath);
                        workbook.Close();
                        excelApp.Quit(); // Close the Excel application

                        MessageBox.Show("Data saved to Excel successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Open the saved Excel file
                        Process.Start(filePath);
                    }
                }
            }
            else
            {
                MessageBox.Show("No record Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadimage_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    imageloc.Text = selectedFolder;
                    imageFilePath = selectedFolder;
                }
            }
        }

        // Add this method to send data to the Display_Attendance form
        private void SendDataToDisplayAttendance()
        {
            // Check if the variables NAME, POS, and IDN are not empty
            if (!string.IsNullOrEmpty(NAME) && !string.IsNullOrEmpty(POS) && !string.IsNullOrEmpty(IDN) && !string.IsNullOrEmpty(TIME) && !string.IsNullOrEmpty(Stat))
            {
                // If the attendanceForm is null or closed, create a new instance
                if (attendanceForm == null || attendanceForm.IsDisposed)
                {
                    attendanceForm = new Display_Attendance(NAME, POS, IDN, TIME, imageFilePath,Stat);
                }
                else
                {
                    // If the form is already open, update its data and labels
                    attendanceForm.UpdateData(NAME, POS, IDN, TIME, imageFilePath,Stat); // Pass imageFilePath to UpdateData
                }
            }
            else
            {
                return;
            }
        }

        // Define a global variable to track the Display_Attendance form
        private Display_Attendance attendanceForm;
        private void button1_Click(object sender, EventArgs e)
        {

            SendDataToDisplayAttendance();
            attendanceForm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            PLATFORM pform = new PLATFORM();
            pform.Show();
            this.Close();
        }

        private void Worksheet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook;
                Microsoft.Office.Interop.Excel.Worksheet worksheet;

                // Prompt the user to select an existing Excel file
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openFileDialog.Title = "Select Excel File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    workbook = excelApp.Workbooks.Open(filePath);
                    int sheetNumber = 1;

                    // Check if the sheet name already exists
                    string sheetName = DateTime.Now.ToString("MMM_dd_yyyy");
                    while (WorksheetExists(workbook, sheetName))
                    {
                        sheetNumber++;
                        sheetName = DateTime.Now.ToString("MMM_dd_yyyy") + "_" + sheetNumber;
                    }

                    worksheet = workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
                    worksheet.Name = sheetName;

                    // Add column headers to Excel
                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }

                    // Find the next available row in the worksheet
                    int nextRow = worksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell).Row + 1;

                    // Add data rows to Excel starting from the next available row
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[nextRow + i, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                // Handle the case when the cell value is null, such as assigning a default value or skipping the cell
                                worksheet.Cells[nextRow + i, j + 1] = "N/A"; // Example: Assigning "N/A" for null values
                            }
                        }
                    }

                    // Save the changes to the Excel file
                    workbook.Save();
                    workbook.Close();
                    excelApp.Quit();

                    MessageBox.Show("Data added to Excel successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Excel file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No record Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to check if a worksheet exists in a workbook
        private bool WorksheetExists(Microsoft.Office.Interop.Excel.Workbook workbook, string sheetName)
        {
            foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name == sheetName)
                {
                    return true;
                }
            }
            return false;
        }

        private void AttendanceExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelCaptureAndCloseReader(this.OnCaptured);
            SaveDataToFile();
        }
        private void SaveDataToFile()
        {
            string dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data1.json");
            var dataToSave = table.AsEnumerable()
                                  .Select(row => new
                                  {
                                      ID = row.Field<string>("ID"),
                                      Name = row.Field<string>("Name"),
                                      Position = row.Field<string>("Position"),
                                      Timein = row.Field<string>("Timein"),
                                      Timeout = row.Field<string>("Timeout"),
                                  })
                                  .ToList();

            string jsonData = JsonConvert.SerializeObject(dataToSave, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(dataFilePath, jsonData);
        }
    }
}
