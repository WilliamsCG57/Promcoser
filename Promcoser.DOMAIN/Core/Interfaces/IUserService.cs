using Promcoser.DOMAIN.Core.DTOs;

namespace Promcoser.DOMAIN.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserRequestAuthDTO> SignIn(string email, string password);
        Task<bool> SignUp(UserRequestAuthDTO usuarioDTO);
    }
}