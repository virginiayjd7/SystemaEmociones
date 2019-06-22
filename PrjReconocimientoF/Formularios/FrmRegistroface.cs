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
using PrjReconocimientoF.Entidad;
using PrjReconocimientoF.Negocios;
using Reconocimiento_facial;

namespace PrjReconocimientoF.Formularios
{
    public partial class FrmRegistroface : Form
    {

        public FrmRegistroface()
        {
            InitializeComponent();
            heigth = this.Height; width = this.Width;
            //GARGAMOS LA DETECCION DE LAS CARAS POR  haarcascades 
            Face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                dbc.ObtenerBytesImagen();
                //carga de caras y etiquetas para cada imagen               
                Labels = dbc.Name;
                NumLabels = dbc.TotalUser;
                ContTrain = NumLabels;


                for (int tf = 0; tf < NumLabels; tf++)
                {
                    con = tf;
                    Bitmap bmp = new Bitmap(dbc.ConvertByteToImg(con));
                    trainingImages.Add(new Image<Gray, byte>(bmp));//cargo la foto con ese nombre
                    labels.Add(Labels[tf]);//cargo el nombre que se encuentre en la posicion del tf
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e + "No hay ningun rosto registrado).", "Cargar rostros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public int heigth, width;
        public string[] Labels;
        DBCon dbc = new DBCon();
        int con = 0;

        //DECLARANDO TODAS LAS VARIABLES, vectores y  haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade Face;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.4d, 0.4d);
        Image<Gray, byte> result = null, gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> labels1 = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;

        private void BtnDetectar_Click(object sender, EventArgs e)
        {
            try
            {
                grabber = new Capture();
                grabber.QueryFrame();

             
            }
            catch (Exception)
            {

                throw;
            }
        }

        string name;

        private void btn6_Click(object sender, EventArgs e)
        {

        }

        private void imageBoxFrameGrabber_Click(object sender, EventArgs e)
        {

        }
        void FrameGrabber(object sender, EventArgs e)
        {
            lblNumeroDetect.Text = "0";
            NamePersons.Add("");
            try {
                try
                {//obtener la secuencia del dispositivo de captura
                    currentFrame = grabber.QueryFrame().Resize(400, 300, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    currentFrame._Flip(FLIP.HORIZONTAL);
                }

                catch (Exception)
                {
                    imageBoxFrameGrabber.Image = null;
                }
                gray = currentFrame.Convert<Gray, Byte>();
                //detector de rostros
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
                //accion para cada elemneto detectado 
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                    //dibujar el cuadro de rostro
                    currentFrame.Draw(f.rect, new Bgr(Color.FromArgb(0, 122, 204)), 1);
                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                    lblNumeroDetect.Text = facesDetected[0].Length.ToString();
                }
                t = 0;
                //mostrar los rostros procesados y recinocidos
                imageBoxFrameGrabber.Image = currentFrame;
                name = "";
                NamePersons.Clear();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private void FrmTabletRegistro_Load(object sender, EventArgs e)
        {
            imageBoxFrameGrabber.ImageLocation = "ing/1.png";
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
