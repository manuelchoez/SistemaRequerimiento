using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRequerimientos.Infraestructure.Conexion
{
    public class SqlParametrosDapper
    {
        public string Name;
        public DbType? Type;
        public int Size;
        public object? Value;
        public ParameterDirection Direction;
        public byte Precision;
        public byte Scale;
        public bool IsTypeTable;

        public SqlParametrosDapper(string parametro, int tamanio)
        {
            Name = parametro;
            Size = tamanio;
            Value = null;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = null;
        }

        public SqlParametrosDapper(string parametro, DataTable tipo, bool esTipoTabla)
        {
            Name = parametro;
            Value = tipo;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            IsTypeTable = esTipoTabla;
        }

        public SqlParametrosDapper(string parametro, DbType? tipo, int tamanio)
        {
            Name = parametro;
            Size = tamanio;
            Value = null;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = tipo;
        }

        public SqlParametrosDapper(string parametro, DbType tipo, int tamanio, object valorParametro)
        {
            Name = parametro;
            Size = tamanio;
            Value = valorParametro;
            Direction = ParameterDirection.Input;
            Precision = 0;
            Scale = 0;
            Type = tipo;
        }
    }
}
