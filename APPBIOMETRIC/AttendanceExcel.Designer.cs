namespace APPBIOMETRIC
{
    partial class AttendanceExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttendanceExcel));
            this.lblPlaceFinger = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SHOW = new System.Windows.Forms.Button();
            this.loadimage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.imageloc = new System.Windows.Forms.Label();
            this.dataload = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.timein = new System.Windows.Forms.CheckBox();
            this.timeout = new System.Windows.Forms.CheckBox();
            this.Status = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cboReaders = new System.Windows.Forms.ComboBox();
            this.lblSelectReader = new System.Windows.Forms.Label();
            this.stop = new System.Windows.Forms.Button();
            this.Worksheet = new System.Windows.Forms.Button();
            this.pbFingerprint = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPlaceFinger
            // 
            this.lblPlaceFinger.Location = new System.Drawing.Point(679, 403);
            this.lblPlaceFinger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaceFinger.Name = "lblPlaceFinger";
            this.lblPlaceFinger.Size = new System.Drawing.Size(195, 16);
            this.lblPlaceFinger.TabIndex = 25;
            this.lblPlaceFinger.Text = "\'";
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Start.Location = new System.Drawing.Point(37, 339);
            this.Start.Margin = new System.Windows.Forms.Padding(4);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(169, 36);
            this.Start.TabIndex = 77;
            this.Start.Text = "START";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gold;
            this.button1.Location = new System.Drawing.Point(236, 465);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 36);
            this.button1.TabIndex = 76;
            this.button1.Text = "DISPLAY";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SHOW
            // 
            this.SHOW.BackColor = System.Drawing.SystemColors.Control;
            this.SHOW.Location = new System.Drawing.Point(13, 523);
            this.SHOW.Margin = new System.Windows.Forms.Padding(4);
            this.SHOW.Name = "SHOW";
            this.SHOW.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SHOW.Size = new System.Drawing.Size(216, 28);
            this.SHOW.TabIndex = 79;
            this.SHOW.Text = "LOAD DATA ";
            this.SHOW.UseVisualStyleBackColor = false;
            this.SHOW.Click += new System.EventHandler(this.SHOW_Click);
            // 
            // loadimage
            // 
            this.loadimage.BackColor = System.Drawing.SystemColors.Control;
            this.loadimage.Location = new System.Drawing.Point(13, 559);
            this.loadimage.Margin = new System.Windows.Forms.Padding(4);
            this.loadimage.Name = "loadimage";
            this.loadimage.Size = new System.Drawing.Size(216, 28);
            this.loadimage.TabIndex = 78;
            this.loadimage.Text = "GO TO IMAGE FOLDER";
            this.loadimage.UseVisualStyleBackColor = false;
            this.loadimage.Click += new System.EventHandler(this.loadimage_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(34, 305);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 16);
            this.label1.TabIndex = 80;
            this.label1.Text = "Place a finger on the reader";
            // 
            // imageloc
            // 
            this.imageloc.AutoSize = true;
            this.imageloc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imageloc.Location = new System.Drawing.Point(233, 565);
            this.imageloc.Name = "imageloc";
            this.imageloc.Size = new System.Drawing.Size(100, 16);
            this.imageloc.TabIndex = 82;
            this.imageloc.Text = "LOAD FOLDER";
            // 
            // dataload
            // 
            this.dataload.AutoSize = true;
            this.dataload.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataload.Location = new System.Drawing.Point(233, 529);
            this.dataload.Name = "dataload";
            this.dataload.Size = new System.Drawing.Size(86, 16);
            this.dataload.TabIndex = 81;
            this.dataload.Text = "LOAD DATA ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Maroon;
            this.label13.Location = new System.Drawing.Point(526, 84);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(236, 26);
            this.label13.TabIndex = 84;
            this.label13.Text = "ATTENDANCE TABLE";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Tomato;
            this.button2.Location = new System.Drawing.Point(682, 465);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 36);
            this.button2.TabIndex = 85;
            this.button2.Text = "CREATE FILE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBack.Location = new System.Drawing.Point(1001, 555);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(118, 36);
            this.btnBack.TabIndex = 86;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // timein
            // 
            this.timein.AutoSize = true;
            this.timein.Location = new System.Drawing.Point(46, 455);
            this.timein.Name = "timein";
            this.timein.Size = new System.Drawing.Size(77, 20);
            this.timein.TabIndex = 87;
            this.timein.Text = "TIME IN";
            this.timein.UseVisualStyleBackColor = true;
            this.timein.CheckedChanged += new System.EventHandler(this.timein_CheckedChanged);
            // 
            // timeout
            // 
            this.timeout.AutoSize = true;
            this.timeout.Location = new System.Drawing.Point(46, 481);
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(93, 20);
            this.timeout.TabIndex = 88;
            this.timeout.Text = "TIME OUT";
            this.timeout.UseVisualStyleBackColor = true;
            this.timeout.CheckedChanged += new System.EventHandler(this.timeout_CheckedChanged);
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Font = new System.Drawing.Font("Tahoma", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.ForeColor = System.Drawing.Color.Red;
            this.Status.Location = new System.Drawing.Point(880, 32);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(187, 28);
            this.Status.TabIndex = 90;
            this.Status.Text = "Press the Start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(775, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 29);
            this.label4.TabIndex = 89;
            this.label4.Text = "Status :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(236, 129);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(799, 316);
            this.dataGridView1.TabIndex = 91;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cboReaders
            // 
            this.cboReaders.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboReaders.Location = new System.Drawing.Point(60, 134);
            this.cboReaders.Margin = new System.Windows.Forms.Padding(4);
            this.cboReaders.Name = "cboReaders";
            this.cboReaders.Size = new System.Drawing.Size(133, 24);
            this.cboReaders.TabIndex = 92;
            // 
            // lblSelectReader
            // 
            this.lblSelectReader.Location = new System.Drawing.Point(32, 114);
            this.lblSelectReader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectReader.Name = "lblSelectReader";
            this.lblSelectReader.Size = new System.Drawing.Size(161, 16);
            this.lblSelectReader.TabIndex = 93;
            this.lblSelectReader.Text = "SELECT READER:";
            // 
            // stop
            // 
            this.stop.BackColor = System.Drawing.Color.Crimson;
            this.stop.Location = new System.Drawing.Point(37, 393);
            this.stop.Margin = new System.Windows.Forms.Padding(4);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(169, 36);
            this.stop.TabIndex = 94;
            this.stop.Text = "STOP";
            this.stop.UseVisualStyleBackColor = false;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // Worksheet
            // 
            this.Worksheet.BackColor = System.Drawing.Color.Tomato;
            this.Worksheet.Location = new System.Drawing.Point(875, 465);
            this.Worksheet.Margin = new System.Windows.Forms.Padding(4);
            this.Worksheet.Name = "Worksheet";
            this.Worksheet.Size = new System.Drawing.Size(160, 36);
            this.Worksheet.TabIndex = 96;
            this.Worksheet.Text = "ADD WORKSHEET";
            this.Worksheet.UseVisualStyleBackColor = false;
            this.Worksheet.Click += new System.EventHandler(this.Worksheet_Click);
            // 
            // pbFingerprint
            // 
            this.pbFingerprint.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pbFingerprint.Location = new System.Drawing.Point(46, 166);
            this.pbFingerprint.Margin = new System.Windows.Forms.Padding(4);
            this.pbFingerprint.Name = "pbFingerprint";
            this.pbFingerprint.Size = new System.Drawing.Size(147, 135);
            this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFingerprint.TabIndex = 24;
            this.pbFingerprint.TabStop = false;
            // 
            // AttendanceExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 618);
            this.Controls.Add(this.Worksheet);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.lblSelectReader);
            this.Controls.Add(this.cboReaders);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeout);
            this.Controls.Add(this.timein);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.imageloc);
            this.Controls.Add(this.dataload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SHOW);
            this.Controls.Add(this.loadimage);
            this.Controls.Add(this.pbFingerprint);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPlaceFinger);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AttendanceExcel";
            this.Text = "AttendanceExcel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AttendanceExcel_FormClosing);
            this.Load += new System.EventHandler(this.AttendanceExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox pbFingerprint;
        internal System.Windows.Forms.Label lblPlaceFinger;
        internal System.Windows.Forms.Button Start;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Button SHOW;
        internal System.Windows.Forms.Button loadimage;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label imageloc;
        private System.Windows.Forms.Label dataload;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.CheckBox timein;
        private System.Windows.Forms.CheckBox timeout;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.ComboBox cboReaders;
        internal System.Windows.Forms.Label lblSelectReader;
        internal System.Windows.Forms.Button stop;
        internal System.Windows.Forms.Button Worksheet;
    }
}