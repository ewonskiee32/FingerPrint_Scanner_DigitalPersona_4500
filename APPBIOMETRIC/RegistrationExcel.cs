using DPUruNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace APPBIOMETRIC
{
    public partial class RegistrationExcel : Form
    {
        System.Data.DataTable table;
        int index;
        public RegistrationExcel()
        {
            InitializeComponent();

        }

        #region
        public Dictionary<int, Fmd> Fmds
        {
            get { return fmds; }
            set { fmds = value; }
        }
        private Dictionary<int, Fmd> fmds = new Dictionary<int, Fmd>();


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
            catch (Exception)
            {
            }
        }

        private Reader _reader;

        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Form_Main::StartCaptureAsync"))
            {
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

        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Form_Main::CancelCaptureAndCloseReader"))
            {
                if (currentReader != null)
                {
                    currentReader.CancelCapture();
                    currentReader.Dispose();

                    if (reset)
                    {
                        CurrentReader = null;
                    }
                }
            }
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

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                String text = ex.Message;
                text += "\r\n\r\nPlease check if DigitalPersona service has been started";
                String caption = "Cannot access readers";
                MessageBox.Show(text, caption);
            }

        }
        #endregion
        private void RegistrationExcel_Load(object sender, EventArgs e)
        {
            // Instantiate the DataTable in the Load event
            table = new System.Data.DataTable("table");
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Position", typeof(string));
            table.Columns.Add("ThumbFinger", typeof(string));
            table.Columns.Add("IndexFinger", typeof(string));
            table.Columns.Add("MiddleFinger", typeof(string));
            dataGridViewTables.RowTemplate.Height = 120;
            table.Columns.Add("IDPicture", typeof(byte[]));
            dataGridViewTables.DataSource = table;
            LoadDataFromFile();
            LoadScanners();
            firstFinger = null;
            secondFinger = null;
            thirdFinger = null;
            resultEnrollment1 = null;
            resultEnrollment2 = null;
            resultEnrollment3 = null;
            preenrollmentFmds1 = new List<Fmd>();
            preenrollmentFmds2 = new List<Fmd>();
            preenrollmentFmds3 = new List<Fmd>();
            pbFingerprint.Image = null;
            if (CurrentReader != null)
            {
                CurrentReader.Dispose();
                CurrentReader = null;
            }
            CurrentReader = _readers[cboReaders.SelectedIndex];
            if (!OpenReader())
            {
                // Handle error or close form
            }

            if (!StartCaptureAsync(this.OnCaptured))
            {
                // Handle error or close form
            }
        }

        private void LoadDataFromFile()
        {
            string dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "registry.json");
            if (File.Exists(dataFilePath))
            {
                string jsonData = File.ReadAllText(dataFilePath);
                var loadedData = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                if (loadedData != null)
                {
                    table.Clear();
                    foreach (var item in loadedData)
                    {
                        table.Rows.Add(item.ID, item.Name, item.Position, item.ThumbFinger, item.IndexFInger,item.MiddleFinger);
                    }
                }
            }
        }

        #region
        public bool OpenReader()
        {
            using (Tracer tracer = new Tracer("Form_Main::OpenReader"))
            {
                reset = false;
                DPUruNet.Constants.ResultCode result = DPUruNet.Constants.ResultCode.DP_DEVICE_FAILURE;


                result = currentReader.Open(DPUruNet.Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (result != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("Error:  " + result);
                    reset = true;
                    return false;
                }

                return true;
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

                    if ((captureResult.Quality != DPUruNet.Constants.CaptureQuality.DP_QUALITY_CANCELED))
                    {
                        throw new Exception("Quality - " + captureResult.Quality);
                    }
                    return false;
                }

                return true;
            }
        }
        private const int PROBABILITY_ONE = 0x7fffffff;
        private Fmd firstFinger;
        private Fmd secondFinger;
        private Fmd thirdFinger;
        int countFirstFinger = 0;
        int countSecondFinger = 0;
        int countThirdFinger = 0;
        DataResult<Fmd> resultEnrollment1;
        DataResult<Fmd> resultEnrollment2;
        DataResult<Fmd> resultEnrollment3;
        List<Fmd> preenrollmentFmds1;
        List<Fmd> preenrollmentFmds2;
        List<Fmd> preenrollmentFmds3;
        public void OnCaptured(CaptureResult captureResult)
        {

            try
            {
                // Check capture quality and throw an error if bad.
                if (!CheckCaptureResult(captureResult)) return;

                // Create bitmap
                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    SendMessage(Action.SendBitmap, CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }

                //Enrollment Code:
                {
                    try
                    {
                        if (firstFinger == null)
                        {

                            countFirstFinger++;
                            // Check capture quality and throw an error if bad.
                            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, DPUruNet.Constants.Formats.Fmd.ANSI);

                            if (resultConversion.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                            {
                                Reset = true;
                                throw new Exception(resultConversion.ResultCode.ToString());
                            }

                            preenrollmentFmds1.Add(resultConversion.Data);

                            if (countFirstFinger >= 2)
                            {
                                resultEnrollment1 = DPUruNet.Enrollment.CreateEnrollmentFmd(DPUruNet.Constants.Formats.Fmd.ANSI, preenrollmentFmds1);

                                if (resultEnrollment1.ResultCode == DPUruNet.Constants.ResultCode.DP_SUCCESS)
                                {
                                    firstFinger = resultEnrollment1.Data;
                                    preenrollmentFmds1.Clear();
                                    countFirstFinger = 0;
                                    // Enrollment successful for first finger, proceed to second finger
                                    MessageBox.Show("Please place your Index finger on the reader.");
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        lblPlaceFinger.Text = "Place a Index finger on the reader";
                                    });

                                }
                                else if (resultEnrollment1.ResultCode == DPUruNet.Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                                {
                                    SendMessage(Action.SendMessage, "Enrollment for first finger was unsuccessful. Please try again.");
                                    preenrollmentFmds1.Clear();
                                    countFirstFinger = 0;
                                    return;
                                }
                            }
                        }
                        else if (secondFinger == null)
                        {

                            countSecondFinger++;
                            // Check capture quality and throw an error if bad.
                            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, DPUruNet.Constants.Formats.Fmd.ANSI);

                            if (resultConversion.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                            {
                                Reset = true;
                                throw new Exception(resultConversion.ResultCode.ToString());
                            }

                            preenrollmentFmds2.Add(resultConversion.Data);

                            if (countSecondFinger >= 2)
                            {
                                resultEnrollment2 = DPUruNet.Enrollment.CreateEnrollmentFmd(DPUruNet.Constants.Formats.Fmd.ANSI, preenrollmentFmds2);

                                if (resultEnrollment2.ResultCode == DPUruNet.Constants.ResultCode.DP_SUCCESS)
                                {
                                    secondFinger = resultEnrollment2.Data;
                                    preenrollmentFmds2.Clear();
                                    countSecondFinger = 0;
                                    // Enrollment successful for second finger, proceed to third finger
                                    MessageBox.Show("Please place your Middle finger on the reader.");
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        lblPlaceFinger.Text = "Place a Middle finger on the reader";
                                    });
                                }
                                else if (resultEnrollment2.ResultCode == DPUruNet.Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                                {
                                    SendMessage(Action.SendMessage, "Enrollment for second finger was unsuccessful. Please try again.");
                                    preenrollmentFmds2.Clear();
                                    countSecondFinger = 0;
                                    return;
                                }
                            }
                        }
                        else if (thirdFinger == null)
                        {

                            countThirdFinger++;
                            // Check capture quality and throw an error if bad.
                            DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, DPUruNet.Constants.Formats.Fmd.ANSI);

                            if (resultConversion.ResultCode != DPUruNet.Constants.ResultCode.DP_SUCCESS)
                            {
                                Reset = true;
                                throw new Exception(resultConversion.ResultCode.ToString());
                            }

                            preenrollmentFmds3.Add(resultConversion.Data);

                            if (countThirdFinger >= 2)
                            {
                                resultEnrollment3 = DPUruNet.Enrollment.CreateEnrollmentFmd(DPUruNet.Constants.Formats.Fmd.ANSI, preenrollmentFmds3);

                                if (resultEnrollment3.ResultCode == DPUruNet.Constants.ResultCode.DP_SUCCESS)
                                {
                                    thirdFinger = resultEnrollment3.Data;
                                    preenrollmentFmds3.Clear();
                                    countThirdFinger = 0;
                                    // Enrollment successful for third finger
                                    MessageBox.Show("All fingers have been successfully enrolled.");
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        lblPlaceFinger.Text = "Add or Replace Fingerprint";
                                    });
                                    return;
                                }
                                else if (resultEnrollment3.ResultCode == DPUruNet.Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                                {
                                    SendMessage(Action.SendMessage, "Enrollment for third finger was unsuccessful. Please try again.");
                                    preenrollmentFmds3.Clear();
                                    countThirdFinger = 0;

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Send error message, then close form or handle as needed
                        SendMessage(Action.SendMessage, "Error: " + ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                // Send error message, then close form or handle as needed
                SendMessage(Action.SendMessage, "Error: " + ex.Message);
            }
        }


        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }
        #endregion

        private async void Add_Click(object sender, EventArgs e)
        {
            Add.BackColor = Color.White;
            await Task.Delay(1000);
            Add.BackColor = Color.Lime;

            if (!string.IsNullOrEmpty(textID.Text) && !string.IsNullOrEmpty(txtname.Text) &&
                !string.IsNullOrEmpty(txtPos.Text) && resultEnrollment1 != null &&
                resultEnrollment2 != null && resultEnrollment3 != null)
            {
                byte[] img = null; // Initialize img as null

                if (idpic.Image != null) // Check if ID picture is not null
                {
                    MemoryStream ms = new MemoryStream();
                    idpic.Image.Save(ms, idpic.Image.RawFormat);
                    img = ms.ToArray(); // Update img with the image data
                }

                // Serialize the fingerprint data for each finger
                string serializedFingerprint1 = Fmd.SerializeXml(resultEnrollment1.Data);
                string serializedFingerprint2 = Fmd.SerializeXml(resultEnrollment2.Data);
                string serializedFingerprint3 = Fmd.SerializeXml(resultEnrollment3.Data);

                // Add the data to the DataGridView
                table.Rows.Add(textID.Text, txtname.Text, txtPos.Text,
                    serializedFingerprint1, serializedFingerprint2, serializedFingerprint3, img);

                // Reset enrollment flags and clear captured fingerprint data
                resultEnrollment1 = null;
                resultEnrollment2 = null;
                resultEnrollment3 = null;
                firstFinger = null;
                secondFinger = null;
                thirdFinger = null;
                preenrollmentFmds1.Clear();
                preenrollmentFmds2.Clear();
                preenrollmentFmds3.Clear();

                // Update UI
                this.Invoke((MethodInvoker)delegate
                {
                    lblPlaceFinger.Text = "Place a Thumb finger on the reader";
                });
                pbFingerprint.Image = null;
            }
            else
            {
                MessageBox.Show("Please fill in all fields and enroll all fingers before adding.");
            }
        }

        private void New_Click(object sender, EventArgs e)
        {

            textID.Text= String.Empty;
            txtname.Text= String.Empty;
            txtPos.Text= String.Empty;
            pbFingerprint.Image = null;
        }

        private void dataGridViewTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row= dataGridViewTables.Rows[index];
            textID.Text = row.Cells[0].Value.ToString();
            txtname.Text = row.Cells[1].Value.ToString(); // corrected index for name cell
            txtPos.Text = row.Cells[2].Value.ToString(); // corrected index for position cell
        }

        private void update_Click(object sender, EventArgs e)
        {
            DataGridViewRow newdata = dataGridViewTables.Rows[index];
            newdata.Cells[0].Value = textID.Text;
            newdata.Cells[1].Value = txtname.Text;
            newdata.Cells[2].Value = txtPos.Text;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            index= dataGridViewTables.CurrentCell.RowIndex;
            dataGridViewTables.Rows.RemoveAt(index);

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            PLATFORM pform = new PLATFORM();
            pform.Show();
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application mexcel = new Microsoft.Office.Interop.Excel.Application();
                mexcel.Application.Workbooks.Add(Type.Missing);

                // Write headers to Excel
                for (int i = 1; i <= dataGridViewTables.Columns.Count; i++)
                {
                    mexcel.Cells[1, i] = dataGridViewTables.Columns[i - 1].HeaderText;
                }

                // Choose the folder to save ID pictures
                string selectedFolder = "";
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedFolder = folderBrowserDialog.SelectedPath;
                    }
                    else
                    {
                        MessageBox.Show("Please select a folder to save ID pictures.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // Exit the method if no folder is selected
                    }
                }

                // Write data to Excel and save ID pictures
                for (int i = 0; i < dataGridViewTables.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridViewTables.Columns.Count; j++)
                    {
                        if (dataGridViewTables.Columns[j].HeaderText == "IDPicture" && dataGridViewTables.Rows[i].Cells[j].Value is byte[] imgBytes)
                        {
                            Image img = byteArrayToImage(imgBytes);
                            string fileName = dataGridViewTables.Rows[i].Cells[1].Value.ToString() + ".png";
                            string filePath = Path.Combine(selectedFolder, fileName);

                            img.Save(filePath, ImageFormat.Png);
                        }
                        else
                        {
                            mexcel.Cells[i + 2, j + 1] = dataGridViewTables.Rows[i].Cells[j].Value?.ToString() ?? "";
                        }
                    }
                }

                // Autofit columns and rows, set font size, and make Excel visible
                mexcel.Columns.AutoFit();
                mexcel.Rows.AutoFit();
                mexcel.Columns.Font.Size = 12;
                mexcel.Visible = true;

                // Clean up Excel objects
                Marshal.ReleaseComObject(mexcel);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                MessageBox.Show("No record found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to convert byte array to Image
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void idpic_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                idpic.Image = Image.FromFile(opf.FileName);
            }
        }

        private void RegistrationExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelCaptureAndCloseReader(this.OnCaptured);
            SaveDataToFile();
        }
        private void SaveDataToFile()
        {
            string dataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "registry.json");
            var dataToSave = table.AsEnumerable()
                                  .Select(row => new
                                  {
                                      ID = row.Field<string>("ID"),
                                      Name = row.Field<string>("Name"),
                                      Position = row.Field<string>("Position"),
                                      ThumbFinger = row.Field<string>("ThumbFinger"),
                                      IndexFinger = row.Field<string>("IndexFinger"),
                                      MiddleFinger = row.Field<string>("MiddleFinger")
                                  })
                                  .ToList();

            string jsonData = JsonConvert.SerializeObject(dataToSave, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(dataFilePath, jsonData);
        }
    }
}
