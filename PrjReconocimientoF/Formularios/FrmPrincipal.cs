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
    public partial class FrmPrincipal : Form
    {
        const string subcriptionKey = "6b7cb2d9555f41439b7b5c86495a65e4";
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";
        public int heigth, width;
        public string[] Labels;
        DBCon dbc = new DBCon();
        int con = 0;

        //DECLARANDO TODAS LAS VARIABLES, vectores y  haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade Face;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.4d, 0.4d);
        Image<Gray, byte> result = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;

        private void FrameGrabber_Click(object sender, EventArgs e)
        {

        }

        private void FrmTabletReconocimientoFacial_Load(object sender, EventArgs e)
        {
            Reconocer();
        }

        private void Reconocer()
        {
            try
            {//inicio el dispositivo de captura
                grabber = new Capture();
                grabber.QueryFrame();
                //iniciar el evento framegraber
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
                currentFrame = grabber.QueryFrame().Resize(400, 300, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                currentFrame._Flip(FLIP.HORIZONTAL);
                //convertir a escala de grises
                gray = currentFrame.Convert<Gray, Byte>();

                //detector de rostros
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
                //ACCION PARA CADA ELEMENTO DETECTADO
                foreach(MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                    //DIBUJAR EL CUADRO PARA EL ROSTRO
                    currentFrame.Draw(f.rect, new Bgr(Color.FromArgb(0, 122, 204)), 1);
                    if(trainingImages.ToArray().Length !=0)
                    {
                        //CLASE PARA RECONOCIMIENTO CON EL NUMERO DE IMAGENES
                        MCvTermCriteria termCriteria = new MCvTermCriteria(ContTrain, 0.001);
                        //CLASE ELIGEN PARA RECONOCIMIENTO DE ROSTRO
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), ref termCriteria);
                        var fa = new Image<Gray, byte>[trainingImages.Count];//currenframe.conver<bitmap>
                        name = recognizer.Recognize(result);
                        //dibujar el nombre para cada rostro detectado y rconocido
                        currentFrame.Draw(name, ref font, new Point(f.rect.X -2, f.rect.Y - 2), new Bgr(Color.Red));

                    }
                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                    //establecer el numero de rsotro detectados
                    lblNumeroDetect.Text = facesDetected[0].Length.ToString();
                    lblNadie.Text = name;
                }
                t = 0;
                //nombres concatenados de todos los rostro reconocidos
                for(int nnn=0;nnn<facesDetected[0].Length;nnn++)
                {
                    names = names + NamePersons[nnn] + ",";
                }
                //mostrar los rostros procesados y reconocidos
                imageBoxFrameGrabber.Image = currentFrame;
                name = "";
                //borrra la lista de nombre
                NamePersons.Clear();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Desconectar()
        {
            Application.Idle -= new EventHandler(FrameGrabber);
            grabber.Dispose();
            imageBoxFrameGrabber.ImageLocation = "img/1.png";
            lblNadie.Text = string.Empty;
            lblNumeroDetect.Text = string.Empty;
            BtnDesconectar.Text = "conectar";


        }
        //int cont = 0;

        private void BtnDesconectar_Click(object sender, EventArgs e)
        {
            switch(BtnDesconectar.Text)
            {
                case "conectar":
                    Reconocer();
                    BtnDesconectar.Text = "desconectar";
                    break;

                case "desconctar":
                    Desconectar();
                    break;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> MarcoActual = grabber.QueryFrame().Resize(400, 300, INTER.CV_INTER_AREA);
            MarcoActual._Flip(FLIP.HORIZONTAL);
            MakeAnalysisRequest((Image)MarcoActual.ToBitmap());
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
        //    ReleaseCapture();
        //    Sendm
        //    w = this.width;
        //    h = this.Height;
        }

        public FrmPrincipal()
        {
            InitializeComponent();
            heigth = this.Height; width = this.Width;
            //cargamos la deteeicion de las caras por haarcascade
            Face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                dbc.obtenerByLesImagen();
                //carga de caras y etiquetas para cada imagen
                string[] labels = dbc.Name;
                NumLabels = dbc.TotalUser;
                ContTrain = NumLabels;
                for (int tf = 0; tf < NumLabels; tf++)
                {
                    con = tf;
                    Bitmap bmp = new Bitmap(dbc.ConvertByteToImg(con));
                    trainingImages.Add(new Image<Gray, byte>(bmp));//carga ka foto con este nombre
                    //labels.Add(Labels[tf]);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e + "No Hay Ningun rostro Registrado).", "Cargar rostros", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmReconocimientoFacial frm = new FrmReconocimientoFacial();
            frm.Show();
            this.Hide();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            FrmRegistro frm = new FrmRegistro();
            frm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        async void MakeAnalysisRequest(Image imagen)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-key", subcriptionKey);
            string requestParameters = "retunFaceId-false&returnFaceLandmarks-false&returnFaceRectangle-false&returnFaceAttributes-emotion";
            
            string uri = uriBase + "?" + requestParameters;
            HttpResponseMessage response;
            byte[] byteData = ImageToByteArray(imagen);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                try
                {
                    response = await client.PostAsync(uri, content);
                    string contentString = await response.Content.ReadAsStringAsync();
                    label5.Text = JsonPrettyPrint(contentString);
                }
               catch(HttpRequestException)
                {
                    MessageBox.Show("No se puede enviar la peticion a hhttp", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;
            json = json.Replace(Environment.NewLine,"").Replace("\t", "");
            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;
            foreach (char ch in json)
            {
                switch(ch)
                {
                    case '"':
                        if (!ignore) quote =!quote;
                    break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }
                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;

                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            sb.Append(ch);
                            break;

                        case ':':      
                            sb.Append(' ');
                            sb.Append(ch);
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;

                    }
               
                }
            }
            string texto = sb.ToString().Trim();
            if (texto.Length<10)
            {
                texto = "no detectado";
                return texto;
            }
            else
            {
                texto = texto.Remove(0, texto.IndexOf("anger"));
                texto = texto.Replace('"', ' ');
                texto = texto.Replace('}', ' ');
                texto = texto.Replace(']', ' ');
                texto = texto.Replace(",", "");
                texto = texto.Replace("            ", "");
                texto = texto.Replace("anger", " Molesto");
                texto = texto.Replace("contenpt", " Asqueado");
                texto = texto.Replace("disgust", " Asustado");
                texto = texto.Replace("happiness", " Feliz");
                texto = texto.Replace("neutral", " Neutral");
                texto = texto.Replace("sadness", " Triste");
                texto = texto.Replace("surprise", " Sorprendido");
                return texto;
            }
        }
    }
     
}
