using GestorEstoque.Domain.Dto;

namespace GestorEstoque.Application.Contract
{
    public interface IUsuarioService
    {
        Task<bool> Add(UsuarioDto dto);
        Task<UsuarioDto> Find(int id);
    }
}
