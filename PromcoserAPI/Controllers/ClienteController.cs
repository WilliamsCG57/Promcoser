using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteRepository.GetClientes();
            return Ok(clientes);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteRepository.GetClienteById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            await _clienteRepository.Insert(cliente);
            return Ok(cliente);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Cliente cliente)
        {
            bool result = await _clienteRepository.Update(cliente);
            if (!result)
                return BadRequest();
            return Ok(cliente.IdCliente);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _clienteRepository.Delete(id);
            if (!result)
                return BadRequest();
            return Ok(id);
        }
    }

    
}
