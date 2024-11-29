using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Data;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoGeneralController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public InfoGeneralController(PromcoserDbContext context)
        {
            _context = context;
        }
        
        [Authorize]
        [HttpGet("Resumen")]
        public async Task<IActionResult> GetResumen()
        {
            var personalActivo = await _context.Personal.CountAsync(p => p.Estado);
            var personalInactivo = await _context.Personal.CountAsync(p => !p.Estado);

            var maquinariaActivo = await _context.Maquinaria.CountAsync(m => m.Estado);
            var maquinariaInactivo = await _context.Maquinaria.CountAsync(m => !m.Estado);

            var clienteActivo = await _context.Cliente.CountAsync(c => c.Estado);
            var clienteInactivo = await _context.Cliente.CountAsync(c => !c.Estado);

            var partesPorLugarTrabajo = await _context.ParteDiario
                .Where(pd => pd.Estado)
                .GroupBy(pd => pd.IdLugarTrabajoNavigation.Descripcion)
                .Select(g => new
                {
                    LugarTrabajo = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

            var response = new
            {
                Personal = new
                {
                    Activo = personalActivo,
                    Inactivo = personalInactivo
                },
                Maquinaria = new
                {
                    Activo = maquinariaActivo,
                    Inactivo = maquinariaInactivo
                },
                Cliente = new
                {
                    Activo = clienteActivo,
                    Inactivo = clienteInactivo
                },
                ParteDiarioPorLugarTrabajo = partesPorLugarTrabajo
            };

            return Ok(response);
        }
    }

}
