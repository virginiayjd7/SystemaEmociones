using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace PrjReconocimientoF.Negocios
{
    class ClsNegocio_Conexion
    {

        public SqlConnection Conexion;
        public SqlConnection AbrirConexion()
        {

            try
            {
                Conexion = new SqlConnection("Data Source=DESKTOP-65NT5MH; Initial Catalog=CamSeg; Integrated security=True");
                if (Conexion.State == ConnectionState.Broken || Conexion.State == ConnectionState.Closed)
                {
                    Conexion.Open();
                    return Conexion;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Erroe al Tratar de Abrir la Conexion", e);
            }
            return Conexion;
        }

        public void CerrarConexion()
        {
            try
            {
                if (Conexion.State == ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de Cerrar Conexion", e);
            }
        }
    }
}
