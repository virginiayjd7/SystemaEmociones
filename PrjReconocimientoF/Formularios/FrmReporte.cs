using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace PrjReconocimientoF.Formularios
{
    public partial class FrmReporte : Form
    {
        public FrmReporte()
        {
            InitializeComponent();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            //chart2.Dispose();
            string molesto = Frm_EstadoAnimo.contenido[1];
            string asqueado = Frm_EstadoAnimo.contenido[2];
            string disgustado = Frm_EstadoAnimo.contenido[3];
            string asustado = Frm_EstadoAnimo.contenido[4];
            string feliz = Frm_EstadoAnimo.contenido[5];
            string neutral = Frm_EstadoAnimo.contenido[6];
            string triste = Frm_EstadoAnimo.contenido[7];
            string sorprendido = Frm_EstadoAnimo.contenido[8];

            molesto = molesto.Replace("Asqueado", "");
            molesto = molesto.Replace(" ", "");
            molesto = molesto.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            asqueado = asqueado.Replace("Disgustado", "");
            asqueado = asqueado.Replace(" ", "");
            asqueado = asqueado.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            disgustado = disgustado.Replace("Asustado", "");
            disgustado = disgustado.Replace(" ", "");
            disgustado = disgustado.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            asustado = asustado.Replace("Feliz", "");
            asustado = asustado.Replace(" ", "");
            asustado = asustado.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            feliz = feliz.Replace("Neutral", "");
            feliz = feliz.Replace(" ", "");
            feliz = feliz.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            neutral = neutral.Replace("Triste", "");
            neutral = neutral.Replace(" ", "");
            neutral = neutral.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            triste = triste.Replace("Sorprendido", "");
            triste = triste.Replace(" ", "");
            triste = triste.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            sorprendido = sorprendido.Replace("blur", "");
            sorprendido = sorprendido.Replace(" ", "");
            sorprendido = sorprendido.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");

            molesto = molesto.Replace(".", ",");
            asqueado = asqueado.Replace(".", ",");
            disgustado =disgustado.Replace(".", ",");
            asustado = asustado.Replace(".", ",");
            feliz = feliz.Replace(".", ",");
            neutral = neutral.Replace(".", ",");
            triste = triste.Replace(".", ",");
            sorprendido =sorprendido.Replace(".", ",");


            string[] cadena = new string[8];
            cadena[0] = molesto;
            cadena[1] = asqueado;
            cadena[2] = disgustado;
            cadena[3] = asustado;
            cadena[4] = feliz;
            cadena[5] = neutral;
            cadena[6] = triste;
            cadena[7] = sorprendido;

            Double molesto1 =    Convert.ToDouble(cadena[0]);
            Double asqueado1 =   Convert.ToDouble(cadena[1]);
            Double disgustado1=  Convert.ToDouble(cadena[2]);
            Double asustado1 =   Convert.ToDouble(cadena[3]);
            Double feliz1 =      Convert.ToDouble(cadena[4]);
            Double neutral1=     Convert.ToDouble(cadena[5]);
            Double triste1 =     Convert.ToDouble(cadena[6]);
            Double sorprendid1 = Convert.ToDouble(cadena[7]);

            try
            {





                string[] series = { "Molesto", "Asqueado", "Disgustado","Asustado", "Feliz", "Neutral", "Triste", "Sorprendido" };
                //  string[] series = { "Eduardo", "joge", "cris" };
                
                double[] puntos = { molesto1, asqueado1, disgustado1,asustado1,feliz1,neutral1,triste1,sorprendid1 };
                chart1.Palette = ChartColorPalette.Pastel;
                chart1.Titles.Add("Estado de Animo");

                for (int y = 0; y < series.Length; y++)
                {
                    Series serie = chart1.Series.Add(series[y]);
                    serie.Label =cadena[y];
                    serie.Points.Add(puntos[y]);

                }

            }
            catch (Exception)
            {

                MessageBox.Show("No hay Datos");

            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            /*Frm_EstadoAnimo frm = new Frm_EstadoAnimo();
            frm.Show();
            this.Hide();*/
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
