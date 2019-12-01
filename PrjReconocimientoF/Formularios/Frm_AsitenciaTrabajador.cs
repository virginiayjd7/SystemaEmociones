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
    public partial class Frm_AsitenciaTrabajador : Form
    {
        #region Dlls para poder hacer el movimiento del Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        Rectangle sizeGripRectangle;
        bool inSizeDrag = false;
        const int GRIP_SIZE = 15;

        int w = 0;
        int h = 0;
        #endregion

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
        int cont = 0;

        public Frm_AsitenciaTrabajador()
        {
            InitializeComponent();
            FechaHora.Start();
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

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            //para poder arrastrar el formulario sin bordes
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
            w = this.Width;
            h = this.Height;
        }

        private void Reconocer()
        {
            try
            {
                //Iniciar el dispositivo de captura
                grabber = new Capture(1);
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
            lblNumeroDetect.Text = "0";
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
                    }

                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                    //Establecer el nùmero de rostros detectados
                    lblNumeroDetect.Text = facesDetected[0].Length.ToString();
                    lblNadie.Text = name;

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FechaHora_Tick(object sender, EventArgs e)
        {
            lblfecha.Text = DateTime.Now.ToString("dd/mm/yy");
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            //Frm_Movil frm = new Frm_Movil();
            //frm.Show();
            //this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            ClsEntAsistencia objEasis = new ClsEntAsistencia();
            ClsNegReconocimientoFacial objNasis = new ClsNegReconocimientoFacial();                   

            if (lblNumeroDetect.Text == "0" || lblNadie.Text == "")
            {
                MessageBox.Show("No esta registrado", "Debe de estar registrado");
            }
            else
            {
                if (cont == 0)
                {
                    //if metodo de busqueda si la entrada es 1                                              
                    objEasis.nombres = lblNadie.Text;
                    objEasis.fecha = lblfecha.Text;
                    objEasis.estado = cont.ToString();
                    objNasis.MtdBuscar(objEasis);
                    dgvasistencia.DataSource = objNasis.Source;
                    //if ()
                    objEasis.codigo = "12345678";
                    objEasis.nombres = lblNadie.Text;
                    objEasis.fecha = lblfecha.Text;
                    objEasis.hentrada = lblhora.Text;
                    cont = 1;
                    objEasis.estado = cont.ToString();
                    objNasis.MtdGuardar(objEasis);
                }                          
                objNasis.Listar();
                dgvasistencia.DataSource = objNasis.Source;
                ClsConexion.Desconectar();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Frm_RegistroTrabajador frm = new Frm_RegistroTrabajador();
            //frm.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClsEntAsistencia objEasis = new ClsEntAsistencia();
            ClsNegReconocimientoFacial objNasis = new ClsNegReconocimientoFacial();

            if (lblNumeroDetect.Text == "0" || lblNadie.Text == "")
            {
                MessageBox.Show("No esta registrado", "Debe de estar registrado");
            }
            else
            {
                if (cont == 1)
                {
                    //if metodo de busqueda si la entrada es 1                                              
                    objEasis.nombres = lblNadie.Text;
                    objEasis.fecha = lblfecha.Text;
                    objEasis.estado = cont.ToString();
                    objNasis.MtdBuscar(objEasis);
                    dgvasistencia.DataSource = objNasis.Source;
                    objEasis.hsalida = lblhora.Text;
                    cont = 0;
                    objEasis.estado = cont.ToString();
                    objNasis.MtdModificar(objEasis);
                }
                objNasis.Listar();
                dgvasistencia.DataSource = objNasis.Source;
                ClsConexion.Desconectar();
            }
        }

        private void imageBoxFrameGrabber_Click(object sender, EventArgs e)
        {

        }

        //private void StateWin()
        //{

        //    if (this.btn_maximize.Text == "1")
        //    {
        //        this.btn_maximize.Text = "2";
        //        this.Location = new Point(0, 0);
        //        this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        //    }
        //    else if (this.btn_maximize.Text == "2")
        //    {
        //        this.btn_maximize.Text = "1";
        //        this.Size = new Size(width, heigth);
        //        this.StartPosition = FormStartPosition.CenterScreen;
        //    }
        //}

        private void FrmReconocimientoFacial_Load(object sender, EventArgs e)
        {
            
            #region[Metodo deredimension de formulario sin borde]

            SetGripRectangle();
            this.Paint += (o, ea) => { ControlPaint.DrawSizeGrip(ea.Graphics, this.BackColor, sizeGripRectangle); };

            this.MouseUp += delegate { inSizeDrag = false; };
            this.MouseDown += (o, ea) =>
            {
                if (IsInSizeGrip(ea.Location))
                    inSizeDrag = true;
            };
            this.MouseMove += (o, ea) =>
            {
                if (inSizeDrag)
                {
                    this.Width = ea.Location.X + GRIP_SIZE / 2;
                    this.Height = ea.Location.Y + GRIP_SIZE / 2;
                    SetGripRectangle();
                    this.Invalidate();
                }
            };
            #endregion
            Reconocer();
            media.SoundLocation = "sounds/2.wav";
            media.Play();
        }

        private void SetGripRectangle()
        {
            sizeGripRectangle = new Rectangle(
                       this.Width - GRIP_SIZE,
                       this.Height - GRIP_SIZE, GRIP_SIZE, GRIP_SIZE);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //Frm_Principal frm = new Frm_Principal();
            //frm.Show();
            //this.Hide();
        }

        private bool IsInSizeGrip(Point tmp)
        {
            if (tmp.X >= sizeGripRectangle.X
              && tmp.X <= this.Width
              && tmp.Y >= sizeGripRectangle.Y
              && tmp.Y <= this.Height
                )
                return true;
            else
                return false;
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnDesconectar_Click(object sender, EventArgs e)
        {
            switch (BtnDesconectar.Text)
            {
                case "Conectar":
                    Reconocer();
                    BtnDesconectar.Text = "Desconectar";
                    BtnDesconectar.ForeColor = Color.Red;
                    break;
                case "Desconectar":
                    Desconectar();
                    BtnDesconectar.ForeColor = Color.Green;
                    break;
            }
            lblNadie.Text = "";
        }

        private void Desconectar()
        {
            Application.Idle -= new EventHandler(FrameGrabber);
            grabber.Dispose();
            imageBoxFrameGrabber.ImageLocation = "img/1.png";
            lblNadie.Text = string.Empty;
            lblNumeroDetect.Text = string.Empty;
            BtnDesconectar.Text = "Conectar";
        }
    }
}
