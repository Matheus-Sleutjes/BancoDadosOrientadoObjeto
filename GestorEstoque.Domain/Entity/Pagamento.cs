namespace GestorEstoque.Domain.Entity
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }
        public DateTime DataPagamento { get; set; }
        public double ValorPagamento { get; set; }
        public int CarrinhoId { get; set;}
        public int MetodoPagamentoId { get; set;}
        public Carrinho? Carrinho { get; set; }
        public MetodoPagamento? MetodoPagamento { get; set;}
    }
}
