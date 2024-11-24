using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using PromcoserDOMAIN.Data;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly PromcoserDbContext _context;

        public ClienteController(IClienteRepository clienteRepository, PromcoserDbContext context)
        {
            _clienteRepository = clienteRepository;
            _context = context;
        }

        
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var clientes = await _clienteRepository.GetClientes();
            var clientesActivos = clientes.Where(c => c.Estado).ToList();
            return Ok(clientesActivos);
        }

        [Authorize]
        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var clientes = await _clienteRepository.GetClientes();
            var clientesInactivos = clientes.Where(c => !c.Estado).ToList();
            return Ok(clientesInactivos);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            await _clienteRepository.Insert(cliente);
            return Ok(cliente);
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Cliente cliente)
        {
            bool result = await _clienteRepository.Update(cliente);
            if (!result)
                return BadRequest();
            return Ok(cliente.IdCliente);
        }

        [Authorize]
        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutEstadoCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }
            cliente.Estado = true;

            _context.Entry(cliente).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }
            cliente.Estado = false;

            _context.Entry(cliente).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    
}
