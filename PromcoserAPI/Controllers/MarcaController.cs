using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Data;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public MarcaController(PromcoserDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var entidadesActivas = await _context.Marca.Where(r => r.Estado).ToListAsync();
            return Ok(entidadesActivas);
        }

        [Authorize]
        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var entidadesInactivas = await _context.Marca.Where(r => !r.Estado).ToListAsync();
            return Ok(entidadesInactivas);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> PutEntidad(Marca entidad)
        {

            if (!MarcaExists(entidad.IdMarca))
            {
                return NotFound();
            }

            _context.Entry(entidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<Rol>> PostEntidad(Marca entidad)
        {
            _context.Marca.Add(entidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllActive), new { id = entidad.IdMarca }, entidad);
        }

        [Authorize]
        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoEntidad(int id)
        {
            var entidad = await _context.Marca.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = true;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoEntidad(int id)
        {
            var entidad = await _context.Marca.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = false;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcaExists(int id)
        {
            return _context.Marca.Any(e => e.IdMarca == id);
        }
    }
}
