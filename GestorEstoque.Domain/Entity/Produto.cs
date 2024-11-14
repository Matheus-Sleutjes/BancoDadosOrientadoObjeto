using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;
        public double ValorProduto { get; set; } = double.MinValue;
    }
}
