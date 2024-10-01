using GestorEstoque.Domain.Entity;

namespace GestorEstoque.Data.Contract
{
    public interface IUsuarioRepository
    {
        Usuario Find(int id);
        Usuario GetByEmail(string email);
    }
}
