namespace GestorEstoque.Domain.Entity
{
    public class CarrinhoProduto
    {
        public int CarrinhoProdutoId { get; set; }

        public int QuantidadeItem { get; set; }
        public double ValorTotalItem { get; set; }
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public Produto? Produto { get; set; }
    }
}
