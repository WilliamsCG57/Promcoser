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
    public class LugarTrabajoController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public LugarTrabajoController(PromcoserDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var lugarTrabajoActivos = await _context.LugarTrabajo.Where(r => r.Estado == true).ToListAsync();
            return Ok(lugarTrabajoActivos);
        }

        [Authorize]
        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var lugarTrabajoActivos = await _context.LugarTrabajo.Where(r => !r.Estado == true).ToListAsync();
            return Ok(lugarTrabajoActivos);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> PutLugarTrabajo(LugarTrabajo lugarTrabajo)
        {

            if (!LugarTrabajoExists(lugarTrabajo.IdLugarTrabajo))
            {
                return NotFound();
            }

            _context.Entry(lugarTrabajo).State = EntityState.Modified;

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
        public async Task<ActionResult<LugarTrabajo>> PostLugarTrabajo(LugarTrabajo lugarTrabajo)
        {
            _context.LugarTrabajo.Add(lugarTrabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllActive), new { id = lugarTrabajo.IdLugarTrabajo }, lugarTrabajo);
        }

        [Authorize]
        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoLugarTrabajo(int id)
        {
            var lugarTrabajo = await _context.LugarTrabajo.FindAsync(id);

            if (lugarTrabajo == null)
            {
                return NotFound();
            }
            lugarTrabajo.Estado = true;

            _context.Entry(lugarTrabajo).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoLugarTrabajo(int id)
        {
            var lugarTrabajo = await _context.LugarTrabajo.FindAsync(id);

            if (lugarTrabajo == null)
            {
                return NotFound();
            }
            lugarTrabajo.Estado = false;

            _context.Entry(lugarTrabajo).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LugarTrabajoExists(int id)
        {
            return _context.LugarTrabajo.Any(e => e.IdLugarTrabajo == id);
        }
    }
}
