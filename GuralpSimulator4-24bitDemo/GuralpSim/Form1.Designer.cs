namespace GuralpSim
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStreamID1 = new System.Windows.Forms.TextBox();
            this.txtSysID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.com = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtAmp1 = new System.Windows.Forms.TextBox();
            this.txtFreq1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkSignal1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(105, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Amp";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(175, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Freq";
            // 
            // txtStreamID1
            // 
            this.txtStreamID1.Location = new System.Drawing.Point(52, 144);
            this.txtStreamID1.MaxLength = 2;
            this.txtStreamID1.Name = "txtStreamID1";
            this.txtStreamID1.Size = new System.Drawing.Size(40, 20);
            this.txtStreamID1.TabIndex = 13;
            this.txtStreamID1.Text = "Z1";
            // 
            // txtSysID
            // 
            this.txtSysID.Location = new System.Drawing.Point(141, 61);
            this.txtSysID.MaxLength = 4;
            this.txtSysID.Name = "txtSysID";
            this.txtSysID.Size = new System.Drawing.Size(87, 20);
            this.txtSysID.TabIndex = 12;
            this.txtSysID.Text = "ICCC";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(49, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "System ID";
            // 
            // com
            // 
            this.com.BaudRate = 57600;
            this.com.PortName = "COM2";
            this.com.WriteBufferSize = 6144;
            // 
            // timer1
            // 
            this.timer1.Interval = 995;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(280, 62);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 29);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(280, 125);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(88, 29);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtAmp1
            // 
            this.txtAmp1.Location = new System.Drawing.Point(98, 144);
            this.txtAmp1.Name = "txtAmp1";
            this.txtAmp1.Size = new System.Drawing.Size(60, 20);
            this.txtAmp1.TabIndex = 8;
            this.txtAmp1.Text = "500";
            // 
            // txtFreq1
            // 
            this.txtFreq1.Location = new System.Drawing.Point(168, 144);
            this.txtFreq1.Name = "txtFreq1";
            this.txtFreq1.Size = new System.Drawing.Size(60, 20);
            this.txtFreq1.TabIndex = 9;
            this.txtFreq1.Text = "1.0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(58, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 16);
            this.label11.TabIndex = 34;
            this.label11.Text = "Ch";
            // 
            // chkSignal1
            // 
            this.chkSignal1.AutoSize = true;
            this.chkSignal1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSignal1.Location = new System.Drawing.Point(52, 96);
            this.chkSignal1.Name = "chkSignal1";
            this.chkSignal1.Size = new System.Drawing.Size(110, 17);
            this.chkSignal1.TabIndex = 35;
            this.chkSignal1.Text = "X1 = Asin(2πft)";
            this.chkSignal1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Single Channel GCF Packet Simulation";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 195);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSignal1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtStreamID1);
            this.Controls.Add(this.txtSysID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFreq1);
            this.Controls.Add(this.txtAmp1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Guralp Simulator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAmp1;
        private System.Windows.Forms.TextBox txtFreq1;
        private System.Windows.Forms.TextBox txtStreamID1;
        private System.Windows.Forms.TextBox txtSysID;
        private System.Windows.Forms.Label label10;
        private System.IO.Ports.SerialPort com;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkSignal1;
        private System.Windows.Forms.Label label1;
    }
}

