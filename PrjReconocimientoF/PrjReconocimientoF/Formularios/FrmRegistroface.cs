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
        private void CALLBUTTON(Button BTN)
        {
            if (nombre==true)
            {
                if (btnShitt.FlatStyle == FlatStyle.Flat)
                {
                    txt_nombre.Text = txt_nombre.Text + BTN.Text;
                    btnShitt.PerformClick();
                }
                txt_nombre.Text = txt_nombre.Text + BTN.Text;
                txt_nombre.Focus();
            }
            else if (codigo==true)
            {
                if (btnnum.FlatStyle == FlatStyle.Flat)
                {
                    txt_codigo.Text = txt_codigo.Text + BTN.Text;
                    btnnum.PerformClick();
                }
                txt_codigo.Text = txt_codigo.Text + BTN.Text;
                txt_codigo.Focus();
            }
        }
        public static bool nombre;
        public static bool codigo;

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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn5);
        }

        private void btn6_Click_1(object sender, EventArgs e)
        {
            CALLBUTTON(btn6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn9);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn12);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn13);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn14);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn15);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn16);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn17);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn18);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn19);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn20);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn21);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn22);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn23);
        }

        private void btn24_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn24);
        }

        private void btn25_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn25);
        }

        private void btn26_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn26);
        }

        private void btn27_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btn27);
        }

        private void btncoma_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btncoma);
        }

        private void btnpunto_Click(object sender, EventArgs e)
        {
            CALLBUTTON(btnpunto);
        }

        private void btnEspacio_Click(object sender, EventArgs e)
        {
            if (nombre==true)
            {
                txt_nombre.Text = txt_nombre.Text + " ";
            }
            else if (codigo==true)
            {
                txt_codigo.Text = txt_codigo.Text + " ";
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (nombre == true)
            {
                txt_nombre.Text = txt_nombre.Text + Environment.NewLine;
            }
            else if (codigo == true)
            {
                txt_codigo.Text = txt_codigo.Text + Environment.NewLine;
            }
        }

        private void txt_nombre_Click(object sender, EventArgs e)
        {
            nombre = true;
            codigo = false;
        }

        private void txt_codigo_Click(object sender, EventArgs e)
        {
            codigo = true;
            nombre = false; 
        }

        private void btnShitt_Click(object sender, EventArgs e)
        {
            if ((btnShitt.BackColor == Color.LightGreen))
            {
                btnShitt.BackColor = Color.Silver;

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
                btnpunto.Text = ".";
                btncoma.Text = "@";

                btn20.Text = "ñ";
                btn21.Text = "z";
                btn22.Text = "x";
                btn23.Text = "c";
                btn24.Text = "v";
                btn25.Text = "b";
                btn26.Text = "n";
                btn27.Text = "m";
            }
            else
            {
                btnShitt.BackColor = Color.LightGreen;

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
                btnpunto.Text = "'";
                btncoma.Text = ",";

                btn20.Text = "Ñ";
                btn21.Text = "Z";
                btn22.Text = "X";
                btn23.Text = "C";
                btn24.Text = "V";
                btn25.Text = "B";
                btn26.Text = "N";
                btn27.Text = "M";
            }
        }

        private void btnnum_Click(object sender, EventArgs e)
        {
            if ((btnnum.BackColor == Color.LightGreen))
            {
                btnnum.BackColor = Color.Silver;

                btn1.Text = "1";
                btn2.Text = "2";
                btn3.Text = "3";
                btn4.Text = "4";
                btn5.Text = "5";
                btn6.Text = "6";
                btn7.Text = "7";
                btn8.Text = "8";
                btn9.Text = "9";
                btn10.Text = "10";

                btn11.Text = "!";
                btn12.Text = "#";
                btn13.Text = "$";
                btn14.Text = "%";
                btn15.Text = "&&";
                btn16.Text = "/";
                btn17.Text = "(";
                btn18.Text = ")";
                btn19.Text = "=";
                btnpunto.Text = ".";
                btncoma.Text = ",";

                btn20.Text = "-";
                btn21.Text = "+";
                btn22.Text = "?";
                btn23.Text = ":";
                btn24.Text = ";";
                btn25.Text = "\\";
                btn26.Text = "<";
                btn27.Text = ">";
            }
            else
            {
                btnnum.BackColor = Color.LightGreen;

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
                btnpunto.Text = ".";
                btncoma.Text = "@";

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
