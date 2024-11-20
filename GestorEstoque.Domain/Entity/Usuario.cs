using GestorEstoque.Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class Usuario
    {
        public Usuario(){}

        public Usuario(string nomeCompleto, byte[] senhaHash, byte[] senhaSal, string email, string telefone)
        {
            NomeCompleto = nomeCompleto;
            Senha = senhaHash;
            SenhaSal = senhaSal;
            Email = email;
            Telefone = telefone;
            Ativo = true;
        }

        public int UsuarioId { get; set; }
        [MaxLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;
        public byte[]? Senha { get; set; }
        public byte[]? SenhaSal { get; set; }
        [MaxLength(150)] 
        public string Email { get; set; } = string.Empty;
        [MaxLength(15)]
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; }

        public void AtualizarAtributos(UsuarioDto dto)
        {
            NomeCompleto = dto.NomeCompleto ?? NomeCompleto;
            Email = dto.Email ?? Email;
            Telefone = dto.Telefone ?? Telefone;
            Ativo = dto.Ativo;
        }

        public void AtualizarSenha(byte[] senhaSal, byte[] senha)
        {
            Senha = senha;
            SenhaSal = senhaSal;
        }

        public bool Validar()
        {
            if (NomeCompleto.Length > 100) return false;
            if (Email.Length > 150) return false;
            if (Telefone.Length > 15) return false;

            return true;
        }
    }
}
