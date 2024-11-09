using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromcoserDOMAIN.Core.DTOs;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using PromcoserDOMAIN.Infrastructure.Repositories;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalService _personalService;

        public PersonalController(IPersonalService personalService)
        {
            _personalService = personalService;
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
            if (userAuthDTO.Usuario == "" || userAuthDTO.Contrasena == "") return BadRequest();
            //TODO: Mejorar el userService con DTO
            var result = await _personalService.SignIn(userAuthDTO.Usuario, userAuthDTO.Contrasena);
            if (result == null) return NotFound();
            return Ok(result);
        }


    }
}




