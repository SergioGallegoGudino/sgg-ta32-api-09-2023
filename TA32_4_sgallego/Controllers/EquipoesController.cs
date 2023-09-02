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
    public class EquipoesController : ControllerBase
    {
        private readonly InvestigadorContext _context;

        public EquipoesController(InvestigadorContext context)
        {
            _context = context;
        }

        [HttpGet("porNombre/{nombre}")]

        public async Task<ActionResult<IEnumerable<EquipoDTO>>> GetNombreEquipo(String nombre)
        {
            var equipo = await _context.Equipos
                .Where(p => p.Nombre == nombre)
                .Select(p => new EquipoDTO
                {
                    NumSerie = p.NumSerie,
                    Nombre = p.Nombre
                })
                .ToListAsync();
            if (equipo == null)
            {
                return NotFound();
            }

            return equipo;
        }

        // GET: api/Equipoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipos()
        {
          if (_context.Equipos == null)
          {
              return NotFound();
          }
            return await _context.Equipos.ToListAsync();
        }

        // GET: api/Equipoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipo>> GetEquipo(string id)
        {
          if (_context.Equipos == null)
          {
              return NotFound();
          }
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
            {
                return NotFound();
            }

            return equipo;
        }

        // PUT: api/Equipoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(string id, Equipo equipo)
        {
            if (id != equipo.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
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

        // POST: api/Equipoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipo>> PostEquipo(Equipo equipo)
        {
          if (_context.Equipos == null)
          {
              return Problem("Entity set 'InvestigadorContext.Equipos'  is null.");
          }
            _context.Equipos.Add(equipo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipoExists(equipo.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEquipo", new { id = equipo.NumSerie }, equipo);
        }

        // DELETE: api/Equipoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipo(string id)
        {
            if (_context.Equipos == null)
            {
                return NotFound();
            }
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipoExists(string id)
        {
            return (_context.Equipos?.Any(e => e.NumSerie == id)).GetValueOrDefault();
        }
    }
}
