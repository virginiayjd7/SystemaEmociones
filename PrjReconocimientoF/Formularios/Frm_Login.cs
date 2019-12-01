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
using System.Speech.Synthesis;
using System.Media;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Net.Http.Headers;
using Reconocimiento_facial;
using PrjReconocimientoF.Entidad;
using PrjReconocimientoF.Negocios;

namespace PrjReconocimientoF.Formularios
{
    public partial class Frm_Login : Form
    {
        public int heigth, width;
        public string[] Labels;
        DBCon dbc = new DBCon();
        int con = 0;
        SoundPlayer media = new SoundPlayer();
        SpeechSynthesizer vos = new SpeechSynthesizer();
        //DECLARANDO TODAS LAS VARIABLES, vectores y  haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;

        //HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.4d, 0.4d);
        Image<Gray, byte> result = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, /*Labelsinfo,*/ names = null;
       
        private void Frm_Login_Load(object sender, EventArgs e)
        {
            Reconocer();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //es es el boton ingresar 
        private void BtnDesconectar_Click(object sender, EventArgs e)
        {
            //compara el nombre que tiene la imagen 
            if (lblNadie.Text== "Administrador")//si es administrador entra
            {
                Frm_Principal frm = new Frm_Principal();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No puedes Ingresar a este sistema", "No eres el Administrador");
            }
        }

        //int cont = 0;
        public Frm_Login()
        {
            InitializeComponent();
            
            heigth = this.Height; width = this.Width;
            //GARGAMOS LA DETECCION DE LAS CARAS POR  haarcascades 
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                dbc.ObtenerBytesImagen();
                //carga de caras y etiquetas para cada imagen               
                string[] Labels = dbc.Name;
                NumLabels = dbc.TotalUser;
                ContTrain = NumLabels;
                string LoadFaces;

                for (int tf = 0; tf < NumLabels; tf++)
                {
                    con = tf;
                    Bitmap bmp = new Bitmap(dbc.ConvertByteToImg(con));
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(bmp));//cargo la foto con ese nombre
                    labels.Add(Labels[tf]);//cargo el nombre que se encuentre en la posicion del tf
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e + "No hay ningun rosto registrado).", "Cargar rostros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Reconocer()
        {
            try
            {
                //Iniciar el dispositivo de captura
                grabber = new Capture();
                grabber.QueryFrame();
                //Iniciar el evento FrameGraber
                Application.Idle += new EventHandler(FrameGrabber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrameGrabber(object sender, EventArgs e)
        {
        //    lblNumeroDetect.Text = "0";
            NamePersons.Add("");
            try
            {
                currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //Convertir a escala de grises
                gray = currentFrame.Convert<Gray, Byte>();

                //Detector de Rostros
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                //Accion para cada elemento detectado
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                    //Dibujar el cuadro para el rostro
                    currentFrame.Draw(f.rect, new Bgr(Color.LightGreen), 1);

                    if (trainingImages.ToArray().Length != 0)
                    {
                        //Clase para reconocimiento con el nùmero de imagenes
                        MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                        //Clase Eigen para reconocimiento de rostro
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), ref termCrit);
                        var fa = new Image<Gray, byte>[trainingImages.Count]; //currentFrame.Convert<Bitmap>();

                        name = recognizer.Recognize(result);
                        //Dibujar el nombre para cada rostro detectado y reconocido
                        currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.YellowGreen));
                        ClsNegReconocimientoFacial obj = new ClsNegReconocimientoFacial();
                        //int j = 0;
                        //foreach (DataRow row in obj.ListarAdministrador().Rows)
                        //{
                        //    if (lblNadie.Text.Equals(row[j]))
                        //    {
                        //        Frm_Principal frm = new Frm_Principal();
                        //        frm.Show();
                        //        this.Hide();
                        //    }
                        //}
                    }

                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                    //Establecer el nùmero de rostros detectados
                    //lblNumeroDetect.Text = facesDetected[0].Length.ToString();
                    lblNadie.Text = name;

                    //compara el nombre que tiene la imagen 
                    if (lblNadie.Text == "fddxb")//si es administrador entra
                    {
                        Frm_Principal frm = new Frm_Principal();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No puedes Ingresar a este sistema", "No eres el Administrador");
                    }

                }
                t = 0;

                //Nombres concatenados de todos los rostros reconocidos
                for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
                {
                    names = names + NamePersons[nnn] + ", ";
                }

                //Mostrar los rostros procesados y reconocidos
                imageBoxFrameGrabber.Image = currentFrame;
                name = "";
                //Borrar la lista de nombres            
                NamePersons.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
