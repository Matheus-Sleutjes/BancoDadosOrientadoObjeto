using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class Carrinho
    {
        [Key]
        public int CarrinhoId { get; set; }
        public DateTime DataCadastro { get; set; }
        [MaxLength(50)]
        public string MetodoPagamento { get; set; } = string.Empty;
    }
}
