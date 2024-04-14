using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace APPBIOMETRIC
{
    public partial class Display_Attendance : Form
    {


        public Display_Attendance(string name, string position, string id, string time, string imageFilePath,string Stat)
        {
            InitializeComponent();
            UpdateData(name, position, id, time, imageFilePath, Stat);
        }
        
        
        public async void UpdateData(string newName, string newPosition, string newID, string newtime, string newimageFilePath, string newstat)
        {
            // Update the labels or textboxes on the form with the new data
            nametext.Text = " Name: " + newName;
            postext.Text = " Barangay Position: " + newPosition;
            idtext.Text = " ID No. - " + newID;
            timeinout.Text = newtime;
            status1.Text = newstat;
            if (newstat == "Accepted")
            {
                status1.BackColor = Color.Green;
                await Task.Delay(2000);
                status1.BackColor = Color.Gray;
            }
            if(newstat == "Error")
            {
                status1.BackColor = Color.Red;
                await Task.Delay(2000);
                status1.BackColor = Color.Gray;
            }
            if (!string.IsNullOrEmpty(newimageFilePath))
            {
                newimageFilePath = Path.Combine(newimageFilePath, newName + ".png"); // Assuming PNG format
                if (File.Exists(newimageFilePath))
                {
                    // Display the image in the picturebox
                    picid.Image = Image.FromFile(newimageFilePath);
                }
                else
                {
                    picid.Image = null;
                }
            }
            else
            {
                picid.Image = null;
            }

            await Task.Delay(2000); // Delay for 3 seconds

            // Reset the labels and picturebox
            nametext.Text = " Name:___ ";
            postext.Text = " Barangay Position:___ ";
            idtext.Text = " ID No. -___ ";
            timeinout.Text = " Time :";
            picid.Image = null;
            status1.Text = "___";
        }


        private void Display_Attendance_Load(object sender, EventArgs e)
        {
            daytime1.Start();
            DateTime currentTime = DateTime.Now;
            daytime.Text = currentTime.ToString("dddd hh:mm:ss tt");
            Date1.Text = "Date: " + currentTime.ToString("MMMM dd yyy");
        }

        private void daytime1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            daytime.Text = currentTime.ToString("[dddd] hh:mm:ss tt");
            daytime1.Start();
        }
    }
}
