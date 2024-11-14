using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class CarrinhoProduto
    {
        [Key]
        public int CarrinhoProdutoId { get; set; }

        public int QuantidadeItem { get; set; }
        public double ValorItem { get; set; }
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public Carrinho Carrinho { get; set; }
        public Produto Produto { get; set; }
    }
}
