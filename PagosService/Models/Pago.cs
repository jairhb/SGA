using System;
namespace PagosService.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int InscripcionId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        
        public string Estado { get; set; } = "Pendiente";
    }
}