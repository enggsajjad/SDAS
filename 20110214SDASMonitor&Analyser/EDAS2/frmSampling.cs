using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDAS
{
    public partial class frmSampling : Form
    {
        public frmSampling()
        {
            InitializeComponent();
        }

        private void frmSampling_Load(object sender, EventArgs e)
        {
            this.btnOK .DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.rdoMinPhase.Checked = true;
            this.cmbSampleRate.SelectedIndex = 3;
        }
        public int SampleRate
        {
            get { return ((cmbSampleRate.SelectedIndex)+1); }
        }
        public bool MinPhaseFIR
        {
            get { return rdoMinPhase.Checked; }
        }
        public bool LinPhaseFIR
        {
            get { return rdoLinPhase.Checked; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
