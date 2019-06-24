using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using Reconocimiento_facial;
using PrjReconocimientoF.Entidad;
namespace PrjReconocimientoF.Negocios
{
     class NConexion
    {
        private MySqlConnection conn = null;
        static string user = "root";
        static string password = "";
        static string servidor = "localhost";//127.0.0.1
        static string basedatos = "bdsysplam";

        public MySqlConnection conecctar()
        {
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = "server=" + servidor + ";" + "database=" + basedatos + ";uid=" + user + ";" + "pwd=" + password + ";";
                conn.Open();
            }
            catch (MySqlException ex)

            {
                throw ex;

            }
            return conn;
        }
        public MySqlConnection cerrar()
        {
            try
            {
                conn = new MySqlConnection();
                conn.Close();
            }
            catch (MySqlException ex)

            {
                throw ex;

            }

            return conn;
        }
    }
}
