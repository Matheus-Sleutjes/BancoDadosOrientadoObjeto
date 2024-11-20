using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        [MaxLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;
        [MaxLength(15)]
        public string CPF_CNPJ { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Endereco { get; set; } = string.Empty;
        [MaxLength(15)]
        public string Telefone { get; set; } = string.Empty;
    }
}
