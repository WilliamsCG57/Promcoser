using PromcoserDOMAIN.Core.Entities;

namespace PromcoserDOMAIN.Core.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> Delete(int id);
        Task<Cliente> GetClienteById(int id);
        Task<IEnumerable<Cliente>> GetClientes();
        Task Insert(Cliente cliente);
        Task<bool> Update(Cliente cliente);
    }
}