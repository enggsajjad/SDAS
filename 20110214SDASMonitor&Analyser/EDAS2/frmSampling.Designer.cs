namespace SDAS
{
    partial class frmSampling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSampling));
            this.cmbSampleRate = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rdoMinPhase = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoLinPhase = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cmbSampleRate
            // 
            this.cmbSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleRate.FormattingEnabled = true;
            this.cmbSampleRate.Items.AddRange(new object[] {
            "1",
            "10",
            "20",
            "50",
            "100",
            "125",
            "200",
            "500"});
            this.cmbSampleRate.Location = new System.Drawing.Point(120, 23);
            this.cmbSampleRate.Name = "cmbSampleRate";
            this.cmbSampleRate.Size = new System.Drawing.Size(67, 21);
            this.cmbSampleRate.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(254, 81);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(255, 23);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 26);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rdoMinPhase
            // 
            this.rdoMinPhase.AutoSize = true;
            this.rdoMinPhase.Location = new System.Drawing.Point(32, 66);
            this.rdoMinPhase.Name = "rdoMinPhase";
            this.rdoMinPhase.Size = new System.Drawing.Size(176, 17);
            this.rdoMinPhase.TabIndex = 13;
            this.rdoMinPhase.TabStop = true;
            this.rdoMinPhase.Text = "Minimum Phase FIR Digital Filter";
            this.rdoMinPhase.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sample Rate (Hz)";
            // 
            // rdoLinPhase
            // 
            this.rdoLinPhase.AutoSize = true;
            this.rdoLinPhase.Location = new System.Drawing.Point(32, 89);
            this.rdoLinPhase.Name = "rdoLinPhase";
            this.rdoLinPhase.Size = new System.Drawing.Size(164, 17);
            this.rdoLinPhase.TabIndex = 15;
            this.rdoLinPhase.TabStop = true;
            this.rdoLinPhase.Text = "Linear Phase FIR Digital Filter";
            this.rdoLinPhase.UseVisualStyleBackColor = true;
            // 
            // frmSampling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 132);
            this.Controls.Add(this.rdoLinPhase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoMinPhase);
            this.Controls.Add(this.cmbSampleRate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSampling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample Rate and Digitall Filter";
            this.Load += new System.EventHandler(this.frmSampling_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSampleRate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rdoMinPhase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoLinPhase;
    }
}