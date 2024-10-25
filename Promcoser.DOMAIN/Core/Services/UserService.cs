using Promcoser.DOMAIN.Core.DTOs;
using Promcoser.DOMAIN.Core.Entities;
using Promcoser.DOMAIN.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promcoser.DOMAIN.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<UserResponseAuthDTO> SignIn(string email, string password)
        {
            var user = await _userRepository.SignIn(email, password);
            if (user == null) return null;

            var token = "";
            var sendEmail = false;
            var userDTO = new UserResponseAuthDTO()
            {
                Id = (int)user.Id,
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                //Token = token,
                IsEmailSent = sendEmail
            };

            return userDTO;
        }


        public async Task<bool> SignUp(UserRequestAuthDTO userDTO)
        {
            var user = new Usuarios();
            user.Address = userDTO.Address;
            user.Email = userDTO.Email;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Country = userDTO.Country;
            user.DateOfBirth = userDTO.DateOfBirth;
            user.IsActive = true;
            user.Type = "U";
            return await _userRepository.SignUp(user);
        }

        Task<UserRequestAuthDTO> IUserService.SignIn(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
    }

