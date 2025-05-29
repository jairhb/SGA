using System.Collections.Generic;
using System.Threading.Tasks;
using Inscripciones.Common.DTOs;

namespace Inscripciones.Common.Interfaces
{
    public interface IProgramaService
    {
        Task<IEnumerable<ProgramaDTO>> ObtenerProgramas();
    }
}



