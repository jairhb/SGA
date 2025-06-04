using System.Threading.Tasks;
using Inscripciones.Common.DTOs;


//obtener el pago que simulé 
namespace Inscripciones.Common.Interfaces
{
    public interface IPagoService
    {
        Task RegistrarPagoAsync(PagoDTO pago);
        Task<PagoConsultaDTO?> ObtenerPagoPorInscripcionId(int inscripcionId);
    }
}




