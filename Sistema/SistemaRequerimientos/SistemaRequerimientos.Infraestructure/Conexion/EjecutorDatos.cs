using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRequerimientos.Infraestructure.Conexion
{
    public class EjecutorDatos
    {
        private readonly IConfiguration _configuration;
        private string cadenaConexion = string.Empty;
        private string modulo = string.Empty;
        private static Dictionary<string, string> _dictCadenaConexion = new Dictionary<string, string>();
        private object _lock = new object();
        private SqlParametrosDapper[] parametrosSalida;
        private int timeOut;
        private int numeroParametrosOut;
        public int TimeOut
        {
            get
            {
                return timeOut;
            }
        }

        public SqlParametrosDapper[] OutParametro
        {
            get
            {
                return (SqlParametrosDapper[])parametrosSalida.Clone();
            }
            set
            {
                parametrosSalida = value;
            }
        }


        public string CadenaConexion
        {
            get
            {
                return cadenaConexion;
            }
            set
            {
                if (!_dictCadenaConexion.ContainsKey(value))
                {
                    lock (_lock)
                    {
                        if (!_dictCadenaConexion.ContainsKey(value))
                        {
                            if (_configuration.GetConnectionString(value) != null)
                            {
                                cadenaConexion = _configuration.GetConnectionString(value);
                                timeOut = Convert.ToInt32(_configuration.GetSection(ConstantesEjecutorDatos.TimeOut).Value);
                            }
                            else
                            {
                                throw new ArgumentException(string.Format(ConstantesEjecutorDatos.ErrorCadenaConexion), value);
                            }
                        }
                    }
                }                      
            }
        }


        public string Modulo
        {
            get
            {
                return modulo;
            }
            set
            {
                modulo = value;
            }
        }

        private DynamicParameters AgregarParametrosConexion(SqlParametrosDapper[]? parametros)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                if (parametros != null)
                {
                    foreach (SqlParametrosDapper parametro in parametros)
                    {
                        if (parametro.IsTypeTable)
                        {
                            dynamicParameters.Add(parametro.Name, ((DataTable)parametro.Value).AsTableValuedParameter());
                        }
                        else
                        {
                            if (parametro.Type == DbType.Decimal)
                            {
                                parametro.Precision = 28;
                                parametro.Scale = 4;
                            }
                            dynamicParameters.Add(parametro.Name, parametro.Value, dbType: parametro.Type,
                                direction: parametro.Direction, size: parametro.Size,
                                precision: parametro.Precision, scale: parametro.Scale);

                            if (parametro.Direction == ParameterDirection.Output)
                            {
                                numeroParametrosOut++;
                            }
                            else if (parametro.Direction == ParameterDirection.ReturnValue)
                            {
                                parametro.Value = -999;
                            }
                        }
                    }
                }
                return dynamicParameters;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EjecutorDatos(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<T> ExecuteDataSet<T>(ParametrosEjecucion parametrosEjecucion)
        {
            IEnumerable<T> listaRetorno;
            try
            {
                using (IDbConnection conexion = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    listaRetorno = conexion.Query<T>(parametrosEjecucion.NombreProcedimiento, parametros, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return listaRetorno;
        }

        public async Task<IEnumerable<T>> ExecuteDataSetAsync<T>(ParametrosEjecucion parametrosEjecucion)
        {
            IEnumerable<T> listaRetorno;
            try
            {
                using (IDbConnection conexion = new SqlConnection())
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    listaRetorno = await conexion.QueryAsync<T>(parametrosEjecucion.NombreProcedimiento, parametros, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listaRetorno;
        }

        public int ExecuteNonQuery(ParametrosEjecucion parametrosEjecucion)
        {
            try
            {
                using (IDbConnection conexion = new SqlConnection())
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    return conexion.QuerySingle<int>(parametrosEjecucion.NombreProcedimiento, parametros, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(ParametrosEjecucion parametrosEjecucion)
        {
            try
            {
                using (IDbConnection conexion = new SqlConnection())
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    return await conexion.QuerySingleAsync<int>(parametrosEjecucion.NombreProcedimiento, parametros, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T ExecuteScalar<T>(ParametrosEjecucion parametrosEjecucion)
        {
            try
            {
                using (IDbConnection conexion = new SqlConnection())
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    return conexion.ExecuteScalar<T>(parametrosEjecucion.NombreProcedimiento, parametros, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(ParametrosEjecucion parametrosEjecucion)
        {
            try
            {
                using (IDbConnection conexion = new SqlConnection())
                {
                    DynamicParameters parametros = new DynamicParameters();
                    parametros = AgregarParametrosConexion(parametrosEjecucion.DapperParametros);
                    return await conexion.ExecuteScalarAsync<T>(parametrosEjecucion.NombreProcedimiento, parametros, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
