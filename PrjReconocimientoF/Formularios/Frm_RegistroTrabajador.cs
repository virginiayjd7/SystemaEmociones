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
using Reconocimiento_facial;

namespace PrjReconocimientoF.Formularios
{
    public partial class Frm_RegistroTrabajador : Form
    {
        #region Dlls para poder hacer el movimiento del Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //int w = 0;
        //int h = 0;

        Rectangle sizeGripRectangle;
        bool inSizeDrag = false;
        const int GRIP_SIZE = 15;
        #endregion

        public int heigth, width;

        public string[] Labels;
        DBCon dbc = new DBCon();
        int con = 0, ini = 0;

        //DECLARANDO TODAS LAS VARIABLES, vectores y  haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> labels1 = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name;

        public string LoadFaces { get; private set; }

        public Frm_RegistroTrabajador()
        {
            InitializeComponent();
           
            heigth = this.Height; width = this.Width;
            //GARGAMOS LA DETECCION DE LAS CARAS POR  haarcascades 
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                dbc.ObtenerBytesImagen();//carga de caras previus trainned y etiquetas para cada imagen                
                Labels = dbc.Name; //Labelsinfo.Split('%');//separo los nombres de los usuarios 
                NumLabels = dbc.TotalUser;// Convert.ToInt32(Labels[0]);//extraigo el total de usuarios registrados
                ContTrain = NumLabels;


                for (int tf = 0; tf < NumLabels; tf++)//recorro el numero de nombres registrados
                {
                    con = tf;
                    Bitmap bmp = new Bitmap(dbc.ConvertByteToImg(con));
                    //LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(bmp));//cargo la foto con ese nombre
                    labels.Add(Labels[tf]);//cargo el nombre que se encuentre en la posicion del tf

                }
            }
            catch (Exception e)
            {//Si la variable NumLabels es 0 me presenta el msj
                MessageBox.Show(e + " No hay ningún rostro en la Base de Datos, por favor añadir por lo menos una cara", "Cragar caras en tu Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        int opcion = 0;

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            imageBox2.Image = null;
            this.txt_codigo.Clear();
            this.txt_nombre.Clear();
        }

      
        private void BtnPrimero_Click(object sender, EventArgs e)
        {

            //pictureBox1.Image = dbc.ConvertByteToImg(0);
            label4.Text = dbc.Name[0];

        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (ini < NumLabels - 1)
            {
                ini++;
               // pictureBox1.Image = dbc.ConvertByteToImg(ini);
                label4.Text = dbc.Name[ini];
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (ini > 0)
            {
                ini--;
                //    pictureBox1.Image = dbc.ConvertByteToImg(ini);
                label4.Text = dbc.Name[ini];
            }
        }

        private void BtnUltimo_Click(object sender, EventArgs e)
        {

        }

        private void BtnTodo_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void BtnMaximizar_Click(object sender, EventArgs e)
        {
            StateWin();
        }

        private void StateWin()
        {

            //if (this.BtnMaximizar.Text == "1")
            //{
            //    this.BtnMaximizar.Text = "2";
            //    this.Location = new Point(0, 0);
            //    this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //}
            //else if (this.BtnMaximizar.Text == "2")
            //{
            //    this.BtnMaximizar.Text = "1";
            //    this.Size = new Size(width, heigth);
            //    this.StartPosition = FormStartPosition.CenterScreen;
            //}
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                //Action for each element detected
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                    break;
                }

                //resize face detected image for force to compare the same size with the 
                //test image with cubic interpolation type method
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(txt_nombre.Text);

                //Show face added in gray scale
                  imageBox2.Image = TrainedFace;
                  dbc.ConvertImgToBinary(txt_nombre.Text, txt_codigo.Text, imageBox2.Image.Bitmap);
                               
                MessageBox.Show("Agregado correctamente", "Capturado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pcbvolver_Click(object sender, EventArgs e)
        {
            Frm_Movil frm = new Frm_Movil();
            frm.Show();
            this.Hide();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (opcion == 1)
            {
                if (txt_nombre.Text == "")
                {
                    txt_nombre.Text = btn1.Text;
                }
                else
                {
                    string nom;
                    nom = txt_nombre.Text;
                    txt_nombre.Text = nom + btn1.Text;
                }
            }
            else if (opcion == 2)
            {
                if (txt_codigo.Text == "")
                {
                    txt_codigo.Text = btn1.Text;
                }
                else
                {
                    string nom;
                    nom = txt_codigo.Text;
                    txt_codigo.Text = nom + btn1.Text;
                }
            }
        }

        private void txt_codigo_TextChanged(object sender, EventArgs e)
        {
            opcion = 2;
            pteclado.Visible = true;
        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {
            opcion = 1;
            pteclado.Visible = true;
        }

       

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                btn1.Text = "Q";
                btn2.Text = "W";
                btn3.Text = "E";
                btn4.Text = "R";
                btn5.Text = "T";
                btn6.Text = "Y";
                btn7.Text = "U";
                btn8.Text = "I";
                btn9.Text = "O";
                btn10.Text = "P";
                btn11.Text = "A";
                btn12.Text = "S";
                btn13.Text = "D";
                btn14.Text = "F";
                btn15.Text = "G";
                btn16.Text = "H";
                btn17.Text = "J";
                btn18.Text = "K";
                btn19.Text = "L";
                btn20.Text = "Ñ";
                btn21.Text = "Z";
                btn22.Text = "X";
                btn23.Text = "C";
                btn24.Text = "V";
                btn25.Text = "B";
                btn26.Text = "N";
                btn27.Text = "M";
            }
            else
            {
                btn1.Text = "q";
                btn2.Text = "w";
                btn3.Text = "e";
                btn4.Text = "r";
                btn5.Text = "t";
                btn6.Text = "y";
                btn7.Text = "u";
                btn8.Text = "i";
                btn9.Text = "o";
                btn10.Text = "p";
                btn11.Text = "a";
                btn12.Text = "s";
                btn13.Text = "d";
                btn14.Text = "f";
                btn15.Text = "g";
                btn16.Text = "h";
                btn17.Text = "j";
                btn18.Text = "k";
                btn19.Text = "l";
                btn20.Text = "ñ";
                btn21.Text = "z";
                btn22.Text = "x";
                btn23.Text = "c";
                btn24.Text = "v";
                btn25.Text = "b";
                btn26.Text = "n";
                btn27.Text = "m";
            }
        }

        private void btnocultar_Click(object sender, EventArgs e)
        {
            pteclado.Visible = false;
        }

        private void btn26_Click(object sender, EventArgs e)
        {

        }

        private void BtnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Idle -= new EventHandler(FrameGrabber);//Detenemos el evento de captura
                grabber.Dispose();//Dejamos de usar la clase para capturar usar los dispositivos
                imageBoxFrameGrabber.ImageLocation = "img/1.png";//reiniciamos la imagen del control
                BtnDetectar.Enabled = true;
                BtnDesconectar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn16_Click(object sender, EventArgs e)
        {

        }

        private void BtnDetectar_Click(object sender, EventArgs e)
        {
            try
            {
                //Inicia la Captura            
                grabber = new Capture();
                grabber.QueryFrame();

                //Inicia el evento FrameGraber
                Application.Idle += new EventHandler(FrameGrabber);
                this.BtnDesconectar.Enabled = true;
                BtnDetectar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FrameGrabber(object sender, EventArgs e)
        {
            lblNumeroDetect.Text = "0";
            NamePersons.Add("");
            try
            {

                //Obtener la secuencia del dispositivo de captura
                try
                {
                    currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                }
                catch (Exception)
                {
                    imageBoxFrameGrabber.Image = null;
                }

                //Convertir a escala de grises
               gray = currentFrame.Convert<Gray, Byte>();

                //Detector de Rostros
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                //Accion para cada elemento detectado
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(640, 480, INTER.CV_INTER_CUBIC);
                    //Dibujar el cuadro para el rostro
                    currentFrame.Draw(f.rect, new Bgr(Color.LightGreen), 1);

                    NamePersons[t - 1] = name;
                    NamePersons.Add("");

                    //Establecer el nùmero de rostros detectados
                    lblNumeroDetect.Text = facesDetected[0].Length.ToString();
                    //lblNadie.Text = name;

                }
                t = 0;

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

        private void FrmRegistro_Load(object sender, EventArgs e)
        {

            pteclado.Visible = false;
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
            imageBoxFrameGrabber.ImageLocation = "img/1.png";
        }

        private void SetGripRectangle()
        {
            sizeGripRectangle = new Rectangle(
                       this.Width - GRIP_SIZE,
                       this.Height - GRIP_SIZE, GRIP_SIZE, GRIP_SIZE);
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
    }
}
