using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Domain.Entity
{
    public class MetodoPagamento
    {
        public int MetodoPagamentoId { get; set; }
        [MaxLength(20)]
        public string Descricao { get; set; } = string.Empty;
    }
}
