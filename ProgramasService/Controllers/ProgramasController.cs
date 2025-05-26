using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramasService.Data;
using ProgramasService.Models;

namespace ProgramasService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramasController : ControllerBase
    {
        private readonly ProgramasDbContext _context;

        public ProgramasController(ProgramasDbContext context)
        {
            _context = context;
        }

        // GET: /Programas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programa>>> GetProgramas()
        {
            return await _context.Programas.ToListAsync();
        }

        // GET: /Programas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programa>> GetPrograma(int id)
        {
            var programa = await _context.Programas.FindAsync(id);

            if (programa == null)
                return NotFound();

            return programa;
        }

        // POST: /Programas
        [HttpPost]
        public async Task<ActionResult<Programa>> PostPrograma(Programa programa)
        {
            _context.Programas.Add(programa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrograma), new { id = programa.Id }, programa);
        }

        // PUT: /Programas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrograma(int id, Programa programa)
        {
            if (id != programa.Id)
                return BadRequest();

            _context.Entry(programa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: /Programas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrograma(int id)
        {
            var programa = await _context.Programas.FindAsync(id);

            if (programa == null)
                return NotFound();

            _context.Programas.Remove(programa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}