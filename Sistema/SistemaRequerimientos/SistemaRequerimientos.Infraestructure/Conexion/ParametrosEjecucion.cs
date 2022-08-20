using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRequerimientos.Infraestructure.Conexion
{
    public class ParametrosEjecucion
    {
        public string? NombreProcedimiento { get; set; }
        public SqlParametrosDapper[]? DapperParametros { get; set; }
        public DataTable? DtParametrosEntrada { get; set; }
    }
}
