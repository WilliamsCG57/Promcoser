using PromcoserDOMAIN.Core.Entities;

namespace PromcoserDOMAIN.Core.Interfaces
{
    public interface IPersonalRepository
    {
        Task<Personal> SignIn(string email, string pwd);
        Task<bool> SignUp(Personal user);
        Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword);
    }
}