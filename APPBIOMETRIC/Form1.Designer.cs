namespace APPBIOMETRIC
{
    partial class PLATFORM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLATFORM));
            this.Exit = new APPBIOMETRIC.ROUNDBUTTON();
            this.Attend = new APPBIOMETRIC.ROUNDBUTTON();
            this.Register = new APPBIOMETRIC.ROUNDBUTTON();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Crimson;
            this.Exit.Background = System.Drawing.Color.Crimson;
            this.Exit.BorderColor = System.Drawing.Color.DarkRed;
            this.Exit.BorderRadius = 40;
            this.Exit.BorderSize = 0;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.ForeColor = System.Drawing.Color.Black;
            this.Exit.Location = new System.Drawing.Point(120, 262);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(150, 40);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "EXIT";
            this.Exit.TextColor = System.Drawing.Color.Black;
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Attend
            // 
            this.Attend.BackColor = System.Drawing.Color.YellowGreen;
            this.Attend.Background = System.Drawing.Color.YellowGreen;
            this.Attend.BorderColor = System.Drawing.Color.DarkRed;
            this.Attend.BorderRadius = 40;
            this.Attend.BorderSize = 0;
            this.Attend.FlatAppearance.BorderSize = 0;
            this.Attend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Attend.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Attend.ForeColor = System.Drawing.Color.Black;
            this.Attend.Location = new System.Drawing.Point(120, 206);
            this.Attend.Name = "Attend";
            this.Attend.Size = new System.Drawing.Size(150, 40);
            this.Attend.TabIndex = 1;
            this.Attend.Text = "ATTENDANCE";
            this.Attend.TextColor = System.Drawing.Color.Black;
            this.Attend.UseVisualStyleBackColor = false;
            this.Attend.Click += new System.EventHandler(this.Attend_Click);
            // 
            // Register
            // 
            this.Register.BackColor = System.Drawing.Color.IndianRed;
            this.Register.Background = System.Drawing.Color.IndianRed;
            this.Register.BorderColor = System.Drawing.Color.DarkRed;
            this.Register.BorderRadius = 40;
            this.Register.BorderSize = 0;
            this.Register.FlatAppearance.BorderSize = 0;
            this.Register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Register.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Register.ForeColor = System.Drawing.Color.Black;
            this.Register.Location = new System.Drawing.Point(120, 137);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(150, 40);
            this.Register.TabIndex = 0;
            this.Register.Text = "REGISTRATION";
            this.Register.TextColor = System.Drawing.Color.Black;
            this.Register.UseVisualStyleBackColor = false;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("MV Boli", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(12, 51);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 18);
            this.labelStatus.TabIndex = 3;
            // 
            // PLATFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 353);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Attend);
            this.Controls.Add(this.Register);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(430, 400);
            this.MinimumSize = new System.Drawing.Size(430, 400);
            this.Name = "PLATFORM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BIOMETRIC FINGERPRINT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PLATFORM_FormClosing);
            this.Load += new System.EventHandler(this.PLATFORM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ROUNDBUTTON Register;
        private ROUNDBUTTON Attend;
        private ROUNDBUTTON Exit;
        private System.Windows.Forms.Label labelStatus;
    }
}

