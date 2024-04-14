using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPUruNet;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace Classification
{
    public partial class Form1 : Form
    {
        private int thumbCaptureCount = 0;
        private int indexCaptureCount = 0;
        private int middleCaptureCount = 0;
        public Form1()
        {
            InitializeComponent();
        }
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
                Constants.ResultCode result = currentReader.GetStatus();

                if ((result != Constants.ResultCode.DP_SUCCESS))
                {
                    reset = true;
                    throw new Exception("" + result);
                }

                if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
                {
                    Thread.Sleep(50);
                }
                else if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    currentReader.Calibrate();
                }
                else if ((currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
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

                    Constants.ResultCode captureResult = currentReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                    if (captureResult != Constants.ResultCode.DP_SUCCESS)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Reset variables
            LoadScanners();
            LoadScanners();
            finger1.Image = null;
            finger2.Image = null;
            finger3.Image = null;
            finger4.Image = null;
            finger5.Image = null;
            finger6.Image = null;
            finger7.Image = null;
            finger8.Image = null;
            finger9.Image = null;
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
       
        public bool OpenReader()
        {
            using (Tracer tracer = new Tracer("Form_Main::OpenReader"))
            {
                reset = false;
                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;


                result = currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (result != Constants.ResultCode.DP_SUCCESS)
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
                if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        reset = true;
                        throw new Exception(captureResult.ResultCode.ToString());
                    }

                    
                    if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                    {
                        throw new Exception("Quality - " + captureResult.Quality);
                    }
                    return false;
                }

                return true;
            }
        }

        public void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                if (!CheckCaptureResult(captureResult)) return;

                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    // Display the captured image in different picture boxes based on finger and capture count
                    if (thumbCaptureCount < 3)
                    {
                        if (thumbCaptureCount == 0)
                            finger1.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        else if (thumbCaptureCount == 1)
                            finger2.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        else if (thumbCaptureCount == 2)
                            finger3.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);

                        thumbCaptureCount++;
                    }
                    else if (indexCaptureCount < 3)
                    {
                        if (indexCaptureCount == 0)
                            finger4.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        else if (indexCaptureCount == 1)
                            finger5.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        else if (indexCaptureCount == 2)
                            finger6.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);

                        indexCaptureCount++;
                    }
                    else if (middleCaptureCount < 3)
                    {
                        if (middleCaptureCount == 0)
                            finger7.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        else if (middleCaptureCount == 1)
                            finger8.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        else if (middleCaptureCount == 2)
                            finger9.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);

                        middleCaptureCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelCaptureAndCloseReader(this.OnCaptured);
        }

        private void btnCalculateMetrics_Click(object sender, EventArgs e)
        {
            // Check if all captures have been completed
            if (thumbCaptureCount == 3 && indexCaptureCount == 3 && middleCaptureCount == 3)
            {
                // Calculate metrics for Thumb
                double similarityScoreThumb = CalculateSimilarityScore(finger1.Image, finger2.Image, finger3.Image);

                // Calculate metrics for Index
                double similarityScoreIndex = CalculateSimilarityScore(finger4.Image, finger5.Image, finger6.Image);

                // Calculate metrics for Middle
                double similarityScoreMiddle = CalculateSimilarityScore(finger7.Image, finger8.Image, finger9.Image);

                // Display the similarity scores
                MessageBox.Show($"Similarity score for Thumb finger: {ConvertToPercentage(similarityScoreThumb):F2}%\n" +
                                $"Similarity score for Index finger: {ConvertToPercentage(similarityScoreIndex):F2}%\n" +
                                $"Similarity score for Middle finger: {ConvertToPercentage(similarityScoreMiddle):F2}%");
            }
            else
            {
                MessageBox.Show("Please capture all three images for Thumb, Index, and Middle fingers first.");
            }
        }

        private double CalculateSimilarityScore(Image image1, Image image2, Image image3)
        {
            // Check if all images are available
            if (image1 != null && image2 != null && image3 != null)
            {
                // Convert images to grayscale bitmaps for comparison
                Bitmap bmp1 = ToGrayscaleBitmap(image1);
                Bitmap bmp2 = ToGrayscaleBitmap(image2);
                Bitmap bmp3 = ToGrayscaleBitmap(image3);

                // Calculate MSE between each pair of images
                double mse12 = CalculateMSE(bmp1, bmp2);
                double mse13 = CalculateMSE(bmp1, bmp3);
                double mse23 = CalculateMSE(bmp2, bmp3);

                // Calculate average MSE
                double avgMSE = (mse12 + mse13 + mse23) / 3.0;

                // Calculate similarity score (1 - avgMSE) for similarity
                return 1 - avgMSE;
            }
            else
            {
                MessageBox.Show("Some images are missing for metric calculation.");
                return 0; // Return 0 if images are missing
            }
        }

        private double ConvertToPercentage(double similarityScore)
        {
            // Convert similarity score to percentage
            double percentage = similarityScore * 100;

            // Round off to tens and ensure it's positive
            percentage = Math.Round(Math.Abs(percentage), 1);

            return percentage;
        }

        private Bitmap ToGrayscaleBitmap(Image image)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            return bmp;
        }

        private double CalculateMSE(Bitmap bmp1, Bitmap bmp2)
        {
            double sumSquaredDiff = 0;

            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    Color color1 = bmp1.GetPixel(x, y);
                    Color color2 = bmp2.GetPixel(x, y);

                    int diff = (color1.R - color2.R) + (color1.G - color2.G) + (color1.B - color2.B);
                    sumSquaredDiff += diff * diff;
                }
            }

            double mse = sumSquaredDiff / (bmp1.Width * bmp1.Height);
            return mse;
        }

        private void saveimage_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save Finger Images";
            saveFileDialog.FileName = nametext.Text; // Set the default file name based on nametext.Text
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                // Save thumb images
                SaveImage(finger1.Image, saveFileDialog.FileName + "_Thumb_1");
                SaveImage(finger2.Image, saveFileDialog.FileName + "_Thumb_2");
                SaveImage(finger3.Image, saveFileDialog.FileName + "_Thumb_3");

                // Save index finger images
                SaveImage(finger4.Image, saveFileDialog.FileName + "_Index_1");
                SaveImage(finger5.Image, saveFileDialog.FileName + "_Index_2");
                SaveImage(finger6.Image, saveFileDialog.FileName + "_Index_3");

                // Save middle finger images
                SaveImage(finger7.Image, saveFileDialog.FileName + "_Middle_1");
                SaveImage(finger8.Image, saveFileDialog.FileName + "_Middle_2");
                SaveImage(finger9.Image, saveFileDialog.FileName + "_Middle_3");

                MessageBox.Show("Images saved successfully.");
            }
        }
        private void SaveImage(Image image, string fileName)
        {
            if (image != null)
            {
                image.Save(fileName + ".png", ImageFormat.Png); // Save the image as PNG format
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            // Reset capture counts after capturing three times for each finger
            if (thumbCaptureCount == 3 && indexCaptureCount == 3 && middleCaptureCount == 3)
            {
                thumbCaptureCount = 0;
                indexCaptureCount = 0;
                middleCaptureCount = 0;
                finger1.Image = null;
                finger2.Image = null;
                finger3.Image = null;
                finger4.Image = null;
                finger5.Image = null;
                finger6.Image = null;
                finger7.Image = null;
                finger8.Image = null;
                finger9.Image = null;

                MessageBox.Show("Images and capture counts reset successfully.");
            }
            else
            {
                MessageBox.Show("Please capture all three images for Thumb, Index, and Middle fingers first.");
            }
        }
    }
}
