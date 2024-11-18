using PromcoserDOMAIN.Core.DTOs;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromcoserDOMAIN.Core.Service
{
    public class PersonalService : IPersonalService
    {
        private readonly IPersonalRepository _personalRepository;
        private readonly IJWTService _jwtService;

        public PersonalService(IPersonalRepository personalRepository, IJWTService jwtService)
        {
            _personalRepository = personalRepository;
            _jwtService = jwtService;
        }

        public async Task<PersonalResponseAuthDTO> SignIn(string usuario, string password)
        {
            var user = await _personalRepository.SignIn(usuario, password);
            if (user == null) return null;


            var token = _jwtService.GenerateJWToken(user);
            var sendEmail = false;
            var personalDTO = new PersonalResponseAuthDTO()
            {
                IdPersonal = user.IdPersonal,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                FechaNacimiento = user.FechaNacimiento,
                Direccion = user.Direccion,
                CorreoElectronico = user.CorreoElectronico,
                Usuario = user.Usuario,
                Token = token,
                IsEmailSent = sendEmail
            };

            return personalDTO;
        }

        public async Task<bool> SignUp(PersonalRequestAuthDTO userDTO)
        {
            var user = new Personal();
            user.Direccion = userDTO.Direccion;
            user.CorreoElectronico = userDTO.CorreoElectronico;
            user.Nombre = userDTO.Nombre;
            user.Apellido = userDTO.Apellido;
            user.FechaNacimiento = userDTO.FechaNacimiento;
            user.Estado = true;
            return await _personalRepository.SignUp(user);
        }

        public async Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword)
        {
            return await _personalRepository.ChangePwd(usuario, oldPassword, newPassword);
        }
    }
}


