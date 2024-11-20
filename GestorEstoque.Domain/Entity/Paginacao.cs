namespace GestorEstoque.Domain.Entity
{
    public class Paginacao
    {
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
        public string Sort { get; set; } = "Desc";
        public string Pesquisa { get; set; } = string.Empty;
    }
}
