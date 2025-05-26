using Oracle.ManagedDataAccess.Client;
using System.Data;
using Microsoft.Extensions.Configuration;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;

namespace InscripcionesService.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IConfiguration _config;

        public InscripcionService(IConfiguration config)
        {
            _config = config;
        }

        public async Task CrearInscripcionAsync(InscripcionDto inscripcion)
        {
            Console.WriteLine("➡ [InscripcionService] Preparando conexión a Oracle...");

            using var connection = new OracleConnection(_config.GetConnectionString("OracleDb"));
            using var command = connection.CreateCommand();

            command.CommandText = @"
                INSERT INTO INSCRIPCIONES (PROGRAMA_ID, NOMBRE_ESTUDIANTE, CORREO_ESTUDIANTE, FECHA_INSCRIPCION)
                VALUES (:programaId, :nombre, :correo, :fecha)";

            command.Parameters.Add("programaId", OracleDbType.Int32).Value = inscripcion.ProgramaId;
            command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = inscripcion.NombreEstudiante;
            command.Parameters.Add("correo", OracleDbType.Varchar2).Value = inscripcion.CorreoEstudiante;

            var fecha = inscripcion.FechaInscripcion == default ? DateTime.UtcNow : inscripcion.FechaInscripcion;
            command.Parameters.Add("fecha", OracleDbType.Date).Value = fecha;

            Console.WriteLine("🟡 Conectando a Oracle...");
            await connection.OpenAsync();
            Console.WriteLine("✅ Conectado. Ejecutando INSERT...");

            int rowsAffected = await command.ExecuteNonQueryAsync();
            Console.WriteLine($"✅ INSERT ejecutado. Filas afectadas: {rowsAffected}");
        }
    }
}

