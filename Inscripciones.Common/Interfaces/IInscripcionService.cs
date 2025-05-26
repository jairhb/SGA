using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using System.Threading.Tasks;

namespace Inscripciones.Common.Interfaces
{
    public interface IInscripcionService
    {
        Task CrearInscripcionAsync(InscripcionDto inscripcion);
    }
}
