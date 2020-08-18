using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace PrjReconocimientoF.Entidad
{
    public class EUsuarioRostro
    {
        public int IdImage { get; set; }
        public string Name { get; set; }
        public string dni { get; set; }
        public string Face { get; set; }
    }
}
