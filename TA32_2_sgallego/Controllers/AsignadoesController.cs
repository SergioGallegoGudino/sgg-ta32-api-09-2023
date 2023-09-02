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
    public class AsignadoesController : ControllerBase
    {
        private readonly CientificoContext _context;

        public AsignadoesController(CientificoContext context)
        {
            _context = context;
        }

        [HttpGet("porProyecto/{nombre}")]

        public async Task<ActionResult<IEnumerable<AsignadoDTO>>> GetNombres(int id)
        {
            var asignado = await _context.Asignados
                .Where(p => p.ProyectoId == id)
                .Select(p => new AsignadoDTO
                {
                    ProyectoId = p.ProyectoId,
                    CientificoDni= p.CientificoDni
                })
                .ToListAsync();
            if (asignado == null)
            {
                return NotFound();
            }

            return asignado;
        }

        // GET: api/Asignadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignado>>> GetAsignados()
        {
          if (_context.Asignados == null)
          {
              return NotFound();
          }
            return await _context.Asignados.ToListAsync();
        }

        // GET: api/Asignadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignado>> GetAsignado(string id)
        {
          if (_context.Asignados == null)
          {
              return NotFound();
          }
            var asignado = await _context.Asignados.FindAsync(id);

            if (asignado == null)
            {
                return NotFound();
            }

            return asignado;
        }

        // PUT: api/Asignadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignado(string id, Asignado asignado)
        {
            if (id != asignado.CientificoDni)
            {
                return BadRequest();
            }

            _context.Entry(asignado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignadoExists(id))
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

        // POST: api/Asignadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignado>> PostAsignado(Asignado asignado)
        {
          if (_context.Asignados == null)
          {
              return Problem("Entity set 'CientificoContext.Asignados'  is null.");
          }
            _context.Asignados.Add(asignado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AsignadoExists(asignado.CientificoDni))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAsignado", new { id = asignado.CientificoDni }, asignado);
        }

        // DELETE: api/Asignadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignado(string id)
        {
            if (_context.Asignados == null)
            {
                return NotFound();
            }
            var asignado = await _context.Asignados.FindAsync(id);
            if (asignado == null)
            {
                return NotFound();
            }

            _context.Asignados.Remove(asignado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignadoExists(string id)
        {
            return (_context.Asignados?.Any(e => e.CientificoDni == id)).GetValueOrDefault();
        }
    }
}
