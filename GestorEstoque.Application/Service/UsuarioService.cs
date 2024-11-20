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
                Ativo = usuario.Ativo
            };
        }

        public async Task<bool> Update(int usuarioId, UsuarioDto dto)
        {
            var usuario = await _usuarioRepository.Find(usuarioId);
            if (usuario == null) return false;

            usuario.AtualizarAtributos(dto);

            if (!usuario.Validar()) return false;

            return await _usuarioRepository.Update(usuario);
        }

        public async Task<bool> UpdateSenha(int usuarioId, string senhaNova)
        {
            var usuario = await _usuarioRepository.Find(usuarioId);
            if (usuario == null) return false;

            using (var hmac = new HMACSHA512())
            {
                var senhaSal = hmac.Key;
                var senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senhaNova));

                usuario.AtualizarSenha(senhaSal, senhaHash);
            }

            return await _usuarioRepository.Update(usuario);
        }

        public async Task<UsuarioDto?> Login(UsuarioDto dto)
        {
            var usuario = await _usuarioRepository.FindByEmail(dto.Email);
            if (usuario == null) return null;

            using (var hmac = new HMACSHA512(usuario.SenhaSal))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Senha));
                if (computedHash.SequenceEqual(usuario.Senha))
                    return new UsuarioDto() { NomeCompleto = usuario.NomeCompleto };

                return null;
            }
        }

        public async Task<bool> Remove(int usuarioId)
        {
            var usuario = await _usuarioRepository.Find(usuarioId);
            if (usuario == null) return false;

            return await _usuarioRepository.Remove(usuarioId);
        }
    }
}
