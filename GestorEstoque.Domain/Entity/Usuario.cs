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

        [Key]
        public int UsuarioId { get; set; }
        [MaxLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;
        public byte[] Senha { get; set; }
        public byte[] SenhaSal { get; set; }
        [MaxLength(150)] 
        public string Email { get; set; } = string.Empty;
        [MaxLength(15)]
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
