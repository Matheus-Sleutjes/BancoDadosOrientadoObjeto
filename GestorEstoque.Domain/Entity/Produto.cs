using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        [MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;
        public double ValorProduto { get; set; } = double.MinValue;
        public char TipoProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
