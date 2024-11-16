using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;

namespace GestorEstoque.Data.Contract
{
    public interface IUsuarioRepository
    {
        Task<bool> Add(Usuario dto);
        Task<Usuario> Find(int id);
        Task<Usuario> FindByEmail(string email);
        //Usuario Find(int id);
        //Usuario GetByEmail(string email);
    }
}
