using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using PrjReconocimientoF.Negocios;
using PrjReconocimientoF.Entidad;

namespace PrjReconocimientoF.Negocios
{
    class ClsNegReconocimientoFacial
    {
        static OleDbCommand Comando;
        static OleDbDataAdapter Adaptador;
        static OleDbCommandBuilder Constructor;
        static DataTable Tabla;
        static BindingSource Fuente;

        public BindingSource Source
        {
            get
            {
                return Fuente;
            }
        }

        public void MtdGuardar(ClsEntAsistencia objasis)
        {
            string Consulta = "Insert Into Asistencia (Codigo,Nombres,Fecha,HoraE,HoraS,Estado) values ('" + 
                objasis.codigo + "','" + objasis.nombres + "','" + objasis.fecha + "','" + 
                objasis.hentrada + "','" + objasis.hsalida + "','" + objasis.estado + "');";
            Comando = new OleDbCommand();
            Comando.CommandText = Consulta;
            ClsConexion.Conectar();
            Comando.Connection = ClsConexion.Conex;
            Comando.ExecuteNonQuery();
            ClsConexion.Desconectar();
        }

        public void MtdBuscar(ClsEntAsistencia objasis)
        {
            string Consulta = "Select * from Asistencia where Nombres='" + objasis.nombres + "' AND Fecha='" + objasis.fecha 
                + "' AND Estado='" + objasis.estado + "';";
            ClsConexion.Conectar();     
            Adaptador = new OleDbDataAdapter(Consulta, ClsConexion.Conex);
            Constructor = new OleDbCommandBuilder(Adaptador);
            Tabla = new DataTable("Asistencia");
            Adaptador.Fill(Tabla);
            Fuente = new BindingSource();
            Fuente.DataSource = Tabla;
        }

        public void Listar()
        {
            string Consulta = "Select Id,Codigo,Nombres,Fecha,HoraE,HoraS from Asistencia;";
            ClsConexion.Conectar();//            
            Adaptador = new OleDbDataAdapter(Consulta, ClsConexion.Conex);
            Constructor = new OleDbCommandBuilder(Adaptador);
            Tabla = new DataTable("Asistencia");
            Adaptador.Fill(Tabla);
            Fuente = new BindingSource();
            Fuente.DataSource = Tabla;
        }
        public DataTable ListarAdministrador()
        {
            string Consulta = "Select Id,Codigo,Nombres,Fecha,HoraE,HoraS from Asistencia;";
            ClsConexion.Conectar();//            
            Adaptador = new OleDbDataAdapter(Consulta, ClsConexion.Conex);
            Constructor = new OleDbCommandBuilder(Adaptador);
            Tabla = new DataTable("Asistencia");
            Adaptador.Fill(Tabla);
            Fuente = new BindingSource();
            Fuente.DataSource = Tabla;
            return Tabla;
        }

        public void MtdModificar(ClsEntAsistencia objasis)
        {
            string Consulta = "UPDATE Asistencia SET " +
                "HoraS= '" + objasis.hsalida + "'," +
                "Estado= '" + objasis.estado + "';";
            Comando = new OleDbCommand();
            Comando.CommandText = Consulta;
            ClsConexion.Conectar();
            Comando.Connection = ClsConexion.Conex;
            Comando.ExecuteNonQuery();
            ClsConexion.Desconectar();
        }
    }
}
