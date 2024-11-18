using PromcoserDOMAIN.Core.DTOs;

namespace PromcoserDOMAIN.Core.Interfaces
{
    public interface IPersonalService
    {
        Task<PersonalResponseAuthDTO> SignIn(string email, string password);
        Task<bool> SignUp(PersonalRequestAuthDTO userDTO);
        Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword);
    }
}