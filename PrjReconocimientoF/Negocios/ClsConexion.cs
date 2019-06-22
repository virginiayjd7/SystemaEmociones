using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace PrjReconocimientoF.Negocios
{
    class ClsConexion
    {
        public static OleDbConnection Conex;

        public static void Conectar()
        {
            Conex = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = UsersFace.mdb");
            Conex.Open();
        }

        public static void Desconectar()
        {
            Conex.Close();
        }
    }
}
