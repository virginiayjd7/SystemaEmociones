using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjReconocimientoF.Formularios
{
    public partial class FrmMovil : Form
    {
        public FrmMovil()
        {
            InitializeComponent();
        }

        private void pcbregistro_Click(object sender, EventArgs e)
        {
            FrmRegistro frm = new FrmRegistro();
            frm.Show();
            this.Hide();
        }

        private void pcbreconocimiento_Click(object sender, EventArgs e)
        {
            FrmReconocimientoFacial frm = new FrmReconocimientoFacial();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FrmPrincipal frm = new FrmPrincipal();
            frm.Show();
            this.Hide();
        }
    }
}
