using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace PrjReconocimientoF.Entidad
{
    public class EAsistencia
    {
        public string idAsistencia { get; set; }
        public string dni { get; set; }
        public string hentrada { get; set; }
        public string hsalida { get; set; }
        
    }
}
