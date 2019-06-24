using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using PrjReconocimientoF.Entidad;
namespace PrjReconocimientoF.Negocios
{
     public class NAsistencia
    {
        public DataTable MtdListarU()
        {
            ClsConexion connn = new ClsConexion();
            DataTable result = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connn.conecctar();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Usp_L_Listar_Usuarios";
            command.ExecuteNonQuery();
            adapter.SelectCommand = command;
            adapter.Fill(result);
            return result;


        }

        public Boolean MtdAgregarUsuario( EAsistencia obje)
        {
            try
            {


                ClsConexion ObjConexion = new ClsConexion();
                MySqlCommand ObjComando = new MySqlCommand();
                ObjComando.Connection = ObjConexion.conecctar();
                ObjComando.CommandText = "Usp_I_Insertar_Usuario";
                ObjComando.CommandType = CommandType.StoredProcedure;

                ObjComando.Parameters.Add(new MySqlParameter("pIdUsuario", MySqlDbType.Int32));
                ObjComando.Parameters.Add(new MySqlParameter("pdni", MySqlDbType.Int32));
                ObjComando.Parameters.Add(new MySqlParameter("pNombre", MySqlDbType.VarChar));
                ObjComando.Parameters.Add(new MySqlParameter("pClave", MySqlDbType.VarChar));
                ObjComando.Parameters["pIdUsuario"].Value = obje.idAsistencia;
                ObjComando.Parameters["pdni"].Value = obje.dni;
                ObjComando.Parameters["pNombre"].Value = obje.hentrada;
                ObjComando.Parameters["pClave"].Value = obje.hsalida;

                ObjComando.Connection = ObjConexion.conecctar();
                ObjComando.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public Boolean MtdActualizarUsuario(EAsistencia obje)
        {
            try
            {
                ClsConexion ObjConexion = new ClsConexion();
                MySqlCommand ObjComando = new MySqlCommand();

                ObjComando.Connection = ObjConexion.conecctar();
                ObjComando.CommandText = "Usp_U_Modificar_Usuarios";
                ObjComando.CommandType = CommandType.StoredProcedure;
                ObjComando.Parameters.Add(new MySqlParameter("pIdUsuario", MySqlDbType.Int32));
                ObjComando.Parameters.Add(new MySqlParameter("pdni", MySqlDbType.Int32));
                ObjComando.Parameters.Add(new MySqlParameter("pNombre", MySqlDbType.VarChar));
                ObjComando.Parameters.Add(new MySqlParameter("pClave", MySqlDbType.VarChar));
                ObjComando.Parameters["pIdUsuario"].Value = obje.idAsistencia;
                ObjComando.Parameters["pdni"].Value = obje.dni;
                ObjComando.Parameters["pNombre"].Value = obje.hentrada;
                ObjComando.Parameters["pClave"].Value = obje.hsalida;

                ObjComando.Connection = ObjConexion.conecctar();
                ObjComando.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public DataTable MtdBuscarUsuario(EAsistencia objEc)
        {
            ClsConexion conn = new ClsConexion();
            DataTable result = new DataTable();
            MySqlDataAdapter ObjAdapter = new MySqlDataAdapter();
            MySqlCommand ObjComando = new MySqlCommand();
            ObjComando.Connection = conn.conecctar();
            ObjComando.Parameters.Add(new MySqlParameter("pIdUsuario", MySqlDbType.Int32));
            ObjComando.Parameters["pIdUsuario"].Value = objEc.idAsistencia;
            ObjComando.CommandType = CommandType.StoredProcedure;
            ObjComando.CommandText = "Usp_S_Buscar_Usuario";
            ObjComando.CommandType = CommandType.StoredProcedure;
            ObjComando.ExecuteNonQuery();

            ObjAdapter.SelectCommand = ObjComando;
            ObjAdapter.Fill(result);

            return result;
        }

    }
}

