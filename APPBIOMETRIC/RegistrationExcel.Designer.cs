namespace APPBIOMETRIC
{
    partial class RegistrationExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationExcel));
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridViewTables = new System.Windows.Forms.DataGridView();
            this.delete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboReaders = new System.Windows.Forms.ComboBox();
            this.lblSelectReader = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblPlaceFinger = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.textID = new System.Windows.Forms.TextBox();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.New = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.idpic = new System.Windows.Forms.PictureBox();
            this.pbFingerprint = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idpic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.Location = new System.Drawing.Point(734, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 26);
            this.label6.TabIndex = 78;
            this.label6.Text = "LOAD TABLE";
            // 
            // dataGridViewTables
            // 
            this.dataGridViewTables.AllowUserToAddRows = false;
            this.dataGridViewTables.AllowUserToOrderColumns = true;
            this.dataGridViewTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTables.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTables.Location = new System.Drawing.Point(443, 111);
            this.dataGridViewTables.Name = "dataGridViewTables";
            this.dataGridViewTables.RowHeadersWidth = 51;
            this.dataGridViewTables.RowTemplate.Height = 24;
            this.dataGridViewTables.Size = new System.Drawing.Size(727, 338);
            this.dataGridViewTables.TabIndex = 77;
            this.dataGridViewTables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTables_CellClick);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Red;
            this.delete.Location = new System.Drawing.Point(840, 466);
            this.delete.Margin = new System.Windows.Forms.Padding(4);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 28);
            this.delete.TabIndex = 75;
            this.delete.Text = "DELETE";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(39, 344);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(274, 26);
            this.label4.TabIndex = 64;
            this.label4.Text = "SCANNER CONNECTION";
            // 
            // cboReaders
            // 
            this.cboReaders.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboReaders.Location = new System.Drawing.Point(175, 386);
            this.cboReaders.Margin = new System.Windows.Forms.Padding(4);
            this.cboReaders.Name = "cboReaders";
            this.cboReaders.Size = new System.Drawing.Size(133, 24);
            this.cboReaders.TabIndex = 59;
            // 
            // lblSelectReader
            // 
            this.lblSelectReader.Location = new System.Drawing.Point(34, 386);
            this.lblSelectReader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectReader.Name = "lblSelectReader";
            this.lblSelectReader.Size = new System.Drawing.Size(161, 16);
            this.lblSelectReader.TabIndex = 58;
            this.lblSelectReader.Text = "SELECT READER:";
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Lime;
            this.Add.Location = new System.Drawing.Point(462, 466);
            this.Add.Margin = new System.Windows.Forms.Padding(4);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(100, 28);
            this.Add.TabIndex = 54;
            this.Add.Text = "ADD";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(1066, 593);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 28);
            this.btnBack.TabIndex = 55;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblPlaceFinger
            // 
            this.lblPlaceFinger.Location = new System.Drawing.Point(58, 582);
            this.lblPlaceFinger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlaceFinger.Name = "lblPlaceFinger";
            this.lblPlaceFinger.Size = new System.Drawing.Size(221, 23);
            this.lblPlaceFinger.TabIndex = 56;
            this.lblPlaceFinger.Text = "Place a Thumb finger on the reader";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 214);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "ID No.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 181);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 60;
            this.label2.Text = "Position:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(117, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 29);
            this.label5.TabIndex = 66;
            this.label5.Text = "SIGN UP";
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(79, 145);
            this.txtname.Margin = new System.Windows.Forms.Padding(4);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(132, 22);
            this.txtname.TabIndex = 52;
            // 
            // textID
            // 
            this.textID.Location = new System.Drawing.Point(79, 214);
            this.textID.Margin = new System.Windows.Forms.Padding(4);
            this.textID.Name = "textID";
            this.textID.Size = new System.Drawing.Size(132, 22);
            this.textID.TabIndex = 63;
            // 
            // txtPos
            // 
            this.txtPos.Location = new System.Drawing.Point(79, 178);
            this.txtPos.Margin = new System.Windows.Forms.Padding(4);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(132, 22);
            this.txtPos.TabIndex = 62;
            // 
            // New
            // 
            this.New.BackColor = System.Drawing.Color.LightPink;
            this.New.Location = new System.Drawing.Point(635, 466);
            this.New.Margin = new System.Windows.Forms.Padding(4);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(100, 28);
            this.New.TabIndex = 84;
            this.New.Text = "NEW";
            this.New.UseVisualStyleBackColor = false;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // update
            // 
            this.update.BackColor = System.Drawing.Color.LightSeaGreen;
            this.update.Location = new System.Drawing.Point(1041, 466);
            this.update.Margin = new System.Windows.Forms.Padding(4);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(100, 28);
            this.update.TabIndex = 85;
            this.update.Text = "UPDATE";
            this.update.UseVisualStyleBackColor = false;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.BlueViolet;
            this.save.Location = new System.Drawing.Point(729, 539);
            this.save.Margin = new System.Windows.Forms.Padding(4);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(100, 28);
            this.save.TabIndex = 86;
            this.save.Text = "EXPORT";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(267, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 26);
            this.label7.TabIndex = 88;
            this.label7.Text = "ADD YOUR IMAGE";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // idpic
            // 
            this.idpic.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.idpic.BackgroundImage = global::APPBIOMETRIC.Properties.Resources.plus_icon_black_2;
            this.idpic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.idpic.Location = new System.Drawing.Point(254, 111);
            this.idpic.Name = "idpic";
            this.idpic.Size = new System.Drawing.Size(160, 164);
            this.idpic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.idpic.TabIndex = 87;
            this.idpic.TabStop = false;
            this.idpic.Click += new System.EventHandler(this.idpic_Click);
            // 
            // pbFingerprint
            // 
            this.pbFingerprint.BackColor = System.Drawing.SystemColors.Info;
            this.pbFingerprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbFingerprint.Location = new System.Drawing.Point(82, 418);
            this.pbFingerprint.Margin = new System.Windows.Forms.Padding(4);
            this.pbFingerprint.Name = "pbFingerprint";
            this.pbFingerprint.Size = new System.Drawing.Size(158, 149);
            this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFingerprint.TabIndex = 57;
            this.pbFingerprint.TabStop = false;
            // 
            // RegistrationExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.idpic);
            this.Controls.Add(this.save);
            this.Controls.Add(this.update);
            this.Controls.Add(this.New);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridViewTables);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboReaders);
            this.Controls.Add(this.lblSelectReader);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblPlaceFinger);
            this.Controls.Add(this.pbFingerprint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.textID);
            this.Controls.Add(this.txtPos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 720);
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "RegistrationExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrationExcel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegistrationExcel_FormClosing);
            this.Load += new System.EventHandler(this.RegistrationExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idpic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridViewTables;
        internal System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox cboReaders;
        internal System.Windows.Forms.Label lblSelectReader;
        internal System.Windows.Forms.Button Add;
        internal System.Windows.Forms.Button btnBack;
        internal System.Windows.Forms.Label lblPlaceFinger;
        internal System.Windows.Forms.PictureBox pbFingerprint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox textID;
        private System.Windows.Forms.TextBox txtPos;
        internal System.Windows.Forms.Button New;
        internal System.Windows.Forms.Button update;
        internal System.Windows.Forms.Button save;
        private System.Windows.Forms.PictureBox idpic;
        internal System.Windows.Forms.Label label7;
    }
}