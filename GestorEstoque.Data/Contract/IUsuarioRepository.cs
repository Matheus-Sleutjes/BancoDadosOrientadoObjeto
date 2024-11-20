using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;

namespace GestorEstoque.Data.Contract
{
    public interface IUsuarioRepository
    {
        Task<bool> Add(Usuario dto);
        Task<Usuario> Find(int id);
        Task<Usuario> FindByEmail(string email);
        Task<bool> Update(Usuario usuario);
        Task<bool> Remove(int usuarioId);
        Task<List<UsuarioDto>> Paginacao(Paginacao paginacao);
    }
}
