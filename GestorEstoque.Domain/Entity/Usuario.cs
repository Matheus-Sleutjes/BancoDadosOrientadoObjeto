using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class Usuario : Entity
    {
        [MaxLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public byte[] Senha { get; set; }
        public byte[] SenhaSal { get; set; }
        [MaxLength(200)] 
        public string Email { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Telefone { get; set; } = string.Empty;
    }
}
