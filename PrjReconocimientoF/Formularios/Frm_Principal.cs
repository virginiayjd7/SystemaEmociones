using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Net.Http.Headers;
using Reconocimiento_facial;
using PrjReconocimientoF.Entidad;
using PrjReconocimientoF.Negocios;



namespace PrjReconocimientoF.Formularios
{
    public partial class Frm_Principal : Form
    {
        

        private void FrameGrabber_Click(object sender, EventArgs e)
        {

        }

        private void FrmTabletReconocimientoFacial_Load(object sender, EventArgs e)
        {
            
        }

       

        private void FrameGrabber(object sender, EventArgs e)
        {
           
        }

     
       

        private void BtnDesconectar_Click(object sender, EventArgs e)
        {
            

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
       
        }

        public Frm_Principal()
        {
            InitializeComponent();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frm_AsitenciaTrabajador frm = new Frm_AsitenciaTrabajador();
            frm.Show();
            this.Hide();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Frm_RegistroTrabajador frm = new Frm_RegistroTrabajador();
            frm.Show();
            this.Hide();
        }

        private void imageBoxFrameGrabber_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Frm_EstadoAnimo frm = new Frm_EstadoAnimo();
            frm.Show();
            this.Hide();
        }
    }
     
}
