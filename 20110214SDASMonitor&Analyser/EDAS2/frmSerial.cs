using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SDAS
{
    public partial class frmSerial : Form
    {
        public frmSerial()
        {
            InitializeComponent();
            // Set the Deafault Settings for the Combo Box
            cmbPortName.SelectedIndex = 0;
            cmbBaudRate.SelectedIndex = 4;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            this.btnOpen.DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel ;
            this.AcceptButton = this.btnOpen;
            this.CancelButton = this.btnCancel;
        }
        public int ComBaudrate
        {
            get { return int.Parse(cmbBaudRate.Text ); }
        }
        public string ComName
        {
            get { return (cmbPortName .Text) ; }
        }
    }
}
