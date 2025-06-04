using System;
using System.Threading.Tasks;
using System.Data;
using Inscripciones.Common.DTOs;
using Inscripciones.Common.Interfaces;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace PagosService.Services
{
    public class PagoService : IPagoService
    {
        private readonly IConfiguration _config;

        public PagoService(IConfiguration config)
        {
            _config = config;
        }

        public async Task RegistrarPagoAsync(PagoDTO pago)
        {
            using var connection = new OracleConnection(_config.GetConnectionString("OracleDb"));
            await connection.OpenAsync();

            var fechaLimite = pago.FechaPago.AddDays(5);

            // Verificar si ya existe un pago para esta inscripción
            using (var checkCmd = connection.CreateCommand())
            {
                checkCmd.CommandText = "SELECT COUNT(*) FROM PAGOS WHERE INSCRIPCION_ID = :inscripcionId";
                checkCmd.Parameters.Add("inscripcionId", OracleDbType.Int32).Value = pago.InscripcionId;

                var existe = Convert.ToInt32(await checkCmd.ExecuteScalarAsync()) > 0;

                using var command = connection.CreateCommand();

                if (existe)
                {
                    // UPDATE actualizar el pago si ya existe
                    command.CommandText = @"
                        UPDATE PAGOS 
                        SET MONTO = :monto, FECHA_PAGO = :fechaPago, ESTADO = :estado, FECHA_LIMITE = :fechaLimite
                        WHERE INSCRIPCION_ID = :inscripcionId";

                    command.Parameters.Add("monto", OracleDbType.Decimal).Value = pago.Monto;
                    command.Parameters.Add("fechaPago", OracleDbType.Date).Value = pago.FechaPago;
                    command.Parameters.Add("estado", OracleDbType.Varchar2).Value = pago.Estado;
                    command.Parameters.Add("fechaLimite", OracleDbType.Date).Value = fechaLimite;
                    command.Parameters.Add("inscripcionId", OracleDbType.Int32).Value = pago.InscripcionId;
                }
                else
                {
                    // INSERT si no existe el pago para pruebas
                    command.CommandText = @"
                        INSERT INTO PAGOS (INSCRIPCION_ID, MONTO, FECHA_PAGO, ESTADO, FECHA_LIMITE)
                        VALUES (:inscripcionId, :monto, :fechaPago, :estado, :fechaLimite)";

                    command.Parameters.Add("inscripcionId", OracleDbType.Int32).Value = pago.InscripcionId;
                    command.Parameters.Add("monto", OracleDbType.Decimal).Value = pago.Monto;
                    command.Parameters.Add("fechaPago", OracleDbType.Date).Value = pago.FechaPago;
                    command.Parameters.Add("estado", OracleDbType.Varchar2).Value = pago.Estado;
                    command.Parameters.Add("fechaLimite", OracleDbType.Date).Value = fechaLimite;
                }

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<PagoConsultaDTO> ObtenerPagoPorInscripcionId(int inscripcionId)
        {
            using var connection = new OracleConnection(_config.GetConnectionString("OracleDb"));
            using var command = connection.CreateCommand();

            command.CommandText = @"
                SELECT ID, INSCRIPCION_ID, MONTO, FECHA_LIMITE, FECHA_PAGO, ESTADO
                FROM PAGOS
                WHERE INSCRIPCION_ID = :inscripcionId";

            command.Parameters.Add("inscripcionId", OracleDbType.Int32).Value = inscripcionId;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new PagoConsultaDTO
                {
                    Id = reader.GetInt32(0),
                    InscripcionId = reader.GetInt32(1),
                    Monto = reader.GetDecimal(2),
                    FechaLimite = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                    FechaPago = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                    Estado = reader.GetString(5)
                };
            }

            return null;
        }
    }
}



