using System.Net.Http;
using System.Net.Http.Json;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ProgramasService.Data
{
    public class ProgramaService : IProgramaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProgramaService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["Apis:Programas"];
        }

        public async Task<IEnumerable<ProgramaDTO>> ObtenerProgramas()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProgramaDTO>>($"{_baseUrl}/api/programas");
            return response ?? new List<ProgramaDTO>();
        }
    }
}



