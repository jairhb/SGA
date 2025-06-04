using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using InscripcionesWeb.DTOs;

namespace InscripcionesWeb.Services
{
    public class PagoService : IPagoService
    {
        private readonly HttpClient _httpClient;

        public PagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegistrarPagoAsync(PagoDTO pago)
        {
            var response = await _httpClient.PostAsJsonAsync("api/pagos", pago);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PagoConsultaDTO?> ObtenerPagoPorInscripcionId(int inscripcionId)
        {
            var response = await _httpClient.GetAsync($"api/pagos/por-inscripcion/{inscripcionId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PagoConsultaDTO>();
            }

            return null;
        }

        public Task CrearPago(PagoDTO pago)
        {
            throw new NotImplementedException();
        }
    }
}



