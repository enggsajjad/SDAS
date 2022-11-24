using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using ZedGraph;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic ;
namespace SDAS
{
	public partial class frmMain : Form
	{
        List<byte> all = new List<byte>();
        bool rtPcktFinding = true;
        int pcktWithErr = 0;
        // Off Line Variables
        int[] pcktTime = new int[3650];
        PointPairList pcktList1 = new PointPairList();
        PointPairList pcktList2 = new PointPairList();
        PointPairList pcktList3 = new PointPairList();
        // Real Time Variables
        LineItem rtCurve1, rtCurve2, rtCurve3;
        IPointListEdit rtList1, rtList2, rtList3;
        Scale rtXScale;
        const int P_SIZE = 468;
        byte[] PlotBuff = new byte[P_SIZE];
        //byte[] TempBuff = new byte[P_SIZE];
        //byte[] rtSerBuff = new byte[1024];
        int rtTotBytes;//rtTotBytesRead;
        int rtSid, rtSidNew;
        int rtSRate, rtSRateNew;
        int rtPcktTime;
        double rtXValue = 0;
        string rtPcktTimeText;
        string rtFileName = "";
        string rtPath = "";
        int rtAmp = 0, rtPreHour = 0;
        // used in serial event handler
        int rtState = 0, rtCnt = 0, rtCntOld = 0;
        int rtPcktCRC = 0, rtTempSum = 0;
        // Other Variables
        bool OffLineMode = false, SavingMode = false;
        // Axis Max/Min Settings
        int y1_axis = 32, y2_axis = 32, y3_axis = 32, x_axis=2;
        int[] xList = new int[] { 3, 10, 30, 100, 300, 1000, 3000, 3600 };

        frmSerial  fm1 = new frmSerial();

        public frmMain()
		{
            InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e )
		{
            
            // zg1
            // Hide the XAxis scale and title
            zg1.GraphPane.XAxis.Title.IsVisible = false;
            zg1.GraphPane.XAxis.Scale.IsVisible = false;
            // Hide the legend, border, and GraphPane title
            zg1.GraphPane.Legend.IsVisible = false;
            zg1.GraphPane.Border.IsVisible = false;
            zg1.GraphPane.Title.IsVisible = false;
            // Get rid of the tics that are outside the chart rect
            zg1.GraphPane.XAxis.MajorTic.IsOutside = false;
            zg1.GraphPane.XAxis.MinorTic.IsOutside = false;
            // Show the X grids
            zg1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zg1.GraphPane.XAxis.MinorGrid.IsVisible = true;
            
            // Remove all margins
            zg1.GraphPane.Margin.All = 0;
            //leave some top margin on the first GraphPane
            zg1.GraphPane.Margin.Top = 20;
            zg1.GraphPane.Margin.Bottom = 10;
            // This sets the minimum amount of space for the left and right side, respectively
            // The reason for this is so that the ChartRect's all end up being the same size.
            zg1.GraphPane.YAxis.MinSpace = 110;
            zg1.GraphPane.Y2Axis.MinSpace = 20;
            zg1.GraphPane.YAxis.Scale.Align = AlignP.Inside;
            //  zg2
            // Hide the XAxis scale and title
            zg2.GraphPane.XAxis.Title.IsVisible = false;
            zg2.GraphPane.XAxis.Scale.IsVisible = false;
            // Hide the legend, border, and GraphPane title
            zg2.GraphPane.Legend.IsVisible = false;
            zg2.GraphPane.Border.IsVisible = false;
            zg2.GraphPane.Title.IsVisible = false;
            // Get rid of the tics that are outside the chart rect
            zg2.GraphPane.XAxis.MajorTic.IsOutside = false;
            zg2.GraphPane.XAxis.MinorTic.IsOutside = false;
            // Show the X grids
            zg2.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zg2.GraphPane.XAxis.MinorGrid.IsVisible = true;
            // Remove all margins
            zg2.GraphPane.Margin.All = 0;
            //leave some top margin on the first GraphPane
            zg2.GraphPane.Margin.Top = 10;
            zg2.GraphPane.Margin.Bottom = 10;
            // And some bottom margin on the last GraphPane
            // Also, show the X title and scale on the last GraphPane only
            //zg2.GraphPane.YAxis.Scale.IsSkipLastLabel = true;
            // This sets the minimum amount of space for the left and right side, respectively
            // The reason for this is so that the ChartRect's all end up being the same size.
            zg2.GraphPane.YAxis.MinSpace = 110;
            zg2.GraphPane.Y2Axis.MinSpace = 20;

            zg2.GraphPane.YAxis.Scale.Align = AlignP.Inside;
            //zg3
            // Hide the XAxis scale and title
            zg3.GraphPane.XAxis.Title.IsVisible = false;
            zg3.GraphPane.XAxis.Scale.IsVisible = false;
            // Hide the legend, border, and GraphPane title
            zg3.GraphPane.Legend.IsVisible = false;
            zg3.GraphPane.Border.IsVisible = false;
            zg3.GraphPane.Title.IsVisible = false;
            // Get rid of the tics that are outside the chart rect
            zg3.GraphPane.XAxis.MajorTic.IsOutside = false;
            zg3.GraphPane.XAxis.MinorTic.IsOutside = false;
            // Show the X grids
            zg3.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zg3.GraphPane.XAxis.MinorGrid.IsVisible = true;
            // Remove all margins
            zg3.GraphPane.Margin.All = 0;
            //leave some top margin on the first GraphPane
            zg3.GraphPane.Margin.Top = 10;
            // And some bottom margin on the last GraphPane
            // Also, show the X title and scale on the last GraphPane only
            zg3.GraphPane.XAxis.Title.IsVisible = true;
            zg3.GraphPane.XAxis.Scale.IsVisible = true;
            zg3.GraphPane.Margin.Bottom = 10;

            //zg3.GraphPane.YAxis.Scale.IsSkipLastLabel = true;
            // This sets the minimum amount of space for the left and right side, respectively
            // The reason for this is so that the ChartRect's all end up being the same size.
            zg3.GraphPane.YAxis.MinSpace = 110;
            zg3.GraphPane.Y2Axis.MinSpace = 20;

            zg3.GraphPane.YAxis.Scale.Align = AlignP.Inside;
            //
            zgParamRealTime();
            // Load the Form
            if (fm1.ShowDialog() == DialogResult.OK)
            {
                // Enable the RealTime Monitor ICON
                toolRealMonitor.Enabled = true;
                mnuCapture.Enabled = true;
                // Set the port's settings
                comport.BaudRate = int.Parse(fm1.Controls[1].Text);
                comport.DataBits = 8;
                comport.StopBits = StopBits.One;
                comport.Parity = Parity.None;
                comport.PortName = fm1.Controls[0].Text;
                comport.ReceivedBytesThreshold = 468;//1
            }
            // Generate the Directory For Events File
            rtPath = @"C:\";
            rtPath = System.IO.Path.Combine(rtPath, "Events");
            System.IO.Directory.CreateDirectory(rtPath);
            
        }

        #region Form Resize and ZedGraph Resize
		private void RefreshGraph()
		{
            zg1.Refresh();
            zg2.Refresh();
            zg3.Refresh();
        }
        #endregion

        #region Plots the 3 Channel Data, Read from the EVT File
        private void OfflinePlot()
        {
            GraphPane myPane;
            LineItem myCurve;
            // Creat First Channel
            myPane = zg1.GraphPane;
            myCurve = myPane.AddCurve("", pcktList1, Color.Black, SymbolType.None);
            zg1.AxisChange();
            // Creat 2nd Channel
            myPane = zg2.GraphPane;
            myCurve = myPane.AddCurve("", pcktList2, Color.Black, SymbolType.None);
            zg2.AxisChange();
            // Creat 3rd Channel
            myPane = zg3.GraphPane;
            myCurve = myPane.AddCurve("", pcktList3, Color.Black, SymbolType.None);
            zg3.AxisChange();
            //Resize
            RefreshGraph();
        }
        #endregion

        #region Read all the packets from the EVT File
        private int ReadEVT(string fileName)
        {
            byte[] pckt = new byte[468];
            int pcktIndex=0, pcktAmp=0;
            double pcktX=0;
            int fileSid=0,fileSR=0,fileTime=0,fileSize=0;
            BinaryReader dataIn;
            // Now, read the data.
            try
            {
                dataIn = new
                BinaryReader(new FileStream(@fileName, FileMode.Open));
            }
            catch (IOException exc)
            {
                MessageBox.Show("Cannot Open File For Input");
                MessageBox.Show(exc.Message);
                return (0);
            }//End Try
            try
            {
                pcktIndex = 0;
                pcktX = 0;
                pcktWithErr = 0;
                pcktList1.Clear();
                pcktList2.Clear();
                pcktList3.Clear();
                while (dataIn.PeekChar() >= 0)
                {
                    pckt = dataIn.ReadBytes(468);
                    fileSize++;

                    // Calculate the CRC
                    rtTempSum = 0;
                    for (int d = 4; d < 466; d += 2)
                        rtTempSum = rtTempSum + (pckt[d + 1] << 8) + pckt[d];

                    // Check CRC, and add to corresponding List
                    rtPcktCRC = (pckt[467] << 8) + pckt[466];
                    rtTempSum = ((rtTempSum ^ 0xffff) + 1) & 0xffff;

                    if (rtPcktCRC != rtTempSum) { pcktWithErr++;}
                    
                    // Packet Time
                    pcktTime [pcktIndex] = (pckt[13] << 24) + (pckt[12] << 16) + (pckt[11] << 8) + pckt[10];

                    for (int d = 0; d < 50; d++)
                    {
                        // Get Channel No 1 Data ,Make it Signed Extended
                        pcktAmp = (pckt[18 + d * 3] << 24) + (pckt[17 + d * 3] << 16) + (pckt[16 + d * 3] << 8);
                        pcktAmp = pcktAmp >> 8;
                        // Add the Corresponding List
                        pcktList1.Add(pcktX, (double)pcktAmp);
                        // Get Channel No 2 Data ,Make it Signed Extended
                        pcktAmp = (pckt[168 + d * 3] << 24) + (pckt[167 + d * 3] << 16) + (pckt[166 + d * 3] << 8);
                        pcktAmp = pcktAmp >> 8;
                        // Add the Corresponding List
                        pcktList2.Add(pcktX, (double)pcktAmp);
                        // Get Channel No 3 Data ,Make it Signed Extended
                        pcktAmp = (pckt[318 + d * 3] << 24) + (pckt[317 + d * 3] << 16) + (pckt[316 + d * 3] << 8);
                        pcktAmp = pcktAmp >> 8;
                        // Add the Corresponding List
                        pcktList3.Add(pcktX, (double)pcktAmp);
                        // Increment the X Axis Value
                        pcktX = pcktX + 0.02;
                    }//for
                    pcktIndex++;
                }//while
                // Update Status Tabs
                ssStatus.Text = "Status:(Offline)  No. of Packets = " + fileSize.ToString();
                // Update Time Info Tab
                fileTime  = pcktTime [pcktIndex - 1];
                System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dt = dt.AddSeconds((double)fileTime);
                ssTimeInfo.Text = "Time:  " + dt.ToShortDateString() + " " + dt.ToLongTimeString();
                // Update Site ID Tab
                fileSid = (pckt[5] << 8) + pckt[4];
                ssSystemID.Text = "Sid:  " + fileSid.ToString();
                // Update Sampling Rate Tab
                fileSR  = (pckt[9] << 8) + pckt[8]; ;
                fileSR = (fileSR - 8) / (3 * 3);  // Channels=3; Value=3Bytes
                ssSampling.Text = "Sampling:  " + fileSR.ToString();
                //
                ssComPort.Text = "Error:  " + pcktWithErr.ToString();
            }//End Try
            catch (IOException exc)
            {
                MessageBox.Show("Error Reading File");
                MessageBox.Show(exc.Message);
            }
            // Close the File
            dataIn.Close();
            // Return the filesize
            return (fileSize);
        }
        #endregion

        #region ZedGraph Mouse Up/Down Events
        private bool zg1_MouseUpEvent(ZedGraphControl control, MouseEventArgs e)
        {
            toolLblTimeInfo.Text = "";
            return false;
        }
        private bool zg2_MouseUpEvent(ZedGraphControl control, MouseEventArgs e)
        {
            toolLblTimeInfo.Text = "";
            return false;
        }
        private bool zg3_MouseUpEvent(ZedGraphControl control, MouseEventArgs e)
        {
            toolLblTimeInfo.Text = "";
            return false;
        }
        private bool zg1_MouseDownEvent(ZedGraphControl control, MouseEventArgs e)
        {
            zgMouseDownEvent(control,e);
            /*if (OffLineMode)
            {
                CurveItem dragCurve;
                int dragIndex;
                GraphPane myPane = control.GraphPane;
                PointF mousePt = new PointF(e.X, e.Y);
                bool chk = myPane.FindNearestPoint(mousePt, out dragCurve, out dragIndex);
                int nSeconds, nMilli;
                nSeconds = dragIndex / 50;
                nMilli = (dragIndex % 50) * 20;
                TimeConversion(pcktTime [nSeconds], nMilli);
                toolLblTimeInfo.Text = rtPcktTimeText;
            }*/
            return false;
        }

        private bool zg2_MouseDownEvent(ZedGraphControl control, MouseEventArgs e)
        {
            zgMouseDownEvent(control,e);
            /*
            if (OffLineMode)
            {
                CurveItem dragCurve;
                int dragIndex;
                GraphPane myPane = control.GraphPane;
                PointF mousePt = new PointF(e.X, e.Y);
                bool chk = myPane.FindNearestPoint(mousePt, out dragCurve, out dragIndex);
                int nSeconds, nMilli;
                nSeconds = dragIndex / 50;
                nMilli = (dragIndex % 50) * 20;
                TimeConversion(pcktTime [nSeconds], nMilli);
                toolLblTimeInfo.Text = rtPcktTimeText;
            }*/
            return false;
        }
        private bool zg3_MouseDownEvent(ZedGraphControl control, MouseEventArgs e)
        {
            zgMouseDownEvent(control,e);
            return false;
        }
        private void zgMouseDownEvent(ZedGraphControl control, MouseEventArgs e)
        {
            if (OffLineMode)
            {
                CurveItem dragCurve;
                int dragIndex;
                GraphPane myPane = control.GraphPane;
                PointF mousePt = new PointF(e.X, e.Y);
                bool chk = myPane.FindNearestPoint(mousePt, out dragCurve, out dragIndex);
                int nSeconds, nMilli;
                nSeconds = dragIndex / 50;
                nMilli = (dragIndex % 50) * 20;
                TimeConversion(pcktTime[nSeconds], nMilli);
                toolLblTimeInfo.Text = rtPcktTimeText;
            }
        }
        #endregion

        #region User Defined Time Conversion Routine (Seconds to Human Readable Formate)
        /// <summary>
        /// Convert From UTC time to Human Readable Time and Date
        /// </summary>
        private void TimeConversion(int utcTime, int tmilli)
        {
            int[] DaysToMonth = new int[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };

            int H;  // Total hours since midnight - [0,23]
            int D;  // Total days of the month - [1,31]
            int M;  // Total minutes after the hour - [0,59]
            int S;  // Total seconds after the minute - [0,59]
            int Mn; // Total months since January - [0,11]
            int Y;  // Total  years since 1900
            int WM; // Whole minutes
            int WH; // Whole Hours
            int WD; // Whole Days
            int D1; //Whole days since 1968
            int D2; // Lear year periods
            int D3; // days since current leap year
            int WY; // Whole years
            int D4; // days since first of year
            int DoM;// days to month
            int DoW;// day of the week  days since Sunday - [0,6]
            // Step 1
            WM = utcTime / 60;
            S = utcTime % 60;
            // Step 2
            WH = WM / 60;
            M = WM % 60;
            // Step 3
            WD = WH / 24;
            H = WH % 24;
            // Step 4
            D1 = WD + 731;
            D2 = D1 / 1461;
            // Step 5
            D3 = D1 % 1461;

            // Step 6 if days are after a current leap year then add a leap year period
            if ((D3 >= 60))
                D2 = D2 + 1;
            // Step 7
            WY = (D1 - D2) / 365;
            D4 = D1 - (WY * 365) - D2;
            // Step 8
            if ((D3 <= 365) && (D3 >= 60))
                D4 = D4 + 1;
            // Step 9
            Y = WY + 68;

            // Step 10 setup for a search for what month it is based on how many days have past
            //   within the current year
            Mn = 13;
            DoM = 366;
            while (D4 < DoM)
            {
                Mn = Mn - 1;
                DoM = DaysToMonth[Mn - 1];
                if ((Mn > 2) && ((Y % 4) == 0))
                    DoM = DoM + 1;
            }
            // Step 11
            D = D4 - DoM + 1;
            DoW = (WD + 4) % 7;
            // Step 12
            Y = Y % 100;
            // Print the date and time
            rtPcktTimeText = H.ToString() + ":" + M.ToString() + ":" + S.ToString() + ":" + tmilli.ToString() + " " +
                D.ToString() + "/" + Mn.ToString() + "/" + Y.ToString();
        }
        #endregion

        #region Serial Port Data Recieved Method and its Delegate Functions
        
        //Delegate
        delegate void PlottingDelegate(byte[] p);
        delegate void SerialDelegate(byte[] r);
        // Callback function to Plotting Delegate
        private void PlottingDlgtCallback(byte[] p)
        {
            rtCnt = 0;
            ProcessPacket(p);
        }
        private void SerialDlgtCallback(byte[] r)
        {
            all.RemoveRange(0, rtCntOld);
            rtCntOld = 0; rtPcktFinding = true;
            all.AddRange(r);
            if (all.Count >= 1450) timer1.Start();
        }
        private void DataRec(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            rtTotBytes = comport.BytesToRead;
            byte[] result = new byte[rtTotBytes];
            comport.Read(result, 0, rtTotBytes);
            this.BeginInvoke(new SerialDelegate(SerialDlgtCallback), result);
        }
        private void PcktValidate()
        {
            while (rtPcktFinding)
            {
                switch (rtState)
                {
                    case 0:
                        if (all[rtCnt] == 0xbf) { rtState = 1; }
                        else rtState = 0;
                        break;
                    case 1:
                        if (all[rtCnt] == 0x13) { rtState = 2; }
                        else rtState = 0;
                        break;
                    case 2:
                        if (all[rtCnt] == 0x97) { rtState = 3; }
                        else rtState = 0;
                        break;
                    case 3:
                        if (all[rtCnt] == 0x74)
                        {
                            rtPcktFinding = false;
                            PlotBuff[0] = 0xbf; PlotBuff[1] = 0x13; PlotBuff[2] = 0x97;
                            all.CopyTo(rtCnt, PlotBuff, 3, 465);
                            rtCntOld = rtCnt+465;
                            rtCnt = 0; rtState = 0;
                            this.BeginInvoke(new PlottingDelegate(PlottingDlgtCallback), PlotBuff);
                        }
                        else rtState = 0;
                        break;
                }//switch
                rtCnt++;
            }//while
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (all.Count < 468)
                timer1.Stop();
            else
                PcktValidate();
        }
        #endregion

        #region Process the Recieved pakcet, displays it, store it
        /// <summary>
        /// After recieving whole buf1, it is processed for display and storage
        /// </summary>
        private void ProcessPacket(byte[] bfr)//Object sender, EventArgs e)
        {
            // Calculate the CRC
            for (int d = 4; d < 466; d+=2)
                rtTempSum = rtTempSum + (bfr[d + 1] << 8) + bfr[d];

            // Check CRC, and add to corresponding List
            rtPcktCRC  = (bfr[467] << 8) + bfr[466];
            rtTempSum = ((rtTempSum ^ 0xffff) + 1) & 0xffff;

            if (rtPcktCRC != rtTempSum) Array.Clear(bfr, 0, 468);
            rtTempSum = 0; rtPcktCRC = 0;
            // Check that Scale has been filled, Clear, and Start again
            if (rtXValue >= rtXScale.Max-1)
            {
                rtXValue = 0;
                rtList1.Clear();
                rtList2.Clear();
                rtList3.Clear();
            }
            for (int d = 0; d < 50; d++)
            {
                // Get Channel No 1 Data
                rtAmp = (bfr[18 + d * 3] << 24) + (bfr[17 + d * 3] << 16) + (bfr[16 + d * 3]<<8);
                // Add to List
                rtList1.Add(rtXValue, (double)(rtAmp >> 8));
                // Get Channel No 2 Data
                rtAmp = (bfr[168 + d * 3] << 24) + (bfr[167 + d * 3] << 16) + (bfr[166 + d * 3] << 8);
                // Add to List
                rtList2.Add(rtXValue, (double)(rtAmp >> 8));
                // Get Channel No 3 Data
                rtAmp = (bfr[318 + d * 3] << 24) + (bfr[317 + d * 3] << 16) + (bfr[316 + d * 3] << 8);
                // Add to List
                rtList3.Add(rtXValue, (double)(rtAmp >> 8));
                rtXValue = rtXValue + 0.02;
            }
            // Refresh the ZedGraph Plane
            zg1.AxisChange(); 
            zg1.Invalidate();
            zg2.AxisChange(); 
            zg2.Invalidate();
            zg3.AxisChange(); 
            zg3.Invalidate();
            
            // Extract the Time Information from the Packet
            // Convert it to human Readable Format
            rtPcktTime = (bfr[13] << 24) + (bfr[12] << 16) + (bfr[11] << 8) + bfr[10] + 18000;
            System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dt = dt.AddSeconds((double)rtPcktTime);
            // Change the File Name at Every Hour, and First Time also
            if (dt.Hour != rtPreHour)
            {
                // Generate new File Name
                // Generate new File Name
                rtFileName = System.IO.Path.Combine(rtPath,
                    dt.Year.ToString() + dt.Month.ToString("00") + dt.Day.ToString("00") +
                    dt.Hour.ToString("00") + dt.Minute.ToString("00") + dt.Second.ToString("00"));
            }
            rtPreHour = dt.Hour;
            // Update the Status Tab
            ssStatus.Text = "Status:  ";
            if (toolRealMonitor.Checked) ssStatus.Text += "Monitoring=ON ";
            if (toolSaveFile.Checked) ssStatus.Text += "Saving=ON";
            else ssStatus.Text += "Saving=OFF";
            // Update the Time Information Status Tab
            ssTimeInfo.Text = "Time:  " + dt.ToShortDateString() + " " + dt.ToLongTimeString();
            // Update the Site ID Status Tab
            rtSid = (bfr[5] << 8) + bfr[4];
            ssSystemID.Text = "Sid:  " + rtSid.ToString();
            // Update the Sampling Rate Status Tab
            rtSRate = (bfr[9] << 8) + bfr[8] - 8;
            rtSRate = rtSRate / (3 * 3);  // Channels=3; Value=3Bytes
            ssSampling.Text = "Sampling:  " + rtSRate.ToString();

            // Write to File, if Saving Mode is Enabled
            if(SavingMode )
                using (BinaryWriter bw = new BinaryWriter(new FileStream(rtFileName + ".EVT", FileMode.Append)))
                {
                    bw.Write(bfr, 0, 468);
                    bw.Close();
                }
            
        }
        #endregion

        #region Send Commands through Serial Port
        private void SendCommand(int cmbType)
        {
            byte[] cmd = new byte[20];
            int cmd_crc = 0;
            switch (cmbType)
            {
                case 1:// Site ID
                    cmd_crc = 0x1010 + rtSid + rtSidNew;
                    cmd_crc = ((cmd_crc & 0xffff) ^ 0xffff) + 1;
                    cmd[0] = 0xbf; cmd[1] = 0x13; cmd[2] = 0x97; cmd[3] = 0x74;
                    cmd[4] = (byte)(rtSid & 0xff); cmd[5] = (byte)((rtSid & 0xff00) >> 8);
                    cmd[6] = 0x08; cmd[7] = 0x10; cmd[8] = 0x08; cmd[9] = 0x00;
                    cmd[10] = 0x00; cmd[11] = 0x00; cmd[12] = 0x00; cmd[13] = 0x00;
                    cmd[14] = (byte)(rtSidNew & 0xff); cmd[15] = (byte)((rtSidNew & 0xff00) >> 8);
                    cmd[16] = (byte)(cmd_crc & 0xff); cmd[17] = (byte)((cmd_crc & 0xff00) >> 8);
                    comport.Write(cmd, 0, 18);
                    break;
                case 2:// Sampling Rate with Minimum Phase
                    cmd_crc = 0x1808 + rtSRateNew;
                    cmd_crc = ((cmd_crc & 0xffff) ^ 0xffff) + 1;
                    cmd[0] = 0xbf; cmd[1] = 0x13; cmd[2] = 0x97; cmd[3] = 0x74;
                    cmd[4] = 0x00; cmd[5] = 0x00;
                    cmd[6] = 0x04; cmd[7] = 0x10; cmd[8] = 0x04; cmd[9] = 0x00;
                    cmd[10] = (byte)rtSRateNew; cmd[11] = 0x08;
                    cmd[12] = (byte)(cmd_crc & 0xff); cmd[13] = (byte)((cmd_crc & 0xff00) >> 8);
                    comport.Write(cmd, 0, 14);
                    break;
                case 3:// Sampling Rate with Linear Phase
                    cmd_crc = 0x1408 + rtSRateNew;
                    cmd_crc = ((cmd_crc & 0xffff) ^ 0xffff) + 1;
                    cmd[0] = 0xbf; cmd[1] = 0x13; cmd[2] = 0x97; cmd[3] = 0x74;
                    cmd[4] = 0x00; cmd[5] = 0x00;
                    cmd[6] = 0x04; cmd[7] = 0x10; cmd[8] = 0x04; cmd[9] = 0x00;
                    cmd[10] = (byte)rtSRateNew; cmd[11] = 0x04;
                    cmd[12] = (byte)(cmd_crc & 0xff); cmd[13] = (byte)((cmd_crc & 0xff00) >> 8);
                    comport.Write(cmd, 0, 14);
                    break;
            }
        }
        #endregion

        #region Y-Axis Zoom in/ Zoom Out Routines
        private void toolUpChan1_Click(object sender, EventArgs e)
        {
            if (y1_axis < 8388608) y1_axis = y1_axis * 2;
            zg1.GraphPane.YAxis.Scale.Min = -y1_axis;
            zg1.GraphPane.YAxis.Scale.Max = y1_axis;
            zg1.GraphPane.YAxis.Scale.MajorStep = y1_axis / 2;
            zg1.GraphPane.YAxis.Scale.MinorStep = y1_axis / 2;
            zg1.AxisChange();
            zg1.Refresh();
        }

        private void toolDownChan1_Click(object sender, EventArgs e)
        {
            if (y1_axis >2) y1_axis = y1_axis / 2;
            zg1.GraphPane.YAxis.Scale.Min = -y1_axis;
            zg1.GraphPane.YAxis.Scale.Max = y1_axis;
            zg1.GraphPane.YAxis.Scale.MajorStep = y1_axis / 2;
            zg1.GraphPane.YAxis.Scale.MinorStep = y1_axis / 2;
            zg1.AxisChange();
            zg1.Refresh();
        }

        private void toolUpChan2_Click(object sender, EventArgs e)
        {
            if (y2_axis < 8388608) y2_axis = y2_axis * 2;
            zg2.GraphPane.YAxis.Scale.Min = -y2_axis;
            zg2.GraphPane.YAxis.Scale.Max = y2_axis;
            zg2.GraphPane.YAxis.Scale.MajorStep = y2_axis / 2;
            zg2.GraphPane.YAxis.Scale.MinorStep = y2_axis / 2;
            zg2.AxisChange();
            zg2.Refresh();
        }

        private void toolDownChan2_Click(object sender, EventArgs e)
        {
            if (y2_axis > 2) y2_axis = y2_axis / 2;
            zg2.GraphPane.YAxis.Scale.Min = -y2_axis;
            zg2.GraphPane.YAxis.Scale.Max = y2_axis;
            zg2.GraphPane.YAxis.Scale.MajorStep = y2_axis / 2;
            zg2.GraphPane.YAxis.Scale.MinorStep = y2_axis / 2;
            zg2.AxisChange();
            zg2.Refresh();
        }

        private void toolUpChan3_Click(object sender, EventArgs e)
        {
            if (y3_axis < 8388608) y3_axis = y3_axis * 2;
            zg3.GraphPane.YAxis.Scale.Min = -y3_axis;
            zg3.GraphPane.YAxis.Scale.Max = y3_axis;
            zg3.GraphPane.YAxis.Scale.MajorStep = y3_axis / 2;
            zg3.GraphPane.YAxis.Scale.MinorStep = y3_axis / 2;
            zg3.AxisChange();
            zg3.Refresh();
        }

        private void toolDownChan3_Click(object sender, EventArgs e)
        {
            if (y3_axis > 1) y3_axis = y3_axis / 2;
            zg3.GraphPane.YAxis.Scale.Min = -y3_axis;
            zg3.GraphPane.YAxis.Scale.Max = y3_axis;
            zg3.GraphPane.YAxis.Scale.MajorStep = y3_axis / 2;
            zg3.GraphPane.YAxis.Scale.MinorStep = y3_axis / 2;
            zg3.AxisChange();
            zg3.Refresh();
        }
        #endregion

        #region X-Axis Zoom in/ Zoom Out Routines
        private void toolLeft_Click(object sender, EventArgs e)
        {
            if(x_axis>0) x_axis--;
            zg1.GraphPane.XAxis.Scale.Min = 0;
            zg1.GraphPane.XAxis.Scale.Max = xList[x_axis];
            zg2.GraphPane.XAxis.Scale.Min = 0;
            zg2.GraphPane.XAxis.Scale.Max = xList[x_axis];
            zg3.GraphPane.XAxis.Scale.Min = 0;
            zg3.GraphPane.XAxis.Scale.Max = xList[x_axis];
            switch (x_axis )
            {
                case 0:
                case 1:
                    zg1.GraphPane.XAxis.Scale.MajorStep = 1;
                    zg1.GraphPane.XAxis.Scale.MinorStep = 0.1;
                    zg2.GraphPane.XAxis.Scale.MajorStep = 1;
                    zg2.GraphPane.XAxis.Scale.MinorStep = 0.1;
                    zg3.GraphPane.XAxis.Scale.MajorStep = 1;
                    zg3.GraphPane.XAxis.Scale.MinorStep = 0.1;
                    break;
                case 2:
                    zg1.GraphPane.XAxis.Scale.MajorStep = 3;
                    zg1.GraphPane.XAxis.Scale.MinorStep = 0.3;
                    zg2.GraphPane.XAxis.Scale.MajorStep = 3;
                    zg2.GraphPane.XAxis.Scale.MinorStep = 0.3;
                    zg3.GraphPane.XAxis.Scale.MajorStep = 3;
                    zg3.GraphPane.XAxis.Scale.MinorStep = 0.3;
                    break;
                default:
                    zg1.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
                    zg1.GraphPane.XAxis.Scale.MinorStep  = xList[x_axis] / 100;
                    zg2.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
                    zg2.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 100;
                    zg3.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
                    zg3.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 100;
                    break;
            }
            zg1.AxisChange();
            zg2.AxisChange();
            zg3.AxisChange();
            zg1.Refresh();
            zg2.Refresh();
            zg3.Refresh();
        }

        private void toolRight_Click(object sender, EventArgs e)
        {
            if (x_axis < 7) x_axis++;
            switch (x_axis)
            {
                case 0:
                case 1:
                    zg1.GraphPane.XAxis.Scale.Min = 0;
                    zg1.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg1.GraphPane.XAxis.Scale.MajorStep = 1;
                    zg1.GraphPane.XAxis.Scale.MinorStep = 0.1;
                    zg2.GraphPane.XAxis.Scale.Min = 0;
                    zg2.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg2.GraphPane.XAxis.Scale.MajorStep = 1;
                    zg2.GraphPane.XAxis.Scale.MinorStep = 0.1;
                    zg3.GraphPane.XAxis.Scale.Min = 0;
                    zg3.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg3.GraphPane.XAxis.Scale.MajorStep = 1;
                    zg3.GraphPane.XAxis.Scale.MinorStep = 0.1;
                    break;
                case 2:
                    zg1.GraphPane.XAxis.Scale.Min = 0;
                    zg1.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg1.GraphPane.XAxis.Scale.MajorStep = 3;
                    zg1.GraphPane.XAxis.Scale.MinorStep = 0.3;
                    zg2.GraphPane.XAxis.Scale.Min = 0;
                    zg2.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg2.GraphPane.XAxis.Scale.MajorStep = 3;
                    zg2.GraphPane.XAxis.Scale.MinorStep = 0.3;
                    zg3.GraphPane.XAxis.Scale.Min = 0;
                    zg3.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg3.GraphPane.XAxis.Scale.MajorStep = 3;
                    zg3.GraphPane.XAxis.Scale.MinorStep = 0.3;
                    break;
                default:
                    zg1.GraphPane.XAxis.Scale.Min = 0;
                    zg1.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg1.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
                    zg1.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 100;
                    zg2.GraphPane.XAxis.Scale.Min = 0;
                    zg2.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg2.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
                    zg2.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 100;
                    zg3.GraphPane.XAxis.Scale.Min = 0;
                    zg3.GraphPane.XAxis.Scale.Max = xList[x_axis];
                    zg3.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
                    zg3.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 100;
                    break;
            }
            zg1.AxisChange();
            zg2.AxisChange();
            zg3.AxisChange();
            zg1.Refresh();
            zg2.Refresh();
            zg3.Refresh();
        }
        #endregion

        #region Tool Bar Items
        private void toolRealMonitor_Click(object sender, EventArgs e)
        {
            HandleRealMonitor();
        }
        private void HandleRealMonitor()
        {
            if (toolRealMonitor.Checked == true)
            {
                // Make Capturing Buttons Checked
                toolRealMonitor.CheckState = CheckState.Unchecked;
                mnuCapture.CheckState = CheckState.Unchecked;
                // Make Saving Buttons Disabled and Unchecked
                toolSaveFile.Enabled = false ;
                mnuSaving.Enabled = false ;
                mnuSaving.CheckState = CheckState.Unchecked;
                toolSaveFile.CheckState = CheckState.Unchecked;

                if (comport.IsOpen) comport.Close();
                ssStatus.Text = "Status:  Monitoring=OFF";
                ssComPort.Text = "Port:  OFF";
                timer1.Stop();
            }
            else if (toolRealMonitor.Checked == false)
            {
                // Make Capturing Buttons Checked
                toolRealMonitor.CheckState = CheckState.Checked;
                mnuCapture.CheckState = CheckState.Checked;
                // Make Saving Buttons Enabled and Unchecked
                toolSaveFile.Enabled = true;
                mnuSaving.Enabled = true;
                mnuSaving.CheckState = CheckState.Unchecked;
                toolSaveFile.CheckState = CheckState.Unchecked;
                // Set Variables
                OffLineMode = false;
                bool error = false;
                rtState = 0;
                rtCnt = 0; rtCntOld = 0;
                rtPreHour = 0;
                rtXValue = 0;
                all.Clear();
                //tmState = 0;
                rtPcktFinding = true;
                // If the port is open, close it.
                if (comport.IsOpen) comport.Close();
                else
                {
                    try
                    {
                        // Set Default Axis Min/Max Values
                        y1_axis = 32; y2_axis = 32; y3_axis = 32; x_axis = 2;
                        // Set ZedGraph Parameters
                        zgParamRealTime();
                        // Open the port
                        comport.Open();
                        //
                        ssComPort.Text = "Port:  " + comport.PortName.ToString ();
                        //
                        //timer1.Start();
                    }
                    catch (UnauthorizedAccessException) { error = true; }
                    catch (IOException) { error = true; }
                    catch (ArgumentException) { error = true; }

                    if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        private void toolSaveFile_Click(object sender, EventArgs e)
        {
            HandleFileSaving();

        }
        private void HandleFileSaving()
        {
            if (toolSaveFile.Checked == true)
            {
                SavingMode = false;
                // Make Saving Buttons Un-Checked
                mnuSaving.CheckState = CheckState.Unchecked;
                toolSaveFile.CheckState = CheckState.Unchecked;
            }
            else if (toolSaveFile.Checked == false)
            {
                SavingMode = true;
                // Make Saving Buttons Checked
                mnuSaving.CheckState = CheckState.Checked;
                toolSaveFile.CheckState = CheckState.Checked;
            }
        }
        private void toolOpenFile_Click(object sender, EventArgs e)
        {
            OffLineMode = true;
            if (comport.IsOpen) comport.Close();
            
            toolSaveFile.Enabled = false;
            mnuSaving.Enabled = false;
            toolRealMonitor.CheckState = CheckState.Unchecked;

            rtPreHour = 0;
            rtXValue = 0;
            rtCnt = 0; rtState = 0; rtCntOld = 0;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string FN = "";
            openFileDialog1.InitialDirectory = @"C:\Events";// FN;
            openFileDialog1.Filter = "Event files (*.EVT)|*.EVT|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FN = openFileDialog1.FileName;
                    if (Path.GetExtension(FN) == ".EVT")
                    {
                        zg1.GraphPane.CurveList.Clear();
                        zg2.GraphPane.CurveList.Clear();
                        zg3.GraphPane.CurveList.Clear();
                        this.Text = "Seismic Data Acquisition:  " + Path.GetFileName(FN);
                        RefreshGraph();
                        int xMax = ReadEVT(FN);
                        zgParamOffLine(xMax);
                        OfflinePlot();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (comport.IsOpen) comport.Close();
            Close();
        }

        #endregion

        #region Set ZedGraph Axis, Title, Steps etc..
        private void zgParamRealTime()
        {
            pcktList1.Clear();
            pcktList2.Clear();
            pcktList3.Clear();
            zg1.GraphPane.Title.Text = "";
            zg1.GraphPane.XAxis.Title.Text = "";
            zg1.GraphPane.YAxis.Title.Text = "";// "Amplitude";
            zg1.GraphPane.XAxis.Scale.Min = 0;
            zg1.GraphPane.XAxis.Scale.Max = xList[x_axis];
            zg1.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
            zg1.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 10;
            zg1.GraphPane.YAxis.Scale.Min = -y1_axis;
            zg1.GraphPane.YAxis.Scale.MajorStep = y1_axis / 2;
            zg1.GraphPane.YAxis.Scale.MinorStep = y1_axis / 2;
            zg1.GraphPane.YAxis.Scale.Max = y1_axis;
            zg1.GraphPane.XAxis.MajorGrid.IsVisible = true;
            // Fill the axis background with a color gradient
            zg1.GraphPane.Chart.Fill = new Fill(Color.FromArgb(128, 255, 255));
            // Fill the pane background with a color gradient
            zg1.GraphPane.Fill = new Fill(Color.FromArgb(128, 255, 255));
            LineItem curve1 = zg1.GraphPane.AddCurve("", pcktList1, Color.Black, SymbolType.None);
            // Axis Font
            zg1.GraphPane.YAxis.Scale.MagAuto = false;
            zg1.GraphPane.YAxis.Scale.FontSpec.Size = 20;
            //
            zg2.GraphPane.Title.Text = "";
            zg2.GraphPane.XAxis.Title.Text = "";
            zg2.GraphPane.YAxis.Title.Text = "";//"Amplitude";
            zg2.GraphPane.XAxis.Scale.Min = 0;
            zg2.GraphPane.XAxis.Scale.Max = xList[x_axis];
            zg2.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
            zg2.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 10;
            zg2.GraphPane.YAxis.Scale.Min = -y2_axis;
            zg2.GraphPane.YAxis.Scale.Max = y2_axis;
            zg2.GraphPane.YAxis.Scale.MajorStep = y2_axis / 2;
            zg2.GraphPane.YAxis.Scale.MinorStep = y2_axis / 2;
            zg2.GraphPane.XAxis.MajorGrid.IsVisible = true;
            // Fill the axis background with a color gradient
            zg2.GraphPane.Chart.Fill = new Fill(Color.FromArgb(128, 255, 255));
            // Fill the pane background with a color gradient
            zg2.GraphPane.Fill = new Fill(Color.FromArgb(128, 255, 255));
            //
            LineItem curve2 = zg2.GraphPane.AddCurve("", pcktList2, Color.Black, SymbolType.None);
            // Axis Font
            zg2.GraphPane.YAxis.Scale.MagAuto = false;
            zg2.GraphPane.YAxis.Scale.FontSpec.Size = 20;

            zg3.GraphPane.Title.Text = "";
            zg3.GraphPane.XAxis.Title.Text = "";
            zg3.GraphPane.YAxis.Title.Text = "";//"Amplitude";
            zg3.GraphPane.XAxis.Scale.Min = 0;
            zg3.GraphPane.XAxis.Scale.Max = xList[x_axis];
            zg3.GraphPane.XAxis.Scale.MajorStep = xList[x_axis] / 10;
            zg3.GraphPane.XAxis.Scale.MinorStep = xList[x_axis] / 10;
            zg3.GraphPane.YAxis.Scale.Min = -y3_axis;
            zg3.GraphPane.YAxis.Scale.Max = y3_axis;
            zg3.GraphPane.YAxis.Scale.MajorStep = y3_axis / 2;
            zg3.GraphPane.YAxis.Scale.MinorStep = y3_axis / 2;
            zg3.GraphPane.XAxis.MajorGrid.IsVisible = true;
            // Fill the axis background with a color gradient
            zg3.GraphPane.Chart.Fill = new Fill(Color.FromArgb(128, 255, 255));
            // Fill the pane background with a color gradient
            zg3.GraphPane.Fill = new Fill(Color.FromArgb(128, 255, 255));
            //
            LineItem curve3 = zg3.GraphPane.AddCurve("", pcktList3, Color.Black, SymbolType.None);
            // Axis Font
            zg3.GraphPane.YAxis.Scale.MagAuto = false;
            zg3.GraphPane.YAxis.Scale.FontSpec.Size = 20;
            zg3.GraphPane.XAxis.Scale.FontSpec.Size = 20;
            //
            rtCurve1 = zg1.GraphPane.CurveList[0] as LineItem;
            rtCurve2 = zg2.GraphPane.CurveList[0] as LineItem;
            rtCurve3 = zg3.GraphPane.CurveList[0] as LineItem;
            rtList1 = rtCurve1.Points as IPointListEdit;
            rtList2 = rtCurve2.Points as IPointListEdit;
            rtList3 = rtCurve3.Points as IPointListEdit;
            rtXScale = zg1.GraphPane.XAxis.Scale;
        }
        private void zgParamOffLine(int xMaximum )
        {
            zg1.GraphPane.XAxis.Scale.Min = 0;
            zg1.GraphPane.XAxis.Scale.Max = xMaximum;
            zg1.GraphPane.XAxis.Scale.MajorStep = xMaximum / 10;
            zg1.GraphPane.XAxis.Scale.MinorStep = xMaximum / 100;
            zg1.GraphPane.YAxis.Scale.Min = -32;
            zg1.GraphPane.YAxis.Scale.Max = 32;
            zg1.GraphPane.YAxis.Scale.MajorStep = 32 / 2;
            zg1.GraphPane.YAxis.Scale.MinorStep = 32 / 2;
            // Fill the axis background with a color gradient
            zg1.GraphPane.Chart.Fill = new Fill(Color.FromArgb(255, 128, 128));
            // Fill the pane background with a color gradient
            zg1.GraphPane.Fill = new Fill(Color.FromArgb(255, 128, 128));


            zg2.GraphPane.XAxis.Scale.Min = 0;
            zg2.GraphPane.XAxis.Scale.Max = xMaximum;
            zg2.GraphPane.XAxis.Scale.MajorStep = xMaximum / 10;
            zg2.GraphPane.XAxis.Scale.MinorStep = xMaximum / 100;
            zg2.GraphPane.YAxis.Scale.Min = -32;
            zg2.GraphPane.YAxis.Scale.Max = 32;
            zg2.GraphPane.YAxis.Scale.MajorStep = 32 / 2;
            zg2.GraphPane.YAxis.Scale.MinorStep = 32 / 2;
            // Fill the axis background with a color gradient
            zg2.GraphPane.Chart.Fill = new Fill(Color.FromArgb(255, 128, 128));
            // Fill the pane background with a color gradient
            zg2.GraphPane.Fill = new Fill(Color.FromArgb(255, 128, 128));

            zg3.GraphPane.XAxis.Scale.Min = 0;
            zg3.GraphPane.XAxis.Scale.Max = xMaximum;
            zg3.GraphPane.XAxis.Scale.MajorStep = xMaximum / 10;
            zg3.GraphPane.XAxis.Scale.MinorStep = xMaximum / 100;
            zg3.GraphPane.YAxis.Scale.Min = -32;
            zg3.GraphPane.YAxis.Scale.Max = 32;
            zg3.GraphPane.YAxis.Scale.MajorStep = 32 / 2;
            zg3.GraphPane.YAxis.Scale.MinorStep = 32 / 2;
            // Fill the axis background with a color gradient
            zg3.GraphPane.Chart.Fill = new Fill(Color.FromArgb(255, 128, 128));
            // Fill the pane background with a color gradient
            zg3.GraphPane.Fill = new Fill(Color.FromArgb(255, 128, 128));
        }
        #endregion

        #region Menu Bar Items
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            toolOpenFile_Click(sender, e);
        }

        private void mnuCapture_Click(object sender, EventArgs e)
        {
            HandleRealMonitor();
        }

        private void mnuSaving_Click(object sender, EventArgs e)
        {
            HandleFileSaving();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuSiteId_Click(object sender, EventArgs e)
        {
            if (comport.IsOpen == false) { MessageBox.Show("Port is Closed!"); return; }
            frmSiteId fm2 = new frmSiteId();
            fm2.OldSiteID = rtSid;
            if (fm2.ShowDialog() == DialogResult.OK)
            {
                rtSid = fm2.OldSiteID;
                rtSidNew = fm2.NewSiteID;
                SendCommand(1);
            }
        }

        private void mnuSampleRate_Click(object sender, EventArgs e)
        {
            if (comport.IsOpen == false) { MessageBox.Show("Port is Closed!"); return; }
            frmSampling fm3 = new frmSampling();
            if (fm3.ShowDialog() == DialogResult.OK)
            {
                rtSRateNew = fm3.SampleRate;
                if (fm3.MinPhaseFIR == true)
                    SendCommand(2);
                else
                    SendCommand(3);
            }
        }

        private void mnuLocalCOM_Click(object sender, EventArgs e)
        {
            // Set Default Axis Min/Max Values
            y1_axis = 32; y2_axis = 32; y3_axis = 32; x_axis = 2;
            // Set ZedGraph Parameters
            zgParamRealTime ();
            // Load the Form
            if (fm1.ShowDialog() == DialogResult.OK)
            {
                // Enable the RealTime Monitor ICON
                toolRealMonitor.Enabled = true;
                // Set the port's settings
                comport.BaudRate = int.Parse(fm1.Controls[1].Text);
                comport.DataBits = 8;
                comport.StopBits = StopBits.One;
                comport.Parity = Parity.None;
                comport.PortName = fm1.Controls[0].Text;
                //comport.ReadBufferSize = 500;
                comport.ReceivedBytesThreshold = 468;
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog(this);

        }
        #endregion

        

    }
}