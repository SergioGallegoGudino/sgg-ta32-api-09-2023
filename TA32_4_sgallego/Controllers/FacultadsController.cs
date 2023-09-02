﻿using System;
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
    public class FacultadsController : ControllerBase
    {
        private readonly InvestigadorContext _context;

        public FacultadsController(InvestigadorContext context)
        {
            _context = context;
        }

        [HttpGet("porFacultad/{nombre}")]

        public async Task<ActionResult<IEnumerable<FacultadDTO>>> GetNombreFacultad(string nombre)
        {
            var facultad= await _context.Facultades
                .Where(p => p.Nombre == nombre)
                .Select(p => new FacultadDTO
                {
                    Codigo= p.Codigo,
                    Nombre = p.Nombre
                })
                .ToListAsync();
            if (facultad == null)
            {
                return NotFound();
            }

            return facultad;
        }

        // GET: api/Facultads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facultad>>> GetFacultades()
        {
          if (_context.Facultades == null)
          {
              return NotFound();
          }
            return await _context.Facultades.ToListAsync();
        }

        // GET: api/Facultads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facultad>> GetFacultad(int id)
        {
          if (_context.Facultades == null)
          {
              return NotFound();
          }
            var facultad = await _context.Facultades.FindAsync(id);

            if (facultad == null)
            {
                return NotFound();
            }

            return facultad;
        }

        // PUT: api/Facultads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacultad(int id, Facultad facultad)
        {
            if (id != facultad.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(facultad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultadExists(id))
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

        // POST: api/Facultads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facultad>> PostFacultad(Facultad facultad)
        {
          if (_context.Facultades == null)
          {
              return Problem("Entity set 'InvestigadorContext.Facultades'  is null.");
          }
            _context.Facultades.Add(facultad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacultad", new { id = facultad.Codigo }, facultad);
        }

        // DELETE: api/Facultads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacultad(int id)
        {
            if (_context.Facultades == null)
            {
                return NotFound();
            }
            var facultad = await _context.Facultades.FindAsync(id);
            if (facultad == null)
            {
                return NotFound();
            }

            _context.Facultades.Remove(facultad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacultadExists(int id)
        {
            return (_context.Facultades?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
