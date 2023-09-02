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
    public class InvestigadorsController : ControllerBase
    {
        private readonly InvestigadorContext _context;

        public InvestigadorsController(InvestigadorContext context)
        {
            _context = context;
        }

        [HttpGet("porDni/{nombre}")]

        public async Task<ActionResult<IEnumerable<InvestigadorDTO>>> GetDni(string dni)
        {
            var investigador = await _context.Investigadores
                .Where(p => p.Dni == dni)
                .Select(p => new InvestigadorDTO
                {
                    Dni = p.Dni,
                    NomApels = p.NomApels
                })
                .ToListAsync();
            if (investigador == null)
            {
                return NotFound();
            }

            return investigador;
        }

        // GET: api/Investigadors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investigador>>> GetInvestigadores()
        {
          if (_context.Investigadores == null)
          {
              return NotFound();
          }
            return await _context.Investigadores.ToListAsync();
        }

        // GET: api/Investigadors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investigador>> GetInvestigador(string id)
        {
          if (_context.Investigadores == null)
          {
              return NotFound();
          }
            var investigador = await _context.Investigadores.FindAsync(id);

            if (investigador == null)
            {
                return NotFound();
            }

            return investigador;
        }

        // PUT: api/Investigadors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigador(string id, Investigador investigador)
        {
            if (id != investigador.Dni)
            {
                return BadRequest();
            }

            _context.Entry(investigador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigadorExists(id))
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

        // POST: api/Investigadors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investigador>> PostInvestigador(Investigador investigador)
        {
          if (_context.Investigadores == null)
          {
              return Problem("Entity set 'InvestigadorContext.Investigadores'  is null.");
          }
            _context.Investigadores.Add(investigador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvestigadorExists(investigador.Dni))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvestigador", new { id = investigador.Dni }, investigador);
        }

        // DELETE: api/Investigadors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigador(string id)
        {
            if (_context.Investigadores == null)
            {
                return NotFound();
            }
            var investigador = await _context.Investigadores.FindAsync(id);
            if (investigador == null)
            {
                return NotFound();
            }

            _context.Investigadores.Remove(investigador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestigadorExists(string id)
        {
            return (_context.Investigadores?.Any(e => e.Dni == id)).GetValueOrDefault();
        }
    }
}
