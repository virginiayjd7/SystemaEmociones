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
    public partial class Frm_Movil : Form
    {
        public Frm_Movil()
        {
            InitializeComponent();
        }

        private void pcbregistro_Click(object sender, EventArgs e)
        {
            Frm_RegistroTrabajador frm = new Frm_RegistroTrabajador();
            frm.Show();
            this.Hide();
        }

        private void pcbreconocimiento_Click(object sender, EventArgs e)
        {
           Frm_Principal frm = new Frm_Principal();
            frm.Show();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }

        private void Frm_Movil_Load(object sender, EventArgs e)
        {

        }
    }
}
