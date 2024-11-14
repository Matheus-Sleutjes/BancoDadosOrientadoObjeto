using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class StatusCarrinho
    {
        [Key]
        public int StatusCarrinhoId { get; set; }
        [MaxLength(40)]
        public string Descricao { get; set; } = string.Empty;
    }
}
