using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDAS
{
    public partial class frmSiteId : Form
    {
        public frmSiteId()
        {
            InitializeComponent();
        }

        private void frmSiteId_Load(object sender, EventArgs e)
        {
            
            this.btnOK .DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            
        }
        public int OldSiteID
        {
            get { return int.Parse(txtOldSiteId.Text); }
            set { txtOldSiteId.Text = value.ToString(); }
        }
        public int NewSiteID
        {
            get { if (txtNewSiteId .Text == "") return (0);
                  else return int.Parse(txtNewSiteId.Text); }
            set { txtNewSiteId.Text = value.ToString(); }
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
