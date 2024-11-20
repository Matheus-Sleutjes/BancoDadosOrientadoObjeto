using GestorEstoque.Domain.Dto;

namespace GestorEstoque.Application.Contract
{
    public interface IUsuarioService
    {
        Task<bool> Add(UsuarioDto dto);
        Task<UsuarioDto> Find(int id);
        Task<bool> Update(int usuarioId, UsuarioDto dto);
        Task<bool> UpdateSenha(int usuarioId, string senhaNova);
        Task<UsuarioDto?> Login(UsuarioDto dto);
        Task<bool> Remove(int usuarioId);
    }
}
