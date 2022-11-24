using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

using System.IO;

namespace GuralpSim
{
    public partial class frmMain : Form
    {
        
        int tiks = new int();
        byte[] GCF = new byte[1030];// frame with max limit
        int GCFInd = new int();
        int[] D;
        int dSize = new int();      // difference size
        int dSizeP = new int();     // difference size Previous
        int M = new int();
        int Mx = new int();
        int E = new int();
        int x1 = new int();
        int x2 = new int();
        int RIC = new int();
        int RIC1 = new int();
        int RIC2 = new int();
        int FIC = new int();
        int FIC1 = new int();
        int FIC2 = new int();
        int gt = new int();
        int gt1 = new int();
        int gt2 = new int();
        float t = new float();     //Time
        bool dwChanged = new bool();
        int[] SPSn = new int[6];// Samples per seconds
        string SysID;      // System Id
        string[] StreamID = new string[6];   // Stream ID for 6 Channels

        Queue PacQ = new Queue();
        Queue SPacQ;
        
        ManualResetEvent myEvent = new ManualResetEvent(false);
        //Thread myThread;

        // Control Arrays
        TextBox[] txtAmp = new TextBox[6];// Amplitudes
        TextBox[] txtFreq = new TextBox[6];// Frequncy
        TextBox[] txtStreamID = new TextBox[6];// Stream ID
        CheckBox[] chkChannel = new CheckBox[6]; // Channel Selection
        //FileStream fs;
        //StreamWriter br;

        public frmMain()
        {
            InitializeComponent();
            SPacQ = Queue.Synchronized(PacQ);
            //myThread = new Thread(new ThreadStart(myCircularBuffer));
            SysID = txtSysID.Text;
            // Fill Control Arrays
            txtAmp[0] = txtAmp1; txtFreq[0] = txtFreq1; txtStreamID[0] = txtStreamID1; chkChannel[0] = chkSignal1;
            /*
            txtAmp[1] = txtAmp2; txtFreq[1] = txtFreq2; txtStreamID[1] = txtStreamID2; chkChannel[1] = chkSignal2;
            txtAmp[2] = txtAmp3; txtFreq[2] = txtFreq3; txtStreamID[2] = txtStreamID3; chkChannel[2] = chkSignal3;
            txtAmp[3] = txtAmp4; txtFreq[3] = txtFreq4; txtStreamID[3] = txtStreamID4; chkChannel[3] = chkSignal4;
            txtAmp[4] = txtAmp5; txtFreq[4] = txtFreq5; txtStreamID[4] = txtStreamID5; chkChannel[4] = chkSignal5;
            txtAmp[5] = txtAmp6; txtFreq[5] = txtFreq6; txtStreamID[5] = txtStreamID6; chkChannel[5] = chkSignal6;*/
        }

        /*private void myCircularBuffer()
        {
            L = 0;
            while (true)
            {
                L = SPacQ.Count;
                Sbuf.Initialize();
                if (SPacQ.Count > 0)
                {
                    for (int mcb = 0; mcb < L; mcb++)
                    {
                        Sbuf[mcb] = (byte)SPacQ.Dequeue();
                    }//for
                    com.Write(Sbuf, 0, L);
                    //txtTick3.Text = (tiks[0] / 10).ToString();
                }//if
                else
                    myEvent.Reset();
            }//while
            myEvent.WaitOne();
        }*/
        private void btnStart_Click(object sender, EventArgs e)
        {
            //dSizeP = 1;
            timer1.Enabled = true;
            timer1.Start();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            //MessageBox.Show(Math.Ceiling (2.5).ToString ());
            //MessageBox.Show(Math.Floor(2.5).ToString());
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            float A = new float();
            

            FindDynamic(0);
            /*
            switch (tiks)
            {
                case 0: A = 300.0F; break;
                case 1: A = 90000.0F; break;
                case 2: A = 90000.0F; break;
                case 3: A = 90000.0F; break;
                case 4: A = 90000.0F; break;
                case 5: A = 10000.0F; break;
                case 6: A = 10000.0F; break;
                case 7: A = 1000000.0F; break;
                case 8: A = 1000000.0F; break;
                case 9: A = 1000000.0F; break;
                case 10: A = 500.0F; break;
            }
            txtAmp[0].Text = A.ToString();*/
            tiks++;
            if (tiks == 11) tiks = 0;
            //txtTick1.Text =  tiks .ToString ();
        }
        #region Conversion for 36-base Number
        private long Convert36(string inp)
        {
            int Len = new int();
            long res = new long();
            long d = new long();

            Len = inp.Length - 1;
            foreach (char c in inp)
            {
                if (c > 47 && c < 58) d = c - 48;
                if (c > 64 && c < 91) d = c - 55;
                res += d * (long)Math.Pow(36, Len--);
            }
            return res;
        }
        #endregion

        # region Calculate the Data Width of Differnces and the Maximum End Limit of GCF Packet
        private void  dWidth(int Mxx, out int DW, out int NN)
        {
            if (Math.Ceiling(Math.Log(2 * Mxx, 2.0)) > 16)
            { DW = 3; NN = 600; }
            else if (Math.Ceiling(Math.Log(2 * Mxx, 2.0)) > 8)
            { DW = 2; NN = 1000; }
            else
            { DW = 1; NN = 1000; }
        }
        # endregion
        
        # region Calculate the GCF Time
        private int GCFTime()
        {
            double days = new double(); // days
            int days_w = new int();     // days Integer
            double secs = new double(); // secs
            int secs_w = new int();     // secs Integer

            TimeSpan span = DateTime.Now.Subtract(new DateTime(1989, 11, 17, 0, 0, 0, 0));

            days = span.TotalDays;
            days_w = (int)Math.Floor(days);
            secs = days - days_w;
            secs_w = (int)Math.Floor(secs * 86400);
            days_w = (days_w << 17) + (secs_w & 0x1FFFF);
            return days_w;
        }
        # endregion

        #region Calculate the Header (24 Byte) and Footer (6 Byte)
        private void GCFPacketHF(int rate)
        {
            int ChkSum = new int();
            int i = new int();
            GCF[0] = 71;
            //GCF Index
            GCF[1] = (byte)GCFInd++;
            //total samples in a frame
            GCF[2] = (byte)(((M + 24) >> 8) & 0xFF);
            GCF[3] = (byte)((M + 24) & 0xFF);
            // System ID
            GCF[4] = (byte)((Convert36(SysID) >> 24) & 0xFF);
            GCF[5] = (byte)((Convert36(SysID) >> 16) & 0xFF);
            GCF[6] = (byte)((Convert36(SysID) >> 8) & 0xFF);
            GCF[7] = (byte)(Convert36(SysID) & 0xFF);
            // Stream ID
            GCF[8] = (byte)((Convert36(txtStreamID[0].Text) >> 24) & 0xFF);
            GCF[9] = (byte)((Convert36(txtStreamID[0].Text) >> 16) & 0xFF);
            GCF[10] = (byte)((Convert36(txtStreamID[0].Text) >> 8) & 0xFF);
            GCF[11] = (byte)(Convert36(txtStreamID[0].Text) & 0xFF);
            // time Info
            GCF[12] = (byte)((gt >> 24) & 0xFF);
            GCF[13] = (byte)((gt >> 16) & 0xFF);
            GCF[14] = (byte)((gt >> 8) & 0xFF);
            GCF[15] = (byte)(gt & 0xFF);
            //fixed value
            GCF[16] = 19;
            // Samples per second
            GCF[17] = (byte)rate;
            // compression code
            GCF[18] = (byte)(4 / dSizeP);
            // data records
            GCF[19] = (dSizeP ==3)? (byte)((M) / 3): (byte)((M) / 4);
            // FIC
            GCF[20] = (byte)((FIC >> 24) & 0xFF);
            GCF[21] = (byte)((FIC >> 16) & 0xFF);
            GCF[22] = (byte)((FIC >> 8) & 0xFF);
            GCF[23] = (byte)((FIC) & 0xFF);
            // Samples Values
            //GCF[24] .... GCF[1023]
            // RIC
            GCF[M + 24] = (byte)((RIC >> 24) & 0xFF);
            GCF[M + 25] = (byte)((RIC >> 16) & 0xFF);
            GCF[M + 26] = (byte)((RIC >> 8) & 0xFF);
            GCF[M + 27] = (byte)(RIC & 0xFF);
            // Calculate Check Sum
            ChkSum = 0;
            for (i = 0; i < (M + 28); i++)
                ChkSum += GCF[i];
            // Check Sum
            GCF[M + 28] = (byte)((ChkSum >> 8) & 0xFF);
            GCF[M + 29] = (byte)(ChkSum & 0xFF);
        }
        #endregion

        #region Calculate the GCF Body i.e. 8, 16, or 32-bit Differnces
        private void GCFPacketBody(int rate)
        {
            int p = new int();
            int q = new int ();
            int i = new int();
            for (i = 0; i < (rate*dSize ); i++)
            {
                p = (int)(i/dSize);
                q = dSize - (i % dSize) - 1;
                q = q * 8;
                GCF[M+ i + 24] = (byte)(D[p]>>q);
            }
             M = M + rate * dSize;
        }
        #endregion


        private void FindDynamic(int chan)
        {
            
            int sRate = new int();      // Samples per seconds
            sRate = SPSn[chan];
            float A = new float();      //Amplitude 1
            float F = new float();      // Frequency 1
            D = new int[sRate];   // differences
            int i = new int();          // General integer

            A = float.Parse(txtAmp[chan].Text);
            F = float.Parse(txtFreq[chan].Text);

            RIC1 = RIC2;
            for (i = 0; i < sRate; i++)
            {
                x1 = (int)(A * Math.Sin(2 * Math.PI * F * t- 10) + 100 * Math.Cos(160 * Math.PI *  t));
                D[i] = x1 - x2;
                if (M + i == 0) { FIC1 = x1; gt1 = GCFTime(); D[0] = 0; }
                if (i == 0) { FIC2 = x1; gt2 = GCFTime(); }
                x2 = x1;
                if (Math.Abs(D[i]) > Mx) Mx = Math.Abs(D[i]);
                t = t + (1.0f / (float)sRate);
            }
            RIC2 = x1;

            dWidth(Mx, out dSize, out E);
            Mx = 0;
            dwChanged  = ((dSize != dSizeP)&&(dSizeP >0))? true :false ;
            if ((dwChanged) || ((M + dSize*sRate ) > E))
            {
                // Find FIC, GCT Time and RIC of Last
                FIC = FIC1;
                gt = gt1;
                RIC = RIC1;
                //Append Header and Footer
                GCFPacketHF(sRate);
                //Send the GCF Packet
                com.Write(GCF, 0, M  + 30);
                //Reset the variables
                M = 0;
                D[0] = 0;
                FIC1 = FIC2;
                gt1 = gt2;
                
            }
            //Append the Main GCF Body
            GCFPacketBody(sRate);
            dSizeP = dSize;
            //textBox2.Text += "\r\nM=" + M.ToString() + " dSize=" + dSize.ToString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SPSn[0] = 100;
            SPSn[1] = 100;
            SPSn[2] = 100;
            SPSn[3] = 100;
            SPSn[4] = 100;
            SPSn[5] = 100;

             // Open the Com Port
            bool error = false;
            byte[] buf = new byte[5];
            com.PortName = "COM2";
            if (com.IsOpen) com.Close();
            try
            {
                com.Open();
            }
            catch (UnauthorizedAccessException) { error = true; }
            catch (IOException) { error = true; }
            catch (ArgumentException) { error = true; }

            if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //myThread.Start();

            // Create a new file using the name of the Save dialog box
            
            //fs = new FileStream("E:\\Samples.txt", FileMode.Create, FileAccess.Write, FileShare.Write);
            //br = new StreamWriter(fs);
            //br.WriteLine("*******"); 
        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (com.IsOpen) com.Close();
            //if (myThread.IsAlive) //myThread.Abort();
            //br.Close();
            //fs.Close();
        }
      
    }
}
