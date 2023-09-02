using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TA32_1_sgallego.Models;

namespace TA32_1_sgallego.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajeroesController : ControllerBase
    {
        private readonly ProductoContext _context;

        public CajeroesController(ProductoContext context)
        {
            _context = context;
        }

        [HttpGet("porNombre/{nombre}")]

        public async Task<ActionResult<IEnumerable<CajeroDTO>>> GetNombres(String nombre)
        {
            var cajero = await _context.Cajeros
                .Where(p => p.NomApels == nombre)
                .Select(p => new CajeroDTO
                {
                    Codigo = p.Codigo,
                    NomApels = p.NomApels
                })
                .ToListAsync();
            if (cajero == null)
            {
                return NotFound();
            }

            return cajero;
        }

        // GET: api/Cajeroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cajero>>> GetCajeros()
        {
          if (_context.Cajeros == null)
          {
              return NotFound();
          }
            return await _context.Cajeros.ToListAsync();
        }

        // GET: api/Cajeroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cajero>> GetCajero(int id)
        {
          if (_context.Cajeros == null)
          {
              return NotFound();
          }
            var cajero = await _context.Cajeros.FindAsync(id);

            if (cajero == null)
            {
                return NotFound();
            }

            return cajero;
        }

        // PUT: api/Cajeroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCajero(int id, Cajero cajero)
        {
            if (id != cajero.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(cajero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CajeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cajeroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cajero>> PostCajero(Cajero cajero)
        {
          if (_context.Cajeros == null)
          {
              return Problem("Entity set 'ProductoContext.Cajeros'  is null.");
          }
            _context.Cajeros.Add(cajero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCajero", new { id = cajero.Codigo }, cajero);
        }

        // DELETE: api/Cajeroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCajero(int id)
        {
            if (_context.Cajeros == null)
            {
                return NotFound();
            }
            var cajero = await _context.Cajeros.FindAsync(id);
            if (cajero == null)
            {
                return NotFound();
            }

            _context.Cajeros.Remove(cajero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CajeroExists(int id)
        {
            return (_context.Cajeros?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
