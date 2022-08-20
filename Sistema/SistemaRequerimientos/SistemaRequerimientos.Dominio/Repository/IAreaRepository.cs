using SistemaRequerimientos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRequerimientos.Dominio.Repository
{
    public interface IAreaRepository
    {
        IEnumerable<Area> ConsultarArea();
        Area ConsultarArea(int idArea);
        Area EliminarArea(int idArea);
        Area ActualizarArea(Area Area);
        Area CrearArea(Area Area);
    }
}
