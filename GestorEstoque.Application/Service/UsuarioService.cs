using GestorEstoque.Application.Contract;
using GestorEstoque.Data.Contract;
using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;
using System.Security.Cryptography;
using System.Text;

namespace GestorEstoque.Application.Service
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<bool> Add(UsuarioDto dto)
        {
            var retorno = false;

            var existeUsuario = await _usuarioRepository.FindByEmail(dto.Email);
            if (existeUsuario != null) throw new Exception($"{dto.Email} ja existe!");

            using (var hmac = new HMACSHA512())
            {
                var senhaSal = hmac.Key;
                var senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Senha));
                var usuario = new Usuario(dto.NomeCompleto, senhaHash, senhaSal, dto.Email, dto.Telefone);

                retorno = await _usuarioRepository.Add(usuario);
            }

            return retorno;
        }

        public async Task<UsuarioDto> Find(int id)
        {
            var usuario = await _usuarioRepository.Find(id);
            if (usuario == null) return null;

            return new UsuarioDto()
            {
                UsuarioId = usuario.UsuarioId,
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
            };
        }
    }
}
