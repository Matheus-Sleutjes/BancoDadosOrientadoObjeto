using GestorEstoque.Application.Contract;
using GestorEstoque.Data;
using GestorEstoque.Data.Contract;
using GestorEstoque.Domain.Dto;
using GestorEstoque.Domain.Entity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            var (senhaHash, senhaSal) = HashPassword(dto.Senha);
            var usuario = new Usuario(dto.NomeCompleto, senhaHash, senhaSal, dto.Email, dto.Telefone);

            retorno = await _usuarioRepository.Add(usuario);

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

            var (senhaHash, senhaSal) = HashPassword(senhaNova);

            usuario.AtualizarSenha(senhaSal, senhaHash);

            return await _usuarioRepository.Update(usuario);
        }

        public async Task<UsuarioDto?> Login(UsuarioDto dto)
        {
            var usuario = await _usuarioRepository.FindByEmail(dto.Email);
            if (usuario == null) return null;

            if (VerifyPassword(dto.Senha, usuario.Senha, usuario.SenhaSal))
            {
                var token = GenerateToken(usuario.NomeCompleto);
                var response = new UsuarioDto() { NomeCompleto = usuario.NomeCompleto, Token = token };
                return response;
            }

            return null;
        }

        public async Task<bool> Remove(int usuarioId)
        {
            var usuario = await _usuarioRepository.Find(usuarioId);
            if (usuario == null) return false;

            return await _usuarioRepository.Remove(usuarioId);
        }

        public async Task<List<UsuarioDto>> Paginacao(Paginacao paginacao)
        {
            return await _usuarioRepository.Paginacao(paginacao);
        }

        private string GenerateToken(string nomeCompleto )
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nomeCompleto),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Utils.KeyToken));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private (string senhaHash, string salt) HashPassword(string senha)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,  // Aumenta a dificuldade de brute force
                numBytesRequested: 32
            );

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        private bool VerifyPassword(string senhaDigitada, string senhaHashArmazenada, string saltArmazenado)
        {
            byte[] salt = Convert.FromBase64String(saltArmazenado);
            byte[] hashArmazenado = Convert.FromBase64String(senhaHashArmazenada);

            byte[] hashDigitado = KeyDerivation.Pbkdf2(
                password: senhaDigitada,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100000,
                numBytesRequested: 32
            );

            return CryptographicOperations.FixedTimeEquals(hashDigitado, hashArmazenado);
        }
    }
}
