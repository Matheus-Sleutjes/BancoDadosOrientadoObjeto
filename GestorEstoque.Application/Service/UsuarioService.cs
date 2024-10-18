using GestorEstoque.Application.Contract;
using GestorEstoque.Data.Contract;

namespace GestorEstoque.Application.Service
{
    public class UsuarioService(IUsuarioRepository _usuarioRepository) : IUsuarioService
    {
    }
}
