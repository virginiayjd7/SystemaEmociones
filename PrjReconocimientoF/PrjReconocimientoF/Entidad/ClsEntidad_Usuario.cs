using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PrjReconocimientoF.Negocios;
using PrjReconocimientoF.Entidad;
namespace PrjReconocimientoF.Entidad
{
    class ClsEntidad_Usuario
    {
        ClsNegocio_Conexion con = new ClsNegocio_Conexion();
        //public bool MtdGuardar(ClsEntidad_Usuario obj)
        //{
        //    SqlCommand ObjSqlCommand = new SqlCommand();
        //    DataSet ObjDataSet = new DataSet();
        //    SqlDataAdapter ObjDataAdapter = new SqlDataAdapter(ObjSqlCommand);

        //    ObjSqlCommand.Connection = con.AbrirConexion();
        //    ObjSqlCommand.CommandType = CommandType.StoredProcedure;
        //    ObjSqlCommand.CommandText = "spu_addUsuario";
        //    //ObjSqlCommand.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.Int)).Value = ClsEObjCargo.Codigo;
        //    ObjSqlCommand.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = obj.Username;
        //    ObjSqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = obj.Password;
        //    ObjSqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar)).Value = obj.Email;
        //    //ObjSqlCommand.Parameters.Add(new SqlParameter("@FechaCreado", SqlDbType.VarChar)).Value = objELogin.FechaCreado;
        //    ObjSqlCommand.ExecuteNonQuery();
        //    con.CerrarConexion();
        //    return true;



        //    //OleDbConnection conexion = ClsNeConexion.ObtenerConexion();

        //    //OleDbCommand cmd = conexion.CreateCommand();
        //    //conexion.Open();
        //    //cmd.CommandText = "INSERT INTO Usuarios([Username], [Password], Email, FechaCreado) VALUES('" + objELogin.Username + "','" + objELogin.Password + "','" + objELogin.Email + "', NOW())";
        //    //cmd.Connection = conexion;
        //    //cmd.ExecuteNonQuery();

        //    //conexion.Close();
        //    //FrmWebCam.AlLogin.Add(objELogin);
        //}
    }
}
