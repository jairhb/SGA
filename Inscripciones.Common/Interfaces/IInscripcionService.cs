using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using System.Threading.Tasks;

namespace Inscripciones.Common.Interfaces

{
    public interface IInscripcionService
    {
        Task<int> CrearInscripcionAsync(InscripcionDTO inscripcion); // Ahora devuelve el ID generado
    }
}

