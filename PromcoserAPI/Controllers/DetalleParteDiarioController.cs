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
    public class DetalleParteDiarioController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public DetalleParteDiarioController(PromcoserDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllActive/{idParteDiario}")]
        public async Task<IActionResult> GetAllActive(int idParteDiario)
        {
            var entidadesActivas = await _context.DetalleParteDiario
                .Where(r => r.Estado && r.IdParteDiarioNavigation.IdParteDiario == idParteDiario)
                .Include(d => d.IdParteDiarioNavigation)
                .Select(d => new
                {
                    d.IdDetalleParteDiario,
                    d.IdParteDiarioNavigation.IdParteDiario,
                    d.HoraInicio,
                    d.HoraFin,
                    d.TrabajoEfectuado,
                    d.Ocurrencias,
                    d.Estado
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var entidadesInactivas = await _context.DetalleParteDiario
                .Where(r => !r.Estado)
                .Include(d => d.IdParteDiarioNavigation) // Incluye la relación con Marca
                .Select(d => new
                {
                    d.IdDetalleParteDiario,
                    d.IdParteDiarioNavigation.IdParteDiario,
                    d.HoraInicio,
                    d.HoraFin,
                    d.TrabajoEfectuado,
                    d.Ocurrencias,
                    d.Estado
                })
                .ToListAsync();

            return Ok(entidadesInactivas);
        }

        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoEntidad(int id)
        {
            var entidad = await _context.DetalleParteDiario.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = true;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PutEntidad(DetalleParteDiarioUpdateDTO entidadDTO)
        {
            var detallePartoExistente = await _context.DetalleParteDiario
                .FirstOrDefaultAsync(m => m.IdDetalleParteDiario == entidadDTO.IdDetalleParteDiario);

            if (detallePartoExistente == null)
            {
                return NotFound();
            }

            detallePartoExistente.IdParteDiario = entidadDTO.IdParteDiario;
            detallePartoExistente.HoraInicio = entidadDTO.HoraInicio;
            detallePartoExistente.HoraFin = entidadDTO.HoraFin;
            detallePartoExistente.TrabajoEfectuado = entidadDTO.TrabajoEfectuado;
            detallePartoExistente.Ocurrencias = entidadDTO.Ocurrencias;
            detallePartoExistente.Estado = entidadDTO.Estado;

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
        public async Task<ActionResult<Maquinaria>> PostEntidad([FromBody] DetalleParteDiarioDTO dto)
        {
            var entidad = new DetalleParteDiario
            {
                IdParteDiario = dto.IdParteDiario,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                TrabajoEfectuado = dto.TrabajoEfectuado,
                Ocurrencias = dto.Ocurrencias,
                Estado = dto.Estado
            };

            _context.DetalleParteDiario.Add(entidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllActive), new { id = entidad.IdDetalleParteDiario }, entidad);
        }

        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoEntidad(int id)
        {
            var entidad = await _context.DetalleParteDiario.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = false;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleParteDiarioExists(int id)
        {
            return _context.DetalleParteDiario.Any(e => e.IdDetalleParteDiario == id);
        }
    }
}
