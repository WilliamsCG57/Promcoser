using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Data;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaquinariaController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public MaquinariaController(PromcoserDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var entidadesActivas = await _context.Maquinaria
                .Where(r => r.Estado)
                .Include(m => m.IdMarcaNavigation) // Incluye la relación con Marca
                .Select(m => new
                {
                    m.IdMaquinaria,
                    m.IdMarcaNavigation.NombreMarca,
                    m.Placa,
                    m.Modelo,
                    m.HorometroCompra,
                    m.HorometroActual,
                    m.TipoMaquinaria,
                    m.AnoFabricacion,
                    m.Estado,
                    m.IdMarca
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var entidadesInactivas = await _context.Maquinaria
                .Where(r => !r.Estado)
                .Include(m => m.IdMarcaNavigation) // Incluye la relación con Marca
                .Select(m => new
                {
                    m.IdMaquinaria,
                    m.IdMarcaNavigation.NombreMarca,
                    m.Placa,
                    m.Modelo,
                    m.HorometroCompra,
                    m.HorometroActual,
                    m.TipoMaquinaria,
                    m.AnoFabricacion,
                    m.Estado,
                    m.IdMarca
                })
                .ToListAsync();
            return Ok(entidadesInactivas);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PutEntidad(MaquinariaUpdateDTO entidadDTO)
        {
            var maquinariaExistente = await _context.Maquinaria
                .Include(m => m.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.IdMaquinaria == entidadDTO.IdMaquinaria);

            if (maquinariaExistente == null)
            {
                return NotFound();
            }

            maquinariaExistente.Placa = entidadDTO.Placa;
            maquinariaExistente.Modelo = entidadDTO.Modelo;
            maquinariaExistente.HorometroCompra = entidadDTO.HorometroCompra;
            maquinariaExistente.HorometroActual = entidadDTO.HorometroActual;
            maquinariaExistente.TipoMaquinaria = entidadDTO.TipoMaquinaria;
            maquinariaExistente.AnoFabricacion = entidadDTO.AnoFabricacion;
            maquinariaExistente.Estado = entidadDTO.Estado;

            if (maquinariaExistente.IdMarca != entidadDTO.IdMarca)
            {
                maquinariaExistente.IdMarca = entidadDTO.IdMarca;
            }

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
        public async Task<ActionResult<Maquinaria>> PostEntidad([FromBody] MaquinariaDTO dto)
        {
            var entidad = new Maquinaria
            {
                IdMarca = dto.IdMarca,
                Placa = dto.Placa,
                Modelo = dto.Modelo,
                HorometroCompra = dto.HorometroCompra,
                HorometroActual = dto.HorometroActual,
                TipoMaquinaria = dto.TipoMaquinaria,
                AnoFabricacion = dto.AnoFabricacion,
                Estado = dto.Estado
            };

            _context.Maquinaria.Add(entidad);
            await _context.SaveChangesAsync();  

            return CreatedAtAction(nameof(GetAllActive), new { id = entidad.IdMaquinaria }, entidad);
        }

        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoEntidad(int id)
        {
            var entidad = await _context.Maquinaria.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = true;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoEntidad(int id)
        {
            var entidad = await _context.Maquinaria.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = false;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaquinariaExists(int id)
        {
            return _context.Maquinaria.Any(e => e.IdMaquinaria == id);
        }
    }
}
