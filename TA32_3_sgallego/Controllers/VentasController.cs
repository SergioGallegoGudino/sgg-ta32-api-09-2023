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
    public class VentasController : ControllerBase
    {
        private readonly ProductoContext _context;

        public VentasController(ProductoContext context)
        {
            _context = context;
        }

        [HttpGet("porCodigoCajero/{cajero}")]

        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetNombres(int codigo)
        {
            var venta = await _context.Ventas
                .Where(p => p.CajeroCodigo == codigo)
                .Select(p => new VentaDTO
                {
                    CajeroCodigo = p.CajeroCodigo,
                    MaquinaCodigo = p.MaquinaCodigo,
                    ProductoCodigo = p.ProductoCodigo
                })
                .ToListAsync();
            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            return await _context.Ventas.ToListAsync();
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, Venta venta)
        {
            if (id != venta.MaquinaCodigo)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
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

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
          if (_context.Ventas == null)
          {
              return Problem("Entity set 'ProductoContext.Ventas'  is null.");
          }
            _context.Ventas.Add(venta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VentaExists(venta.MaquinaCodigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVenta", new { id = venta.MaquinaCodigo }, venta);
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            if (_context.Ventas == null)
            {
                return NotFound();
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaExists(int id)
        {
            return (_context.Ventas?.Any(e => e.MaquinaCodigo == id)).GetValueOrDefault();
        }
    }
}
