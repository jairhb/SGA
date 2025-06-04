using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
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

        public async Task<int> CrearInscripcionAsync(InscripcionDTO inscripcion)
{
    Console.WriteLine("➡ [InscripcionService] Preparando conexión a Oracle...");

    using var connection = new OracleConnection(_config.GetConnectionString("OracleDb"));
    await connection.OpenAsync();

    using var command = connection.CreateCommand();

    // Usamos RETURNING INTO para obtener el ID generado
    command.CommandText = @"
        INSERT INTO INSCRIPCIONES (PROGRAMA_ID, NOMBRE_ESTUDIANTE, CORREO_ESTUDIANTE, FECHA_INSCRIPCION)
        VALUES (:programaId, :nombre, :correo, :fecha)
        RETURNING ID INTO :id";

    command.Parameters.Add("programaId", OracleDbType.Int32).Value = inscripcion.ProgramaId;
    command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = inscripcion.NombreEstudiante;
    command.Parameters.Add("correo", OracleDbType.Varchar2).Value = inscripcion.CorreoEstudiante;
    command.Parameters.Add("fecha", OracleDbType.Date).Value = inscripcion.FechaInscripcion;

    // Parámetro de salida para el ID
    var idParam = new OracleParameter("id", OracleDbType.Decimal)
    {
        Direction = ParameterDirection.Output
    };
    command.Parameters.Add(idParam);

    Console.WriteLine("🟡 Ejecutando INSERT con trigger...");
    await command.ExecuteNonQueryAsync();

    // Conversión segura del valor devuelto
    int idGenerado = Convert.ToInt32(((OracleDecimal)idParam.Value).Value);
    Console.WriteLine($"✅ INSERT ejecutado. ID generado: {idGenerado}");

    return idGenerado;
}

    }
}

