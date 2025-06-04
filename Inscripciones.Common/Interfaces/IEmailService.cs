using System.Threading.Tasks;
using Inscripciones.Common.DTOs;

namespace Inscripciones.Common.Interfaces
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(InscripcionDTO inscripcion);
        //Task EnviarCorreoConfirmacionPagoAsync(PagoConsultaDTO pago);
    }
}
