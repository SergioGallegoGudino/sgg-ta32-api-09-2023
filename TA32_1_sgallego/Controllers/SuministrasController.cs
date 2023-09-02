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
    public class SuministrasController : ControllerBase
    {
        private readonly PiezaContext _context;

        public SuministrasController(PiezaContext context)
        {
            _context = context;
        }

        [HttpGet("porPrecio/{precio}")]

        public async Task<ActionResult<IEnumerable<SuministraDTO>>> GetNombres(int precio)
        {
            var suministra = await _context.Suministras
                .Where(p => p.Precio == precio)
                .Select(p => new SuministraDTO
                {
                    PiezaCodigo = p.PiezaCodigo,
                    ProveedorId = p.ProveedorId,
                    Precio = p.Precio
                })
                .ToListAsync();
            if (suministra == null)
            {
                return NotFound();
            }

            return suministra;
        }

        // GET: api/Suministras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suministra>>> GetSuministras()
        {
          if (_context.Suministras == null)
          {
              return NotFound();
          }
            return await _context.Suministras.ToListAsync();
        }

        // GET: api/Suministras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suministra>> GetSuministra(string id)
        {
          if (_context.Suministras == null)
          {
              return NotFound();
          }
            var suministra = await _context.Suministras.FindAsync(id);

            if (suministra == null)
            {
                return NotFound();
            }

            return suministra;
        }

        // PUT: api/Suministras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuministra(string id, Suministra suministra)
        {
            if (id != suministra.ProveedorId)
            {
                return BadRequest();
            }

            _context.Entry(suministra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuministraExists(id))
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

        // POST: api/Suministras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suministra>> PostSuministra(Suministra suministra)
        {
          if (_context.Suministras == null)
          {
              return Problem("Entity set 'PiezaContext.Suministras'  is null.");
          }
            _context.Suministras.Add(suministra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SuministraExists(suministra.ProveedorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSuministra", new { id = suministra.ProveedorId }, suministra);
        }

        // DELETE: api/Suministras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuministra(string id)
        {
            if (_context.Suministras == null)
            {
                return NotFound();
            }
            var suministra = await _context.Suministras.FindAsync(id);
            if (suministra == null)
            {
                return NotFound();
            }

            _context.Suministras.Remove(suministra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuministraExists(string id)
        {
            return (_context.Suministras?.Any(e => e.ProveedorId == id)).GetValueOrDefault();
        }
    }
}
