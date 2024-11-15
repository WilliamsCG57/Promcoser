using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.DTOs;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using PromcoserDOMAIN.Data;
using PromcoserDOMAIN.Infrastructure.Repositories;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        private readonly PromcoserDbContext _context;

        public PersonalController(IPersonalService personalService, PromcoserDbContext context)
        {
            _personalService = personalService;
            _context = context;
        }

        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var entidadesActivas = await _context.Personal
                .Where(r => r.Estado)
                .Include(p => p.IdRolNavigation) // Incluye la relación con Marca
                .Select(p => new
                {
                    p.IdPersonal,
                    p.Nombre,
                    p.Apellido,
                    p.IdRolNavigation.DescripcionRol,
                    p.Dni,
                    p.Telefono,
                    p.CorreoElectronico,
                    p.FechaIngreso,
                    p.FechaNacimiento,
                    p.Direccion,
                    p.IdRol,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var entidadesInactivas = await _context.Personal
                .Where(r => !r.Estado)
                .Include(p => p.IdRolNavigation) // Incluye la relación con Marca
                .Select(p => new
                {
                    p.IdPersonal,
                    p.Nombre,
                    p.Apellido,
                    p.IdRolNavigation.DescripcionRol,
                    p.Dni,
                    p.Telefono,
                    p.CorreoElectronico,
                    p.FechaIngreso,
                    p.FechaNacimiento,
                    p.Direccion,
                    p.IdRol,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesInactivas);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PutEntidad(PersonalUpdateDTO entidadDTO)
        {
            var personalExistente = await _context.Personal
                .Include(p => p.IdRolNavigation)
                .FirstOrDefaultAsync(p => p.IdPersonal == entidadDTO.IdPersonal);

            if (personalExistente == null)
            {
                return NotFound();
            }

            personalExistente.Nombre = entidadDTO.Nombre;
            personalExistente.Apellido = entidadDTO.Apellido;
            personalExistente.Dni = entidadDTO.Dni;
            personalExistente.CorreoElectronico = entidadDTO.CorreoElectronico;
            personalExistente.Telefono = entidadDTO.Telefono;
            personalExistente.FechaIngreso = entidadDTO.FechaIngreso;
            personalExistente.Direccion = entidadDTO.Direccion;
            personalExistente.FechaNacimiento = entidadDTO.FechaNacimiento;
            personalExistente.Estado = entidadDTO.Estado;

            if (personalExistente.IdRol != entidadDTO.IdRol)
            {
                personalExistente.IdRol = entidadDTO.IdRol;
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
        public async Task<ActionResult<Personal>> PostEntidad([FromBody] PersonalCreateDTO dto)
        {
            var entidad = new Personal
            {
                IdRol = dto.IdRol,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Dni = dto.Dni,
                Telefono = dto.Telefono,
                CorreoElectronico = dto.CorreoElectronico,
                FechaIngreso = dto.FechaIngreso,
                Direccion = dto.Direccion,
                FechaNacimiento = dto.FechaNacimiento,
                Estado = dto.Estado
            };

            _context.Personal.Add(entidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllActive), new { id = entidad.IdPersonal }, entidad);
        }

        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoEntidad(int id)
        {
            var entidad = await _context.Personal.FindAsync(id);

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
            var entidad = await _context.Personal.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = false;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] PersonalRequestAuthDTO personalRequestAuthDTO)
        {
            var result = await _personalService.SignUp(personalRequestAuthDTO);
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] PersonalAuthDTO userAuthDTO)
        {
            if (userAuthDTO.CorreoElectronico == "" || userAuthDTO.Contrasena == "") return BadRequest();
            //TODO: Mejorar el userService con DTO
            var result = await _personalService.SignIn(userAuthDTO.CorreoElectronico, userAuthDTO.Contrasena);
            if (result == null) return NotFound();
            return Ok(result);
        }

        private bool PersonalExists(int id)
        {
            return _context.Personal.Any(e => e.IdPersonal == id);
        }
    }
}




