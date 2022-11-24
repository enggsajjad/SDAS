using System;
namespace SDAS
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
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
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
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.zg2 = new ZedGraph.ZedGraphControl();
            this.zg3 = new ZedGraph.ZedGraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolRealMonitor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolUpChan1 = new System.Windows.Forms.ToolStripButton();
            this.toolDownChan1 = new System.Windows.Forms.ToolStripButton();
            this.toolUpChan2 = new System.Windows.Forms.ToolStripButton();
            this.toolDownChan2 = new System.Windows.Forms.ToolStripButton();
            this.toolUpChan3 = new System.Windows.Forms.ToolStripButton();
            this.toolDownChan3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolLeft = new System.Windows.Forms.ToolStripButton();
            this.toolRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.toolLblTimeInfo = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssComPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssSampling = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssSystemID = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssTimeInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaving = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSiteId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSampleRate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocalCOM = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.comport = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zg1
            // 
            this.zg1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zg1.ForeColor = System.Drawing.Color.Blue;
            this.zg1.Location = new System.Drawing.Point(10, 50);
            this.zg1.Name = "zg1";
            this.zg1.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg1.ScrollGrace = 0;
            this.zg1.ScrollMaxX = 0;
            this.zg1.ScrollMaxY = 0;
            this.zg1.ScrollMaxY2 = 0;
            this.zg1.ScrollMinX = 0;
            this.zg1.ScrollMinY = 0;
            this.zg1.ScrollMinY2 = 0;
            this.zg1.Size = new System.Drawing.Size(1008, 215);
            this.zg1.TabIndex = 0;
            this.zg1.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zg1_MouseDownEvent);
            this.zg1.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zg1_MouseUpEvent);
            // 
            // zg2
            // 
            this.zg2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.zg2.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg2.Location = new System.Drawing.Point(10, 260);
            this.zg2.Name = "zg2";
            this.zg2.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg2.ScrollGrace = 0;
            this.zg2.ScrollMaxX = 0;
            this.zg2.ScrollMaxY = 0;
            this.zg2.ScrollMaxY2 = 0;
            this.zg2.ScrollMinX = 0;
            this.zg2.ScrollMinY = 0;
            this.zg2.ScrollMinY2 = 0;
            this.zg2.Size = new System.Drawing.Size(1008, 215);
            this.zg2.TabIndex = 1;
            this.zg2.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zg2_MouseDownEvent);
            this.zg2.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zg2_MouseUpEvent);
            // 
            // zg3
            // 
            this.zg3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.zg3.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg3.Location = new System.Drawing.Point(10, 470);
            this.zg3.Name = "zg3";
            this.zg3.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg3.ScrollGrace = 0;
            this.zg3.ScrollMaxX = 0;
            this.zg3.ScrollMaxY = 0;
            this.zg3.ScrollMaxY2 = 0;
            this.zg3.ScrollMinX = 0;
            this.zg3.ScrollMinY = 0;
            this.zg3.ScrollMinY2 = 0;
            this.zg3.Size = new System.Drawing.Size(1008, 215);
            this.zg3.TabIndex = 2;
            this.zg3.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zg3_MouseDownEvent);
            this.zg3.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zg3_MouseUpEvent);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpenFile,
            this.toolStripSeparator4,
            this.toolSaveFile,
            this.toolRealMonitor,
            this.toolStripSeparator1,
            this.toolUpChan1,
            this.toolDownChan1,
            this.toolUpChan2,
            this.toolDownChan2,
            this.toolUpChan3,
            this.toolDownChan3,
            this.toolStripSeparator2,
            this.toolLeft,
            this.toolRight,
            this.toolStripSeparator3,
            this.toolExit,
            this.toolLblTimeInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1022, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolOpenFile
            // 
            this.toolOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("toolOpenFile.Image")));
            this.toolOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpenFile.Name = "toolOpenFile";
            this.toolOpenFile.Size = new System.Drawing.Size(23, 22);
            this.toolOpenFile.Text = "toolStripButton1";
            this.toolOpenFile.ToolTipText = "Load the Stored File";
            this.toolOpenFile.Click += new System.EventHandler(this.toolOpenFile_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSaveFile
            // 
            this.toolSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolSaveFile.Enabled = false;
            this.toolSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("toolSaveFile.Image")));
            this.toolSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSaveFile.Name = "toolSaveFile";
            this.toolSaveFile.Size = new System.Drawing.Size(23, 22);
            this.toolSaveFile.Text = "toolStripButton2";
            this.toolSaveFile.ToolTipText = "Save the data to files";
            this.toolSaveFile.Click += new System.EventHandler(this.toolSaveFile_Click);
            // 
            // toolRealMonitor
            // 
            this.toolRealMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRealMonitor.Enabled = false;
            this.toolRealMonitor.Image = ((System.Drawing.Image)(resources.GetObject("toolRealMonitor.Image")));
            this.toolRealMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRealMonitor.Name = "toolRealMonitor";
            this.toolRealMonitor.Size = new System.Drawing.Size(23, 22);
            this.toolRealMonitor.Text = "toolStripButton5";
            this.toolRealMonitor.ToolTipText = "Real Time data monitoring";
            this.toolRealMonitor.Click += new System.EventHandler(this.toolRealMonitor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolUpChan1
            // 
            this.toolUpChan1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolUpChan1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolUpChan1.Image = ((System.Drawing.Image)(resources.GetObject("toolUpChan1.Image")));
            this.toolUpChan1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUpChan1.Name = "toolUpChan1";
            this.toolUpChan1.Size = new System.Drawing.Size(23, 22);
            this.toolUpChan1.Text = "toolStripButton4";
            this.toolUpChan1.ToolTipText = "Channel1 Y-axis Zoom in";
            this.toolUpChan1.Click += new System.EventHandler(this.toolUpChan1_Click);
            // 
            // toolDownChan1
            // 
            this.toolDownChan1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDownChan1.Image = ((System.Drawing.Image)(resources.GetObject("toolDownChan1.Image")));
            this.toolDownChan1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDownChan1.Name = "toolDownChan1";
            this.toolDownChan1.Size = new System.Drawing.Size(23, 22);
            this.toolDownChan1.Text = "toolStripButton6";
            this.toolDownChan1.ToolTipText = "Channel1 Y-axis Zoom Out";
            this.toolDownChan1.Click += new System.EventHandler(this.toolDownChan1_Click);
            // 
            // toolUpChan2
            // 
            this.toolUpChan2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolUpChan2.Image = ((System.Drawing.Image)(resources.GetObject("toolUpChan2.Image")));
            this.toolUpChan2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUpChan2.Name = "toolUpChan2";
            this.toolUpChan2.Size = new System.Drawing.Size(23, 22);
            this.toolUpChan2.Text = "toolStripButton3";
            this.toolUpChan2.ToolTipText = "Channel2 Y-axis Zoom In";
            this.toolUpChan2.Click += new System.EventHandler(this.toolUpChan2_Click);
            // 
            // toolDownChan2
            // 
            this.toolDownChan2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDownChan2.Image = ((System.Drawing.Image)(resources.GetObject("toolDownChan2.Image")));
            this.toolDownChan2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDownChan2.Name = "toolDownChan2";
            this.toolDownChan2.Size = new System.Drawing.Size(23, 22);
            this.toolDownChan2.Text = "toolStripButton4";
            this.toolDownChan2.ToolTipText = "Channel2 Y-axis Zoom Out";
            this.toolDownChan2.Click += new System.EventHandler(this.toolDownChan2_Click);
            // 
            // toolUpChan3
            // 
            this.toolUpChan3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolUpChan3.Image = ((System.Drawing.Image)(resources.GetObject("toolUpChan3.Image")));
            this.toolUpChan3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUpChan3.Name = "toolUpChan3";
            this.toolUpChan3.Size = new System.Drawing.Size(23, 22);
            this.toolUpChan3.Text = "toolStripButton7";
            this.toolUpChan3.ToolTipText = "Channel3 Y-axis Zoom In";
            this.toolUpChan3.Click += new System.EventHandler(this.toolUpChan3_Click);
            // 
            // toolDownChan3
            // 
            this.toolDownChan3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDownChan3.Image = ((System.Drawing.Image)(resources.GetObject("toolDownChan3.Image")));
            this.toolDownChan3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDownChan3.Name = "toolDownChan3";
            this.toolDownChan3.Size = new System.Drawing.Size(23, 22);
            this.toolDownChan3.Text = "toolStripButton4";
            this.toolDownChan3.ToolTipText = "Channel3 Y-axis Zoom Out";
            this.toolDownChan3.Click += new System.EventHandler(this.toolDownChan3_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolLeft
            // 
            this.toolLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolLeft.Image")));
            this.toolLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.Size = new System.Drawing.Size(23, 22);
            this.toolLeft.Text = "toolStripButton3";
            this.toolLeft.ToolTipText = "X-axis Zoom In";
            this.toolLeft.Click += new System.EventHandler(this.toolLeft_Click);
            // 
            // toolRight
            // 
            this.toolRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRight.Image = ((System.Drawing.Image)(resources.GetObject("toolRight.Image")));
            this.toolRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRight.Name = "toolRight";
            this.toolRight.Size = new System.Drawing.Size(23, 22);
            this.toolRight.Text = "toolStripButton4";
            this.toolRight.ToolTipText = "X-axis Zoom Out";
            this.toolRight.Click += new System.EventHandler(this.toolRight_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolExit
            // 
            this.toolExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(23, 22);
            this.toolExit.Text = "toolStripButton4";
            this.toolExit.ToolTipText = "Exit the Application";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // toolLblTimeInfo
            // 
            this.toolLblTimeInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolLblTimeInfo.AutoSize = false;
            this.toolLblTimeInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolLblTimeInfo.Name = "toolLblTimeInfo";
            this.toolLblTimeInfo.Size = new System.Drawing.Size(170, 22);
            this.toolLblTimeInfo.Text = "                              ";
            this.toolLblTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssStatus,
            this.ssComPort,
            this.ssSampling,
            this.ssSystemID,
            this.ssTimeInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 685);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1022, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ssStatus
            // 
            this.ssStatus.AutoSize = false;
            this.ssStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(430, 17);
            this.ssStatus.Text = "Status:  Waiting for user...";
            this.ssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssComPort
            // 
            this.ssComPort.AutoSize = false;
            this.ssComPort.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ssComPort.Name = "ssComPort";
            this.ssComPort.Size = new System.Drawing.Size(120, 17);
            this.ssComPort.Text = "Port:  ";
            this.ssComPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssSampling
            // 
            this.ssSampling.AutoSize = false;
            this.ssSampling.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ssSampling.Name = "ssSampling";
            this.ssSampling.Size = new System.Drawing.Size(120, 17);
            this.ssSampling.Text = "Sampling:  ";
            this.ssSampling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssSystemID
            // 
            this.ssSystemID.AutoSize = false;
            this.ssSystemID.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ssSystemID.Name = "ssSystemID";
            this.ssSystemID.Size = new System.Drawing.Size(120, 17);
            this.ssSystemID.Text = "Sid:  ";
            this.ssSystemID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssTimeInfo
            // 
            this.ssTimeInfo.AutoSize = false;
            this.ssTimeInfo.Name = "ssTimeInfo";
            this.ssTimeInfo.Size = new System.Drawing.Size(200, 17);
            this.ssTimeInfo.Text = "Time:  ";
            this.ssTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1022, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.mnuCapture,
            this.mnuSaving,
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpen.Size = new System.Drawing.Size(178, 22);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuCapture
            // 
            this.mnuCapture.Enabled = false;
            this.mnuCapture.Name = "mnuCapture";
            this.mnuCapture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuCapture.Size = new System.Drawing.Size(178, 22);
            this.mnuCapture.Text = "Capture";
            this.mnuCapture.Click += new System.EventHandler(this.mnuCapture_Click);
            // 
            // mnuSaving
            // 
            this.mnuSaving.Enabled = false;
            this.mnuSaving.Name = "mnuSaving";
            this.mnuSaving.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSaving.Size = new System.Drawing.Size(178, 22);
            this.mnuSaving.Text = "Saving";
            this.mnuSaving.Click += new System.EventHandler(this.mnuSaving_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuExit.Size = new System.Drawing.Size(178, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSiteId,
            this.mnuSampleRate,
            this.mnuLocalCOM});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // mnuSiteId
            // 
            this.mnuSiteId.Name = "mnuSiteId";
            this.mnuSiteId.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuSiteId.Size = new System.Drawing.Size(206, 22);
            this.mnuSiteId.Text = "Site Id";
            this.mnuSiteId.Click += new System.EventHandler(this.mnuSiteId_Click);
            // 
            // mnuSampleRate
            // 
            this.mnuSampleRate.Name = "mnuSampleRate";
            this.mnuSampleRate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSampleRate.Size = new System.Drawing.Size(206, 22);
            this.mnuSampleRate.Text = "Sample Rate";
            this.mnuSampleRate.Click += new System.EventHandler(this.mnuSampleRate_Click);
            // 
            // mnuLocalCOM
            // 
            this.mnuLocalCOM.Name = "mnuLocalCOM";
            this.mnuLocalCOM.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuLocalCOM.Size = new System.Drawing.Size(206, 22);
            this.mnuLocalCOM.Text = "Local COM";
            this.mnuLocalCOM.Click += new System.EventHandler(this.mnuLocalCOM_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuAbout.Size = new System.Drawing.Size(166, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // comport
            // 
            this.comport.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.DataRec);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1022, 707);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.zg3);
            this.Controls.Add(this.zg2);
            this.Controls.Add(this.zg1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDAS Monitor & Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ZedGraph.ZedGraphControl zg1;
        private ZedGraph.ZedGraphControl zg2;
        private ZedGraph.ZedGraphControl zg3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolOpenFile;
        private System.Windows.Forms.ToolStripButton toolSaveFile;
        private System.Windows.Forms.ToolStripButton toolUpChan2;
        private System.Windows.Forms.ToolStripButton toolUpChan1;
        private System.Windows.Forms.ToolStripButton toolRealMonitor;
        private System.Windows.Forms.ToolStripButton toolDownChan1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolDownChan2;
        private System.Windows.Forms.ToolStripButton toolUpChan3;
        private System.Windows.Forms.ToolStripButton toolDownChan3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolLeft;
        private System.Windows.Forms.ToolStripButton toolRight;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel ssSystemID;
        private System.Windows.Forms.ToolStripStatusLabel ssTimeInfo;
        private System.Windows.Forms.ToolStripStatusLabel ssSampling;
        private System.Windows.Forms.ToolStripLabel toolLblTimeInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuCapture;
        private System.Windows.Forms.ToolStripMenuItem mnuSaving;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSiteId;
        private System.Windows.Forms.ToolStripMenuItem mnuSampleRate;
        private System.Windows.Forms.ToolStripMenuItem mnuLocalCOM;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.IO.Ports.SerialPort comport;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel ssComPort;
	}
}

