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

using AForge.Video;
using AForge.Video.DirectShow;
namespace PrjReconocimientoF.Formularios
{
    public partial class Frm_EstadoAnimo : Form
    {
        public static string[] contenido = new string[100];
        public static string[] contenido2 = new string[100];
        const string subcriptionKey = "2fa290bdbeeb4663a7cc0f93ed9b76e7";
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect";
        public int heigth, width;
        public string[] Labels;
        DBCon dbc = new DBCon();
        int con = 0;
        //VARIABLE PARA LISTA DE DISPOSITIVOS
        private FilterInfoCollection dispositivosDeVideo;
        //DECLARANDO TODAS LAS VARIABLES, vectores y  haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade Face;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.4d, 0.4d);
        Image<Gray, byte> result = null, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;

        private void Frm_EstadoAnimo_Load(object sender, EventArgs e)
        {
            dispositivosDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo dispositivo in dispositivosDeVideo)
            {
                cmbCamara.Items.Add(dispositivo.Name);
            }
            cmbCamara.SelectedIndex = 0;
            Reconocer();
        }

        private void Reconocer()
        {
            try
            {//inicio el dispositivo de captura
                grabber = new Capture(1);
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
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    t = t + 1;
                    result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                    //DIBUJAR EL CUADRO PARA EL ROSTRO
                    currentFrame.Draw(f.rect, new Bgr(Color.FromArgb(0, 122, 204)), 1);
                    if (trainingImages.ToArray().Length != 0)
                    {
                        //CLASE PARA RECONOCIMIENTO CON EL NUMERO DE IMAGENES
                        MCvTermCriteria termCriteria = new MCvTermCriteria(ContTrain, 0.001);
                        //CLASE ELIGEN PARA RECONOCIMIENTO DE ROSTRO
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), ref termCriteria);
                        var fa = new Image<Gray, byte>[trainingImages.Count];//currenframe.conver<bitmap>
                        name = recognizer.Recognize(result);
                        //dibujar el nombre para cada rostro detectado y rconocido
                        currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));

                    }
                    NamePersons[t - 1] = name;
                    NamePersons.Add("");
                    //establecer el numero de rsotro detectados
                    lblNumeroDetect.Text = facesDetected[0].Length.ToString();
                    lblNadie.Text = name;
                }
                t = 0;
                //nombres concatenados de todos los rostro reconocidos
                for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
                {
                    names = names + NamePersons[nnn] + ",";
                }
                //mostrar los rostros procesados y reconocidos
                imageBox1.Image = currentFrame;
                name = "";
                //borrra la lista de nombre
                NamePersons.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string ruta = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //Image<Bgr, Byte> MarcoActual = grabber.QueryFrame().Resize(400, 300, INTER.CV_INTER_AREA);
            //MarcoActual._Flip(FLIP.HORIZONTAL);
            //MakeAnalysisRequest((Image)MarcoActual.ToBitmap());

            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Title = "Abrir Archivo";
            abrir.Filter = "Choose Image(*.jpg; *.png; *.gif)| *.jpg; *.png; *.gif";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                ruta = abrir.FileName;

            }
            MakeAnalysisRequest(ruta);
        }

        private void Desconectar()
        {
            Application.Idle -= new EventHandler(FrameGrabber);
            grabber.Dispose();
            imageBox1.ImageLocation = "img/1.png";
            lblNadie.Text = string.Empty;
            lblNumeroDetect.Text = string.Empty;
            BtnDesconectar.Text = "conectar";
        }
        public Frm_EstadoAnimo()
        {
            InitializeComponent();
            heigth = this.Height; width = this.Width;
            //cargamos la deteeicion de las caras por haarcascade
            Face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                dbc.obtenerByLesImagen();
                //carga de caras y etiquetas para cada imagen
                string[] Labels = dbc.Name;
                NumLabels = dbc.TotalUser;
                ContTrain = NumLabels;
                for (int tf = 0; tf < NumLabels; tf++)
                {
                    con = tf;
                    Bitmap bmp = new Bitmap(dbc.ConvertByteToImg(con));
                    trainingImages.Add(new Image<Gray, byte>(bmp));//carga ka foto con este nombre
                    labels.Add(Labels[tf]);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e + "No Hay Ningun rostro Registrado).", "Cargar rostros", MessageBoxButtons.OK);
            }
        }
        async void MakeAnalysisRequest(string nombreimagen)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-key", subcriptionKey);
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            string uri = uriBase + "?" + requestParameters;
            HttpResponseMessage response;
            byte[] byteData = ImageToByteArray(nombreimagen);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                try
                {
                    response = await client.PostAsync(uri, content);
                    string contentString = await response.Content.ReadAsStringAsync();
                    //label1.Text = JsonPrettyPrint(contentString);
                    string junto = JsonPrettyPrint(contentString);
                    contenido = junto.Split(':');
                    //lblNadie.Text = contenido[1];
                    //listBox1.Items.Add(JsonPrettyPrint(contentString));
                    FrmReporte frm = new FrmReporte();
                    frm.Show();
                    this.Hide();
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("No se puede enviar la peticion a HTTP", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void BtnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Idle -= new EventHandler(FrameGrabber);//Detenemos el evento de captura
                grabber.Dispose();//Dejamos de usar la clase para capturar usar los dispositivos
                imageBox1.ImageLocation = "img/1.png";//reiniciamos la imagen del control
                BtnDetectar.Enabled = true;
                BtnDesconectar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarImagen_Click(object sender, EventArgs e)
        {
            //Image imagen;
            //SaveFileDialog carp = new SaveFileDialog();
            //carp.Filter = "Archivos jpg (.jpg) |*.jpg";
            //carp.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //imagen = (imageBox2.Image).Bitmap;

            //if (carp.ShowDialog() == DialogResult.OK)
            //{
            //    imagen.Save(carp.FileName);
            //    imagen.Dispose();
            //}
            //else
            //    imagen.Dispose();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            /*FrmReporte frm = new FrmReporte();
            frm.Show();
            this.Hide();*/
        }

        private void btnTomarFoto_Click(object sender, EventArgs e)
        {

            try
            {
                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

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
                //labels.Add(txt_nombre.Text); //agrega npmbre de base de datos

                //Show face added in gray scale
                imageBox2.Image = TrainedFace;
                //dbc.ConvertImgToBinary(txt_nombre.Text, txt_codigo.Text, imageBox2.Image.Bitmap); //codigo de base de datos

                MessageBox.Show("Agregado correctamente", "Capturado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Image imagen;
                SaveFileDialog carp = new SaveFileDialog();
                carp.Filter = "Archivos jpg (.jpg) |*.jpg";
                carp.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                imagen = (imageBox2.Image).Bitmap;

                if (carp.ShowDialog() == DialogResult.OK)
                {
                    imagen.Save(carp.FileName);
                    imagen.Dispose();
                }
                else
                    imagen.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public byte[] ImageToByteArray(string nombreimagen)
        {
            //using (var ms = new MemoryStream())
            //{
            //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    return ms.ToArray();
            //}

            using (FileStream fileStream =
                new FileStream(nombreimagen, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
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
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            string texto = sb.ToString().Trim();
            
            if (texto.Length < 10)
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
                texto = texto.Replace("contempt", " Asqueado");
                texto = texto.Replace("disgust", " Disgustado");
                texto = texto.Replace("fear", " Asustado");
                texto = texto.Replace("happiness", " Feliz");
                texto = texto.Replace("neutral", " Neutral");
                texto = texto.Replace("sadness", " Triste");
                texto = texto.Replace("surprise", " Sorprendido");
                //int cantidad = texto.Length;
                return texto;
            }
        }
    
        private void BtnDetectar_Click(object sender, EventArgs e)
        {
            //switch (BtnDesconectar.Text)
            //{
            //    case "conectar":
            //        Reconocer();
            //        BtnDesconectar.Text = "desconectar";
            //        break;

            //    case "desconctar":
            //        Desconectar();
            //        break;

            //}

            try
            {
                //Inicia la Captura            
                grabber = new Capture(cmbCamara.SelectedIndex);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
