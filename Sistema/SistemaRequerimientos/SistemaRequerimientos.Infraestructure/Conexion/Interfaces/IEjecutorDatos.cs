using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRequerimientos.Infraestructure.Conexion.Interfaces
{
    public interface IEjecutorDatos
    {
        string CadenaConexion { get; set; }
        string Modulo { get; set; }
        IEnumerable<T> ExecuteDataSet<T>(ParametrosEjecucion parametrosEjecucion);
        T ExecuteScalar<T>(ParametrosEjecucion parametrosEjecucion);
        int ExecuteNonQuery(ParametrosEjecucion parametrosEjecucion);
        Task<IEnumerable<T>> ExecuteDataSetAsync<T>(ParametrosEjecucion parametrosEjecucion);
        Task<T> ExecuteScalarAsync<T>(ParametrosEjecucion parametrosEjecucion);
        Task<int> ExecuteNonQueryAsync(ParametrosEjecucion parametrosEjecucion);
    }
}
