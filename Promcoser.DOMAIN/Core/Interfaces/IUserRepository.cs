using Promcoser.DOMAIN.Core.DTOs;
using Promcoser.DOMAIN.Core.Entities;

namespace Promcoser.DOMAIN.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Delete(int id);
        Task<Usuarios> GetUsuarioById(int id);
        Task<IEnumerable<Usuarios>> GetUsuarios();
        Task Insert(Usuarios usuario);
        Task<bool> Update(Usuarios usuario);
        Task<bool> SignUp(UserRequestAuthDTO usuarioDTO);
        Task<UserResponseAuthDTO> SignIn(string email, string password);
    }
}