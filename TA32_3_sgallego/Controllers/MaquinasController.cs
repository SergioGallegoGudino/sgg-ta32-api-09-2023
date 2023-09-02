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
    public class MaquinasController : ControllerBase
    {
        private readonly ProductoContext _context;

        public MaquinasController(ProductoContext context)
        {
            _context = context;
        }

        [HttpGet("porPiso/{piso}")]

        public async Task<ActionResult<IEnumerable<MaquinaDTO>>> GetNombres(int piso)
        {
            var maquina = await _context.Maquinas
                .Where(p => p.Piso== piso)
                .Select(p => new MaquinaDTO
                {
                    Codigo = p.Codigo,
                    Piso = p.Piso
                })
                .ToListAsync();
            if (maquina == null)
            {
                return NotFound();
            }

            return maquina;
        }

        // GET: api/Maquinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquina>>> GetMaquinas()
        {
          if (_context.Maquinas == null)
          {
              return NotFound();
          }
            return await _context.Maquinas.ToListAsync();
        }

        // GET: api/Maquinas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquina>> GetMaquina(int id)
        {
          if (_context.Maquinas == null)
          {
              return NotFound();
          }
            var maquina = await _context.Maquinas.FindAsync(id);

            if (maquina == null)
            {
                return NotFound();
            }

            return maquina;
        }

        // PUT: api/Maquinas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquina(int id, Maquina maquina)
        {
            if (id != maquina.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(maquina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(id))
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

        // POST: api/Maquinas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maquina>> PostMaquina(Maquina maquina)
        {
          if (_context.Maquinas == null)
          {
              return Problem("Entity set 'ProductoContext.Maquinas'  is null.");
          }
            _context.Maquinas.Add(maquina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaquina", new { id = maquina.Codigo }, maquina);
        }

        // DELETE: api/Maquinas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquina(int id)
        {
            if (_context.Maquinas == null)
            {
                return NotFound();
            }
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return NotFound();
            }

            _context.Maquinas.Remove(maquina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaquinaExists(int id)
        {
            return (_context.Maquinas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
