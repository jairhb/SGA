using InscripcionesWeb.DTOs;
using System.Net.Http.Json;

namespace InscripcionesWeb.Services
{
    public class InscripcionService
    {
        private readonly HttpClient _httpClient;

        public InscripcionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task EnviarInscripcionAsync(InscripcionDTO inscripcion)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5180/api/Inscripciones", inscripcion);
            response.EnsureSuccessStatusCode(); // Lanza excepción si falla
        }
    }
}

