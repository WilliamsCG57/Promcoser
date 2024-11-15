using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Data;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public RolController(PromcoserDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var rolesActivos = await _context.Rol.Where(r => r.Estado).ToListAsync();
            return Ok(rolesActivos);
        }

        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {   
            var rolesActivos = await _context.Rol.Where(r => !r.Estado).ToListAsync();
            return Ok(rolesActivos);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PutRol(Rol rol)
        {
            
            if (!RolExists(rol.IdRol))
            {
                return NotFound();
            }

            _context.Entry(rol).State = EntityState.Modified;

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

        [HttpPost("Create")]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllActive), new { id = rol.IdRol }, rol);
        }

        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoRol(int id)
        {
            var rol = await _context.Rol.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }
            rol.Estado = true;

            _context.Entry(rol).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoRol(int id)
        {
            var rol = await _context.Rol.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }
            rol.Estado = false;

            _context.Entry(rol).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _context.Rol.Any(e => e.IdRol == id);
        }
    }

}
