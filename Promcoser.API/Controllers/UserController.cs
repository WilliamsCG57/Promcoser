﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Promcoser.DOMAIN.Core.DTOs;
using Promcoser.DOMAIN.Core.Entities;
using Promcoser.DOMAIN.Core.Interfaces;

namespace Promcoser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUsuarios();
            return Ok(users);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetUsuarioById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Usuarios user)
        {
            await _userRepository.Insert(user);
            return Ok(user);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Usuarios user)
        {
            bool result = await _userRepository.Update(user);
            if (!result)
                return BadRequest();

            return Ok(user.Id);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _userRepository.Delete(id);
            if (!result)
                return BadRequest();
            return Ok(id);
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserRequestAuthDTO UserRequestAuthDTO)
        {
            var result = await _userRepository.SignUp(UserRequestAuthDTO);
            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] UserAuthDTO UserAuthDTO)
        {
            if(UserAuthDTO.Email == "" || UserAuthDTO.Password == "") return BadRequest();
            var result = await _userRepository.SignIn(UserAuthDTO.Email, UserAuthDTO.Password);
            if (result == null) return NotFound();

            return Ok(result);
        }

    }
}
