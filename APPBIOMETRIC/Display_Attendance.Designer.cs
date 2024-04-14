namespace APPBIOMETRIC
{
    partial class Display_Attendance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Display_Attendance));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nametext = new System.Windows.Forms.Label();
            this.picid = new System.Windows.Forms.PictureBox();
            this.postext = new System.Windows.Forms.Label();
            this.idtext = new System.Windows.Forms.Label();
            this.timeinout = new System.Windows.Forms.Label();
            this.Date1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.status1 = new System.Windows.Forms.Label();
            this.daytime = new System.Windows.Forms.Label();
            this.daytime1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picid)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::APPBIOMETRIC.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(63, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(192, 171);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(248, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(887, 156);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sangguniang Barangay\r\nBalayong, Bauan Batangas\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nametext
            // 
            this.nametext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nametext.AutoSize = true;
            this.nametext.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametext.Location = new System.Drawing.Point(500, 239);
            this.nametext.Name = "nametext";
            this.nametext.Size = new System.Drawing.Size(186, 58);
            this.nametext.TabIndex = 2;
            this.nametext.Text = "Name :";
            // 
            // picid
            // 
            this.picid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picid.BackColor = System.Drawing.SystemColors.Control;
            this.picid.Location = new System.Drawing.Point(244, 239);
            this.picid.Name = "picid";
            this.picid.Size = new System.Drawing.Size(250, 306);
            this.picid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picid.TabIndex = 3;
            this.picid.TabStop = false;
            // 
            // postext
            // 
            this.postext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.postext.AutoSize = true;
            this.postext.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.postext.Location = new System.Drawing.Point(500, 311);
            this.postext.Name = "postext";
            this.postext.Size = new System.Drawing.Size(458, 58);
            this.postext.TabIndex = 4;
            this.postext.Text = "Barangay Position :";
            // 
            // idtext
            // 
            this.idtext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idtext.AutoSize = true;
            this.idtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idtext.Location = new System.Drawing.Point(237, 564);
            this.idtext.Name = "idtext";
            this.idtext.Size = new System.Drawing.Size(152, 39);
            this.idtext.TabIndex = 5;
            this.idtext.Text = " ID No. - ";
            // 
            // timeinout
            // 
            this.timeinout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.timeinout.AutoSize = true;
            this.timeinout.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeinout.Location = new System.Drawing.Point(512, 396);
            this.timeinout.Name = "timeinout";
            this.timeinout.Size = new System.Drawing.Size(355, 58);
            this.timeinout.TabIndex = 6;
            this.timeinout.Text = "TIME IN/OUT :\r";
            this.timeinout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Date1
            // 
            this.Date1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Date1.AutoSize = true;
            this.Date1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Date1.Location = new System.Drawing.Point(527, 477);
            this.Date1.Name = "Date1";
            this.Date1.Size = new System.Drawing.Size(144, 48);
            this.Date1.TabIndex = 7;
            this.Date1.Text = "DATE:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1100, 497);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 48);
            this.label7.TabIndex = 8;
            this.label7.Text = "Status:";
            // 
            // status1
            // 
            this.status1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.status1.AutoSize = true;
            this.status1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.status1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status1.Location = new System.Drawing.Point(1247, 497);
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(48, 48);
            this.status1.TabIndex = 9;
            this.status1.Text = "S";
            // 
            // daytime
            // 
            this.daytime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.daytime.AutoSize = true;
            this.daytime.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.daytime.Location = new System.Drawing.Point(974, 631);
            this.daytime.Name = "daytime";
            this.daytime.Size = new System.Drawing.Size(231, 48);
            this.daytime.TabIndex = 10;
            this.daytime.Text = "[DAY]TIME";
            this.daytime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // daytime1
            // 
            this.daytime1.Tick += new System.EventHandler(this.daytime1_Tick);
            // 
            // Display_Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::APPBIOMETRIC.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1478, 673);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.daytime);
            this.Controls.Add(this.status1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Date1);
            this.Controls.Add(this.timeinout);
            this.Controls.Add(this.idtext);
            this.Controls.Add(this.postext);
            this.Controls.Add(this.picid);
            this.Controls.Add(this.nametext);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Display_Attendance";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Display_Attendance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Display_Attendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nametext;
        private System.Windows.Forms.PictureBox picid;
        private System.Windows.Forms.Label postext;
        private System.Windows.Forms.Label idtext;
        private System.Windows.Forms.Label timeinout;
        private System.Windows.Forms.Label Date1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label status1;
        private System.Windows.Forms.Label daytime;
        private System.Windows.Forms.Timer daytime1;
    }
}